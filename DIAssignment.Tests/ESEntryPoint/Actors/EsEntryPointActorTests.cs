using Akka.Actor;
using Akka.TestKit.Xunit2;
using DIAssignment.Core.Models.Entity;
using DIAssignment.ESEntryPoint.Actors;
using DIAssignment.ESEntryPoint.Services;
using DIAssignment.Tests.MockServices;
using Moq;
using System;
using Xunit;

namespace DIAssignment.Tests.ESEntryPoint.Actors
{
    public class EsEntryPointActorTests : TestKit
    {
        [Fact]
        public void Test()
        {
            // Setup services mocks
            var esService = new Mock<IElasticService>();
            esService.Setup(e => e.EnsureIndex<Album>())
                .Returns(true);
            esService.Setup(e => e.Index(It.IsAny<Album>()))
                .Returns(true);

            var injector = new IInjectorServiceMock()
                .Register(_ => esService.Object)
                .Build();

            var subject = Sys.ActorOf(Props.Create<EsEntryPointActor>(injector));
            var testProbe = CreateTestProbe();

            // Set probe as child actor
            subject.Tell(testProbe.Ref, TestActor);

            var album = new Album { Id = 123 };

            // Message first album
            subject.Tell(album, TestActor);

            // While ElasticReady message is not received, no message should be processed
            testProbe.ExpectNoMsg(TimeSpan.FromSeconds(1));

            subject.Tell(new EsEntryPointActor.ElasticReady(), TestActor);

            // Expect Message to be processed
            testProbe.ExpectMsg<Album>(e => e.Id == album.Id, TimeSpan.FromSeconds(1));

            // Verify that ensure index was called
            esService.Verify(e => e.EnsureIndex<Album>());
        }
    }
}
