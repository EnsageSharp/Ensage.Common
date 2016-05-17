namespace Ensage.Common.Objects.DrawObjects
{
    using Ensage.Common.Objects.UtilityObjects;

    using SharpDX;

    /// <summary>
    ///     The draw text.
    /// </summary>
    public class DrawText : DrawObject
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
        ///     The draw.
        /// </summary>
        public override void Draw()
        {
            Drawing.DrawText(this.text, this.Position, this.textSize, this.Color, this.FontFlags);
        }

        #endregion
    }
}