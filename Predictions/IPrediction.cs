namespace Ensage.Common.Predictions
{
    using Ensage.Common.Abilities;

    public interface IPrediction
    {
        #region Public Methods and Operators

        IPredictionOutput GetPrediction(IAbility ability, Unit target);

        #endregion
    }
}