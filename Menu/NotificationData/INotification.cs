// <copyright file="INotification.cs" company="EnsageSharp">
//    Copyright (c) 2016 EnsageSharp.
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
namespace Ensage.Common.Menu.NotificationData
{
    /// <summary>
    /// </summary>
    public interface INotification
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Returns the Notification ID
        /// </summary>
        /// <returns>GUID</returns>
        string GetId();

        /// <summary>
        ///     Gets called when Screen->Present(); is called
        /// </summary>
        void OnDraw();

        /// <summary>
        ///     Gets called after resetting the device
        /// </summary>
        void OnPostReset();

        /// <summary>
        ///     Gets called before resetting the device
        /// </summary>
        void OnPreReset();

        /// <summary>
        ///     Gets called when Game -> Tick happens and updates the game.
        /// </summary>
        void OnUpdate();

        /// <summary>
        ///     Gets called on a WindowsMessage event.
        /// </summary>
        /// <param name="args">WndEventArgs</param>
        void OnWndProc(WndEventArgs args);

        #endregion
    }
}