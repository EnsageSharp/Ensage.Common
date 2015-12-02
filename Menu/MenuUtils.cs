// <copyright file="Utils.cs" company="LeagueSharp">
//    Copyright (c) 2015 LeagueSharp.
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

namespace Ensage.Common.Menu
{
    using System;

    using SharpDX;

    /// <summary>
    ///     The user interface utils class.
    /// </summary>
    internal class MenuUtils
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Draws a Box
        /// </summary>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <param name="linewidth">Line Width</param>
        /// <param name="color">Color</param>
        public static void DrawBox(float x, float y, float w, float h, float linewidth, Color color)
        {
            if (linewidth.Equals(0) || linewidth.Equals(1))
            {
                DrawBoxFilled(x, y, w, 1, color); // Top
                DrawBoxFilled(x, y + h - 1, w, 1, color); // Bottom
                DrawBoxFilled(x, y + 1, 1, h - (2 * 1), color); // Left
                DrawBoxFilled(x + w - 1, y + 1, 1, h - (2 * 1), color); // Right
            }
            else
            {
                DrawBoxFilled(x, y, w, linewidth, color); // Top
                DrawBoxFilled(x, y + h - linewidth, w, linewidth, color); // Bottom
                DrawBoxFilled(x, y + linewidth, linewidth, h - (2 * linewidth), color); // Left
                DrawBoxFilled(x + w - linewidth, y + linewidth, linewidth, h - (2 * linewidth), color); // Right
            }
        }

        /// <summary>
        ///     Draws a bordered Box
        /// </summary>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <param name="borderWidth">The border width</param>
        /// <param name="color">Color</param>
        /// <param name="colorBorder">Border Color</param>
        public static void DrawBoxBordered(
            float x,
            float y,
            float w,
            float h,
            float borderWidth,
            Color color,
            Color colorBorder)
        {
            DrawBoxFilled(x, y, w, h, color);
            DrawBox(x - borderWidth, y - borderWidth, w + (2 * borderWidth), h + borderWidth, borderWidth, colorBorder);
        }

        /// <summary>
        ///     Draws a filled Box
        /// </summary>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <param name="color">Color</param>
        public static void DrawBoxFilled(float x, float y, float w, float h, Color color)
        {
            //var vLine = new Vector2[2];
            //Line.Width = w;
            //Line.Begin();

            //vLine[0][0] = x + (w / 2);
            //vLine[0][1] = y;
            //vLine[1][0] = x + (w / 2);
            //vLine[1][1] = y + h;

            //Line.Draw(new[] { vLine[0], vLine[1] }, color);
            //Line.End();
            Drawing.DrawRect(new Vector2(x, y), new Vector2(w, h), color);
        }

        /// <summary>
        ///     Draws a line from X to Y with a width and a color
        /// </summary>
        /// <param name="xa">Position X1</param>
        /// <param name="ya">Position Y1</param>
        /// <param name="xb">Position X2</param>
        /// <param name="yb">Position Y2</param>
        /// <param name="dwWidth">Width</param>
        /// <param name="color">Color</param>
        public static void DrawLine(float xa, float ya, float xb, float yb, float dwWidth, Color color)
        {
            //var vLine = new Vector2[2];
            //Line.Width = dwWidth;
            //Line.Begin();

            //vLine[0][0] = xa; // Set points into array
            //vLine[0][1] = ya;
            //vLine[1][0] = xb;
            //vLine[1][1] = yb;

            //Line.Draw(new[] { vLine[0], vLine[1] }, color); // Draw with Line, number of lines, and color
            //Line.End(); // finish
            Drawing.DrawLine(new Vector2(xa, ya), new Vector2(xb, yb), color);
        }

        /// <summary>
        ///     Draws a rounded Rectangle
        /// </summary>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <param name="iSmooth">Smooth</param>
        /// <param name="color">Color</param>
        public static void RoundedRectangle(int x, int y, int w, int h, int iSmooth, Color color)
        {
            var pt = new Vector2[4];

            // Get all corners 
            pt[0].X = x + (w - iSmooth);
            pt[0].Y = y + (h - iSmooth);

            pt[1].X = x + iSmooth;
            pt[1].Y = y + (h - iSmooth);

            pt[2].X = x + iSmooth;
            pt[2].Y = y + iSmooth;

            pt[3].X = x + w - iSmooth;
            pt[3].Y = y + iSmooth;

            // Draw cross 
            DrawBoxFilled(x, y + iSmooth, w, h - (iSmooth * 2), color);

            DrawBoxFilled(x + iSmooth, y, w - (iSmooth * 2), h, color);

            float fDegree = 0;

            for (var i = 0; i < 4; i++)
            {
                for (var k = fDegree; k < fDegree + ((Math.PI * 2) / 4f); k += (float)(1 * (Math.PI / 180.0f)))
                {
                    // Draw quarter circles on every corner 
                    //DrawLine(
                    //    pt[i].X,
                    //    pt[i].Y,
                    //    pt[i].X + (float)(Math.Cos(k) * iSmooth),
                    //    pt[i].Y + (float)(Math.Sin(k) * iSmooth),
                    //    1,
                    //    color); // 3 is with line width 
                    Drawing.DrawLine(
                        new Vector2(pt[i].X, pt[i].Y),
                        new Vector2(pt[i].X + (float)(Math.Cos(k) * iSmooth), pt[i].Y + (float)(Math.Sin(k) * iSmooth)),
                        color);
                }

                fDegree += (float)(Math.PI * 2) / 4; // quarter circle offset 
            }
        }

        #endregion
    }
}