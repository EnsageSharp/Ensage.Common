// <copyright file="Utils.cs" company="EnsageSharp">
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
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Security.Cryptography;
    using System.Security.Permissions;
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
        public static Dictionary<string, double> Sleeps = new Dictionary<string, double>();

        /// <summary>
        ///     The disable modifiers.
        /// </summary>
        private static readonly string[] DisableModifiers =
            {
                "modifier_shadow_demon_disruption",
                "modifier_obsidian_destroyer_astral_imprisonment_prison",
                "modifier_eul_cyclone", "modifier_invoker_tornado",
                "modifier_bane_nightmare",
                "modifier_shadow_shaman_shackles",
                "modifier_crystal_maiden_frostbite",
                "modifier_ember_spirit_searing_chains",
                "modifier_axe_berserkers_call",
                "modifier_lone_druid_spirit_bear_entangle_effect",
                "modifier_meepo_earthbind",
                "modifier_naga_siren_ensnare",
                "modifier_storm_spirit_electric_vortex_pull",
                "modifier_treant_overgrowth", "modifier_cyclone",
                "modifier_sheepstick_debuff",
                "modifier_shadow_shaman_voodoo", "modifier_lion_voodoo",
                "modifier_sheepstick",
                "modifier_brewmaster_storm_cyclone",
                "modifier_puck_phase_shift",
                "modifier_dark_troll_warlord_ensnare",
                "modifier_invoker_deafening_blast_knockback",
                "modifier_pudge_meat_hook"
            };

        /// <summary>
        ///     The key text dictionary.
        /// </summary>
        private static readonly Dictionary<uint, string> KeyCodeDictionary = new Dictionary<uint, string>
                                                                                 {
                                                                                     { 8, "Backspace" }, { 9, "Tab" },
                                                                                     { 13, "Enter" }, { 16, "Shift" },
                                                                                     { 17, "Ctrl" }, { 18, "Alt" },
                                                                                     { 19, "Pause" }, { 20, "CapsLock" },
                                                                                     { 27, "Escape" }, { 32, "Space" },
                                                                                     { 33, "PageUp" },
                                                                                     { 34, "PageDown" }, { 35, "End" },
                                                                                     { 36, "Home" }, { 37, "LeftArrow" },
                                                                                     { 38, "UpArrow" },
                                                                                     { 39, "RightArrow" },
                                                                                     { 40, "DownArrow" },
                                                                                     { 45, "Insert" }, { 46, "Delete" },
                                                                                     { 48, "0" }, { 49, "1" },
                                                                                     { 50, "2" }, { 51, "3" },
                                                                                     { 52, "4" }, { 53, "5" },
                                                                                     { 54, "6" }, { 55, "7" },
                                                                                     { 56, "8" }, { 57, "9" },
                                                                                     { 91, "LeftWindow" },
                                                                                     { 92, "RightWindow" },
                                                                                     { 93, "Select" }, { 96, "Num0" },
                                                                                     { 97, "Num1" }, { 98, "Num2" },
                                                                                     { 99, "Num3" }, { 100, "Num4" },
                                                                                     { 101, "Num5" }, { 102, "Num6" },
                                                                                     { 103, "Num7" }, { 104, "Num8" },
                                                                                     { 105, "Num9" }, { 106, "*" },
                                                                                     { 107, "+" }, { 109, "-" },
                                                                                     { 110, "," }, { 111, "/" },
                                                                                     { 144, "NumLock" },
                                                                                     { 145, "ScrollLock" }, { 186, ";" },
                                                                                     { 187, "=" }, { 188, "," },
                                                                                     { 189, "-" }, { 190, "." },
                                                                                     { 191, "/" }, { 192, "`" },
                                                                                     { 219, "(" }, { 220, "'\'" },
                                                                                     { 221, ")" }, { 222, "'" }
                                                                                 };

        /// <summary>
        ///     The last stun ability.
        /// </summary>
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

        #region Public Properties

        /// <summary>
        ///     Gets the tick count.
        /// </summary>
        public static float TickCount
        {
            get
            {
                if (!Game.IsInGame)
                {
                    return Environment.TickCount & int.MaxValue;
                }

                return Game.RawGameTime * 1000;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Checks if given unit wont be stunned after given delay in seconds.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="delay">
        ///     Delay of possible stun in seconds
        /// </param>
        /// <param name="except">
        ///     Entering a modifier name will ignore that modifier
        /// </param>
        /// <param name="onlychain">
        ///     Entering true will make the function return true only in case enemy is already stunned
        /// </param>
        /// <param name="abilityName">
        ///     The ability Name.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool ChainStun(Unit unit, double delay, string except, bool onlychain, string abilityName = "")
        {
            if (!SleepCheck("CHAINSTUN_SLEEP") && abilityName != lastStunAbility)
            {
                return false;
            }

            lastStunAbility = abilityName;
            var stunned = false;
            var remainingTime = DisableDuration(unit, except);
            var chain = false;
            if (remainingTime > 0)
            {
                stunned = true;
                chain = remainingTime <= delay;
            }

            return (!(stunned || unit.IsStunned()) || chain) && !onlychain || onlychain && chain;
        }

        /// <summary>
        ///     Switches given degrees to radians
        /// </summary>
        /// <param name="angle">
        ///     The angle.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0f;
        }

        /// <summary>
        ///     The disable duration.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="except">
        ///     The except.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float DisableDuration(Unit unit, string except = null)
        {
            Modifier disableModifier = null;
            var maxTime = 0f;
            foreach (var modifier in unit.Modifiers)
            {
                if (
                    !((modifier.IsStunDebuff || DisableModifiers.Contains(modifier.Name))
                      && (except == null || modifier.Name != except)))
                {
                    continue;
                }

                var remainingTime = modifier.RemainingTime;
                if (!(remainingTime > maxTime))
                {
                    continue;
                }

                disableModifier = modifier;
                maxTime = remainingTime;
            }

            if (disableModifier == null)
            {
                return 0;
            }

            if (disableModifier.Name == "modifier_eul_cyclone" || disableModifier.Name == "modifier_invoker_tornado")
            {
                maxTime += 0.07f;
            }

            return maxTime;
        }

        /// <summary>
        ///     The fix virtual key.
        /// </summary>
        /// <param name="key">
        ///     The key.
        /// </param>
        /// <returns>
        ///     The <see cref="byte" />.
        /// </returns>
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
        /// <param name="point">
        ///     The point.
        /// </param>
        /// <param name="x">
        ///     The x.
        /// </param>
        /// <param name="y">
        ///     The y.
        /// </param>
        /// <param name="width">
        ///     The width.
        /// </param>
        /// <param name="height">
        ///     The height.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsUnderRectangle(Vector2 point, float x, float y, float width, float height)
        {
            return point.X > x && point.X < x + width && point.Y > y && point.Y < y + height;
        }

        /// <summary>
        ///     Converts given key code to text
        /// </summary>
        /// <param name="keyCode">
        ///     The v Key.
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public static string KeyToText(uint keyCode)
        {
            /*A-Z */
            if (keyCode >= 65 && keyCode <= 90)
            {
                return ((char)keyCode).ToString();
            }

            /*F1-F12*/
            if (keyCode >= 112 && keyCode <= 123)
            {
                return "F" + (keyCode - 111);
            }

            return KeyCodeDictionary.ContainsKey(keyCode) ? KeyCodeDictionary[keyCode] : keyCode.ToString();
        }

        /// <summary>
        ///     Returns the md5 hash from a string.
        /// </summary>
        /// <param name="s">
        ///     The s.
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
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
        ///     The move camera.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        public static void MoveCamera(Vector3 position)
        {
            Game.ExecuteCommand("dota_camera_set_lookatpos " + position.X + " " + position.Y);
        }

        /// <summary>
        ///     Converts given radian to degrees
        /// </summary>
        /// <param name="angle">
        ///     The angle.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double RadianToDegree(double angle)
        {
            return angle * 180 / Math.PI;
        }

        /// <summary>
        ///     Sleeps the sleeping engine with the given id for given milliseconds. If engine is already sleeping for more than
        ///     the
        ///     given time it will be ignored.
        /// </summary>
        /// <param name="duration">
        ///     The duration.
        /// </param>
        /// <param name="name">
        ///     The name.
        /// </param>
        public static void Sleep(double duration, string name)
        {
            double dur;
            var tick = TickCount;
            if (!Sleeps.TryGetValue(name, out dur) || dur < tick + duration)
            {
                Sleeps[name] = tick + duration;
            }
        }

        /// <summary>
        ///     Checks sleeping status of the sleep engine with given id
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <returns>
        ///     Returns true in case id was not found or is not sleeping
        /// </returns>
        public static bool SleepCheck(string id)
        {
            double time;
            return !Sleeps.TryGetValue(id, out time) || TickCount > time;
        }

        /// <summary>
        ///     Checks sleeping status of the sleep engine with given id
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <param name="remainingTime">
        ///     The remaining time in milliseconds. 0 in case not sleeping.
        /// </param>
        /// <returns>
        ///     Returns true in case id was not found or is not sleeping
        /// </returns>
        public static bool SleepCheck(string id, out double remainingTime)
        {
            double time;
            var found = Sleeps.TryGetValue(id, out time);
            if (!found)
            {
                remainingTime = 0;
                return true;
            }

            remainingTime = time - TickCount;
            return remainingTime > 0;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Convert a byte array to an Object
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="arrBytes">
        ///     The array bytes.
        /// </param>
        /// <returns>
        ///     The <see cref="T" />.
        /// </returns>
        [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
        internal static T Deserialize<T>(byte[] arrBytes)
        {
            var memStream = new MemoryStream();
            var binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            return (T)binForm.Deserialize(memStream);
        }

        /// <summary>
        ///     Convert an object to a byte array
        /// </summary>
        /// <param name="obj">
        ///     The object.
        /// </param>
        /// <returns>
        ///     The <see cref="byte[]" />.
        /// </returns>
        [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
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