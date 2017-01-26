// <copyright file="Slider.cs" company="EnsageSharp">
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
namespace Ensage.Common.Menu
{
    using System;

    /// <summary>
    ///     The slider.
    /// </summary>
    [Serializable]
    public struct Slider
    {
        #region Fields

        /// <summary>
        ///     The max value.
        /// </summary>
        public int MaxValue;

        /// <summary>
        ///     The min value.
        /// </summary>
        public int MinValue;

        /// <summary>
        ///     The value.
        /// </summary>
        private int value;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Slider" /> struct.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <param name="minValue">
        ///     The min value.
        /// </param>
        /// <param name="maxValue">
        ///     The max value.
        /// </param>
        public Slider(int value = 0, int minValue = 0, int maxValue = 100)
        {
            this.MaxValue = Math.Max(maxValue, minValue);
            this.MinValue = Math.Min(maxValue, minValue);
            this.value = value;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        public int Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = Math.Min(Math.Max(value, this.MinValue), this.MaxValue);
            }
        }

        #endregion
    }
}