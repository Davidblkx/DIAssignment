using DIAssignment.Core.DBFile;

namespace DIAssignment.Core.Models.Messages
{
    public class ImportEntityMessage : Message
    {
        public ImportEntityType Type { get; set; }
        public DbFileRow? EntityRow { get; set; }

        public ImportEntityMessage() { }

        public ImportEntityMessage(ImportEntityType type, DbFileRow row)
        {
            Type = type;
            EntityRow = row;
        }
    }
}
