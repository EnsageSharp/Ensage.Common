#region

using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX;

#endregion

namespace Ensage.Common
{
    public class AttackAnimation
    {
        public string UnitName;
        public ClassId UnitClassId;
        public double MoveTime;
        public double EndTime;
        public bool CanMove;

        public AttackAnimation() { }

        public AttackAnimation(string unitName,
            ClassId unitClassId,
            double moveTime,
            double endTime,
            bool canMove)
        {
            UnitName = unitName;
            UnitClassId = unitClassId;
            MoveTime = moveTime;
            EndTime = endTime;
            CanMove = canMove;
        }
    }

    public class AttackAnimationData
    {
        public string UnitName;
        public ClassId UnitClassId;
        public double AttackRate;
        public double AttackPoint;
        public double AttackBackswing;
        public int ProjectileSpeed;
        public double TurnRate;

        public AttackAnimationData() { }

        public AttackAnimationData(string unitName,
            ClassId unitClassId,
            double attackRate,
            double attackPoint,
            double attackBackswing,
            int projectileSpeed,
            double turnRate)
        {
            UnitName = unitName;
            UnitClassId = unitClassId;
            AttackRate = attackRate;
            AttackPoint = attackPoint;
            AttackBackswing = attackBackswing;
            ProjectileSpeed = projectileSpeed;
            TurnRate = turnRate;
        }
    }
    
    public class UnitData
    {

        public static double Count = 0;
        public static double MaxCount = 0;
        public static double StartTime = 0;

        static UnitData()
        {
            Entity.OnIntegerPropertyChange += Entity_OnIntegerPropertyChange;
            Drawing.OnDraw += TrackTick;
            EntityList.OnProjectileAdd += EntityList_OnProjectileAdd;
        }

        public static List<AttackAnimation> AttackAnimation = new List<AttackAnimation>();

        static void EntityList_OnProjectileAdd(EntityListProjectileAddEventArgs args)
        {
            if (!Game.IsInGame || args.Projectile.Source == null || MaxCount < 1)
                return;

            var projectile = args.Projectile;
            var unit = projectile.Source as Unit;
            //Console.WriteLine(unit.Name);
            var data =
                AttackAnimation.FirstOrDefault(
                    unitData => unitData.UnitName == unit.Name || unitData.UnitClassId == unit.ClassId);
            if (data == null)
                return;
            if (data.CanMove)
                return;

            var attackPoint = UnitDatabase.GetAttackPoint(unit);
            var attackRate = UnitDatabase.GetAttackRate(unit);
            data.CanMove = true;
            data.EndTime = Game.GameTime + attackRate - attackPoint;
            //Console.WriteLine("proj");
        }

        public static double FPS()
        {
            return MaxCount;
        }

        public static bool IsInBackswingtime(Unit unit)
        {
            if (MaxCount < 1)
                return false;

            var attackPoint = UnitDatabase.GetAttackPoint(unit);
            //if (attackPoint * 1000 < Game.Ping/2)
            //    return false;
            var data =
                AttackAnimation.FirstOrDefault(
                    unitData => unitData.UnitName == unit.Name || unitData.UnitClassId == unit.ClassId);
            //Console.WriteLine(data);
            return data != null && data.CanMove;
        }

        public static void Entity_OnIntegerPropertyChange(Entity sender, EntityIntegerPropertyChangeEventArgs args)
        {
            if (!Game.IsInGame || Game.IsPaused || args.Property != "m_NetworkActivity" || MaxCount < 1)
                return;


            var unit = sender as Unit;
            var data =
                AttackAnimation.FirstOrDefault(
                    unitData => unitData.UnitName == unit.Name || unitData.UnitClassId == unit.ClassId);
            if (data == null) 
                return;
            var gameTime = Game.GameTime;
            var attackPoint = UnitDatabase.GetAttackPoint(unit);
            var attackRate = UnitDatabase.GetAttackRate(unit);
            //Console.WriteLine(attackPoint + " " + attackRate);
           // Console.WriteLine("{0}  {1}",data.EndTime,gameTime);
            if (args.NewValue == 424 && Math.Abs(data.MoveTime) == 0)
            {
                data.MoveTime = gameTime + attackPoint;
                data.EndTime = gameTime + attackRate;
                // Console.WriteLine(gameTime + " " + data.MoveTime + " " + data.EndTime);
            }
            else if (data.MoveTime > 0 && gameTime > data.MoveTime && !data.CanMove)
            {
                data.CanMove = true;
            }
            else if (data.EndTime > 0 && data.EndTime <= gameTime)
            {
                data.CanMove = false;
                data.MoveTime = 0;
                data.EndTime = 0;
            }
        }

        public static void TrackTick(EventArgs args)
        {
            if (!Game.IsInGame || Game.IsPaused)
                return;
            var me = EntityList.Hero;
            if (me == null) return;
            //Console.WriteLine(me.ClassId);
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
                Count += 1;

            if (MaxCount < 1)
                return;
            //Console.WriteLine(MaxCount);
            var units = EntityList.GetEntities<Unit>();
            foreach (var unit in units)
            {
                var data =
                    AttackAnimation.FirstOrDefault(
                        unitData => unitData.UnitName == unit.Name || unitData.UnitClassId == unit.ClassId);
                if (data == null && unit.IsAlive && unit.IsVisible)
                {
                    //Console.WriteLine(unit.ClassId);
                    data = new AttackAnimation(unit.Name, unit.ClassId, 0, 0, false);
                    AttackAnimation.Add(data);
                }
                if (data != null && (!unit.IsAlive || !unit.IsVisible))
                {
                    AttackAnimation.Remove(data);
                    continue;
                }
                if (data == null)
                    continue;
                if (data.MoveTime > 0 && gameTime > data.MoveTime)
                {
                    data.CanMove = true;
                }
                if (!(data.EndTime <= gameTime && data.EndTime > 0)) continue;
                data.CanMove = false;
                data.MoveTime = 0;
                data.EndTime = 0;
            }
        }

    }
}
