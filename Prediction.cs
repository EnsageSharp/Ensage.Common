namespace Ensage.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.Common.Extensions;

    using SharpDX;

    public class Prediction
    {
        #region Static Fields

        public static List<Prediction> TrackTable = new List<Prediction>();

        #endregion

        #region Fields

        public Vector3 LastPosition;

        public float LastRotR;

        public float Lasttick;

        public float RotSpeed;

        public Vector3 Speed;

        public ClassID UnitClassID;

        public string UnitName;

        #endregion

        #region Constructors and Destructors

        static Prediction()
        {
            Game.OnUpdate += SpeedTrack;
        }

        public Prediction()
        {
        }

        public Prediction(
            string unitName,
            ClassID unitClassID,
            Vector3 speed,
            float rotSpeed,
            Vector3 lastPosition,
            float lastRotR,
            float lasttick)
        {
            this.UnitName = unitName;
            this.UnitClassID = unitClassID;
            this.Speed = speed;
            this.RotSpeed = rotSpeed;
            this.LastPosition = lastPosition;
            this.LastRotR = lastRotR;
            this.Lasttick = lasttick;
        }

        #endregion

        #region Public Methods and Operators

        public static bool AbilityMove(Unit unit)
        {
            return
                unit.Modifiers.Any(
                    x =>
                    x.Name == "modifier_spirit_breaker_charge_of_darkness"
                    || x.Name == "modifier_earth_spirit_boulder_smash"
                    || x.Name == "modifier_earth_spirit_rolling_boulder_caster"
                    || x.Name == "modifier_earth_spirit_geomagnetic_grip"
                    || x.Name == "modifier_spirit_breaker_charge_of_darkness"
                    || x.Name == "modifier_huskar_life_break_charge" || x.Name == "modifier_magnataur_skewer_movement"
                    || x.Name == "modifier_storm_spirit_ball_lightning" || x.Name == "modifier_faceless_void_time_walk"
                    || x.Name == "modifier_mirana_leap" || x.Name == "modifier_slark_pounce");
        }

        public static float CalculateReachTime(Unit target, float speed, Vector3 dePos)
        {
            var data =
                TrackTable.FirstOrDefault(
                    unitData => unitData.UnitName == target.Name || unitData.UnitClassID == target.ClassID);
            if (data == null)
            {
                return 0;
            }
            var a = Math.Pow(data.Speed.X, 2) + Math.Pow(data.Speed.Y, 2) - Math.Pow(speed / 1000, 2);
            var b = 2 * (dePos.X * data.Speed.X + dePos.Y * data.Speed.Y);
            var c = Math.Pow(dePos.X, 2) + Math.Pow(dePos.Y, 2);
            return (float)((-b - Math.Sqrt(Math.Pow(b, 2) - 4 * a * c)) / (2 * a));
        }

        /// <summary>
        ///     Returns vector in facing direction of given unit with given distance
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static Vector3 InFront(Unit unit, float distance)
        {
            var v = unit.Position + unit.Vector3FromPolarAngle() * distance;
            return new Vector3(v.X, v.Y, 0);
        }

        /// <summary>
        ///     Checks if enemy is not moving
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool IsIdle(Unit unit)
        {
            var data =
                TrackTable.FirstOrDefault(
                    unitData => unitData.UnitName == unit.Name || unitData.UnitClassID == unit.ClassID);
            return (data != null && data.Speed == new Vector3(0, 0, 0))
                   || unit.Modifiers.Any(x => x.Name == "modifier_eul_cyclone" || x.Name == "modifier_invoker_tornado")
                   || (unit.NetworkActivity == NetworkActivity.Idle1 && !AbilityMove(unit)
                       && unit.Modifiers.Any(x => x.Name == "modifier_invoker_deafening_blast_knockback"))
                   || unit.NetworkActivity == NetworkActivity.Attack1;
        }

        /// <summary>
        /// Checks if a unit is currently changing their direction
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="tolerancy">tolerancy of rotation speed</param>
        /// <returns></returns>
        public static bool IsTurning(Unit unit, double tolerancy = 0)
        {
            var data =
                TrackTable.ToArray().FirstOrDefault(
                    unitData => unitData.UnitName == unit.Name || unitData.UnitClassID == unit.ClassID);
            return data != null && Math.Abs(data.RotSpeed) > tolerancy;
        }

        /// <summary>
        ///     Returns predicted location of given unit after given delay in ms
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public static Vector3 PredictedXYZ(Unit unit, float delay)
        {
            var data =
                TrackTable.FirstOrDefault(
                    unitData => unitData.UnitName == unit.Name || unitData.UnitClassID == unit.ClassID);
            if (IsIdle(unit) || data == null)
            {
                return unit.Position;
            }
            var v = unit.Position + data.Speed * delay;
            return new Vector3(v.X, v.Y, 0);
        }

        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="delay"></param>
        /// <param name="speed"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static Vector3 SkillShotXYZ(Unit source, Unit target, float delay, float speed, float radius)
        {
            var data =
                TrackTable.FirstOrDefault(
                    unitData => unitData.UnitName == target.Name || unitData.UnitClassID == target.ClassID);
            if (IsIdle(target) || data == null)
            {
                return target.Position;
            }
            var predict = PredictedXYZ(target, delay);
            var sourcePos = source.Position;
            var reachTime = CalculateReachTime(target, speed, predict - sourcePos);
            predict = PredictedXYZ(target, delay + reachTime);
            if (!(source.Distance2D(target) > radius))
            {
                return PredictedXYZ(target, delay + reachTime);
            }
            sourcePos = (sourcePos - predict) * (sourcePos.Distance2D(predict) - radius - 100)
                        / sourcePos.Distance2D(predict) + predict;
            reachTime = CalculateReachTime(target, speed, predict - sourcePos);
            return PredictedXYZ(target, delay + reachTime);
        }

        /// <summary>
        ///     Tracks heroes movements
        /// </summary>
        /// <param name="args"></param>
        public static void SpeedTrack(EventArgs args)
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

            var heroes = ObjectMgr.GetEntities<Hero>().Where(x => !x.IsIllusion);
            var tick = Environment.TickCount;
            foreach (var unit in heroes)
            {
                var data =
                    TrackTable.FirstOrDefault(
                        unitData => unitData.UnitName == unit.Name || unitData.UnitClassID == unit.ClassID);
                if (data == null && unit.IsAlive && unit.IsVisible)
                {
                    data = new Prediction(unit.Name, unit.ClassID, new Vector3(0, 0, 0), 0, new Vector3(0, 0, 0), 0, 0);
                    TrackTable.Add(data);
                }
                if (data != null && (!unit.IsAlive || !unit.IsVisible))
                {
                    data.LastPosition = new Vector3(0, 0, 0);
                    data.LastRotR = 0;
                    data.Lasttick = 0;
                    continue;
                }
                if (data == null || (data.LastPosition != new Vector3(0, 0, 0) && !((tick - data.Lasttick) > 0)))
                {
                    continue;
                }
                if (data.LastPosition == new Vector3(0, 0, 0))
                {
                    data.LastPosition = unit.Position;
                    data.LastRotR = unit.RotationRad;
                    data.Lasttick = tick;
                }
                else
                {
                    data.RotSpeed = data.LastRotR - unit.RotationRad;
                    var speed = (unit.Position - data.LastPosition) / (tick - data.Lasttick);
                    if (Math.Abs(data.RotSpeed) > 0.05 && data.Speed != new Vector3(0, 0, 0))
                    {
                        data.Speed = unit.Vector3FromPolarAngle(data.RotSpeed) / (tick - data.Lasttick);
                    }
                    else
                    {
                        if (unit.Modifiers.Any(x => x.Name == "modifier_storm_spirit_ball_lightning"))
                        {
                            var ballLightning = unit.FindSpell("storm_spirit_ball_lightning");
                            var firstOrDefault =
                                ballLightning.AbilityData.FirstOrDefault(x => x.Name == "ball_lightning_move_speed");
                            if (firstOrDefault != null)
                            {
                                var ballSpeed = firstOrDefault.GetValue(ballLightning.Level - 1);
                                //Console.WriteLine(ballSpeed);
                                var newpredict = unit.Vector3FromPolarAngle(data.RotSpeed) * (ballSpeed / 1000);
                                data.Speed = newpredict;
                            }
                        }
                        else if (unit.NetworkActivity != NetworkActivity.Move)
                        {
                            data.Speed = speed;
                        }
                        else
                        {
                            var newpredict = unit.Vector3FromPolarAngle(data.RotSpeed) * (unit.MovementSpeed / 1000);
                            data.Speed = newpredict;
                        }
                    }
                    var predict = PredictedXYZ(unit, 1000);
                    var realspeed = predict.Distance2D(unit.Position);
                    if ((realspeed + 100 > unit.MovementSpeed) && unit.NetworkActivity != NetworkActivity.Move)
                    {
                        var newpredict = unit.Vector3FromPolarAngle() * (unit.MovementSpeed / 1000);
                        data.Speed = newpredict;
                    }
                    data.LastPosition = unit.Position;
                    data.LastRotR = unit.RotationRad;
                    data.Lasttick = tick;
                }
            }
        }

        #endregion
    }
}