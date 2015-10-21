namespace Ensage.Common.AbilityInfo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.Common.Extensions;


    /// <summary>
    ///     Class used to calculate damage from most abilities
    /// </summary>
    public class AbilityDamage
    {
        private static readonly Dictionary<Ability,float> damageDictionary = new Dictionary<Ability, float>();
        public static Dictionary<Ability,AbilityInfo> dataDictionary = new Dictionary<Ability, AbilityInfo>();
        public static Dictionary<Ability,uint> levelDictionary = new Dictionary<Ability, uint>(); 

        #region Public Methods and Operators

        /// <summary>
        ///     Calculates damage from given ability on given target
        /// </summary>
        /// <param name="ability"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static float CalculateDamage(Ability ability, Hero source, Unit target)
        {
            var name = ability.Name;
            var level = ability.Level;
            AbilityInfo data;
            if (!dataDictionary.TryGetValue(ability, out data))
            {
                data = AbilityDatabase.Find(name);
                if (data != null && data.IsNuke)
                {
                    dataDictionary.Add(ability,data);
                }
            }

            //var data = AbilityDatabase.Find(name);
            if (data == null || !data.IsNuke)
            {
                return 0;
            }

            var outgoingDamage = 0f;
            float bonusDamage;
            switch (name)
            {
                case "ember_spirit_sleight_of_fist":
                    outgoingDamage = source.MinimumDamage + source.BonusDamage;
                    if (!damageDictionary.TryGetValue(ability, out bonusDamage))
                    {
                        bonusDamage = ability.GetAbilityData(data.BonusDamageString);
                        outgoingDamage += bonusDamage;
                        damageDictionary.Add(ability,bonusDamage);
                        levelDictionary.Add(ability,ability.Level);
                    }
                    else if (levelDictionary[ability] != ability.Level)
                    {
                        levelDictionary[ability] = ability.Level;
                        bonusDamage = ability.GetAbilityData(data.BonusDamageString);
                        damageDictionary[ability] = bonusDamage;
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
                        data.MagicImmunityPierce);
                    break;
                case "doom_bringer_lvl_death":
                    float tempDmg;
                    if (!damageDictionary.TryGetValue(ability, out tempDmg))
                    {
                        tempDmg = ability.GetAbilityData(data.DamageString);
                        damageDictionary.Add(ability, tempDmg);
                        levelDictionary.Add(ability, ability.Level);
                    }
                    else if (levelDictionary[ability] != ability.Level)
                    {
                        levelDictionary[ability] = ability.Level;
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
                    outgoingDamage = target.DamageTaken(tempDmg, DamageType.Magical, source, data.MagicImmunityPierce);
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
                    outgoingDamage = target.DamageTaken(
                        outgoingDamage,
                        DamageType.Physical,
                        source,
                        data.MagicImmunityPierce);
                    break;
                case "templar_assassin_meld":
                    if (!damageDictionary.TryGetValue(ability, out bonusDamage))
                    {
                        bonusDamage = ability.GetAbilityData(data.DamageString);
                        damageDictionary.Add(ability, bonusDamage);
                        levelDictionary.Add(ability, ability.Level);
                    }
                    else if (levelDictionary[ability] != ability.Level)
                    {
                        levelDictionary[ability] = ability.Level;
                        bonusDamage = ability.GetAbilityData(data.DamageString);
                        damageDictionary[ability] = bonusDamage;
                    }
                    //var minusArmor = ability.GetAbilityData("bonus_armor");
                    var minusArmors = new [] { -2, -4, -6, -8 };
                    var minusArmor = target.Armor + minusArmors[ability.Level - 1];
                    //Console.WriteLine(minusArmor);
                    var damageIncrease = 1 - 0.06 * minusArmor / (1 + 0.06 * Math.Abs(minusArmor));
                    //Console.WriteLine(damageIncrease);
                    outgoingDamage = (float)(target.DamageTaken(
                        ((source.MaximumDamage + source.BonusDamage)),
                        DamageType.Physical,
                        source,
                        data.MagicImmunityPierce) + bonusDamage * damageIncrease);
                    break;
                case "mirana_starfall":
                    var radiusMax = ability.GetAbilityData("starfall_secondary_radius");
                    if (!damageDictionary.TryGetValue(ability, out bonusDamage))
                    {
                        bonusDamage = Convert.ToSingle(
                            Game.FindKeyValues(name + "/AbilityDamage", KeyValueSource.Ability).StringValue.Split(' ')[
                                level - 1]);
                        damageDictionary.Add(ability, bonusDamage);
                        levelDictionary.Add(ability, ability.Level);
                    }
                    else if (levelDictionary[ability] != ability.Level)
                    {
                        levelDictionary[ability] = ability.Level;
                        bonusDamage =
                            Convert.ToSingle(
                                Game.FindKeyValues(name + "/AbilityDamage", KeyValueSource.Ability)
                                    .StringValue.Split(' ')[level - 1]);
                        damageDictionary[ability] = bonusDamage;
                    }
                    outgoingDamage = target.DamageTaken(
                        bonusDamage,
                        DamageType.Magical,
                        source,
                        data.MagicImmunityPierce);
                    if (source.Distance2D(target) < radiusMax)
                    {
                        outgoingDamage *= 2;
                    }
                    break;
                case "nyx_assassin_mana_burn":
                    var intMultiplier = ability.GetAbilityData("float_multiplier");
                    var hero = target as Hero;
                    outgoingDamage = target.ManaBurnDamageTaken(
                        hero.TotalIntelligence * intMultiplier,
                        1,
                        DamageType.Magical,
                        source);
                    break;
                case "riki_blink_strike":
                    var damage = ability.GetAbilityData(data.DamageString);
                    var backstab = source.Spellbook.SpellE;
                    var agiMultiplier = backstab.GetAbilityData("damage_multiplier");
                    var blinkdamage = target.DamageTaken(damage, DamageType.Magical, source, data.MagicImmunityPierce);
                    outgoingDamage = blinkdamage
                                     + target.DamageTaken(
                                         agiMultiplier * source.TotalAgility
                                         + (source.MinimumDamage + source.BonusDamage),
                                         DamageType.Physical,
                                         source,
                                         data.MagicImmunityPierce);
                    break;
                case "undying_soul_rip":
                    var radius = ability.GetAbilityData("radius");
                    var nearUnits =
                        ObjectMgr.GetEntities<Unit>()
                            .Where(
                                x =>
                                (x.ClassID == ClassID.CDOTA_BaseNPC_Creep_Lane
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
                                && x.IsVisible && x.Distance2D(source) < (radius + x.HullRadius));
                    var damagePerUnit = ability.GetAbilityData("damage_per_unit");
                    var maxUnits = ability.GetAbilityData("max_units");
                    outgoingDamage = Math.Min(nearUnits.Count(), maxUnits) * damagePerUnit;
                    outgoingDamage = target.DamageTaken(
                        outgoingDamage,
                        DamageType.Magical,
                        source,
                        data.MagicImmunityPierce);
                    break;
                default:
                    var damageString = data.DamageString;
                    if (damageString == null)
                    {
                        outgoingDamage =
                            Convert.ToSingle(
                                Game.FindKeyValues(name + "/AbilityDamage", KeyValueSource.Ability)
                                    .StringValue.Split(' ')[level - 1]);
                    }
                    else
                    {
                        if (data.SpellLevel != null)
                        {
                            var spellLevel = source.FindSpell(data.SpellLevel);
                            level = spellLevel.Level;
                        }
                        if (source.AghanimState() && data.DamageScepterString != null)
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
                        outgoingDamage = target.DamageTaken(
                            outgoingDamage,
                            GetDamageType(ability),
                            source,
                            data.MagicImmunityPierce);
                    }
                    break;
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
            if (ability.Name == "abaddon_aphotiv_shield")
            {
                type = DamageType.Magical;
            }
            else if (ability.Name == "meepo_poof")
            {
                type = DamageType.Magical;
            }
            else if (ability.Name == "axe_culling_blade")
            {
                type = DamageType.Pure;
            }
            else if (ability.Name == "invoker_sun_strinke")
            {
                type = DamageType.Pure;
            }
            else if (ability.Name == "alchemist_unstable_concoction_throw")
            {
                type = DamageType.Physical;
            }
            else if (ability.Name == "centaur_stampede")
            {
                type = DamageType.Physical;
            }
            else if (ability.Name == "lina_laguna_blade")
            {
                if ((ability.Owner as Hero).AghanimState())
                {
                    type = DamageType.Pure;
                }
            }
            else if (ability.Name == "legion_commander_duel")
            {
                type = DamageType.Physical;
            }
            else if (ability.Name.Substring(0, "item_dagon".Length) == "item_dagon")
            {
                type = DamageType.Magical;
            }
            return type;
        }

        #endregion
    }
}