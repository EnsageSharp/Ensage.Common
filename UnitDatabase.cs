// <copyright file="UnitDatabase.cs" company="EnsageSharp">
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
    using System.Security.Permissions;
    using System.Text;

    using Ensage.Common.Extensions;
    using Ensage.Common.Objects;
    using Ensage.Common.Properties;
    using Ensage.Common.UnitData;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    ///     The unit database.
    /// </summary>
    public static class UnitDatabase
    {
        #region Static Fields

        /// <summary>
        ///     The attack point dictionary.
        /// </summary>
        private static readonly Dictionary<uint, double> AttackPointDictionary = new Dictionary<uint, double>();

        /// <summary>
        ///     The attack rate dictionary.
        /// </summary>
        private static readonly Dictionary<uint, double> AttackRateDictionary = new Dictionary<uint, double>();

        /// <summary>
        ///     The projectile speed dictionary.
        /// </summary>
        private static readonly Dictionary<uint, double> ProjSpeedDictionary = new Dictionary<uint, double>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="UnitDatabase" /> class.
        /// </summary>
        [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
        static UnitDatabase()
        {
            JToken @object;
            if (JObject.Parse(Encoding.Default.GetString(Resources.UnitDatabase)).TryGetValue("Units", out @object))
            {
                Units = JsonConvert.DeserializeObject<AttackAnimationData[]>(@object.ToString()).ToList();
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the units.
        /// </summary>
        public static List<AttackAnimationData> Units { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Gets the attack backswing.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double GetAttackBackswing(Hero unit)
        {
            var attackRate = GetAttackRate(unit);
            var attackPoint = GetAttackPoint(unit);
            return attackRate - attackPoint;
        }

        /// <summary>
        ///     Gets the attack backswing.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double GetAttackBackswing(Unit unit)
        {
            var attackRate = GetAttackRate(unit);
            var attackPoint = GetAttackPoint(unit);
            return attackRate - attackPoint;
        }

        /// <summary>The get attack point.</summary>
        /// <param name="unit">The unit.</param>
        /// <returns>The <see cref="double" />.</returns>
        public static double GetAttackPoint(Hero unit)
        {
            return GetAttackPoint(unit, GetAttackSpeed(unit));
        }

        /// <summary>Gets the attack point.</summary>
        /// <param name="unit">The unit.</param>
        /// <param name="attackSpeedValue">The attack Speed Value.</param>
        /// <returns>The <see cref="double" />.</returns>
        public static double GetAttackPoint(Hero unit, float attackSpeedValue)
        {
            if (unit == null)
            {
                return 0;
            }

            try
            {
                var name = unit.StoredName();
                double attackAnimationPoint;
                if (!AttackPointDictionary.TryGetValue(unit.Handle, out attackAnimationPoint))
                {
                    attackAnimationPoint =
                        Game.FindKeyValues(name + "/AttackAnimationPoint", KeyValueSource.Hero).FloatValue;
                    AttackPointDictionary.Add(unit.Handle, attackAnimationPoint);
                }

                return attackAnimationPoint / (1 + (attackSpeedValue - 100) / 100);
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

        /// <summary>The get attack point.</summary>
        /// <param name="unit">The unit.</param>
        /// <returns>The <see cref="double" />.</returns>
        public static double GetAttackPoint(Unit unit)
        {
            return GetAttackPoint(unit, GetAttackSpeed(unit));
        }

        /// <summary>Gets the attack point.</summary>
        /// <param name="unit">The unit.</param>
        /// <param name="attackSpeedValue">The attack Speed Value.</param>
        /// <returns>The <see cref="double" />.</returns>
        public static double GetAttackPoint(Unit unit, float attackSpeedValue)
        {
            var hero = unit as Hero;
            if (hero != null)
            {
                return GetAttackPoint(hero, attackSpeedValue);
            }

            if (unit == null)
            {
                return 0;
            }

            try
            {
                var name = unit.StoredName();
                double attackAnimationPoint;
                if (!AttackPointDictionary.TryGetValue(unit.Handle, out attackAnimationPoint))
                {
                    attackAnimationPoint =
                        Game.FindKeyValues(name + "/AttackAnimationPoint", KeyValueSource.Unit).FloatValue;
                    AttackPointDictionary.Add(unit.Handle, attackAnimationPoint);
                }

                return attackAnimationPoint / (1 + (attackSpeedValue - 100) / 100);
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

        /// <summary>The get attack rate.</summary>
        /// <param name="unit">The unit.</param>
        /// <returns>The <see cref="double" />.</returns>
        public static double GetAttackRate(Hero unit)
        {
            return GetAttackRate(unit, GetAttackSpeed(unit));
        }

        /// <summary>Gets the attack rate.</summary>
        /// <param name="unit">The unit.</param>
        /// <param name="attackSpeedValue">The attack Speed Value.</param>
        /// <returns>The <see cref="double" />.</returns>
        public static double GetAttackRate(Hero unit, float attackSpeedValue)
        {
            try
            {
                double attackBaseTime;
                if (!AttackRateDictionary.TryGetValue(unit.Handle, out attackBaseTime))
                {
                    attackBaseTime =
                        Game.FindKeyValues(unit.StoredName() + "/AttackRate", KeyValueSource.Hero).FloatValue;
                    AttackRateDictionary.Add(unit.Handle, attackBaseTime);
                }

                Ability spell = null;
                if (
                    !unit.HasModifiers(
                        new[]
                            {
                                "modifier_alchemist_chemical_rage", "modifier_terrorblade_metamorphosis",
                                "modifier_lone_druid_true_form", "modifier_troll_warlord_berserkers_rage"
                            },
                        false))
                {
                    return attackBaseTime / (1 + (attackSpeedValue - 100) / 100);
                }

                switch (unit.ClassId)
                {
                    case ClassId.CDOTA_Unit_Hero_Alchemist:
                        spell = unit.Spellbook.Spells.First(x => x.StoredName() == "alchemist_chemical_rage");
                        break;
                    case ClassId.CDOTA_Unit_Hero_Terrorblade:
                        spell = unit.Spellbook.Spells.First(x => x.StoredName() == "terrorblade_metamorphosis");
                        break;
                    case ClassId.CDOTA_Unit_Hero_LoneDruid:
                        spell = unit.Spellbook.Spells.First(x => x.StoredName() == "lone_druid_true_form");
                        break;
                    case ClassId.CDOTA_Unit_Hero_TrollWarlord:
                        spell = unit.Spellbook.Spells.First(x => x.StoredName() == "troll_warlord_berserkers_rage");
                        break;
                }

                if (spell == null)
                {
                    return attackBaseTime / (1 + (attackSpeedValue - 100) / 100);
                }

                attackBaseTime = spell.GetAbilityData("base_attack_time");

                return attackBaseTime / (1 + (attackSpeedValue - 100) / 100);
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

        /// <summary>The get attack rate.</summary>
        /// <param name="unit">The unit.</param>
        /// <returns>The <see cref="double" />.</returns>
        public static double GetAttackRate(Unit unit)
        {
            return GetAttackRate(unit, GetAttackSpeed(unit));
        }

        /// <summary>Gets the attack rate.</summary>
        /// <param name="unit">The unit.</param>
        /// <param name="attackSpeedValue">The attack Speed Value.</param>
        /// <returns>The <see cref="double" />.</returns>
        public static double GetAttackRate(Unit unit, float attackSpeedValue)
        {
            var hero = unit as Hero;
            if (hero != null)
            {
                return GetAttackRate(hero, attackSpeedValue);
            }

            try
            {
                double attackBaseTime;
                if (AttackRateDictionary.TryGetValue(unit.Handle, out attackBaseTime))
                {
                    return attackBaseTime / (1 + (attackSpeedValue - 100) / 100);
                }

                attackBaseTime = Game.FindKeyValues(unit.StoredName() + "/AttackRate", KeyValueSource.Unit).FloatValue;
                AttackRateDictionary.Add(unit.Handle, attackBaseTime);

                return attackBaseTime / (1 + (attackSpeedValue - 100) / 100);
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
        ///     Gets the attack speed.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float GetAttackSpeed(Hero unit)
        {
            // try
            // {
            // double attackBaseTime;
            // if (!AttackRateDictionary.TryGetValue(unit.Handle, out attackBaseTime))
            // {
            // attackBaseTime =
            // Game.FindKeyValues(unit.StoredName() + "/AttackRate", KeyValueSource.Hero).FloatValue;
            // AttackRateDictionary.Add(unit.Handle, attackBaseTime);
            // }
            var attackSpeed = Math.Min(unit.AttackSpeedValue, 600);

            if (unit.HasModifier("modifier_ursa_overpower"))
            {
                attackSpeed = 600;
            }

            return (float)attackSpeed;

            // }
            // catch (KeyValuesNotFoundException)
            // {
            // if (!Utils.SleepCheck("Ensage.Common.DemoModeWarning"))
            // {
            // return 0;
            // }

            // Utils.Sleep(10000, "Ensage.Common.DemoModeWarning");
            // Console.WriteLine(@"[[Please do not use demo mode for testing assemblies]]");
            // return 0;
            // }
        }

        /// <summary>
        ///     Gets the attack speed.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float GetAttackSpeed(Unit unit)
        {
            var hero = unit as Hero;
            if (hero != null)
            {
                return GetAttackSpeed(hero);
            }

            // try
            // {
            // double attackBaseTime;
            // if (!AttackRateDictionary.TryGetValue(unit.Handle, out attackBaseTime))
            // {
            // attackBaseTime =
            // Game.FindKeyValues(unit.StoredName() + "/AttackRate", KeyValueSource.Unit).FloatValue;
            // AttackRateDictionary.Add(unit.Handle, attackBaseTime);
            // }
            var attackSpeed = Math.Min(unit.AttackSpeedValue, 600);
            return (float)attackSpeed;

            // }
            // catch (KeyValuesNotFoundException)
            // {
            // if (!Utils.SleepCheck("Ensage.Common.DemoModeWarning"))
            // {
            // return 0;
            // }

            // Utils.Sleep(10000, "Ensage.Common.DemoModeWarning");
            // Console.WriteLine(@"[[Please do not use demo mode for testing assemblies]]");
            // return 0;
            // }
        }

        /// <summary>
        ///     Gets the attack animation data by class id.
        /// </summary>
        /// <param name="classId">
        ///     The class id.
        /// </param>
        /// <returns>
        ///     The <see cref="AttackAnimationData" />.
        /// </returns>
        public static AttackAnimationData GetByClassId(ClassId classId)
        {
            return Units.FirstOrDefault(unitData => unitData.UnitClassId.Equals(classId));
        }

        /// <summary>
        ///     Gets the attack animation data by name.
        /// </summary>
        /// <param name="unitName">
        ///     The unit name.
        /// </param>
        /// <returns>
        ///     The <see cref="AttackAnimationData" />.
        /// </returns>
        public static AttackAnimationData GetByName(string unitName)
        {
            return Units.FirstOrDefault(unitData => unitData.UnitName.ToLower() == unitName);
        }

        /// <summary>
        ///     Returns units projectile speed
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double GetProjectileSpeed(Hero unit)
        {
            if (unit == null || !unit.IsRanged)
            {
                return double.MaxValue;
            }

            var name = unit.StoredName();
            try
            {
                double projSpeed;
                if (!ProjSpeedDictionary.TryGetValue(unit.Handle, out projSpeed))
                {
                    projSpeed = Game.FindKeyValues(name + "/ProjectileSpeed", KeyValueSource.Hero).IntValue;
                }

                return projSpeed;
            }
            catch (KeyValuesNotFoundException)
            {
                if (!Utils.SleepCheck("Ensage.Common.DemoModeWarning"))
                {
                    return double.MaxValue;
                }

                Utils.Sleep(10000, "Ensage.Common.DemoModeWarning");
                Console.WriteLine(@"[[Please do not use demo mode for testing assemblies]]");
                return double.MaxValue;
            }
        }

        /// <summary>
        ///     Returns units projectile speed
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double GetProjectileSpeed(Unit unit)
        {
            var hero = unit as Hero;
            if (hero != null)
            {
                return GetProjectileSpeed(hero);
            }

            if (unit == null || !unit.IsRanged)
            {
                return double.MaxValue;
            }

            var name = unit.StoredName();
            try
            {
                double projSpeed;
                if (!ProjSpeedDictionary.TryGetValue(unit.Handle, out projSpeed))
                {
                    projSpeed = Game.FindKeyValues(name + "/ProjectileSpeed", KeyValueSource.Unit).IntValue;
                }

                return projSpeed;
            }
            catch (KeyValuesNotFoundException)
            {
                if (!Utils.SleepCheck("Ensage.Common.DemoModeWarning"))
                {
                    return double.MaxValue;
                }

                Utils.Sleep(10000, "Ensage.Common.DemoModeWarning");
                Console.WriteLine(@"[[Please do not use demo mode for testing assemblies]]");
                return double.MaxValue;
            }
        }

        #endregion
    }
}