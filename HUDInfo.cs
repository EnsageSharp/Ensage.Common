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

        private static readonly double RadiantCompare;

        private static readonly float Rate;

        private static readonly float Monitor;

        private static readonly double HpBarWidth;

        private static readonly double HpBarHeight;

        private static readonly double HpBarX;

        private static readonly float HpBarY;

        private static readonly double X;

        private static double y;

        #endregion

        #region Constructors and Destructors

        static HUDInfo()
        {
            double tinfoHeroDown;
            double panelHeroSizeX;
            float compareWidth;
            var screenSize = new Vector2(Drawing.Width, Drawing.Height);
            var ratio = Math.Floor((decimal)(screenSize.X / screenSize.Y * 100));
            Console.WriteLine(ratio);
            if (ratio == 213)
            {
                compareWidth = 1600;
                panelHeroSizeX = 45.28;
                tinfoHeroDown = 25.714;
                DireCompare = 2.402;
                RadiantCompare = 3.08;
                HpBarHeight = 10;
                HpBarWidth = 83.5;
                HpBarX = 43;
                HpBarY = 28;
            }
            else if (ratio == 177)
            {
                compareWidth = 1600;
                panelHeroSizeX = 55.09;
                tinfoHeroDown = 25.714;
                DireCompare = 2.5401;
                RadiantCompare = 3.499;
                HpBarHeight = 8.7;
                HpBarWidth = 87.6;
                HpBarX = 44.8;
                HpBarY = 27;
            }
            else if (ratio == 166)
            {
                compareWidth = 1280;
                panelHeroSizeX = 47.19;
                tinfoHeroDown = 25.714;
                DireCompare = 2.56;
                RadiantCompare = 4.95;
                HpBarHeight = 7;
                HpBarWidth = 71;
                HpBarX = 37;
                HpBarY = 23;
            }
            else if (ratio == 160)
            {
                compareWidth = 1280;
                panelHeroSizeX = 48.95;
                tinfoHeroDown = 25.714;
                DireCompare = 2.609;
                RadiantCompare = 5.32;
                HpBarHeight = 8;
                HpBarWidth = 74;
                HpBarX = 40;
                HpBarY = 25;
            }
            else if (ratio == 133)
            {
                compareWidth = 1024;
                panelHeroSizeX = 47.21;
                tinfoHeroDown = 25.714;
                DireCompare = 2.775;
                RadiantCompare = 4.57;
                HpBarHeight = 10;
                HpBarWidth = 72;
                HpBarX = 37;
                HpBarY = 23;
            }
            else if (ratio == 125)
            {
                compareWidth = 1280;
                panelHeroSizeX = 58.3;
                tinfoHeroDown = 25.714;
                DireCompare = 2.78;
                RadiantCompare = 4.65;
                HpBarHeight = 10;
                HpBarWidth = 94;
                HpBarX = 48;
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
            Monitor = screenSize.X / compareWidth;
            Rate = Math.Max(Monitor, 1);
            X = panelHeroSizeX * Monitor;
            y = screenSize.Y / tinfoHeroDown;
        }

        #endregion

        #region Public Methods and Operators

        public static Vector2 GetTopPanelPosition(Hero hero)
        {
            var id = hero.Player.ID;
            return new Vector2((float)(GetXX(hero) - 20 * Monitor + X * id), 0);
        }

        public static double[] GetTopPanelSize(Hero hero)
        {
            double[] size = { GetTopPanelSizeX(hero), GetTopPanelSizeY(hero) };
            return size;
        }

        public static double GetTopPanelSizeX(Hero hero)
        {
            return X;
        }

        public static double GetTopPanelSizeY(Hero hero)
        {
            return 35 * Rate;
        }

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
                return screenPos + new Vector2((float)(-HpBarX * Monitor), (-HpBarY-10) * Monitor);
            }
            return screenPos + new Vector2((float)(-HpBarX * Monitor), -HpBarY * Monitor);
        }

        public static float GetHPBarSizeX(Unit unit)
        {
            return (float)HpBarWidth * Monitor;
        }

        public static float GetHpBarSizeY(Unit unit)
        {
            return (float)(HpBarHeight * Monitor);
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