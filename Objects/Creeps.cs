// <copyright file="Creeps.cs" company="EnsageSharp">
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

    /// <summary>
    ///     The creeps.
    /// </summary>
    public class Creeps
    {
        #region Static Fields

        /// <summary>
        ///     The all.
        /// </summary>
        public static IEnumerable<Creep> All;

        /// <summary>
        ///     The loaded.
        /// </summary>
        private static bool loaded;

        /// <summary>
        ///     The temp list.
        /// </summary>
        private static List<Creep> tempList;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="Creeps" /> class.
        /// </summary>
        static Creeps()
        {
            All = new List<Creep>();
            Events.OnLoad += (sender, args) =>
                {
                    if (loaded)
                    {
                        return;
                    }

                    Load();
                };
            if (!loaded && ObjectManager.LocalHero != null && Game.IsInGame)
            {
                Load();
            }

            Events.OnClose += (sender, args) =>
                {
                    Events.OnUpdate -= Update;
                    ObjectManager.OnAddEntity -= ObjectMgr_OnAddEntity;
                    ObjectManager.OnRemoveEntity -= ObjectMgr_OnRemoveEntity;
                    loaded = false;
                };
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The update.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
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

            if (Utils.SleepCheck("Common.Creeps.SpecialUpdate"))
            {
                tempList = ObjectManager.GetEntitiesParallel<Creep>().ToList();
                Utils.Sleep(1700, "Common.Creeps.SpecialUpdate");
                return;
            }

            UpdateCreeps();
            Utils.Sleep(500, "Common.Creeps.Update");
        }

        /// <summary>
        ///     The update creeps.
        /// </summary>
        public static void UpdateCreeps()
        {
            All = tempList.Where(creep => creep.IsValid);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The load.
        /// </summary>
        private static void Load()
        {
            All = new List<Creep>();
            tempList = ObjectManager.GetEntities<Creep>().ToList();
            Events.OnUpdate += Update;
            ObjectManager.OnAddEntity += ObjectMgr_OnAddEntity;
            ObjectManager.OnRemoveEntity += ObjectMgr_OnRemoveEntity;
            loaded = true;
        }

        /// <summary>
        ///     The object manager on add entity.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private static void ObjectMgr_OnAddEntity(EntityEventArgs args)
        {
            DelayAction.Add(
                50,
                () =>
                    {
                        var all = new List<Creep>(All);
                        var creep = args.Entity as Creep;
                        if (creep != null)
                        {
                            tempList.Add(creep);
                            all.Add(creep);
                        }

                        All = all;
                    });
        }

        /// <summary>
        ///     The object manager on remove entity.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private static void ObjectMgr_OnRemoveEntity(EntityEventArgs args)
        {
            var creep = args.Entity as Creep;
            var all = new List<Creep>(All);
            if (creep != null)
            {
                tempList.Remove(creep);
                all.Remove(creep);
            }

            All = all;
        }

        #endregion
    }
}