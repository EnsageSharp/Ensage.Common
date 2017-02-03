// <copyright file="HUDInfo.cs" company="EnsageSharp">
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
namespace Ensage.Common
{
    using System;
    using System.Collections.Generic;

    using Ensage.Common.Menu;
    using Ensage.Common.Objects.UtilityObjects;

    using SharpDX;

    using Rectangle = Ensage.Common.Objects.RenderObjects.Rectangle;

    /// <summary>
    ///     Class used for locating several HUD positions
    /// </summary>
    public static class HUDInfo
    {
        #region Constants

        /// <summary>
        ///     The map bottom.
        /// </summary>
        private const float MapBottom = -7521;

        /// <summary>
        ///     The map left.
        /// </summary>
        private const float MapLeft = -8068;

        /// <summary>
        ///     The map right.
        /// </summary>
        private const float MapRight = 7933;

        /// <summary>
        ///     The map top.
        /// </summary>
        private const float MapTop = 7679;

        #endregion

        #region Static Fields

        /// <summary>
        ///     The dire compare.
        /// </summary>
        private static readonly double DireCompare;

        /// <summary>
        ///     The health bar height.
        /// </summary>
        private static readonly double HpBarHeight;

        /// <summary>
        ///     The health bar width.
        /// </summary>
        private static readonly double HpBarWidth;

        /// <summary>
        ///     The player id dictionary.
        /// </summary>
        private static readonly Dictionary<float, int> PlayerIdDictionary = new Dictionary<float, int>();

        /// <summary>
        ///     The radiant compare.
        /// </summary>
        private static readonly double RadiantCompare;

        /// <summary>
        ///     The rate.
        /// </summary>
        private static readonly float Rate;

        /// <summary>
        ///     The screen size.
        /// </summary>
        private static readonly Vector2 ScreenSize;

        /// <summary>
        ///     The x.
        /// </summary>
        private static readonly double X;

        /// <summary>
        ///     The map height.
        /// </summary>
        private static float mapHeight = Math.Abs(MapBottom - MapTop);

        /// <summary>
        ///     The map width.
        /// </summary>
        private static float mapWidth = Math.Abs(MapLeft - MapRight);

        /// <summary>
        ///     The current minimap.
        /// </summary>
        private static Minimap minimap;

        private static bool minimapIsOnRight1;

        private static float minimapMapScaleX;

        private static float minimapMapScaleY;

        private static Rectangle rectangle;

