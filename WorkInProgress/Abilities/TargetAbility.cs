namespace Ensage.Common.WorkInProgress.Abilities
{
    using System;
    using System.Reflection;

    using Ensage.Common.Extensions;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    public class TargetAbility : AbilityBase
    {
        #region Static Fields

        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Constructors and Destructors

        public TargetAbility(Hero owner, Ability ability)
            : base(owner, ability)
        {
        }

        public TargetAbility(Hero owner, string ability)
            : base(owner, owner.FindSpell(ability))
        {
        }

        #endregion

        #region Public Methods and Operators

        public override bool Use(Unit target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            Log.Debug($"Use {this.Instance.Name} @ {target.Name}");
            this.Instance.UseAbility(target);
            return true;
        }

        public override bool Use(Vector3 position)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}