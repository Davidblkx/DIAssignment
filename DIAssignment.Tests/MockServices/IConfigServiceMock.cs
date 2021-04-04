using DIAssignment.Core.Services;
using Moq;
using System.Collections.Generic;

namespace DIAssignment.Tests.MockServices
{
    public static class IConfigServiceMock
    {
        public static IConfigService Build(Dictionary<string, string> keyValues)
        {
            var mock = new Mock<IConfigService>();

            mock.Setup(c => c.Get(It.IsAny<string>()))
                .Returns<string>(s => keyValues[s]);

            return mock.Object;
        }
    }
}
