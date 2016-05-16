namespace Ensage.Common.Objects.DrawObjects
{
    using Ensage.Common.Objects.UtilityObjects;

    using SharpDX;

    /// <summary>
    ///     The draw text.
    /// </summary>
    public class DrawText : IDrawObject
    {
        #region Fields

        /// <summary>
        ///     The sleeper.
        /// </summary>
        private readonly Sleeper sleeper;

        /// <summary>
        ///     The text.
        /// </summary>
        private string text;

        /// <summary>
        ///     The text size.
        /// </summary>
        private Vector2 textSize;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DrawText" /> class.
        /// </summary>
        public DrawText()
        {
            this.sleeper = new Sleeper();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the color.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        ///     Gets or sets the font flags.
        /// </summary>
        public FontFlags FontFlags { get; set; }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        ///     Gets the size.
        /// </summary>
        public Vector2 Size { get; private set; }

        /// <summary>
        ///     Gets or sets the text.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (this.text == value && this.Size != Vector2.Zero)
                {
                    return;
                }

                this.text = value;
                if (this.sleeper.Sleeping && this.Size != Vector2.Zero)
                {
                    return;
                }

                this.Size = Drawing.MeasureText(this.text, "Arial", this.textSize, this.FontFlags);
            }
        }

        /// <summary>
        ///     Gets or sets the text size.
        /// </summary>
        public Vector2 TextSize
        {
            get
            {
                return this.textSize;
            }

            set
            {
                if (this.textSize == value && this.Size != Vector2.Zero)
                {
                    return;
                }

                this.textSize = value;
                this.Size = Drawing.MeasureText(this.text, "Arial", this.textSize, this.FontFlags);
                this.sleeper.Sleep(200);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Centers the DrawText on given rectangle
        /// </summary>
        /// <param name="rectanglePosition">
        ///     The rectangle position.
        /// </param>
        /// <param name="rectangleSize">
        ///     The rectangle size.
        /// </param>
        public void CenterOnRectangle(Vector2 rectanglePosition, Vector2 rectangleSize)
        {
            this.Position = rectanglePosition
                            + new Vector2(
                                  (rectangleSize.X / 2) - (this.Size.X / 2), 
                                  (rectangleSize.Y / 2) - (this.Size.Y / 2));
        }

        /// <summary>
        ///     Centers the DrawText on given rectangle
        /// </summary>
        /// <param name="rectangle">
        ///     The rectangle.
        /// </param>
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

        /// <summary>
        ///     Centers the DrawText on given rectangle
        /// </summary>
        /// <param name="rectangleX">
        ///     The rectangle x.
        /// </param>
        /// <param name="rectangleY">
        ///     The rectangle y.
        /// </param>
        /// <param name="rectangleWidth">
        ///     The rectangle width.
        /// </param>
        /// <param name="rectangleHeight">
        ///     The rectangle height.
        /// </param>
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

        /// <summary>
        ///     The draw.
        /// </summary>
        public void Draw()
        {
            Drawing.DrawText(this.text, this.Position, this.textSize, this.Color, this.FontFlags);
        }

        #endregion
    }
}