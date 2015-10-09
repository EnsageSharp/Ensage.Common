// <copyright file="UnitDatabase.cs" company="EnsageSharp">
//    Copyright (c) 2015 EnsageSharp.
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ensage.Common.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ensage.Common
{
    /// <summary>
    ///     The unit database.
    /// </summary>
    public static class UnitDatabase
    {
        #region Static Fields

        /// <summary>
        ///     The units.
        /// </summary>
        public static List<AttackAnimationData> Units = new List<AttackAnimationData>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="UnitDatabase" /> class.
        /// </summary>
        static UnitDatabase()
        {
            JsonConvert.DeserializeObject<AttackAnimationData>(
                JObject.Parse(Encoding.Default.GetString(Resources.UnitDatabase)).ToString());
        }

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
        public static double GetAttackBackswing(Unit unit)
        {
            var attackRate = GetAttackRate(unit);
            var attackPoint = GetAttackPoint(unit);
            return attackRate - attackPoint;
        }

        /// <summary>
        ///     Gets the attack point.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double GetAttackPoint(Unit unit)
        {
            if (unit == null)
            {
                return 0;
            }

            var classId = unit.ClassID;
            var name = unit.Name;
            var data = GetByClassId(classId) ?? GetByName(name);
            if (data == null)
            {
                return 0;
            }

            var attackSpeed = GetAttackSpeed(unit);

            return (data.AttackPoint / (1 + ((attackSpeed - 100) / 100)))
                   - (((Game.Ping / 1000) / (1 + (1 - (1 / UnitData.MaxCount)))) * 2)
                   + ((1 / UnitData.MaxCount) * 3 * (1 + (1 - (1 / UnitData.MaxCount))));
        }

        /// <summary>
        ///     Gets the attack rate.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double GetAttackRate(Unit unit)
        {
            var attackSpeed = GetAttackSpeed(unit);
            var attackBaseTime = unit.BaseAttackTime;
            Ability spell = null;
            if (
                !unit.Modifiers.Any(
                    x =>
                    (x.Name == "modifier_alchemist_chemical_rage" || x.Name == "modifier_terrorblade_metamorphosis"
                     || x.Name == "modifier_lone_druid_true_form" || x.Name == "modifier_troll_warlord_berserkers_rage")))
            {
                return (attackBaseTime / (1 + ((attackSpeed - 100) / 100))) - 0.03;
            }

            switch (unit.ClassID)
            {
                case ClassID.CDOTA_Unit_Hero_Alchemist:
                    spell = unit.Spellbook.Spells.First(x => x.Name == "alchemist_chemical_rage");
                    break;
                case ClassID.CDOTA_Unit_Hero_Terrorblade:
                    spell = unit.Spellbook.Spells.First(x => x.Name == "terrorblade_metamorphosis");
                    break;
                case ClassID.CDOTA_Unit_Hero_LoneDruid:
                    spell = unit.Spellbook.Spells.First(x => x.Name == "lone_druid_true_form");
                    break;
                case ClassID.CDOTA_Unit_Hero_TrollWarlord:
                    spell = unit.Spellbook.Spells.First(x => x.Name == "troll_warlord_berserkers_rage");
                    break;
            }

            if (spell != null)
            {
                var baseAttackTime = spell.AbilityData.FirstOrDefault(x => x.Name == "base_attack_time");
                if (baseAttackTime != null)
                {
                    attackBaseTime = baseAttackTime.Value;
                }

                return (attackBaseTime / (1 + ((attackSpeed - 100) / 100)))
                       - (((Game.Ping / 1000) / (1 + (1 - (1 / UnitData.MaxCount)))) * 2)
                       + ((1 / UnitData.MaxCount) * 3 * (1 + (1 - (1 / UnitData.MaxCount))));
            }

            return 0d;
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
            var attackSpeed = Math.Min(unit.Speed, 600);

            if (unit.Modifiers.Any(x => (x.Name == "modifier_ursa_overpower")))
            {
                attackSpeed = 600;
            }

            return attackSpeed;
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
        public static AttackAnimationData GetByClassId(ClassID classId)
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

        #endregion
    }
}