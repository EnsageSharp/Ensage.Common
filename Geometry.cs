// <copyright file="Geometry.cs" company="EnsageSharp">
//    Copyright (c) 2015 EnsageSharp.
// 
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
// 
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
// 
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>

namespace Ensage.Common
{
    using System;

    using SharpDX;
    using SharpDX.Direct3D9;

    /// <summary>
    ///     Constant values of EnsageSharp.SDK
    /// </summary>
    public static class Constants
    {
        #region Static Fields

        /// <summary>
        ///     The league sharp font.
        /// </summary>
        private static Font ensageSharpFont;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the league sharp font.
        /// </summary>
        public static Font EnsageSharpFont
        {
            get
            {
                if (ensageSharpFont != null && !ensageSharpFont.IsDisposed)
                {
                    return ensageSharpFont;
                }

                return
                    ensageSharpFont =
                    new Font(
                        Drawing.Direct3DDevice9,
                        14,
                        0,
                        FontWeight.DoNotCare,
                        0,
                        false,
                        FontCharacterSet.Default,
                        FontPrecision.Default,
                        FontQuality.Antialiased,
                        FontPitchAndFamily.DontCare | FontPitchAndFamily.Decorative,
                        "Tahoma");
            }
        }

        #endregion
    }

    /// <summary>
    ///     Geometry math class, contains geometry calculations.
    /// </summary>
    public static class Geometry
    {
        #region Enums

        /// <summary>
        ///     CenteredText Drawing Flags
        /// </summary>
        [Flags]
        public enum CenteredFlags
        {
            /// <summary>
            ///     None Flag
            /// </summary>
            None = 0,

            /// <summary>
            ///     Center Horizontally Left.
            /// </summary>
            HorizontalLeft = 1 << 0,

            /// <summary>
            ///     Center Horizontally.
            /// </summary>
            HorizontalCenter = 1 << 1,

            /// <summary>
            ///     Center Horizontally Right.
            /// </summary>
            HorizontalRight = 1 << 2,

            /// <summary>
            ///     Center Vertically Up.
            /// </summary>
            VerticalUp = 1 << 3,

            /// <summary>
            ///     Center Vertically.
            /// </summary>
            VerticalCenter = 1 << 4,

            /// <summary>
            ///     Center Vertically Down.
            /// </summary>
            VerticalDown = 1 << 5
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Returns the center position of the rendering object on the rectangle.
        /// </summary>
        /// <param name="rectangle">Rectangle boundaries</param>
        /// <param name="sprite">Sprite which is being drawn on</param>
        /// <param name="dimensions">Object Dimensions</param>
        /// <param name="flags">Centered Flags</param>
        /// <returns>Vector2 center position of the rendering object on the rectangle.</returns>
        public static Vector2 GetCenter(
            this Rectangle rectangle,
            Sprite sprite,
            Rectangle dimensions,
            CenteredFlags flags)
        {
            var x = 0;
            var y = 0;

            if (flags.HasFlag(CenteredFlags.HorizontalLeft))
            {
                x = rectangle.TopLeft.X;
            }
            else if (flags.HasFlag(CenteredFlags.HorizontalCenter))
            {
                x = rectangle.TopLeft.X + ((rectangle.Width - dimensions.Width) / 2);
            }
            else if (flags.HasFlag(CenteredFlags.HorizontalRight))
            {
                x = rectangle.TopRight.X - dimensions.Width;
            }

            if (flags.HasFlag(CenteredFlags.VerticalUp))
            {
                y = rectangle.TopLeft.Y;
            }
            else if (flags.HasFlag(CenteredFlags.VerticalCenter))
            {
                y = rectangle.TopLeft.Y + ((rectangle.Height - dimensions.Height) / 2);
            }
            else if (flags.HasFlag(CenteredFlags.VerticalDown))
            {
                y = rectangle.BottomLeft.Y - dimensions.Height;
            }

            return new Vector2(x, y);
        }

        /// <summary>
        ///     Calculates the center position for the given text on within a rectangle boundaries.
        /// </summary>
        /// <param name="rectangle">Rectangle boundaries</param>
        /// <param name="sprite">Sprite which is being drawn on</param>
        /// <param name="text">The Text</param>
        /// <param name="flags">Centered Flags</param>
        /// <returns>Returns the center position of the text on the rectangle.</returns>
        public static Vector2 GetCenteredText(this Rectangle rectangle, Sprite sprite, string text, CenteredFlags flags)
        {
            return rectangle.GetCenter(sprite, Constants.EnsageSharpFont.MeasureText(sprite, text, 0), flags);
        }

        /// <summary>
        ///     Calculates the center position for the given text on within a rectangle boundaries.
        /// </summary>
        /// <param name="rectangle">Rectangle boundaries</param>
        /// <param name="sprite">Sprite which is being drawn on</param>
        /// <param name="font">Text Font</param>
        /// <param name="text">The Text</param>
        /// <param name="flags">Centered Flags</param>
        /// <returns>Returns the center position of the text on the rectangle.</returns>
        public static Vector2 GetCenteredText(
            this Rectangle rectangle,
            Sprite sprite,
            Font font,
            string text,
            CenteredFlags flags)
        {
            return font == null
                       ? rectangle.GetCenteredText(sprite, text, flags)
                       : rectangle.GetCenter(sprite, font.MeasureText(sprite, text, 0), flags);
        }

        #endregion
    }
}