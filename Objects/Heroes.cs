// <copyright file="Heroes.cs" company="EnsageSharp">
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
namespace Ensage.Common.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.Common.Objects.UtilityObjects;

    /// <summary>
    ///     The heroes.
    /// </summary>
    public class Heroes
    {
        #region Static Fields

        /// <summary>
        ///     The all.
        /// </summary>
        public static List<Hero> All;

        /// <summary>
        ///     The dire.
        /// </summary>
        public static List<Hero> Dire;

        /// <summary>
        ///     The radiant.
        /// </summary>
        public static List<Hero> Radiant;

        /// <summary>
        ///     The loaded.
        /// </summary>
        private static bool loaded;

        /// <summary>The teams.</summary>
        private static Dictionary<Team, List<Hero>> teams;

        /// <summary>
        ///     The temp list.
        /// </summary>
        private static List<Hero> tempList;

        /// <summary>The update sleeper.</summary>
        private static Sleeper updateSleeper;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="Heroes" /> class.
        /// </summary>
        static Heroes()
        {
            All = new List<Hero>();
            Dire = new List<Hero>();
            Radiant = new List<Hero>();
            teams = new Dictionary<Team, List<Hero>>();
            Events.OnLoad += (sender, args) =>
                {
                    if (loaded)
                    {
                        return;
                    }

                    Load();
                    loaded = true;
                };
            if (!loaded && ObjectManager.LocalHero != null && Game.IsInGame)
            {
                Load();
                loaded = true;
            }

            Events.OnClose += (sender, args) =>
                {
                    All = new List<Hero>();
                    Dire = new List<Hero>();
                    Radiant = new List<Hero>();
                    tempList = new List<Hero>();
                    Events.OnUpdate -= Update;
                    ObjectManager.OnAddEntity -= ObjectMgr_OnAddEntity;
                    ObjectManager.OnRemoveEntity -= ObjectMgr_OnRemoveEntity;
                    loaded = false;
                };
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets the teams.</summary>
        public static IReadOnlyDictionary<Team, List<Hero>> Teams => teams;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The get by team.
        /// </summary>
        /// <param name="team">
        ///     The team.
        /// </param>
        /// <returns>
        ///     The <see cref="List" />.
        /// </returns>
        public static List<Hero> GetByTeam(Team team)
        {
            return team == Team.Radiant
                       ? Radiant
                       : (team == Team.Dire ? Dire : teams.FirstOrDefault(x => x.Key == team).Value);
        }

        /// <summary>
        ///     The update.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        public static void Update(EventArgs args)
        {
            if (!Game.IsInGame)
            {
                return;
            }

            if (updateSleeper.Sleeping || !Game.IsCustomGame && All.Count(x => x.IsValid) >= 10)
            {
                return;
            }

            UpdateHeroes();
            updateSleeper.Sleep(1000);
        }

        /// <summary>
        ///     The update heroes.
        /// </summary>
        public static void UpdateHeroes()
        {
            var herolist = new List<Hero>(All);
            var herolistRadiant = new List<Hero>(Radiant);
            var herolistDire = new List<Hero>(Dire);
            foreach (var hero in tempList)
            {
                if (!(hero != null && hero.IsValid))
                {
                    continue;
                }

                if (!All.Contains(hero))
                {
                    herolist.Add(hero);
                }

                if (hero.Team == Team.Radiant)
                {
                    if (!Radiant.Contains(hero))
                    {
                        herolistRadiant.Add(hero);
                    }
                }
                else if (hero.Team == Team.Dire)
                {
                    if (!Dire.Contains(hero))
                    {
                        herolistDire.Add(hero);
                    }
                }
                else
                {
                    List<Hero> list;
                    if (!teams.TryGetValue(hero.Team, out list))
                    {
                        list = new List<Hero> { hero };
                        teams.Add(hero.Team, list);
                        continue;
                    }

                    var temp = new List<Hero>(list) { hero };
                    teams[hero.Team] = temp;
                }
            }

            All = herolist;
            Radiant = herolistRadiant;
            Dire = herolistDire;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The load.
        /// </summary>
        private static void Load()
        {
            updateSleeper = new Sleeper();
            All = new List<Hero> { ObjectManager.LocalHero };
            Dire = new List<Hero>();
            Radiant = new List<Hero>();
            teams = new Dictionary<Team, List<Hero>> { { Team.Radiant, Radiant }, { Team.Dire, Dire } };
            if (ObjectManager.LocalHero.Team == Team.Dire)
            {
                Dire.Add(ObjectManager.LocalHero);
            }
            else if (ObjectManager.LocalHero.Team == Team.Radiant)
            {
                Radiant.Add(ObjectManager.LocalHero);
            }
            else
            {
                teams.Add(ObjectManager.LocalHero.Team, new List<Hero> { ObjectManager.LocalHero });
            }

            tempList = Players.All.Where(x => x.Hero != null && x.Hero.IsValid).Select(x => x.Hero).ToList();
            foreach (var hero in
                ObjectManager.GetEntities<Hero>()
                    .Where(hero => !hero.IsIllusion && tempList.All(x => x.Handle != hero.Handle)))
            {
                tempList.Add(hero);
            }

            UpdateHeroes();
            Events.OnUpdate += Update;
            ObjectManager.OnAddEntity += ObjectMgr_OnAddEntity;
            ObjectManager.OnRemoveEntity += ObjectMgr_OnRemoveEntity;
        }

        /// <summary>
        ///     The object mgr_ on add entity.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private static void ObjectMgr_OnAddEntity(EntityEventArgs args)
        {
            var hero = args.Entity as Hero;
            if (hero == null || hero.IsIllusion)
            {
                return;
            }

            tempList.Add(hero);
            if (!All.Contains(hero))
            {
                All.Add(hero);
            }

            if (hero.Team == Team.Radiant)
            {
                if (!Radiant.Contains(hero))
                {
                    Radiant.Add(hero);
                }
            }
            else if (hero.Team == Team.Dire)
            {
                if (!Dire.Contains(hero))
                {
                    Dire.Add(hero);
                }
            }
            else
            {
                List<Hero> list;
                if (!teams.TryGetValue(hero.Team, out list))
                {
                    list = new List<Hero> { hero };
                    teams.Add(hero.Team, list);
                    return;
                }

                var temp = new List<Hero>(list) { hero };
                teams[hero.Team] = temp;
            }
        }

        /// <summary>
        ///     The object mgr_ on remove entity.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private static void ObjectMgr_OnRemoveEntity(EntityEventArgs args)
        {
            var hero = args.Entity as Hero;
            if (hero == null || hero.IsIllusion)
            {
                return;
            }

            tempList.Remove(hero);
            if (All.Contains(hero))
            {
                All.Remove(hero);
            }

            if (hero.Team == Team.Radiant)
            {
                if (Radiant.Contains(hero))
                {
                    Radiant.Remove(hero);
                }
            }
            else if (hero.Team == Team.Dire)
            {
                if (Dire.Contains(hero))
                {
                    Dire.Remove(hero);
                }
            }
            else
            {
                if (!teams.ContainsKey(hero.Team))
                {
                    return;
                }

                teams[hero.Team].Remove(hero);
            }
        }

        #endregion
    }
}