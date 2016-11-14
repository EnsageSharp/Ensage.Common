namespace Ensage.Common.Threading
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     Await Helpers.
    /// </summary>
    public class Await
    {
        #region Static Fields

        private static readonly SynchronizedCollection<string> Running = new SynchronizedCollection<string>();

        #endregion

        #region Public Methods and Operators

        public static async void Block(string key, Func<Task> taskFactory)
        {
            if (Running.Contains(key))
            {
                // block if running
                return;
            }

            Running.Add(key);

            try
            {
                await taskFactory();
            }
            finally
            {
                Running.Remove(key);
            }
        }

        /// <summary>
        ///     Awaits the larger of two <see ref="time" /> or <see cref="Game.Ping" />
        /// </summary>
        /// <param name="time"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<int> Delay(int time, CancellationToken token = default(CancellationToken))
        {
            var waitTime = Math.Max((int)Game.Ping, time);
            await Task.Delay(waitTime, token);
            return waitTime;
        }

        #endregion
    }
}