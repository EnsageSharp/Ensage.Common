// <copyright file="AbilityDamage.cs" company="EnsageSharp">
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
        /// </summary>
        public static Dictionary<Ability, AbilityInfo> DataDictionary = new Dictionary<Ability, AbilityInfo>();

        /// <summary>
        /// </summary>
        public static Dictionary<Ability, uint> LevelDictionary = new Dictionary<Ability, uint>();

        private static readonly Dictionary<Ability, float> DamageDictionary = new Dictionary<Ability, float>();

        private static readonly Dictionary<Ability, double> MultiplierDictionary = new Dictionary<Ability, double>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Calculates damage from given ability on given target
        /// </summary>
        /// <param name="ability"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="minusArmor"></param>
        /// <param name="minusDamageResistancePerc"></param>
        /// <param name="minusMagicResistancePerc"></param>
        /// <param name="minusHealth"></param>
        /// <returns></returns>
        public static float CalculateDamage(
            Ability ability, 
            Hero source, 
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
            if (!DataDictionary.TryGetValue(ability, out data))
            {
                data = AbilityDatabase.Find(name);
                if (data != null && data.IsNuke)
                {
                    DataDictionary.Add(ability, data);
                }
            }

            // var data = AbilityDatabase.Find(name);
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
                    outgoingDamage = target.DamageTaken(150, DamageType.Pure, source);
                    break;
                case "ember_spirit_sleight_of_fist":
                    outgoingDamage = source.MinimumDamage + source.BonusDamage;
                    if (!DamageDictionary.TryGetValue(ability, out bonusDamage))
                    {
                        bonusDamage = ability.GetAbilityData(data.BonusDamageString);
                        outgoingDamage += bonusDamage;
                        DamageDictionary.Add(ability, bonusDamage);
                        LevelDictionary.Add(ability, level);
                    }
                    else if (LevelDictionary[ability] != level)
                    {
                        LevelDictionary[ability] = level;
                        bonusDamage = ability.GetAbilityData(data.BonusDamageString);
                        DamageDictionary[ability] = bonusDamage;
                        outgoingDamage += bonusDamage;
                    }
                    else
                    {
                        outgoingDamage += bonusDamage;
                    }

                    outgoingDamage = target.DamageTaken(
                        outgoingDamage, 
                        DamageType.Physical, 
                        source, 
                        true, 
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
                case "doom_bringer_lvl_death":
                    if (!DamageDictionary.TryGetValue(ability, out tempDmg))
                    {
                        tempDmg = ability.GetAbilityData(data.DamageString);
                        DamageDictionary.Add(ability, tempDmg);
                        LevelDictionary.Add(ability, level);
                    }
                    else if (LevelDictionary[ability] != level)
                    {
                        LevelDictionary[ability] = level;
                        tempDmg = ability.GetAbilityData(data.DamageString);
                        DamageDictionary[ability] = tempDmg;
                    }

                    var multiplier = ability.GetAbilityData("lvl_bonus_multiple");
                    bonusDamage = ability.GetAbilityData("lvl_bonus_damage");
                    var levelc = target.Level / multiplier;
                    if (levelc == Math.Floor(levelc) || target.Level == 25)
                    {
                        tempDmg += target.MaximumHealth * (bonusDamage / 100);
                    }

                    outgoingDamage = target.DamageTaken(
                        tempDmg, 
                        DamageType.Magical, 
                        source, 
                        data.MagicImmunityPierce, 
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
                case "phantom_assassin_phantom_strike":
                    var crit = source.Spellbook.SpellR;
                    if (crit.Level > 0)
                    {
                        float critMulti;
                        if (!DamageDictionary.TryGetValue(crit, out critMulti))
                        {
                            critMulti = crit.GetAbilityData("crit_bonus");
                            DamageDictionary.Add(crit, critMulti);
                            LevelDictionary.Add(crit, crit.Level);
                        }
                        else if (LevelDictionary[crit] != crit.Level)
                        {
                            LevelDictionary[crit] = crit.Level;
                            critMulti = crit.GetAbilityData("crit_bonus");
                            DamageDictionary[crit] = critMulti;
                        }

                        outgoingDamage = (source.MinimumDamage + source.BonusDamage) * (critMulti / 100);
                    }

                    outgoingDamage = target.DamageTaken(
                        outgoingDamage, 
                        DamageType.Physical, 
                        source, 
                        data.MagicImmunityPierce, 
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
                case "tusk_walrus_punch":
                    if (!MultiplierDictionary.TryGetValue(ability, out multi))
                    {
                        multi = ability.GetAbilityData("crit_multiplier");
                        MultiplierDictionary.Add(ability, multi);
                    }

                    outgoingDamage =
                        target.DamageTaken(
                            (float)((source.MinimumDamage + source.BonusDamage) * (multi / 100)), 
                            DamageType.Physical, 
                            source, 
                            data.MagicImmunityPierce, 
                            minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
                case "necrolyte_reapers_scythe":
                    if (!MultiplierDictionary.TryGetValue(ability, out multi))
                    {
                        multi = ability.GetAbilityData(data.DamageString);
                        MultiplierDictionary.Add(ability, multi);
                        LevelDictionary.Add(ability, level);
                    }
                    else if (LevelDictionary[ability] != level)
                    {
                        LevelDictionary[ability] = level;
                        multi = ability.GetAbilityData(data.DamageString);
                        MultiplierDictionary[ability] = multi;
                    }

                    var missingHp = target.MaximumHealth - target.Health + minusHealth;
                    outgoingDamage = target.DamageTaken(
                        (float)(missingHp * multi), 
                        DamageType.Magical, 
                        source, 
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
                case "chaos_knight_reality_rift":
                    if (!DamageDictionary.TryGetValue(ability, out bonusDamage))
                    {
                        bonusDamage = ability.GetAbilityData(data.DamageString);
                        DamageDictionary.Add(ability, bonusDamage);
                        LevelDictionary.Add(ability, level);
                    }
                    else if (LevelDictionary[ability] != level)
                    {
                        LevelDictionary[ability] = level;
                        bonusDamage = ability.GetAbilityData(data.DamageString);
                        DamageDictionary[ability] = bonusDamage;
                    }

                    outgoingDamage = target.DamageTaken(
                        source.MaximumDamage + source.BonusDamage + bonusDamage, 
                        DamageType.Physical, 
                        source, 
                        data.MagicImmunityPierce);
                    break;
                case "templar_assassin_meld":
                    if (!DamageDictionary.TryGetValue(ability, out bonusDamage))
                    {
                        bonusDamage = ability.GetAbilityData(data.DamageString);
                        DamageDictionary.Add(ability, bonusDamage);
                        LevelDictionary.Add(ability, level);
                    }
                    else if (LevelDictionary[ability] != level)
                    {
                        LevelDictionary[ability] = level;
                        bonusDamage = ability.GetAbilityData(data.DamageString);
                        DamageDictionary[ability] = bonusDamage;
                    }

                    // var minusArmor = ability.GetAbilityData("bonus_armor");
                    var minusArmors = new[] { -2, -4, -6, -8 };
                    var meldminusArmor = target.Armor + minusArmors[level - 1];

                    // Console.WriteLine(minusArmor);
                    var damageIncrease = 1 - 0.06 * meldminusArmor / (1 + 0.06 * Math.Abs(meldminusArmor));

                    // Console.WriteLine(damageIncrease);
                    outgoingDamage =
                        (float)
                        (target.DamageTaken(
                            source.MaximumDamage + source.BonusDamage, 
                            DamageType.Physical, 
                            source, 
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
                                     + target.DamageTaken(
                                         ability.GetAbilityData(data.DamageString), 
                                         DamageType.Magical, 
                                         source, 
                                         false, 
                                         minusMagicResistancePerc: minusMagicResistancePerc);

                    // Console.WriteLine(outgoingDamage);
                    break;
                case "visage_soul_assumption":
                    var dmg = ability.GetAbilityData(data.DamageString);
                    var modif = source.FindModifier("modifier_visage_soul_assumption");
                    if (modif != null)
                    {
                        dmg += modif.StackCount * ability.GetAbilityData("soul_charge_damage");
                    }

                    outgoingDamage = target.DamageTaken(
                        dmg, 
                        DamageType.Magical, 
                        source, 
                        false, 
                        minusMagicResistancePerc: minusMagicResistancePerc);

                    // Console.WriteLine(outgoingDamage);
                    break;
                case "morphling_adaptive_strike":
                    if (!DamageDictionary.TryGetValue(ability, out bonusDamage))
                    {
                        bonusDamage = ability.GetAbilityData(data.DamageString);
                        DamageDictionary.Add(ability, bonusDamage);
                        LevelDictionary.Add(ability, level);
                    }
                    else if (LevelDictionary[ability] != level)
                    {
                        LevelDictionary[ability] = level;
                        bonusDamage = ability.GetAbilityData(data.DamageString);
                        DamageDictionary[ability] = bonusDamage;
                    }

                    hero = source;
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

                    outgoingDamage = target.DamageTaken(
                        (float)(bonusDamage + agi * multi), 
                        DamageType.Magical, 
                        source, 
                        false, 
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
                case "mirana_starfall":
                    var radiusMax = ability.GetAbilityData("starfall_secondary_radius");
                    if (!DamageDictionary.TryGetValue(ability, out bonusDamage))
                    {
                        bonusDamage =
                            Convert.ToSingle(
                                Game.FindKeyValues(name + "/AbilityDamage", KeyValueSource.Ability)
                                    .StringValue.Split(' ')[level - 1]);
                        DamageDictionary.Add(ability, bonusDamage);
                        LevelDictionary.Add(ability, level);
                    }
                    else if (LevelDictionary[ability] != level)
                    {
                        LevelDictionary[ability] = level;
                        bonusDamage =
                            Convert.ToSingle(
                                Game.FindKeyValues(name + "/AbilityDamage", KeyValueSource.Ability)
                                    .StringValue.Split(' ')[level - 1]);
                        DamageDictionary[ability] = bonusDamage;
                    }

                    outgoingDamage = target.DamageTaken(
                        bonusDamage, 
                        DamageType.Magical, 
                        source, 
                        data.MagicImmunityPierce, 
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    if (source.Distance2D(target) < radiusMax)
                    {
                        outgoingDamage *= 2;
                    }

                    break;
                case "item_ethereal_blade":
                    hero = source;
                    var primaryAtt = hero.PrimaryAttribute;
                    if (primaryAtt == Attribute.Agility)
                    {
                        bonusDamage = 2 * hero.TotalAgility;
                    }
                    else if (primaryAtt == Attribute.Intelligence)
                    {
                        bonusDamage = 2 * hero.TotalIntelligence;
                    }
                    else if (primaryAtt == Attribute.Strength)
                    {
                        bonusDamage = 2 * hero.TotalStrength;
                    }

                    if (!DamageDictionary.TryGetValue(ability, out tempDmg))
                    {
                        tempDmg = ability.GetAbilityData(data.DamageString);
                        DamageDictionary.Add(ability, tempDmg);
                        LevelDictionary.Add(ability, level);
                    }
                    else if (LevelDictionary[ability] != level)
                    {
                        LevelDictionary[ability] = level;
                        tempDmg = ability.GetAbilityData(data.DamageString);
                        DamageDictionary[ability] = tempDmg;
                    }

                    outgoingDamage = target.DamageTaken(
                        tempDmg + bonusDamage, 
                        DamageType.Magical, 
                        source, 
                        false, 
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
                case "nyx_assassin_mana_burn":
                    var intMultiplier = ability.GetAbilityData("float_multiplier");
                    hero = target as Hero;
                    outgoingDamage = target.ManaBurnDamageTaken(
                        hero.TotalIntelligence * intMultiplier, 
                        1, 
                        DamageType.Magical, 
                        source, 
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
                case "antimage_mana_void":
                    if (!DamageDictionary.TryGetValue(ability, out tempDmg))
                    {
                        tempDmg = ability.GetAbilityData(data.DamageString);
                        DamageDictionary.Add(ability, tempDmg);
                        LevelDictionary.Add(ability, level);
                    }
                    else if (LevelDictionary[ability] != level)
                    {
                        LevelDictionary[ability] = level;
                        tempDmg = ability.GetAbilityData(data.DamageString);
                        DamageDictionary[ability] = tempDmg;
                    }

                    hero = target as Hero;
                    var missingMana = hero.MaximumMana - hero.Mana;
                    outgoingDamage = target.DamageTaken(
                        missingMana * tempDmg, 
                        DamageType.Magical, 
                        source, 
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
                case "riki_blink_strike":
                    var damage = ability.GetAbilityData(data.DamageString);
                    var backstab = source.Spellbook.SpellE;
                    var agiMultiplier = backstab.GetAbilityData("damage_multiplier");
                    var blinkdamage = target.DamageTaken(
                        damage, 
                        DamageType.Magical, 
                        source, 
                        data.MagicImmunityPierce, 
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    outgoingDamage = blinkdamage
                                     + target.DamageTaken(
                                         agiMultiplier * source.TotalAgility
                                         + (source.MinimumDamage + source.BonusDamage), 
                                         DamageType.Physical, 
                                         source, 
                                         data.MagicImmunityPierce, 
                                         minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
                case "undying_soul_rip":
                    var radius = ability.GetAbilityData("radius");
                    var nearUnits =
                        ObjectManager.GetEntities<Unit>()
                            .Where(
                                x =>
                                !x.Equals(source) && !x.Equals(target)
                                && (x.ClassID == ClassID.CDOTA_BaseNPC_Creep_Lane
                                    || x.ClassID == ClassID.CDOTA_BaseNPC_Creep
                                    || x.ClassID == ClassID.CDOTA_BaseNPC_Creep_Neutral
                                    || x.ClassID == ClassID.CDOTA_BaseNPC_Creep_Siege
                                    || x.ClassID == ClassID.CDOTA_BaseNPC_Creature
                                    || x.ClassID == ClassID.CDOTA_BaseNPC_Invoker_Forged_Spirit
                                    || x.ClassID == ClassID.CDOTA_Unit_Undying_Zombie
                                    || x.ClassID == ClassID.CDOTA_BaseNPC_Warlock_Golem
                                    || (x is Hero
                                        && (x.Team == source.Team
                                            || (x.Team == source.GetEnemyTeam() && !x.IsMagicImmune())))) && x.IsAlive
                                && x.IsVisible && x.Distance2D(source) < radius + x.HullRadius);
                    var damagePerUnit = ability.GetAbilityData("damage_per_unit");
                    var maxUnits = ability.GetAbilityData("max_units");
                    outgoingDamage = Math.Min(nearUnits.Count(), maxUnits) * damagePerUnit;
                    outgoingDamage = target.DamageTaken(
                        outgoingDamage, 
                        DamageType.Magical, 
                        source, 
                        data.MagicImmunityPierce, 
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
                default:
                    var damageString = data.DamageString;
                    if (damageString == null)
                    {
                        outgoingDamage = ability.GetDamage(level - 1);

                        // Convert.ToSingle(
                        // Game.FindKeyValues(name + "/AbilityDamage", KeyValueSource.Ability)
                        // .StringValue.Split(' ')[level - 1]);
                    }
                    else
                    {
                        if (ability.AbilityType.HasFlag(AbilityType.Ultimate) && level > 0 && source.AghanimState())
                        {
                            level -= 1;
                        }

                        if (data.SpellLevel != null)
                        {
                            var spellLevel = source.FindSpell(data.SpellLevel);
                            level = spellLevel.Level;
                        }

                        if (source.ClassID == ClassID.CDOTA_Unit_Hero_Invoker && level > 0 && source.AghanimState())
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

                        // Console.WriteLine(outgoingDamage + " " + ability.Name + " " + GetDamageType(ability));
                    }

                    // Console.WriteLine(outgoingDamage);
                    outgoingDamage = target.DamageTaken(
                        outgoingDamage, 
                        GetDamageType(ability), 
                        source, 
                        data.MagicImmunityPierce, 
                        minusMagicResistancePerc: minusMagicResistancePerc);
                    break;
            }

            var aetherLens = source.FindItem("item_aether_lens");
            if (aetherLens != null && ability.StoredName() != "axe_culling_blade")
            {
                outgoingDamage *= 1 + (aetherLens.GetAbilityData("spell_amp") / 100);
            }

            if (source.ClassID == ClassID.CDOTA_Unit_Hero_Zuus && !(ability is Item)
                && (source.Distance2D(target) <= 1200 || ability.StoredName() != "zuus_thundergods_wrath"))
            {
                var staticField = source.Spellbook.Spell3;
                if (staticField.Level > 0)
                {
                    var bonusDmg = staticField.GetAbilityData("damage_health_pct") / 100 * (target.Health - minusHealth);
                    bonusDmg = target.DamageTaken(
                        bonusDmg, 
                        DamageType.Magical, 
                        source, 
                        minusMagicResistancePerc: minusMagicResistancePerc) * (aetherLens == null ? 1 : 1.08f);
                    outgoingDamage += bonusDmg;
                }
            }

            return outgoingDamage;
        }

        /// <summary>
        ///     Returns DamageType of ability
        /// </summary>
        /// <param name="ability"></param>
        /// <returns></returns>
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

            // Console.WriteLine(ability.Name.Substring(0, "item_dagon".Length));
            return type;
        }

        #endregion
    }
}