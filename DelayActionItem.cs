// <copyright file="DelayActionItem.cs" company="EnsageSharp">
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
    using System.Threading;

    /// <summary>
    ///     Class that contains all of the needed information for delaying an action.
    /// </summary>
    public class DelayActionItem
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DelayActionItem" /> class.
        /// </summary>
        /// <param name="time">The time(in milliseconds) to call the function..</param>
        /// <param name="func">The function to call once the <paramref name="time" /> has expired.</param>
        /// <param name="token">The cancelation token.</param>
        public DelayActionItem(int time, Action func, CancellationToken token)
        {
            this.Time = (int)(time + Utils.TickCount);
            this.Function = func;
            this.Token = token;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the function.
        /// </summary>
        /// <value>
        ///     The function.
        /// </value>
        public Action Function { get; set; }

        /// <summary>
        ///     Gets or sets the time the function will be executed at.
        /// </summary>
        /// <value>
        ///     The time the function will be executed at.
        /// </value>
        public int Time { get; set; }

        /// <summary>
        ///     Gets or sets the cancelation token.
        /// </summary>
        /// <value>
        ///     The cancelation token.
        /// </value>
        /// <example>
        ///     <see cref="CancellationTokenSource" />
        /// </example>
        public CancellationToken Token { get; set; }

        #endregion
    }
}