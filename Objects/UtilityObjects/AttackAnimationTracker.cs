// <copyright file="AttackAnimationTracker.cs" company="EnsageSharp">
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
namespace Ensage.Common.Objects.UtilityObjects
{
    using System;

    using Ensage.Common.Extensions;

    /// <summary>
    ///     The attack animation tracker.
    /// </summary>
    public class AttackAnimationTracker
    {
        #region Fields

        /// <summary>
        ///     The last unit activity.
        /// </summary>
        private NetworkActivity lastUnitActivity;

        /// <summary>
        ///     The next unit attack end.
        /// </summary>
        private float nextUnitAttackEnd;

        /// <summary>
        ///     The next unit attack release.
        /// </summary>
        private float nextUnitAttackRelease;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AttackAnimationTracker" /> class.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        protected AttackAnimationTracker(Unit unit)
        {
            this.Unit = unit;
            Drawing.OnDraw += this.Track;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        public Unit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The can attack.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="bonusWindupMs">
        ///     The bonus windup milliseconds.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool CanAttack(Unit target = null, float bonusWindupMs = 0)
        {
            if (this.Unit == null || !this.Unit.IsValid)
            {
                return false;
            }

            var targetHull = 0f;
            if (target != null)
            {
                targetHull = target.HullRadius;
            }

            float distance = 0;
            if (target != null)
            {
                var pos = Prediction.InFront(
                    this.Unit,
                    (float)(Game.Ping / 1000 + this.Unit.GetTurnTime(target.Position) * this.Unit.MovementSpeed));
                distance = pos.Distance2D(target) - this.Unit.Distance2D(target);
            }

            var isValid = (target != null && target.IsValid && target.IsAlive && target.IsVisible && !target.IsInvul()
                          && !target.HasModifiers(
                              new[] { "modifier_ghost_state", "modifier_item_ethereal_blade_slow" },
                              false)
                          && target.Distance2D(this.Unit)
                          <= this.Unit.GetAttackRange() + this.Unit.HullRadius + 75 + targetHull
                          + Math.Max(distance, 0)) || (target != null && this.Unit.IsAttacking() && this.Unit.GetTurnTime(target.Position) < 0.1);

            return !this.IsAttackOnCoolDown(target, bonusWindupMs) && (target == null || isValid);
        }

        /// <summary>
        /// The is attack on cool down.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="bonusWindupMs">
        /// The bonus windup milliseconds.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsAttackOnCoolDown(Unit target = null, float bonusWindupMs = 0)
        {
            if (this.Unit == null || !this.Unit.IsValid)
            {
                return false;
            }

            var turnTime = 0d;
            if (target != null)
            {
                turnTime = this.Unit.GetTurnTime(target)
                           + (Math.Max(this.Unit.Distance2D(target) - this.Unit.GetAttackRange() - 100, 0)
                              / this.Unit.MovementSpeed);
            }

            return this.nextUnitAttackEnd - Game.Ping - (turnTime * 1000) - 75 + bonusWindupMs >= Utils.TickCount;
        }

        /// <summary>
        ///     The can cancel attack.
        /// </summary>
        /// <param name="delay">
        ///     The delay.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool CanCancelAttack(float delay = 0f)
        {
            if (this.Unit == null || !this.Unit.IsValid)
            {
                return true;
            }

            var time = Utils.TickCount;
            var cancelTime = this.nextUnitAttackRelease - Game.Ping - delay + 50;
            return time >= cancelTime;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The track.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void Track(EventArgs args)
        {
            if (this.Unit == null || !this.Unit.IsValid)
            {
                Drawing.OnDraw -= this.Track;
                return;
            }

            if (!Game.IsInGame || Game.IsPaused)
            {
                return;
            }

            if (this.Unit.NetworkActivity == this.lastUnitActivity)
            {
                return;
            }

            this.lastUnitActivity = this.Unit.NetworkActivity;
            if (!this.Unit.IsAttacking())
            {
                if (this.CanCancelAttack())
                {
                    return;
                }

                this.lastUnitActivity = 0;
                this.nextUnitAttackEnd = 0;
                this.nextUnitAttackRelease = 0;
                return;
            }

            this.nextUnitAttackEnd = (float)(Utils.TickCount + UnitDatabase.GetAttackRate(this.Unit) * 1000);
            this.nextUnitAttackRelease = (float)(Utils.TickCount + UnitDatabase.GetAttackPoint(this.Unit) * 1000);
        }

        #endregion
    }
}