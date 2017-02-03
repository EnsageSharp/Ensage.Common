// <copyright file="Rectangle.cs" company="EnsageSharp">
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
    using SharpDX;
    using SharpDX.Direct3D9;

    /// <summary>The rectangle.</summary>
    public sealed class Rectangle : RenderObject
    {
        #region Fields

        /// <summary>The line.</summary>
        private readonly Line line;

        /// <summary>The size.</summary>
        private Vector2 size;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="Rectangle" /> class.</summary>
        /// <param name="size">The size.</param>
        /// <param name="color">The color.</param>
        public Rectangle(Vector2 size, ColorBGRA color)
        {
            this.line = new Line(Drawing.Direct3DDevice9);

            this.Size = size;
            this.Color = color;
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets the color.</summary>
        public ColorBGRA Color { get; set; }

        /// <summary>Gets or sets the size.</summary>
        public override Vector2 Size
        {
            get
            {
                return this.size;
            }

            set
            {
                this.size = value;
                this.line.Width = this.size.Y;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>The end scene.</summary>
        public override void EndScene()
        {
            if (this.line.IsDisposed)
            {
                return;
            }

            this.line.Begin();
            this.line.Draw(
                new[]
                    {
                        new Vector2(this.Position.X, this.Position.Y + this.Size.Y / 2),
                        new Vector2(this.Position.X + this.Size.X, this.Position.Y + this.Size.Y / 2)
                    },
                this.Color);
            this.line.End();
        }

        /// <summary>The post reset.</summary>
        public override void PostReset()
        {
            this.line.OnResetDevice();
        }

        /// <summary>The pre reset.</summary>
        public override void PreReset()
        {
            this.line.OnLostDevice();
        }

        #endregion
    }
}