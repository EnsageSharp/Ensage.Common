namespace Ensage.Common.WorkInProgress.Prediction
{
    using Ensage.Common.WorkInProgress.Abilities;

    public interface IPrediction
    {
        #region Public Methods and Operators

        IPredictionOutput GetPrediction(IAbility ability, Unit target);

        #endregion
    }
}