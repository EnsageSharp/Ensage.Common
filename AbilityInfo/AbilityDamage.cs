// <copyright file="AbilityDamage.cs" company="EnsageSharp">
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
namespace Ensage.Common.AbilityInfo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.Common.Extensions;
    using Ensage.Common.Objects;

    using Attribute = Ensage.Attribute;

    /// <summary>
    ///     Class used to calculate damage from most abilities
    /// </summary>
    public class AbilityDamage
    {
        #region Static Fields

        /// <summary>
        ///     The damage dictionary.
        /// </summary>
        private static Dictionary<Ability, float> damageDictionary = new Dictionary<Ability, float>();

        /// <summary>
        ///     The data dictionary.
        /// </summary>
        private static Dictionary<Ability, AbilityInfo> dataDictionary = new Dictionary<Ability, AbilityInfo>();

        /// <summary>
        ///     The level dictionary.
        /// </summary>
        private static Dictionary<Ability, uint> levelDictionary = new Dictionary<Ability, uint>();

        /// <summary>
        ///     The multiplier dictionary.
        /// </summary>
        private static Dictionary<Ability, double> multiplierDictionary = new Dictionary<Ability, double>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Calculates damage from given ability on given target
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <param name="source">
        ///     The source.
        /// </param>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="minusArmor">
        ///     The minus Armor.
        /// </param>
        /// <param name="minusDamageResistancePerc">
        ///     The minus Damage Resistance Percentage.
        /// </param>
        /// <param name="minusMagicResistancePerc">
        ///     The minus Magic Resistance Percentage.
        /// </param>
        /// <param name="minusHealth">
        ///     The minus Health.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float CalculateDamage(
            Ability ability,
            Unit source,
            Unit target,
            double minusArmor = 0d,
            double minusDamageResistancePerc = 0d,
            double minusMagicResistancePerc = 0d,
            float minusHealth = 0f)
        {
            var name = ability.StoredName();
            var level = ability.Level;
            if (ability.AbilityType.HasFlag(AbilityType.Ultimate) && level > 0 && source.AghanimState())
            {
                level += 1;
            }

            AbilityInfo data;
            if (!dataDictionary.TryGetValue(ability, out data))
            {
                data = AbilityDatabase.Find(name);
                if (data != null)
                {
                    dataDictionary.Add(ability, data);
                }
            }

            if (data == null)
            {
                return 0;
            }

            var outgoingDamage = 0f;
            float bonusDamage = 0;
            Hero hero;
            double multi;
            float tempDmg;
            switch (name)
            {
                case "item_urn_of_shadows":
                    outgoingDamage = target.SpellDamageTaken(150, DamageType.Pure, source, name);
                    break;
                case "ember_spirit_sleight_of_fist":
                    outgoingDamage = source.MinimumDamage + source.BonusDamage;
                    if (!damageDictionary.TryGetValue(ability, out bonusDamage))
                    {
                        bonusDamage = ability.GetAbilityData(data.BonusDamageString);
                        outgoingDamage += bonusDamage;
                        damageDictionary.Add(ability, bonusDamage);
                        levelDictionary.Add(ability, level);
                    }
                    else if (levelDictionary[ability] != level)
                    {
                        levelDictionary[ability] = level;
                        bonusDamage = ability.GetAbilityData(data.BonusDamageString);
                        damageDictionary[ability] = bonusDamage;
                        outgoingDamage += bonusDamage;
                    }
                    else
                    {
                        outgoingDamage += bonusDamage;
                    }

                    outgoingDamage = target.SpellDamageTaken(
                        outgoingDamage,
                        DamageType.Physical,
                        source,
                        name,
                        true,
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
                case "doom_bringer_lvl_death":
                    if (!damageDictionary.TryGetValue(ability, out tempDmg))
                    {
                        tempDmg = ability.GetAbilityData(data.DamageString);
                        damageDictionary.Add(ability, tempDmg);
                        levelDictionary.Add(ability, level);
                    }
                    else if (levelDictionary[ability] != level)
                    {
                        levelDictionary[ability] = level;
                        tempDmg = ability.GetAbilityData(data.DamageString);
                        damageDictionary[ability] = tempDmg;
                    }

                    var multiplier = ability.GetAbilityData("lvl_bonus_multiple");
                    bonusDamage = ability.GetAbilityData("lvl_bonus_damage");
                    var levelc = target.Level / multiplier;
                    if (levelc == Math.Floor(levelc) || target.Level == 25)
                    {
                        tempDmg += target.MaximumHealth * (bonusDamage / 100);
                    }

                    outgoingDamage = target.SpellDamageTaken(
                        tempDmg,
                        DamageType.Magical,
                        source,
                        name,
                        data.MagicImmunityPierce,
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
                case "phantom_assassin_phantom_strike":
                    var crit = source.Spellbook.SpellR;
                    if (crit.Level > 0)
                    {
                        float critMulti;
                        if (!damageDictionary.TryGetValue(crit, out critMulti))
                        {
                            critMulti = crit.GetAbilityData("crit_bonus");
                            damageDictionary.Add(crit, critMulti);
                            levelDictionary.Add(crit, crit.Level);
                        }
                        else if (levelDictionary[crit] != crit.Level)
                        {
                            levelDictionary[crit] = crit.Level;
                            critMulti = crit.GetAbilityData("crit_bonus");
                            damageDictionary[crit] = critMulti;
                        }

                        outgoingDamage = (source.MinimumDamage + source.BonusDamage) * (critMulti / 100);
                    }

                    outgoingDamage = target.SpellDamageTaken(
                        outgoingDamage,
                        DamageType.Physical,
                        source,
                        name,
                        data.MagicImmunityPierce,
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
                case "tusk_walrus_punch":
                    if (!multiplierDictionary.TryGetValue(ability, out multi))
                    {
                        multi = ability.GetAbilityData("crit_multiplier");
                        multiplierDictionary.Add(ability, multi);
                    }

                    outgoingDamage =
                        target.SpellDamageTaken(
                            (float)((source.MinimumDamage + source.BonusDamage) * (multi / 100)),
                            DamageType.Physical,
                            source,
                            name,
                            data.MagicImmunityPierce,
                            minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
                case "necrolyte_reapers_scythe":
                    if (!multiplierDictionary.TryGetValue(ability, out multi))
                    {
                        multi = ability.GetAbilityData(data.DamageString);
                        multiplierDictionary.Add(ability, multi);
                        levelDictionary.Add(ability, level);
                    }
                    else if (levelDictionary[ability] != level)
                    {
                        levelDictionary[ability] = level;
                        multi = ability.GetAbilityData(data.DamageString);
                        multiplierDictionary[ability] = multi;
                    }

                    var missingHp = target.MaximumHealth - target.Health + minusHealth;
                    outgoingDamage = target.SpellDamageTaken(
                        (float)(missingHp * multi),
                        DamageType.Magical,
                        source,
                        name,
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
                case "chaos_knight_reality_rift":
                    if (!damageDictionary.TryGetValue(ability, out bonusDamage))
                    {
                        bonusDamage = ability.GetAbilityData(data.DamageString);
                        damageDictionary.Add(ability, bonusDamage);
                        levelDictionary.Add(ability, level);
                    }
                    else if (levelDictionary[ability] != level)
                    {
                        levelDictionary[ability] = level;
                        bonusDamage = ability.GetAbilityData(data.DamageString);
                        damageDictionary[ability] = bonusDamage;
                    }

                    outgoingDamage = target.SpellDamageTaken(
                        source.MaximumDamage + source.BonusDamage + bonusDamage,
                        DamageType.Physical,
                        source,
                        name,
                        data.MagicImmunityPierce);
                    break;
                case "templar_assassin_meld":
                    if (!damageDictionary.TryGetValue(ability, out bonusDamage))
                    {
                        bonusDamage = ability.GetAbilityData(data.DamageString);
                        damageDictionary.Add(ability, bonusDamage);
                        levelDictionary.Add(ability, level);
                    }
                    else if (levelDictionary[ability] != level)
                    {
                        levelDictionary[ability] = level;
                        bonusDamage = ability.GetAbilityData(data.DamageString);
                        damageDictionary[ability] = bonusDamage;
                    }

                    var minusArmors = new[] { -2, -4, -6, -8 };
                    var meldminusArmor = target.Armor + minusArmors[level - 1];
                    var damageIncrease = 1 - 0.06 * meldminusArmor / (1 + 0.06 * Math.Abs(meldminusArmor));
                    outgoingDamage =
                        (float)
                        (target.SpellDamageTaken(
                             source.MaximumDamage + source.BonusDamage,
                             DamageType.Physical,
                             source,
                             name,
                             data.MagicImmunityPierce,
                             minusMagicResistancePerc: minusMagicResistancePerc) + bonusDamage * damageIncrease);
                    break;
                case "undying_decay":
                    var strengthSteal = ability.GetAbilityData("str_steal");
                    if (source.AghanimState())
                    {
                        strengthSteal = ability.GetAbilityData("str_Steal_scepter");
                    }

                    outgoingDamage = strengthSteal * 19
                                     + target.SpellDamageTaken(
                                         ability.GetAbilityData(data.DamageString),
                                         DamageType.Magical,
                                         source,
                                         name,
                                         false,
                                         minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
                case "visage_soul_assumption":
                    var dmg = ability.GetAbilityData(data.DamageString);
                    var modif = source.FindModifier("modifier_visage_soul_assumption");
                    if (modif != null)
                    {
                        dmg += modif.StackCount * ability.GetAbilityData("soul_charge_damage");
                    }

                    outgoingDamage = target.SpellDamageTaken(
                        dmg,
                        DamageType.Magical,
                        source,
                        name,
                        false,
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
                case "morphling_adaptive_strike":
                    if (!damageDictionary.TryGetValue(ability, out bonusDamage))
                    {
                        bonusDamage = ability.GetAbilityData(data.DamageString);
                        damageDictionary.Add(ability, bonusDamage);
                        levelDictionary.Add(ability, level);
                    }
                    else if (levelDictionary[ability] != level)
                    {
                        levelDictionary[ability] = level;
                        bonusDamage = ability.GetAbilityData(data.DamageString);
                        damageDictionary[ability] = bonusDamage;
                    }

                    hero = source as Hero;
                    var agi = Math.Floor(hero.TotalAgility);
                    var str = Math.Floor(hero.TotalStrength);
                    var difference = agi / str;
                    var multimin = ability.GetAbilityData("damage_min");
                    var multimax = ability.GetAbilityData("damage_max");
                    multi = multimin + (difference - 0.5) * (multimax - multimin);
                    if (difference > 1.5)
                    {
                        multi = multimax;
                    }
                    else if (difference < 0.5)
                    {
                        multi = multimin;
                    }

                    outgoingDamage = target.SpellDamageTaken(
                        (float)(bonusDamage + agi * multi),
                        DamageType.Magical,
                        source,
                        name,
                        false,
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
                case "mirana_starfall":
                    var radiusMax = ability.GetAbilityData("starfall_secondary_radius");
                    if (!damageDictionary.TryGetValue(ability, out bonusDamage))
                    {
                        bonusDamage =
                            Convert.ToSingle(
                                Game.FindKeyValues(name + "/AbilityDamage", KeyValueSource.Ability)
                                    .StringValue.Split(' ')[level - 1]);
                        damageDictionary.Add(ability, bonusDamage);
                        levelDictionary.Add(ability, level);
                    }
                    else if (levelDictionary[ability] != level)
                    {
                        levelDictionary[ability] = level;
                        bonusDamage =
                            Convert.ToSingle(
                                Game.FindKeyValues(name + "/AbilityDamage", KeyValueSource.Ability)
                                    .StringValue.Split(' ')[level - 1]);
                        damageDictionary[ability] = bonusDamage;
                    }

                    outgoingDamage = target.SpellDamageTaken(
                        bonusDamage,
                        DamageType.Magical,
                        source,
                        name,
                        data.MagicImmunityPierce,
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    if (source.Distance2D(target) < radiusMax)
                    {
                        outgoingDamage *= 2;
                    }

                    break;
                case "item_ethereal_blade":
                    hero = source as Hero;
                    var primaryAtt = hero.PrimaryAttribute;
                    if (primaryAtt == Attribute.Agility)
                    {
                        bonusDamage = 2f * hero.TotalAgility;
                    }
                    else if (primaryAtt == Attribute.Intelligence)
                    {
                        bonusDamage = 2f * hero.TotalIntelligence;
                    }
                    else if (primaryAtt == Attribute.Strength)
                    {
                        bonusDamage = 2f * hero.TotalStrength;
                    }

                    if (!damageDictionary.TryGetValue(ability, out tempDmg))
                    {
                        tempDmg = ability.GetAbilityData(data.DamageString);
                        damageDictionary.Add(ability, tempDmg);
                        levelDictionary.Add(ability, level);
                    }
                    else if (levelDictionary[ability] != level)
                    {
                        levelDictionary[ability] = level;
                        tempDmg = ability.GetAbilityData(data.DamageString);
                        damageDictionary[ability] = tempDmg;
                    }

                    outgoingDamage = target.SpellDamageTaken(
                        tempDmg + bonusDamage,
                        DamageType.Magical,
                        source,
                        name,
                        false,
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
                case "nyx_assassin_mana_burn":
                    var intMultiplier = ability.GetAbilityData("float_multiplier");
                    hero = target as Hero;
                    outgoingDamage = target.ManaBurnSpellDamageTaken(
                        hero.TotalIntelligence * intMultiplier,
                        1,
                        DamageType.Magical,
                        source,
                        name,
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
                case "antimage_mana_void":
                    if (!damageDictionary.TryGetValue(ability, out tempDmg))
                    {
                        tempDmg = ability.GetAbilityData(data.DamageString);
                        damageDictionary.Add(ability, tempDmg);
                        levelDictionary.Add(ability, level);
                    }
                    else if (levelDictionary[ability] != level)
                    {
                        levelDictionary[ability] = level;
                        tempDmg = ability.GetAbilityData(data.DamageString);
                        damageDictionary[ability] = tempDmg;
                    }

                    hero = target as Hero;
                    var missingMana = hero.MaximumMana - hero.Mana;
                    outgoingDamage = target.SpellDamageTaken(
                        missingMana * tempDmg,
                        DamageType.Magical,
                        source,
                        name,
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
                case "riki_blink_strike":
                    var damage = ability.GetAbilityData(data.DamageString);
                    var backstab = source.Spellbook.SpellE;
                    var agiMultiplier = backstab.GetAbilityData("damage_multiplier");
                    hero = source as Hero;
                    var blinkdamage = target.SpellDamageTaken(
                        damage,
                        DamageType.Magical,
                        source,
                        name,
                        data.MagicImmunityPierce,
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    outgoingDamage = blinkdamage
                                     + target.SpellDamageTaken(
                                         agiMultiplier * hero.TotalAgility + (source.MinimumDamage + source.BonusDamage),
                                         DamageType.Physical,
                                         source,
                                         name,
                                         data.MagicImmunityPierce,
                                         minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
                case "undying_soul_rip":
                    var radius = ability.GetAbilityData("radius");
                    var nearUnits = ObjectManager.GetEntities<Unit>();
                    var damagePerUnit = ability.GetAbilityData("damage_per_unit");
                    var maxUnits = ability.GetAbilityData("max_units");
                    outgoingDamage =
                        Math.Min(
                            nearUnits.Count(
                                x =>
                                    !x.Equals(source) && !x.Equals(target)
                                    && x.Distance2D(source) < radius + x.HullRadius
                                    && (x.ClassId == ClassId.CDOTA_BaseNPC_Creep_Lane
                                        || x.ClassId == ClassId.CDOTA_BaseNPC_Creep
                                        || x.ClassId == ClassId.CDOTA_BaseNPC_Creep_Neutral
                                        || x.ClassId == ClassId.CDOTA_BaseNPC_Creep_Siege
                                        || x.ClassId == ClassId.CDOTA_BaseNPC_Creature
                                        || x.ClassId == ClassId.CDOTA_BaseNPC_Invoker_Forged_Spirit
                                        || x.ClassId == ClassId.CDOTA_Unit_Undying_Zombie
                                        || x.ClassId == ClassId.CDOTA_BaseNPC_Warlock_Golem
                                        || x is Hero
                                        && (x.Team == source.Team
                                            || x.Team == source.GetEnemyTeam() && !x.IsMagicImmune())) && x.IsAlive
                                    && x.IsVisible),
                            maxUnits) * damagePerUnit;
                    outgoingDamage = target.SpellDamageTaken(
                        outgoingDamage,
                        DamageType.Magical,
                        source,
                        name,
                        data.MagicImmunityPierce,
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
                default:
                    var damageString = data.DamageString;
                    if (damageString == null)
                    {
                        outgoingDamage = ability.GetDamage(level - 1);
                    }
                    else
                    {
                        if (ability.AbilityType.HasFlag(AbilityType.Ultimate) && level > 0 && source.AghanimState())
                        {
                            level -= 1;
                        }

                        if (data.SpellLevel != null)
                        {
                            var spellLevel = source.FindSpell(data.SpellLevel, true);
                            level = spellLevel.Level;
                        }

                        if (source.ClassId == ClassId.CDOTA_Unit_Hero_Invoker && level > 0 && source.AghanimState())
                        {
                            level += 1;
                        }

                        if (data.DamageScepterString != null && source.AghanimState())
                        {
                            outgoingDamage = ability.GetAbilityData(data.DamageScepterString, level);
                        }
                        else
                        {
                            outgoingDamage = ability.GetAbilityData(damageString, level);
                        }

                        if (data.DamageMultiplier > 0)
                        {
                            outgoingDamage = outgoingDamage * data.DamageMultiplier;
                        }
                    }

                    outgoingDamage = target.SpellDamageTaken(
                        outgoingDamage,
                        GetDamageType(ability),
                        source,
                        name,
                        data.MagicImmunityPierce,
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
            }

            if (source.ClassId == ClassId.CDOTA_Unit_Hero_Zuus && !(ability is Item)
                && (source.Distance2D(target) <= 1200 || ability.StoredName() != "zuus_thundergods_wrath"))
            {
                var staticField = source.Spellbook.Spell3;
                if (staticField.Level <= 0)
                {
                    return outgoingDamage;
                }

                var bonusDmg = staticField.GetAbilityData("damage_health_pct") / 100 * (target.Health - minusHealth);
                bonusDmg = target.SpellDamageTaken(
                    bonusDmg,
                    DamageType.Magical,
                    source,
                    name,
                    minusMagicResistancePerc: minusMagicResistancePerc);
                outgoingDamage += bonusDmg;
            }

            return outgoingDamage;
        }

        /// <summary>
        ///     Returns DamageType of ability
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <returns>
        ///     The <see cref="DamageType" />.
        /// </returns>
        public static DamageType GetDamageType(Ability ability)
        {
            var type = ability.DamageType;
            var name = ability.StoredName();
            switch (name)
            {
                case "item_urn_of_shadows":
                    type = DamageType.Pure;
                    break;
                case "abaddon_aphotic_shield":
                    type = DamageType.Magical;
                    break;
                case "meepo_poof":
                    type = DamageType.Magical;
                    break;
                case "axe_culling_blade":
                    type = DamageType.Pure;
                    break;
                case "invoker_sun_strinke":
                    type = DamageType.Pure;
                    break;
                case "alchemist_unstable_concoction_throw":
                    type = DamageType.Physical;
                    break;
                case "centaur_stampede":
                    type = DamageType.Physical;
                    break;
                case "lina_laguna_blade":
                    if ((ability.Owner as Hero).AghanimState())
                    {
                        type = DamageType.Pure;
                    }

                    break;
                case "legion_commander_duel":
                    type = DamageType.Physical;
                    break;
                case "item_ethereal_blade":
                    type = DamageType.Magical;
                    break;
                case "tusk_walrus_kick":
                    type = DamageType.Magical;
                    break;
                case "tusk_walrus_punch":
                    type = DamageType.Physical;
                    break;
                case "item_shivas_guard":
                    type = DamageType.Magical;
                    break;
                case "chaos_knight_reality_rift":
                    type = DamageType.Physical;
                    break;
                case "item_veil_of_discord":
                    type = DamageType.Magical;
                    break;
            }

            if (type == DamageType.None)
            {
                type = DamageType.Magical;
            }

            if (name.StartsWith("item_dagon"))
            {
                type = DamageType.Magical;
            }

            return type;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The initialize.
        /// </summary>
        internal static void Init()
        {
            dataDictionary = new Dictionary<Ability, AbilityInfo>();
            levelDictionary = new Dictionary<Ability, uint>();
            damageDictionary = new Dictionary<Ability, float>();
            multiplierDictionary = new Dictionary<Ability, double>();
        }

        #endregion
    }
}