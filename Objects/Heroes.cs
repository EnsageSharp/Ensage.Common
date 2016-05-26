// <copyright file="Heroes.cs" company="EnsageSharp">
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
namespace Ensage.Common.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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

        /// <summary>
        ///     The temp list.
        /// </summary>
        private static List<Hero> tempList;

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
            return team == Team.Radiant ? Radiant : Dire;
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

            if (!Utils.SleepCheck("Common.Heroes.Update") || All.Count(x => x.IsValid) >= 10)
            {
                return;
            }

            UpdateHeroes();
            Utils.Sleep(1000, "Common.Heroes.Update");
        }

        /// <summary>
        ///     The update heroes.
        /// </summary>
        public static void UpdateHeroes()
        {
            var herolist = new List<Hero>(All);
            var herolistRadiant = new List<Hero>(Radiant);
            var herolistDire = new List<Hero>(Dire);
            foreach (var hero in tempList.Where(x => x != null && x.IsValid))
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

        #region Methods

        /// <summary>
        ///     The load.
        /// </summary>
        private static void Load()
        {
            All = new List<Hero> { ObjectManager.LocalHero };
            Dire = new List<Hero>();
            Radiant = new List<Hero>();
            if (ObjectManager.LocalHero.Team == Team.Dire)
            {
                Dire.Add(ObjectManager.LocalHero);
            }
            else
            {
                Radiant.Add(ObjectManager.LocalHero);
            }

            tempList = Players.All.Where(x => x.Hero != null && x.Hero.IsValid).Select(x => x.Hero).ToList();
            foreach (
                var hero in ObjectManager.GetEntities<Hero>().Where(hero => tempList.All(x => x.Handle != hero.Handle)))
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
            DelayAction.Add(
                200, 
                () =>
                    {
                        var hero = args.Entity as Hero;
                        if (hero == null)
                        {
                            return;
                        }

                        tempList.Add(hero);
                        if (!All.Contains(hero))
                        {
                            All.Add(hero);
                        }

                        if (!Radiant.Contains(hero) && hero.Team == Team.Radiant)
                        {
                            Radiant.Add(hero);
                            return;
                        }

                        if (!Dire.Contains(hero) && hero.Team == Team.Dire)
                        {
                            Dire.Add(hero);
                        }
                    });
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
            if (hero == null)
            {
                return;
            }

            tempList.Remove(hero);
            if (All.Contains(hero))
            {
                All.Remove(hero);
            }

            if (Radiant.Contains(hero) && hero.Team == Team.Radiant)
            {
                Radiant.Remove(hero);
                return;
            }

            if (Dire.Contains(hero) && hero.Team == Team.Dire)
            {
                Dire.Remove(hero);
            }
        }

        #endregion
    }
}