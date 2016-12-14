// <copyright file="Events.cs" company="EnsageSharp">
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
namespace Ensage.Common
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;

    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Extensions;
    using Ensage.Common.Extensions.Damage;
    using Ensage.Common.Objects;
    using Ensage.Common.Threading;

    /// <summary>
    ///     Provides custom events
    /// </summary>
    public class Events
    {
        #region Static Fields

        private static bool callOnLoad;

        /// <summary>
        ///     The loaded.
        /// </summary>
        private static bool loaded;

        /// <summary>
        ///     The unloaded.
        /// </summary>
        private static bool unloaded = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="Events" /> class.
        /// </summary>
        static Events()
        {
            Game.OnUpdate += args =>
                {
                    CallOnUpdate();
                    if (!Game.IsInGame || ObjectManager.LocalHero == null || !ObjectManager.LocalHero.IsValid)
                    {
                        if (!unloaded)
                        {
                            CallOnClose();
                            Load();
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
                    Load();
                    DelayAction.Add(200, CallOnLoad);
                };

            //GameDispatcher.OnUpdate += args =>
            //    {
            //        if (!callOnLoad)
            //        {
            //            return;
            //        }

            //        callOnLoad = false;
            //        CallOnLoadAsync();
            //    };
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

        /// <summary>
        ///     OnUpdate Delegate.
        /// </summary>
        /// <param name="e"><see cref="EventArgs" /> event data</param>
        public delegate void OnUpdateDelegate(EventArgs e);

        #endregion

        #region Public Events

        /// <summary>
        ///     OnClose is getting called after game ends
        /// </summary>
        public static event OnCloseDelegate OnClose;

        /// <summary>
        ///     OnLoad is getting called after you pick a hero (doesn't matter if started or restarted while game is already
        ///     running) and when reloading an assembly.
        /// </summary>
        public static event OnLoadDelegate OnLoad;

        /// <summary>
        ///     Temporary fix for InvalidOperationException
        /// </summary>
        public static event OnUpdateDelegate OnUpdate;

        #endregion

        #region Methods

        /// <summary>
        ///     The call on close.
        /// </summary>
        private static void CallOnClose()
        {
            OnClose?.Invoke(MethodBase.GetCurrentMethod().DeclaringType, EventArgs.Empty);
        }

        /// <summary>
        ///     Calls the OnLoad event.
        /// </summary>
        private static void CallOnLoad()
        {
            OnLoad?.Invoke(MethodBase.GetCurrentMethod().DeclaringType, EventArgs.Empty);
        }

        private static async void CallOnLoadAsync()
        {
            Load();
            await Task.Delay(50);
            await OnLoadAsync();
        }

        /// <summary>
        ///     The call on update.
        /// </summary>
        private static void CallOnUpdate()
        {
            OnUpdate?.Invoke(EventArgs.Empty);
        }

        /// <summary>
        ///     The load.
        /// </summary>
        private static void Load()
        {
            AbilityDatabase.Init();
            AbilityDamage.Init();
            AbilityExtensions.Init();
            HeroExtensions.Init();
            UnitExtensions.Init();
            Names.Init();
            Abilities.Init();
            Calculations.Init();
            EntityExtensions.Init();
            Orbwalking.Events_OnLoad(null, null);
            Utils.Sleeps = new Dictionary<string, double>();
        }

        private static async Task OnLoadAsync()
        {
            if (OnLoad == null)
            {
                return;
            }

            foreach (var @delegate in OnLoad.GetInvocationList())
            {
                if (@delegate == null)
                {
                    return;
                }

                @delegate.DynamicInvoke(MethodBase.GetCurrentMethod().DeclaringType, EventArgs.Empty);
                if (@delegate.Target == null)
                {
                    //Console.WriteLine(@delegate.ToString());
                    continue;
                }

                if (@delegate.Target.ToString().Contains("Transitions."))
                {
                    continue;
                }

                //Console.WriteLine(@delegate.Target.ToString());
                await Task.Delay(50);
            }
        }

        #endregion
    }
}