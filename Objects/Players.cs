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

        #endregion

        #region Constructors and Destructors

        static Players()
        {
            All = new List<Player>();
            Dire = new List<Player>();
            Radiant = new List<Player>();
            Game.OnUpdate += Update;
            Events.OnLoad += (sender, args) =>
                {
                    All = new List<Player>();
                    Dire = new List<Player>();
                    Radiant = new List<Player>();
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
            if (!Utils.SleepCheck("Common.Players.Update"))
            {
                return;
            }
            if (All.Count < 10)
            {
                All = ObjectMgr.GetEntities<Player>().ToList();
            }
            if (Radiant.Count < 5)
            {
                Radiant = ObjectMgr.GetEntities<Player>().Where(x => x.Team == Team.Radiant).ToList();
            }
            if (Dire.Count < 5)
            {
                Dire = ObjectMgr.GetEntities<Player>().Where(x => x.Team == Team.Dire).ToList();
            }
            Utils.Sleep(1000, "Common.Players.Update");
        }

        #endregion
    }
}