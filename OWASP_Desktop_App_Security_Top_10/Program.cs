namespace OWASP_Desktop_App_Security_Top_10
{
    static class Program
    {
        static void Main()
        {

        }

        public static unsafe int SubarraySum(int[] array, int start, int end)  // Sensitive
        {
            var sum = 0;

            // Skip array bound checks for extra performance
            fixed (int* firstNumber = array)
            {
                for (int i = start; i < end; i++)
                    sum += *(firstNumber + i);
            }

            return sum;
        }
    }
}
