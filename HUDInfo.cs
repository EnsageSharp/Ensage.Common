// <copyright file="HUDInfo.cs" company="EnsageSharp">
//    Copyright (c) 2016 EnsageSharp.
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

    using SharpDX;

    /// <summary>
    ///     Class used for locating several HUD positions
    /// </summary>
    public class HUDInfo
    {
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
        ///     The health bar x.
        /// </summary>
        private static readonly double HpBarX;

        /// <summary>
        ///     The health bar y.
        /// </summary>
        private static readonly float HpBarY;

        /// <summary>
        ///     The monitor.
        /// </summary>
        private static readonly float Monitor;

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
            var ratio = Math.Floor((decimal)(ScreenSize.X / ScreenSize.Y * 100));
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
            }
            else if (ratio == 177)
            {
                compareWidth = 1600;
                panelHeroSizeX = 55.09;
                tinfoHeroDown = 25.714;
                DireCompare = 2.5401;
                RadiantCompare = 3.499;
                HpBarHeight = 8.7;
                HpBarWidth = 84;
                HpBarX = 43;
                HpBarY = 27;
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
            }

            Monitor = ScreenSize.X / compareWidth;
            Rate = Math.Max(Monitor, 1);
            X = panelHeroSizeX * Monitor;
            y = ScreenSize.Y / tinfoHeroDown;
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

            if (unit.Handle.Equals(ObjectManager.LocalHero.Handle))
            {
                if (unit.ClassID == ClassID.CDOTA_Unit_Hero_Meepo)
                {
                    return screenPos + new Vector2((float)(-HpBarX * 1.05 * Monitor), (float)(-HpBarY * 1.3 * Monitor));
                }

                return screenPos + new Vector2((float)(-HpBarX * 1.015 * Monitor), (float)(-HpBarY * 1.38 * Monitor));
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
            if (unit != null && unit.Handle.Equals(ObjectManager.LocalHero.Handle))
            {
                return (float)((float)HpBarWidth * Monitor * 1.05);
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

        #endregion

        #region Methods

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