using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Town.Utility;

namespace Town.Services
{
    /// <summary>
    /// The GameLoopService manages processing the game loop and updating game entities
    /// </summary>
    public class GameLoopService : IHostedService
    {
        /// <summary>
        /// Defines how often the game loop updates
        /// </summary>
        private readonly float tickLengthMs = 50.0f;

        /// <summary>
        /// How many ticks have elapsed
        /// </summary>
        private uint elapsedTicks = 0;

        /// <summary>
        /// The high resolution timer that controls game loop execution
        /// </summary>
        private HighResolutionTimer gameTimer;

        /// <summary>
        /// A stopwatch to measure timer performance
        /// </summary>
        private Stopwatch stopwatch;

        public GameLoopService()
        {
            gameTimer = new HighResolutionTimer(tickLengthMs);
            gameTimer.Elapsed += GameLoop;
            stopwatch = new Stopwatch();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            stopwatch.Start();
            gameTimer.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            stopwatch.Stop();
            gameTimer.Stop(false);
            return Task.CompletedTask;
        }

        private void GameLoop(object sender, HighResolutionTimerElapsedEventArgs e)
        {
            Console.WriteLine($"Tick #{elapsedTicks}: {elapsedTicks * tickLengthMs}ms / {stopwatch.ElapsedMilliseconds}ms elapsed ({ Math.Abs((elapsedTicks * tickLengthMs) - stopwatch.ElapsedMilliseconds) }ms diff)");
            elapsedTicks++;
        }
    }
}