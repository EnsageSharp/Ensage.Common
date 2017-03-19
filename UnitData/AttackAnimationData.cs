// <copyright file="AttackAnimationData.cs" company="EnsageSharp">
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
namespace Ensage.Common.UnitData
{
    /// <summary>
    ///     The attack animation data struct.
    /// </summary>
    public class AttackAnimationData
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AttackAnimationData" /> class.
        /// </summary>
        /// <param name="unitName">
        ///     The unit name.
        /// </param>
        /// <param name="unitClassId">
        ///     The unit class id.
        /// </param>
        /// <param name="attackRate">
        ///     The attack animation rate.
        /// </param>
        /// <param name="attackPoint">
        ///     The attack animation point.
        /// </param>
        /// <param name="attackBackswing">
        ///     The attack animation backswing.
        /// </param>
        /// <param name="projectileSpeed">
        ///     The attack animation projectile speed.
        /// </param>
        /// <param name="turnRate">
        ///     The attack animation turn rate.
        /// </param>
        public AttackAnimationData(
            string unitName,
            ClassId unitClassId,
            double attackRate,
            double attackPoint,
            double attackBackswing,
            int projectileSpeed,
            double turnRate)
        {
            this.UnitName = unitName;
            this.UnitClassId = unitClassId;
            this.AttackRate = attackRate;
            this.AttackPoint = attackPoint;
            this.AttackBackswing = attackBackswing;
            this.ProjectileSpeed = projectileSpeed;
            this.TurnRate = turnRate;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AttackAnimationData" /> class.
        /// </summary>
        public AttackAnimationData()
        {
            // If this comment is removed the program will blow up.
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     The attack animation backswing.
        /// </summary>
        public double AttackBackswing { get; set; }

        /// <summary>
        ///     The attack animation point.
        /// </summary>
        public double AttackPoint { get; set; }

        /// <summary>
        ///     The attack animation rate.
        /// </summary>
        public double AttackRate { get; set; }

        /// <summary>
        ///     The attack animation projectile speed.
        /// </summary>
        public int ProjectileSpeed { get; set; }

        /// <summary>
        ///     The attack animation turn rate.
        /// </summary>
        public double TurnRate { get; set; }

        /// <summary>
        ///     The unit class id.
        /// </summary>
        public ClassId UnitClassId { get; set; }

        /// <summary>
        ///     The unit name.
        /// </summary>
        public string UnitName { get; set; }

        #endregion
    }
}