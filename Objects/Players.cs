namespace Ensage.Common.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// </summary>
    public class Players
    {
        #region Static Fields

        /// <summary>
        /// </summary>
        public static List<Player> All;

        /// <summary>
        /// </summary>
        public static List<Player> Dire;

        /// <summary>
        /// </summary>
        public static List<Player> Radiant;

        private static bool loaded;

        #endregion

        #region Constructors and Destructors

        static Players()
        {
            All = ObjectManager.GetEntities<Player>().ToList();
            Dire = All.Where(x => x.Team == Team.Radiant).ToList();
            Radiant = All.Where(x => x.Team == Team.Dire).ToList();
            Events.OnLoad += (sender, args) =>
                {
                    if (loaded)
                    {
                        return;
                    }

                    Load();
                };
            if (!loaded && Game.IsInGame && ObjectManager.LocalHero != null)
            {
                Load();
            }

            Events.OnClose += (sender, args) =>
                {
                    All = new List<Player>();
                    Dire = new List<Player>();
                    Radiant = new List<Player>();
                    Game.OnUpdate -= Update;
                    loaded = false;
                };
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        /// <param name="args"></param>
        public static void Update(EventArgs args)
        {
            if (!Game.IsInGame)
            {
                return;
            }

            if (!Utils.SleepCheck("Common.Players.Update"))
            {
                return;
            }

            if (All.Count < 10)
            {
                All = ObjectManager.GetEntities<Player>().ToList();
            }

            if (Radiant.Count < 5)
            {
                Radiant = All.Where(x => x.Team == Team.Radiant).ToList();
            }

            if (Dire.Count < 5)
            {
                Dire = All.Where(x => x.Team == Team.Dire).ToList();
            }

            Utils.Sleep(1000, "Common.Players.Update");
        }

        #endregion

        #region Methods

        private static void Load()
        {
            All = ObjectManager.GetEntities<Player>().ToList();
            Dire = All.Where(x => x.Team == Team.Radiant).ToList();
            Radiant = All.Where(x => x.Team == Team.Dire).ToList();
            Game.OnUpdate += Update;
            loaded = true;
            Update(null);
        }

        #endregion
    }
}