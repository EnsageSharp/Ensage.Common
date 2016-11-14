namespace Ensage.Common.WorkInProgress.Abilities
{
    using Ensage.Common.WorkInProgress.Prediction;

    using SharpDX;

    public interface IAbility
    {
        #region Public Properties

        Ability Instance { get; }

        bool IsHidden { get; }

        bool IsReady { get; }

        bool IsSkilled { get; }

        float ManaCost { get; }

        #endregion

        #region Public Methods and Operators

        float GetDamage(Unit target, int index = 0);

        void SetPrediction(IPredictionInput input);

        bool Use(Unit target);

        bool Use(Vector3 position);

        #endregion
    }
}