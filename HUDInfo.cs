namespace Ensage.Common
{
    using System;

    using SharpDX;

    /// <summary>
    ///     Class used for locating several HUD positions
    /// </summary>
    public class HUDInfo
    {
        #region Static Fields

        private static readonly double DireCompare;

        private static readonly double HpBarHeight;

        private static readonly double HpBarWidth;

        private static readonly double HpBarX;

        private static readonly float HpBarY;

        private static readonly float Monitor;

        private static readonly double RadiantCompare;

        private static readonly float Rate;

        private static readonly Vector2 ScreenSize;

        private static readonly double X;

        private static double y;

        #endregion

        #region Constructors and Destructors

        static HUDInfo()
        {
            double tinfoHeroDown;
            double panelHeroSizeX;
            float compareWidth;
            ScreenSize = new Vector2(Drawing.Width, Drawing.Height);
            var ratio = Math.Floor((decimal)(ScreenSize.X / ScreenSize.Y * 100));
            Console.WriteLine(ratio);
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
        /// <param name="unit"></param>
        /// <returns></returns>
        public static Vector2 GetHPbarPosition(Unit unit)
        {
            var pos = unit.Position + new Vector3(0, 0, unit.HealthBarOffset);
            Vector2 screenPos;
            if (!Drawing.WorldToScreen(pos, out screenPos))
            {
                return Vector2.Zero;
            }
            if (unit.Equals(ObjectMgr.LocalHero))
            {
                return screenPos + new Vector2((float)(-HpBarX * Monitor), (-HpBarY - 10) * Monitor);
            }
            return screenPos + new Vector2((float)(-HpBarX * Monitor), -HpBarY * Monitor);
        }

        /// <summary>
        ///     Returns HealthBar X position for given unit
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static float GetHPBarSizeX(Unit unit = null)
        {
            return (float)HpBarWidth * Monitor;
        }

        /// <summary>
        ///     Returns HealthBar Y position for given unit
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static float GetHpBarSizeY(Unit unit = null)
        {
            return (float)(HpBarHeight * Monitor);
        }

        /// <summary>
        ///     Returns top panel position for given hero
        /// </summary>
        /// <param name="hero"></param>
        /// <returns></returns>
        public static Vector2 GetTopPanelPosition(Hero hero)
        {
            var id = hero.Player.ID;
            return new Vector2((float)(GetXX(hero) - 20 * Monitor + X * id), 0);
        }

        /// <summary>
        ///     Returns top panel size
        /// </summary>
        /// <param name="hero"></param>
        /// <returns></returns>
        public static double[] GetTopPanelSize(Hero hero)
        {
            double[] size = { GetTopPanelSizeX(hero), GetTopPanelSizeY(hero) };
            return size;
        }

        /// <summary>
        ///     Returns top panel hero icon width
        /// </summary>
        /// <param name="hero"></param>
        /// <returns></returns>
        public static double GetTopPanelSizeX(Hero hero)
        {
            return X;
        }

        /// <summary>
        ///     Returns top panel hero icon height
        /// </summary>
        /// <param name="hero"></param>
        /// <returns></returns>
        public static double GetTopPanelSizeY(Hero hero)
        {
            return 35 * Rate;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public static float RatioPercentage()
        {
            return Monitor;
        }

        /// <summary>
        ///     Returns screen width
        /// </summary>
        /// <returns></returns>
        public static float ScreenSizeX()
        {
            return ScreenSize.X;
        }

        /// <summary>
        ///     Returns screen height
        /// </summary>
        /// <returns></returns>
        public static float ScreenSizeY()
        {
            return ScreenSize.Y;
        }

        #endregion

        #region Methods

        private static double GetXX(Hero hero)
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