using Akka.Actor;
using DIAssignment.Core.Actors;
using DIAssignment.Core.Infra;
using DIAssignment.Core.Models.Messages;
using DIAssignment.FileHandler.Actors;
using DIAssignment.FileHandler.Services;
using System;
using System.Threading.Tasks;

namespace DIAssignment.Core
{
    internal sealed class FileHandlerService : BaseActorService
    {
        public static FileHandlerService Start() => new();

        protected override void OnStart()
        {
            var injector = new Injector();

            ActorRef.FileHandler = ActorSystem.ActorOf(
                Props.Create<FileHandlerActor>(injector), ActorNames.FileHandler);
            EmmitStartMessage();
        }

        // Send messages to start the import process
        private async void EmmitStartMessage()
        {
            await Task.Delay(TimeSpan.FromSeconds(15));

            ActorRef.FileHandler.Tell(new ImportFile
            {
                FileId = "10jbXvwXGBVjrod6b2MtSH_uwHypmiq_0",
                // FileId = "1tjcdJfyUXo1zRUpn5aD5hTzC9Eyf9y4N",
                Type = Models.ImportEntityType.Collection
            });

            ActorRef.FileHandler.Tell(new ImportFile
            {
                FileId = "1uHRFZZ2nicwyROD5iRBsqGuawjMuoQuQ",
                // FileId = "1W3b2d6k9be1uwM6j-HPXkasJG8L6nXMn",
                Type = Models.ImportEntityType.Artist
            });

            ActorRef.FileHandler.Tell(new ImportFile
            {
                FileId = "1_7DEkjboKGermJoHtsN-EQsjZa9jeOVz",
                // FileId = "1pEhFL2OzTAp06rm2wkhfcqX9LUPM7zh5",
                Type = Models.ImportEntityType.ArtistCollection
            });

            ActorRef.FileHandler.Tell(new ImportFile
            {
                FileId = "1jFXSElLNuYI8yEmj74xMzpXeV7wOYUBu",
                // FileId = "10te-xo8Lybdhf5yHcLGCu2A9VEocHkHJ",
                Type = Models.ImportEntityType.CollectionMatch
            });


        }
    }
}
