using Akka.Actor;
using Akka.Bootstrap.Docker;
using Akka.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DIAssignment.Core.Actors
{
    /// <summary>
    /// Service to start a actor system
    /// </summary>
    public abstract class BaseActorService
    {
        public const string ACTOR_SYSTEM_NAME = "dia";
        public const string CONFIG_NAME = "config.hocon";

        protected readonly ActorSystem ActorSystem;

        public Task WhenTerminated => ActorSystem.WhenTerminated;

        public BaseActorService(string configPath = CONFIG_NAME)
        {
            var configContent = File.ReadAllText(configPath);

            // Load config file
            var config = ConfigurationFactory
                .ParseString(configContent);
            // Start actor system
            ActorSystem = ActorSystem.Create(
                ACTOR_SYSTEM_NAME, config.BootstrapFromDocker());

            OnStart();
        }

        /// <summary>
        /// Invoked after actor system is init
        /// </summary>
        protected virtual void OnStart() { }

        /// <summary>
        /// Safely shutdown the actor system
        /// </summary>
        /// <returns></returns>
        public Task Stop()
            => CoordinatedShutdown.Get(ActorSystem)
                .Run(CoordinatedShutdown.ClrExitReason.Instance);

        /// <summary>
        /// Wait for cancel key press or shutdown request
        /// </summary>
        public void WaitForExit()
        {
            Console.CancelKeyPress += (_, EventArgs) =>
            {
                Stop();
                EventArgs.Cancel = true;
            };

            WhenTerminated.Wait();
        }
    }
}
