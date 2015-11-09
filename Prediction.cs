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

        public static Dictionary<float,Vector3> SpeedDictionary = new Dictionary<float, Vector3>();

        public static Dictionary<float,double> RotSpeedDictionary = new Dictionary<float, double>();

        public static Dictionary<float, float> RotTimeDictionary = new Dictionary<float, float>(); 

        private static Dictionary<float,ParticleEffect> PredictionDrawings = new Dictionary<float, ParticleEffect>(); 

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
            Vector3 targetSpeed;
            if ((!SpeedDictionary.TryGetValue(target.Handle, out targetSpeed) || targetSpeed == new Vector3(0, 0, 0)) && target.NetworkActivity == NetworkActivity.Move)
            {
                var rotSpeed = 0d;
                if (RotSpeedDictionary.ContainsKey(target.Handle))
                {
                    rotSpeed = RotSpeedDictionary[target.Handle];
                }
                targetSpeed = target.Vector3FromPolarAngle((float)rotSpeed) * (target.MovementSpeed) / 1000;
            }
            var a = Math.Pow(targetSpeed.X, 2) + Math.Pow(targetSpeed.Y, 2) - Math.Pow(speed / 1000, 2);
            var b = 2 * (dePos.X * targetSpeed.X + dePos.Y * targetSpeed.Y);
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
            return unit.Modifiers.Any(x => x.Name == "modifier_eul_cyclone" || x.Name == "modifier_invoker_tornado")
                   || (unit.NetworkActivity == NetworkActivity.Idle && !AbilityMove(unit))
                   || unit.Modifiers.Any(x => x.Name == "modifier_invoker_deafening_blast_knockback")
                   || unit.IsAttacking();
        }

        /// <summary>
        ///     Checks if a unit is currently changing their direction
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="tolerancy">tolerancy of rotation speed</param>
        /// <returns></returns>
        public static bool IsTurning(Unit unit, double tolerancy = 0)
        {
            double rotSpeed;
            if (!RotSpeedDictionary.TryGetValue(unit.Handle, out rotSpeed))
            {
                return false;
            }
            return Math.Abs(rotSpeed) > tolerancy;
        }

        /// <summary>
        /// Returns in miliseconds how long is unit walking straight
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static float StraightTime(Unit unit)
        {
            if (!RotTimeDictionary.ContainsKey(unit.Handle))
            {
                return 0;
            }
            if (IsIdle(unit))
            {
                return 5000;
            }
            return Environment.TickCount - RotTimeDictionary[unit.Handle] + Game.Ping;
        }

        /// <summary>
        ///     Returns predicted location of given unit after given delay in ms
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public static Vector3 PredictedXYZ(Unit unit, float delay)
        {
            Vector3 targetSpeed;
            if ((!SpeedDictionary.TryGetValue(unit.Handle, out targetSpeed) || targetSpeed == new Vector3(0,0,0)) && unit.NetworkActivity == NetworkActivity.Move)
            {
                var rotSpeed = 0d;
                if (RotSpeedDictionary.ContainsKey(unit.Handle))
                {
                    rotSpeed = RotSpeedDictionary[unit.Handle];
                }
                targetSpeed = unit.Vector3FromPolarAngle((float)rotSpeed) * (unit.MovementSpeed) / 1000;
            }
            //Console.WriteLine(targetSpeed + " " + unit.Name);
            if (IsIdle(unit))
            {
                return unit.Position;
            }
            var v = unit.Position + targetSpeed * delay;
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
           // Console.WriteLine(IsIdle(target) + " and " + (data == null) + " and " + (data.Speed));
            if (IsIdle(target))
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
            sourcePos = (sourcePos - predict) * (sourcePos.Distance2D(predict) - radius - target.HullRadius)
                        / sourcePos.Distance2D(predict) + predict;
            if (!(speed < 6000))
            {
                return PredictedXYZ(target, delay);
            }
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
                    return; 
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
                    if (!RotTimeDictionary.ContainsKey(unit.Handle))
                    {
                        RotTimeDictionary.Add(unit.Handle,tick);
                    }
                    var speed = (unit.Position - data.LastPosition) / (tick - data.Lasttick);
                    //Console.WriteLine(data.RotSpeed + " rot");
                    if (Math.Abs(data.RotSpeed) > 0.09 && data.Speed != new Vector3(0, 0, 0))
                    {                       
                        RotTimeDictionary[unit.Handle] = tick;
                        data.Speed = unit.Vector3FromPolarAngle(-data.RotSpeed * 10) * (unit.MovementSpeed) / 3000;
                    }
                    else if (StraightTime(unit) < 500)
                    {
                        data.Speed = unit.Vector3FromPolarAngle(-data.RotSpeed * 10) * (unit.MovementSpeed) / 3000;
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
                                var newpredict = unit.Vector3FromPolarAngle(-data.RotSpeed) * (ballSpeed / 1000);
                                data.Speed = newpredict;
                            }
                        }
                        else if (unit.NetworkActivity != NetworkActivity.Move)
                        {
                            data.Speed = speed;
                        }
                        else
                        {
                            var newpredict = unit.Vector3FromPolarAngle(-data.RotSpeed*10) * (unit.MovementSpeed) / 1000;
                            data.Speed = newpredict;
                            //Console.WriteLine("speed" + " " + newpredict + " " + (unit.MovementSpeed / 1000) + " " + unit.Vector3FromPolarAngle(unit.RotationRad + data.RotSpeed) + " " + data.RotSpeed);
                        }
                    }
                    //var predict = unit.Position + data.Speed * 1000;
                    //var realspeed = predict.Distance2D(unit.Position);
                    //if ((realspeed + 100 > unit.MovementSpeed) && unit.NetworkActivity == NetworkActivity.Move)
                    //{
                    //    var newpredict = unit.Vector3FromPolarAngle(data.RotSpeed) * (unit.MovementSpeed) / 1000;
                    //    data.Speed = newpredict;
                    //}
                    data.LastPosition = unit.Position;
                    data.LastRotR = unit.RotationRad;
                    data.Lasttick = tick;
                    if (!SpeedDictionary.ContainsKey(unit.Handle))
                    {
                        SpeedDictionary.Add(unit.Handle,data.Speed);
                    }
                    else
                    {
                        SpeedDictionary[unit.Handle] = data.Speed;
                    }
                    if (!RotSpeedDictionary.ContainsKey(unit.Handle))
                    {
                        RotSpeedDictionary.Add(unit.Handle, data.RotSpeed);
                    }
                    else
                    {
                        RotSpeedDictionary[unit.Handle] = data.RotSpeed;
                    }
                }
            }
        }

        public static void DrawPredictions(float delay = 1000)
        {
            var heroes = ObjectMgr.GetEntities<Hero>().Where(x => !x.IsIllusion);
            foreach (var unit in heroes)
            {
                ParticleEffect effect;
                if (!PredictionDrawings.TryGetValue(unit.Handle, out effect))
                {
                    effect = new ParticleEffect(@"particles\ui_mouseactions\range_display.vpcf", PredictedXYZ(unit, delay));
                    effect.SetControlPoint(1, new Vector3(unit.HullRadius+20, 0, 0));
                    PredictionDrawings.Add(unit.Handle,effect);                   
                }
                effect.SetControlPoint(0, PredictedXYZ(unit, delay));
            }
        }

        #endregion
    }
}