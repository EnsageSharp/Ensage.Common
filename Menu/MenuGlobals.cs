// <copyright file="MenuGlobals.cs" company="EnsageSharp">
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
    using System.Collections.Generic;

    /// <summary>
    ///     The menu globals.
    /// </summary>
    public static class MenuGlobals
    {
        #region Static Fields

        /// <summary>
        ///     The menu state.
        /// </summary>
        private static List<string> menuState = new List<string>();

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether draw menu.
        /// </summary>
        public static bool DrawMenu { get; set; }

        /// <summary>
        ///     Gets or sets the menu state.
        /// </summary>
        public static List<string> MenuState
        {
            get
            {
                return menuState;
            }

            set
            {
                menuState = value;
            }
        }

        #endregion
    }
}