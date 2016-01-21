namespace Ensage.Common.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Creeps
    {
        #region Static Fields

        /// <summary>
        /// </summary>
        public static List<Creep> All;

        private static bool loaded;

        #endregion

        #region Constructors and Destructors

        static Creeps()
        {
            All = new List<Creep>();
            Events.OnLoad += (sender, args) =>
                {
                    if (loaded)
                    {
                        return;
                    }

                    All = new List<Creep>();
                    Game.OnUpdate += Update;
                    loaded = true;
                };
            if (!loaded && ObjectMgr.LocalHero != null && Game.IsInGame)
            {
                All = new List<Creep>();
                Game.OnUpdate += Update;
                loaded = true;
            }

            Events.OnClose += (sender, args) =>
                {
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
            if (!Game.IsInGame || Game.IsPaused)
            {
                return;
            }
            if (!Utils.SleepCheck("Common.Creeps.Update"))
            {
                return;
            }

            UpdateCreeps();
            Utils.Sleep(500, "Common.Creeps.Update");
        }

        /// <summary>
        /// </summary>
        public static void UpdateCreeps()
        {
            All =
                ObjectMgr.GetEntities<Creep>()
                    .Where(creep => creep.IsAlive && creep.IsSpawned && creep.IsVisible)
                    .ToList();
        }

        #endregion
    }
}