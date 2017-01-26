// <copyright file="DrawRect.cs" company="EnsageSharp">
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
    ///     The draw rect.
    /// </summary>
    public class DrawRect : DrawObject
    {
        #region Fields

        /// <summary>
        ///     The texture.
        /// </summary>
        private readonly bool texture;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DrawRect" /> class.
        /// </summary>
        /// <param name="color">
        ///     The color.
        /// </param>
        public DrawRect(Color color)
        {
            this.Color = color;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DrawRect" /> class.
        /// </summary>
        /// <param name="texture">
        ///     The texture.
        /// </param>
        public DrawRect(DotaTexture texture)
        {
            this.Texture = texture;
            this.texture = true;
        }

        #endregion

        #region Public Properties

        public bool Border { get; set; }

        public Color BorderColor { get; set; } = Color.Black;

        /// <summary>
        ///     Gets or sets the color.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        ///     Gets or sets the texture.
        /// </summary>
        public DotaTexture Texture { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The draw.
        /// </summary>
        public override void Draw()
        {
            if (!this.texture)
            {
                Drawing.DrawRect(this.Position, this.Size, this.Color);
            }
            else
            {
                Drawing.DrawRect(this.Position, this.Size, this.Texture);
            }

            if (this.Border)
            {
                Drawing.DrawRect(this.Position, this.Size, this.BorderColor, true);
            }
        }

        #endregion
    }
}