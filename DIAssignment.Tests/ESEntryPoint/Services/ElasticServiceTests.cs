using Xunit;
using DIAssignment.ESEntryPoint.Services;
using DIAssignment.Tests.MockServices;
using System.Collections.Generic;

namespace DIAssignment.Tests.ESEntryPoint
{
    public class ElasticServiceTests
    {
        [Fact]
        public void TestIndexCreationAndInsert()
        {
            var config = IConfigServiceMock.Build(new Dictionary<string, string>
            {
                { "ELASTICSEARCH_URL", "http://localhost:9200" },
            });

            var elasticService = new ElasticService(config);

            // Only run tests when a elastic endpoint is available
            if (!elasticService.Ping()) { return; }

            var indexStatus = elasticService.EnsureIndex<TestIndex>();
            Assert.True(indexStatus);

            var subject = new TestIndex { Id = 123, Value = "potato" };
            var insertStatus = elasticService.Index(subject);
            Assert.True(insertStatus);
        }

        private class TestIndex
        {
            public long Id { get; set; }
            public string Value { get; set; } = "";
        }
    }
}
