// <copyright file="StringList.cs" company="EnsageSharp">
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
    ///     The string list.
    /// </summary>
    [Serializable]
    public struct StringList
    {
        #region Fields

        /// <summary>
        ///     The selected index.
        /// </summary>
        public int SelectedIndex;

        /// <summary>
        ///     The s list.
        /// </summary>
        public string[] SList;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="StringList" /> struct.
        /// </summary>
        /// <param name="list">
        ///     The list.
        /// </param>
        /// <param name="defaultSelectedIndex">
        ///     The default selected index.
        /// </param>
        public StringList(string[] list, int defaultSelectedIndex = 0)
        {
            this.SList = list;
            this.SelectedIndex = defaultSelectedIndex;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the selected value.
        /// </summary>
        public string SelectedValue
        {
            get
            {
                return this.SList[this.SelectedIndex];
            }
        }

        #endregion
    }
}