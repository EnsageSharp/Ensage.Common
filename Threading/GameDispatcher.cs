// <copyright file="GameDispatcher.cs" company="EnsageSharp">
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

    public static class GameDispatcher
    {
        #region Constructors and Destructors

        static GameDispatcher()
        {
            Game.OnUpdate += UpdateDispatcher;
            Game.OnIngameUpdate += IngameUpdateDispatcher;
        }

        #endregion

        #region Public Events

        public static event GameIngameUpdate OnIngameUpdate;

        public static event GameUpdate OnUpdate;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Schedules <paramref name="action" /> to be executed on next Update
        /// </summary>
        /// <param name="action"></param>
        public static void BeginInvoke(Action action)
        {
            GameSynchronizationContext.Instance.Post(state => action(), null);
        }

        public static void InvokeEvent(Action action)
        {
            var gameContext = GameSynchronizationContext.Instance;
            var context = SynchronizationContext.Current;

            try
            {
                SynchronizationContext.SetSynchronizationContext(gameContext);
                action();
                gameContext.RunOnCurrentThread();
            }
            finally
            {
                SynchronizationContext.SetSynchronizationContext(context);
            }
        }

        #endregion

        #region Methods

        private static void IngameUpdateDispatcher(EventArgs args)
        {
            InvokeEvent(() => OnIngameUpdate?.Invoke(EventArgs.Empty));
        }

        private static void UpdateDispatcher(EventArgs args)
        {
            InvokeEvent(() => OnUpdate?.Invoke(EventArgs.Empty));
        }

        #endregion
    }
}