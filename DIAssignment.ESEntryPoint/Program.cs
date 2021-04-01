namespace DIAssignment.ESEntryPoint
{
    class Program
    {
        static void Main()
        {
            ESEntryPointService.Start().WaitForExit();
        }
    }
}
