using DIAssignment.Core.Extensions;
using System.Collections.Generic;
using Xunit;

namespace DIAssignment.Tests.Core.Extensions
{
    public class CollectionTests
    {
        [Fact]
        public void MergeTest()
        {
            var subject = new Dictionary<string, string>
            {
                { "1", "a" },
                { "2", "b" },
                { "3", "c" }
            };

            subject.Merge(new Dictionary<string, string> { 
                { "1", "x" },
                { "4", "d" }
            });

            Assert.Equal(4, subject.Count);
            Assert.Equal("x", subject["1"]);
            Assert.Equal("d", subject["4"]);
        }
    }
}
