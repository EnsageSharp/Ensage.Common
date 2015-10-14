namespace Ensage.Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.Heroes;

    using SharpDX;

    internal class ExternalDmgAmps
    {
        #region Fields

        public string Amp;

        public ClassID HeroID;

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
            ClassID heroID,
            DamageType type)
        {
            this.ModifierName = modifierName;
            this.SourceTeam = sourceTeam;
            this.Amp = amp;
            this.SourceSpellName = sourceSpellName;
            this.HeroID = heroID;
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
            ClassID heroID,
            float type,
            bool magicOnly)
        {
            this.ModifierName = modifierName;
            this.SourceTeam = sourceTeam;
            this.Reduce = reduce;
            this.SourceSpellName = sourceSpellName;
            this.HeroID = heroID;
            this.Type = type;
            this.MagicOnly = magicOnly;
        }

        #endregion
    }

    public static class EntityExtensions
    {
        #region Static Fields

        private static readonly List<ExternalDmgAmps> ExternalDmgAmps = new List<ExternalDmgAmps>();

        private static readonly List<ExternalDmgReductions> ExternalDmgReductions = new List<ExternalDmgReductions>();

        #endregion

        #region Constructors and Destructors

        static EntityExtensions()
        {
            ExternalDmgAmps.Add(
                new ExternalDmgAmps
                    {
                        ModifierName = "modifier_shadow_demon_soul_catcher", SourceTeam = -1, Amp = "bonus_damage_taken",
                        SourceSpellName = "shadow_demon_soul_catcher", HeroID = ClassID.CDOTA_Unit_Hero_Shadow_Demon,
                        Type = DamageType.Pure
                    });

            ExternalDmgAmps.Add(
                new ExternalDmgAmps
                    {
                        ModifierName = "modifier_bloodseeker_bloodrage", SourceTeam = -2, Amp = "damage_increase_pct",
                        SourceSpellName = "bloodseeker_bloodrage", HeroID = ClassID.CDOTA_Unit_Hero_Bloodseeker,
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

            ExternalDmgAmps.Add(
                new ExternalDmgAmps
                    {
                        ModifierName = "modifier_wisp_overcharge", SourceSpellName = "wisp_overcharge",
                        Amp = "bonus_damage_pct"
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
                        SourceSpellName = "templar_assassin_refraction_absorb", Type = 1
                    });

            ExternalDmgReductions.Add(
                new ExternalDmgReductions
                    {
                        ModifierName = "modifier_abaddon_borrowed_time", SourceSpellName = "abaddon_borrowed_time",
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
                        ModifierName = "modifier_abaddon_aphotic_shield", Type = 0, SourceTeam = 1,
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
            return hero.FindItem("item_ultimate_scepter") != null
                   || hero.Modifiers.Any(x => x.Name == "modifier_item_ultimate_scepter_consumed");
        }

        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Hero BestAATarget(this Hero source)
        {
            return TargetSelector.BestAutoAttackTarget(source);
        }

        /// <summary>
        ///     Checks if given unit is able to attack
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool CanAttack(this Unit unit)
        {
            return unit.AttackCapabilities != AttackCapabilities.None && !IsDisarmed(unit) && !IsStunned(unit)
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
        ///     Checks if given unit can become invisible
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool CanGoInvis(this Unit unit)
        {
            var invis =
                unit.Spellbook.Spells.FirstOrDefault(
                    x => x.Name == "bounty_hunter_wind_walk" || x.Name == "clinkz_skeleton_walk")
                ?? unit.Inventory.Items.FirstOrDefault(
                    x => x.Name == "item_invis_sword" || x.Name == "item_silver_edge" || x.Name == "item_glimmer_cape");
            var riki = FindSpell(unit, "riki_permanent_invisibility");
            return (invis != null && invis.CanBeCasted()) || (riki != null && riki.Level > 0);
        }

        /// <summary>
        ///     Checks if given unit is able to move
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool CanMove(this Unit unit)
        {
            return !IsRooted(unit) && !IsStunned(unit)
                   && unit.Modifiers.Any(x => x.Name != "modifier_slark_pounce_leash") && unit.IsAlive;
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
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static float DamageTaken(this Unit target, float dmg, DamageType dmgType, Unit source, bool throughBKB)
        {
            if (target.IsInvul())
            {
                return 0;
            }

            var tempDmg = dmg;
            var amp = 0d;
            var ampFromME = 0d;
            var reduceProc = 0d;
            var reduceOther = 0d;
            var reduceStatic = 0d;
            var reduceBlock = 0d;
            var ManaShield = 0d;
            var MagOnly = 0d;
            var AA = 0d;

            foreach (var v in ExternalDmgAmps.Where(v => target.Modifiers.Any(x => x.Name == v.ModifierName)))
            {
                var ability = ObjectMgr.GetEntities<Ability>().FirstOrDefault(x => x.Name == v.SourceSpellName)
                              ?? ObjectMgr.GetEntities<Item>().FirstOrDefault(x => x.Name == v.SourceSpellName);
                var burst = 0f;
                if (ability == null)
                {
                    continue;
                }
                var firstOrDefault = ability.AbilityData.FirstOrDefault(x => x.Name == v.Amp);
                if (firstOrDefault != null)
                {
                    burst = firstOrDefault.GetValue(ability.Level - 1) / 100;
                }
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

            foreach (var v in ExternalDmgReductions.Where(v => target.Modifiers.Any(x => x.Name == v.ModifierName)))
            {
                var ability = ObjectMgr.GetEntities<Ability>().FirstOrDefault(x => x.Name == v.SourceSpellName)
                              ?? ObjectMgr.GetEntities<Item>().FirstOrDefault(x => x.Name == v.SourceSpellName);
                var burst = 0f;
                if (ability == null)
                {
                    continue;
                }
                var firstOrDefault = ability.AbilityData.FirstOrDefault(x => x.Name == v.Reduce);
                if (firstOrDefault != null)
                {
                    burst = firstOrDefault.GetValue(ability.Level - 1) / 100;
                }
                if (v.Type == 1)
                {
                    if (v.SourceTeam == 1 && ability.Owner.Team == target.Team)
                    {
                        reduceProc += burst;
                    }
                    else if (v.SourceTeam == 0)
                    {
                        reduceProc += burst;
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
                            MagOnly += burst;
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
                            MagOnly += burst;
                        }
                    }
                }
                else
                {
                    reduceStatic += burst;
                }
            }

            if (target.Modifiers.Any(x => x.Name == "modifier_bristleback_bristleback"))
            {
                var spell = target.FindSpell("bristleback_bristleback");
                if (spell != null)
                {
                    var burst = 0d;
                    var angle = ((target.FindRelativeAngle(source.Position)) % (2 * Math.PI)) * 180 / Math.PI;
                    if (angle >= 110 && angle <= 250)
                    {
                        burst = ((1 + spell.Level) * 0.08);
                    }
                    else if (angle >= 70 && angle <= 290)
                    {
                        burst = ((1 + spell.Level) * 0.04);
                    }
                    reduceProc += burst;
                }
            }

            if (target.Modifiers.Any(x => x.Name == "modifier_centaur_stampede"))
            {
                var heroes =
                    ObjectMgr.GetEntities<Hero>()
                        .Where(
                            x =>
                            !x.IsIllusion()
                            && (x.ClassID == ClassID.CDOTA_Unit_Hero_Centaur
                                || x.ClassID == ClassID.CDOTA_Unit_Hero_Rubick) && x.AghanimState());
                reduceProc = heroes.Aggregate(reduceProc, (current, hero) => (current + 0.7));
            }

            if (target.Modifiers.Any(x => x.Name == "modifier_medusa_mana_shield"))
            {
                var spell = target.FindSpell("medusa_mana_shield");
                if (spell != null)
                {
                    var firstOrDefault = spell.AbilityData.FirstOrDefault(x => x.Name == "damage_per_mana");
                    if (firstOrDefault != null)
                    {
                        var treshold = firstOrDefault.GetValue(spell.Level - 1);
                        double burst;
                        if (target.Mana >= tempDmg * .6 / treshold)
                        {
                            burst = 0.6;
                        }
                        else
                        {
                            burst = target.Mana * treshold / tempDmg;
                        }
                        ManaShield = burst;
                    }
                }
            }

            if (target.Modifiers.Any(x => x.Name == "modifier_undying_flesh_golem_plague_aura"))
            {
                var spell = ObjectMgr.GetEntities<Ability>().FirstOrDefault(x => x.Name == "undying_flesh_golem");
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
                        amp += (baseAmp + 0.15);
                    }
                    else if (distance > 750)
                    {
                        amp += baseAmp;
                    }
                    else
                    {
                        amp += (baseAmp + (750 - distance) * 0.03 / 110);
                    }
                }
            }

            if (target.Modifiers.Any(x => x.Name == "modifier_abaddon_borrowed_time_damage_redirect"))
            {
                reduceOther += 0.35;
            }
            else if (target.Modifiers.Any(x => x.Name == "modifier_kunkka_ghost_ship_damage_absorb"))
            {
                reduceOther += 0.5;
            }

            if (source.Modifiers.Any(x => x.Name == "modifier_bloodseeker_bloodrage"))
            {
                var spell = ObjectMgr.GetEntities<Ability>().FirstOrDefault(x => x.Name == "bloodseeker_bloodrage");
                if (spell != null)
                {
                    var firstOrDefault = spell.AbilityData.FirstOrDefault(x => x.Name == "damage_increase_pct");
                    if (firstOrDefault != null)
                    {
                        var bloodrite = firstOrDefault.GetValue(spell.Level - 1);
                        if (target.Distance2D(source) > 2200)
                        {
                            bloodrite /= 2;
                        }
                        ampFromME += bloodrite;
                    }
                }
            }

            if (source.Modifiers.Any(x => x.Name == "modifier_silver_edge_debuff"))
            {
                ampFromME -= 0.4;
            }

            if (target.Modifiers.Any(x => x.Name == "modifier_ice_blast"))
            {
                var spell =
                    ObjectMgr.GetEntities<Ability>()
                        .FirstOrDefault(x => x.Name == "ancient_apparition_ice_blast" && x.Owner.Team != target.Team);
                if (spell != null)
                {
                    var treshold = spell.AbilityData.FirstOrDefault(x => x.Name == "kill_pct").GetValue(spell.Level - 1)
                                   / 100;
                    AA = Math.Floor(treshold / target.MaximumHealth);
                }
            }

            switch (dmgType)
            {
                case DamageType.Magical:
                    tempDmg =
                        (float)
                        (((tempDmg * (1 - ManaShield - reduceOther) - MagOnly) * (1 + amp - reduceProc)
                          * (1 + ampFromME)) * (1 - target.MagicDamageResist / 100) - reduceStatic + AA);
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
                            (((tempDmg * (1 - ManaShield - reduceOther)) * (1 + amp - reduceProc) * (1 + ampFromME))
                             - reduceStatic + AA);
                    }
                    break;
                case DamageType.Physical:
                    if (!throughBKB)
                    {
                        //some calculations missing
                    }
                    tempDmg =
                        (float)
                        (((tempDmg * (1 - ManaShield - reduceOther) - reduceBlock) * (1 + amp - reduceProc)
                          * (1 + ampFromME)) * (1 - target.DamageResist / 100) - reduceStatic + AA);
                    break;
                case DamageType.HealthRemoval:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("dmgType", dmgType, null);
            }

            return Math.Max(tempDmg, 0);
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
        /// <returns></returns>
        public static Item FindItem(this Unit unit, string name)
        {
            return unit.Inventory.Items.FirstOrDefault(x => x.Name == name);
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
                (((Math.Atan2(pos.Y - unit.Position.Y, pos.X - unit.Position.X) - unit.RotationRad + Math.PI)
                  % (2 * Math.PI)) - Math.PI);
        }

        /// <summary>
        ///     Returns spell of the unit with given name if it exists
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Ability FindSpell(this Unit unit, string name)
        {
            return unit.Spellbook.Spells.FirstOrDefault(x => x.Name == name);
        }

        /// <summary>
        ///     Returns actual attack range of a unit
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static float GetAttackRange(this Unit unit)
        {
            var bonus = 0.0;
            var ClassID = unit.ClassID;
            switch (ClassID)
            {
                case ClassID.CDOTA_Unit_Hero_TemplarAssassin:
                    var psi = unit.Spellbook.SpellW;
                    if (psi != null && psi.Level > 0)
                    {
                        var firstOrDefault = psi.AbilityData.FirstOrDefault(x => x.Name == "bonus_attack_range");
                        if (firstOrDefault != null)
                        {
                            bonus = firstOrDefault.GetValue(psi.Level - 1);
                        }
                    }
                    break;
                case ClassID.CDOTA_Unit_Hero_Sniper:
                    var aim = unit.Spellbook.SpellE;
                    if (aim != null && aim.Level > 0)
                    {
                        var firstOrDefault = aim.AbilityData.FirstOrDefault(x => x.Name == "bonus_attack_range");
                        if (firstOrDefault != null)
                        {
                            bonus = firstOrDefault.GetValue(aim.Level - 1);
                        }
                    }
                    break;
                case ClassID.CDOTA_Unit_Hero_Enchantress:
                    var impetus = unit.Spellbook.SpellR;
                    if (impetus.Level > 0 && unit.Inventory.Items.Any(x => (x.Name == "item_ultimate_scepter")))
                    {
                        bonus = 190;
                    }
                    break;
                default:
                    if (unit.Modifiers.Any(x => (x.Name == "modifier_lone_druid_true_form")))
                    {
                        bonus = -423;
                    }
                    else if (unit.Modifiers.Any(x => (x.Name == "dragon_knight_elder_dragon_form")))
                    {
                        bonus = 372;
                    }
                    else if (unit.Modifiers.Any(x => (x.Name == "terrorblade_metamorphosis")))
                    {
                        bonus = 422;
                    }
                    break;
            }
            return (float)(unit.AttackRange + bonus + unit.HullRadius / 2);
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
            switch (team)
            {
                case Team.Dire:
                    return Team.Radiant;
                case Team.Radiant:
                    return Team.Dire;
            }
            return team;
        }

        /// <summary>
        ///     Finds spell/item which is currently being channeled by given unit
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static Ability GetChanneledAbility(this Unit unit)
        {
            var channelingItem = unit.Inventory.Items.ToList().FirstOrDefault(v => v.IsChanneling);
            var channelingAbility = unit.Spellbook.Spells.ToList().FirstOrDefault(v => v.IsChanneling);
            return channelingItem ?? channelingAbility;
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
                    .OrderByDescending(x => x.Name)
                    .FirstOrDefault(x => x.Name.Substring(0, name.Length) == name);
        }

        /// <summary>
        ///     Calculates how much time it will take for given unit to turn to given vector
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static double GetTurnTime(this Entity unit, Vector3 position)
        {
            var turnRate = Game.FindKeyValues(unit.Name + "/MovementTurnRate", KeyValueSource.Hero).FloatValue;
            //Console.WriteLine(FindAngleR(unit) + @" " + Utils.DegreeToRadian(unit.FindAngleBetween(position,true)));
            return
                (Math.Max(
                    Math.Abs(FindAngleR(unit) - Utils.DegreeToRadian(unit.FindAngleForTurnTime(position))) - 0.69,
                    0) / (turnRate * (1 / 0.03)));
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
            return (unit.Inventory.Items.Any(item => item.ClassID == classId));
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
        ///     Checks if given unit's current activity is Attack
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool IsAttacking(this Unit unit)
        {
            return (unit.NetworkActivity == NetworkActivity.Attack1 || unit.NetworkActivity == NetworkActivity.Attack2
                    || unit.NetworkActivity == NetworkActivity.Attack3);
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
        ///     Checks if unit is currently channeling
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool IsChanneling(this Unit unit)
        {
            var channelingItem = unit.Inventory.Items.ToList().Any(v => v.IsChanneling);
            var channelingAbility = unit.Spellbook.Spells.ToList().Any(v => v.IsChanneling);
            return channelingAbility || channelingItem;
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
        /// <param name="unit"></param>
        /// <returns></returns>
        /// <summary>
        /// </summary>
        /// <param name="unit"></param>
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

            if (checkTeam && unit.Team == ObjectMgr.LocalHero.Team)
            {
                return false;
            }

            var @base = unit as Hero;
            var unitPosition = @base != null ? @base.NetworkPosition : unit.Position;

            return !(range < float.MaxValue)
                   || !(Vector2.DistanceSquared(
                       (@from.ToVector2().IsValid() ? @from : ObjectMgr.LocalHero.NetworkPosition).ToVector2(),
                       unitPosition.ToVector2()) > range * range);
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