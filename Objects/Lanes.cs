// <copyright file="Lanes.cs" company="EnsageSharp">
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

    using SharpDX;

    /// <summary>
    ///     The lanes.
    /// </summary>
    public class Lanes
    {
        #region Static Fields

        /// <summary>
        ///     The list.
        /// </summary>
        private static readonly ICollection<Lane> List = new List<Lane>
                                                             {
                                                                 new Lane
                                                                     {
                                                                         Path =
                                                                             new List<Vector3>
                                                                                 {
                                                                                     new Vector3(-3466, -6043, 0),
                                                                                     new Vector3(-673, -6349, 0),
                                                                                     new Vector3(4478, -6119, 0),
                                                                                     new Vector3(5490, -5672, 0),
                                                                                     new Vector3(6172, -3896, 0)
                                                                                 },
                                                                         Name = "Radiant Bot Lane", Team = Team.Radiant,
                                                                         Position = LanePosition.Bottom
                                                                     }

                                                                 // add other lanes
                                                             };

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the get lanes.
        /// </summary>
        public ICollection<Lane> GetLanes
        {
            get
            {
                return List;
            }
        }

        #endregion
    }
}