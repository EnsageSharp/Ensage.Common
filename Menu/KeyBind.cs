// <copyright file="KeyBind.cs" company="EnsageSharp">
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
    ///     The key bind.
    /// </summary>
    [Serializable]
    public struct KeyBind
    {
        #region Fields

        /// <summary>
        ///     The active.
        /// </summary>
        public bool Active;

        /// <summary>
        ///     The key.
        /// </summary>
        public uint Key;

        /// <summary>
        ///     The type.
        /// </summary>
        public KeyBindType Type;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="KeyBind" /> struct.
        /// </summary>
        /// <param name="key">
        ///     The key.
        /// </param>
        /// <param name="type">
        ///     The type.
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        public KeyBind(uint key, KeyBindType type, bool defaultValue = false)
        {
            this.Key = key;
            this.Active = defaultValue;
            this.Type = type;
        }

        #endregion
    }
}