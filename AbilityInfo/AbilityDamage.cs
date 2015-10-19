namespace Ensage.Common.AbilityInfo
{
    using System;
    using System.Linq;

    using Ensage.Common.Extensions;

    using EnsageSharp.Sandbox;

    /// <summary>
    ///     Class used to calculate damage from most abilities
    /// </summary>
    public class AbilityDamage
    {
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
            var data = AbilityDatabase.Find(name);
            if (data == null || data.IsNuke == false)
            {
                return 0;
            }
            var outgoingDamage = 0f;
            switch (name)
            {
                case "ember_spirit_sleight_of_fist":
                    outgoingDamage = source.MinimumDamage + source.BonusDamage;
                    outgoingDamage += ability.GetAbilityData(data.BonusDamageString);
                    outgoingDamage = target.DamageTaken(
                        outgoingDamage,
                        DamageType.Physical,
                        source,
                        data.MagicImmunityPierce);
                    break;
                case "doom_bringer_lvl_death":
                    var tempDmg = ability.GetAbilityData(data.DamageString);
                    var multiplier = ability.GetAbilityData("lvl_bonus_multiple");
                    var bonusDamage = ability.GetAbilityData("lvl_bonus_damage");
                    var levelc = target.Level / multiplier;
                    if (levelc == Math.Floor(levelc) || target.Level == 25)
                    {
                        tempDmg += target.MaximumHealth * (bonusDamage / 100);
                    }
                    outgoingDamage = target.DamageTaken(
                        tempDmg,
                        DamageType.Magical,
                        source,
                        data.MagicImmunityPierce);
                    break;
                case "phantom_assassin_phantom_strike":
                    var crit = source.Spellbook.SpellR;
                    if (crit.Level > 0)
                    {
                        var critMulti = crit.GetAbilityData("crit_bonus");
                        outgoingDamage = (source.MinimumDamage + source.BonusDamage) * (critMulti / 100);
                    }
                    outgoingDamage = target.DamageTaken(
                        outgoingDamage,
                        DamageType.Physical,
                        source,
                        data.MagicImmunityPierce);
                    break;
                case "mirana_starfall":
                    var radiusMax = ability.GetAbilityData("starfall_secondary_radius");
                    outgoingDamage =
                        Convert.ToSingle(
                            Game.FindKeyValues(name + "/AbilityDamage", KeyValueSource.Ability).StringValue.Split(' ')[
                                level - 1]);
                    outgoingDamage = target.DamageTaken(
                        outgoingDamage,
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
                                         agiMultiplier * source.TotalAgility + (source.MinimumDamage + source.BonusDamage),
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