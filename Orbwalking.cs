// <copyright file="Orbwalking.cs" company="EnsageSharp">
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
            Load();
        }

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
            return !orbwalker.CanAttack(target as Unit, bonusWindupMs);
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
            return !orbwalker.CanAttack(target, bonusWindupMs);
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

        /// <summary>
        ///     Loads orbwalking if its not loaded yet
        /// </summary>
        public static void Load()
        {
            Events.OnLoad += Events_OnLoad;
            Events.OnClose += Events_OnClose;
            if (Game.IsInGame)
            {
                Events_OnLoad(null, null);
            }
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

        /// <summary>
        ///     The events_ on close.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private static void Events_OnClose(object sender, EventArgs e)
        {
            if (!loaded)
            {
                return;
            }

            loaded = false;
        }

        /// <summary>
        ///     The events_ on load.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private static void Events_OnLoad(object sender, EventArgs e)
        {
            if (loaded)
            {
                return;
            }

            loaded = true;
            orbwalker = new Orbwalker(ObjectManager.LocalHero);
        }

        #endregion
    }
}