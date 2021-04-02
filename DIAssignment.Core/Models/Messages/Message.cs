using System;

namespace DIAssignment.Core.Models
{
    public abstract class Message
    {
        public Guid ID { get; }

        public Message()
        {
            ID = Guid.NewGuid();
        }
    }
}
