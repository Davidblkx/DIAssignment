using DIAssignment.Core.Services;
using Moq;
using System;
using System.Collections.Generic;

namespace DIAssignment.Tests.MockServices
{
    public class IInjectorServiceMock
    {
        public Mock<IInjector> Mock { get; }
        private readonly IConfigService _config;

        public IInjectorServiceMock(Dictionary<string, string> configValues = null)
        {
            Mock = new Mock<IInjector>();

            _config = IConfigServiceMock.Build(configValues ?? new Dictionary<string, string>());
            Mock.Setup(x => x.Get<IConfigService>())
                .Returns(_config);
        }

        public IInjectorServiceMock Register<T>(Func<IInjector, T> expression)
            where T : class
        {
            Mock.Setup(x => x.Get<T>())
                .Returns(expression(Mock.Object));

            return this;
        }

        public IInjector Build() => Mock.Object;
    }
}
