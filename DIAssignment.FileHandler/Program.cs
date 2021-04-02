namespace DIAssignment.Core
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
