// <copyright file="DrawObject.cs" company="EnsageSharp">
//    Copyright (c) 2016 EnsageSharp.
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

    /// <summary>The draw object.</summary>
    public abstract class DrawObject : IDrawObject
    {
        #region Public Properties

        /// <summary>Gets or sets the position.</summary>
        public Vector2 Position { get; set; }

        /// <summary>Gets or sets the size.</summary>
        public Vector2 Size { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>The center on rectangle.</summary>
        /// <param name="rectanglePosition">The rectangle position.</param>
        /// <param name="rectangleSize">The rectangle size.</param>
        public void CenterOnRectangle(Vector2 rectanglePosition, Vector2 rectangleSize)
        {
            this.Position = rectanglePosition
                            + new Vector2(rectangleSize.X / 2 - this.Size.X / 2, rectangleSize.Y / 2 - this.Size.Y / 2);
        }

        /// <summary>The center on rectangle.</summary>
        /// <param name="rectangle">The rectangle.</param>
        public void CenterOnRectangle(Rectangle rectangle)
        {
            this.Position = new Vector2(
                rectangle.X + rectangle.Width / 2 - this.Size.X / 2, 
                rectangle.Y + rectangle.Height / 2 - this.Size.Y / 2);
        }

        /// <summary>The center on rectangle.</summary>
        /// <param name="rectangle">The rectangle.</param>
        public void CenterOnRectangle(DrawRect rectangle)
        {
            this.Position = new Vector2(
                rectangle.Position.X + rectangle.Size.X / 2 - this.Size.X / 2, 
                rectangle.Position.Y + rectangle.Size.Y / 2 - this.Size.Y / 2);
        }

        /// <summary>The center on rectangle.</summary>
        /// <param name="rectangleX">The rectangle x.</param>
        /// <param name="rectangleY">The rectangle y.</param>
        /// <param name="rectangleWidth">The rectangle width.</param>
        /// <param name="rectangleHeight">The rectangle height.</param>
        public void CenterOnRectangle(float rectangleX, float rectangleY, float rectangleWidth, float rectangleHeight)
        {
            this.Position = new Vector2(
                rectangleX + rectangleWidth / 2 - this.Size.X / 2, 
                rectangleY + rectangleHeight / 2 - this.Size.Y / 2);
        }

        /// <summary>The center on rectangle horizontally.</summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="indent">The indent.</param>
        public void CenterOnRectangleHorizontally(DrawRect rectangle, float indent = 0)
        {
            this.Position = new Vector2(
                rectangle.Position.X + indent, 
                rectangle.Position.Y + rectangle.Size.Y / 2 - this.Size.Y / 2);
        }

        /// <summary>The center on rectangle vertically.</summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="indent">The indent.</param>
        public void CenterOnRectangleVertically(DrawRect rectangle, float indent = 0)
        {
            this.Position = new Vector2(
                rectangle.Position.X + rectangle.Size.X / 2 - this.Size.X / 2, 
                rectangle.Position.Y + indent);
        }

        /// <summary>The draw.</summary>
        public abstract void Draw();

        #endregion
    }
}