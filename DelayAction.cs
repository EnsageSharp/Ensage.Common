// <copyright file="DelayAction.cs" company="EnsageSharp">
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
namespace Ensage.Common
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using Ensage.Common.Signals;

    /// <summary>
    ///     Delays actions by a set time.
    /// </summary>
    public class DelayAction
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Adds a new delayed action.
        /// </summary>
        /// <param name="time">Delayed Time</param>
        /// <param name="func">Callback Function</param>
        public static void Add(int time, Action func)
        {
            Add(new DelayActionItem(time, func, new CancellationToken(false)));
        }

        /// <summary>
        ///     Adds a new delayed action, casting the time to an integer.
        /// </summary>
        /// <param name="time">The time(in milliseconds) to call the function.</param>
        /// <param name="func">The function to call once the <paramref name="time" /> has expired.</param>
        public static void Add(float time, Action func)
        {
            Add(new DelayActionItem((int)time, func, new CancellationToken(false)));
        }

        /// <summary>
        ///     Adds a new delayed action with a cancelation token. Use the <see cref="CancellationTokenSource" /> class for
        ///     tokens.
        /// </summary>
        /// <param name="time">The time(in milliseconds) to call the function.</param>
        /// <param name="func">The function to call once the <paramref name="time" /> has expired.</param>
        /// <param name="token">The cancelation token.</param>
        public static void Add(int time, Action func, CancellationToken token)
        {
            Add(new DelayActionItem(time, func, token));
        }

        /// <summary>
        ///     Adds a new delayed action with a cancelation token. Use the <see cref="CancellationTokenSource" /> class for
        ///     tokens.
        /// </summary>
        /// <param name="time">The time(in milliseconds) to call the function. (Gets casted into an integer)</param>
        /// <param name="func">The function to call once the <paramref name="time" /> has expired.</param>
        /// <param name="token">The cancelation token.</param>
        public static void Add(float time, Action func, CancellationToken token)
        {
            Add(new DelayActionItem((int)time, func, token));
        }

        /// <summary>
        ///     Adds a new delayed action.
        /// </summary>
        /// <param name="item">The <see cref="DelayActionItem" /> to add.</param>
        public static void Add(DelayActionItem item)
        {
            Signal.Create(
                (sender, args) =>
                    {
                        var delayActionItem = (DelayActionItem)args.Signal.Properties["DelayActionItem"];

                        if (delayActionItem.Token.IsCancellationRequested)
                        {
                            return;
                        }

                        delayActionItem.Function();
                    },
                signal =>
                    {
                        var delayActionItem = (DelayActionItem)signal.Properties["DelayActionItem"];
                        return Utils.TickCount >= delayActionItem.Time;
                    },
                default(DateTimeOffset),
                new Dictionary<string, object> { { "DelayActionItem", item } });
        }

        #endregion
    }
}