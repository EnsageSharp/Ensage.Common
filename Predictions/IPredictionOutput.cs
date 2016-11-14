namespace Ensage.Common.Predictions
{
    using SharpDX;

    public interface IPredictionOutput
    {
        #region Public Properties

        bool Cast { get; }

        Vector3 Position { get; }

        #endregion
    }
}