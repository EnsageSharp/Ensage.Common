// <copyright file="Await.cs" company="EnsageSharp">
//    Copyright (c) 2017 EnsageSharp.
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>
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