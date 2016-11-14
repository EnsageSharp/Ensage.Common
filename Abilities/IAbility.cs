namespace Ensage.Common.Abilities
{
    using System.Threading;
    using System.Threading.Tasks;

    using Ensage.Common.Predictions;

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

        Task Execute(Unit target, CancellationToken token = default(CancellationToken));

        Task Execute(Vector3 position, CancellationToken token = default(CancellationToken));

        float GetDamage(Unit target, int index = 0);

        void SetPrediction(IPredictionInput input);

        #endregion
    }
}