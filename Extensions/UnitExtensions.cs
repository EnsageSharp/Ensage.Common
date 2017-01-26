// <copyright file="UnitExtensions.cs" company="EnsageSharp">
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
namespace Ensage.Common.Extensions
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;

    using Ensage.Common.Enums;
    using Ensage.Common.Extensions.Damage;
    using Ensage.Common.Objects;

    using global::SharpDX;

    /// <summary>
    ///     The unit extensions.
    /// </summary>
    public static class UnitExtensions
    {
        #region Static Fields

        /// <summary>
        ///     The turn rate dictionary.
        /// </summary>
        private static readonly ConcurrentDictionary<uint, double> TurnrateDictionary =
            new ConcurrentDictionary<uint, double>();

        /// <summary>
        ///     The ability dictionary.
        /// </summary>
        private static ConcurrentDictionary<string, Ability> abilityDictionary =
            new ConcurrentDictionary<string, Ability>();

        /// <summary>
        ///     The boolean dictionary.
        /// </summary>
        private static ConcurrentDictionary<string, bool> boolDictionary = new ConcurrentDictionary<string, bool>();

        /// <summary>
        ///     The item dictionary.
        /// </summary>
        private static ConcurrentDictionary<string, Item> itemDictionary = new ConcurrentDictionary<string, Item>();

        /// <summary>
        ///     The modifier dictionary.
        /// </summary>
        private static ConcurrentDictionary<string, bool> modifierBoolDictionary =
            new ConcurrentDictionary<string, bool>();

        /// <summary>
        ///     The modifier dictionary.
        /// </summary>
        private static ConcurrentDictionary<string, Modifier> modifierDictionary =
            new ConcurrentDictionary<string, Modifier>();

        /// <summary>
        ///     The range dictionary.
        /// </summary>
        private static ConcurrentDictionary<float, float> rangeDictionary = new ConcurrentDictionary<float, float>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Checks if given hero has AghanimScepter
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
        ///     The attack backswing.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double AttackBackswing(this Unit unit)
        {
            return UnitDatabase.GetAttackBackswing(unit);
        }

        /// <summary>
        ///     The attack point.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double AttackPoint(this Unit unit)
        {
            return UnitDatabase.GetAttackPoint(unit);
        }

        /// <summary>
        ///     The attack rate.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double AttackRate(this Unit unit)
        {
            return UnitDatabase.GetAttackRate(unit);
        }

        /// <summary>
        ///     The base predict.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="delay">
        ///     The delay in milliseconds.
        /// </param>
        /// <returns>
        ///     The <see cref="Vector3" />.
        /// </returns>
        public static Vector3 BasePredict(this Unit unit, float delay)
        {
            delay /= 1000;
            if (unit.NetworkActivity != NetworkActivity.Move)
            {
                return unit.NetworkPosition;
            }

            return unit.NetworkPosition + unit.Vector3FromPolarAngle() * (delay * unit.MovementSpeed);
        }

        /// <summary>
        ///     The base predict.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="source">
        ///     The source.
        /// </param>
        /// <param name="delay">
        ///     The delay in milliseconds.
        /// </param>
        /// <param name="speed">
        ///     The speed.
        /// </param>
        /// <returns>
        ///     The <see cref="Vector3" />.
        /// </returns>
        public static Vector3 BasePredict(this Unit unit, Unit source, float delay, float speed)
        {
            delay /= 1000;
            if (unit.NetworkActivity != NetworkActivity.Move)
            {
                return unit.NetworkPosition;
            }

            var predict = unit.NetworkPosition + unit.Vector3FromPolarAngle() * (delay * unit.MovementSpeed);
            var reachTime = Prediction.CalculateReachTime(unit, speed, predict - source.NetworkPosition);
            return unit.NetworkPosition
                   + unit.Vector3FromPolarAngle() * ((delay * 1000 + reachTime) * unit.MovementSpeed) / 1000;
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
            return TargetSelector.BestAutoAttackTarget(unit, bonusRange);
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
                return boolDictionary[n];
            }

            var canMove = !unit.IsRooted() && !unit.IsStunned() && !unit.HasModifier("modifier_slark_pounce_leash")
                          && unit.IsAlive;
            if (!boolDictionary.ContainsKey(n))
            {
                boolDictionary.TryAdd(n, canMove);
            }
            else
            {
                boolDictionary[n] = canMove;
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
            return unit.FindItem("item_aegis", true) != null
                   || unit.FindSpell("skeleton_king_reincarnation", true).CanBeCasted();
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
            return TargetSelector.ClosestToMouse(source, range);
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
        ///     Uses available disable ability which takes least time to hit the target, chains with other disables
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool DisableTarget(this Unit unit, Unit target)
        {
            var stunAbility =
                unit.Spellbook.Spells.Where(
                        x => x.CanBeCasted() && x.CommonProperties() != null && x.CommonProperties().IsDisable)
                    .MinOrDefault(x => x.GetHitDelay(target));

            return stunAbility != null && stunAbility.CastStun(target);
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
        ///     Store the item and use stored next time (Risk of EntityNotFoundException)
        /// </param>
        /// <returns>
        ///     The <see cref="Item" />.
        /// </returns>
        public static Item FindItem(this Unit unit, string name, bool cache = false)
        {
            if (!unit.IsVisible || !unit.HasInventory)
            {
                return null;
            }

            if (!cache)
            {
                return unit.Inventory.Items.FirstOrDefault(x => x != null && x.IsValid && x.StoredName() == name);
            }

            Item item;
            var n = unit.Handle + name;
            if (!itemDictionary.TryGetValue(n, out item) || item == null || !item.IsValid
                || Utils.SleepCheck("Common.FindItem." + name))
            {
                item = unit.Inventory.Items.FirstOrDefault(x => x != null && x.IsValid && x.StoredName() == name);
                if (itemDictionary.ContainsKey(n))
                {
                    itemDictionary[n] = item;
                }
                else
                {
                    itemDictionary.TryAdd(n, item);
                }

                Utils.Sleep(1000, "Common.FindItem." + name);
            }

            if (item == null || !item.IsValid)
            {
                return null;
            }

            return item;
        }

        /// <summary>
        ///     The find item.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <param name="item">
        ///     The item.
        /// </param>
        /// <param name="cache">
        ///     The cache.
        /// </param>
        /// <returns>
        ///     The <see cref="Item" />.
        /// </returns>
        public static Item FindItem(this Unit unit, string name, out Item item, bool cache = false)
        {
            item = unit.FindItem(name, cache);
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
                modifierDictionary = new ConcurrentDictionary<string, Modifier>();
                Utils.Sleep(20000, "Ensage.Common.FindModifierReset");
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

            if (found && isValid && !Utils.SleepCheck("Ensage.Common.FindModifier" + name))
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
                modifierDictionary.TryAdd(name, modifier);
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
            var angle = Math.Abs(Math.Atan2(pos.Y - unit.Position.Y, pos.X - unit.Position.X) - unit.RotationRad);

            if (angle > Math.PI)
            {
                angle = Math.PI * 2 - angle;
            }

            return (float)angle;
        }

        /// <summary>
        ///     The find relative angle.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="pos">
        ///     The pos.
        /// </param>
        /// <param name="delta">
        ///     The alpha.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float FindRelativeAngle(this Unit unit, Vector3 pos, float delta)
        {
            var angle = Math.Abs(
                Math.Atan2(pos.Y - unit.Position.Y, pos.X - unit.Position.X) - unit.RotationRad - delta);

            if (angle > Math.PI)
            {
                angle = Math.PI * 2 - angle;
            }

            return (float)angle;
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
        /// <param name="cache">
        ///     Use cached spell (risk of EntityNotFoundException)
        /// </param>
        /// <returns>
        ///     The <see cref="Ability" />.
        /// </returns>
        public static Ability FindSpell(this Unit unit, string name, bool cache = false)
        {
            if (!cache)
            {
                return unit.Spellbook.Spells.FirstOrDefault(x => x.StoredName() == name);
            }

            Ability ability;
            var n = unit.Handle + name;
            if (!abilityDictionary.TryGetValue(n, out ability) || ability == null || !ability.IsValid
                || Utils.SleepCheck("Common.FindSpell." + name))
            {
                ability = unit.Spellbook.Spells.FirstOrDefault(x => x.StoredName() == name);
                if (abilityDictionary.ContainsKey(n))
                {
                    abilityDictionary[n] = ability;
                }
                else
                {
                    abilityDictionary.TryAdd(n, ability);
                }

                Utils.Sleep(1000, "Common.FindSpell." + name);
            }

            if (ability == null || !ability.IsValid)
            {
                return null;
            }

            return ability;
        }

        /// <summary>
        ///     The find spell.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <param name="spell">
        ///     The spell.
        /// </param>
        /// <returns>
        ///     The <see cref="Ability" />.
        /// </returns>
        public static Ability FindSpell(this Unit unit, string name, out Ability spell)
        {
            spell = unit.FindSpell(name);
            return spell;
        }

        /// <summary>
        ///     Returns an ability by using its internal ID.
        /// </summary>
        /// <param name="owner">Owner unit.</param>
        /// <param name="abilityId">The ability ID of the wanted ability.</param>
        /// <returns></returns>
        public static Ability GetAbilityById(this Unit owner, AbilityId abilityId)
        {
            return owner.Spellbook.Spells.FirstOrDefault(x => x.AbilityData2.ID == (uint)abilityId);
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
            var hero = unit as Hero;
            if (hero != null)
            {
                return hero.GetAttackRange();
            }

            float range;
            if (rangeDictionary.TryGetValue(unit.Handle, out range)
                && !Utils.SleepCheck("Common.GetAttackRange." + unit.Handle))
            {
                return range;
            }

            range = unit.AttackRange + unit.HullRadius;
            if (!rangeDictionary.ContainsKey(unit.Handle))
            {
                rangeDictionary.TryAdd(unit.Handle, range);
            }
            else
            {
                rangeDictionary[unit.Handle] = range;
            }

            Utils.Sleep(1500, "Common.GetAttackRange." + unit.Handle);

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
        ///     Returns an item by using its internal ID.
        /// </summary>
        /// <param name="owner">Owner unit.</param>
        /// <param name="itemId">The item ID of the wanted item.</param>
        /// <returns></returns>
        public static Item GetItemById(this Unit owner, ItemId itemId)
        {
            return owner.Inventory.Items.FirstOrDefault(x => x.AbilityData2.ID == (uint)itemId);
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
        ///     The get turn rate.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="currentTurnRate">
        ///     The current turn rate.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double GetTurnRate(this Unit unit, bool currentTurnRate = true)
        {
            var handle = unit.Handle;
            double turnRate;

            if (!TurnrateDictionary.TryGetValue(handle, out turnRate))
            {
                try
                {
                    turnRate =
                        Game.FindKeyValues(
                            unit.StoredName() + "/MovementTurnRate",
                            unit is Hero ? KeyValueSource.Hero : KeyValueSource.Unit).FloatValue;
                }
                catch (KeyValuesNotFoundException)
                {
                    turnRate = 0.5;
                }

                TurnrateDictionary.TryAdd(handle, turnRate);
            }

            if (currentTurnRate)
            {
                if (unit.HasModifier("modifier_medusa_stone_gaze_slow"))
                {
                    turnRate *= 0.65;
                }

                if (unit.HasModifier("modifier_batrider_sticky_napalm"))
                {
                    turnRate *= 0.3;
                }
            }

            return turnRate;
        }

        /// <summary>
        ///     Calculates how much time it will take for given unit to turn to given vector
        /// </summary>
        /// <param name="unit">
        ///     The entity.
        /// </param>
        /// <param name="position">
        ///     The position.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double GetTurnTime(this Unit unit, Vector3 position)
        {
            if (unit.ClassID == ClassID.CDOTA_Unit_Hero_Wisp)
            {
                return 0;
            }

            var angle = unit.FindRelativeAngle(position);
            if (angle <= 0.5)
            {
                return 0;
            }

            return 0.03 / unit.GetTurnRate() * angle;
        }

        /// <summary>
        ///     Calculates how much time it will take for given unit to turn to another entity
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="entity">
        ///     The unit 2.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double GetTurnTime(this Unit unit, Entity entity)
        {
            return unit.GetTurnTime(entity.NetworkPosition);
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
                modifierBoolDictionary = new ConcurrentDictionary<string, bool>();
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
                modifierBoolDictionary.TryAdd(name, value);
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
                modifierBoolDictionary = new ConcurrentDictionary<string, bool>();
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
            foreach (var modifier in
                unit.Modifiers)
            {
                var name = modifier.Name;
                if (!modifierNames.Contains(name))
                {
                    continue;
                }

                if (modifierBoolDictionary.ContainsKey(name))
                {
                    modifierBoolDictionary[name] = true;
                }
                else
                {
                    modifierBoolDictionary.TryAdd(name, true);
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
                        modifierBoolDictionary.TryAdd(aname, true);
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
                modifierBoolDictionary.TryAdd(aname, value);
            }

            Utils.Sleep(50, "Ensage.Common.HasModifiers" + aname);

            return value;
        }

        /// <summary>
        ///     The in front.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="distance">
        ///     The distance.
        /// </param>
        /// <returns>
        ///     The <see cref="Vector3" />.
        /// </returns>
        public static Vector3 InFront(this Unit unit, float distance)
        {
            var v = unit.Position + unit.Vector3FromPolarAngle() * distance;
            return new Vector3(v.X, v.Y, unit.Position.Z);
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
            var networkActivity = unit.NetworkActivity;
            return networkActivity == NetworkActivity.Attack || networkActivity == NetworkActivity.Crit
                   || networkActivity == NetworkActivity.Attack2 || networkActivity == NetworkActivity.AttackEvent
                   || networkActivity == NetworkActivity.AttackEventBash
                   || networkActivity == NetworkActivity.EarthshakerTotemAttack;
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
            if (unit.NetworkActivity == NetworkActivity.Move || unit.IsAttacking() || unit.IsStunned()
                || unit.IsRooted())
            {
                return false;
            }

            var n = unit.Handle + ".Ensage.Common.IsChanneling";
            bool channeling;
            var exist = boolDictionary.TryGetValue(n, out channeling);
            if (!exist)
            {
                boolDictionary.TryAdd(n, false);
            }

            if (!Utils.SleepCheck(n) && channeling)
            {
                return true;
            }

            channeling = unit.Inventory.Items.Any(v => v.IsChanneling) || unit.Spellbook.Spells.Any(v => v.IsChanneling);
            boolDictionary[n] = channeling;
            Utils.Sleep(100, n);
            return channeling;
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
        ///     The is hexed.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="hexDuration">
        ///     The hex duration.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsHexed(this Unit unit, out float hexDuration)
        {
            hexDuration = Utils.DisableDuration(unit);
            return unit.IsHexed();
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
        ///     The is invulnerable.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="invulnerabilityDuration">
        ///     The invulnerability duration.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsInvul(this Unit unit, out float invulnerabilityDuration)
        {
            invulnerabilityDuration = Utils.DisableDuration(unit);
            return unit.IsInvul();
        }

        /// <summary>
        ///     The is linken protected.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsLinkensProtected(this Unit unit)
        {
            var linkensphere = unit.FindItem("item_sphere", true);
            return linkensphere != null && linkensphere.Cooldown == 0 || unit.HasModifier("modifier_item_sphere_target");
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
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsPurgable(this Unit unit)
        {
            return
                unit.HasModifiers(
                    new[]
                        {
                            "modifier_ghost_state", "modifier_item_ethereal_blade_slow",
                            "modifier_omninight_guardian_angel"
                        },
                    false);
        }

        /// <summary>
        ///     Returns false for announcers, portrait entities etc.
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool IsRealUnit(this Unit unit)
        {
            return unit.UnitType != 0 && !unit.UnitState.HasFlag(UnitState.FakeAlly);
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
        ///     The is rooted.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="rootDuration">
        ///     The root duration.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsRooted(this Unit unit, out float rootDuration)
        {
            rootDuration = Utils.DisableDuration(unit);
            return unit.IsRooted();
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
        ///     The is stunned.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="stunDuration">
        ///     The stun duration.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsStunned(this Unit unit, out float stunDuration)
        {
            stunDuration = Utils.DisableDuration(unit);
            return IsStunned(unit);
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
            if (unit == null || !unit.IsValid)
            {
                return false;
            }

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
        ///     The mana burn spell damage taken.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="burnAmount">
        ///     The burn amount.
        /// </param>
        /// <param name="multiplier">
        ///     The multiplier.
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
        /// <param name="throughBkb">
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
        public static float ManaBurnSpellDamageTaken(
            this Unit unit,
            float burnAmount,
            double multiplier,
            DamageType dmgType,
            Unit source,
            string spellName,
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

            return Calculations.SpellDamageTaken(
                unit,
                (float)(tempBurn * multiplier),
                dmgType,
                source,
                spellName,
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

        /// <summary>
        ///     The projectile speed.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double ProjectileSpeed(this Unit unit)
        {
            return UnitDatabase.GetProjectileSpeed(unit);
        }

        /// <summary>
        ///     Uses available silence ability which takes least time to hit the target, chains with disables
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool SilenceTarget(this Unit unit, Unit target)
        {
            var stunAbility =
                unit.Spellbook.Spells.Where(
                        x => x.CanBeCasted() && x.CommonProperties() != null && x.CommonProperties().IsSilence)
                    .MinOrDefault(x => x.GetHitDelay(target));

            return stunAbility != null && stunAbility.CastStun(target);
        }

        /// <summary>
        ///     Uses available slow ability which takes least time to hit the target, chains with other disables
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool SlowTarget(this Unit unit, Unit target)
        {
            var stunAbility =
                unit.Spellbook.Spells.Where(
                        x => x.CanBeCasted() && x.CommonProperties() != null && x.CommonProperties().IsSlow)
                    .MinOrDefault(x => x.GetHitDelay(target));

            return stunAbility != null && stunAbility.CastStun(target);
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
            return Calculations.SpellDamageTaken(
                target,
                dmg,
                dmgType,
                source,
                spellName,
                throughBKB,
                minusArmor,
                minusDamageResistancePerc,
                minusMagicResistancePerc);
        }

        /// <summary>
        ///     The vector 2 from polar angle.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="delta">
        ///     The delta.
        /// </param>
        /// <param name="radial">
        ///     The radial.
        /// </param>
        /// <returns>
        ///     The <see cref="Vector2" />.
        /// </returns>
        public static Vector2 Vector2FromPolarAngle(this Unit unit, float delta = 0f, float radial = 1f)
        {
            var diff = Utils.DegreeToRadian(unit.RotationDifference);
            var alpha = unit.NetworkRotationRad + diff;
            return VectorExtensions.FromPolarCoordinates(radial, (float)(alpha + delta));
        }

        /// <summary>
        ///     The vector 3 from polar angle.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <param name="delta">
        ///     The delta.
        /// </param>
        /// <param name="radial">
        ///     The radial.
        /// </param>
        /// <returns>
        ///     The <see cref="Vector3" />.
        /// </returns>
        public static Vector3 Vector3FromPolarAngle(this Unit unit, float delta = 0f, float radial = 1f)
        {
            return Vector2FromPolarAngle(unit, delta, radial).ToVector3();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The initialize.
        /// </summary>
        internal static void Init()
        {
            boolDictionary = new ConcurrentDictionary<string, bool>();
            itemDictionary = new ConcurrentDictionary<string, Item>();
            abilityDictionary = new ConcurrentDictionary<string, Ability>();
            rangeDictionary = new ConcurrentDictionary<float, float>();
            modifierBoolDictionary = new ConcurrentDictionary<string, bool>();
            modifierDictionary = new ConcurrentDictionary<string, Modifier>();
        }

        #endregion
    }
}