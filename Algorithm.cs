// <copyright file="Algorithm.cs" company="EnsageSharp">
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

    using SharpDX;

    /// <summary>
    ///     Static class for holding general algorithm implementations
    /// </summary>
    public static class Algorithm
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Finds a circle enclosing the maximum possible number of given vectors, with their weights.
        /// </summary>
        /// <param name="vector">List of the Tuples containing vectors and weight of the vector.</param>
        /// <param name="radius">Radius of the enclosing circle</param>
        /// <exception cref="System.ArgumentException">Thrown when vector list is empty or radius is negative</exception>
        /// <returns>Returns the center of the resulting circle</returns>
        public static Vector2 MaximalEnclosingCircle(List<Tuple<Vector2, uint>> vector, float radius)
        {
            if (radius < 0)
            {
                throw new ArgumentException("Number must be non-negative", "radius");
            }

            if (!vector.Any())
            {
                throw new ArgumentException("Vector list can not be empty", "vector");
            }

            var returnValue = Vector2.Zero;
            uint contain = 0;

            foreach (var tuple1 in vector)
            {
                var point1 = tuple1.Item1;
                foreach (var tuple2 in vector)
                {
                    var point2 = tuple2.Item1;
                    if (point1 == point2)
                    {
                        continue;
                    }

                    var distance = Vector2.Distance(point1, point2);

                    if (Math.Abs(distance - radius) < 0.0001f)
                    {
                        var center = point1 + (point2 - point1) / 2;
                        MaximalEnclosingCircleCompare(vector, radius, center, ref returnValue, ref contain);
                    }
                    else if (distance < radius)
                    {
                        var center = Vector2.Lerp(point1, point2, 0.5f);
                        var unit = point2 - point1;
                        unit.Normalize();
                        var unitClockWise = unit.Rotate((float)Math.PI / 2);
                        var unitCounterClockWise = unit.Rotate((float)Math.PI / -2);
                        var delta = (float)Math.Sqrt(Math.Pow(radius, 2f) - Math.Pow(distance, 2f));
                        MaximalEnclosingCircleCompare(
                            vector,
                            radius,
                            center + delta * unitClockWise,
                            ref returnValue,
                            ref contain);
                        MaximalEnclosingCircleCompare(
                            vector,
                            radius,
                            center + delta * unitCounterClockWise,
                            ref returnValue,
                            ref contain);
                    }
                }
            }

            if (contain == 0)
            {
                returnValue = vector[0].Item1;
            }

            return returnValue;
        }

        /// <summary>
        ///     Finds a circle enclosing the maximum possible number of given vectors.
        /// </summary>
        /// <param name="vector">List of the vectors.</param>
        /// <param name="radius">Radius of the enclosing circle</param>
        /// <exception cref="System.ArgumentException">Thrown when vector list is empty or radius is negative</exception>
        /// <returns>Returns the center of the resulting circle</returns>
        public static Vector2 MaximalEnclosingCircle(List<Vector2> vector, float radius)
        {
            var tupleList = vector.Select(vec => new Tuple<Vector2, uint>(vec, 1)).ToList();
            return MaximalEnclosingCircle(tupleList, radius);
        }

        #endregion

        #region Methods

        private static void MaximalEnclosingCircleCompare(
            List<Tuple<Vector2, uint>> vector,
            float radius,
            Vector2 candidate,
            ref Vector2 oldCenter,
            ref uint oldContain)
        {
            var containList = vector.FindAll(x => Vector2.Distance(x.Item1, candidate) < radius);
            var sum = containList.Aggregate<Tuple<Vector2, uint>, uint>(0, (current, tuple) => current + tuple.Item2);
            if (oldContain >= sum)
            {
                return;
            }

            oldCenter = candidate;
            oldContain = sum;
        }

        #endregion
    }
}