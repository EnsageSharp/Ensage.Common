namespace Ensage.Common.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// </summary>
    public class Heroes
    {
        #region Static Fields

        /// <summary>
        /// </summary>
        public static List<Hero> All;

        /// <summary>
        /// </summary>
        public static List<Hero> Dire;

        /// <summary>
        /// </summary>
        public static List<Hero> Radiant;

        #endregion

        #region Constructors and Destructors

        static Heroes()
        {
            All = new List<Hero>();
            Dire = new List<Hero>();
            Radiant = new List<Hero>();
            Events.OnLoad += (sender, args) =>
                {
                    All = new List<Hero>();
                    Dire = new List<Hero>();
                    Radiant = new List<Hero>();
                };
            Game.OnUpdate += Update;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        public static List<Hero> GetByTeam(Team team)
        {
            return team == Team.Radiant ? Radiant : Dire;
        }

        /// <summary>
        /// </summary>
        /// <param name="args"></param>
        public static void Update(EventArgs args)
        {
            if (!Game.IsInGame || Game.IsPaused)
            {
                return;
            }
            if (!Utils.SleepCheck("Common.Heroes.Update") || All.Count(x => x.IsValid) == 10)
            {
                return;
            }
            UpdateHeroes();
            Utils.Sleep(1000, "Common.Heroes.Update");
        }

        /// <summary>
        /// </summary>
        public static void UpdateHeroes()
        {
            var list = new List<Player>(Players.All);
            var herolist = new List<Hero>(All);
            var herolistRadiant = new List<Hero>(Radiant);
            var herolistDire = new List<Hero>(Dire);
            foreach (var hero in list.Where(x => x.Hero != null).Select(p => p.Hero))
            {
                if (!All.Contains(hero))
                {
                    herolist.Add(hero);
                }
                if (!Radiant.Contains(hero) && hero.Team == Team.Radiant)
                {
                    herolistRadiant.Add(hero);
                }
                if (!Dire.Contains(hero) && hero.Team == Team.Dire)
                {
                    herolistDire.Add(hero);
                }
            }
            All = herolist;
            Radiant = herolistRadiant;
            Dire = herolistDire;
        }

        #endregion
    }
}