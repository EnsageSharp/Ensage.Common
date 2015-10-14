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
        public static void Orbwalk(Unit target, float bonusWindupMs = 0, float bonusRange = 0)
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
            var isValid = target != null && target.Distance2D(me) <= (me.GetAttackRange() + me.HullRadius + 50 + targetHull + bonusRange);
            //Console.WriteLine(isValid);
            if (isValid)
            {
                var canAttack = !AttackOnCooldown(target, bonusWindupMs) && !target.IsAttackImmune() && !target.IsInvul() && me.CanAttack();
                if (canAttack && Utils.SleepCheck("Orbwalk.Attack"))
                {
                    me.Attack(target);
                    //Console.WriteLine("attack");
                    Utils.Sleep(100, "Orbwalk.Attack");
                    return;
                }
                //var canCancel = target.Distance2D(me)
                //                > (me.GetAttackRange() + me.HullRadius + target.HullRadius + bonusRange)
                //                || (CanCancelAnimation() && me.NetworkActivity == (NetworkActivity)1503);
            }
            var canCancel = (CanCancelAnimation() && AttackOnCooldown(target, bonusWindupMs))
                            || (!isValid && me.NetworkActivity != (NetworkActivity)1503 && me.NetworkActivity != (NetworkActivity)1505);
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
            Console.WriteLine("aaaa");
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