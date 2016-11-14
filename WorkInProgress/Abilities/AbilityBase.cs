namespace Ensage.Common.WorkInProgress.Abilities
{
    using System;
    using System.Reflection;

    using Ensage.Common.WorkInProgress.Prediction;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    public abstract class AbilityBase : IAbility
    {
        #region Static Fields

        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Constructors and Destructors

        protected AbilityBase(Hero owner, Ability ability)
        {
            if (owner == null)
            {
                throw new ArgumentNullException(nameof(owner));
            }

            if (ability == null)
            {
                throw new ArgumentNullException(nameof(ability));
            }

            this.Owner = owner;
            this.Instance = ability;
        }

        #endregion

        #region Public Properties

        public Ability Instance { get; }

        public virtual bool IsHidden => !this.Instance.IsHidden;

        public virtual bool IsReady => this.Instance.Cooldown == 0;

        public virtual bool IsSkilled => this.Instance.Level > 0;

        public virtual float ManaCost => this.Instance.ManaCost;

        public Hero Owner { get; }

        #endregion

        #region Properties

        protected IPredictionInput Prediction { get; set; }

        #endregion

        #region Public Methods and Operators

        public virtual float GetDamage(Unit target, int index = 0)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            return this.Instance.GetDamage((uint)index);
        }

        public void SetPrediction(IPredictionInput input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            this.Prediction = input;
        }

        public abstract bool Use(Unit target);

        public abstract bool Use(Vector3 position);

        #endregion
    }
}