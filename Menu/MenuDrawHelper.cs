// <copyright file="MenuDrawHelper.cs" company="EnsageSharp">
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
    using Ensage.Common.Extensions.SharpDX;
    using Ensage.Common.Objects;

    using SharpDX;

    using Color = System.Drawing.Color;

    /// <summary>
    ///     The menu draw helper.
    /// </summary>
    internal static class MenuDrawHelper
    {
        #region Methods

        /// <summary>
        ///     The draw arrow.
        /// </summary>
        /// <param name="left">
        ///     The left.
        /// </param>
        /// <param name="position">
        ///     The position.
        /// </param>
        /// <param name="item">
        ///     The item.
        /// </param>
        /// <param name="color">
        ///     The color.
        /// </param>
        internal static void DrawArrow(bool left, Vector2 position, MenuItem item, Color color)
        {
            Drawing.DrawRect(
                position + new Vector2(0, item.Height / 6),
                new Vector2(item.Height - item.Height / 12 * 2, item.Height - item.Height / 6 * 2),
                Textures.GetTexture(Menu.Root.SelectedTheme.MenuBackground));
            Drawing.DrawRect(
                position + new Vector2(0, item.Height / 6),
                new Vector2(item.Height - item.Height / 12 * 2, item.Height - item.Height / 6 * 2),
                new SharpDX.Color(20, 20, 20, 190));
            Drawing.DrawRect(
                position + new Vector2(0, item.Height / 6),
                new Vector2(item.Height - item.Height / 12 * 2, item.Height - item.Height / 6 * 2),
                Utils.IsUnderRectangle(Game.MouseScreenPosition, position.X, position.Y, item.Height, item.Height)
                    ? Menu.Root.SelectedTheme.StringListArrowHoveredOverlayColor
                    : Menu.Root.SelectedTheme.StringListArrowOverlayColor);

            var s = left ? "<" : ">";
            var textSize = Drawing.MeasureText(
                s,
                "Arial",
                new Vector2((float)(item.Height * 0.67), item.Height / 2),
                FontFlags.AntiAlias);
            var a = left ? item.Height / 10 : item.Height / 14;
            var textPos = position
                          + new Vector2(
                              (float)(item.Height * 0.5 - textSize.X * 0.5 - a),
                              (float)(item.Height * 0.5 - textSize.Y * 0.5) + 1);

            Drawing.DrawText(
                s,
                textPos,
                new Vector2((float)(item.Height * 0.67), item.Height / 2),
                Utils.IsUnderRectangle(Game.MouseScreenPosition, position.X, position.Y, item.Height, item.Height)
                    ? Menu.Root.SelectedTheme.StringListArrowHoveredColor
                    : Menu.Root.SelectedTheme.StringListArrowColor,
                FontFlags.AntiAlias);
        }

        /// <summary>
        ///     The draw on off.
        /// </summary>
        /// <param name="on">
        ///     The on.
        /// </param>
        /// <param name="position">
        ///     The position.
        /// </param>
        /// <param name="item">
        ///     The item.
        /// </param>
        internal static void DrawOnOff(bool on, Vector2 position, MenuItem item)
        {
            var alpha = Utils.IsUnderRectangle(
                            Game.MouseScreenPosition,
                            position.X + item.Height - item.Width,
                            position.Y,
                            item.Width,
                            item.Height)
                            ? 30
                            : 0;
            var alpha2 = Utils.IsUnderRectangle(
                             Game.MouseScreenPosition,
                             position.X,
                             position.Y,
                             item.Height,
                             item.Height)
                             ? 25
                             : 0;
            var noUnicode = MenuConfig.SelectedLanguage == "Chinese" || MenuConfig.SelectedLanguage == "Russian";
            var s = on ? "✔" : string.Empty;
            var pos = position + new Vector2(item.Height / 6, item.Height / 6);
            var height = item.Height - item.Height / 6 * 2;

            MenuUtils.DrawBoxBordered(
                pos.X,
                pos.Y,
                height,
                height,
                1f,
                Color.FromArgb(140 + alpha, 90 + alpha, 1 + alpha).ToSharpDxColor(),
                new SharpDX.Color(0, 0, 0));

            Drawing.DrawRect(
                pos + new Vector2(height / 10, height / 10),
                new Vector2((float)(height - height / 10 * 2), (float)(height - height / 10 * 2) - 1),
                new SharpDX.Color(5 + alpha2, 5 + alpha2, 5 + alpha2));
            if (noUnicode)
            {
                if (!on)
                {
                    return;
                }

                Drawing.DrawRect(
                    pos + new Vector2(height / 4, height / 4),
                    new Vector2((float)(height - height / 4 * 2), (float)(height - height / 4 * 2) - 1),
                    new SharpDX.Color(230, 148, 2));
            }
            else
            {
                var tsize = new Vector2((float)(height / 1.1), item.Width);
                var textSize = Drawing.MeasureText(s, "Arial", tsize, FontFlags.AntiAlias);
                var textPos = item.Position
                              + new Vector2(
                                  (float)(item.Width - item.Height / 2 - textSize.X / 2.9),
                                  (float)(+(item.Height * 0.5) - textSize.Y / 1.9));

                Drawing.DrawText(s, textPos, tsize, Color.NavajoWhite.ToSharpDxColor(), FontFlags.Italic);
            }
        }

        /// <summary>
        ///     The draw slider.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        /// <param name="item">
        ///     The item.
        /// </param>
        /// <param name="width">
        ///     The width.
        /// </param>
        /// <param name="drawText">
        ///     The draw text.
        /// </param>
        internal static void DrawSlider(Vector2 position, MenuItem item, int width = -1, bool drawText = true)
        {
            var val = item.GetValue<Slider>();
            DrawSlider(position, item, val.MinValue, val.MaxValue, val.Value, width, drawText);
        }

        /// <summary>
        ///     The draw slider.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        /// <param name="item">
        ///     The item.
        /// </param>
        /// <param name="min">
        ///     The min.
        /// </param>
        /// <param name="max">
        ///     The max.
        /// </param>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <param name="width">
        ///     The width.
        /// </param>
        /// <param name="drawText">
        ///     The draw text.
        /// </param>
        internal static void DrawSlider(
            Vector2 position,
            MenuItem item,
            int min,
            int max,
            int value,
            int width,
            bool drawText)
        {
            width = width > 0 ? width : item.Width;
            var percentage = 100 * (value - min) / (max - min);
            var x = position.X + 3 + percentage * (width - 3) / 100;
            var x2D = 3 + percentage * (width - 3) / 100;

            MenuUtils.DrawLine(x, position.Y, x, position.Y + item.Height - 2, 2, Menu.Root.SelectedTheme.SliderColor);
            MenuUtils.DrawBoxFilled(
                position.X,
                position.Y - 1,
                x2D - 1f,
                item.Height,
                Menu.Root.SelectedTheme.SliderFillColor);

            if (!drawText)
            {
                return;
            }

            var textSize = Drawing.MeasureText(
                value.ToString(),
                "Arial",
                new Vector2((float)(item.Height * 0.45), (float)item.Width / 2),
                FontFlags.AntiAlias);
            var textPos = position
                          + new Vector2(
                              (float)(item.Width - item.Height * 0.5 - 2 - textSize.X * 0.5),
                              (float)(+(item.Height * 0.5) - textSize.Y * 0.5));
            Drawing.DrawText(
                value.ToString(),
                textPos,
                new Vector2((float)(item.Height * 0.48), (float)item.Width / 2),
                Menu.Root.SelectedTheme.SliderTextColor,
                FontFlags.AntiAlias);
        }

        /// <summary>
        ///     The draw tool tip_ button.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        /// <param name="item">
        ///     The item.
        /// </param>
        internal static void DrawToolTipButton(Vector2 position, MenuItem item)
        {
            if (item.ValueType == MenuValueType.StringList || item.ValueType == MenuValueType.AbilityToggler
                || item.ValueType == MenuValueType.HeroToggler)
            {
                return;
            }

            var texture = Textures.GetTexture("materials/ensage_ui/other/statpop_question.vmat_c");

            var textPos = item.Position + new Vector2(item.Width - item.Height * 2, -(float)(item.Height * 0.05));
            Drawing.DrawRect(textPos, new Vector2((float)(item.Height / 1.1), (float)(item.Height * 1.1)), texture);
        }

        /// <summary>
        ///     The draw tool tip_ text.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        /// <param name="item">
        ///     The item.
        /// </param>
        /// <param name="add">
        ///     The add.
        /// </param>
        /// <param name="textColor">
        ///     The text color.
        /// </param>
        internal static void DrawToolTipText(
            Vector2 position,
            MenuItem item,
            double add,
            SharpDX.Color? textColor = null)
        {
            if (item.ValueType == MenuValueType.StringList || item.ValueType == MenuValueType.AbilityToggler
                || item.ValueType == MenuValueType.HeroToggler)
            {
                return;
            }

            var s = item.Tooltip;
            var textSize = Drawing.MeasureText(
                s,
                "Arial",
                new Vector2((float)(item.Height * 0.42), 14),
                FontFlags.AntiAlias);

            // MenuUtils.DrawBoxBordered(
            // position.X, 
            // position.Y + 1, 
            // textSize.X + 8, 
            // item.Height, 
            // 1,
            // new SharpDX.Color(37, 37, 37, (int)(add * 58)), 
            // new SharpDX.Color(20, 20, 20, (int)(add * 58)));
            Drawing.DrawRect(
                new Vector2(position.X, position.Y),
                new Vector2(textSize.X + 8, item.Height),
                Menu.Root.SelectedTheme.ToolTipBackgroundColor - new SharpDX.Color(0, 0, 0, 255)
                + new SharpDX.Color(0, 0, 0, (int)(add / 2 * (add / 2) * (add / 2) * (add / 2) * 40)));
            Drawing.DrawRect(
                new Vector2(position.X - 1, position.Y),
                new Vector2(textSize.X + 10, item.Height),
                new SharpDX.Color(0, 0, 0, (int)(add / 2 * (add / 2) * (add / 2) * (add / 2) * 40)),
                true);

            var textPos = position + new Vector2(4, (float)(item.Height * 0.5 - textSize.Y * 0.5));
            Drawing.DrawText(
                s,
                textPos,
                new Vector2((float)(item.Height * 0.42), 14),
                Menu.Root.SelectedTheme.ToolTipTextColor - new SharpDX.Color(0, 0, 0, 255)
                + new SharpDX.Color(0, 0, 0, (int)(add * 68)),
                FontFlags.AntiAlias);
        }

        #endregion
    }
}