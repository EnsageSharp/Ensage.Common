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
                                                                         CampPosition = new Vector3(-563, -3314, 256),
                                                                         StackPosition = new Vector3(-694, -4873, 384),
                                                                         WaitPosition = new Vector3(-859, -3217, 256),
                                                                         Id = 1, StackTime = 54, Team = Team.Radiant,
                                                                         Ancients = false, Name = "Hard Camp Middle"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-1812, -4302, 128),
                                                                         StackPosition = new Vector3(-1611, -2589, 256),
                                                                         WaitPosition = new Vector3(-1829, -3834, 256),
                                                                         Id = 2, StackTime = 56, Team = Team.Radiant,
                                                                         Ancients = false, Name = "Medium Camp Middle"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(591, -4537, 384),
                                                                         StackPosition = new Vector3(751, -3479, 384),
                                                                         WaitPosition = new Vector3(719, -4391, 384),
                                                                         Id = 3, StackTime = 55, Team = Team.Radiant,
                                                                         Ancients = false, Name = "Medium Camp Bot"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(4679, -4293, 256),
                                                                         StackPosition = new Vector3(3064, -3733, 256),
                                                                         WaitPosition = new Vector3(4514, -4114, 256),
                                                                         Id = 4, StackTime = 54, Team = Team.Radiant,
                                                                         Ancients = false, Name = "Hard Camp Bot"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(3030, -4555, 256),
                                                                         StackPosition = new Vector3(4864, -5157, 384),
                                                                         WaitPosition = new Vector3(3432, -4656, 256),
                                                                         Id = 5, StackTime = 54, Team = Team.Radiant,
                                                                         Ancients = false, Name = "Easy Camp"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(222, -2072, 384),
                                                                         StackPosition = new Vector3(945, -3544, 384),
                                                                         WaitPosition = new Vector3(528, -2283, 384),
                                                                         Id = 6, StackTime = 54, Team = Team.Radiant,
                                                                         Ancients = true, Name = "Ancients Camp"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-2858, -126, 384),
                                                                         StackPosition = new Vector3(-3472, -1566, 384),
                                                                         WaitPosition = new Vector3(-2301, -208, 384),
                                                                         Id = 7, StackTime = 53, Team = Team.Radiant,
                                                                         Ancients = true, Name = "Secret Ancients Camp"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-4710, -287, 256),
                                                                         StackPosition = new Vector3(-4658, 1403, 384),
                                                                         WaitPosition = new Vector3(-4573, -111, 256),
                                                                         Id = 8, StackTime = 55, Team = Team.Radiant,
                                                                         Ancients = false, Name = "Secret Hard Camp"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-3824, 766, 256),
                                                                         StackPosition = new Vector3(-4896, 736, 256),
                                                                         WaitPosition = new Vector3(-4049, 620, 256),
                                                                         Id = 9, StackTime = 54, Team = Team.Radiant,
                                                                         Ancients = false, Name = "Secret Medium Camp"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-270, 3424, 256),
                                                                         StackPosition = new Vector3(-395, 4616, 384),
                                                                         WaitPosition = new Vector3(-551, 3556, 256),
                                                                         Id = 10, StackTime = 54, Team = Team.Dire,
                                                                         Ancients = false, Name = "Hard Camp"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(1174, 3453, 384),
                                                                         StackPosition = new Vector3(449, 4752, 384),
                                                                         WaitPosition = new Vector3(960, 3688, 384),
                                                                         Id = 11, StackTime = 53, Team = Team.Dire,
                                                                         Ancients = false, Name = "Medium Camp"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-1735, 4024, 256),
                                                                         StackPosition = new Vector3(-961, 5011, 384),
                                                                         WaitPosition = new Vector3(-1526, 3855, 256),
                                                                         Id = 12, StackTime = 55, Team = Team.Dire,
                                                                         Ancients = false, Name = "Medium Camp Top"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-2919, 4955, 384),
                                                                         StackPosition = new Vector3(-1923, 6042, 384),
                                                                         WaitPosition = new Vector3(-3023, 5180, 384),
                                                                         Id = 13, StackTime = 53, Team = Team.Dire,
                                                                         Ancients = false, Name = "Easy Camp"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-4331, 3706, 256),
                                                                         StackPosition = new Vector3(-2894, 3515, 256),
                                                                         WaitPosition = new Vector3(-4174, 3893, 256),
                                                                         Id = 14, StackTime = 55, Team = Team.Dire,
                                                                         Ancients = false, Name = "Top Hard Camp"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-716, 2378, 384),
                                                                         StackPosition = new Vector3(982, 2251, 384),
                                                                         WaitPosition = new Vector3(-419, 2473, 384),
                                                                         Id = 15, StackTime = 53, Team = Team.Dire,
                                                                         Ancients = true, Name = "Ancients Camp"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(4273, 791, 384),
                                                                         StackPosition = new Vector3(3454, 1710, 256),
                                                                         WaitPosition = new Vector3(4001, 745, 384),
                                                                         Id = 16, StackTime = 55, Team = Team.Dire,
                                                                         Ancients = false, Name = "Secret Hard Camp"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(2685, 110, 384),
                                                                         StackPosition = new Vector3(4830, -75, 384),
                                                                         WaitPosition = new Vector3(3039, 106, 384),
                                                                         Id = 17, StackTime = 54, Team = Team.Dire,
                                                                         Ancients = false, Name = "Secret Medium Camp"
                                                                     },
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(3664, -709, 256),
                                                                         StackPosition = new Vector3(2221, -1013, 256),
                                                                         WaitPosition = new Vector3(3446, -697, 256),
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