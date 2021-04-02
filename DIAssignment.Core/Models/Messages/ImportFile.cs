namespace DIAssignment.Core.Models.Messages
{
    /// <summary>
    /// Message to start a file import process
    /// </summary>
    public class ImportFile : Message
    {
        public string FileId { get; set; } = "";
        public ImportEntityType Type { get; set; }

        public class ImportFileStarted { }
        public class ImportFileFailed
        {
            public string Message { get; set; } = "";
        }
    }
}
