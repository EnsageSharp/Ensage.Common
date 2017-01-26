// <copyright file="MenuUtils.cs" company="EnsageSharp">
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
namespace Ensage.Common.Menu
{
    using System;

    using Ensage.Common.Objects;

    using SharpDX;
    using SharpDX.Direct3D9;

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
                DrawBoxFilled(x, y + 1, 1, h - 2 * 1, color); // Left
                DrawBoxFilled(x + w - 1, y + 1, 1, h - 2 * 1, color); // Right
            }
            else
            {
                DrawBoxFilled(x, y, w, linewidth, color); // Top
                DrawBoxFilled(x, y + h - linewidth, w, linewidth, color); // Bottom
                DrawBoxFilled(x, y + linewidth, linewidth, h - 2 * linewidth, color); // Left
                DrawBoxFilled(x + w - linewidth, y + linewidth, linewidth, h - 2 * linewidth, color); // Right
            }
        }

        public static void DrawBoxBordered(
            float x,
            float y,
            float w,
            float h,
            float borderWidth,
            DotaTexture texture,
            Color colorBorder)
        {
            Drawing.DrawRect(new Vector2(x, y), new Vector2(w, h), texture);
            DrawBox(x - borderWidth, y - borderWidth, w + 2 * borderWidth, h + borderWidth, borderWidth, colorBorder);
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
            DrawBox(x - borderWidth, y - borderWidth, w + 2 * borderWidth, h + borderWidth, borderWidth, colorBorder);
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
            // var vLine = new Vector2[2];
            // Line.Width = w;
            // Line.Begin();

            // vLine[0][0] = x + (w / 2);
            // vLine[0][1] = y;
            // vLine[1][0] = x + (w / 2);
            // vLine[1][1] = y + h;

            // Line.Draw(new[] { vLine[0], vLine[1] }, color);
            // Line.End();
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
            // var vLine = new Vector2[2];
            // Line.Width = dwWidth;
            // Line.Begin();

            // vLine[0][0] = xa; // Set points into array
            // vLine[0][1] = ya;
            // vLine[1][0] = xb;
            // vLine[1][1] = yb;

            // Line.Draw(new[] { vLine[0], vLine[1] }, color); // Draw with Line, number of lines, and color
            // Line.End(); // finish
            Drawing.DrawLine(new Vector2(xa, ya), new Vector2(xb, yb), color);
        }

        /// <summary>
        ///     The rounded rectangle.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        /// <param name="size">
        ///     The size.
        /// </param>
        /// <param name="color">
        ///     The color.
        /// </param>
        public static void RoundedRectangle(Vector2 position, Vector2 size, Color color)
        {
            Drawing.DrawRect(position, size, color);
            var a = size.Y / 2;
            Drawing.DrawRect(
                position,
                new Vector2(a, size.Y),
                Textures.GetTexture("materials/ensage_ui/menu/roundleft.vmat_c"));
            Drawing.DrawRect(
                position + new Vector2(size.X - a, 0),
                new Vector2(a, size.Y),
                Textures.GetTexture("materials/ensage_ui/menu/roundright.vmat_c"));
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
        public static void RoundedRectangle(float x, int y, int w, int h, int iSmooth, Color color)
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
            DrawBoxFilled(x, y + iSmooth, w, h - iSmooth * 2, color);

            DrawBoxFilled(x + iSmooth, y, w - iSmooth * 2, h, color);

            float fDegree = 0;

            Drawing.Direct3DDevice9.SetRenderState(RenderState.MultisampleAntialias, true);
            Drawing.Direct3DDevice9.SetRenderState(RenderState.AntialiasedLineEnable, true);

            for (var i = 0; i < 4; i++)
            {
                for (var k = fDegree; k < fDegree + Math.PI * 2 / 4f; k += (float)(1 * (Math.PI / 180.0f)))
                {
                    // Draw quarter circles on every corner 
                    // DrawLine(
                    // pt[i].X,
                    // pt[i].Y,
                    // pt[i].X + (float)(Math.Cos(k) * iSmooth),
                    // pt[i].Y + (float)(Math.Sin(k) * iSmooth),
                    // 1,
                    // color); // 3 is with line width 
                    Drawing.DrawLine(
                        new Vector2(pt[i].X, pt[i].Y),
                        new Vector2((float)(pt[i].X + Math.Cos(k) * iSmooth), (float)(pt[i].Y + Math.Sin(k) * iSmooth)),
                        color);
                }

                fDegree += (float)(Math.PI * 2) / 4; // quarter circle offset 
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The main menu draw.
        /// </summary>
        /// <param name="menu">
        ///     The menu.
        /// </param>
        /// <param name="add">
        ///     The add.
        /// </param>
        internal static void MainMenuDraw(Menu menu, double add)
        {
            if (!menu.IsRootMenu)
            {
                var abg = Textures.GetTexture(Menu.Root.SelectedTheme.MenuBackground);
                Drawing.DrawRect(menu.Position, new Vector2(menu.Width, menu.Height), abg);
                Drawing.DrawRect(
                    menu.Position,
                    new Vector2(menu.Width, menu.Height),
                    Menu.Root.SelectedTheme.SubMenuOverlayColor);
                Drawing.DrawRect(
                    menu.Position,
                    new Vector2(menu.Height / 14, menu.Height),
                    menu.IsOpen
                        ? Menu.Root.SelectedTheme.SubMenuOpenSideLineColor
                        : Menu.Root.SelectedTheme.SubMenuSideLineColor);
            }
            else
            {
                var abg = Textures.GetTexture(Menu.Root.SelectedTheme.MenuBackground);
                Drawing.DrawRect(menu.Position, new Vector2(menu.Width, menu.Height), abg);
                Drawing.DrawRect(
                    menu.Position,
                    new Vector2(menu.Width, menu.Height),
                    Menu.Root.SelectedTheme.RootMenuOverlayColor);
                Drawing.DrawRect(
                    menu.Position - new Vector2(menu.Height / 7, 0),
                    new Vector2(menu.Height / 7, menu.Height),
                    menu.IsOpen
                        ? Menu.Root.SelectedTheme.RootMenuOpenSideLineColor
                        : Menu.Root.SelectedTheme.RootMenuSideLineColor);
            }

            var textSize = Drawing.MeasureText(
                MultiLanguage._(menu.DisplayName),
                "Arial",
                new Vector2((float)(menu.Height * 0.48), 100),
                FontFlags.AntiAlias);
            var textPos = menu.Position + new Vector2(5, (float)(menu.Height * 0.5 - textSize.Y * 0.5));
            var bonusWidth = 0;
            if (menu.TextureName != null)
            {
                var tName = menu.TextureName;
                if (tName.Contains("npc_dota_hero"))
                {
                    Drawing.DrawRect(
                        menu.Position + new Vector2(3, 3),
                        new Vector2((float)(menu.Height * 1.4), menu.Height - 6),
                        Textures.GetHeroTexture(tName));
                    Drawing.DrawRect(
                        menu.Position + new Vector2(2, 2),
                        new Vector2((float)(menu.Height * 1.4) + 2, menu.Height - 4),
                        Color.Black,
                        true);
                    bonusWidth = (int)(menu.Height * 1.44);
                }
                else if (tName.Contains("npc_dota_neutral"))
                {
                    Drawing.DrawRect(
                        menu.Position + new Vector2(3, 3),
                        new Vector2(menu.Height - 6, menu.Height - 6),
                        Textures.GetNeutralCreepTexture(tName));
                    Drawing.DrawRect(
                        menu.Position + new Vector2(2, 2),
                        new Vector2(menu.Height - 4, menu.Height - 4),
                        Color.Black,
                        true);
                    bonusWidth = (int)(menu.Height * 0.89);
                }
                else if (tName.Contains("item_"))
                {
                    Drawing.DrawRect(
                        menu.Position + new Vector2(3, 3),
                        new Vector2(menu.Height + (float)(menu.Height * 0.16), menu.Height - 6),
                        Textures.GetItemTexture(tName));
                    Drawing.DrawRect(
                        menu.Position + new Vector2(2, 2),
                        new Vector2(menu.Height - 4, menu.Height - 4),
                        Color.Black,
                        true);
                    bonusWidth = (int)(menu.Height * 0.8);
                }
                else
                {
                    Drawing.DrawRect(
                        menu.Position + new Vector2(3, 3),
                        new Vector2(menu.Height - 6, menu.Height - 6),
                        Textures.GetSpellTexture(tName));
                    Drawing.DrawRect(
                        menu.Position + new Vector2(2, 2),
                        new Vector2(menu.Height - 4, menu.Height - 4),
                        Color.Black,
                        true);
                    bonusWidth = (int)(menu.Height * 0.85);
                }
            }

            if ((menu.TextureName == null || menu.ShowTextWithTexture ? textSize.X : 0) + bonusWidth
                < (float)(menu.Width - menu.Height * 0.3))
            {
                var arrow =
                    Textures.GetTexture(
                        menu.IsOpen ? Menu.Root.SelectedTheme.MenuArrowOpen : Menu.Root.SelectedTheme.MenuArrow);
                var size = new Vector2((float)(menu.Height * 0.53), (float)(menu.Height * 0.53));
                var add1 = menu.IsOpen ? menu.Height * 0.1 : add;
                Drawing.DrawRect(
                    menu.Position
                    + new Vector2(
                        (float)(menu.Width - menu.Height * 0.5 + add1 - size.X * 0.6),
                        (float)(menu.Height * 0.5 - size.Y * 0.5)),
                    size,
                    arrow);
            }

            Drawing.DrawRect(
                new Vector2(menu.Position.X, menu.Position.Y),
                new Vector2(menu.Width, menu.Height),
                menu.IsOpen ? new Color(70, 70, 70, (int)(25 + add * 5)) : new Color(60, 60, 60, (int)(5 + add * 7)));
            if (menu.TextureName == null || menu.ShowTextWithTexture)
            {
                Drawing.DrawText(
                    MultiLanguage._(menu.DisplayName),
                    textPos + new Vector2(bonusWidth, 0),
                    new Vector2((float)(menu.Height * 0.48), 100),
                    menu.IsOpen ? menu.Color + new Color(50, 50, 50) : menu.Color,
                    FontFlags.AntiAlias);
            }

            // Draw the menu submenus
            foreach (var child in menu.Children)
            {
                if (!child.Visible)
                {
                    continue;
                }

                child.Drawing_OnDraw(null);
            }

            // Draw the items
            for (var i = menu.Items.Count - 1; i >= 0; i--)
            {
                var item = menu.Items[i];
                if (item.Visible)
                {
                    item.Drawing_OnDraw();
                }
            }
        }

        #endregion
    }
}