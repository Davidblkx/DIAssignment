namespace DIAssignment.EventStore
{
    class Program
    {
        static void Main()
        {
            EventStoreService.Start().WaitForExit();
        }
    }
}
