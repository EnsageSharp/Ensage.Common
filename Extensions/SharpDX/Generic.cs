namespace Ensage.Common.Extensions.SharpDX
{
    using global::SharpDX;

    /// <summary>
    ///     The generic SharpDX extensions.
    /// </summary>
    public static class Generic
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Converts a SharpDX Color to <c>Argb</c> format.
        /// </summary>
        /// <param name="color">
        ///     The color
        /// </param>
        /// <returns>
        ///     The <see cref="int" /> in 0xAARRGGBB format.
        /// </returns>
        public static int ToArgb(this Color color)
        {
            var x = color.ToRgba();
            return (int)((x & 0xFF000000) >> 0x8) | ((x & 0x00FF0000) >> 0x8) | ((x & 0x0000FF00) >> 0x8)
                   | ((x & 0x000000FF) << 0x18);
        }

        /// <summary>
        ///     Converts a System Color to <c>Rgba</c> format.
        /// </summary>
        /// <param name="color">
        ///     The color
        /// </param>
        /// <returns>
        ///     The <see cref="int" /> in 0xRRGGBBAA format.
        /// </returns>
        public static int ToRgba(this System.Drawing.Color color)
        {
            var x = color.ToArgb();
            return (int)((x & 0xFF000000) >> 0x18) | ((x & 0x00FF0000) << 0x8) | ((x & 0x0000FF00) << 0x8)
                   | ((x & 0x000000FF) << 0x8);
        }

        /// <summary>
        ///     Converts a System Color to a SharpDX Color.
        /// </summary>
        /// <param name="color">
        ///     The color.
        /// </param>
        /// <returns>
        ///     The SharpDX Color instance.
        /// </returns>
        public static Color ToSharpDxColor(this System.Drawing.Color color)
        {
            return new Color(color.R, color.G, color.B, color.A);
        }

        /// <summary>
        ///     Converts a SharpDX Color to a System Color.
        /// </summary>
        /// <param name="color">
        ///     The color.
        /// </param>
        /// <returns>
        ///     The System Color instance.
        /// </returns>
        public static System.Drawing.Color ToSystemColor(this Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        /// <summary>
        ///     Converts a SharpDX Color to a System Color.
        /// </summary>
        /// <param name="color">
        ///     The color.
        /// </param>
        /// <returns>
        ///     The System Color instance.
        /// </returns>
        public static System.Drawing.Color ToSystemColor(this ColorBGRA color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        #endregion
    }
}