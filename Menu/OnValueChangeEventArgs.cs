// <copyright file="OnValueChangeEventArgs.cs" company="EnsageSharp">
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
    /// <summary>
    ///     The on value change event args.
    /// </summary>
    public class OnValueChangeEventArgs
    {
        #region Fields

        /// <summary>
        ///     The _new value.
        /// </summary>
        private readonly object newValue;

        /// <summary>
        ///     The _old value.
        /// </summary>
        private readonly object oldValue;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="OnValueChangeEventArgs" /> class.
        /// </summary>
        /// <param name="oldValue">
        ///     The old value.
        /// </param>
        /// <param name="newValue">
        ///     The new value.
        /// </param>
        public OnValueChangeEventArgs(object oldValue, object newValue)
        {
            this.oldValue = oldValue;
            this.newValue = newValue;
            this.Process = true;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether process.
        /// </summary>
        public bool Process { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The get new value.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="T" />.
        /// </returns>
        public T GetNewValue<T>()
        {
            return (T)this.newValue;
        }

        /// <summary>
        ///     The get old value.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="T" />.
        /// </returns>
        public T GetOldValue<T>()
        {
            return (T)this.oldValue;
        }

        #endregion
    }
}