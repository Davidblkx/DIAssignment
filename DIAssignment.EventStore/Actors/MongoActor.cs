using Akka.Actor;
using DIAssignment.Core.Models.Messages;
using DIAssignment.Core.Services;
using DIAssignment.EventStore.Services;

namespace DIAssignment.EventStore.Actors
{
    /// <summary>
    /// Actor that saves an entity state into a mongo database
    /// </summary>
    public class MongoActor : ReceiveActor
    {
        private readonly IMongoService _mongo;

        public MongoActor(IInjector injector)
        {
            _mongo = injector.Get<IMongoService>();
            Receive<UpsertMessage>(SaveEntity);
        }

        private void SaveEntity(UpsertMessage message)
        {
            _mongo.Insert(message);
        }
    }
}
