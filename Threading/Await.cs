namespace Ensage.Common.Threading
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     Await Helpers.
    /// </summary>
    public class Await
    {
        #region Public Methods and Operators

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