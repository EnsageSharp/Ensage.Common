// <copyright file="MenuSettings.cs" company="EnsageSharp">
//    Copyright (c) 2015 EnsageSharp.
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
        public static Vector2 BasePosition = new Vector2(10, (float)(HUDInfo.ScreenSizeY() * 0.06));

        /// <summary>
        ///     The _draw the menu.
        /// </summary>
        private static bool drawTheMenu;

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
                     + (Menu.Root.Item("EnsageSharp.Common.IncreaseSize").GetValue<Slider>().Value * 1.7)); // 32
            }
        }

        /// <summary>
        ///     Gets the menu item width.
        /// </summary>
        public static int MenuItemWidth
        {
            get
            {
                return
                    (int)
                    (Math.Max((int)(HUDInfo.GetHPBarSizeX() * 2), 180)
                     + Menu.Root.Item("EnsageSharp.Common.IncreaseSize").GetValue<Slider>().Value * 1.3); // 160
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
                return Math.Min((int)(HUDInfo.GetHPBarSizeX() * 2), 100)
                       + Menu.Root.Item("EnsageSharp.Common.IncreaseSize").GetValue<Slider>().Value
                       + RootMenuWidthIncrease; // 160
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

            if (Game.IsChatOpen)
            {
                return;
            }

            if ((args.Msg == (uint)Utils.WindowsMessages.WM_KEYUP || args.Msg == (uint)Utils.WindowsMessages.WM_KEYDOWN)
                && args.WParam == CommonMenu.MenuConfig.Item("pressKey").GetValue<KeyBind>().Key)
            {
                DrawMenu = args.Msg == (uint)Utils.WindowsMessages.WM_KEYDOWN;
            }

            if (args.Msg == (uint)Utils.WindowsMessages.WM_KEYUP
                && args.WParam == CommonMenu.MenuConfig.Item("toggleKey").GetValue<KeyBind>().Key)
            {
                DrawMenu = !DrawMenu;
            }
        }

        #endregion
    }
}