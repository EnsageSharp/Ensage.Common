// <copyright file="DrawVerticalLine.cs" company="EnsageSharp">
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

    /// <summary>The draw line.</summary>
    public class DrawVerticalLine : DrawObject
    {
        #region Fields

        /// <summary>The position.</summary>
        private Vector2 position;

        /// <summary>The position 2.</summary>
        private Vector2 position2;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="DrawVerticalLine" /> class.</summary>
        /// <param name="length">The length.</param>
        public DrawVerticalLine(float length)
        {
            this.Length = length;
        }

        /// <summary>Initializes a new instance of the <see cref="DrawVerticalLine" /> class.</summary>
        /// <param name="length">The length.</param>
        /// <param name="color">The color.</param>
        public DrawVerticalLine(float length, Color color)
        {
            this.Length = length;
            this.Color = color;
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets the color.</summary>
        public Color Color { get; set; } = Color.Black;

        /// <summary>Gets or sets the length.</summary>
        public float Length { get; set; }

        /// <summary>Gets or sets the position.</summary>
        public override Vector2 Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
                this.position2 = this.position + new Vector2(0, this.Length);
            }
        }

        /// <summary>Gets or sets the size.</summary>
        public override Vector2 Size
        {
            get
            {
                return new Vector2(1, this.Length);
            }

            set
            {
                this.Length = value.Y;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>The draw.</summary>
        public override void Draw()
        {
            Drawing.DrawLine(this.position, this.position2, this.Color);
        }

        #endregion
    }
}