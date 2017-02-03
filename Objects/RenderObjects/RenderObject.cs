// <copyright file="RenderObject.cs" company="EnsageSharp">
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
namespace Ensage.Common.Objects.RenderObjects
{
    using System;

    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;

    /// <summary>The render object.</summary>
    public abstract class RenderObject : IRenderObject
    {
        #region Public Properties

        /// <summary>Gets a value indicating whether is initialized.</summary>
        public bool IsInitialized { get; private set; }

        /// <summary>Gets or sets the position.</summary>
        public virtual Vector2 Position { get; set; }

        /// <summary>Gets or sets the size.</summary>
        public virtual Vector2 Size { get; set; }

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
                rectangle.Position.X + rectangle.Size.X / 2 - this.Size.X / 2,
                rectangle.Position.Y + rectangle.Size.Y / 2 - this.Size.Y / 2);
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

        /// <summary>The dispose.</summary>
        public virtual void Dispose()
        {
            this.IsInitialized = false;
            Drawing.OnPostReset -= this.OnPostReset;
            Drawing.OnPreReset -= this.OnPreReset;
        }

        /// <summary>The end scene.</summary>
        public abstract void EndScene();

        /// <summary>Initializes render object, subscribes to reset events</summary>
        public virtual void Initialize()
        {
            this.IsInitialized = true;
            Drawing.OnPostReset += this.OnPostReset;
            Drawing.OnPreReset += this.OnPreReset;
        }

        /// <summary>The post reset.</summary>
        public abstract void PostReset();

        /// <summary>The pre reset.</summary>
        public abstract void PreReset();

        /// <summary>The render. Must be called in OnEndScene</summary>
        public void Render()
        {
            if (!this.IsInitialized)
            {
                return;
            }

            this.EndScene();
        }

        #endregion

        #region Methods

        /// <summary>The on post reset.</summary>
        /// <param name="args">The args.</param>
        private void OnPostReset(EventArgs args)
        {
            this.PostReset();
        }

        /// <summary>The on pre reset.</summary>
        /// <param name="args">The args.</param>
        private void OnPreReset(EventArgs args)
        {
            this.PreReset();
        }

        #endregion
    }
}