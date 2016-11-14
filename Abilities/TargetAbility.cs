namespace Ensage.Common.Abilities
{
    using System;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Ensage.Common.Extensions;

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

        public override async Task Execute(Unit target, CancellationToken token = default(CancellationToken))
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            Log.Debug($"Use {this.Instance.Name} @ {target.Name}");
            this.Instance.UseAbility(target);
        }

        public override Task Execute(Vector3 position, CancellationToken token = default(CancellationToken))
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}