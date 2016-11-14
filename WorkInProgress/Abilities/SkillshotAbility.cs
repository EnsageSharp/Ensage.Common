namespace Ensage.Common.WorkInProgress.Abilities
{
    using System;
    using System.Reflection;

    using Ensage.Common.Extensions;
    using Ensage.Common.WorkInProgress.Prediction;

    using log4net;

    using PlaySharp.Toolkit.Logging;

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

        public override bool Use(Unit target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            var output = this.Prediction.Instance.GetPrediction(this, target);
            Log.Debug($"PredictionOutput {output}");

            if (output.Cast)
            {
                this.Use(output.Position);
                return true;
            }

            return false;
        }

        public override bool Use(Vector3 position)
        {
            if (position.IsValid())
            {
                throw new ArgumentNullException(nameof(position));
            }

            Log.Debug($"UseAbility {this.Instance.Name} @ {position}");
            this.Instance.UseAbility(position);
            return true;
        }

        #endregion
    }
}