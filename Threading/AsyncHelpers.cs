namespace Ensage.Common.Threading
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Ensage.Common.Extensions;

    /// <summary>
    /// ...
    /// </summary>
    public static class AsyncHelpers
    {
        #region Public Methods and Operators

        /// <summary>
        /// Waits until the target has a certain modifier.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="name">Name of the modifier.</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public static async Task<bool> WaitModifierAsync(Unit target, string name,
            CancellationToken ct = default(CancellationToken))
        {
            try
            {
                while (!target.HasModifier(name))
                    await Task.Delay(100, ct);
            }
            catch (OperationCanceledException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Move to a target until you're in a certain range and stops then.
        /// </summary>
        /// <param name="me"></param>
        /// <param name="target"></param>
        /// <param name="range"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public static async Task<bool> MoveToTargetAsync(Unit me, Unit target, float range,
              CancellationToken ct = default(CancellationToken))
        {
            try
            {
                while (me.Distance2D(target) > range)
                {
                    me.Move(target.Position);

                    await Task.Delay(100, ct);
                }
            }
            catch (OperationCanceledException)
            {
                me.Stop();
                return false;
            }
            me.Stop();
            return true;
        }

        #endregion
    }
}