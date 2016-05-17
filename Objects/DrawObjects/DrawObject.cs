using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensage.Common.Objects.DrawObjects
{
    using SharpDX;

    /// <summary>The draw object.</summary>
    public abstract class DrawObject : IDrawObject
    {
        /// <summary>Gets or sets the position.</summary>
        public Vector2 Position { get; set; }

        /// <summary>Gets or sets the size.</summary>
        public Vector2 Size { get; set; }

        /// <summary>The draw.</summary>
        public abstract void Draw();

        /// <summary>The center on rectangle.</summary>
        /// <param name="rectanglePosition">The rectangle position.</param>
        /// <param name="rectangleSize">The rectangle size.</param>
        public void CenterOnRectangle(Vector2 rectanglePosition, Vector2 rectangleSize)
        {
            this.Position = rectanglePosition
                            + new Vector2(
                                  (rectangleSize.X / 2) - (this.Size.X / 2),
                                  (rectangleSize.Y / 2) - (this.Size.Y / 2));
        }

        /// <summary>The center on rectangle.</summary>
        /// <param name="rectangle">The rectangle.</param>
        public void CenterOnRectangle(Rectangle rectangle)
        {
            this.Position = new Vector2(
                rectangle.X + (rectangle.Width / 2) - (this.Size.X / 2),
                rectangle.Y + (rectangle.Height / 2) - (this.Size.Y / 2));
        }

        /// <summary>The center on rectangle.</summary>
        /// <param name="rectangle">The rectangle.</param>
        public void CenterOnRectangle(DrawRect rectangle)
        {
            this.Position = new Vector2(
                rectangle.Position.X + (rectangle.Size.X / 2) - (this.Size.X / 2),
                rectangle.Position.Y + (rectangle.Size.Y / 2) - (this.Size.Y / 2));
        }

        /// <summary>The center on rectangle.</summary>
        /// <param name="rectangleX">The rectangle x.</param>
        /// <param name="rectangleY">The rectangle y.</param>
        /// <param name="rectangleWidth">The rectangle width.</param>
        /// <param name="rectangleHeight">The rectangle height.</param>
        public void CenterOnRectangle(float rectangleX, float rectangleY, float rectangleWidth, float rectangleHeight)
        {
            this.Position = new Vector2(
                rectangleX + (rectangleWidth / 2) - (this.Size.X / 2),
                rectangleY + (rectangleHeight / 2) - (this.Size.Y / 2));
        }

        /// <summary>The center on rectangle horizontally.</summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="indent">The indent.</param>
        public void CenterOnRectangleHorizontally(DrawRect rectangle, float indent = 0)
        {
            this.Position = new Vector2(
                rectangle.Position.X + indent,
                rectangle.Position.Y + (rectangle.Size.Y / 2) - (this.Size.Y / 2));
        }

        /// <summary>The center on rectangle vertically.</summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="indent">The indent.</param>
        public void CenterOnRectangleVertically(DrawRect rectangle, float indent = 0)
        {
            this.Position = new Vector2(
                rectangle.Position.X + (rectangle.Size.X / 2) - (this.Size.X / 2),
                rectangle.Position.Y + indent);
        }
    }
}
