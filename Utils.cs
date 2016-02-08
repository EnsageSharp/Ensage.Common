// <copyright file="Utils.cs" company="EnsageSharp">
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
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Security.Cryptography;
    using System.Text;

    using Ensage.Common.Extensions;

    using SharpDX;

    /// <summary>
    ///     Utility methods
    /// </summary>
    public class Utils
    {
        #region Static Fields

        /// <summary>
        ///     Stores sleep values
        /// </summary>
        public static readonly Dictionary<string, double> Sleeps = new Dictionary<string, double>();

        private static string lastStunAbility;

        #endregion

        #region Enums

        /// <summary>
        ///     Messages called by OnWndProc
        /// </summary>
        public enum WindowsMessages
        {
            /// <summary>
            ///     Left mouse button double-click
            /// </summary>
            WM_LBUTTONDBLCLCK = 0x203, 

            /// <summary>
            ///     Right mouse button double click
            /// </summary>
            WM_RBUTTONDBLCLCK = 0x206, 

            /// <summary>
            ///     Middle mouse button double click
            /// </summary>
            WM_MBUTTONDBLCLCK = 0x209, 

            /// <summary>
            ///     Middle mouse button down
            /// </summary>
            WM_MBUTTONDOWN = 0x207, 

            /// <summary>
            ///     Middle mouse button up
            /// </summary>
            WM_MBUTTONUP = 0x208, 

            /// <summary>
            ///     Mouse being moved
            /// </summary>
            WM_MOUSEMOVE = 0x200, 

            /// <summary>
            ///     Left mouse button down
            /// </summary>
            WM_LBUTTONDOWN = 0x201, 

            /// <summary>
            ///     Left mouse button up
            /// </summary>
            WM_LBUTTONUP = 0x202, 

            /// <summary>
            ///     Right mouse button down
            /// </summary>
            WM_RBUTTONDOWN = 0x204, 

            /// <summary>
            ///     Right mouse button up
            /// </summary>
            WM_RBUTTONUP = 0x205, 

            /// <summary>
            ///     Key down
            /// </summary>
            WM_KEYDOWN = 0x0100, 

            /// <summary>
            ///     Key up
            /// </summary>
            WM_KEYUP = 0x0101
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Checks if given unit wont be stunned after given delay in seconds.
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="delay">Delay of possible stun in seconds</param>
        /// <param name="except">Entering a modifier name will ignore that modifier</param>
        /// <param name="onlychain">Entering true will make the function return true only in case enemy is already stunned</param>
        /// <param name="abilityName"></param>
        /// <returns></returns>
        public static bool ChainStun(Unit unit, double delay, string except, bool onlychain, string abilityName = "")
        {
            if (!SleepCheck("CHAINSTUN_SLEEP") && abilityName != lastStunAbility)
            {
                return false;
            }

            lastStunAbility = abilityName;
            var chain = false;
            var stunned = false;
            string[] modifiersList =
                {
                    "modifier_shadow_demon_disruption", 
                    "modifier_obsidian_destroyer_astral_imprisonment_prison", "modifier_eul_cyclone", 
                    "modifier_invoker_tornado", "modifier_bane_nightmare", 
                    "modifier_shadow_shaman_shackles", "modifier_crystal_maiden_frostbite", 
                    "modifier_ember_spirit_searing_chains", "modifier_axe_berserkers_call", 
                    "modifier_lone_druid_spirit_bear_entangle_effect", "modifier_meepo_earthbind", 
                    "modifier_naga_siren_ensnare", "modifier_storm_spirit_electric_vortex_pull", 
                    "modifier_treant_overgrowth", "modifier_cyclone", "modifier_sheepstick_debuff", 
                    "modifier_shadow_shaman_voodoo", "modifier_lion_voodoo", "modifier_sheepstick", 
                    "modifier_brewmaster_storm_cyclone", "modifier_puck_phase_shift", 
                    "modifier_dark_troll_warlord_ensnare", 
                    "modifier_invoker_deafening_blast_knockback", "modifier_pudge_meat_hook"
                };
            var modifiers = unit.Modifiers.OrderByDescending(x => x.RemainingTime);
            foreach (var m in
                modifiers.Where(
                    m => (m.IsStunDebuff || modifiersList.Contains(m.Name)) && (except == null || m.Name != except)))
            {
                stunned = true;
                var remainingTime = m.RemainingTime;
                if (m.Name == "modifier_eul_cyclone" || m.Name == "modifier_invoker_tornado")
                {
                    remainingTime += 0.07f;
                }

                chain = remainingTime <= delay;
            }

            return ((!(stunned || unit.IsStunned()) || chain) && !onlychain) || (onlychain && chain);
        }

        /// <summary>
        ///     Switches given degrees to radians
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte FixVirtualKey(byte key)
        {
            switch (key)
            {
                case 160:
                    return 0x10;
                case 161:
                    return 0x10;
                case 162:
                    return 0x11;
                case 163:
                    return 0x11;
            }

            return key;
        }

        /// <summary>
        ///     Returns true if the point is under the rectangle
        /// </summary>
        public static bool IsUnderRectangle(Vector2 point, float x, float y, float width, float height)
        {
            return point.X > x && point.X < x + width && point.Y > y && point.Y < y + height;
        }

        /// <summary>
        ///     Converts given key code to text
        /// </summary>
        /// <param name="vKey"></param>
        /// <returns></returns>
        public static string KeyToText(uint vKey)
        {
            /*A-Z */
            if (vKey >= 65 && vKey <= 90)
            {
                return ((char)vKey).ToString();
            }

            /*F1-F12*/
            if (vKey >= 112 && vKey <= 123)
            {
                return "F" + (vKey - 111);
            }

            switch (vKey)
            {
                case 9:
                    return "Tab";
                case 16:
                    return "Shift";
                case 17:
                    return "Ctrl";
                case 20:
                    return "CAPS";
                case 27:
                    return "ESC";
                case 32:
                    return "Space";
                case 45:
                    return "Insert";
                case 220:
                    return "º";
                default:
                    return vKey.ToString();
            }
        }

        /// <summary>
        ///     Returns the md5 hash from a string.
        /// </summary>
        public static string Md5Hash(string s)
        {
            var sb = new StringBuilder();
            HashAlgorithm algorithm = MD5.Create();
            var h = algorithm.ComputeHash(Encoding.UTF8.GetBytes(s));

            foreach (var b in h)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Converts given radian to degrees
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double RadianToDegree(double angle)
        {
            return angle * 180 / Math.PI;
        }

        /// <summary>
        ///     Sleeps the sleeping engine with the given id for given miliseconds. If engine is already sleeping for more than the
        ///     given time it will be ignored.
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="name"></param>
        public static void Sleep(double duration, string name)
        {
            double dur;
            var tick = Environment.TickCount & int.MaxValue;
            if (!Sleeps.TryGetValue(name, out dur) || dur < tick + duration)
            {
                Sleeps[name] = tick + duration;
            }
        }

        /// <summary>
        ///     Checks sleeping status of the sleep engine with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns true in case id was not found or is not sleeping</returns>
        public static bool SleepCheck(string id)
        {
            double asd;
            return !Sleeps.TryGetValue(id, out asd) || (Environment.TickCount & int.MaxValue) > asd;
        }

        #endregion

        #region Methods

        // Convert a byte array to an Object
        internal static T Deserialize<T>(byte[] arrBytes)
        {
            var memStream = new MemoryStream();
            var binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            return (T)binForm.Deserialize(memStream);
        }

        // Convert an object to a byte array
        internal static byte[] Serialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            var bf = new BinaryFormatter();
            var ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        #endregion
    }
}