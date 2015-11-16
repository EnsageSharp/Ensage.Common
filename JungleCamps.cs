// <copyright file="JungleCamps.cs" company="EnsageSharp">
//    Copyright (c) 2015 EnsageSharp.
// 
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
// 
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
// 
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>

namespace Ensage.Common
{
    using System.Collections.Generic;

    using Ensage.Common.Extensions;

    using SharpDX;

    /// <summary>
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
        /// </summary>
        public JungleCamp()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="campPosition"></param>
        /// <param name="stackPosition"></param>
        /// <param name="waitPosition"></param>
        /// <param name="team"></param>
        /// <param name="id"></param>
        /// <param name="stackTime"></param>
        /// <param name="ancients"></param>
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
    /// </summary>
    public class JungleCamps
    {
        #region Static Fields

        private static readonly List<JungleCamp> Camps = new List<JungleCamp>();

        #endregion

        #region Constructors and Destructors

        #region Constructors and Destructors

        static JungleCamps()
        {
            Camps.Add(
                new JungleCamp
                    {
                        CampPosition = new Vector3(-1131, -4044, 127), StackPosition = new Vector3(-2750, -3517, 128),
                        WaitPosition = new Vector3(-1401, -3791, 128), ID = 1, StackTime = 54.5, Team = Team.Radiant,
                        Ancients = false
                    });

            Camps.Add(
                new JungleCamp
                    {
                        CampPosition = new Vector3(-366, -2945, 127), StackPosition = new Vector3(-534, -1795, 128),
                        WaitPosition = new Vector3(-408, -2731, 127), ID = 2, StackTime = 55, Team = Team.Radiant,
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
                        CampPosition = new Vector3(3126, -3439, 256), StackPosition = new Vector3(5284, -3922, 256),
                        WaitPosition = new Vector3(3231, -3807, 256), ID = 4, StackTime = 53.1, Team = Team.Radiant,
                        Ancients = false
                    });

            Camps.Add(
                new JungleCamp
                    {
                        CampPosition = new Vector3(3031, -4480, 256), StackPosition = new Vector3(3774, -6700, 256),
                        WaitPosition = new Vector3(3030, -4975, 256), ID = 5, StackTime = 53, Team = Team.Radiant,
                        Ancients = false
                    });

            Camps.Add(
                new JungleCamp
                    {
                        CampPosition = new Vector3(-2991, 191, 256), StackPosition = new Vector3(-3351, -1798, 205),
                        WaitPosition = new Vector3(-2684, -23, 256), ID = 6, StackTime = 54, Team = Team.Radiant,
                        Ancients = true
                    });

            Camps.Add(
                new JungleCamp
                    {
                        CampPosition = new Vector3(1167, 3295, 256), StackPosition = new Vector3(570, 4515, 256),
                        WaitPosition = new Vector3(1011, 3656, 256), ID = 7, StackTime = 54, Team = Team.Dire,
                        Ancients = false
                    });

            Camps.Add(
                new JungleCamp
                    {
                        CampPosition = new Vector3(-244, 3629, 256), StackPosition = new Vector3(236, 5000, 256),
                        WaitPosition = new Vector3(-523, 4041, 256), ID = 8, StackTime = 54.5, Team = Team.Dire,
                        Ancients = false
                    });

            Camps.Add(
                new JungleCamp
                    {
                        CampPosition = new Vector3(-1588, 2697, 127), StackPosition = new Vector3(-1302, 3689, 136),
                        WaitPosition = new Vector3(-1491, 2986, 127), ID = 9, StackTime = 53, Team = Team.Dire,
                        Ancients = false
                    });

            Camps.Add(
                new JungleCamp
                    {
                        CampPosition = new Vector3(-3157, 4475, 256), StackPosition = new Vector3(-3296, 5508, 256),
                        WaitPosition = new Vector3(-3086, 4924, 256), ID = 10, StackTime = 54, Team = Team.Dire,
                        Ancients = false
                    });

            Camps.Add(
                new JungleCamp
                    {
                        CampPosition = new Vector3(-4382, 3612, 256), StackPosition = new Vector3(-3026, 3819, 132),
                        WaitPosition = new Vector3(-4200, 3850, 256), ID = 11, StackTime = 53, Team = Team.Dire,
                        Ancients = false
                    });

            Camps.Add(
                new JungleCamp
                    {
                        CampPosition = new Vector3(4026, -709, 128), StackPosition = new Vector3(2636, -1017, 127),
                        WaitPosition = new Vector3(3583, -736, 127), ID = 12, StackTime = 54, Team = Team.Dire,
                        Ancients = true
                    });

            #endregion
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static JungleCamp FindClosestCamp(Vector3 pos)
        {
            JungleCamp bestResult = null;
            foreach (var jungleCamp in Camps)
            {
                if (bestResult == null
                    || bestResult.CampPosition.Distance2D(pos) > jungleCamp.CampPosition.Distance2D(pos))
                {
                    bestResult = jungleCamp;
                }
            }
            return bestResult;
        }

        #endregion
    }
}