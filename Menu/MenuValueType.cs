// <copyright file="MenuValueType.cs" company="EnsageSharp">
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
    ///     The menu value type.
    /// </summary>
    internal enum MenuValueType
    {
        /// <summary>
        ///     The none.
        /// </summary>
        None,

        /// <summary>
        ///     The boolean.
        /// </summary>
        Boolean,

        /// <summary>
        ///     The slider.
        /// </summary>
        Slider,

        /// <summary>
        ///     The key bind.
        /// </summary>
        KeyBind,

        /// <summary>
        ///     The integer.
        /// </summary>
        Integer,

        /// <summary>
        ///     The color.
        /// </summary>
        Color,

        /// <summary>
        ///     The circle.
        /// </summary>
        Circle,

        /// <summary>
        ///     The string list.
        /// </summary>
        StringList,

        /// <summary>
        ///     The ability toggler.
        /// </summary>
        AbilityToggler,

        /// <summary>
        ///     The hero toggler.
        /// </summary>
        HeroToggler,

        /// <summary>
        ///     The priority changer.
        /// </summary>
        PriorityChanger
    }
}