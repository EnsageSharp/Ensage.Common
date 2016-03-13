// <copyright file="EntityExtensions.cs" company="EnsageSharp">
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
namespace Ensage.Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.Common.Objects;
    using Ensage.Heroes;

    using global::SharpDX;

    internal class ExternalDmgAmps
    {
        #region Fields

        public string Amp;

        public ClassID HeroId;

        public string ModifierName;

        public string SourceSpellName;

        public double SourceTeam;

        public DamageType Type;

        #endregion

        #region Constructors and Destructors

        public ExternalDmgAmps()
        {
        }

        public ExternalDmgAmps(
            string modifierName, 
            double sourceTeam, 
            string amp, 
            string sourceSpellName, 
            ClassID heroId, 
            DamageType type)
        {
            this.ModifierName = modifierName;
            this.SourceTeam = sourceTeam;
            this.Amp = amp;
            this.SourceSpellName = sourceSpellName;
            this.HeroId = heroId;
            this.Type = type;
        }

        #endregion
    }

    internal class ExternalDmgReductions
    {
        #region Fields

        public ClassID HeroID;

        public bool MagicOnly;

        public string ModifierName;

        public string Reduce;

        public string SourceSpellName;

        public double SourceTeam;

        public float Type;

        #endregion

        #region Constructors and Destructors

        public ExternalDmgReductions()
        {
        }

        public ExternalDmgReductions(
            string modifierName, 
            double sourceTeam, 
            string reduce, 
            string sourceSpellName, 
            ClassID heroId, 
            float type, 
            bool magicOnly)
        {
            this.ModifierName = modifierName;
            this.SourceTeam = sourceTeam;
            this.Reduce = reduce;
            this.SourceSpellName = sourceSpellName;
            this.HeroID = heroId;
            this.Type = type;
            this.MagicOnly = magicOnly;
        }

        #endregion
    }

    internal class DamageBlocks
    {
        #region Fields

        public string AbilityName;

        public bool Item;

        public string MeleeBlock;

        public string ModifierName;

        public string RangedBlock;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DamageBlocks" /> class.
        /// </summary>
        /// <param name="modifierName">
        ///     The modifier name.
        /// </param>
        /// <param name="meleeBlock">
        ///     The melee block.
        /// </param>
        /// <param name="rangedBlock">
        ///     The ranged block.
        /// </param>
        /// <param name="abilityName">
        ///     The ability name.
        /// </param>
        public DamageBlocks(string modifierName, string meleeBlock, string rangedBlock, string abilityName, bool item)
        {
            this.ModifierName = modifierName;
            this.MeleeBlock = meleeBlock;
            this.RangedBlock = rangedBlock;
            this.AbilityName = abilityName;
            this.Item = item;
        }

        #endregion
    }

    /// <summary>
    /// </summary>
    public static class EntityExtensions
    {
        #region Static Fields

        /// <summary>
        /// </summary>
        public static Dictionary<string, Item> ItemDictionary = new Dictionary<string, Item>();

        private static readonly Dictionary<string, bool> BoolDictionary = new Dictionary<string, bool>();

        private static readonly List<ExternalDmgAmps> ExternalDmgAmps = new List<ExternalDmgAmps>();

        private static readonly List<ExternalDmgReductions> ExternalDmgReductions = new List<ExternalDmgReductions>();

        private static readonly Dictionary<uint, double> TurnrateDictionary = new Dictionary<uint, double>();

        private static List<DamageBlocks> damageBlocksList = new List<DamageBlocks>
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
        ///     The modifier dictionary.
        /// </summary>
        private static Dictionary<string, bool> modifierBoolDictionary = new Dictionary<string, bool>();

        /// <summary>
        ///     The modifier dictionary.
        /// </summary>
        private static Dictionary<string, Modifier> modifierDictionary = new Dictionary<string, Modifier>();

        private static Dictionary<float, float> RangeDictionary = new Dictionary<float, float>();

        #endregion

        #region Constructors and Destructors

        static EntityExtensions()
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
                        ModifierName = "modifier_slardar_sprint", SourceSpellName = "slardar_sprint", Amp = "bonus_damage"
                    });

            ExternalDmgAmps.Add(
                new ExternalDmgAmps
                    {
                        ModifierName = "modifier_oracle_fates_edict", SourceSpellName = "oracle_fates_edict", 
                        Amp = "damage_amp"
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
                        Reduce = "bonus_damage_pct", Type = 1, SourceTeam = 1
                    });

            ExternalDmgReductions.Add(
                new ExternalDmgReductions
                    {
                        ModifierName = "modifier_spectre_dispersion", SourceTeam = 0, Reduce = "damage_reflection_pct", 
                        SourceSpellName = "spectre_dispersion", Type = 1
                    });

            ExternalDmgReductions.Add(
                new ExternalDmgReductions
                    {
                        ModifierName = "modifier_nyx_assassin_burrow", SourceSpellName = "nyx_assassin_burrow", 
                        Reduce = "damage_reduction", Type = 1
                    });

            ExternalDmgReductions.Add(
                new ExternalDmgReductions
                    {
                        ModifierName = "modifier_winter_wyvern_winters_curse", 
                        SourceSpellName = "winter_wyvern_winters_curse", Reduce = "damage_reduction", Type = 1
                    });

            ExternalDmgReductions.Add(
                new ExternalDmgReductions
                    {
                        ModifierName = "modifier_ursa_enrage", SourceSpellName = "ursa_enrage", 
                        Reduce = "damage_reduction", Type = 1
                    });

            ExternalDmgReductions.Add(
                new ExternalDmgReductions
                    {
                        ModifierName = "modifier_templar_assassin_refraction_absorb", 
                        SourceSpellName = "templar_assassin_refraction", Type = 1
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
                        SourceSpellName = "ember_spirit_flame_guard", Reduce = "absorb_amount", MagicOnly = true
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
        ///     Checks if given hero has AghanimScepter
        /// </summary>
        /// <param name="hero"></param>
        /// <returns></returns>
        public static bool AghanimState(this Unit hero)
        {
            return
                hero.HasModifiers(
                    new[] { "modifier_item_ultimate_scepter_consumed", "modifier_item_ultimate_scepter" }, 
                    false);
        }

        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="bonusRange"></param>
        /// <returns></returns>
        public static Hero BestAATarget(this Hero source, float bonusRange = 0)
        {
            return TargetSelector.BestAutoAttackTarget(source, bonusRange);
        }

        /// <summary>
        ///     Checks if given unit is able to attack
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool CanAttack(this Unit unit)
        {
            return unit.AttackCapability != AttackCapability.None && !IsDisarmed(unit) && !IsStunned(unit)
                   && unit.IsAlive;
        }

        /// <summary>
        ///     Checks if given unit is able to cast spells
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool CanCast(this Unit unit)
        {
            return !IsSilenced(unit) && !IsStunned(unit) && unit.IsAlive;
        }

        /// <summary>
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="sourceAbilityName"></param>
        /// <param name="ignoreReincarnation"></param>
        /// <returns></returns>
        public static bool CanDie(this Unit unit, string sourceAbilityName = null, bool ignoreReincarnation = false)
        {
            var cullingBlade = sourceAbilityName != null && sourceAbilityName == "axe_culling_blade";
            return !ignoreReincarnation && !unit.CanReincarnate()
                   && (cullingBlade
                           ? !unit.HasModifier("modifier_skeleton_king_reincarnation_scepter_active")
                           : !unit.HasModifiers(
                               new[]
                                   {
                                       "modifier_dazzle_shallow_grave", "modifier_oracle_false_promise", 
                                       "modifier_skeleton_king_reincarnation_scepter_active"
                                   }, 
                               false));
        }

        /// <summary>
        ///     Checks if given unit can become invisible
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool CanGoInvis(this Unit unit)
        {
            var n = unit.Handle + "CanGoInvis";
            if (!Utils.SleepCheck(n))
            {
                return BoolDictionary[n];
            }

            Ability invis = null;
            Ability riki = null;
            foreach (var x in unit.Spellbook.Spells)
            {
                var name = x.StoredName();
                if (name == "bounty_hunter_wind_walk" || name == "clinkz_skeleton_walk"
                    || name == "templar_assassin_meld")
                {
                    invis = x;
                    break;
                }

                if (name == "riki_permanent_invisibility")
                {
                    riki = x;
                }
            }

            if (invis == null)
            {
                invis =
                    unit.Inventory.Items.FirstOrDefault(
                        x =>
                        x.StoredName() == "item_invis_sword" || x.StoredName() == "item_silver_edge"
                        || x.StoredName() == "item_glimmer_cape");
            }

            var canGoInvis = (invis != null && unit.CanCast() && invis.CanBeCasted())
                             || (riki != null && riki.Level > 0 && !unit.IsSilenced());
            if (!BoolDictionary.ContainsKey(n))
            {
                BoolDictionary.Add(n, canGoInvis);
            }
            else
            {
                BoolDictionary[n] = canGoInvis;
            }

            Utils.Sleep(150, n);
            return canGoInvis;
        }

        /// <summary>
        ///     Checks if given unit is able to move
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool CanMove(this Unit unit)
        {
            var n = unit.Handle + "CanMove";
            if (!Utils.SleepCheck(n))
            {
                return BoolDictionary[n];
            }

            var canMove = !IsRooted(unit) && !IsStunned(unit) && !unit.HasModifier("modifier_slark_pounce_leash")
                          && unit.IsAlive;
            if (!BoolDictionary.ContainsKey(n))
            {
                BoolDictionary.Add(n, canMove);
            }
            else
            {
                BoolDictionary[n] = canMove;
            }

            Utils.Sleep(150, n);
            return canMove;
        }

        /// <summary>
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool CanReincarnate(this Unit unit)
        {
            return unit.FindItem("item_aegis") != null || unit.FindSpell("skeleton_king_reincarnation").CanBeCasted();
        }

        /// <summary>
        ///     Checks if given unit is not muted
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool CanUseItems(this Unit unit)
        {
            return !unit.IsUnitState(UnitState.Muted) && !IsStunned(unit) && unit.IsAlive
                   && !unit.HasModifiers(
                       new[] { "modifier_axe_berserkers_call", "modifier_phoenix_supernova_hiding" }, 
                       false);
        }

        /// <summary>
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static JungleCamp ClosestCamp(this Unit unit)
        {
            return JungleCamps.FindClosestCamp(unit.Position);
        }

        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public static Hero ClosestToMouseTarget(this Hero source, float range = 1000)
        {
            return TargetSelector.ClosestToMouse(source, range);
        }

        /// <summary>
        ///     Returns actual damage the unit takes
        /// </summary>
        /// <param name="target">damaged unit</param>
        /// <param name="dmg">amount of damage</param>
        /// <param name="dmgType">Type of damage (Magical/Physical/Pure/Health removal)</param>
        /// <param name="source">source of the damage</param>
        /// <param name="throughBKB">true if the damage pierces magic immunity</param>
        /// <param name="minusArmor"></param>
        /// <param name="minusDamageResistancePerc"></param>
        /// <param name="minusMagicResistancePerc"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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

            // Console.WriteLine(minusMagicResistancePerc/100);
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
                    foreach (var damageBlock in damageBlocksList)
                    {
                        if (damageBlock.ModifierName != name)
                        {
                            continue;
                        }

                        var ability = damageBlock.Item
                                          ? target.FindItem(damageBlock.AbilityName)
                                          : target.FindSpell(damageBlock.AbilityName);
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
                var ability = Abilities.FindAbility(v.SourceSpellName)
                              ?? ObjectManager.GetEntities<Item>()
                                     .FirstOrDefault(x => x.StoredName() == v.SourceSpellName);

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
                var ability = Abilities.FindAbility(v.SourceSpellName)
                              ?? ObjectManager.GetEntities<Item>()
                                     .FirstOrDefault(x => x.StoredName() == v.SourceSpellName);

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
                var spell = target.FindSpell("bristleback_bristleback");
                if (spell != null)
                {
                    var burst = 0d;
                    var angle = target.FindRelativeAngle(source.Position) % (2 * Math.PI) * 180 / Math.PI;
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
                        !x.IsIllusion()
                        && (x.ClassID == ClassID.CDOTA_Unit_Hero_Centaur || x.ClassID == ClassID.CDOTA_Unit_Hero_Rubick)
                        && x.AghanimState());
                reduceProc = heroes.Aggregate(reduceProc, (current, hero) => current + 0.7);
            }

            if (medusaManaShield)
            {
                var spell = target.FindSpell("medusa_mana_shield");
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
                    var owner = spell.Owner as Unit;
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
                         * (1 + ampFromMe)
                         * (1 - target.DamageResist * (1 - minusDamageResistancePerc / 100)
                            + 0.06 * minusArmor / (1 + 0.06 * Math.Abs(minusArmor))) - reduceStatic + aa);
                    break;
                case DamageType.HealthRemoval:
                    break;
            }

            return (float)Math.Max(tempDmg, 0);
        }

        /// <summary>
        ///     Distance between a unit and a vector
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static float Distance2D(this Entity unit, Vector3 vector)
        {
            return unit.Position.Distance2D(vector);
        }

        /// <summary>
        ///     Distance between two units
        /// </summary>
        /// <param name="unit1"></param>
        /// <param name="unit2"></param>
        /// <returns></returns>
        public static float Distance2D(this Entity unit1, Entity unit2)
        {
            return unit1.Position.Distance2D(unit2.Position);
        }

        /// <summary>
        /// </summary>
        /// <param name="projectile"></param>
        /// <param name="unit2"></param>
        /// <returns></returns>
        public static float Distance2D(this TrackingProjectile projectile, Entity unit2)
        {
            return projectile.Position.Distance2D(unit2.Position);
        }

        /// <summary>
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static float Distance2D(this TrackingProjectile p1, TrackingProjectile p2)
        {
            return p1.Position.Distance2D(p2.Position);
        }

        /// <summary>
        ///     Angle between a unit and a vector in degrees
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="second"></param>
        /// <param name="radian"></param>
        /// <returns></returns>
        public static float FindAngleBetween(this Entity unit, Vector3 second, bool radian = false)
        {
            return unit.Position.ToVector2().FindAngleBetween(second.ToVector2(), radian);
        }

        /// <summary>
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static float FindAngleForTurnTime(this Entity unit, Vector3 position)
        {
            var first = unit.Position;
            var second = position;
            var xAngle =
                Utils.RadianToDegree(
                    Math.Atan(Math.Abs(position.X - unit.Position.X) / Math.Abs(position.Y - unit.Position.Y)));
            if (first.X <= second.X && first.Y >= second.Y)
            {
                return (float)(90 - xAngle);
            }

            if (first.X >= second.X && first.Y >= second.Y)
            {
                return (float)(xAngle + 90);
            }

            if (first.X >= second.X && first.Y <= second.Y)
            {
                return (float)(270 - xAngle);
            }

            if (first.X <= second.X && first.Y <= second.Y)
            {
                return (float)(xAngle + 270);
            }

            return 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public static float FindAngleR(this Entity ent)
        {
            return (float)(ent.RotationRad < 0 ? Math.Abs(ent.RotationRad) : 2 * Math.PI - ent.RotationRad);
        }

        /// <summary>
        ///     Searches for a item in the units inventory with given name
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="name"></param>
        /// <param name="cache">Store the item and use stored next time</param>
        /// <returns></returns>
        public static Item FindItem(this Unit unit, string name, bool cache = false)
        {
            if (!unit.IsVisible)
            {
                return null;
            }

            Item item;
            var n = unit.Handle + name;
            if (!ItemDictionary.TryGetValue(n, out item) || item == null || !item.IsValid
                || (Utils.SleepCheck("Common.FindItem." + name) && !cache))
            {
                item = unit.Inventory.Items.FirstOrDefault(x => x != null && x.IsValid && x.StoredName() == name);
                if (ItemDictionary.ContainsKey(n))
                {
                    ItemDictionary[n] = item;
                }
                else
                {
                    ItemDictionary.Add(n, item);
                }

                Utils.Sleep(500, "Common.FindItem." + name);
            }

            if (item == null || !item.IsValid)
            {
                return null;
            }

            return item;
        }

        /// <summary>
        ///     The has modifier.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="modifierName">
        ///     The modifier name.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static Modifier FindModifier(this Unit unit, string modifierName)
        {
            if (Utils.SleepCheck("Ensage.Common.FindModifierReset"))
            {
                modifierDictionary = new Dictionary<string, Modifier>();
                Utils.Sleep(1200000, "Ensage.Common.FindModifierReset");
            }

            var name = unit.StoredName() + modifierName;
            Modifier modifier;
            if (modifierDictionary.TryGetValue(name, out modifier)
                && !Utils.SleepCheck("Ensage.Common.FindModifier" + name))
            {
                return modifier;
            }

            modifier = unit.Modifiers.FirstOrDefault(x => x.Name == modifierName);
            if (modifier == null)
            {
                return null;
            }

            if (modifierDictionary.ContainsKey(name))
            {
                modifierDictionary[name] = modifier;
            }
            else
            {
                modifierDictionary.Add(name, modifier);
            }

            Utils.Sleep(50, "Ensage.Common.FindModifier" + name);

            return modifier;
        }

        /// <summary>
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static float FindRelativeAngle(this Unit unit, Vector3 pos)
        {
            return
                (float)
                ((Math.Atan2(pos.Y - unit.Position.Y, pos.X - unit.Position.X) - unit.RotationRad + Math.PI)
                 % (2 * Math.PI) - Math.PI);
        }

        /// <summary>
        ///     Returns spell of the unit with given name if it exists
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Ability FindSpell(this Unit unit, string name)
        {
            return unit.Spellbook.Spells.FirstOrDefault(x => x.StoredName() == name);
        }

        /// <summary>
        ///     Returns actual attack range of a unit
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static float GetAttackRange(this Unit unit)
        {
            var bonus = 0.0;
            float range;
            if (!RangeDictionary.TryGetValue(unit.Handle, out range)
                || Utils.SleepCheck("Common.GetAttackRange." + unit.Handle))
            {
                var classId = unit.ClassID;
                switch (classId)
                {
                    case ClassID.CDOTA_Unit_Hero_TemplarAssassin:
                        var psi = unit.Spellbook.SpellE;
                        if (psi != null && psi.Level > 0)
                        {
                            bonus = psi.GetAbilityData("bonus_attack_range");
                        }

                        break;
                    case ClassID.CDOTA_Unit_Hero_Sniper:
                        var aim = unit.Spellbook.SpellE;
                        if (aim != null && aim.Level > 0)
                        {
                            bonus = aim.GetAbilityData("bonus_attack_range");
                        }

                        break;
                    case ClassID.CDOTA_Unit_Hero_Enchantress:
                        var impetus = unit.Spellbook.SpellR;
                        if (impetus.Level > 0 && unit.AghanimState())
                        {
                            bonus = 190;
                        }

                        break;
                    default:
                        if (unit.HasModifier("modifier_lone_druid_true_form"))
                        {
                            bonus = -423;
                        }
                        else if (unit.HasModifier("modifier_dragon_knight_dragon_form"))
                        {
                            bonus = 372;
                        }
                        else if (unit.HasModifier("modifier_terrorblade_metamorphosis"))
                        {
                            bonus = 422;
                        }

                        break;
                }

                if (unit.IsRanged)
                {
                    var dragonLance = unit.FindItem("item_dragon_lance");
                    if (dragonLance != null)
                    {
                        bonus += dragonLance.GetAbilityData("base_attack_range");
                    }
                }

                range = (float)(unit.AttackRange + bonus + unit.HullRadius / 2);
                if (!RangeDictionary.ContainsKey(unit.Handle))
                {
                    RangeDictionary.Add(unit.Handle, range);
                }
                else
                {
                    RangeDictionary[unit.Handle] = range;
                }

                Utils.Sleep(500, "Common.GetAttackRange." + unit.Handle);
            }

            return range;
        }

        /// <summary>
        ///     Finds spell/item which is currently being channeled by given unit
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static Ability GetChanneledAbility(this Unit unit)
        {
            return unit.Inventory.Items.FirstOrDefault(v => v.IsChanneling)
                   ?? unit.Spellbook.Spells.FirstOrDefault(v => v.IsChanneling);
        }

        /// <summary>
        ///     Finds a dagon in the units inventory
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static Item GetDagon(this Unit unit)
        {
            return unit.GetLeveledItem("item_dagon");
        }

        /// <summary>
        ///     Returns Enemy Team of the unit
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static Team GetEnemyTeam(this Unit unit)
        {
            var team = unit.Team;
            return team == Team.Dire ? Team.Radiant : Team.Dire;
        }

        /// <summary>
        ///     Finds item with given name which has more than 1 level
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Item GetLeveledItem(this Unit unit, string name)
        {
            return
                unit.Inventory.Items.ToList()
                    .OrderByDescending(x => x.Level)
                    .FirstOrDefault(x => x.StoredName().StartsWith(name));
        }

        /// <summary>
        ///     Calculates how much time it will take for given unit to turn to given vector
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static double GetTurnTime(this Entity unit, Vector3 position)
        {
            try
            {
                double turnRate;
                if (TurnrateDictionary.TryGetValue(unit.Handle, out turnRate))
                {
                    return
                        Math.Max(
                            Math.Abs(FindAngleR(unit) - Utils.DegreeToRadian(unit.FindAngleForTurnTime(position)))
                            - 0.69, 
                            0) / (turnRate * (1 / 0.03));
                }

                turnRate = Game.FindKeyValues(unit.StoredName() + "/MovementTurnRate", KeyValueSource.Hero).FloatValue;
                TurnrateDictionary.Add(unit.Handle, turnRate);
                return
                    Math.Max(
                        Math.Abs(FindAngleR(unit) - Utils.DegreeToRadian(unit.FindAngleForTurnTime(position))) - 0.69, 
                        0) / (turnRate * (1 / 0.03));
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
        ///     Calculates how much time it will take for given unit to turn to another unit
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="unit2"></param>
        /// <returns></returns>
        public static double GetTurnTime(this Entity unit, Entity unit2)
        {
            return unit.GetTurnTime(unit2.Position);
        }

        /// <summary>
        ///     Returns if the target has the given Item
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="classId"></param>
        /// <returns></returns>
        public static bool HasItem(this Unit unit, ClassID classId)
        {
            return unit.Inventory.Items.Any(item => item.ClassID == classId);
        }

        /// <summary>
        ///     The has modifier.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="modifierName">
        ///     The modifier name.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool HasModifier(this Unit unit, string modifierName)
        {
            if (Utils.SleepCheck("Ensage.Common.HasModifierReset"))
            {
                modifierBoolDictionary = new Dictionary<string, bool>();
                Utils.Sleep(1200000, "Ensage.Common.HasModifierReset");
            }

            var name = unit.StoredName() + modifierName;
            bool value;
            if (modifierBoolDictionary.TryGetValue(name, out value)
                && !Utils.SleepCheck("Ensage.Common.HasModifier" + name))
            {
                return value;
            }

            value = unit.Modifiers.Any(x => x.Name == modifierName);
            if (modifierBoolDictionary.ContainsKey(name))
            {
                modifierBoolDictionary[name] = value;
            }
            else
            {
                modifierBoolDictionary.Add(name, value);
            }

            Utils.Sleep(50, "Ensage.Common.HasModifier" + name);

            return value;
        }

        /// <summary>
        ///     The has modifiers.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="modifierNames">
        ///     The modifier names.
        /// </param>
        /// <param name="hasAll">
        ///     The has all.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool HasModifiers(this Unit unit, string[] modifierNames, bool hasAll = true)
        {
            if (Utils.SleepCheck("Ensage.Common.HasModifierReset"))
            {
                modifierBoolDictionary = new Dictionary<string, bool>();
                Utils.Sleep(1200000, "Ensage.Common.HasModifierReset");
            }

            var aname = unit.StoredName() + hasAll;
            aname = modifierNames.Aggregate(aname, (current, modifierName) => current + modifierName);

            bool value;
            if (modifierBoolDictionary.TryGetValue(aname, out value)
                && !Utils.SleepCheck("Ensage.Common.HasModifiers" + aname))
            {
                return value;
            }

            var count = 0;
            foreach (var name in
                unit.Modifiers.Where(modifier => modifierNames.Contains(modifier.Name))
                    .Select(modifier => unit.StoredName() + modifier.Name))
            {
                if (modifierBoolDictionary.ContainsKey(name))
                {
                    modifierBoolDictionary[name] = true;
                }
                else
                {
                    modifierBoolDictionary.Add(name, true);
                }

                Utils.Sleep(50, "Ensage.Common.HasModifier" + name);

                if (!hasAll)
                {
                    if (modifierBoolDictionary.ContainsKey(aname))
                    {
                        modifierBoolDictionary[aname] = true;
                    }
                    else
                    {
                        modifierBoolDictionary.Add(aname, true);
                    }

                    Utils.Sleep(50, "Ensage.Common.HasModifiers" + aname);
                    return true;
                }

                count += 1;
            }

            value = count == modifierNames.Length;

            if (modifierBoolDictionary.ContainsKey(aname))
            {
                modifierBoolDictionary[aname] = value;
            }
            else
            {
                modifierBoolDictionary.Add(aname, value);
            }

            Utils.Sleep(50, "Ensage.Common.HasModifiers" + aname);

            return value;
        }

        /// <summary>
        ///     Checks if unit is immune to auto attack
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool IsAttackImmune(this Unit unit)
        {
            return IsUnitState(unit, UnitState.AttackImmune);
        }

        /// <summary>
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool IsAttacking(this Unit unit)
        {
            return unit.NetworkActivity == NetworkActivity.Attack || unit.NetworkActivity == NetworkActivity.Crit
                   || unit.NetworkActivity == NetworkActivity.Attack2
                   || unit.NetworkActivity == NetworkActivity.AttackEvent
                   || unit.NetworkActivity == NetworkActivity.AttackEventBash;
        }

        /// <summary>
        ///     Checks if unit is currently channeling
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool IsChanneling(this Unit unit)
        {
            return unit.Inventory.Items.Any(v => v.IsChanneling) || unit.Spellbook.Spells.Any(v => v.IsChanneling);
        }

        /// <summary>
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool IsDisarmed(this Unit unit)
        {
            return IsUnitState(unit, UnitState.Disarmed);
        }

        /// <summary>
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool IsHexed(this Unit unit)
        {
            return IsUnitState(unit, UnitState.Hexed);
        }

        /// <summary>
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool IsIllusion(this Hero unit)
        {
            return unit.IsIllusion;
        }

        /// <summary>
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool IsIllusion(this Meepo unit)
        {
            return unit.IsIllusion;
        }

        /// <summary>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool IsIllusion(this Entity entity)
        {
            var unit = entity as Unit;
            return unit != null && unit.IsIllusion;
        }

        /// <summary>
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public static bool IsInvisible(this Unit unit)
        {
            return IsUnitState(unit, UnitState.Invisible);
        }

        /// <summary>
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool IsInvul(this Unit unit)
        {
            return IsUnitState(unit, UnitState.Invulnerable);

            // || unit.Modifiers.Any(
            // x =>
            // x.Name == "modifier_invoker_tornado" || x.Name == "modifier_eul_cyclone"
            // || x.Name == "modifier_cyclone");
        }

        /// <summary>
        /// </summary>
        /// <param name="hero"></param>
        /// <returns></returns>
        public static bool IsLinkensProtected(this Unit hero)
        {
            var linkensphere = hero.FindItem("item_sphere");
            return (linkensphere != null && linkensphere.Cooldown == 0)
                   || hero.HasModifier("modifier_item_sphere_target");
        }

        /// <summary>
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool IsMagicImmune(this Unit unit)
        {
            return IsUnitState(unit, UnitState.MagicImmune);
        }

        /// <summary>
        ///     Checks if enemy have a modifier which can be purged
        /// </summary>
        /// <param name="hero"></param>
        /// <returns></returns>
        public static bool IsPurgable(this Unit hero)
        {
            return
                hero.HasModifiers(
                    new[]
                        {
                            "modifier_ghost_state", "modifier_item_ethereal_blade_slow", 
                            "modifier_omninight_guardian_angel"
                        }, 
                    false);
        }

        /// <summary>
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool IsRooted(this Unit unit)
        {
            return IsUnitState(unit, UnitState.Rooted);
        }

        /// <summary>
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool IsSilenced(this Unit unit)
        {
            return IsUnitState(unit, UnitState.Silenced);
        }

        /// <summary>
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool IsStunned(this Unit unit)
        {
            return IsUnitState(unit, UnitState.Stunned);
        }

        /// <summary>
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static bool IsUnitState(this Unit unit, UnitState state)
        {
            return unit.UnitState.HasFlag(state);
        }

        /// <summary>
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="range"></param>
        /// <param name="checkTeam"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        public static bool IsValidTarget(
            this Unit unit, 
            float range = float.MaxValue, 
            bool checkTeam = true, 
            Vector3 from = new Vector3())
        {
            if (unit == null || !unit.IsValid || !unit.IsAlive || !unit.IsVisible || !unit.IsSpawned || unit.IsNeutral
                || unit.IsInvul())
            {
                return false;
            }

            if (checkTeam && unit.Team == ObjectManager.LocalHero.Team)
            {
                return false;
            }

            var @base = unit as Hero;
            var unitPosition = @base != null ? @base.NetworkPosition : unit.Position;

            return !(range < float.MaxValue)
                   || !(Vector2.DistanceSquared(
                       (@from.ToVector2().IsValid() ? @from : ObjectManager.LocalHero.NetworkPosition).ToVector2(), 
                       unitPosition.ToVector2()) > range * range);
        }

        /// <summary>
        ///     Returns how much mana burn damage given unit receives
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="burnAmount"></param>
        /// <param name="multiplier"></param>
        /// <param name="dmgType"></param>
        /// <param name="source"></param>
        /// <param name="throughBkb"></param>
        /// <param name="minusArmor"></param>
        /// <param name="minusDamageResistancePerc"></param>
        /// <param name="minusMagicResistancePerc"></param>
        /// <returns></returns>
        public static float ManaBurnDamageTaken(
            this Unit unit, 
            float burnAmount, 
            double multiplier, 
            DamageType dmgType, 
            Unit source, 
            bool throughBkb = false, 
            double minusArmor = 0d, 
            double minusDamageResistancePerc = 0d, 
            double minusMagicResistancePerc = 0d)
        {
            var tempBurn = burnAmount;
            if (unit.Mana < tempBurn)
            {
                tempBurn = unit.Mana;
            }

            return unit.DamageTaken(
                (float)(tempBurn * multiplier), 
                dmgType, 
                source, 
                throughBkb, 
                minusArmor, 
                minusDamageResistancePerc, 
                minusMagicResistancePerc);
        }

        /// <summary>
        ///     Returns predicted location of a unit after given miliseconds
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public static Vector3 Predict(this Unit unit, float delay)
        {
            return Prediction.PredictedXYZ(unit, delay);
        }

        /// <summary>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Spellbook Spellbook(this Entity entity)
        {
            var unit = entity as Unit;
            return unit != null ? unit.Spellbook : null;
        }

        /// <summary>
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="delta"></param>
        /// <param name="radial"></param>
        /// <returns></returns>
        public static Vector2 Vector2FromPolarAngle(this Entity unit, float delta = 0f, float radial = 1f)
        {
            var alpha = unit.RotationRad;
            return VectorExtensions.FromPolarCoordinates(radial, alpha + delta);
        }

        /// <summary>
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="delta"></param>
        /// <param name="radial"></param>
        /// <returns></returns>
        public static Vector3 Vector3FromPolarAngle(this Entity unit, float delta = 0f, float radial = 1f)
        {
            return Vector2FromPolarAngle(unit, delta, radial).ToVector3();
        }

        #endregion
    }
}
