// <copyright file="AttackAnimationTracker.cs" company="EnsageSharp">
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

        /// <summary>The custom attack speed value.</summary>
        private float customAttackSpeedValue;

        /// <summary>
        ///     The is attacking.
        /// </summary>
        private bool isAttacking;

        /// <summary>
        ///     The last unit activity.
        /// </summary>
        private NetworkActivity lastUnitActivity;

        /// <summary>
        ///     The unit.
        /// </summary>
        private Unit unit;

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

            // Drawing.OnDraw += this.Track;
            Entity.OnInt32PropertyChange += this.Entity_OnInt32PropertyChange;
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets the custom attack speed value.</summary>
        public float CustomAttackSpeedValue
        {
            get
            {
                return this.customAttackSpeedValue;
            }

            set
            {
                this.customAttackSpeedValue = value;
                this.UsingCustomAttackSpeedValue = this.customAttackSpeedValue > 0;
            }
        }

        /// <summary>
        ///     Gets the last unit attack start.
        /// </summary>
        public float LastUnitAttackStart { get; private set; }

        /// <summary>
        ///     The next unit attack end.
        /// </summary>
        public float NextUnitAttackEnd { get; private set; }

        /// <summary>
        ///     The next unit attack release.
        /// </summary>
        public float NextUnitAttackRelease { get; private set; }

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        public Unit Unit
        {
            get
            {
                return this.unit;
            }

            set
            {
                this.unit = value;
                if (this.unit != null && this.unit.IsValid)
                {
                    Entity.OnInt32PropertyChange += this.Entity_OnInt32PropertyChange;
                }
            }
        }

        /// <summary>The using custom attack speed value.</summary>
        public bool UsingCustomAttackSpeedValue { get; private set; }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets a value indicating whether attack order sent.
        /// </summary>
        private bool AttackOrderSent { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The attack end.
        /// </summary>
        public virtual void AttackEnd()
        {
        }

        /// <summary>
        ///     Informs attack animation tracker that the attack order was sent
        /// </summary>
        public void AttackOrder()
        {
            if (!this.isAttacking)
            {
                this.AttackOrderSent = true;
                DelayAction.Add(
                    Game.Ping + 50,
                    () =>
                        {
                            if (!this.isAttacking)
                            {
                                this.AttackOrderSent = false;
                            }
                        });
            }
        }

        /// <summary>
        ///     The attack start.
        /// </summary>
        public virtual void AttackStart()
        {
        }

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

            var isValid = target != null && target.IsValid && target.IsAlive && target.IsVisible && !target.IsInvul()
                          && !target.HasModifiers(
                              new[] { "modifier_ghost_state", "modifier_item_ethereal_blade_slow" },
                              false)
                          && target.Distance2D(this.Unit)
                          <= this.Unit.GetAttackRange() + this.Unit.HullRadius + 75 + targetHull + Math.Max(distance, 0)
                          || target != null && this.Unit.IsAttacking() && this.Unit.GetTurnTime(target.Position) < 0.1;

            return !this.IsAttackOnCoolDown(target, bonusWindupMs) && (target == null || isValid);
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
            if (this.AttackOrderSent && !this.isAttacking)
            {
                return false;
            }

            if (this.Unit == null || !this.Unit.IsValid)
            {
                return true;
            }

            var time = Utils.TickCount;
            var cancelTime = this.NextUnitAttackRelease + delay - Game.Ping;
            return time > cancelTime;
        }

        /// <summary>
        ///     The is attack on cool down.
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
                           + Math.Max(this.Unit.Distance2D(target) - this.Unit.GetAttackRange() - 100, 0)
                           / this.Unit.MovementSpeed;
            }

            return this.NextUnitAttackEnd - Game.Ping - turnTime * 1000 - 120 + bonusWindupMs > Utils.TickCount;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The entity_ on int 32 property change.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void Entity_OnInt32PropertyChange(Entity sender, Int32PropertyChangeEventArgs args)
        {
            if (!sender.Equals(this.Unit) || args.PropertyName != "m_NetworkActivity")
            {
                return;
            }

            if (this.Unit == null || !this.Unit.IsValid)
            {
                Entity.OnInt32PropertyChange -= this.Entity_OnInt32PropertyChange;
                return;
            }

            if (!Game.IsInGame || Game.IsPaused)
            {
                return;
            }

            var newValue = (NetworkActivity)args.NewValue;
            var oldValue = (NetworkActivity)args.OldValue;
            if (newValue == this.lastUnitActivity || newValue == oldValue)
            {
                return;
            }

            var canCancel = this.CanCancelAttack();
            var wasAttacking = false;
            if (!this.IsAttackOnCoolDown() || canCancel)
            {
                wasAttacking = this.isAttacking;
                this.lastUnitActivity = newValue;
                this.isAttacking = newValue == NetworkActivity.Attack || newValue == NetworkActivity.Crit
                                   || newValue == NetworkActivity.Attack2 || newValue == NetworkActivity.AttackEvent
                                   || newValue == NetworkActivity.AttackEventBash
                                   || newValue == NetworkActivity.EarthshakerTotemAttack;
            }

            if (wasAttacking && canCancel && !this.isAttacking
                && (oldValue == NetworkActivity.Attack || oldValue == NetworkActivity.Crit
                    || oldValue == NetworkActivity.Attack2 || oldValue == NetworkActivity.AttackEvent
                    || oldValue == NetworkActivity.AttackEventBash || oldValue == NetworkActivity.EarthshakerTotemAttack))
            {
                this.AttackEnd();
            }

            if (!this.isAttacking || !this.isAttacking && !canCancel)
            {
                return;
            }

            this.LastUnitAttackStart = Game.RawGameTime;
            this.NextUnitAttackEnd =
                (float)
                (this.LastUnitAttackStart * 1000
                 + (this.UsingCustomAttackSpeedValue
                        ? UnitDatabase.GetAttackRate(this.Unit, this.customAttackSpeedValue)
                        : UnitDatabase.GetAttackRate(this.Unit)) * 1000);
            this.NextUnitAttackRelease =
                (float)
                (this.LastUnitAttackStart * 1000
                 + (this.UsingCustomAttackSpeedValue
                        ? UnitDatabase.GetAttackPoint(this.Unit, this.customAttackSpeedValue)
                        : UnitDatabase.GetAttackPoint(this.Unit)) * 1000);
            this.AttackOrderSent = false;
            this.AttackStart();
        }

        #endregion
    }
}