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

        //public static Dictionary<float[], float> attackData = new Dictionary<float[], float>();

        private static float lastActivity;

        private static bool loaded;

        private static Hero me;

        private static float tick;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Attacks target, uses spell UniqueAttackModifiers if enabled
        /// </summary>
        /// <param name="target"></param>
        /// <param name="useModifiers"></param>
        public static void Attack(Unit target, bool useModifiers)
        {
            if (target is Hero && me.CanCast())
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
                    var poison = me.Spellbook.Spell1;
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
        /// <param name="target"></param>
        /// <param name="bonusWindupMs"></param>
        /// <returns></returns>
        public static bool AttackOnCooldown(Entity target, float bonusWindupMs = 0)
        {
            if (me == null)
            {
                return false;
            }
            var validTarget = target != null;
            if (!validTarget)
            {
                return false;
            }
            var turnTime = me.GetTurnTime(target);
            //Console.WriteLine(turnTime*1000);
            return (LastAttackStart + UnitDatabase.GetAttackRate(me) * 1000 - Game.Ping - turnTime * 1000 - 150
                    + bonusWindupMs) > tick;
        }

        /// <summary>
        ///     Checks if attack animation can be safely canceled
        /// </summary>
        /// <returns></returns>
        public static bool CanCancelAnimation()
        {
            return tick >= (LastAttackStart + UnitDatabase.GetAttackPoint(me) * 1000 - Game.Ping);
        }

        /// <summary>
        ///     Loads orbwalking if its not loaded yet
        /// </summary>
        public static void Load()
        {
            if (loaded)
            {
                return;
            }
            Game.OnUpdate += Game_OnUpdate;
            LastAttackStart = 0;
            lastActivity = 0;
            me = null;
            loaded = true;
        }

        /// <summary>
        ///     Orbwalks on given target if they are in range, while moving to mouse position
        /// </summary>
        /// <param name="target"></param>
        /// <param name="bonusWindupMs"></param>
        /// <param name="bonusRange"></param>
        public static void Orbwalk(Unit target, float bonusWindupMs = 0, float bonusRange = 0, bool attackmodifiers = false)
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
            var isValid = target != null
                          && target.Distance2D(me)
                          <= (me.GetAttackRange() + me.HullRadius + 50 + targetHull + bonusRange);
            //Console.WriteLine(isValid);
            if (isValid)
            {
                var canAttack = !AttackOnCooldown(target, bonusWindupMs) && !target.IsAttackImmune()
                                && !target.IsInvul() && me.CanAttack();
                if (canAttack && Utils.SleepCheck("Orbwalk.Attack"))
                {
                    Attack(target,attackmodifiers);
                    //Console.WriteLine("attack");
                    Utils.Sleep(100, "Orbwalk.Attack");
                    return;
                }
                //var canCancel = target.Distance2D(me)
                //                > (me.GetAttackRange() + me.HullRadius + target.HullRadius + bonusRange)
                //                || (CanCancelAnimation() && me.NetworkActivity == (NetworkActivity)1503);
            }
            var canCancel = (CanCancelAnimation() && AttackOnCooldown(target, bonusWindupMs))
                            || (!isValid && me.NetworkActivity != (NetworkActivity)1503
                                && me.NetworkActivity != (NetworkActivity)1505);
            if (!canCancel || !Utils.SleepCheck("Orbwalk.Move"))
            {
                return;
            }
            //Console.WriteLine("move");            
            me.Move(Game.MousePosition);
            Utils.Sleep(100, "Orbwalk.Move");
        }

        #endregion

        #region Methods

        private static void Game_OnUpdate(EventArgs args)
        {
            if (me == null)
            {
                me = ObjectMgr.LocalHero;
            }
            if (!Game.IsInGame || me == null || Game.IsPaused)
            {
                if (Game.IsInGame && me != null)
                {
                    return;
                }
                loaded = false;
                LastAttackStart = 0;
                lastActivity = 0;
                Game.OnUpdate -= Game_OnUpdate;
                me = null;
                return;
            }
            //Console.WriteLine("a");
            tick = Environment.TickCount;
            if (me.NetworkActivity == (NetworkActivity)lastActivity)
            {
                return;
            }
            lastActivity = (float)me.NetworkActivity;
            if (lastActivity != 1503 && lastActivity != 1505)
            {
                return;
            }
            //Console.WriteLine("aaaa");
            //if (orbwalkTarget != null)
            //{
            //    LastAttackStart = (float)(tick + me.GetTurnTime(orbwalkTarget) * 1000);
            //}
            //else
            //{
            LastAttackStart = tick;
            // }
        }

        #endregion
    }
}