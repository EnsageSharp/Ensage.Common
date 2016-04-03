// <copyright file="UnitExtensions.cs" company="EnsageSharp">
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

    using Ensage.Common.Extensions.Damage;
    using Ensage.Common.Objects;
    using Ensage.Heroes;

    using global::SharpDX;

    /// <summary>
    ///     The unit extensions.
    /// </summary>
    public static class UnitExtensions
    {
        #region Static Fields

        /// <summary>
        ///     The boolean dictionary.
        /// </summary>
        private static readonly Dictionary<string, bool> BoolDictionary = new Dictionary<string, bool>();

        /// <summary>
        ///     The item dictionary.
        /// </summary>
        private static readonly Dictionary<string, Item> ItemDictionary = new Dictionary<string, Item>();

        /// <summary>
        ///     The range dictionary.
        /// </summary>
        private static readonly Dictionary<float, float> RangeDictionary = new Dictionary<float, float>();

        /// <summary>
        ///     The modifier dictionary.
        /// </summary>
        private static Dictionary<string, bool> modifierBoolDictionary = new Dictionary<string, bool>();

        /// <summary>
        ///     The modifier dictionary.
        /// </summary>
        private static Dictionary<string, Modifier> modifierDictionary = new Dictionary<string, Modifier>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Checks if given unit has AghanimScepter
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool AghanimState(this Unit unit)
        {
            return
                unit.HasModifiers(
                    new[] { "modifier_item_ultimate_scepter_consumed", "modifier_item_ultimate_scepter" }, 
                    false);
        }

        /// <summary>
        ///     The best aa target.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="bonusRange">
        ///     The bonus range.
        /// </param>
        /// <returns>
        ///     The <see cref="Hero" />.
        /// </returns>
        public static Hero BestAATarget(this Unit unit, float bonusRange = 0)
        {
            return TargetSelector.BestAutoAttackTarget(unit as Hero, bonusRange);
        }

        /// <summary>
        ///     Checks if given unit is able to attack
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool CanAttack(this Unit unit)
        {
            return unit.AttackCapability != AttackCapability.None && !unit.IsDisarmed() && !unit.IsStunned()
                   && unit.IsAlive;
        }

        /// <summary>
        ///     Checks if given unit is able to cast spells
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool CanCast(this Unit unit)
        {
            return !IsSilenced(unit) && !IsStunned(unit) && unit.IsAlive;
        }

        /// <summary>
        ///     The can die.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="sourceAbilityName">
        ///     The source ability name.
        /// </param>
        /// <param name="ignoreReincarnation">
        ///     The ignore reincarnation.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
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
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
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
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool CanMove(this Unit unit)
        {
            var n = unit.Handle + "CanMove";
            if (!Utils.SleepCheck(n))
            {
                return BoolDictionary[n];
            }

            var canMove = !unit.IsRooted() && !unit.IsStunned() && !unit.HasModifier("modifier_slark_pounce_leash")
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
        ///     The can reincarnate.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool CanReincarnate(this Unit unit)
        {
            return unit.FindItem("item_aegis") != null || unit.FindSpell("skeleton_king_reincarnation").CanBeCasted();
        }

        /// <summary>
        ///     Checks if given unit is not muted
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool CanUseItems(this Unit unit)
        {
            return !unit.IsUnitState(UnitState.Muted) && !IsStunned(unit) && unit.IsAlive
                   && !unit.HasModifiers(
                       new[] { "modifier_axe_berserkers_call", "modifier_phoenix_supernova_hiding" }, 
                       false);
        }

        /// <summary>
        ///     The closest to mouse target.
        /// </summary>
        /// <param name="source">
        ///     The source.
        /// </param>
        /// <param name="range">
        ///     The range.
        /// </param>
        /// <returns>
        ///     The <see cref="Hero" />.
        /// </returns>
        public static Hero ClosestToMouseTarget(this Unit source, float range = 1000)
        {
            return TargetSelector.ClosestToMouse(source as Hero, range);
        }

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
        ///     The minus Damage Resistance Percentage.
        /// </param>
        /// <param name="minusMagicResistancePerc">
        ///     The minus Magic Resistance Percentage.
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
            return Calculations.DamageTaken(
                target, 
                dmg, 
                dmgType, 
                source, 
                throughBKB, 
                minusArmor, 
                minusDamageResistancePerc, 
                minusMagicResistancePerc);
        }

        /// <summary>
        ///     Searches for a item in the units inventory with given name
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <param name="cache">
        ///     Store the item and use stored next time
        /// </param>
        /// <returns>
        ///     The <see cref="Item" />.
        /// </returns>
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

            var name = unit.Handle + modifierName;
            Modifier modifier;
            var found = modifierDictionary.TryGetValue(name, out modifier);
            var isValid = true;
            if (found)
            {
                try
                {
                    var test = modifier.RemainingTime;
                }
                catch (ModifierNotFoundException)
                {
                    isValid = false;
                }
            }

            if (found && isValid
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

            Utils.Sleep(100, "Ensage.Common.FindModifier" + name);
            return modifier;
        }

        /// <summary>
        ///     The find relative angle.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="pos">
        ///     The position.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
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
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="Ability" />.
        /// </returns>
        public static Ability FindSpell(this Unit unit, string name)
        {
            return unit.Spellbook.Spells.FirstOrDefault(x => x.StoredName() == name);
        }

        /// <summary>
        ///     Returns actual attack range of a unit
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
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

                range = (float)(unit.AttackRange + bonus + (unit.HullRadius / 2));
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
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="Ability" />.
        /// </returns>
        public static Ability GetChanneledAbility(this Unit unit)
        {
            return unit.Inventory.Items.FirstOrDefault(v => v.IsChanneling)
                   ?? unit.Spellbook.Spells.FirstOrDefault(v => v.IsChanneling);
        }

        /// <summary>
        ///     Finds a dagon in the units inventory
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="Item" />.
        /// </returns>
        public static Item GetDagon(this Unit unit)
        {
            return unit.GetLeveledItem("item_dagon");
        }

        /// <summary>
        ///     Returns Enemy Team of the unit
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="Team" />.
        /// </returns>
        public static Team GetEnemyTeam(this Unit unit)
        {
            var team = unit.Team;
            return team == Team.Dire ? Team.Radiant : Team.Dire;
        }

        /// <summary>
        ///     Finds item with given name which has more than 1 level
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="Item" />.
        /// </returns>
        public static Item GetLeveledItem(this Unit unit, string name)
        {
            return
                unit.Inventory.Items.ToList()
                    .OrderByDescending(x => x.Level)
                    .FirstOrDefault(x => x.StoredName().StartsWith(name));
        }

        /// <summary>
        ///     Returns if the target has the given Item
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="classId">
        ///     The class Id.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
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
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsAttackImmune(this Unit unit)
        {
            return IsUnitState(unit, UnitState.AttackImmune);
        }

        /// <summary>
        ///     The is attacking.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
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
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsChanneling(this Unit unit)
        {
            return unit.Inventory.Items.Any(v => v.IsChanneling) || unit.Spellbook.Spells.Any(v => v.IsChanneling);
        }

        /// <summary>
        ///     The is disarmed.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsDisarmed(this Unit unit)
        {
            return IsUnitState(unit, UnitState.Disarmed);
        }

        /// <summary>
        ///     The is hexed.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsHexed(this Unit unit)
        {
            return IsUnitState(unit, UnitState.Hexed);
        }

        /// <summary>
        ///     The is illusion.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsIllusion(this Unit unit)
        {
            return unit.IsIllusion;
        }

        /// <summary>
        ///     The is illusion.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsIllusion(this Meepo unit)
        {
            return unit.IsIllusion;
        }

        /// <summary>
        ///     The is invisible.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsInvisible(this Unit unit)
        {
            return IsUnitState(unit, UnitState.Invisible);
        }

        /// <summary>
        ///     The is invulnerable.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsInvul(this Unit unit)
        {
            return IsUnitState(unit, UnitState.Invulnerable);
        }

        /// <summary>
        ///     The is linken protected.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsLinkensProtected(this Unit hero)
        {
            var linkensphere = hero.FindItem("item_sphere");
            return (linkensphere != null && linkensphere.Cooldown == 0)
                   || hero.HasModifier("modifier_item_sphere_target");
        }

        /// <summary>
        ///     The is magic immune.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsMagicImmune(this Unit unit)
        {
            return IsUnitState(unit, UnitState.MagicImmune);
        }

        /// <summary>
        ///     Checks if enemy have a modifier which can be purged
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
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
        ///     The is rooted.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsRooted(this Unit unit)
        {
            return IsUnitState(unit, UnitState.Rooted);
        }

        /// <summary>
        ///     The is silenced.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsSilenced(this Unit unit)
        {
            return IsUnitState(unit, UnitState.Silenced);
        }

        /// <summary>
        ///     The is stunned.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsStunned(this Unit unit)
        {
            return IsUnitState(unit, UnitState.Stunned);
        }

        /// <summary>
        ///     The is unit state.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="state">
        ///     The state.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsUnitState(this Unit unit, UnitState state)
        {
            return unit.UnitState.HasFlag(state);
        }

        /// <summary>
        ///     The is valid target.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="range">
        ///     The range.
        /// </param>
        /// <param name="checkTeam">
        ///     The check team.
        /// </param>
        /// <param name="from">
        ///     The from.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
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
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="burnAmount">
        ///     The burn Amount.
        /// </param>
        /// <param name="multiplier">
        ///     The multiplier.
        /// </param>
        /// <param name="dmgType">
        ///     The dmg Type.
        /// </param>
        /// <param name="source">
        ///     The source.
        /// </param>
        /// <param name="throughBkb">
        ///     The through Bkb.
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

            return Calculations.DamageTaken(
                unit, 
                (float)(tempBurn * multiplier), 
                dmgType, 
                source, 
                throughBkb, 
                minusArmor, 
                minusDamageResistancePerc, 
                minusMagicResistancePerc);
        }

        /// <summary>
        ///     Returns predicted location of a unit after given milliseconds
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="delay">
        ///     The delay.
        /// </param>
        /// <returns>
        ///     The <see cref="Vector3" />.
        /// </returns>
        public static Vector3 Predict(this Unit unit, float delay)
        {
            return Prediction.PredictedXYZ(unit, delay);
        }

        #endregion
    }
}