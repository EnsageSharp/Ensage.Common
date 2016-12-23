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
    using System.Linq;
    using System.Reflection;

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

        private static readonly EdgeTrigger IngameTrigger = new EdgeTrigger();

        private static readonly List<Delegate> NotifiedSubscribers = new List<Delegate>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="Events" /> class.
        /// </summary>
        static Events()
        {
            IngameTrigger.Fallen += IngameTriggerOnFallen;
            IngameTrigger.Risen += IngameTriggerOnRisen;
            Game.OnUpdate += UpdateTrigger;
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

        private static void IngameTriggerOnFallen(object sender, EventArgs eventArgs)
        {
            Console.WriteLine("Unloading...");

            // cleanup
            Game.OnUpdate -= UpdateOnLoad;

            // raise and reset framework
            OnClose?.Invoke(MethodBase.GetCurrentMethod().DeclaringType, EventArgs.Empty);
            Init();
        }

        private static void IngameTriggerOnRisen(object sender, EventArgs eventArgs)
        {
            Console.WriteLine("Loading...");

            Init();
            Game.OnUpdate += UpdateOnLoad;
        }

        /// <summary>
        ///     The load.
        /// </summary>
        private static void Init()
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
            Utils.Sleeps = new Dictionary<string, double>();
            NotifiedSubscribers.Clear();
        }

        private static void UpdateOnLoad(EventArgs args)
        {
            if (!Utils.SleepCheck("Events.OnLoad"))
            {
                return;
            }

            if (OnLoad == null)
            {
                return;
            }

            if (!IngameTrigger.Value)
            {
                return;
            }

            var subscribers = OnLoad.GetInvocationList();

            foreach (var subscriber in subscribers.Where(s => !NotifiedSubscribers.Contains(s)))
            {
                NotifiedSubscribers.Add(subscriber);

                try
                {
                    subscriber.DynamicInvoke(MethodBase.GetCurrentMethod().DeclaringType, EventArgs.Empty);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            Utils.Sleep(250, "Events.OnLoad");
        }

        private static void UpdateTrigger(EventArgs args)
        {
            OnUpdate?.Invoke(EventArgs.Empty);

            IngameTrigger.Value = Game.IsInGame && ObjectManager.LocalHero != null && ObjectManager.LocalHero.IsValid;
        }

        #endregion
    }
}