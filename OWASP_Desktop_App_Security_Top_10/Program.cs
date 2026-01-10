namespace OWASP_Desktop_App_Security_Top_10
{
    public class Publisher
    {
        public event EventHandler RaiseEvent;
        public void DoSomething()
        {
            RaiseEvent?.Invoke(this, EventArgs.Empty);
        }
    }

    public class Subscriber
    {
        public Subscriber(Publisher pub)
        {
            pub.RaiseEvent += Handler;
        }

        private void Handler(object sender, EventArgs e)
        {
            Console.WriteLine("Handled event.");
        }
    }
    static class Program
    {
        static void Main()
        {
            Publisher pub = new Publisher();
            Subscriber sub = new Subscriber(pub);

            pub.DoSomething();
        }
    }
}
