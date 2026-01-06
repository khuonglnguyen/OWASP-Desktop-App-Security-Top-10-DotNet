using Microsoft.Data.Sqlite;

class Program
{
    static void Main()
    {
        var normalInput = "Alice";
        var maliciousInput = "' OR '1'='1";

        using var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();

        using (var cmd = connection.CreateCommand())
        {
            cmd.CommandText = "CREATE TABLE Users (Id INTEGER PRIMARY KEY, username TEXT);";
            cmd.ExecuteNonQuery();

            // Vulnerable: insert using string literals (no parameters anywhere)
            cmd.CommandText = "INSERT INTO Users (username) VALUES ('Alice'), ('Bob'), ('Eve');";
            cmd.ExecuteNonQuery();
        }

        Console.WriteLine();
        ExecuteVulnerableQuery(connection, normalInput);
        ExecuteVulnerableQuery(connection, maliciousInput);
    }

    static string BuildVulnerableQuery(string username)
    {
        return "SELECT username FROM Users WHERE username = '" + username + "';";
    }

    static void ExecuteVulnerableQuery(SqliteConnection conn, string input)
    {
        var sql = BuildVulnerableQuery(input);
        Console.WriteLine("Input: " + QuoteForDisplay(input));
        Console.WriteLine("Query: " + sql);

        var rows = new List<string>();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            rows.Add(reader.GetString(0));
        }

        Console.WriteLine("Returned rows: " + (rows.Count == 0 ? "(none)" : string.Join(", ", rows)));
        Console.WriteLine();
    }

    static string QuoteForDisplay(string s) => s.Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t");
}
