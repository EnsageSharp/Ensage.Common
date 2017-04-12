// <copyright file="Towers.cs" company="EnsageSharp">
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
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     The towers.
    /// </summary>
    public class Towers
    {
        #region Static Fields

        /// <summary>
        ///     The all.
        /// </summary>
        private static List<Building> all;

        /// <summary>
        ///     The dire.
        /// </summary>
        private static List<Building> dire;

        /// <summary>
        ///     The loaded.
        /// </summary>
        private static bool loaded;

        /// <summary>
        ///     The radiant.
        /// </summary>
        private static List<Building> radiant;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="Towers" /> class.
        /// </summary>
        static Towers()
        {
            all =
                ObjectManager.GetEntities<Building>()
                    .Where(x => x.IsAlive && x.ClassId == ClassId.CDOTA_BaseNPC_Tower)
                    .ToList();
            dire = all.Where(x => x.Team == Team.Dire).ToList();
            radiant = all.Where(x => x.Team == Team.Radiant).ToList();
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
                    ObjectManager.OnRemoveEntity -= ObjectMgr_OnRemoveEntity;
                    loaded = false;
                };
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Returns all towers in game.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        public static IEnumerable<Building> GetAll()
        {
            return all;
        }

        /// <summary>
        ///     Returns all towers of specified team
        /// </summary>
        /// <param name="team">
        ///     The team (Dire/Radiant).
        /// </param>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        public static IEnumerable<Building> GetByTeam(Team team)
        {
            return team == Team.Dire ? dire : radiant;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The load.
        /// </summary>
        private static void Load()
        {
            all =
                ObjectManager.GetEntities<Building>()
                    .Where(x => x.IsAlive && x.ClassId == ClassId.CDOTA_BaseNPC_Tower)
                    .ToList();
            dire = all.Where(x => x.Team == Team.Dire).ToList();
            radiant = all.Where(x => x.Team == Team.Radiant).ToList();
            ObjectManager.OnRemoveEntity += ObjectMgr_OnRemoveEntity;
            loaded = true;
        }

        /// <summary>
        ///     The object manager on remove entity.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private static void ObjectMgr_OnRemoveEntity(EntityEventArgs args)
        {
            var tower = args.Entity as Building;
            if (tower == null)
            {
                return;
            }

            all.Remove(tower);
            if (tower.Team == Team.Dire)
            {
                dire.Remove(tower);
            }
            else
            {
                radiant.Remove(tower);
            }
        }

        #endregion
    }
}