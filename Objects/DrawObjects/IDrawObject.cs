// <copyright file="IDrawObject.cs" company="EnsageSharp">
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
namespace Ensage.Common.Objects.DrawObjects
{
    using SharpDX;

    /// <summary>
    ///     The DrawObject interface.
    /// </summary>
    public interface IDrawObject
    {
        #region Public Properties

        /// <summary>Gets or sets the position.</summary>
        Vector2 Position { get; set; }

        /// <summary>Gets or sets the size.</summary>
        Vector2 Size { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The draw.
        /// </summary>
        void Draw();

        #endregion
    }
}