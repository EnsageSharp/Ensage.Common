// <copyright file="AttackAnimation.cs" company="EnsageSharp">
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
    /// <summary>
    ///     The attack animation struct.
    /// </summary>
    public class AttackAnimation
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AttackAnimation" /> class.
        /// </summary>
        /// <param name="unitName">
        ///     The unit name.
        /// </param>
        /// <param name="unitClassId">
        ///     The unit class id.
        /// </param>
        /// <param name="moveTime">
        ///     The attack animation move time.
        /// </param>
        /// <param name="endTime">
        ///     The attack animation end time.
        /// </param>
        /// <param name="canMove">
        ///     Indicates whether the unit can move.
        /// </param>
        public AttackAnimation(string unitName, ClassID unitClassId, double moveTime, double endTime, bool canMove)
        {
            this.UnitName = unitName;
            this.UnitClassId = unitClassId;
            this.MoveTime = moveTime;
            this.EndTime = endTime;
            this.CanMove = canMove;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Indicates whether the unit can move.
        /// </summary>
        public bool CanMove { get; set; }

        /// <summary>
        ///     The end time of the attack animation.
        /// </summary>
        public double EndTime { get; set; }

        /// <summary>
        ///     The move time of the attack animation.
        /// </summary>
        public double MoveTime { get; set; }

        /// <summary>
        ///     The unit class id.
        /// </summary>
        public ClassID UnitClassId { get; set; }

        /// <summary>
        ///     The unit name.
        /// </summary>
        public string UnitName { get; set; }

        #endregion
    }
}