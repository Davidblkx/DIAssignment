namespace DIAssignment.FileHandler
{
    class Program
    {
        static void Main()
        {
            FileHandlerService
                .Start()
                .WaitForExit();
        }
    }
}
