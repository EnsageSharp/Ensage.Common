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
            var ratio = Math.Floor((double)((Drawing.Width / Drawing.Height) * 100));
            if (ratio == 177)
            {
                compareWidth = 1600;
                panelHeroSizeX = 55;
                tinfoHeroDown = 25.714;
                DireCompare = 2.535;
                RadiantCompare = 3.485;
            }
            else if (ratio == 166)
            {
                compareWidth = 1280;
                panelHeroSizeX = 47.1;
                tinfoHeroDown = 25.714;
                DireCompare = 2.558;
                RadiantCompare = 3.62;
            }
            else if (ratio == 160)
            {
                compareWidth = 1280;
                panelHeroSizeX = 48.5;
                tinfoHeroDown = 25.714;
                DireCompare = 2.579;
                RadiantCompare = 3.74;
            }
            else if (ratio == 133)
            {
                compareWidth = 1024;
                panelHeroSizeX = 47;
                tinfoHeroDown = 25.714;
                DireCompare = 2.78;
                RadiantCompare = 4.63;
            }
            else if (ratio == 125)
            {
                compareWidth = 1280;
                panelHeroSizeX = 58;
                tinfoHeroDown = 25.714;
                DireCompare = 2.747;
                RadiantCompare = 4.54;
            }
            else
            {
                compareWidth = 1600;
                panelHeroSizeX = 55;
                tinfoHeroDown = 25.714;
                DireCompare = 2.535;
                RadiantCompare = 3.485;
            }
            var monitor = Drawing.Width / compareWidth;
            Rate = Math.Max(monitor, 1);
            X = panelHeroSizeX * monitor;
            y = Drawing.Height / tinfoHeroDown;
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
            if (hero.Team == Team.Radiant)
            {
                return Drawing.Width / RadiantCompare + 1;
            }
            return Drawing.Width / DireCompare;
        }

        #endregion
    }
}