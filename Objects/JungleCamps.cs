// <copyright file="JungleCamps.cs" company="EnsageSharp">
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
                                                                         CampPosition = new Vector3(-1655, -4329, 256), 
                                                                         StackPosition = new Vector3(-1833, -3062, 256), 
                                                                         WaitPosition = new Vector3(-1890, -3896, 256), 
                                                                         Id = 1, StackTime = 54.5, Team = Team.Radiant, 
                                                                         Ancients = false, Name = "Hard Camp"
                                                                     }, 
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-260, -3234, 256), 
                                                                         StackPosition = new Vector3(-554, -1925, 256), 
                                                                         WaitPosition = new Vector3(-337, -2652, 256), 
                                                                         Id = 2, StackTime = 55, Team = Team.Radiant, 
                                                                         Ancients = false, Name = "Medium Camp"
                                                                     }, 
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(1606, -3433, 256), 
                                                                         StackPosition = new Vector3(1598, -5117, 256), 
                                                                         WaitPosition = new Vector3(1541, -4265, 256), 
                                                                         Id = 3, StackTime = 54.5, Team = Team.Radiant, 
                                                                         Ancients = false, Name = "Medium Camp"
                                                                     }, 
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(4495, -3488, 384), 
                                                                         StackPosition = new Vector3(3002, -3936, 384), 
                                                                         WaitPosition = new Vector3(4356, -4089, 384), 
                                                                         Id = 4, StackTime = 53.1, Team = Team.Radiant, 
                                                                         Ancients = false, Name = "Hard Camp"
                                                                     }, 
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(3031, -4480, 256), 
                                                                         StackPosition = new Vector3(1555, -5337, 384), 
                                                                         WaitPosition = new Vector3(3099, -5325, 384), 
                                                                         Id = 5, StackTime = 53, Team = Team.Radiant, 
                                                                         Ancients = false, Name = "Easy Camp"
                                                                     }, 
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-3097, 4, 384), 
                                                                         StackPosition = new Vector3(-3472, -1566, 384), 
                                                                         WaitPosition = new Vector3(-2471, -227, 384), 
                                                                         Id = 6, StackTime = 54, Team = Team.Radiant, 
                                                                         Ancients = true, Name = "Ancients Camp"
                                                                     }, 
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-3593, 825, 384), 
                                                                         StackPosition = new Vector3(-3893, -737, 384), 
                                                                         WaitPosition = new Vector3(-4129, 600, 384), 
                                                                         Id = 7, StackTime = 53, Team = Team.Radiant, 
                                                                         Ancients = true, Name = "Secret Hard Camp"
                                                                     }, 
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(1266, 3271, 384), 
                                                                         StackPosition = new Vector3(449, 4752, 384), 
                                                                         WaitPosition = new Vector3(979, 3671, 384), 
                                                                         Id = 8, StackTime = 54, Team = Team.Dire, 
                                                                         Ancients = false, Name = "Hard Camp"
                                                                     }, 
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-291, 3644, 384), 
                                                                         StackPosition = new Vector3(236, 5000, 256), 
                                                                         WaitPosition = new Vector3(-566, 4151, 384), 
                                                                         Id = 9, StackTime = 54.5, Team = Team.Dire, 
                                                                         Ancients = false, Name = "Medium Camp"
                                                                     }, 
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-1640, 2562, 256), 
                                                                         StackPosition = new Vector3(-1180, 4090, 384), 
                                                                         WaitPosition = new Vector3(-1380, 2979, 256), 
                                                                         Id = 10, StackTime = 53, Team = Team.Dire, 
                                                                         Ancients = false, Name = "Medium Camp"
                                                                     }, 
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-3084, 4492, 384), 
                                                                         StackPosition = new Vector3(-3533, 6295, 384), 
                                                                         WaitPosition = new Vector3(-3058, 4997, 384), 
                                                                         Id = 11, StackTime = 54, Team = Team.Dire, 
                                                                         Ancients = false, Name = "Easy Camp"
                                                                     }, 
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(-4628, 3483, 384), 
                                                                         StackPosition = new Vector3(-2801, 3684, 245), 
                                                                         WaitPosition = new Vector3(-4200, 3850, 256), 
                                                                         Id = 12, StackTime = 53, Team = Team.Dire, 
                                                                         Ancients = false, Name = "Top Hard Camp"
                                                                     }, 
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(4150, -678, 256), 
                                                                         StackPosition = new Vector3(2493, -1059, 256), 
                                                                         WaitPosition = new Vector3(3583, -736, 127), 
                                                                         Id = 13, StackTime = 54, Team = Team.Dire, 
                                                                         Ancients = true, Name = "Ancients Camp"
                                                                     }, 
                                                                 new JungleCamp
                                                                     {
                                                                         CampPosition = new Vector3(4280, 588, 384), 
                                                                         StackPosition = new Vector3(3537, 1713, 256), 
                                                                         WaitPosition = new Vector3(3710, 548, 384), 
                                                                         Id = 14, StackTime = 54, Team = Team.Dire, 
                                                                         Ancients = false, Name = "Secret Hard Camp"
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