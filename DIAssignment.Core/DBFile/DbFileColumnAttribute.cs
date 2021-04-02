using System;

namespace DIAssignment.Core.DBFile
{
    /// <summary>
    /// Attribute for serialization
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DbFileColumnAttribute : Attribute
    {
        public string Name { get;  }
        public DbFileColumnAttribute(string name)
        { Name = name; }
    }
}
