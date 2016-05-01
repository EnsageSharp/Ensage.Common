// <copyright file="Orbwalking.cs" company="EnsageSharp">
//    Copyright (c) 2015 EnsageSharp.
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

    using Ensage.Common.Extensions;

    /// <summary>
    ///     Class used for Orbwalking stuff
    /// </summary>
    public class Orbwalking
    {
        #region Static Fields

        /// <summary>
        ///     Tick of last attack animation start
        /// </summary>
        public static float LastAttackStart;

        /// <summary>
        ///     The last activity.
        /// </summary>
        private static NetworkActivity lastActivity;

        /// <summary>
        ///     The loaded.
        /// </summary>
        private static bool loaded;

        /// <summary>
        ///     The me.
        /// </summary>
        private static Hero me;

        /// <summary>
        ///     The next attack end.
        /// </summary>
        private static float nextAttackEnd;

        /// <summary>
        ///     The next attack release.
        /// </summary>
        private static float nextAttackRelease;

        /// <summary>
        ///     The tick.
        /// </summary>
        private static float tick;

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
            if (target is Hero && me.CanCast() && useModifiers)
            {
                if (me.ClassID == ClassID.CDOTA_Unit_Hero_Clinkz)
                {
                    var searinga = me.Spellbook.Spell2;
                    if (searinga.Level > 0 && me.Mana > searinga.ManaCost)
                    {
                        searinga.UseAbility(target);
                        return;
                    }
                }
                else if (me.ClassID == ClassID.CDOTA_Unit_Hero_DrowRanger && !target.IsMagicImmune())
                {
                    var frost = me.Spellbook.Spell1;
                    if (frost.Level > 0 && me.Mana > frost.ManaCost)
                    {
                        frost.UseAbility(target);
                        return;
                    }
                }
                else if (me.ClassID == ClassID.CDOTA_Unit_Hero_Viper && !target.IsMagicImmune())
                {
                    var poison = me.Spellbook.SpellQ;
                    if (poison.Level > 0 && me.Mana > poison.ManaCost)
                    {
                        poison.UseAbility(target);
                        return;
                    }
                }
                else if (me.ClassID == ClassID.CDOTA_Unit_Hero_Huskar && !target.IsMagicImmune())
                {
                    var burning = me.Spellbook.Spell2;
                    if (burning.Level > 0 && me.Health > me.MaximumHealth * 0.35)
                    {
                        burning.UseAbility(target);
                        return;
                    }
                }
                else if (me.ClassID == ClassID.CDOTA_Unit_Hero_Silencer && !target.IsMagicImmune())
                {
                    var glaives = me.Spellbook.Spell2;
                    if (glaives.Level > 0 && me.Mana > glaives.ManaCost)
                    {
                        glaives.UseAbility(target);
                        return;
                    }
                }
                else if (me.ClassID == ClassID.CDOTA_Unit_Hero_Jakiro && !target.IsMagicImmune())
                {
                    var liquid = me.Spellbook.Spell3;
                    if (liquid.Level > 0 && liquid.CanBeCasted())
                    {
                        liquid.UseAbility(target);
                        return;
                    }
                }
                else if (me.ClassID == ClassID.CDOTA_Unit_Hero_Obsidian_Destroyer && !target.IsMagicImmune())
                {
                    var arcane = me.Spellbook.Spell1;
                    if (arcane.Level > 0 && me.Mana > arcane.ManaCost)
                    {
                        arcane.UseAbility(target);
                        return;
                    }
                }
                else if (me.ClassID == ClassID.CDOTA_Unit_Hero_Enchantress && !target.IsMagicImmune())
                {
                    var impetus = me.Spellbook.Spell4;
                    if (impetus.Level > 0 && me.Mana > impetus.ManaCost)
                    {
                        impetus.UseAbility(target);
                        return;
                    }
                }
            }

            me.Attack(target);
        }

        /// <summary>
        ///     Checks if attack is currently on cooldown
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="bonusWindupMs">
        ///     The bonus Windup Ms.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool AttackOnCooldown(Entity target = null, float bonusWindupMs = 0)
        {
            if (me == null)
            {
                return false;
            }

            var turnTime = 0d;
            if (target != null)
            {
                turnTime = me.GetTurnTime(target)
                           + (Math.Max(me.Distance2D(target) - me.GetAttackRange() - 100, 0) / me.MovementSpeed);
            }

            return nextAttackEnd - Game.Ping - (turnTime * 1000) - 75 + bonusWindupMs >= tick;
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
            var time = tick;
            var cancelTime = nextAttackRelease - Game.Ping - delay + 50;
            return time >= cancelTime;
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
            if (me == null)
            {
                return;
            }

            var targetHull = 0f;
            if (target != null)
            {
                targetHull = target.HullRadius;
            }

            float distance = 0;
            if (target != null)
            {
                var pos = Prediction.InFront(
                    me, 
                    (float)((Game.Ping / 1000) + (me.GetTurnTime(target.Position) * me.MovementSpeed)));
                distance = pos.Distance2D(target) - me.Distance2D(target);
            }

            var isValid = target != null && target.IsValid && target.IsAlive && target.IsVisible && !target.IsInvul()
                          && !target.HasModifiers(
                              new[] { "modifier_ghost_state", "modifier_item_ethereal_blade_slow" }, 
                              false)
                          && target.Distance2D(me)
                          <= me.GetAttackRange() + me.HullRadius + 50 + targetHull + bonusRange + Math.Max(distance, 0);
            if (isValid || (target != null && me.IsAttacking() && me.GetTurnTime(target.Position) < 0.1))
            {
                var canAttack = !AttackOnCooldown(target, bonusWindupMs) && !target.IsAttackImmune()
                                && !target.IsInvul() && me.CanAttack();
                if (canAttack && Utils.SleepCheck("Orbwalk.Attack"))
                {
                    Attack(target, attackmodifiers);
                    Utils.Sleep(
                        (UnitDatabase.GetAttackPoint(me) * 1000) + (me.GetTurnTime(target) * 1000) + Game.Ping + 100, 
                        "Orbwalk.Attack");
                    Utils.Sleep(
                        (UnitDatabase.GetAttackPoint(me) * 1000) + (me.GetTurnTime(target) * 1000) + 50, 
                        "Orbwalk.Move");
                    return;
                }

                if (canAttack)
                {
                    Attack(target, attackmodifiers);
                    return;
                }
            }

            var canCancel = (CanCancelAnimation() && AttackOnCooldown(target, bonusWindupMs))
                            || (!isValid && !me.IsAttacking() && CanCancelAnimation());
            if (!canCancel || !Utils.SleepCheck("Orbwalk.Move") || !Utils.SleepCheck("Orbwalk.Attack"))
            {
                return;
            }

            if (followTarget)
            {
                me.Follow(target);
            }
            else
            {
                me.Move(Game.MousePosition);
            }

            Utils.Sleep(100, "Orbwalk.Move");
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

            Drawing.OnDraw -= Game_OnUpdate;
            LastAttackStart = 0;
            lastActivity = 0;
            me = null;
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

            Drawing.OnDraw += Game_OnUpdate;
            LastAttackStart = 0;
            lastActivity = 0;
            me = null;
            loaded = true;
        }

        /// <summary>
        ///     The game_ on update.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private static void Game_OnUpdate(EventArgs args)
        {
            if (me == null)
            {
                me = ObjectManager.LocalHero;
            }

            if (!Game.IsInGame || me == null || Game.IsPaused)
            {
                if (Game.IsInGame && me != null)
                {
                    return;
                }

                LastAttackStart = 0;
                nextAttackEnd = 0;
                lastActivity = 0;
                nextAttackRelease = 0;
                me = null;
                return;
            }

            tick = Environment.TickCount & int.MaxValue;
            if (me.NetworkActivity == lastActivity)
            {
                return;
            }

            lastActivity = me.NetworkActivity;
            if (!me.IsAttacking())
            {
                if (CanCancelAnimation())
                {
                    return;
                }

                LastAttackStart = 0;
                lastActivity = 0;
                nextAttackEnd = 0;
                nextAttackRelease = 0;
                return;
            }

            LastAttackStart = tick;
            nextAttackEnd = (float)(tick + (UnitDatabase.GetAttackRate(me) * 1000));
            nextAttackRelease = (float)(tick + (UnitDatabase.GetAttackPoint(me) * 1000));
        }

        #endregion
    }
}