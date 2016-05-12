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
            Events.OnUpdate += this.Track;
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
        public bool CanAttack(Entity target = null, float bonusWindupMs = 0)
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

            return this.nextUnitAttackEnd - Game.Ping - (turnTime * 1000) - 75 + bonusWindupMs < Utils.TickCount;
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
                Events.OnUpdate -= this.Track;
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

            this.nextUnitAttackEnd = (float)(Utils.TickCount + (UnitDatabase.GetAttackRate(this.Unit) * 1000));
            this.nextUnitAttackRelease = (float)(Utils.TickCount + (UnitDatabase.GetAttackPoint(this.Unit) * 1000));
        }

        #endregion
    }
}