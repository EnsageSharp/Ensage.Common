namespace Ensage.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UnitData
    {
        #region Static Fields

        public static List<AttackAnimation> AttackAnimation = new List<AttackAnimation>();

        public static double Count = 0;

        public static double MaxCount = 0;

        public static double StartTime = 0;

        #endregion

        #region Constructors and Destructors

        static UnitData()
        {
            //Entity.OnIntegerPropertyChange += Entity_OnIntegerPropertyChange;
            Drawing.OnDraw += TrackTick;
            //ObjectMgr.OnProjectileAdd += ObjectMgr_OnProjectileAdd;
        }

        #endregion

        #region Public Methods and Operators

        //static void ObjectMgr_OnProjectileAdd(ObjectMgrProjectileAddEventArgs args)
        //{
        //    if (!Game.IsInGame || args.Projectile.Source == null || MaxCount < 1)
        //        return;

        //    var projectile = args.Projectile;
        //    var unit = projectile.Source as Unit;
        //    //Console.WriteLine(unit.Name);
        //    var data =
        //        AttackAnimation.FirstOrDefault(
        //            unitData => unitData.UnitName == unit.Name || unitData.UnitClassID == unit.ClassID);
        //    if (data == null)
        //        return;
        //    if (data.CanMove)
        //        return;

        //    var attackPoint = UnitDatabase.GetAttackPoint(unit);
        //    var attackRate = UnitDatabase.GetAttackRate(unit);
        //    data.CanMove = true;
        //    data.EndTime = Game.GameTime + attackRate - attackPoint;
        //    //Console.WriteLine("proj");
        //}

        public static double FPS()
        {
            return MaxCount;
        }

        public static bool IsInBackswingtime(Unit unit)
        {
            if (MaxCount < 1)
            {
                return false;
            }

            var attackPoint = UnitDatabase.GetAttackPoint(unit);
            //if (attackPoint * 1000 < Game.Ping/2)
            //    return false;
            var data =
                AttackAnimation.FirstOrDefault(
                    unitData => unitData.UnitName == unit.Name || unitData.UnitClassId == unit.ClassID);
            //Console.WriteLine(data);
            return data != null && data.CanMove;
        }

        //public static void Entity_OnIntegerPropertyChange(Entity sender, EntityIntegerPropertyChangeEventArgs args)
        //{
        //    if (!Game.IsInGame || Game.IsPaused || args.Property != "m_NetworkActivity" || MaxCount < 1)
        //        return;

        //    var unit = sender as Unit;
        //    var data =
        //        AttackAnimation.FirstOrDefault(
        //            unitData => unitData.UnitName == unit.Name || unitData.UnitClassID == unit.ClassID);
        //    if (data == null) 
        //        return;
        //    var gameTime = Game.GameTime;
        //    var attackPoint = UnitDatabase.GetAttackPoint(unit);
        //    var attackRate = UnitDatabase.GetAttackRate(unit);
        //    //Console.WriteLine(attackPoint + " " + attackRate);
        //   // Console.WriteLine("{0}  {1}",data.EndTime,gameTime);
        //    if (args.NewValue == 424 && Math.Abs(data.MoveTime) == 0)
        //    {
        //        data.MoveTime = gameTime + attackPoint;
        //        data.EndTime = gameTime + attackRate;
        //        // Console.WriteLine(gameTime + " " + data.MoveTime + " " + data.EndTime);
        //    }
        //    else if (data.MoveTime > 0 && gameTime > data.MoveTime && !data.CanMove)
        //    {
        //        data.CanMove = true;
        //    }
        //    else if (data.EndTime > 0 && data.EndTime <= gameTime)
        //    {
        //        data.CanMove = false;
        //        data.MoveTime = 0;
        //        data.EndTime = 0;
        //    }
        //}

        public static void TrackTick(EventArgs args)
        {
            if (!Game.IsInGame || Game.IsPaused)
            {
                return;
            }
            var me = ObjectMgr.LocalHero;
            if (me == null)
            {
                return;
            }
            //Console.WriteLine(me.ClassID);
            var gameTime = Game.GameTime;
            var tick = Environment.TickCount;
            if (StartTime == 0)
            {
                StartTime = gameTime;
            }
            else if (gameTime - StartTime >= 1)
            {
                StartTime = gameTime;
                MaxCount = Count;
                Count = 0;
            }
            else
            {
                Count += 1;
            }

            if (MaxCount < 1)
            {
                return;
            }
            //Console.WriteLine(MaxCount);
            var units = ObjectMgr.GetEntities<Unit>();
            foreach (var unit in units)
            {
                var data =
                    AttackAnimation.FirstOrDefault(
                        unitData => unitData.UnitName == unit.Name || unitData.UnitClassId == unit.ClassID);
                if (data == null && unit.IsAlive && unit.IsVisible)
                {
                    //Console.WriteLine(unit.ClassID);
                    data = new AttackAnimation(unit.Name, unit.ClassID, 0, 0, false);
                    AttackAnimation.Add(data);
                }
                if (data != null && (!unit.IsAlive || !unit.IsVisible))
                {
                    AttackAnimation.Remove(data);
                    continue;
                }
                if (data == null)
                {
                    continue;
                }
                if (data.MoveTime > 0 && gameTime > data.MoveTime)
                {
                    data.CanMove = true;
                }
                if (!(data.EndTime <= gameTime && data.EndTime > 0))
                {
                    continue;
                }
                data.CanMove = false;
                data.MoveTime = 0;
                data.EndTime = 0;
            }
        }

        #endregion
    }
}