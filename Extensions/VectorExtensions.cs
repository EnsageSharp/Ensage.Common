// <copyright file="VectorExtensions.cs" company="EnsageSharp">
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

    using global::SharpDX;

    /// <summary>
    ///     Extension class for Vector2, Vector3 and Vector4 classes of SharpDX.
    /// </summary>
    public static class VectorExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Extension wrapper for Vector2.Distance method
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>The distance between two vectors</returns>
        public static float Distance(this Vector2 v1, Vector2 v2)
        {
            return Vector2.Distance(v1, v2);
        }

        /// <summary>
        ///     Extension wrapper for Vector3.Distance method
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>The distance between two vectors</returns>
        public static float Distance(this Vector3 v1, Vector3 v2)
        {
            return Vector3.Distance(v1, v2);
        }

        /// <summary>
        ///     Extension wrapper for Vector4.Distance method
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>The distance between two vectors</returns>
        public static float Distance(this Vector4 v1, Vector4 v2)
        {
            return Vector4.Distance(v1, v2);
        }

        /// <summary>
        ///     Extension wrapper for Vector2.Distance method
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>The distance between two vectors</returns>
        public static float Distance2D(this Vector3 v1, Vector3 v2)
        {
            return Vector2.Distance(v1.ToVector2(), v2.ToVector2());
        }

        /// <summary>
        ///     Extension wrapper for Vector2.Distance method, to calculate distance between a vector and a unit
        /// </summary>
        /// <param name="vector">Vector</param>
        /// <param name="unit">Unit</param>
        /// <returns>The distance between two vectors</returns>
        public static float Distance2D(this Vector3 vector, Entity unit)
        {
            return unit.Position.Distance2D(vector);
        }

        /// <summary>
        ///     Extension wrapper for Vector2.Distance method
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>The distance between two vectors</returns>
        public static float Distance2D(this Vector4 v1, Vector4 v2)
        {
            return Vector2.Distance(v1.ToVector2(), v2.ToVector2());
        }

        /// <summary>
        ///     Extension wrapper for Vector3.Distance method
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>The distance between two vectors</returns>
        public static float Distance3D(this Vector4 v1, Vector4 v2)
        {
            return Vector3.Distance(v1.ToVector3(), v2.ToVector3());
        }

        /// <summary>
        ///     Calculates the angle between two vectors. The angle represents at which direction the second vector resides in
        ///     compared to first vector.
        ///     Returns degree values on default, radian if requested.
        /// </summary>
        /// <param name="first">First Vector</param>
        /// <param name="second">Second Vector</param>
        /// <param name="radian">Should the format be radian</param>
        /// <returns>Angle between the vectors</returns>
        public static float FindAngleBetween(this Vector2 first, Vector2 second, bool radian = false)
        {
            return (second - first).PolarAngle(radian);
        }

        /// <summary>
        ///     Calculates the angle between two vectors. The angle represents at which direction the second vector resides in
        ///     compared to first vector.
        ///     Returns degree values on default, radian if requested.
        /// </summary>
        /// <param name="first">First Vector</param>
        /// <param name="second">Second Vector</param>
        /// <param name="radian">Should the format be radian</param>
        /// <returns>Angle between the vectors</returns>
        [Obsolete("FindAngleBetween is deprecated for using with Vector3. Please use Vector2 for arguements.")]
        public static float FindAngleBetween(this Vector2 first, Vector3 second, bool radian = false)
        {
            return (second.ToVector2() - first).PolarAngle(radian);
        }

        /// <summary>
        ///     Calculates the angle between a vector and a unit. The angle represents at which direction the unit resides in
        ///     compared to vector.
        ///     Returns degree values on default, radian if requested.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="unit"></param>
        /// <param name="radian">Should the format be radian</param>
        /// <returns>Angle between the vectors</returns>
        public static float FindAngleBetween(this Vector2 first, Entity unit, bool radian = false)
        {
            return first.FindAngleBetween(unit.Position.ToVector2(), radian);
        }

        /// <summary>
        ///     Calculates the angle between a vector and a unit. The angle represents at which direction the unit resides in
        ///     compared to vector.
        ///     Returns degree values on default, radian if requested.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="radian">Should the format be radian</param>
        /// <returns>Angle between the vectors</returns>
        [Obsolete("FindAngleBetween is deprecated for using with Vector3. Please use Vector2 for arguements.")]
        public static float FindAngleBetween(this Vector3 first, Vector2 second, bool radian = false)
        {
            return (second - first.ToVector2()).PolarAngle(radian);
        }

        /// <summary>
        ///     Calculates the angle between a vector and a unit. The angle represents at which direction the unit resides in
        ///     compared to vector.
        ///     Returns degree values on default, radian if requested.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="radian">Should the format be radian</param>
        /// <returns>Angle between the vectors</returns>
        [Obsolete("FindAngleBetween is deprecated for using with Vector3. Please use Vector2 for arguements.")]
        public static float FindAngleBetween(this Vector3 first, Vector3 second, bool radian = false)
        {
            return (second.ToVector2() - first.ToVector2()).PolarAngle(radian);
        }

        /// <summary>
        ///     Constructs a Unit Vector2 from polar angle
        /// </summary>
        /// <param name="polar">Polar angle of the vector</param>
        /// <returns>Constructed Vector2</returns>
        public static Vector2 FromPolarAngle(float polar)
        {
            return new Vector2((float)Math.Cos(polar), (float)Math.Sin(polar));
        }

        /*
		===============================================================================

			Vector2 Extensions

		===============================================================================
		*/

        /// <summary>
        ///     Constructs a Vector2 from polar coordinates.
        /// </summary>
        /// <param name="radial">Radial of the vector</param>
        /// <param name="polar">Polar angle of the vector</param>
        /// <returns>Constructed Vector2</returns>
        public static Vector2 FromPolarCoordinates(float radial, float polar)
        {
            return new Vector2((float)Math.Cos(polar) * radial, (float)Math.Sin(polar) * radial);
        }

        /// <summary>
        ///     Returns true if the vector is valid.
        /// </summary>
        public static bool IsValid(this Vector2 v)
        {
            return v != Vector2.Zero;
        }

        /// <summary>
        ///     Returns true if the vector is valid.
        /// </summary>
        public static bool IsValid(this Vector3 v)
        {
            return v != Vector3.Zero;
        }

        /// <summary>
        ///     Calculates the polar angle of the given vector. Returns degree values on default, radian if requested.
        /// </summary>
        /// <param name="vector">Vector whose polar angle will be returned</param>
        /// <param name="radian">Should the format be radian</param>
        /// <returns>Polar angle of the vector</returns>
        public static float PolarAngle(this Vector2 vector, bool radian = false)
        {
            if (radian)
            {
                return (float)Math.Atan2(vector.Y, vector.X);
            }

            return (float)(Math.Atan2(vector.Y, vector.X) * (180d / Math.PI));
        }

        /// <summary>
        ///     Rotates the given vector by given angle relative to given origin
        /// </summary>
        /// <param name="vector">Vector to be rotated</param>
        /// <param name="delta">Rotation represented in radians</param>
        /// <param name="origin">Origin vector</param>
        /// <returns>The resulting vector after the rotation</returns>
        public static Vector2 Rotate(this Vector2 vector, float delta, Vector2 origin)
        {
            var cos = (float)Math.Cos(delta);
            var sin = (float)Math.Sin(delta);
            var rotationMatrix = new Matrix(cos, sin, 0, 0, sin, cos, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            vector = vector - origin;
            Vector4 result;
            Vector2.Transform(ref vector, ref rotationMatrix, out result);
            return result.ToVector2() + origin;
        }

        /// <summary>
        ///     Rotates the given vector by given angle relative to zero vector.
        /// </summary>
        /// <param name="vector">Vector to be rotated</param>
        /// <param name="delta">Rotation represented in radians</param>
        /// <returns>The resulting vector after the rotation</returns>
        public static Vector2 Rotate(this Vector2 vector, float delta)
        {
            return Rotate(vector, delta, Vector2.Zero);
        }

        /// <summary>
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 SwitchYZ(this Vector3 v)
        {
            return new Vector3(v.X, v.Z, v.Y);
        }

        /*
		===============================================================================

			Vector3 Extensions

		===============================================================================
		*/

        /// <summary>
        ///     Converts the given Vector3 to a Vector2. Disregards Z property.
        /// </summary>
        /// <param name="vector">Vector to be converted</param>
        /// <returns>Converted Vector2</returns>
        public static Vector2 ToVector2(this Vector3 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        /*
		===============================================================================

			Vector4 Extensions

		===============================================================================
		*/

        /// <summary>
        ///     Converts the given Vector4 to a Vector2. Disregards Z and W property.
        /// </summary>
        /// <param name="vector">Vector to be converted</param>
        /// <returns>Converted Vector2</returns>
        public static Vector2 ToVector2(this Vector4 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        /// <summary>
        ///     Converts the Vector2 to Vector3. Populates the Z property of the returned value with ground data if requested.
        /// </summary>
        /// <param name="vector">Source Vector2</param>
        /// <param name="ground">Boolean value representing if the ground position should be found</param>
        /// <returns>Converted Vector3</returns>
        public static Vector3 ToVector3(this Vector2 vector, bool ground = false)
        {
            var returnValue = new Vector3(vector.X, vector.Y, 0f);
            if (ground)
            {
                // TO DO: Ground poistion implementation
            }

            return returnValue;
        }

        /// <summary>
        ///     Converts the given Vector4 to a Vector3. Disregards W property.
        /// </summary>
        /// <param name="vector">Vector to be converted</param>
        /// <returns>Converted Vector3</returns>
        public static Vector3 ToVector3(this Vector4 vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }

        /// <summary>
        ///     Converts the Vector2 to Vector4. Populates the Z property of the returned value with ground data if requested.
        /// </summary>
        /// <param name="vector">Source Vector2</param>
        /// <param name="ground">Boolean value representing if the ground position should be found</param>
        /// <returns>Converted Vector4</returns>
        public static Vector4 ToVector4(this Vector2 vector, bool ground = false)
        {
            var returnValue = new Vector4(vector.X, vector.Y, 0f, 0f);
            if (ground)
            {
                // TO DO: Ground poistion implementation
            }

            return returnValue;
        }

        /// <summary>
        ///     Converts the given Vector3 to a Vector4.
        /// </summary>
        /// <param name="vector">Vector to be converted</param>
        /// <returns>Converted Vector4</returns>
        public static Vector4 ToVector4(this Vector3 vector)
        {
            return new Vector4(vector.X, vector.Y, vector.Z, 0f);
        }

        #endregion
    }
}