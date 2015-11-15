// <copyright file="Load.cs" company="EnsageSharp">
//    Copyright (c) 2015 EnsageSharp.
// 
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
// 
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
// 
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>

namespace Ensage.Common
{
    using System;
    using System.Reflection;

    /// <summary>
    ///     Provides custom events
    /// </summary>
    public class Events
    {
        #region Static Fields

        private static bool loaded;

        private static bool unloaded;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="Events" /> class.
        /// </summary>
        static Events()
        {
            Game.OnUpdate += args =>
                {
                    if (!Game.IsInGame || ObjectMgr.LocalHero == null || !ObjectMgr.LocalHero.IsValid)
                    {
                        if (!unloaded)
                        {
                            CallOnClose();
                            unloaded = true;
                        }
                        loaded = false;
                        return;
                    }
                    if (loaded)
                    {
                        return;
                    }
                    unloaded = false;
                    loaded = true;
                    CallOnLoad();
                };
        }

        #endregion

        #region Delegates

        /// <summary>
        ///     OnClose Delegate
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e"><see cref="EventArgs" /> event data</param>
        public delegate void OnCloseDelegate(object sender, EventArgs e);

        /// <summary>
        ///     OnLoad Delegate.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e"><see cref="EventArgs" /> event data</param>
        public delegate void OnLoadDelegate(object sender, EventArgs e);

        #endregion

        #region Public Events

        /// <summary>
        ///     OnClose is getting called after game ends
        /// </summary>
        public static event OnLoadDelegate OnClose;

        /// <summary>
        ///     OnLoad is getting called after you pick a hero (doesn't matter if started or restarted while game is already
        ///     running) and when reloading an assembly.
        /// </summary>
        public static event OnLoadDelegate OnLoad;

        #endregion

        #region Methods

        private static void CallOnClose()
        {
            if (OnClose != null)
            {
                OnClose(MethodBase.GetCurrentMethod().DeclaringType, EventArgs.Empty);
            }
        }

        /// <summary>
        ///     Calls the OnLoad event.
        /// </summary>
        private static void CallOnLoad()
        {
            if (OnLoad != null)
            {
                OnLoad(MethodBase.GetCurrentMethod().DeclaringType, EventArgs.Empty);
            }
        }

        #endregion
    }
}