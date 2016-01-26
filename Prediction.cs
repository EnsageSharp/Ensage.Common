// <copyright file="Prediction.cs" company="EnsageSharp">
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
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.Common.Extensions;
    using Ensage.Common.Objects;

    using SharpDX;

    /// <summary>
    /// </summary>
    public class Prediction
    {
        #region Static Fields

        /// <summary>
        /// </summary>
        public static Dictionary<float, double> RotSpeedDictionary = new Dictionary<float, double>();

        /// <summary>
        /// </summary>
        public static Dictionary<float, float> RotTimeDictionary = new Dictionary<float, float>();

        /// <summary>
        /// </summary>
        public static Dictionary<float, Vector3> SpeedDictionary = new Dictionary<float, Vector3>();

        /// <summary>
        /// </summary>
        public static List<Prediction> TrackTable = new List<Prediction>();

        private static bool loaded;

        private static List<Hero> playerList = new List<Hero>();

        private static Dictionary<float, ParticleEffect> predictionDrawings = new Dictionary<float, ParticleEffect>();

        #endregion

        #region Fields

        /// <summary>
        /// </summary>
        public Vector3 LastPosition;

        /// <summary>
        /// </summary>
        public float LastRotR;

        /// <summary>
        /// </summary>
        public float Lasttick;

        /// <summary>
        /// </summary>
        public float RotSpeed;

        /// <summary>
        /// </summary>
        public Vector3 Speed;

        /// <summary>
        /// </summary>
        public ClassID UnitClassId;

        /// <summary>
        /// </summary>
        public string UnitName;

        #endregion

        #region Constructors and Destructors

        static Prediction()
        {
            Events.OnLoad += Events_OnLoad;
            Events.OnClose += Events_OnClose;
            if (Game.IsInGame)
            {
                Events_OnLoad(null, null);
            }
        }

        /// <summary>
        /// </summary>
        public Prediction()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="unitName"></param>
        /// <param name="unitClassId"></param>
        /// <param name="speed"></param>
        /// <param name="rotSpeed"></param>
        /// <param name="lastPosition"></param>
        /// <param name="lastRotR"></param>
        /// <param name="lasttick"></param>
        public Prediction(
            string unitName, 
            ClassID unitClassId, 
            Vector3 speed, 
            float rotSpeed, 
            Vector3 lastPosition, 
            float lastRotR, 
            float lasttick)
        {
            this.UnitName = unitName;
            this.UnitClassId = unitClassId;
            this.Speed = speed;
            this.RotSpeed = rotSpeed;
            this.LastPosition = lastPosition;
            this.LastRotR = lastRotR;
            this.Lasttick = lasttick;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
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

        /// <summary>
        /// </summary>
        /// <param name="target"></param>
        /// <param name="speed"></param>
        /// <param name="dePos"></param>
        /// <returns></returns>
        public static float CalculateReachTime(Unit target, float speed, Vector3 dePos)
        {
            Vector3 targetSpeed;
            if ((!SpeedDictionary.TryGetValue(target.Handle, out targetSpeed) || targetSpeed == new Vector3(0, 0, 0))
                && target.NetworkActivity == NetworkActivity.Move)
            {
                var rotSpeed = 0d;
                if (RotSpeedDictionary.ContainsKey(target.Handle))
                {
                    rotSpeed = RotSpeedDictionary[target.Handle];
                }

                targetSpeed = target.Vector3FromPolarAngle((float)rotSpeed) * target.MovementSpeed / 1000;
            }

            var a = Math.Pow(targetSpeed.X, 2) + Math.Pow(targetSpeed.Y, 2) - Math.Pow(speed / 1000, 2);
            var b = 2 * (dePos.X * targetSpeed.X + dePos.Y * targetSpeed.Y);
            var c = Math.Pow(dePos.X, 2) + Math.Pow(dePos.Y, 2);
            return (float)((-b - Math.Sqrt(Math.Pow(b, 2) - 4 * a * c)) / (2 * a));
        }

        /// <summary>
        /// </summary>
        /// <param name="delay"></param>
        public static void DrawPredictions(float delay = 1000)
        {
            var heroes = Heroes.All.Where(x => !x.IsIllusion);
            foreach (var unit in heroes)
            {
                ParticleEffect effect;
                if (!predictionDrawings.TryGetValue(unit.Handle, out effect))
                {
                    effect = new ParticleEffect(
                        @"particles\ui_mouseactions\range_display.vpcf", 
                        PredictedXYZ(unit, delay));
                    effect.SetControlPoint(1, new Vector3(unit.HullRadius + 20, 0, 0));
                    predictionDrawings.Add(unit.Handle, effect);
                }

                effect.SetControlPoint(0, PredictedXYZ(unit, delay));
            }
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
            // var modifiers = unit.Modifiers;
            return unit.IsInvul() || unit.IsStunned()
                   || (unit.NetworkActivity == NetworkActivity.Idle
                       && (!SpeedDictionary.ContainsKey(unit.Handle) || SpeedDictionary[unit.Handle] == Vector3.Zero))
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
        ///     Returns predicted location of given unit after given delay in ms
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public static Vector3 PredictedXYZ(Unit unit, float delay)
        {
            Vector3 targetSpeed;
            if ((!SpeedDictionary.TryGetValue(unit.Handle, out targetSpeed) || targetSpeed == new Vector3(0, 0, 0))
                && unit.NetworkActivity == NetworkActivity.Move)
            {
                var rotSpeed = 0d;
                if (RotSpeedDictionary.ContainsKey(unit.Handle))
                {
                    rotSpeed = RotSpeedDictionary[unit.Handle];
                }

                targetSpeed = unit.Vector3FromPolarAngle((float)rotSpeed) * unit.MovementSpeed / 1000;
            }

            // Console.WriteLine(targetSpeed + " " + unit.Name);
            if (IsIdle(unit))
            {
                return unit.Position;
            }

            var v = unit.Position + targetSpeed * (float)delay;
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

            if (!(speed < 6000) || speed <= 0)
            {
                return PredictedXYZ(target, delay);
            }

            var predict = PredictedXYZ(target, delay);
            var sourcePos = source.Position;
            var reachTime = CalculateReachTime(target, speed, predict - sourcePos);
            predict = PredictedXYZ(target, delay + reachTime);
            if (!(source.Distance2D(predict) > radius))
            {
                return PredictedXYZ(target, delay + reachTime);
            }

            if (target.MovementSpeed * ((predict.Distance2D(sourcePos) - radius) / speed) < radius)
            {
                sourcePos = (sourcePos - predict) * (sourcePos.Distance2D(predict) - radius)
                            / sourcePos.Distance2D(predict) + predict;
                reachTime = CalculateReachTime(target, speed, predict - sourcePos);
            }
            else
            {
                sourcePos = (sourcePos - predict)
                            * (sourcePos.Distance2D(predict)
                               + target.MovementSpeed * ((predict.Distance2D(sourcePos) - radius) / speed) - radius)
                            / sourcePos.Distance2D(predict) + predict;
                reachTime = CalculateReachTime(target, speed, predict - sourcePos);
            }

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

            if (playerList == null || (playerList.Count < 10 && Utils.SleepCheck("Prediction.SpeedTrack")))
            {
                playerList = Heroes.All;
                Utils.Sleep(1000, "Prediction.SpeedTrack");
            }

            if (!playerList.Any())
            {
                return;
            }

            // DrawPredictions();
            var tick = Environment.TickCount;
            var tempTable = new List<Prediction>(TrackTable);
            foreach (var unit in playerList.Where(x => x.IsValid))
            {
                var data =
                    tempTable.FirstOrDefault(
                        unitData => unitData.UnitName == unit.StoredName() || unitData.UnitClassId == unit.ClassID);
                if (data == null && unit.IsAlive && unit.IsVisible)
                {
                    data = new Prediction(
                        unit.StoredName(), 
                        unit.ClassID, 
                        new Vector3(0, 0, 0), 
                        0, 
                        new Vector3(0, 0, 0), 
                        0, 
                        0);
                    TrackTable.Add(data);
                }

                if (data != null && (!unit.IsAlive || !unit.IsVisible))
                {
                    data.LastPosition = new Vector3(0, 0, 0);
                    data.LastRotR = 0;
                    data.Lasttick = 0;
                    continue;
                }

                if (data == null || (data.LastPosition != new Vector3(0, 0, 0) && !(tick - data.Lasttick > 0)))
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
                        RotTimeDictionary.Add(unit.Handle, tick);
                    }

                    var speed = (unit.Position - data.LastPosition) / (tick - data.Lasttick);

                    // Console.WriteLine(data.RotSpeed + " rot");
                    if (Math.Abs(data.RotSpeed) > 0.0999999 && data.Speed != new Vector3(0, 0, 0))
                    {
                        RotTimeDictionary[unit.Handle] = tick;
                        data.Speed = unit.Vector3FromPolarAngle(-data.RotSpeed * 6) * unit.MovementSpeed / 3000;
                    }
                    else if (StraightTime(unit) < 500)
                    {
                        data.Speed = unit.Vector3FromPolarAngle(-data.RotSpeed * 6) * unit.MovementSpeed / 3000;
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

                                // Console.WriteLine(ballSpeed);
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
                            var newpredict = unit.Vector3FromPolarAngle(-data.RotSpeed * 10) * unit.MovementSpeed / 1000;
                            data.Speed = newpredict;

                            // Console.WriteLine("speed" + " " + newpredict + " " + (unit.MovementSpeed / 1000) + " " + unit.Vector3FromPolarAngle(unit.RotationRad + data.RotSpeed) + " " + data.RotSpeed);
                        }
                    }

                    // var predict = unit.Position + data.Speed * 1000;
                    // var realspeed = predict.Distance2D(unit.Position);
                    // if ((realspeed + 100 > unit.MovementSpeed) && unit.NetworkActivity == NetworkActivity.Move)
                    // {
                    // var newpredict = unit.Vector3FromPolarAngle(data.RotSpeed) * (unit.MovementSpeed) / 1000;
                    // data.Speed = newpredict;
                    // }
                    data.LastPosition = unit.Position;
                    data.LastRotR = unit.RotationRad;
                    data.Lasttick = tick;
                    if (!SpeedDictionary.ContainsKey(unit.Handle))
                    {
                        SpeedDictionary.Add(unit.Handle, data.Speed);
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

            // TrackTable = tempTable;
        }

        /// <summary>
        ///     Returns in miliseconds how long is unit walking straight
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

        #endregion

        #region Methods

        private static void Events_OnClose(object sender, EventArgs e)
        {
            loaded = false;
            Game.OnUpdate -= SpeedTrack;
            playerList = new List<Hero>();
            RotSpeedDictionary = new Dictionary<float, double>();
            RotTimeDictionary = new Dictionary<float, float>();
            SpeedDictionary = new Dictionary<float, Vector3>();
            TrackTable = new List<Prediction>();
            predictionDrawings = new Dictionary<float, ParticleEffect>();
        }

        private static void Events_OnLoad(object sender, EventArgs e)
        {
            if (loaded)
            {
                return;
            }

            loaded = true;
            playerList = new List<Hero>();
            RotSpeedDictionary = new Dictionary<float, double>();
            RotTimeDictionary = new Dictionary<float, float>();
            SpeedDictionary = new Dictionary<float, Vector3>();
            TrackTable = new List<Prediction>();
            predictionDrawings = new Dictionary<float, ParticleEffect>();
            Game.OnUpdate += SpeedTrack;
        }

        #endregion
    }
}