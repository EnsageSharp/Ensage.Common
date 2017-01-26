// <copyright file="EntityExtensions.cs" company="EnsageSharp">
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
namespace Ensage.Common.Extensions
{
    using System;
    using System.Collections.Generic;

    using Ensage.Common.Objects;
    using Ensage.Common.Objects.UtilityObjects;

    using global::SharpDX;

    /// <summary>
    ///     The entity extensions.
    /// </summary>
    public static class EntityExtensions
    {
        #region Static Fields

        /// <summary>
        ///     The turn rate dictionary.
        /// </summary>
        private static readonly Dictionary<uint, double> TurnrateDictionary = new Dictionary<uint, double>();

        /// <summary>
        ///     The rotation dictionary.
        /// </summary>
        private static Dictionary<uint, float> rotationDictionary = new Dictionary<uint, float>();

        /// <summary>
        ///     The sleeper.
        /// </summary>
        private static MultiSleeper sleeper = new MultiSleeper();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The closest camp.
        /// </summary>
        /// <param name="entity">
        ///     The entity.
        /// </param>
        /// <returns>
        ///     The <see cref="JungleCamp" />.
        /// </returns>
        public static JungleCamp ClosestCamp(this Entity entity)
        {
            return JungleCamps.FindClosestCamp(entity.Position);
        }

        /// <summary>
        ///     Distance between a entity and a vector
        /// </summary>
        /// <param name="entity">
        ///     The entity.
        /// </param>
        /// <param name="vector">
        ///     The vector.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float Distance2D(this Entity entity, Vector3 vector)
        {
            return entity.Position.Distance2D(vector);
        }

        /// <summary>
        ///     Distance between two entities
        /// </summary>
        /// <param name="entity1">
        ///     The entity 1.
        /// </param>
        /// <param name="entity2">
        ///     The entity 2.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float Distance2D(this Entity entity1, Entity entity2)
        {
            return entity1.Position.Distance2D(entity2.Position);
        }

        /// <summary>
        ///     The distance between projectile and entity.
        /// </summary>
        /// <param name="projectile">
        ///     The projectile.
        /// </param>
        /// <param name="entity">
        ///     The entity.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float Distance2D(this TrackingProjectile projectile, Entity entity)
        {
            return projectile.Position.Distance2D(entity.Position);
        }

        /// <summary>
        ///     The distance between two projectiles
        /// </summary>
        /// <param name="p1">
        ///     The p 1.
        /// </param>
        /// <param name="p2">
        ///     The p 2.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float Distance2D(this TrackingProjectile p1, TrackingProjectile p2)
        {
            return p1.Position.Distance2D(p2.Position);
        }

        /// <summary>
        ///     Angle between a entity and a vector in degrees
        /// </summary>
        /// <param name="entity">
        ///     The entity.
        /// </param>
        /// <param name="vector">
        ///     The vector.
        /// </param>
        /// <param name="radian">
        ///     The radian.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float FindAngleBetween(this Entity entity, Vector3 vector, bool radian = false)
        {
            return entity.Position.ToVector2().FindAngleBetween(vector.ToVector2(), radian);
        }

        /// <summary>
        ///     The find angle for turn time.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="position">
        ///     The position.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float FindAngleForTurnTime(this Entity unit, Vector3 position)
        {
            var first = unit.Position;
            var second = position;
            var xAngle = Utils.RadianToDegree(
                Math.Atan(Math.Abs(position.X - first.X) / Math.Abs(position.Y - first.Y)));
            if (first.X <= second.X && first.Y >= second.Y)
            {
                return (float)(90 - xAngle);
            }

            if (first.X >= second.X && first.Y >= second.Y)
            {
                return (float)(xAngle + 90);
            }

            if (first.X >= second.X && first.Y <= second.Y)
            {
                return (float)(270 - xAngle);
            }

            if (first.X <= second.X && first.Y <= second.Y)
            {
                return (float)(xAngle + 270);
            }

            return 0;
        }

        /// <summary>
        ///     The find angle for turn time.
        /// </summary>
        /// <param name="first">
        ///     The first.
        /// </param>
        /// <param name="position">
        ///     The position.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float FindAngleForTurnTime(this Vector3 first, Vector3 position)
        {
            var second = position;
            var xAngle = Utils.RadianToDegree(
                Math.Atan(Math.Abs(position.X - first.X) / Math.Abs(position.Y - first.Y)));
            if (first.X <= second.X && first.Y >= second.Y)
            {
                return (float)(90 - xAngle);
            }

            if (first.X >= second.X && first.Y >= second.Y)
            {
                return (float)(xAngle + 90);
            }

            if (first.X >= second.X && first.Y <= second.Y)
            {
                return (float)(270 - xAngle);
            }

            if (first.X <= second.X && first.Y <= second.Y)
            {
                return (float)(xAngle + 270);
            }

            return 0;
        }

        /// <summary>
        ///     The find angle r.
        /// </summary>
        /// <param name="entity">
        ///     The entity.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float FindAngleR(this Entity entity)
        {
            var handle = entity.Handle;
            var sleeping = sleeper.Sleeping(handle);
            var rotationRad = sleeping ? rotationDictionary[handle] : entity.RotationRad;
            if (!sleeping)
            {
                rotationDictionary[handle] = rotationRad;
                sleeper.Sleep(handle, 5);
            }

            return (float)(rotationRad < 0 ? Math.Abs(rotationRad) : 2 * Math.PI - rotationRad);
        }

