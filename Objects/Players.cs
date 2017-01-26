// <copyright file="Players.cs" company="EnsageSharp">
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
    ///     The players.
    /// </summary>
    public class Players
    {
        #region Static Fields

        /// <summary>
        ///     The all.
        /// </summary>
        public static List<Player> All;

        /// <summary>
        ///     The dire.
        /// </summary>
        public static List<Player> Dire;

        /// <summary>
        ///     The radiant.
        /// </summary>
        public static List<Player> Radiant;

        /// <summary>
        ///     The loaded.
        /// </summary>
        private static bool loaded;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="Players" /> class.
        /// </summary>
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
                    Events.OnUpdate -= Update;
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
            if (!Game.IsInGame)
            {
                return;
            }

            if (!Utils.SleepCheck("Common.Players.Update"))
            {
                return;
            }

            if (All.Count(x => x.IsValid) < 10)
            {
                All = ObjectManager.GetEntitiesParallel<Player>().ToList();
                Radiant = All.Where(x => x.Team == Team.Radiant).ToList();
                Dire = All.Where(x => x.Team == Team.Dire).ToList();
            }

            Utils.Sleep(1000, "Common.Players.Update");
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The load.
        /// </summary>
        private static void Load()
        {
            All = ObjectManager.GetEntities<Player>().ToList();
            Dire = All.Where(x => x.Team == Team.Radiant).ToList();
            Radiant = All.Where(x => x.Team == Team.Dire).ToList();
            Events.OnUpdate += Update;
            loaded = true;
            Update(null);
        }

        #endregion
    }
}