// <copyright file="JungleCamps.cs" company="EnsageSharp">
//    Copyright (c) 2015 EnsageSharp.
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

    using Ensage.Common.Extensions;

    using SharpDX;

    /// <summary>
    ///     The jungle camp.
    /// </summary>
    public class JungleCamp
    {
        #region Fields

        /// <summary>
        ///     Camp of Ancients !
        /// </summary>
        public bool Ancients;

        /// <summary>
        /// </summary>
        public Vector3 CampPosition;

        /// <summary>
        /// </summary>
        public uint ID;

        /// <summary>
        ///     Vector of stacking path
        /// </summary>
        public Vector3 StackPosition;

        /// <summary>
        ///     Default time when creeps should be pulled out of the camp
        /// </summary>
        public double StackTime;

        /// <summary>
        ///     Radiant / Dire jungle
        /// </summary>
        public Team Team;

        /// <summary>
        ///     Position which reveals creeps in camp but doesnt block it from spawning.
        /// </summary>
        public Vector3 WaitPosition;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="JungleCamp" /> class.
        /// </summary>
        public JungleCamp()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JungleCamp" /> class.
        /// </summary>
        /// <param name="campPosition">
        ///     The camp position.
        /// </param>
        /// <param name="stackPosition">
        ///     The stack position.
        /// </param>
        /// <param name="waitPosition">
        ///     The wait position.
        /// </param>
        /// <param name="team">
        ///     The team.
        /// </param>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <param name="stackTime">
        ///     The stack time.
        /// </param>
        /// <param name="ancients">
        ///     The ancients.
        /// </param>
        public JungleCamp(
            Vector3 campPosition, 
            Vector3 stackPosition, 
            Vector3 waitPosition, 
            Team team, 
            uint id, 
            double stackTime, 
            bool ancients)
        {
            this.CampPosition = campPosition;
            this.StackPosition = stackPosition;
            this.WaitPosition = waitPosition;
            this.Team = team;
            this.ID = id;
            this.StackTime = stackTime;
            this.Ancients = ancients;
        }

        #endregion
    }

    /// <summary>
    ///     The jungle camps.
    /// </summary>
    public class JungleCamps
    {
        #region Static Fields

        /// <summary>
        ///     The camps.
        /// </summary>
        private static readonly List<JungleCamp> Camps = new List<JungleCamp>();

        #endregion

        #region Constructors and Destructors

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="JungleCamps" /> class.
        /// </summary>
        static JungleCamps()
        {
            Camps.Add(
                new JungleCamp
                    {
                        CampPosition = new Vector3(-1655, -4329, 256), StackPosition = new Vector3(-1833, -3062, 256), 
                        WaitPosition = new Vector3(-1890, -3896, 256), ID = 1, StackTime = 54.5, Team = Team.Radiant, 
                        Ancients = false
                    });

            Camps.Add(
                new JungleCamp
                    {
                        CampPosition = new Vector3(-260, -3234, 256), StackPosition = new Vector3(-554, -1925, 256), 
                        WaitPosition = new Vector3(-337, -2652, 256), ID = 2, StackTime = 55, Team = Team.Radiant, 
                        Ancients = false
                    });

            Camps.Add(
                new JungleCamp
                    {
                        CampPosition = new Vector3(1606, -3433, 256), StackPosition = new Vector3(1598, -5117, 256), 
                        WaitPosition = new Vector3(1541, -4265, 256), ID = 3, StackTime = 54.5, Team = Team.Radiant, 
                        Ancients = false
                    });

            Camps.Add(
                new JungleCamp
                    {
                        CampPosition = new Vector3(4495, -3488, 384), StackPosition = new Vector3(3002, -3936, 384), 
                        WaitPosition = new Vector3(4356, -4089, 384), ID = 4, StackTime = 53.1, Team = Team.Radiant, 
                        Ancients = false
                    });

            Camps.Add(
                new JungleCamp
                    {
                        CampPosition = new Vector3(3031, -4480, 256), StackPosition = new Vector3(1555, -5337, 384), 
                        WaitPosition = new Vector3(3099, -5325, 384), ID = 5, StackTime = 53, Team = Team.Radiant, 
                        Ancients = false
                    });

            Camps.Add(
                new JungleCamp
                    {
                        CampPosition = new Vector3(-3097, 4, 384), StackPosition = new Vector3(-3472, -1566, 384), 
                        WaitPosition = new Vector3(-2471, -227, 384), ID = 6, StackTime = 54, Team = Team.Radiant, 
                        Ancients = true
                    });

            Camps.Add(
                new JungleCamp
                    {
                        CampPosition = new Vector3(-3593, 825, 384), StackPosition = new Vector3(-3893, -737, 384),
                        WaitPosition = new Vector3(-4129, 600, 384), ID = 7, StackTime = 53, Team = Team.Radiant,
                        Ancients = true
                    });

            Camps.Add(
                new JungleCamp
                    {
                        CampPosition = new Vector3(1167, 3295, 256), StackPosition = new Vector3(570, 4515, 256), 
                        WaitPosition = new Vector3(1011, 3656, 256), ID = 8, StackTime = 54, Team = Team.Dire, 
                        Ancients = false
                    });

            Camps.Add(
                new JungleCamp
                    {
                        CampPosition = new Vector3(-244, 3629, 256), StackPosition = new Vector3(236, 5000, 256), 
                        WaitPosition = new Vector3(-523, 4041, 256), ID = 9, StackTime = 54.5, Team = Team.Dire, 
                        Ancients = false
                    });

            Camps.Add(
                new JungleCamp
                    {
                        CampPosition = new Vector3(-1588, 2697, 127), StackPosition = new Vector3(-1302, 3689, 136), 
                        WaitPosition = new Vector3(-1491, 2986, 127), ID = 10, StackTime = 53, Team = Team.Dire, 
                        Ancients = false
                    });

            Camps.Add(
                new JungleCamp
                    {
                        CampPosition = new Vector3(-3157, 4475, 256), StackPosition = new Vector3(-3296, 5508, 256), 
                        WaitPosition = new Vector3(-3086, 4924, 256), ID = 11, StackTime = 54, Team = Team.Dire, 
                        Ancients = false
                    });

            Camps.Add(
                new JungleCamp
                    {
                        CampPosition = new Vector3(-4382, 3612, 256), StackPosition = new Vector3(-3026, 3819, 132), 
                        WaitPosition = new Vector3(-4200, 3850, 256), ID = 12, StackTime = 53, Team = Team.Dire, 
                        Ancients = false
                    });

            Camps.Add(
                new JungleCamp
                    {
                        CampPosition = new Vector3(4026, -709, 128), StackPosition = new Vector3(2636, -1017, 127), 
                        WaitPosition = new Vector3(3583, -736, 127), ID = 13, StackTime = 54, Team = Team.Dire, 
                        Ancients = true
                    });

            #endregion
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the get camps.
        /// </summary>
        public static List<JungleCamp> GetCamps
        {
            get
            {
                return Camps;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The find closest camp.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        /// <returns>
        ///     The <see cref="JungleCamp" />.
        /// </returns>
        public static JungleCamp FindClosestCamp(Vector3 position)
        {
            JungleCamp bestResult = null;
            foreach (var jungleCamp in Camps)
            {
                if (bestResult == null
                    || bestResult.CampPosition.Distance2D(position) > jungleCamp.CampPosition.Distance2D(position))
                {
                    bestResult = jungleCamp;
                }
            }

            return bestResult;
        }

        #endregion
    }
}