        /// <summary>
        ///     Returns in radians how much can entity turn during given time
        /// </summary>
        /// <param name="entity">
        ///     The entity.
        /// </param>
        /// <param name="time">
        ///     The time.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static double GetTurnAmount(this Entity entity, float time)
        {
            var turnRate = entity.GetTurnRate();
            return time / 1000 * (turnRate * (1 / 0.03));
        }

        /// <summary>
        ///     The get turn rate.
        /// </summary>
        /// <param name="entity">
        ///     The entity.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        [Obsolete("GetTurnRate is deprecated for using with entity. Please use Unit for arguement.")]
        public static double GetTurnRate(this Entity entity)
        {
            double turnRate;
            var handle = entity.Handle;
            if (TurnrateDictionary.TryGetValue(handle, out turnRate))
            {
                return turnRate;
            }

            turnRate = entity is Hero
                           ? Game.FindKeyValues(entity.StoredName() + "/MovementTurnRate", KeyValueSource.Hero)
                               .FloatValue
                           : 0.5;
            TurnrateDictionary.Add(handle, turnRate);
            return turnRate;
        }

        /// <summary>
        ///     Calculates how much time it will take for given entity to turn to given vector
        /// </summary>
        /// <param name="entity">
        ///     The entity.
        /// </param>
        /// <param name="position">
        ///     The position.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        [Obsolete("GetTurnTime is deprecated for using with entity. Please use Unit for arguement.")]
        public static double GetTurnTime(this Entity entity, Vector3 position)
        {
            var unit = entity as Unit;
            if (unit != null)
            {
                return unit.GetTurnTime(position);
            }

            try
            {
                double turnRate;
                var handle = entity.Handle;
                var entityPosition = entity.NetworkPosition;
                if (TurnrateDictionary.TryGetValue(handle, out turnRate))
                {
                    return
                        Math.Max(
                            Math.Abs(
                                FindAngleR(entity) - Utils.DegreeToRadian(entityPosition.FindAngleForTurnTime(position)))
                            - 0.69,
                            0) / (turnRate * (1 / 0.03));
                }

                turnRate = entity is Hero
                               ? Game.FindKeyValues(entity.StoredName() + "/MovementTurnRate", KeyValueSource.Hero)
                                   .FloatValue
                               : 0.5;
                TurnrateDictionary.Add(handle, turnRate);
                return
                    Math.Max(
                        Math.Abs(
                            FindAngleR(entity) - Utils.DegreeToRadian(entityPosition.FindAngleForTurnTime(position)))
                        - 0.69,
                        0) / (turnRate * (1 / 0.03));
            }
            catch (KeyValuesNotFoundException)
            {
                if (!Utils.SleepCheck("Ensage.Common.DemoModeWarning"))
                {
                    return 0;
                }

                Utils.Sleep(10000, "Ensage.Common.DemoModeWarning");
                Console.WriteLine(@"[[Please do not use demo mode for testing assemblies]]");
                return 0;
            }
        }

        /// <summary>
        ///     Calculates how much time it will take for given unit to turn to another unit
        /// </summary>
        /// <param name="entity">
        ///     The unit.
        /// </param>
        /// <param name="entity2">
        ///     The unit 2.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        [Obsolete("GetTurnTime is deprecated for using with entity. Please use Unit for arguement.")]
        public static double GetTurnTime(this Entity entity, Entity entity2)
        {
            return entity.GetTurnTime(entity2.NetworkPosition);
        }

        /// <summary>
        ///     The init.
        /// </summary>
        public static void Init()
        {
            sleeper = new MultiSleeper();
            rotationDictionary = new Dictionary<uint, float>();
        }

        /// <summary>
        ///     The is illusion.
        /// </summary>
        /// <param name="entity">
        ///     The entity.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsIllusion(this Entity entity)
        {
            var unit = entity as Unit;
            return unit != null && unit.IsIllusion;
        }

        /// <summary>
        ///     The spell book.
        /// </summary>
        /// <param name="entity">
        ///     The entity.
        /// </param>
        /// <returns>
        ///     The <see cref="Spellbook" />.
        /// </returns>
        public static Spellbook Spellbook(this Entity entity)
        {
            var unit = entity as Unit;
            return unit != null ? unit.Spellbook : null;
        }

        /// <summary>
        ///     The vector 2 from polar angle.
        /// </summary>
        /// <param name="entity">
        ///     The entity.
        /// </param>
        /// <param name="delta">
        ///     The delta.
        /// </param>
        /// <param name="radial">
        ///     The radial.
        /// </param>
        /// <returns>
        ///     The <see cref="Vector2" />.
        /// </returns>
        public static Vector2 Vector2FromPolarAngle(this Entity entity, float delta = 0f, float radial = 1f)
        {
            var alpha = entity.NetworkRotationRad;
            return VectorExtensions.FromPolarCoordinates(radial, alpha + delta);
        }

        /// <summary>
        ///     The vector 3 from polar angle.
        /// </summary>
        /// <param name="entity">
        ///     The entity.
        /// </param>
        /// <param name="delta">
        ///     The delta.
        /// </param>
        /// <param name="radial">
        ///     The radial.
        /// </param>
        /// <returns>
        ///     The <see cref="Vector3" />.
        /// </returns>
        public static Vector3 Vector3FromPolarAngle(this Entity entity, float delta = 0f, float radial = 1f)
        {
            return Vector2FromPolarAngle(entity, delta, radial).ToVector3();
        }

        #endregion
    }
}