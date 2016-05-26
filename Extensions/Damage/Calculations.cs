// <copyright file="Calculations.cs" company="EnsageSharp">
//    Copyright (c) 2016 EnsageSharp.
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
namespace Ensage.Common.Extensions.Damage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.Common.Objects;

    /// <summary>
    ///     The calculations.
    /// </summary>
    public static class Calculations
    {
        #region Static Fields

        /// <summary>
        ///     The damage blocks list.
        /// </summary>
        private static readonly List<DamageBlocks> DamageBlocksList = new List<DamageBlocks>
                                                                          {
                                                                              new DamageBlocks(
                                                                                  "modifier_item_crimson_guard_extra", 
                                                                                  "block_damage_melee_active", 
                                                                                  "block_damage_ranged_active", 
                                                                                  "item_crimson_guard", 
                                                                                  true), 
                                                                              new DamageBlocks(
                                                                                  "modifier_tidehunter_kraken_shell", 
                                                                                  "damage_reduction", 
                                                                                  "damage_reduction", 
                                                                                  "tidehunter_kraken_shell", 
                                                                                  false), 
                                                                              new DamageBlocks(
                                                                                  "modifier_item_crimson_guard", 
                                                                                  "block_damage_melee", 
                                                                                  "block_damage_ranged", 
                                                                                  "item_crimson_guard", 
                                                                                  true), 
                                                                              new DamageBlocks(
                                                                                  "modifier_item_vanguard", 
                                                                                  "block_damage_melee", 
                                                                                  "block_damage_ranged", 
                                                                                  "item_vanguard", 
                                                                                  true), 
                                                                              new DamageBlocks(
                                                                                  "modifier_item_poor_mans_shield", 
                                                                                  "damage_block_melee", 
                                                                                  "damage_block_ranged", 
                                                                                  "item_poor_mans_shield", 
                                                                                  true), 
                                                                              new DamageBlocks(
                                                                                  "modifier_item_stout_shield", 
                                                                                  "damage_block_melee", 
                                                                                  "damage_block_ranged", 
                                                                                  "item_stout_shield", 
                                                                                  true)
                                                                          };

        /// <summary>
        ///     The external damage amps.
        /// </summary>
        private static readonly List<ExternalDmgAmps> ExternalDmgAmps = new List<ExternalDmgAmps>();

        /// <summary>
        ///     The external damage reductions.
        /// </summary>
        private static readonly List<ExternalDmgReductions> ExternalDmgReductions = new List<ExternalDmgReductions>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="Calculations" /> class.
        /// </summary>
        static Calculations()
        {
            ExternalDmgAmps.Add(
                new ExternalDmgAmps
                    {
                        ModifierName = "modifier_shadow_demon_soul_catcher", SourceTeam = -1, Amp = "bonus_damage_taken", 
                        SourceSpellName = "shadow_demon_soul_catcher", HeroId = ClassID.CDOTA_Unit_Hero_Shadow_Demon, 
                        Type = DamageType.Pure
                    });

            ExternalDmgAmps.Add(
                new ExternalDmgAmps
                    {
                        ModifierName = "modifier_bloodseeker_bloodrage", SourceTeam = -2, Amp = "damage_increase_pct", 
                        SourceSpellName = "bloodseeker_bloodrage", HeroId = ClassID.CDOTA_Unit_Hero_Bloodseeker, 
                        Type = DamageType.Pure
                    });

            ExternalDmgAmps.Add(
                new ExternalDmgAmps
                    {
                        ModifierName = "modifier_slardar_sprint", SourceSpellName = "slardar_sprint", Amp = "bonus_damage", 
                        HeroId = ClassID.CDOTA_Unit_Hero_Slardar
                    });

            ExternalDmgAmps.Add(
                new ExternalDmgAmps
                    {
                        ModifierName = "modifier_oracle_fates_edict", SourceSpellName = "oracle_fates_edict", 
                        HeroId = ClassID.CDOTA_Unit_Hero_Oracle, Amp = "damage_amp"
                    });

            ExternalDmgAmps.Add(
                new ExternalDmgAmps
                    {
                        ModifierName = "modifier_item_mask_of_madness_berserk", SourceSpellName = "item_mask_of_madness", 
                        Amp = "berserk_extra_damage"
                    });

            ExternalDmgReductions.Add(
                new ExternalDmgReductions
                    {
                        ModifierName = "modifier_wisp_overcharge", SourceSpellName = "wisp_overcharge", 
                        HeroID = ClassID.CDOTA_Unit_Hero_Wisp, Reduce = "bonus_damage_pct", Type = 1, SourceTeam = 1
                    });

            ExternalDmgReductions.Add(
                new ExternalDmgReductions
                    {
                        ModifierName = "modifier_spectre_dispersion", SourceTeam = 0, Reduce = "damage_reflection_pct", 
                        HeroID = ClassID.CDOTA_Unit_Hero_Spectre, SourceSpellName = "spectre_dispersion", Type = 1
                    });

            ExternalDmgReductions.Add(
                new ExternalDmgReductions
                    {
                        ModifierName = "modifier_nyx_assassin_burrow", SourceSpellName = "nyx_assassin_burrow", 
                        HeroID = ClassID.CDOTA_Unit_Hero_Nyx_Assassin, Reduce = "damage_reduction", Type = 1
                    });

            ExternalDmgReductions.Add(
                new ExternalDmgReductions
                    {
                        ModifierName = "modifier_winter_wyvern_winters_curse", 
                        HeroID = ClassID.CDOTA_Unit_Hero_Winter_Wyvern, SourceSpellName = "winter_wyvern_winters_curse", 
                        Reduce = "damage_reduction", Type = 1
                    });

            ExternalDmgReductions.Add(
                new ExternalDmgReductions
                    {
                        ModifierName = "modifier_ursa_enrage", SourceSpellName = "ursa_enrage", 
                        HeroID = ClassID.CDOTA_Unit_Hero_Ursa, Reduce = "damage_reduction", Type = 1
                    });

            ExternalDmgReductions.Add(
                new ExternalDmgReductions
                    {
                        ModifierName = "modifier_templar_assassin_refraction_absorb", 
                        HeroID = ClassID.CDOTA_Unit_Hero_TemplarAssassin, SourceSpellName = "templar_assassin_refraction", 
                        Type = 1
                    });

            ExternalDmgReductions.Add(
                new ExternalDmgReductions
                    {
                        ModifierName = "modifier_treant_living_armor", Type = 0, SourceTeam = 1, 
                        SourceSpellName = "treant_living_armor", HeroID = ClassID.CDOTA_Unit_Hero_Treant, 
                        Reduce = "damage_block"
                    });

            ExternalDmgReductions.Add(
                new ExternalDmgReductions
                    {
                        ModifierName = "modifier_abaddon_aphotic_shield", Type = 2, SourceTeam = 1, 
                        SourceSpellName = "abaddon_aphotic_shield", HeroID = ClassID.CDOTA_Unit_Hero_Abaddon, 
                        Reduce = "damage_absorb"
                    });

            ExternalDmgReductions.Add(
                new ExternalDmgReductions
                    {
                        ModifierName = "modifier_ember_spirit_flame_guard", Type = 0, SourceTeam = 0, 
                        HeroID = ClassID.CDOTA_Unit_Hero_EmberSpirit, SourceSpellName = "ember_spirit_flame_guard", 
                        Reduce = "absorb_amount", MagicOnly = true
                    });

            ExternalDmgReductions.Add(
                new ExternalDmgReductions
                    {
                        ModifierName = "modifier_item_pipe_barrier", Type = 0, SourceSpellName = "item_pipe", 
                        Reduce = "barrier_block", MagicOnly = true
                    });
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Returns actual damage the unit takes
        /// </summary>
        /// <param name="target">
        ///     damaged unit
        /// </param>
        /// <param name="dmg">
        ///     amount of damage
        /// </param>
        /// <param name="dmgType">
        ///     Type of damage (Magical/Physical/Pure/Health removal)
        /// </param>
        /// <param name="source">
        ///     source of the damage
        /// </param>
        /// <param name="throughBKB">
        ///     true if the damage pierces magic immunity
        /// </param>
        /// <param name="minusArmor">
        ///     The minus Armor.
        /// </param>
        /// <param name="minusDamageResistancePerc">
        ///     The minus Damage Resistance Perc.
        /// </param>
        /// <param name="minusMagicResistancePerc">
        ///     The minus Magic Resistance Perc.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float DamageTaken(
            this Unit target, 
            float dmg, 
            DamageType dmgType, 
            Unit source, 
            bool throughBKB = false, 
            double minusArmor = 0d, 
            double minusDamageResistancePerc = 0d, 
            double minusMagicResistancePerc = 0d)
        {
            if (target.IsInvul())
            {
                return 0;
            }

            var tempDmg = Math.Floor(dmg);
            var amp = 0d;
            var ampFromMe = 0d;
            var reduceProc = 0d;
            var reduceOther = 0d;
            var reduceStatic = 0d;
            var reduceBlock = 0d;
            var manaShield = 0d;
            var magOnly = 0d;
            var aa = 0d;
            var modifiers = target.Modifiers.ToList();
            var bristleback = false;
            var centaurStampede = false;
            var medusaManaShield = false;
            var undyingFleshGolem = false;
            var abaddonRedirect = false;
            var kunkkaGhostShipAbsorb = false;
            var iceBlast = false;
            var chenPenitence = false;

            if (target.IsAttackImmune() && dmgType == DamageType.Physical)
            {
                return 0;
            }

            if ((dmgType == DamageType.Magical || dmgType == DamageType.Pure) && !throughBKB && target.IsMagicImmune())
            {
                return 0;
            }

            foreach (var name in modifiers.Select(modifier => modifier.Name))
            {
                if (dmgType == DamageType.Physical)
                {
                    foreach (var damageBlock in DamageBlocksList)
                    {
                        if (damageBlock.ModifierName != name)
                        {
                            continue;
                        }

                        Ability ability = null;
                        foreach (var hero in Heroes.All)
                        {
                            if (!damageBlock.Item)
                            {
                                ability = hero.FindSpell(damageBlock.AbilityName, true);
                                if (ability != null)
                                {
                                    break;
                                }
                            }

                            ability = hero.FindItem(damageBlock.AbilityName, true);
                            if (ability != null)
                            {
                                break;
                            }
                        }

                        if (ability == null)
                        {
                            continue;
                        }

                        reduceBlock =
                            ability.GetAbilityData(target.IsRanged ? damageBlock.RangedBlock : damageBlock.MeleeBlock);
                        break;
                    }
                }

                switch (name)
                {
                    case "modifier_winter_wyvern_cold_embrace":
                        if (dmgType == DamageType.Physical)
                        {
                            return 0;
                        }

                        break;
                    case "modifier_omninight_guardian_angel":
                        if (dmgType == DamageType.Physical)
                        {
                            return 0;
                        }

                        break;
                    case "modifier_abaddon_borrowed_time":
                        return 0;
                    case "modifier_bristleback_bristleback":
                        bristleback = true;
                        break;
                    case "modifier_centaur_stampede":
                        centaurStampede = true;
                        break;
                    case "modifier_medusa_mana_shield":
                        medusaManaShield = true;
                        break;
                    case "modifier_undying_flesh_golem_plague_aura":
                        undyingFleshGolem = true;
                        break;
                    case "modifier_abaddon_borrowed_time_damage_redirect":
                        abaddonRedirect = true;
                        break;
                    case "modifier_kunkka_ghost_ship_damage_absorb":
                        kunkkaGhostShipAbsorb = true;
                        break;
                    case "modifier_ice_blast":
                        iceBlast = true;
                        break;
                    case "chen_penitence":
                        chenPenitence = true;
                        break;
                }
            }

            foreach (var v in ExternalDmgAmps.Where(v => modifiers.Any(x => x.Name == v.ModifierName)))
            {
                Ability ability = null;
                foreach (var hero in Heroes.All)
                {
                    if (v.HeroId == hero.ClassID || hero.ClassID == ClassID.CDOTA_Unit_Hero_Rubick)
                    {
                        ability = hero.FindSpell(v.SourceSpellName, true);
                        if (ability != null)
                        {
                            break;
                        }
                    }

                    ability = hero.FindItem(v.SourceSpellName, true);
                    if (ability != null)
                    {
                        break;
                    }
                }

                // var burst = 0f;
                if (ability == null)
                {
                    continue;
                }

                var burst = ability.GetAbilityData(v.Amp) / 100;
                if (v.SourceTeam == -1 && ability.Owner.Team != target.Team)
                {
                    amp += burst;
                }
                else if (v.SourceTeam == -2)
                {
                    if (target.Distance2D(source) < 2200)
                    {
                        amp += burst;
                    }
                    else
                    {
                        amp += burst / 2;
                    }
                }
                else
                {
                    amp += burst;
                }
            }

            foreach (var v in ExternalDmgReductions.Where(v => modifiers.Any(x => x.Name == v.ModifierName)))
            {
                Ability ability = null;
                foreach (var hero in Heroes.All)
                {
                    if (v.HeroID == hero.ClassID || hero.ClassID == ClassID.CDOTA_Unit_Hero_Rubick)
                    {
                        ability = hero.FindSpell(v.SourceSpellName, true);
                        if (ability != null)
                        {
                            break;
                        }
                    }

                    ability = hero.FindItem(v.SourceSpellName, true);
                    if (ability != null)
                    {
                        break;
                    }
                }

                // var burst = 0f;
                if (ability == null)
                {
                    continue;
                }

                var burst = Math.Abs(ability.GetAbilityData(v.Reduce) / 100);
                if (ability.StoredName() == "wisp_overcharge")
                {
                    burst = (float)new[] { 0.05, 0.10, 0.15, 0.20 }[ability.Level - 1];
                }

                if (ability.StoredName() == "templar_assassin_refraction")
                {
                    burst = 1;
                }

                if (v.Type == 1)
                {
                    if (v.SourceTeam == 1 && ability.Owner.Team == target.Team)
                    {
                        if (burst > 1)
                        {
                            reduceBlock += burst;
                        }
                        else
                        {
                            reduceProc += burst;
                        }
                    }
                    else if (v.SourceTeam == 0)
                    {
                        if (burst > 1)
                        {
                            reduceBlock += burst;
                        }
                        else
                        {
                            reduceProc += burst;
                        }
                    }
                }
                else if (v.Type == 2)
                {
                    if (v.SourceTeam == 1 && ability.Owner.Team == target.Team)
                    {
                        reduceBlock += burst * 100;
                    }
                    else if (v.SourceTeam == 0)
                    {
                        reduceBlock += burst * 100;
                    }
                }
                else if (!v.MagicOnly || dmgType == DamageType.Magical)
                {
                    if (v.SourceTeam == 1 && ability.Owner.Team == target.Team)
                    {
                        if (!v.MagicOnly)
                        {
                            reduceStatic += burst;
                        }
                        else
                        {
                            magOnly += burst;
                        }
                    }
                    else if (v.SourceTeam == 0)
                    {
                        if (!v.MagicOnly)
                        {
                            reduceStatic += burst;
                        }
                        else
                        {
                            magOnly += burst;
                        }
                    }
                }
                else
                {
                    reduceStatic += burst;
                }
            }

            if (bristleback)
            {
                var spell = target.FindSpell("bristleback_bristleback", true);
                if (spell != null)
                {
                    var burst = 0d;
                    var angle = target.FindRelativeAngle(source.Position) % (2 * Math.PI * 180) / Math.PI;
                    if (angle >= 110 && angle <= 250)
                    {
                        burst = (1 + spell.Level) * 0.08;
                    }
                    else if (angle >= 70 && angle <= 290)
                    {
                        burst = (1 + spell.Level) * 0.04;
                    }

                    reduceProc += burst;
                }
            }

            if (centaurStampede)
            {
                var heroes =
                    Heroes.All.Where(
                        x =>
                        x.IsValid && !x.IsIllusion()
                        && (x.ClassID == ClassID.CDOTA_Unit_Hero_Centaur || x.ClassID == ClassID.CDOTA_Unit_Hero_Rubick)
                        && x.AghanimState());
                reduceProc = heroes.Aggregate(reduceProc, (current, hero) => current + 0.7);
            }

            if (medusaManaShield)
            {
                var spell = target.FindSpell("medusa_mana_shield", true);
                if (spell != null)
                {
                    var treshold = spell.GetAbilityData("damage_per_mana");
                    double burst;
                    if (target.Mana >= tempDmg * .6 / treshold)
                    {
                        burst = 0.6;
                    }
                    else
                    {
                        burst = target.Mana * treshold / tempDmg;
                    }

                    manaShield = burst;
                }
            }

            if (undyingFleshGolem)
            {
                var spell = Abilities.FindAbility("undying_flesh_golem");
                if (spell != null)
                {
                    var baseAmp = .05 * spell.Level;
                    var owner = spell.Owner as Hero;
                    if (owner.AghanimState())
                    {
                        baseAmp = baseAmp + .1;
                    }

                    var distance = target.Distance2D(spell.Owner);
                    if (distance <= 200)
                    {
                        amp += baseAmp + 0.15;
                    }
                    else if (distance > 750)
                    {
                        amp += baseAmp;
                    }
                    else
                    {
                        amp += baseAmp + (750 - distance) * 0.03 / 110;
                    }
                }
            }

            if (abaddonRedirect)
            {
                reduceOther += 0.35;
            }
            else if (kunkkaGhostShipAbsorb)
            {
                reduceOther += 0.5;
            }

            var sourceModifiers = source.Modifiers.ToList();
            var bloodseekerBloodrage = false;
            var silverEdge = false;

            foreach (var name in sourceModifiers.Select(modifier => modifier.Name))
            {
                if (name == "modifier_bloodseeker_bloodrage")
                {
                    bloodseekerBloodrage = true;
                }
                else if (name == "modifier_silver_edge_debuff")
                {
                    silverEdge = true;
                }
            }

            if (bloodseekerBloodrage)
            {
                var spell = Abilities.FindAbility("bloodseeker_bloodrage");
                if (spell != null)
                {
                    var bloodrite = spell.GetAbilityData("damage_increase_pct");
                    if (target.Distance2D(source) > 2200)
                    {
                        bloodrite /= 2;
                    }

                    ampFromMe += bloodrite;
                }
            }

            if (silverEdge)
            {
                ampFromMe -= 0.4;
            }

            if (iceBlast)
            {
                var spell = Abilities.FindAbility("ancient_apparition_ice_blast", target.GetEnemyTeam());
                if (spell != null)
                {
                    var treshold = spell.GetAbilityData("kill_pct") / 100;
                    aa = Math.Floor(treshold / target.MaximumHealth);
                }
            }

            switch (dmgType)
            {
                case DamageType.Magical:

                    // Console.WriteLine(minusMagicResistancePerc/100);
                    var resist = 1 - (1 - target.MagicDamageResist) * (1 + (float)minusMagicResistancePerc / 100);
                    tempDmg =
                        (float)
                        ((tempDmg * (1 - manaShield - reduceOther) - magOnly) * (1 + amp - reduceProc) * (1 + ampFromMe)
                         * (1 - resist) - reduceStatic + aa);
                    break;
                case DamageType.Pure:
                    if (!throughBKB && target.IsMagicImmune())
                    {
                        tempDmg = 0;
                    }
                    else
                    {
                        tempDmg =
                            (float)
                            (tempDmg * (1 - manaShield - reduceOther) * (1 + amp - reduceProc) * (1 + ampFromMe)
                             - reduceStatic + aa);
                    }

                    break;
                case DamageType.Physical:
                    if (target.IsAttackImmune())
                    {
                        return 0;
                    }

                    if (!throughBKB)
                    {
                        if (chenPenitence)
                        {
                            var ability = Abilities.FindAbility("chen_penitence", target.GetEnemyTeam());
                            if (ability != null)
                            {
                                var bonus = ability.GetAbilityData("bonus_damage_taken");
                                amp += bonus / 100;
                            }
                        }
                    }

                    tempDmg =
                        (float)
                        ((tempDmg * (1 - manaShield - reduceOther) - reduceBlock) * (1 + amp - reduceProc)
                         * (1 + ampFromMe) * (1 - target.DamageResist * (1 - minusDamageResistancePerc / 100))
                         + 0.06 * minusArmor / (1 + 0.06 * Math.Abs(minusArmor))) - reduceStatic + aa;
                    break;
                case DamageType.HealthRemoval:
                    break;
            }

            return (float)Math.Max(tempDmg, 0);
        }

        /// <summary>
        ///     The spell damage taken.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="dmg">
        ///     The dmg.
        /// </param>
        /// <param name="dmgType">
        ///     The dmg type.
        /// </param>
        /// <param name="source">
        ///     The source.
        /// </param>
        /// <param name="spellName">
        ///     The spell name.
        /// </param>
        /// <param name="throughBKB">
        ///     The through bkb.
        /// </param>
        /// <param name="minusArmor">
        ///     The minus armor.
        /// </param>
        /// <param name="minusDamageResistancePerc">
        ///     The minus damage resistance perc.
        /// </param>
        /// <param name="minusMagicResistancePerc">
        ///     The minus magic resistance perc.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float SpellDamageTaken(
            this Unit target, 
            float dmg, 
            DamageType dmgType, 
            Unit source, 
            string spellName, 
            bool throughBKB = false, 
            double minusArmor = 0d, 
            double minusDamageResistancePerc = 0d, 
            double minusMagicResistancePerc = 0d)
        {
            var damage = dmg;
            if (spellName != "axe_culling_blade")
            {
                foreach (var item in source.Inventory.Items)
                {
                    if (item.StoredName() == "item_aether_lens")
                    {
                        damage *= 1f + item.GetAbilityData("spell_amp") / 100f;
                    }
                }
            }

            var hero = source as Hero;

            if (hero != null && spellName != "axe_culling_blade")
            {
                damage *= 1f + hero.TotalIntelligence / 16f / 100f;
            }

            var taken = target.DamageTaken(
                damage, 
                dmgType, 
                source, 
                throughBKB, 
                minusArmor, 
                minusDamageResistancePerc, 
                minusMagicResistancePerc);
            return taken;
        }

        #endregion
    }
}