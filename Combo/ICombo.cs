// <copyright file="ICombo.cs" company="EnsageSharp">
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
namespace Ensage.Common.Combo
{
    /// <summary>
    ///     ICombo component.
    /// </summary>
    public interface ICombo
    {
        #region Public Properties

        /// <summary>
        ///     Gets a value indicating whether the current execution is completed.
        /// </summary>
        bool IsCompleted { get; }

        /// <summary>
        ///     Gets a value indicating whether the current execution is running.
        /// </summary>
        bool IsRunning { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Activate Combo.
        /// </summary>
        void Activate();

        /// <summary>
        ///     Cancel current execution cycle.
        /// </summary>
        void Cancel();

        /// <summary>
        ///     Deactivate Combo.
        /// </summary>
        void Deactivate();

        #endregion
    }
}