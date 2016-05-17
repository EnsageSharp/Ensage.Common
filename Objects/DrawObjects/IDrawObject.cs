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