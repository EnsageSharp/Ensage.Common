// <copyright file="Prediction.cs" company="EnsageSharp">
//    Copyright (c) 2017 EnsageSharp.
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
    ///     The prediction.
    /// </summary>
    public class Prediction
    {
        #region Static Fields

        /// <summary>
        ///     The rot speed dictionary.
        /// </summary>
        public static Dictionary<float, double> RotSpeedDictionary = new Dictionary<float, double>();

        /// <summary>
        ///     The rot time dictionary.
        /// </summary>
        public static Dictionary<float, float> RotTimeDictionary = new Dictionary<float, float>();

        /// <summary>
        ///     The speed dictionary.
        /// </summary>
        public static Dictionary<float, Vector3> SpeedDictionary = new Dictionary<float, Vector3>();

        /// <summary>
        ///     The track table.
        /// </summary>
        public static List<Prediction> TrackTable = new List<Prediction>();

        /// <summary>
        ///     The last rot r dictionary.
        /// </summary>
        private static Dictionary<float, float> lastRotRDictionary = new Dictionary<float, float>();

        /// <summary>
        ///     The loaded.
        /// </summary>
        private static bool loaded;

        /// <summary>
        ///     The player list.
        /// </summary>
        private static List<Hero> playerList = new List<Hero>();

        /// <summary>
        ///     The prediction drawings.
        /// </summary>
        private static Dictionary<float, ParticleEffect> predictionDrawings = new Dictionary<float, ParticleEffect>();

        #endregion

        #region Fields

        /// <summary>
        ///     The last position.
        /// </summary>
        public Vector3 LastPosition;

        /// <summary>
        ///     The last rot r.
        /// </summary>
        public float LastRotR;

        /// <summary>
        ///     The lasttick.
        /// </summary>
        public float Lasttick;

        /// <summary>
        ///     The rot speed.
        /// </summary>
        public float RotSpeed;

        /// <summary>
        ///     The speed.
        /// </summary>
        public Vector3 Speed;

        /// <summary>
        ///     The unit class id.
        /// </summary>
        public ClassId UnitClassId;

        /// <summary>
        ///     The unit name.
        /// </summary>
        public string UnitName;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="Prediction" /> class.
        /// </summary>
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
        ///     Initializes a new instance of the <see cref="Prediction" /> class.
        /// </summary>
        public Prediction()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Prediction" /> class.
        /// </summary>
        /// <param name="unitName">
        ///     The unit name.
        /// </param>
        /// <param name="unitClassId">
        ///     The unit class id.
        /// </param>
        /// <param name="speed">
        ///     The speed.
        /// </param>
        /// <param name="rotSpeed">
        ///     The rot speed.
        /// </param>
        /// <param name="lastPosition">
        ///     The last position.
        /// </param>
        /// <param name="lastRotR">
        ///     The last rot r.
        /// </param>
        /// <param name="lasttick">
        ///     The lasttick.
        /// </param>
        public Prediction(
            string unitName,
            ClassId unitClassId,
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
        ///     The ability move.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool AbilityMove(Unit unit)
        {
            return
                unit.HasModifiers(
                    new[]
                        {
                            "modifier_spirit_breaker_charge_of_darkness", "modifier_earth_spirit_boulder_smash",
                            "modifier_earth_spirit_rolling_boulder_caster", "modifier_earth_spirit_geomagnetic_grip",
                            "modifier_spirit_breaker_charge_of_darkness", "modifier_huskar_life_break_charge",
                            "modifier_magnataur_skewer_movement", "modifier_storm_spirit_ball_lightning",
                            "modifier_faceless_void_time_walk", "modifier_mirana_leap", "modifier_slark_pounce"
                        },
                    false);
        }

        /// <summary>
        ///     The calculate reach time.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="speed">
        ///     The speed.
        /// </param>
        /// <param name="dePos">
        ///     The de pos.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
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
        ///     The draw predictions.
        /// </summary>
        /// <param name="delay">
        ///     The delay.
        /// </param>
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
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="distance">
        ///     The distance.
        /// </param>
        /// <returns>
        ///     The <see cref="Vector3" />.
        /// </returns>
        public static Vector3 InFront(Unit unit, float distance)
        {
            var v = unit.Position + unit.Vector3FromPolarAngle() * distance;
            return new Vector3(v.X, v.Y, 0);
        }

        /// <summary>
        ///     Checks if enemy is not moving
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsIdle(Unit unit)
        {
            return unit.NetworkActivity != NetworkActivity.Move;

            // && ((unit.NetworkActivity == NetworkActivity.Idle
            // && (!SpeedDictionary.ContainsKey(unit.Handle) || SpeedDictionary[unit.Handle] == Vector3.Zero))
            // || unit.IsAttacking() || unit.IsInvul() || unit.IsStunned());
        }

        /// <summary>
        ///     Checks if a unit is currently changing their direction
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="tolerancy">
        ///     tolerancy of rotation speed
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
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
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="delay">
        ///     The delay.
        /// </param>
        /// <returns>
        ///     The <see cref="Vector3" />.
        /// </returns>
        public static Vector3 PredictedXYZ(Unit unit, float delay)
        {
            if (IsIdle(unit))
            {
                return unit.Position;
            }

            var targetSpeed = new Vector3();
            if (!lastRotRDictionary.ContainsKey(unit.Handle))
            {
                lastRotRDictionary.Add(unit.Handle, unit.RotationRad);
            }

            var straightTime = StraightTime(unit);
            if (straightTime > 180)
            {
                lastRotRDictionary[unit.Handle] = unit.RotationRad;
            }

            lastRotRDictionary[unit.Handle] = unit.RotationRad;
            if ((unit.ClassId == ClassId.CDOTA_Unit_Hero_StormSpirit || unit.ClassId == ClassId.CDOTA_Unit_Hero_Rubick)
                && unit.HasModifier("modifier_storm_spirit_ball_lightning"))
            {
                var ballLightning = unit.FindSpell("storm_spirit_ball_lightning", true);
                var firstOrDefault =
                    ballLightning.AbilitySpecialData.FirstOrDefault(x => x.Name == "ball_lightning_move_speed");
                if (firstOrDefault != null)
                {
                    var ballSpeed = firstOrDefault.GetValue(ballLightning.Level - 1);
                    var newpredict = unit.Vector3FromPolarAngle() * (ballSpeed / 1000f);
                    targetSpeed = newpredict;
                }
            }
            else
            {
                targetSpeed = unit.Vector3FromPolarAngle() * (unit.MovementSpeed / 1000f);
            }

            var v = unit.Position + targetSpeed * delay;
            return new Vector3(v.X, v.Y, 0);
        }

        /// <summary>
        ///     The skill shot xyz.
        /// </summary>
        /// <param name="source">
        ///     The source.
        /// </param>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="delay">
        ///     The delay.
        /// </param>
        /// <param name="speed">
        ///     The speed.
        /// </param>
        /// <param name="radius">
        ///     The radius.
        /// </param>
        /// <returns>
        ///     The <see cref="Vector3" />.
        /// </returns>
        public static Vector3 SkillShotXYZ(Unit source, Unit target, float delay, float speed, float radius)
        {
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
        /// <param name="args">
        ///     The args.
        /// </param>
        public static void SpeedTrack(EventArgs args)
        {
            if (!Game.IsInGame || Game.IsPaused)
            {
                return;
            }

            var me = ObjectManager.LocalHero;
            if (me == null)
            {
                return;
            }

            if (!Utils.SleepCheck("Prediction.SpeedTrack.Sleep"))
            {
                return;
            }

            if (playerList == null || playerList.Count < 10 && Utils.SleepCheck("Prediction.SpeedTrack"))
            {
                playerList = Heroes.All;
                Utils.Sleep(1000, "Prediction.SpeedTrack");
            }

            if (!playerList.Any())
            {
                return;
            }

            Utils.Sleep(70, "Prediction.SpeedTrack.Sleep");
            var tick = Utils.TickCount;
            var tempTable = new List<Prediction>(TrackTable);
            foreach (var unit in playerList)
            {
                if (!unit.IsValid)
                {
                    continue;
                }

                var data =
                    tempTable.FirstOrDefault(
                        unitData => unitData.UnitName == unit.StoredName() || unitData.UnitClassId == unit.ClassId);
                if (data == null && unit.IsAlive && unit.IsVisible)
                {
                    data = new Prediction(
                        unit.StoredName(),
                        unit.ClassId,
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

                if (data == null || data.LastPosition != new Vector3(0, 0, 0) && !(tick - data.Lasttick > 0))
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

                    if (!lastRotRDictionary.ContainsKey(unit.Handle))
                    {
                        lastRotRDictionary.Add(unit.Handle, unit.RotationRad);
                    }

                    var speed = (unit.Position - data.LastPosition) / (tick - data.Lasttick);
                    if (Math.Abs(data.RotSpeed) > 0.18 && data.Speed != new Vector3(0, 0, 0))
                    {
                        data.Speed = speed;
                        RotTimeDictionary[unit.Handle] = tick;
                    }
                    else
                    {
                        lastRotRDictionary[unit.Handle] = unit.RotationRad;
                        data.Speed = speed;
                    }

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
        }

        /// <summary>
        ///     Returns in miliseconds how long is unit walking straight
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float StraightTime(Unit unit)
        {
            if (!RotTimeDictionary.ContainsKey(unit.Handle))
            {
                return 0;
            }

            return Utils.TickCount - RotTimeDictionary[unit.Handle] + Game.Ping;
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
            loaded = false;
            Events.OnUpdate -= SpeedTrack;
            playerList = new List<Hero>();
            RotSpeedDictionary = new Dictionary<float, double>();
            RotTimeDictionary = new Dictionary<float, float>();
            SpeedDictionary = new Dictionary<float, Vector3>();
            TrackTable = new List<Prediction>();
            predictionDrawings = new Dictionary<float, ParticleEffect>();
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
            playerList = new List<Hero>();
            RotSpeedDictionary = new Dictionary<float, double>();
            RotTimeDictionary = new Dictionary<float, float>();
            SpeedDictionary = new Dictionary<float, Vector3>();
            TrackTable = new List<Prediction>();
            predictionDrawings = new Dictionary<float, ParticleEffect>();
            lastRotRDictionary = new Dictionary<float, float>();
            Events.OnUpdate += SpeedTrack;
        }

        #endregion
    }
}