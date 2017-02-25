// <copyright file="AsyncHelpers.cs" company="EnsageSharp">
//    Copyright (c) 2017 EnsageSharp.
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>
namespace Ensage.Common.Threading
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Ensage.Common.Extensions;

    using SharpDX;

    /// <summary>
    ///     Game Async helper functions.
    /// </summary>
    public static class AsyncHelpers
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Move to a target until you're in a certain range and stops then.
        /// </summary>
        /// <param name="me"></param>
        /// <param name="target"></param>
        /// <param name="range"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public static async Task<bool> MoveToTargetAsync(
            this Unit me,
            Vector3 target,
            float range,
            CancellationToken ct = default(CancellationToken))
        {
            try
            {
                while (me.Distance2D(target) > range)
                {
                    me.Move(target);

                    await Task.Delay(100, ct);
                }
            }
            catch (OperationCanceledException)
            {
                return false;
            }
            finally
            {
                me.Stop();
            }

            return true;
        }

        /// <summary>
        ///     Move to a target until you're in a certain range and stops then.
        /// </summary>
        /// <param name="me"></param>
        /// <param name="target"></param>
        /// <param name="range"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public static async Task<bool> MoveToTargetAsync(
            this Unit me,
            Unit target,
            float range,
            CancellationToken ct = default(CancellationToken))
        {
            return await MoveToTargetAsync(me, target.NetworkPosition, range, ct);
        }

        /// <summary>
        ///     Waits until the target has a certain modifier.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="name">Name of the modifier.</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [Obsolete("WaitModifierAsync is deprecated, please use WaitGainModifierAsync instead.")]
        public static async Task<bool> WaitModifierAsync(
            this Unit target,
            string name,
            CancellationToken ct = default(CancellationToken))
        {
            try
            {
                while (!target.HasModifier(name))
                {
                    await Task.Delay(100, ct);
                }
            }
            catch (OperationCanceledException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Waits until the target has a certain modifier.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="name">Name of the modifier.</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public static async Task<bool> WaitGainModifierAsync(
            this Unit target,
            string name,
            CancellationToken ct = default(CancellationToken))
        {
            try
            {
                while (!target.HasModifier(name))
                {
                    await Task.Delay(100, ct);
                }
            }
            catch (OperationCanceledException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Waits until the target hasn't a certain modifier.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="name">Name of the modifier.</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public static async Task<bool> WaitLossModifierAsync(
            this Unit target,
            string name,
            CancellationToken ct = default(CancellationToken))
        {
            try
            {
                while (target.HasModifier(name))
                {
                    await Task.Delay(100, ct);
                }
            }
            catch (OperationCanceledException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Waits until the target has a certain unitstate.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="state">UnitState to wait for.</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public static async Task<bool> WaitGainUnitStateAsync(
            this Unit target,
            UnitState state,
            CancellationToken ct = default(CancellationToken))
        {
            try
            {
                while (!target.UnitState.HasFlag(state))
                {
                    await Task.Delay(100, ct);
                }
            }
            catch (OperationCanceledException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Waits until the target has not a certain unitstate.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="state">UnitState to wait for.</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public static async Task<bool> WaitLossUnitStateAsync(
            this Unit target,
            UnitState state,
            CancellationToken ct = default(CancellationToken))
        {
            try
            {
                while (target.UnitState.HasFlag(state))
                {
                    await Task.Delay(100, ct);
                }
            }
            catch (OperationCanceledException)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}