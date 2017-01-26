// <copyright file="Sleeper.cs" company="EnsageSharp">
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
namespace Ensage.Common.Objects.UtilityObjects
{
    /// <summary>
    ///     The sleeper.
    /// </summary>
    public class Sleeper
    {
        #region Fields

        /// <summary>
        ///     The last sleep tick count.
        /// </summary>
        private float lastSleepTickCount;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Sleeper" /> class.
        /// </summary>
        public Sleeper()
        {
            this.lastSleepTickCount = 0;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets a value indicating whether sleeping.
        /// </summary>
        public bool Sleeping
        {
            get
            {
                return Utils.TickCount < this.lastSleepTickCount;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The sleep.
        /// </summary>
        /// <param name="duration">
        ///     The duration.
        /// </param>
        public void Sleep(float duration)
        {
            this.lastSleepTickCount = Utils.TickCount + duration;
        }

        #endregion
    }
}