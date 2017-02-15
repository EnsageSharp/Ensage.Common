// <copyright file="Orbwalking.cs" company="EnsageSharp">
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
namespace Ensage.Common
{
    using System;
    using System.Runtime.CompilerServices;

    using Ensage.Common.Menu;
    using Ensage.Common.Objects.UtilityObjects;

    /// <summary>
    ///     Provides basic orbwalking, canceling attack animation after attack point passes and moving meanwhile
    /// </summary>
    public class Orbwalking
    {
        #region Static Fields

        /// <summary>
        ///     The loaded.
        /// </summary>
        private static bool loaded;

        /// <summary>
        ///     The menu.
        /// </summary>
        private static Menu.Menu menu;

        /// <summary>
        ///     The orbwalker.
        /// </summary>
        private static Orbwalker orbwalker;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="Orbwalking" /> class.
        /// </summary>
        static Orbwalking()
        {
            Events.OnLoad += OnLoad;
            Events.OnClose += OnClose;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     The user delay.
        /// </summary>
        public static float UserDelay { get; private set; }

        /// <summary>Gets a value indicating whether enable orbwalking.</summary>
        public static bool EnableOrbwalking { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Attacks target, uses spell UniqueAttackModifiers if enabled
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="useModifiers">
        ///     The use Modifiers.
        /// </param>
        public static void Attack(Unit target, bool useModifiers)
        {
            orbwalker.Attack(target, useModifiers);
        }

        /// <summary>
        ///     Checks if attack is currently on cool down
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="bonusWindupMs">
        ///     The bonus Windup milliseconds.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool AttackOnCooldown(Entity target = null, float bonusWindupMs = 0)
        {
            return orbwalker.IsAttackOnCoolDown(target as Unit, bonusWindupMs);
        }

        /// <summary>
        ///     Checks if attack is currently on cool down
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="bonusWindupMs">
        ///     The bonus Windup milliseconds.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool AttackOnCooldown(Unit target, float bonusWindupMs)
        {
            return orbwalker.IsAttackOnCoolDown(target, bonusWindupMs);
        }

        /// <summary>
        ///     Checks if attack animation can be safely canceled
        /// </summary>
        /// <param name="delay">
        ///     The delay.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool CanCancelAnimation(float delay = 0f)
        {
            return orbwalker.CanCancelAttack(delay);
        }

        public static void Load()
        {
        }

        /// <summary>
        ///     Orbwalks on given target if they are in range, while moving to mouse position
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="bonusWindupMs">
        ///     The bonus Windup Ms.
        /// </param>
        /// <param name="bonusRange">
        ///     The bonus Range.
        /// </param>
        /// <param name="attackmodifiers">
        ///     The attackmodifiers.
        /// </param>
        /// <param name="followTarget">
        ///     The follow Target.
        /// </param>
        public static void Orbwalk(
            Unit target,
            float bonusWindupMs = 0,
            float bonusRange = 0,
            bool attackmodifiers = false,
            bool followTarget = false)
        {
            orbwalker.OrbwalkOn(target, bonusWindupMs, bonusRange, attackmodifiers, followTarget);
        }

        #endregion

        #region Methods

        private static void EnableDebugMenuItem_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            orbwalker.EnableDebug = e.GetNewValue<bool>();
        }

        /// <summary>
        ///     The events_ on close.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private static void OnClose(object sender, EventArgs e)
        {
            // menu.Items.Remove(menu.Items.FirstOrDefault(x => x.Name == ObjectManager.LocalHero?.Name + "Common.Orbwalking.UserDelay"));
            orbwalker.Unit = null;
            Menu.Menu.Root.RemoveSubMenu(menu.Name);
            menu = null;
        }

        private static void OnLoad(object sender, EventArgs eventArgs)
        {
            if (menu == null)
            {
                menu = Menu.Menu.Root.AddSubMenu(new Menu.Menu("Orbwalking", "Common.Orbwalking"));

                var enableOrbwalker =
                    menu.AddItem(
                        new MenuItem(
                                "common.orbwalking.enable",
                                "Enable orbwalking for " + Game.Localize(ObjectManager.LocalHero.Name),
                                true).SetValue(true)
                            .SetTooltip(
                                "Disable this if you dont want currently picked hero to move between attacks. Consider adjusting the AttackCancelDelay below in case attack is getting canceled"));
                enableOrbwalker.ValueChanged += (o, args) =>
                    { EnableOrbwalking = args.GetNewValue<bool>(); };
                EnableOrbwalking = enableOrbwalker.GetValue<bool>();

                var enableDebugMenuItem = menu.AddItem(new MenuItem("common.orbwalking.debug", "Debug").SetValue(false));
                enableDebugMenuItem.ValueChanged += EnableDebugMenuItem_ValueChanged;

                var userDelayMenuItem =
                    menu.AddItem(
                        new MenuItem("Common.Orbwalking.UserDelay", "Attack cancel delay", true).SetValue(
                                new Slider(0, -200, 200))
                            .SetTooltip(
                                "Minus value=attack animation gets canceled sooner, Plus value=attack animation gets canceled later (set plus value in case your hero is canceling attacks)"));

                UserDelay = userDelayMenuItem.GetValue<Slider>().Value;
                userDelayMenuItem.ValueChanged += (o, args) => { UserDelay = args.GetNewValue<Slider>().Value; };
            }

            if (orbwalker == null)
            {
                orbwalker = new Orbwalker(ObjectManager.LocalHero);
            }
            else
            {
                orbwalker.Unit = ObjectManager.LocalHero;
            }
        }

        #endregion
    }
}