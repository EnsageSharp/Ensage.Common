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
            var validTarget = target != null && target.IsValid;
            if (!validTarget)
            {
                return false;
            }
            var turnTime = me.GetTurnTime(target) * 1000;
            return ((LastAttackStart + UnitDatabase.GetAttackRate(me) * 1000 - Game.Ping * 2 - turnTime * 2000)) > tick;
        }

        /// <summary>
        ///     Checks if attack animation can be safely canceled
        /// </summary>
        /// <returns></returns>
        public static bool CanCancelAnimation()
        {
            return tick >= (LastAttackStart + UnitDatabase.GetAttackPoint(me) * 1000);
        }

        /// <summary>
        ///     Orbwalks on given target if they are in range, while moving to mouse position
        /// </summary>
        /// <param name="target"></param>
        /// <param name="bonusWindupMs"></param>
        /// <param name="bonusRange"></param>
        public static void Orbwalk(Unit target, float bonusWindupMs = 0, float bonusRange = 0)
        {
            var canAttack = !AttackOnCooldown(target, bonusWindupMs)
                            && !target.IsValidTarget(
                                me.GetAttackRange() + me.HullRadius + target.HullRadius + bonusRange)
                            && !target.IsAttackImmune() && me.CanAttack() && me.NetworkActivity != (NetworkActivity)1503;
            if (canAttack && Utils.SleepCheck("Orbwalk.Attack"))
            {
                me.Attack(target);
                Utils.Sleep(100, "Orbwalk.Attack");
                return;
            }
            var canCancel = target.Distance2D(me) > (me.GetAttackRange() + me.HullRadius + target.HullRadius + bonusRange)
                            || (CanCancelAnimation() && me.NetworkActivity == (NetworkActivity)1503);
            if (!canCancel || !Utils.SleepCheck("Orbwalk.Move"))
            {
                return;
            }
            me.Move(Game.MousePosition);
            Utils.Sleep(100, "Orbwalk.Move");
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
                if (!Game.IsInGame || me == null)
                {
                    loaded = false;
                    LastAttackStart = 0;
                    lastActivity = 0;
                    Game.OnUpdate -= Game_OnUpdate;
                    me = null;
                }
                return;
            }
            //Console.WriteLine("a");
            tick = Environment.TickCount;
            if (me.NetworkActivity == (NetworkActivity)lastActivity)
            {
                return;
            }
            lastActivity = (float)me.NetworkActivity;
            if (lastActivity == 1503)
            {
                LastAttackStart = tick;
            }
        }

        #endregion
    }
}