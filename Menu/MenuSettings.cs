// <copyright file="MenuSettings.cs" company="EnsageSharp">
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
    using System.IO;

    using SharpDX;

    using Color = System.Drawing.Color;

    /// <summary>
    ///     The menu settings.
    /// </summary>
    internal static class MenuSettings
    {
        #region Static Fields

        /// <summary>
        ///     The base position.
        /// </summary>
        public static Vector2 BasePosition = new Vector2(10, (float)(HUDInfo.ScreenSizeY() * 0.08));

        private static bool dragging;

        /// <summary>
        ///     The _draw the menu.
        /// </summary>
        private static bool drawTheMenu;

        private static Vector2 mouseDifference;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="MenuSettings" /> class.
        /// </summary>
        static MenuSettings()
        {
            Game.OnWndProc += Game_OnWndProc;
            drawTheMenu = MenuGlobals.DrawMenu;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the active background color.
        /// </summary>
        public static Color ActiveBackgroundColor
        {
            get
            {
                return Color.FromArgb(210, 48, 48, 48);
            }
        }

        /// <summary>
        ///     Gets the background color.
        /// </summary>
        public static Color BackgroundColor
        {
            get
            {
                return Color.FromArgb(200, Color.Black);
            }
        }

        /// <summary>
        ///     Gets or sets the menu font size.
        /// </summary>
        public static int MenuFontSize { get; set; }

        /// <summary>
        ///     Gets the menu item height.
        /// </summary>
        public static int MenuItemHeight
        {
            get
            {
                return
                    (int)
                    (Math.Min(Math.Max((int)(HUDInfo.GetHpBarSizeY() * 2.5), 30), 42)
                     + CommonMenu.IncreaseMenuSize * 0.17f); // 32
            }
        }

        /// <summary>
        ///     Gets the menu item width.
        /// </summary>
        public static int MenuItemWidth
        {
            get
            {
                return (int)(Math.Max((int)(HUDInfo.GetHPBarSizeX() * 2), 180) + CommonMenu.IncreaseMenuSize * 0.13f);

                // 160
            }
        }

        /// <summary>
        ///     Gets the menu menu config path.
        /// </summary>
        public static string MenuMenuConfigPath
        {
            get
            {
                return Path.Combine(MenuConfig.AppDataDirectory, "MenuConfig");
            }
        }

        /// <summary>
        ///     Gets the menu width.
        /// </summary>
        public static int MenuWidth
        {
            get
            {
                return (int)(HUDInfo.GetHPBarSizeX() * 0.1 + CommonMenu.IncreaseMenuSize * 0.1 + RootMenuWidthIncrease);

                // 160
            }
        }

        /// <summary>
        ///     Gets or sets the root menu width increase.
        /// </summary>
        public static int RootMenuWidthIncrease { get; set; }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets a value indicating whether draw menu.
        /// </summary>
        internal static bool DrawMenu
        {
            get
            {
                return drawTheMenu;
            }

            set
            {
                drawTheMenu = value;
                MenuGlobals.DrawMenu = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The game_ on wnd proc.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private static void Game_OnWndProc(WndEventArgs args)
        {
            if (!Game.IsInGame)
            {
                return;
            }

            var mousePos = Game.MouseScreenPosition;
            if (args.Msg == (ulong)Utils.WindowsMessages.WM_LBUTTONDOWN
                && Utils.IsUnderRectangle(
                    mousePos,
                    Menu.MenuPanel.Position.X,
                    Menu.MenuPanel.Position.Y,
                    Menu.MenuPanel.Size.X,
                    Menu.MenuPanel.Size.Y))
            {
                mouseDifference = mousePos - BasePosition;
                dragging = true;
                return;
            }

            if (dragging && args.Msg == (ulong)Utils.WindowsMessages.WM_LBUTTONUP)
            {
                dragging = false;
                Menu.Root.Item("positionX").SetValue(new Slider((int)BasePosition.X, 10, Drawing.Height / 3));
                Menu.Root.Item("positionY")
                    .SetValue(new Slider((int)BasePosition.Y, (int)(HUDInfo.ScreenSizeY() * 0.08), Drawing.Width / 4));
                return;
            }

            if (args.Msg == (ulong)Utils.WindowsMessages.WM_MOUSEMOVE && dragging)
            {
                BasePosition = new Vector2(
                    Math.Max(Math.Min(mousePos.X - mouseDifference.X, Drawing.Height / 3), 10),
                    Math.Max(
                        Math.Min(mousePos.Y - mouseDifference.Y, Drawing.Width / 4),
                        (int)(HUDInfo.ScreenSizeY() * 0.08)));
                Menu.Root.Item("positionX").SetValue(new Slider((int)BasePosition.X, 10, Drawing.Height / 3));
                Menu.Root.Item("positionY")
                    .SetValue(new Slider((int)BasePosition.Y, (int)(HUDInfo.ScreenSizeY() * 0.08), Drawing.Width / 4));
            }

            if (Game.IsChatOpen)
            {
                return;
            }

            if ((args.Msg == (uint)Utils.WindowsMessages.WM_KEYUP || args.Msg == (uint)Utils.WindowsMessages.WM_KEYDOWN)
                && args.WParam == Menu.Root.Item("pressKey").GetValue<KeyBind>().Key)
            {
                DrawMenu = args.Msg == (uint)Utils.WindowsMessages.WM_KEYDOWN;
            }

            if (args.Msg == (uint)Utils.WindowsMessages.WM_KEYUP
                && args.WParam == Menu.Root.Item("toggleKey").GetValue<KeyBind>().Key)
            {
                DrawMenu = !DrawMenu;
            }
        }

        #endregion
    }
}