        /// <summary>
        ///     The y.
        /// </summary>
        private static double y;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="HUDInfo" /> class.
        /// </summary>
        static HUDInfo()
        {
            double tinfoHeroDown;
            double panelHeroSizeX;
            float compareWidth;
            ScreenSize = new Vector2(Drawing.Width, Drawing.Height);
            if (ScreenSize.X == 0)
            {
                Console.WriteLine("Ensage couldnt determine your resolution, try to launch in window mode");
                return;
            }

            minimap = new Minimap(new Vector2(-2, 1), new Vector2(0));

            var ratio = Math.Floor((decimal)(ScreenSize.X / ScreenSize.Y * 100));

            var largeMinimap = Game.GetConsoleVar("dota_hud_extra_large_minimap").GetInt() == 1;

            // Console.WriteLine("Your screen ratio is " + ratio);
            if (ratio == 213)
            {
                compareWidth = 1600;
                panelHeroSizeX = 45.28;
                tinfoHeroDown = 25.714;
                DireCompare = 2.402;
                RadiantCompare = 3.08;
                HpBarHeight = 7;
                HpBarWidth = 69;
                HpBarX = 36;
                HpBarY = 23;
                minimap.Size = new Vector2(0.1070833333333333f * ScreenSize.X, 0.2240740740740741f * ScreenSize.Y);
                minimapMapScaleX = minimap.Size.X / mapWidth;
                minimapMapScaleY = minimap.Size.Y / mapHeight;
            }
            else if (ratio == 177)
            {
                compareWidth = 1600;
                panelHeroSizeX = 55.09;
                tinfoHeroDown = 25.714;
                DireCompare = 2.5401;
                RadiantCompare = 3.499;
                HpBarHeight = 10;
                HpBarWidth = 86.5;
                HpBarX = 44;
                HpBarY = 27;
                minimap.Size = new Vector2(0.1280833333333333f * ScreenSize.X, 0.2240740740740741f * ScreenSize.Y);
                minimapMapScaleX = minimap.Size.X / mapWidth;
                minimapMapScaleY = minimap.Size.Y / mapHeight;
            }
            else if (ratio == 166)
            {
                compareWidth = 1280;
                panelHeroSizeX = 47.19;
                tinfoHeroDown = 25.714;
                DireCompare = 2.59;
                RadiantCompare = 3.64;
                HpBarHeight = 7.4;
                HpBarWidth = 71;
                HpBarX = 37;
                HpBarY = 22;
                minimap.Size = new Vector2(0.1370833333333333f * ScreenSize.X, 0.2240740740740741f * ScreenSize.Y);
                minimapMapScaleX = minimap.Size.X / mapWidth;
                minimapMapScaleY = minimap.Size.Y / mapHeight;
            }
            else if (ratio == 160)
            {
                compareWidth = 1280;
                panelHeroSizeX = 48.95;
                tinfoHeroDown = 25.714;
                DireCompare = 2.609;
                RadiantCompare = 3.78;
                HpBarHeight = 9;
                HpBarWidth = 75;
                HpBarX = 38.3;
                HpBarY = 25;
                minimap.Size = new Vector2(0.1425833333333333f * ScreenSize.X, 0.2240740740740741f * ScreenSize.Y);
                minimapMapScaleX = minimap.Size.X / mapWidth;
                minimapMapScaleY = minimap.Size.Y / mapHeight;
            }
            else if (ratio == 150)
            {
                compareWidth = 1280;
                panelHeroSizeX = 51.39;
                tinfoHeroDown = 25.714;
                DireCompare = 2.64;
                RadiantCompare = 4.02;
                HpBarHeight = 8;
                HpBarWidth = 79.2;
                HpBarX = 40.2;
                HpBarY = 24;
                minimap.Size = new Vector2(0.1500233333333333f * ScreenSize.X, 0.2200940740740741f * ScreenSize.Y);
                minimapMapScaleX = minimap.Size.X / mapWidth;
                minimapMapScaleY = minimap.Size.Y / mapHeight;
            }
            else if (ratio == 133)
            {
                compareWidth = 1024;
                panelHeroSizeX = 47.21;
                tinfoHeroDown = 25.714;
                DireCompare = 2.775;
                RadiantCompare = 4.57;
                HpBarHeight = 8;
                HpBarWidth = 71;
                HpBarX = 36.6;
                HpBarY = 23;
                minimap.Size = new Vector2(0.1690833333333333f * ScreenSize.X, 0.2225740740740741f * ScreenSize.Y);
                minimapMapScaleX = minimap.Size.X / mapWidth;
                minimapMapScaleY = minimap.Size.Y / mapHeight;
            }
            else if (ratio == 125)
            {
                compareWidth = 1280;
                panelHeroSizeX = 58.3;
                tinfoHeroDown = 25.714;
                DireCompare = 2.78;
                RadiantCompare = 4.65;
                HpBarHeight = 11;
                HpBarWidth = 96.5;
                HpBarX = 49;
                HpBarY = 32;
                minimap.Size = new Vector2(0.1850833333333333f * ScreenSize.X, 0.2240740740740741f * ScreenSize.Y);
                minimapMapScaleX = minimap.Size.X / mapWidth;
                minimapMapScaleY = minimap.Size.Y / mapHeight;
            }
            else
            {
                Console.WriteLine(
                    @"Your screen resolution is not supported and drawings might have wrong size/position, (" + ratio
                    + ")");
                compareWidth = 1600;
                panelHeroSizeX = 65;
                tinfoHeroDown = 25.714;
                DireCompare = 2.655;
                RadiantCompare = 5.985;
                HpBarHeight = 10;
                HpBarWidth = 83.5;
                HpBarX = 43;
                HpBarY = 28;
                minimap.Size = new Vector2(0.1270833333333333f * ScreenSize.X, 0.2240740740740741f * ScreenSize.Y);
                minimapMapScaleX = minimap.Size.X / mapWidth;
                minimapMapScaleY = minimap.Size.Y / mapHeight;
            }

            if (largeMinimap)
            {
                const float LargeMinimapConstant = 1.1470833333333333f; // © Moones 2017
                minimap.Size *= LargeMinimapConstant;
                minimapMapScaleX = minimap.Size.X / mapWidth;
                minimapMapScaleY = minimap.Size.Y / mapHeight;
            }

            Monitor = ScreenSize.X / compareWidth;
            Rate = Math.Max(Monitor, 1);
            X = panelHeroSizeX * Monitor;
            y = ScreenSize.Y / tinfoHeroDown;

            var mouse = new Rectangle(new Vector2(5, 5), Color.White);
            GameUpdate update = args =>
                {
                    var mousePos = Game.MousePosition;

                    // if (Utils.SleepCheck("mouse"))
                    // {
                    // Console.WriteLine(mousePos);
                    // Utils.Sleep(500, "mouse");
                    // }
                    var minimapPos = mousePos.WorldToMinimap();
                    mouse.Position = minimapPos;
                };

            var mipos = new Vector3(MapLeft, MapTop, 0).WorldToMinimap();
            rectangle = new Rectangle(minimap.Size, new ColorBGRA(255, 255, 255, 25)) { Position = mipos };

            var menu = new Menu.Menu("HUDInfo", nameof(HUDInfo));
            var minimapOnRight =
                menu.AddItem(
                    new MenuItem(menu.Name + "minimapRight", "Minimap is on the right").SetValue(false)
                        .SetTooltip("Enable this if you have minimap on the right"));
            minimapOnRight.ValueChanged += (sender, args) => { MinimapIsOnRight = args.GetNewValue<bool>(); };
            MinimapIsOnRight = minimapOnRight.GetValue<bool>();
            var enableRectangle =
                menu.AddItem(
                    new MenuItem(menu.Name + "enablerectangle", "Enable minimap debug").SetTooltip(
                            "Draws rectangle over minimap in current e.common minimap size (requires -dx9), and shows current mouse position on minimap")
                        .SetValue(false));
            enableRectangle.SetValue(false);
            DrawingEndScene draw = eventArgs =>
                {
                    rectangle.Render();
                    mouse.Render();
                };
            enableRectangle.ValueChanged += (sender, args) =>
                {
                    if (args.GetNewValue<bool>())
                    {
                        rectangle.Initialize();
                        mouse.Initialize();
                        Drawing.OnEndScene += draw;
                        Game.OnUpdate += update;
                    }
                    else
                    {
                        rectangle.Dispose();
                        mouse.Dispose();
                        Drawing.OnEndScene -= draw;
                        Game.OnUpdate -= update;
                    }
                };

            DelayAction.Add(200, () => Menu.Menu.Root.AddSubMenu(menu));
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     The health bar x.
        /// </summary>
        public static double HpBarX { get; set; }

        /// <summary>
        ///     The health bar y.
        /// </summary>
        public static float HpBarY { get; set; }

        /// <summary>
        ///     The current minimap.
        /// </summary>
        public static Minimap Minimap
        {
            get
            {
                return minimap;
            }

            set
            {
                minimap = value;
            }
        }

        /// <summary>The minimap is on right.</summary>
        public static bool MinimapIsOnRight
        {
            get
            {
                return minimapIsOnRight1;
            }

            set
            {
                minimapIsOnRight1 = value;
                rectangle.Position = new Vector3(MapLeft, MapTop, 0).WorldToMinimap();
            }
        }

        /// <summary>
        ///     The monitor.
        /// </summary>
        public static float Monitor { get; set; }

        /// <summary>
        ///     Gets the mouse position from minimap.
        /// </summary>
        public static Vector2 MousePositionFromMinimap
        {
            get
            {
                var mouse = Game.MouseScreenPosition;

                var scaledX = mouse.X - minimap.Position.X;
                var scaledY = ScreenSize.Y - mouse.Y - minimap.Position.Y;

                var x = scaledX / minimapMapScaleX + MapLeft;
                var y = scaledY / minimapMapScaleY + MapBottom;

                if (Math.Abs(x) > 7900 || Math.Abs(y) > 7200)
                {
                    return Vector2.Zero;
                }

                return new Vector2(x, y);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Returns HealthBar position for given unit
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="Vector2" />.
        /// </returns>
        public static Vector2 GetHPbarPosition(Unit unit)
        {
            var pos = unit.Position + new Vector3(0, 0, unit.HealthBarOffset);
            Vector2 screenPos;
            if (!Drawing.WorldToScreen(pos, out screenPos))
            {
                return Vector2.Zero;
            }

            var localHero = ObjectManager.LocalHero;
            if (localHero != null && Equals(unit, localHero))
            {
                if (unit.ClassID == ClassID.CDOTA_Unit_Hero_Meepo)
                {
                    return screenPos + new Vector2((float)(-HpBarX * 1.05 * Monitor), (float)(-HpBarY * 1.3 * Monitor));
                }

                return screenPos + new Vector2((float)(-HpBarX * 1.05 * Monitor), (float)(-HpBarY * 1.38 * Monitor));
            }

            return screenPos + new Vector2((float)(-HpBarX * Monitor), -HpBarY * Monitor);
        }

        /// <summary>
        ///     Returns HealthBar X position for given unit
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float GetHPBarSizeX(Unit unit = null)
        {
            var hero = ObjectManager.LocalHero;
            if (unit != null && hero != null && Equals(unit.Handle, hero.Handle))
            {
                return (float)((float)HpBarWidth * Monitor * 1.08);
            }

            return (float)HpBarWidth * Monitor;
        }

        /// <summary>
        ///     Returns HealthBar Y position for given unit
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float GetHpBarSizeY(Unit unit = null)
        {
            var hero = ObjectManager.LocalHero;
            if (unit != null && hero != null && Equals(unit, hero))
            {
                return (float)(HpBarHeight * Monitor * 1.05);
            }

            return (float)(HpBarHeight * Monitor);
        }

        /// <summary>
        ///     Returns top panel position for given hero
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="Vector2" />.
        /// </returns>
        public static Vector2 GetTopPanelPosition(Hero hero)
        {
            int id;
            if (hero.Player == null)
            {
                if (PlayerIdDictionary.ContainsKey(hero.Handle))
                {
                    id = PlayerIdDictionary[hero.Handle];
                }
                else
                {
                    return Vector2.Zero;
                }
            }
            else
            {
                id = hero.Player.ID;
            }

            if (!PlayerIdDictionary.ContainsKey(hero.Handle))
            {
                PlayerIdDictionary.Add(hero.Handle, id);
            }
            else
            {
                PlayerIdDictionary[hero.Handle] = id;
            }

            return new Vector2((float)(GetXX(hero) - 20 * Monitor + X * id), 0);
        }

        /// <summary>
        ///     Returns top panel size
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="double[]" />.
        /// </returns>
        public static double[] GetTopPanelSize(Hero hero)
        {
            double[] size = { GetTopPanelSizeX(hero), GetTopPanelSizeY(hero) };
            return size;
        }

        /// <summary>
        ///     Returns top panel hero icon width
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double GetTopPanelSizeX(Hero hero)
        {
            return X;
        }

        /// <summary>
        ///     Returns top panel hero icon height
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double GetTopPanelSizeY(Hero hero)
        {
            return 35 * Rate;
        }

        /// <summary>
        ///     The ratio percentage.
        /// </summary>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float RatioPercentage()
        {
            return Monitor;
        }

        /// <summary>
        ///     Returns screen width
        /// </summary>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float ScreenSizeX()
        {
            return ScreenSize.X;
        }

        /// <summary>
        ///     Returns screen height
        /// </summary>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float ScreenSizeY()
        {
            return ScreenSize.Y;
        }

        /// <summary>
        ///     The world to minimap.
        /// </summary>
        /// <param name="mapPosition">
        ///     The map position.
        /// </param>
        /// <returns>
        ///     The <see cref="Vector2" />.
        /// </returns>
        public static Vector2 WorldToMinimap(this Vector3 mapPosition)
        {
            var x = mapPosition.X - MapLeft;
            var y = mapPosition.Y - MapBottom;

            var scaledX = Math.Min(Math.Max(x * minimapMapScaleX, 0), minimap.Size.X);
            var scaledY = Math.Min(Math.Max(y * minimapMapScaleY, 0), minimap.Size.Y);

            float screenX;
            if (!MinimapIsOnRight)
            {
                screenX = minimap.Position.X + scaledX;
            }
            else
            {
                screenX = ScreenSize.X - minimap.Position.X + scaledX - minimap.Size.X;
            }

            var screenY = ScreenSize.Y - scaledY - minimap.Position.Y;

            return new Vector2((float)Math.Floor(screenX), (float)Math.Floor(screenY));
        }

        #endregion

        #region Methods

        private static void Drawing_OnDraw(EventArgs args)
        {
            var mousePos = Game.MousePosition;
            if (Utils.SleepCheck("mouse"))
            {
                Console.WriteLine(mousePos);
                Utils.Sleep(500, "mouse");
            }

            Drawing.DrawRect(mousePos.WorldToMinimap(), new Vector2(5, 5), Color.White);
        }

        /// <summary>
        ///     The get xx.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        private static double GetXX(Entity hero)
        {
            var screenSize = new Vector2(Drawing.Width, Drawing.Height);
            if (hero.Team == Team.Radiant)
            {
                return screenSize.X / RadiantCompare + 1;
            }

            return screenSize.X / DireCompare + 1;
        }

        #endregion
    }
}