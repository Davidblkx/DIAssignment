using System;

namespace DIAssignment.Core.Models
{
    public abstract class Message
    {
        public long ID { get; set; }
        public Guid MessageID { get; set; }
        public DateTime DateTime { get; set; }

        public Message()
        {
            MessageID = Guid.NewGuid();
            DateTime = DateTime.UtcNow;
        }
    }
}
