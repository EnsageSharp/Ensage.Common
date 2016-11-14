namespace Ensage.Common.Abilities
{
    using System;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Ensage.Common.Extensions;
    using Ensage.Common.Predictions;

    using SharpDX;

    public class SkillshotAbility : AbilityBase
    {
        #region Static Fields

        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Constructors and Destructors

        public SkillshotAbility(Hero owner, Ability ability, IPredictionInput prediction = null)
            : base(owner, ability)
        {
            this.Prediction = prediction;
        }

        public SkillshotAbility(Hero owner, string ability, IPredictionInput prediction = null)
            : this(owner, (Ability)owner.FindSpell(ability), prediction)
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

            var output = this.Prediction.Instance.GetPrediction(this, target);
            Log.Debug($"PredictionOutput {output}");

            if (output.Cast)
            {
                await this.Execute(output.Position, token);
            }
        }

        public override async Task Execute(Vector3 position, CancellationToken token = default(CancellationToken))
        {
            if (position.IsValid())
            {
                throw new ArgumentNullException(nameof(position));
            }

            Log.Debug($"UseAbility {this.Instance.Name} @ {position}");
            this.Instance.UseAbility(position);
        }

        #endregion
    }
}