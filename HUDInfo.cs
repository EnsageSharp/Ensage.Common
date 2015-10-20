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
            if (ratio == 177)
            {
                compareWidth = 1600;
                panelHeroSizeX = 55.65;
                tinfoHeroDown = 25.714;
                DireCompare = 2.545;
                RadiantCompare = 4.59;
            }
            else if (ratio == 166)
            {
                compareWidth = 1280;
                panelHeroSizeX = 47.19;
                tinfoHeroDown = 25.714;
                DireCompare = 2.56;
                RadiantCompare = 4.95;
            }
            else if (ratio == 160)
            {
                compareWidth = 1280;
                panelHeroSizeX = 48.95;
                tinfoHeroDown = 25.714;
                DireCompare = 2.609;
                RadiantCompare = 5.32;
            }
            else if (ratio == 133)
            {
                compareWidth = 1024;
                panelHeroSizeX = 47.42;
                tinfoHeroDown = 25.714;
                DireCompare = 2.8;
                RadiantCompare = 8.1;
            }
            else if (ratio == 125)
            {
                compareWidth = 1280;
                panelHeroSizeX = 59.2;
                tinfoHeroDown = 25.714;
                DireCompare = 2.786;
                RadiantCompare = 8.14;
            }
            else
            {
                compareWidth = 1600;
                panelHeroSizeX = 65;
                tinfoHeroDown = 25.714;
                DireCompare = 2.655;
                RadiantCompare = 5.985;
            }
            var monitor = screenSize.X / compareWidth;
            Rate = Math.Max(monitor, 1);
            X = panelHeroSizeX * monitor;
            y = screenSize.Y / tinfoHeroDown;
        }

        #endregion

        #region Public Methods and Operators

        public static Vector2 GetTopPanelPosition(Hero hero)
        {
            var id = hero.Player.ID;
            return new Vector2((float)(GetXX(hero) - 20 + X * id), 0);
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