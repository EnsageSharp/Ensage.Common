// <copyright file="JungleCamps.cs" company="EnsageSharp">
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

    using Ensage.Common.Extensions;

    using SharpDX;

    /// <summary>
    ///     The jungle camps.
    /// </summary>
    public class JungleCamps
    {
        #region Static Fields

        /// <summary>
        ///     The camps.
        /// </summary>
        private static readonly List<JungleCamp> Camps = new List<JungleCamp>
                                                             {
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-417, -3318, 256),
                                                                         StackPosition = new Vector3(-268, -2467, 256),
                                                                         WaitPosition = new Vector3(-776, -3239, 256),
                                                                         Id = 1, StackTime = 54, Team = Team.Radiant,
                                                                         Ancients = false, Name = "Hard Camp Middle"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-1824, -4324, 128),
                                                                         StackPosition = new Vector3(-1891, -3234, 256),
                                                                         WaitPosition = new Vector3(-1797, -3943, 204),
                                                                         Id = 2, StackTime = 56, Team = Team.Radiant,
                                                                         Ancients = false, Name = "Medium Camp Middle"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(484, -4621, 384),
                                                                         StackPosition = new Vector3(720, -3551, 384),
                                                                         WaitPosition = new Vector3(758, -1348, 384),
                                                                         Id = 3, StackTime = 55, Team = Team.Radiant,
                                                                         Ancients = false, Name = "Medium Camp Bot"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(4766, -4445, 256),
                                                                         StackPosition = new Vector3(5418, -3704, 384),
                                                                         WaitPosition = new Vector3(4594, -4107, 256),
                                                                         Id = 4, StackTime = 54, Team = Team.Radiant,
                                                                         Ancients = false, Name = "Hard Camp Bot"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(3024, -4550, 256),
                                                                         StackPosition = new Vector3(3455, -5316, 384),
                                                                         WaitPosition = new Vector3(3350, -4561, 256),
                                                                         Id = 5, StackTime = 54, Team = Team.Radiant,
                                                                         Ancients = false, Name = "Easy Camp"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-151, -1956, 384),
                                                                         StackPosition = new Vector3(974, -2489, 384),
                                                                         WaitPosition = new Vector3(408, -2110, 384),
                                                                         Id = 6, StackTime = 54, Team = Team.Radiant,
                                                                         Ancients = true, Name = "Ancients Camp"
                                                                     },
																	 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-2970, -84, 384),
                                                                         StackPosition = new Vector3(-1686, 266, 256),
                                                                         WaitPosition = new Vector3(-2658, -92, 384),
                                                                         Id = 7, StackTime = 53, Team = Team.Radiant,
                                                                         Ancients = true, Name = "Secret Ancients Camp"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-4765, -357, 256),
                                                                         StackPosition = new Vector3(-3836, -759, 384),
                                                                         WaitPosition = new Vector3(-4646, -171, 256),
                                                                         Id = 8, StackTime = 56, Team = Team.Radiant,
                                                                         Ancients = false, Name = "Secret Hard Camp"
                                                                     },
																	 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-3804, 798, 256),
                                                                         StackPosition = new Vector3(-4765, 662, 256),
                                                                         WaitPosition = new Vector3(-4029, 640, 256),
                                                                         Id = 9, StackTime = 55, Team = Team.Radiant,
                                                                         Ancients = false, Name = "Secret Medium Camp"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-236, 3439, 256),
                                                                         StackPosition = new Vector3(-748, 4243, 256),
                                                                         WaitPosition = new Vector3(-464, 3560, 256),
                                                                         Id = 10, StackTime = 54, Team = Team.Dire,
                                                                         Ancients = false, Name = "Hard Camp"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(1194, 3379, 384),
                                                                         StackPosition = new Vector3(1852, 4376, 256),
                                                                         WaitPosition = new Vector3(1173, 3593, 384),
                                                                         Id = 11, StackTime = 53, Team = Team.Dire,
                                                                         Ancients = false, Name = "Medium Camp"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-1840, 4081, 256),
                                                                         StackPosition = new Vector3(-1436, 4755, 384),
                                                                         WaitPosition = new Vector3(-1607, 4009, 256),
                                                                         Id = 12, StackTime = 55, Team = Team.Dire,
                                                                         Ancients = false, Name = "Medium Camp Top"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-2917,4897, 384),
                                                                         StackPosition = new Vector3(-2290, 5745, 384),
                                                                         WaitPosition = new Vector3(-3058, 5157, 384),
                                                                         Id = 13, StackTime = 55, Team = Team.Dire,
                                                                         Ancients = false, Name = "Easy Camp"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-4330, 3695, 256),
                                                                         StackPosition = new Vector3(-3378, 3718, 256),
                                                                         WaitPosition = new Vector3(-4277, 33881850, 256),
                                                                         Id = 14, StackTime = 55, Team = Team.Dire,
                                                                         Ancients = false, Name = "Top Hard Camp"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-855, 2262, 384),
                                                                         StackPosition = new Vector3(392, 2343, 384),
                                                                         WaitPosition = new Vector3(-538, 2437, 384),
                                                                         Id = 15, StackTime = 53, Team = Team.Dire,
                                                                         Ancients = true, Name = "Ancients Camp"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(4180, 749, 384),
                                                                         StackPosition = new Vector3(3473, 729, 384),
                                                                         WaitPosition = new Vector3(3991, 732, 384),
                                                                         Id = 16, StackTime = 55, Team = Team.Dire,
                                                                         Ancients = false, Name = "Secret Hard Camp"
                                                                     }
																 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(2707, 109, 384),
                                                                         StackPosition = new Vector3(2881, -706, 256),
                                                                         WaitPosition = new Vector3(2997, 177, 384),
                                                                         Id = 17, StackTime = 54, Team = Team.Dire,
                                                                         Ancients = false, Name = "Secret Medium Camp"
                                                                     }
																	 
																 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(3945, -492, 256),
                                                                         StackPosition = new Vector3(3905, -1428, 384),
                                                                         WaitPosition = new Vector3(3753, -840, 256),
                                                                         Id = 18, StackTime = 53, Team = Team.Dire,
                                                                         Ancients = true, Name = "Secret Ancient Camp"
                                                                     }	 
                                                             };

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
