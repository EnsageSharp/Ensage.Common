namespace Ensage.Common.Objects.DrawObjects
{
    using SharpDX;

    /// <summary>
    ///     The draw rect.
    /// </summary>
    public class DrawRect : IDrawObject
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

        /// <summary>
        ///     Gets or sets the color.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        public Vector2 Size { get; set; }

        /// <summary>
        ///     Gets or sets the texture.
        /// </summary>
        public DotaTexture Texture { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The draw.
        /// </summary>
        public void Draw()
        {
            if (!this.texture)
            {
                Drawing.DrawRect(this.Position, this.Size, this.Color);
            }
            else
            {
                Drawing.DrawRect(this.Position, this.Size, this.Texture);
            }
        }

        #endregion
    }
}