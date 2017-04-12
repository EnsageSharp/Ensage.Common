// <copyright file="AbilityExtensions.cs" company="EnsageSharp">
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
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.Common.AbilityInfo;
    using Ensage.Common.Enums;
    using Ensage.Common.Extensions.SharpDX;
    using Ensage.Common.Objects;
    using Ensage.Common.Objects.UtilityObjects;

    using global::SharpDX;

    /// <summary>
    ///     The ability extensions.
    /// </summary>
    public static class AbilityExtensions
    {
        #region Static Fields

        /// <summary>
        ///     The ability behavior dictionary.
        /// </summary>
        private static ConcurrentDictionary<string, AbilityBehavior> abilityBehaviorDictionary =
            new ConcurrentDictionary<string, AbilityBehavior>();

        /// <summary>
        ///     The boolean dictionary.
        /// </summary>
        private static ConcurrentDictionary<string, bool> boolDictionary = new ConcurrentDictionary<string, bool>();

        /// <summary>
        ///     The can hit dictionary.
        /// </summary>
        private static ConcurrentDictionary<uint, bool> canHitDictionary = new ConcurrentDictionary<uint, bool>();

        /// <summary>
        ///     The cast point dictionary.
        /// </summary>
        private static ConcurrentDictionary<string, double> castPointDictionary =
            new ConcurrentDictionary<string, double>();

        /// <summary>
        ///     The cast range dictionary.
        /// </summary>
        private static ConcurrentDictionary<string, float> castRangeDictionary =
            new ConcurrentDictionary<string, float>();

        /// <summary>
        ///     The channel dictionary.
        /// </summary>
        private static ConcurrentDictionary<string, float> channelDictionary = new ConcurrentDictionary<string, float>();

        /// <summary>
        ///     The data dictionary.
        /// </summary>
        private static ConcurrentDictionary<string, AbilitySpecialData> dataDictionary =
            new ConcurrentDictionary<string, AbilitySpecialData>();

        /// <summary>
        ///     Temporarily stores cast delay values
        /// </summary>
        private static ConcurrentDictionary<string, double> delayDictionary = new ConcurrentDictionary<string, double>();

        /// <summary>
        ///     The hit delay dictionary.
        /// </summary>
        private static ConcurrentDictionary<string, double> hitDelayDictionary =
            new ConcurrentDictionary<string, double>();

        /// <summary>
        ///     Temporarily stores radius values
        /// </summary>
        private static ConcurrentDictionary<string, float> radiusDictionary = new ConcurrentDictionary<string, float>();

        /// <summary>
        ///     The sleeper.
        /// </summary>
        private static MultiSleeper sleeper = new MultiSleeper();

        /// <summary>
        ///     Temporarily stores speed values
        /// </summary>
        private static ConcurrentDictionary<string, float> speedDictionary = new ConcurrentDictionary<string, float>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Checks if given ability can be used
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <param name="bonusMana">
        ///     The bonus Mana.
        /// </param>
        /// <returns>
        ///     returns true in case ability can be used
        /// </returns>
        public static bool CanBeCasted(this Ability ability, float bonusMana = 0)
        {
            if (ability == null || !ability.IsValid)
            {
                return false;
            }

            var item = ability as Item;
            if (item != null)
            {
                return item.CanBeCasted(bonusMana);
            }

            try
            {
                var owner = ability.Owner as Hero;
                bool canBeCasted;
                if (owner == null)
                {
                    canBeCasted = ability.Level > 0 && ability.Cooldown <= Math.Max(Game.Ping / 1000 - 0.1, 0);
                    return canBeCasted;
                }

                if (owner.ClassId != ClassId.CDOTA_Unit_Hero_Invoker)
                {
                    canBeCasted = ability.Level > 0 && owner.Mana + bonusMana >= ability.ManaCost
                                  && ability.Cooldown <= Math.Max(Game.Ping / 1000 - 0.1, 0);
                    return canBeCasted;
                }

                var name = ability.StoredName();
                if (name != "invoker_invoke" && name != "invoker_quas" && name != "invoker_wex"
                    && name != "invoker_exort" && ability.AbilitySlot != AbilitySlot.Slot_4
                    && ability.AbilitySlot != AbilitySlot.Slot_5)
                {
                    return false;
                }

                canBeCasted = ability.Level > 0 && owner.Mana + bonusMana >= ability.ManaCost
                              && ability.Cooldown <= Math.Max(Game.Ping / 1000 - 0.1, 0);
                return canBeCasted;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     Checks if given ability can be used
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <returns>
        ///     returns true in case ability can be used
        /// </returns>
        public static bool CanBeCasted(this Ability ability, Unit target)
        {
            if (ability == null || !ability.IsValid)
            {
                return false;
            }

            if (target == null || !target.IsValid)
            {
                return false;
            }

            if (!target.IsValidTarget())
            {
                return false;
            }

            var canBeCasted = ability.CanBeCasted();
            if (!target.IsMagicImmune())
            {
                return canBeCasted;
            }

            var data = ability.CommonProperties();

            return data == null ? canBeCasted : data.MagicImmunityPierce;
        }

        /// <summary>
        ///     The can hit.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="abilityName">
        ///     The ability name.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool CanHit(this Ability ability, Unit target, string abilityName = null)
        {
            if (ability == null || !ability.IsValid)
            {
                return false;
            }

            if (target == null || !target.IsValid)
            {
                return false;
            }

            return CanHit(ability, target, ability.Owner.Position, abilityName);
        }

        /// <summary>
        ///     Checks if you could hit hero with given ability
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="sourcePosition">
        ///     The source Position.
        /// </param>
        /// <param name="abilityName">
        ///     The ability Name.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool CanHit(this Ability ability, Unit target, Vector3 sourcePosition, string abilityName = null)
        {
            if (ability == null || !ability.IsValid)
            {
                return false;
            }

            if (target == null || !target.IsValid)
            {
                return false;
            }

            var name = abilityName ?? ability.StoredName();
            if (ability.Owner.Equals(target))
            {
                return true;
            }

            var id = ability.Handle + target.Handle;
            if (sleeper.Sleeping(id))
            {
                return canHitDictionary[id];
            }

            var position = sourcePosition;
            if (ability.IsAbilityBehavior(AbilityBehavior.Point, name) || name == "lion_impale"
                || name == "earthshaker_enchant_totem" && (ability.Owner as Hero).AghanimState())
            {
                var pred = ability.GetPrediction(target, abilityName: name);
                var lion = name == "lion_impale" ? ability.GetAbilityData("length_buffer") : 0;
                return position.Distance2D(pred)
                       <= ability.TravelDistance() + ability.GetRadius(name) + lion + target.HullRadius;
            }

            if (ability.IsAbilityBehavior(AbilityBehavior.NoTarget, name))
            {
                var pred = ability.GetPrediction(target, abilityName: name);
                var distanceXyz = position.Distance2D(pred);
                var radius = ability.GetRadius(name);
                var range = ability.GetCastRange(name);
                if (name.StartsWith("nevermore_shadowraze"))
                {
                    range += radius / 2;
                }

                if (distanceXyz <= range && position.Distance2D(target.Position) <= range)
                {
                    canHitDictionary[id] = true;
                    sleeper.Sleep(50, id);
                    return true;
                }

                canHitDictionary[id] = name == "pudge_rot" && target.HasModifier("modifier_pudge_meat_hook")
                                       && position.Distance2D(target) < 1500;
                sleeper.Sleep(50, id);
                return canHitDictionary[id];
            }

            if (!ability.IsAbilityBehavior(AbilityBehavior.UnitTarget, name))
            {
                canHitDictionary[id] = false;
                sleeper.Sleep(50, id);
                return false;
            }

            if (target.IsInvul())
            {
                canHitDictionary[id] = false;
                sleeper.Sleep(50, id);
                return false;
            }

            if (position.Distance2D(target.Position) <= ability.GetCastRange(name) + 100)
            {
                canHitDictionary[id] = true;
                sleeper.Sleep(50, id);
                return true;
            }

            canHitDictionary[id] = name == "pudge_dismember" && target.HasModifier("modifier_pudge_meat_hook")
                                   && position.Distance2D(target) < 600;
            sleeper.Sleep(50, id);
            return canHitDictionary[id];
        }

        /// <summary>
        ///     The cast skill shot.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="abilityName">
        ///     The ability name.
        /// </param>
        /// <param name="soulRing">
        ///     The soul ring.
        /// </param>
        /// <param name="otherTargets">
        ///     The other targets.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool CastSkillShot(
            this Ability ability,
            Unit target,
            string abilityName = null,
            Ability soulRing = null,
            List<Unit> otherTargets = null)
        {
            if (ability == null || !ability.IsValid)
            {
                return false;
            }

            if (target == null || !target.IsValid)
            {
                return false;
            }

            return CastSkillShot(ability, target, ability.Owner.Position, abilityName, soulRing, otherTargets);
        }

        /// <summary>
        ///     Uses prediction to cast given skill shot ability
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="sourcePosition">
        ///     The source Position.
        /// </param>
        /// <param name="abilityName">
        ///     The ability Name.
        /// </param>
        /// <param name="soulRing">
        ///     The soul Ring.
        /// </param>
        /// <param name="otherTargets">
        ///     Targets which are supposed to be hit by AOE Skill Shot
        /// </param>
        /// <returns>
        ///     returns true in case of successful cast
        /// </returns>
        public static bool CastSkillShot(
            this Ability ability,
            Unit target,
            Vector3 sourcePosition,
            string abilityName = null,
            Ability soulRing = null,
            List<Unit> otherTargets = null)
        {
            if (ability == null || !ability.IsValid)
            {
                return false;
            }

            if (target == null || !target.IsValid)
            {
                return false;
            }

            if (!Utils.SleepCheck("CastSkillshot" + ability.Handle))
            {
                return false;
            }

            var name = abilityName ?? ability.StoredName();
            var owner = ability.Owner as Unit;
            var position = sourcePosition;
            var delay = ability.GetHitDelay(target, name);
            var data = ability.CommonProperties();

            // delay += data.AdditionalDelay;
            if (target.IsInvul() && !Utils.ChainStun(target, delay, null, false))
            {
                return false;
            }

            var xyz = ability.GetPrediction(target, abilityName: name);
            if (otherTargets != null)
            {
                var avPosX = otherTargets.Average(x => ability.GetPrediction(x, abilityName: name).X);
                var avPosY = otherTargets.Average(x => ability.GetPrediction(x, abilityName: name).Y);
                xyz = (xyz + new Vector3(avPosX, avPosY, 0)) / 2;
            }

            var radius = ability.GetRadius(name);
            var range = ability.TravelDistance();

            if (data.AllyBlock)
            {
                if (
                    Creeps.All.Any(
                        x =>
                            x.IsValid && x.IsAlive && x.Team == owner.Team && x.Distance2D(xyz) <= range
                            && x.Distance2D(owner) < owner.Distance2D(target)
                            && x.Position.ToVector2().DistanceToLineSegment(sourcePosition.ToVector2(), xyz.ToVector2())
                            <= radius + x.HullRadius))
                {
                    return false;
                }

                if (
                    Heroes.GetByTeam(owner.Team)
                        .Any(
                            hero =>
                                hero.IsAlive && !hero.Equals(owner) && !hero.Equals(target)
                                && hero.Distance2D(xyz) <= range && hero.Distance2D(owner) < owner.Distance2D(target)
                                && hero.Position.ToVector2()
                                    .DistanceToLineSegment(sourcePosition.ToVector2(), xyz.ToVector2())
                                <= radius + hero.HullRadius))
                {
                    return false;
                }
            }

            if (data.EnemyBlock)
            {
                if (
                    Creeps.All.Any(
                        x =>
                            x.IsValid && x.IsAlive && x.Team != owner.Team && x.Distance2D(xyz) <= range
                            && x.Distance2D(owner) < owner.Distance2D(target)
                            && x.Position.ToVector2().DistanceToLineSegment(sourcePosition.ToVector2(), xyz.ToVector2())
                            <= radius + x.HullRadius))
                {
                    return false;
                }

                if (
                    Heroes.GetByTeam(owner.GetEnemyTeam())
                        .Any(
                            hero =>
                                hero.IsAlive && !hero.Equals(target) && hero.Distance2D(xyz) <= range
                                && hero.Distance2D(owner) < owner.Distance2D(target)
                                && hero.Position.ToVector2()
                                    .DistanceToLineSegment(sourcePosition.ToVector2(), xyz.ToVector2())
                                <= radius + hero.HullRadius))
                {
                    return false;
                }
            }

            var speed = ability.GetProjectileSpeed(name);
            var distanceXyz = xyz.Distance2D(position);
            var lion = name == "lion_impale" ? ability.GetAbilityData("length_buffer") : 0;
            if (!(distanceXyz <= range + radius + lion + target.HullRadius))
            {
                return false;
            }

            if (distanceXyz > range)
            {
                xyz = xyz - position;
                xyz /= xyz.Length();
                xyz *= range;
                xyz += position;
            }

            // Console.WriteLine(ability.GetCastRange() + " " + radius);
            if (name.StartsWith("nevermore_shadowraze"))
            {
                xyz = Prediction.SkillShotXYZ(
                    owner,
                    target,
                    (float)((delay + (float)owner.GetTurnTime(xyz)) * 1000),
                    speed,
                    radius);

                // Console.WriteLine(distanceXyz + " " + range + " " + radius);
                if (distanceXyz < range + radius && distanceXyz > range - radius)
                {
                    if (owner.GetTurnTime(xyz) > 0.01)
                    {
                        owner.Move((owner.Position - xyz) * 25 / distanceXyz + xyz);
                        owner.Stop();
                    }
                    else
                    {
                        ability.UseAbility();
                    }

                    return true;
                }

                return false;
            }

            if (name == "invoker_ice_wall" && distanceXyz - 50 > 200 && distanceXyz - 50 < 610)
            {
                var mepred = (position - target.Position) * 50 / position.Distance2D(target) + target.Position;
                var v1 = xyz.X - mepred.X;
                var v2 = xyz.Y - mepred.Y;
                var a = Math.Acos(175 / xyz.Distance(mepred));
                var x1 = v1 * Math.Cos(a) - v2 * Math.Sin(a);
                var y1 = v2 * Math.Cos(a) + v1 * Math.Sin(a);
                var b = Math.Sqrt(x1 * x1 + y1 * y1);
                var k1 = x1 * 50 / b;
                var k2 = y1 * 50 / b;
                var vec1 = new Vector3((float)(k1 + mepred.X), (float)(k2 + mepred.Y), mepred.Z);
                if (vec1.Distance2D(mepred) > 0)
                {
                    owner.Move(mepred);
                    owner.Move(vec1, true);
                    ability.UseAbility(true);

                    return true;
                }

                return false;
            }

            if (ability.ManaCost > 0 && soulRing.CanBeCasted())
            {
                soulRing.UseAbility();
            }

            ability.UseAbility(xyz);
            return true;
        }

        /// <summary>
        ///     The cast stun.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="straightTimeforSkillShot">
        ///     The straight time for skill shot.
        /// </param>
        /// <param name="chainStun">
        ///     The chain stun.
        /// </param>
        /// <param name="useSleep">
        ///     The use sleep.
        /// </param>
        /// <param name="abilityName">
        ///     The ability name.
        /// </param>
        /// <param name="soulRing">
        ///     The soul ring.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool CastStun(
            this Ability ability,
            Unit target,
            float straightTimeforSkillShot = 0,
            bool chainStun = true,
            bool useSleep = true,
            string abilityName = null,
            Ability soulRing = null)
        {
            if (ability == null || !ability.IsValid)
            {
                return false;
            }

            if (target == null || !target.IsValid)
            {
                return false;
            }

            return CastStun(
                ability,
                target,
                ability.Owner.Position,
                straightTimeforSkillShot,
                chainStun,
                useSleep,
                abilityName,
                soulRing);
        }

        /// <summary>
        ///     Uses given ability in case enemy is not disabled or would be chain stunned.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="sourcePosition">
        ///     The source Position.
        /// </param>
        /// <param name="straightTimeforSkillShot">
        ///     The straight Time for Skill Shot.
        /// </param>
        /// <param name="chainStun">
        ///     The chain Stun.
        /// </param>
        /// <param name="useSleep">
        ///     The use Sleep.
        /// </param>
        /// <param name="abilityName">
        ///     The ability Name.
        /// </param>
        /// <param name="soulRing">
        ///     The soul Ring.
        /// </param>
        /// <param name="otherTargets">
        ///     The other Targets.
        /// </param>
        /// <returns>
        ///     returns true in case of successful cast
        /// </returns>
        public static bool CastStun(
            this Ability ability,
            Unit target,
            Vector3 sourcePosition,
            float straightTimeforSkillShot = 0,
            bool chainStun = true,
            bool useSleep = true,
            string abilityName = null,
            Ability soulRing = null,
            List<Unit> otherTargets = null)
        {
            if (!ability.CanBeCasted())
            {
                return false;
            }

            if (target == null || !target.IsValid)
            {
                return false;
            }

            var name = abilityName ?? ability.StoredName();
            var delay = ability.GetHitDelay(target, name) + 0.1f;
            var canUse = Utils.ChainStun(target, delay, null, false, name);
            if (!canUse && chainStun
                && (!target.HasModifier("modifier_pudge_meat_hook")
                    || ability.StoredName() != "pudge_dismember" && ability.StoredName() != "pudge_rot"))
            {
                return false;
            }

            if (ability.IsAbilityBehavior(AbilityBehavior.UnitTarget, name) && name != "lion_impale"
                && !target.IsInvul())
            {
                if (!ability.CanHit(target, sourcePosition))
                {
                    return false;
                }

                if (ability.ManaCost > 0 && soulRing.CanBeCasted())
                {
                    soulRing.UseAbility();
                }

                ability.UseAbility(target);
            }
            else if (ability.IsAbilityBehavior(AbilityBehavior.AreaOfEffect, name)
                     || ability.IsAbilityBehavior(AbilityBehavior.Point, name) || name == "lion_impale"
                     || name == "earthshaker_enchant_totem" && (ability.Owner as Hero).AghanimState()
                     || ability.IsSkillShot())
            {
                var stunned = target.IsStunned() || target.IsInvul() || target.IsRooted() || target.IsHexed();
                if (!(Prediction.StraightTime(target) > straightTimeforSkillShot * 1000) && !stunned
                    || !ability.CastSkillShot(target, name, soulRing, otherTargets))
                {
                    return false;
                }

                if (useSleep)
                {
                    Utils.Sleep(Math.Max(delay, 0.2) * 1000 + 250, "CHAINSTUN_SLEEP");
                }

                return true;
            }
            else if (ability.IsAbilityBehavior(AbilityBehavior.NoTarget, name))
            {
                if (name == "invoker_ice_wall")
                {
                    ability.CastSkillShot(target, name);
                }
                else
                {
                    if (ability.ManaCost > 0 && soulRing.CanBeCasted())
                    {
                        soulRing.UseAbility();
                    }

                    ability.UseAbility();
                }
            }

            if (useSleep)
            {
                Utils.Sleep(Math.Max(delay, 0.2) * 1000 + 250, "CHAINSTUN_SLEEP");
            }

            return true;
        }

        /// <summary>
        ///     The channel time.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <param name="abilityName">
        ///     The ability name.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float ChannelTime(this Ability ability, string abilityName = null)
        {
            return ChannelTime(ability, 0, abilityName);
        }

        /// <summary>
        ///     The channel time.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <param name="abilityLevel">
        ///     The ability Level.
        /// </param>
        /// <param name="abilityName">
        ///     The ability name.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float ChannelTime(this Ability ability, uint abilityLevel, string abilityName = null)
        {
            if (ability == null || !ability.IsValid)
            {
                return 0;
            }

            var level = abilityLevel != 0 ? abilityLevel : ability.Level;
            var name = abilityName ?? ability.StoredName();
            float channel;
            if (!channelDictionary.TryGetValue(name + level, out channel))
            {
                channel = ability.GetChannelTime(level - 1);
                channelDictionary.TryAdd(name + level, channel);
            }

            // Console.WriteLine(ability.GetChannelTime(ability.Level - 1) + "  " + delay + " " + name);
            return channel;
        }

        /// <summary>
        ///     The common properties.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <returns>
        ///     The <see cref="AbilityInfo" />.
        /// </returns>
        public static AbilityInfo CommonProperties(this Ability ability)
        {
            return AbilityDatabase.Find(ability.StoredName());
        }

        /// <summary>
        ///     The end radius.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float EndRadius(this Ability ability)
        {
            var data = ability.CommonProperties();
            if (data == null)
            {
                return ability.GetRadius();
            }

            var radius = ability.GetAbilityData(data.EndWidth);
            return radius > 0 ? radius : ability.GetRadius();
        }

        /// <summary>
        ///     Returns cast point of given ability
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <param name="abilityName">
        ///     The ability Name.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double FindCastPoint(this Ability ability, string abilityName = null)
        {
            if (ability == null || !ability.IsValid)
            {
                return 0;
            }

            if (ability is Item)
            {
                return 0;
            }

            if (ability.OverrideCastPoint != -1)
            {
                return 0.1;
            }

            var name = abilityName ?? ability.StoredName();
            double castPoint;
            if (castPointDictionary.TryGetValue(name + " " + ability.Level, out castPoint))
            {
                return castPoint;
            }

            castPoint = ability.GetCastPoint(ability.Level);
            castPointDictionary.TryAdd(name + " " + ability.Level, castPoint);
            return castPoint;
        }

        /// <summary>
        ///     Returns ability data with given name, checks if data are level dependent or not
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <param name="dataName">
        ///     The data Name.
        /// </param>
        /// <param name="level">
        ///     Custom level
        /// </param>
        /// <param name="abilityName">
        ///     The ability Name.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float GetAbilityData(
            this Ability ability,
            string dataName,
            uint level = 0,
            string abilityName = null)
        {
            if (ability == null || !ability.IsValid)
            {
                return 0;
            }

            var lvl = ability.Level;
            var name = abilityName ?? ability.StoredName();
            AbilitySpecialData data;
            if (!dataDictionary.TryGetValue(name + "_" + dataName, out data))
            {
                data = ability.AbilitySpecialData.FirstOrDefault(x => x.Name == dataName);
                dataDictionary.TryAdd(name + "_" + dataName, data);
            }

            if (level > 0)
            {
                lvl = level;
            }

            if (data == null)
            {
                return 0;
            }

            return data.Count > 1 ? data.GetValue(lvl - 1) : data.Value;
        }

        /// <summary>
        ///     Returns the ability id
        /// </summary>
        /// <param name="ability"></param>
        /// <returns></returns>
        public static AbilityId GetAbilityId(this Ability ability)
        {
            return (AbilityId)ability.AbilityData.Id;
        }

        /// <summary>
        ///     The get cast delay.
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
        /// <param name="usePing">
        ///     The use ping.
        /// </param>
        /// <param name="useCastPoint">
        ///     The use cast point.
        /// </param>
        /// <param name="abilityName">
        ///     The ability name.
        /// </param>
        /// <param name="useChannel">
        ///     The use channel.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double GetCastDelay(
            this Ability ability,
            Hero source,
            Unit target,
            bool usePing = false,
            bool useCastPoint = true,
            string abilityName = null,
            bool useChannel = false)
        {
            return ability.GetCastDelay(source as Unit, target, usePing, useCastPoint, abilityName, useChannel);
        }

        /// <summary>
        ///     The get cast delay.
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
        /// <param name="usePing">
        ///     The use ping.
        /// </param>
        /// <param name="useCastPoint">
        ///     The use cast point.
        /// </param>
        /// <param name="abilityName">
        ///     The ability name.
        /// </param>
        /// <param name="useChannel">
        ///     The use channel.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double GetCastDelay(
            this Ability ability,
            Unit source,
            Unit target,
            bool usePing = false,
            bool useCastPoint = true,
            string abilityName = null,
            bool useChannel = false)
        {
            return ability.GetCastDelay(source, target, ability.Level, usePing, useCastPoint, abilityName, useChannel);
        }

        /// <summary>
        ///     Returns delay before ability is casted
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
        /// <param name="abilityLevel">
        ///     The ability Level.
        /// </param>
        /// <param name="usePing">
        ///     The use Ping.
        /// </param>
        /// <param name="useCastPoint">
        ///     The use Cast Point.
        /// </param>
        /// <param name="abilityName">
        ///     The ability Name.
        /// </param>
        /// <param name="useChannel">
        ///     The use Channel.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double GetCastDelay(
            this Ability ability,
            Unit source,
            Unit target,
            uint abilityLevel,
            bool usePing = false,
            bool useCastPoint = true,
            string abilityName = null,
            bool useChannel = false)
        {
            if (ability == null || !ability.IsValid)
            {
                return 0;
            }

            if (target == null || !target.IsValid || source == null || !source.IsValid)
            {
                return 0;
            }

            var level = abilityLevel != 0 ? abilityLevel : ability.Level;
            var name = abilityName ?? ability.StoredName();
            double delay;
            if (useCastPoint)
            {
                if (!delayDictionary.TryGetValue(name + " " + level, out delay))
                {
                    delay = Math.Max(ability.FindCastPoint(name), 0.07);
                    delayDictionary.TryAdd(name + " " + level, delay);
                }

                if (name == "templar_assassin_meld")
                {
                    delay += UnitDatabase.GetAttackPoint(source) + Game.Ping / 500 + 0.1 + source.GetTurnTime(target);
                }

                if (name == "item_diffusal_blade" || name == "item_diffusal_blade_2")
                {
                    delay += 2;
                }
            }
            else
            {
                if (ability is Item)
                {
                    delay = 0;
                }
                else
                {
                    delay = 0.05;
                }
            }

            if (usePing)
            {
                delay += Game.Ping / 1000;
            }

            if (useChannel)
            {
                delay += ability.ChannelTime(level, name);
            }

            if (!ability.IsAbilityBehavior(AbilityBehavior.NoTarget, name))
            {
                return Math.Max(delay + (!target.Equals(source) ? source.GetTurnTime(target) : 0), 0);
            }

            return Math.Max(delay, 0);
        }

        /// <summary>
        ///     Returns cast range of ability, if ability is NonTargeted it will return its radius!
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <param name="abilityName">
        ///     The ability Name.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float GetCastRange(this Ability ability, string abilityName = null)
        {
            if (ability == null || !ability.IsValid)
            {
                return 0;
            }

            var name = abilityName ?? ability.StoredName();
            var owner = ability.Owner;
            var n = name + owner.Handle;
            if (castRangeDictionary.ContainsKey(n) && !Utils.SleepCheck("Common.GetCastRange." + n))
            {
                return castRangeDictionary[n];
            }

            if (name == "templar_assassin_meld")
            {
                return (ability.Owner as Unit).GetAttackRange() + 50;
            }

            var data = ability.CommonProperties();
            if (!ability.IsAbilityBehavior(AbilityBehavior.NoTarget, name))
            {
                var castRange = (float)ability.CastRange;
                var bonusRange = 0f;
                if (data != null && data.RealCastRange != null)
                {
                    castRange = ability.GetAbilityData(data.RealCastRange, abilityName: name);
                }

                if (castRange <= 0)
                {
                    castRange = 999999;
                }

                var hero = owner as Hero;
                if (hero != null && name == "dragon_knight_dragon_tail"
                    && hero.HasModifier("modifier_dragon_knight_dragon_form"))
                {
                    bonusRange = 250;
                }
                else if (hero != null && name == "beastmaster_primal_roar" && hero.AghanimState())
                {
                    bonusRange = 350;
                }

                var aetherLens = hero?.FindItem("item_aether_lens", true);
                if (aetherLens != null)
                {
                    bonusRange += aetherLens.GetAbilityData("cast_range_bonus");
                }

                var talent = hero?.Spellbook.Spells.FirstOrDefault(x => x.Name.StartsWith("special_bonus_cast_range_"));
                if (talent?.Level > 0)
                {
                    bonusRange += talent.GetAbilityData("value");
                }

                if (!castRangeDictionary.ContainsKey(n))
                {
                    castRangeDictionary.TryAdd(n, castRange + bonusRange);
                    Utils.Sleep(5000, "Common.GetCastRange." + n);
                }
                else
                {
                    castRangeDictionary[n] = castRange + bonusRange;
                    Utils.Sleep(5000, "Common.GetCastRange." + n);
                }

                return castRange + bonusRange;
            }

            float radius;
            if (data == null)
            {
                return ability.CastRange;
            }

            if (ability.StoredName() == "earthshaker_enchant_totem" && (owner as Hero).AghanimState())
            {
                radius = ability.GetAbilityData("scepter_distance") + 100;
            }
            else if (!data.FakeCastRange)
            {
                radius = ability.GetRadius(name);
            }
            else
            {
                radius = ability.GetAbilityData(data.RealCastRange, abilityName: name);
            }

            if (!castRangeDictionary.ContainsKey(n))
            {
                castRangeDictionary.TryAdd(n, radius);
                Utils.Sleep(5000, "Common.GetCastRange." + n);
            }
            else
            {
                castRangeDictionary[n] = radius;
                Utils.Sleep(5000, "Common.GetCastRange." + n);
            }

            return radius;
        }

        /// <summary>
        ///     Checks all aspects and returns full delay before target gets hit by given ability
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="abilityName">
        ///     The ability Name.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double GetHitDelay(this Ability ability, Unit target, string abilityName = null)
        {
            if (ability == null || !ability.IsValid)
            {
                return 0;
            }

            if (target == null || !target.IsValid)
            {
                return 0;
            }

            var name = abilityName ?? ability.StoredName();
            var owner = ability.Owner as Unit;
            var n = name + owner.StoredName() + target.StoredName();
            double storedDelay;
            var found = hitDelayDictionary.TryGetValue(n, out storedDelay);
            if (!found)
            {
                hitDelayDictionary.TryAdd(n, 0);
            }

            if (found && !Utils.SleepCheck(n))
            {
                return storedDelay;
            }

            var data = ability.CommonProperties();
            var delay = ability.GetCastDelay(owner as Hero, target, true, abilityName: name);
            if (data != null)
            {
                delay += data.AdditionalDelay;
            }

            var speed = ability.GetProjectileSpeed(name);
            var radius = ability.GetRadius(name);
            if (!ability.IsAbilityBehavior(AbilityBehavior.NoTarget, name) && speed < 6000 && speed > 0)
            {
                var xyz = ability.GetPrediction(target, abilityName: name);
                delay += Math.Max((int)(owner.Distance2D(xyz) - radius / 2), 100) / speed;
            }

            if (name == "tinker_heat_seeking_missile")
            {
                var xyz = ability.GetPrediction(target, abilityName: name);
                delay += Math.Max(owner.Distance2D(xyz), 100) / speed;
            }

            hitDelayDictionary[n] = delay;
            Utils.Sleep(40, n);
            return delay;
        }

        /// <summary>
        ///     Returns prediction for given target after given ability hit delay
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="customDelay">
        ///     enter your custom delay
        /// </param>
        /// <param name="abilityName">
        ///     The ability Name.
        /// </param>
        /// <returns>
        ///     The <see cref="Vector3" />.
        /// </returns>
        public static Vector3 GetPrediction(
            this Ability ability,
            Unit target,
            double customDelay = 0,
            string abilityName = null)
        {
            if (ability == null || !ability.IsValid)
            {
                return new Vector3();
            }

            if (target == null || !target.IsValid)
            {
                return new Vector3();
            }

            var name = abilityName ?? ability.StoredName();
            var data = ability.CommonProperties();
            var owner = ability.Owner as Unit;
            var delay = ability.GetCastDelay(owner as Hero, target, true, abilityName: name, useChannel: true);
            if (data != null)
            {
                delay += data.AdditionalDelay;
            }

            var speed = ability.GetProjectileSpeed(name);
            var radius = ability.GetRadius(name);
            Vector3 xyz;
            if (speed > 0 && speed < 6000)
            {
                xyz = Prediction.SkillShotXYZ(
                    owner,
                    target,
                    (float)((delay + owner.GetTurnTime(target.Position)) * 1000),
                    speed,
                    radius);
                if (!ability.IsAbilityBehavior(AbilityBehavior.NoTarget, name))
                {
                    xyz = Prediction.SkillShotXYZ(
                        owner,
                        target,
                        (float)((delay + (float)owner.GetTurnTime(xyz)) * 1000),
                        speed,
                        radius);
                }
            }
            else
            {
                xyz = Prediction.PredictedXYZ(target, (float)(delay * 1000));
            }

            return xyz;
        }

        /// <summary>
        ///     The get projectile speed.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <param name="abilityName">
        ///     The ability name.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float GetProjectileSpeed(this Ability ability, string abilityName = null)
        {
            return GetProjectileSpeed(ability, 0, abilityName);
        }

        /// <summary>
        ///     Returns projectile speed of the ability
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <param name="abilityLevel">
        ///     The ability Level.
        /// </param>
        /// <param name="abilityName">
        ///     The ability Name.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float GetProjectileSpeed(this Ability ability, uint abilityLevel, string abilityName = null)
        {
            if (ability == null || !ability.IsValid)
            {
                return 0;
            }

            var level = abilityLevel != 0 ? abilityLevel : ability.Level;
            var name = abilityName ?? ability.StoredName();
            float speed;
            if (speedDictionary.TryGetValue(name + " " + level, out speed))
            {
                return speed;
            }

            var data = ability.CommonProperties();
            if (data == null)
            {
                speed = float.MaxValue;
                speedDictionary.TryAdd(name + " " + level, speed);
                return speed;
            }

            if (data.Speed == null)
            {
                return speed;
            }

            speed = ability.GetAbilityData(data.Speed, abilityName: name);
            speedDictionary.TryAdd(name + " " + level, speed);

            return speed;
        }

        /// <summary>
        ///     Returns impact radius of given ability
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <param name="abilityName">
        ///     The ability Name.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float GetRadius(this Ability ability, string abilityName = null)
        {
            if (ability == null || !ability.IsValid)
            {
                return 0;
            }

            var name = abilityName ?? ability.StoredName();
            float radius;
            if (radiusDictionary.TryGetValue(name + " " + ability.Level, out radius))
            {
                return radius;
            }

            var data = ability.CommonProperties();
            if (data == null)
            {
                radius = 0;
                radiusDictionary.TryAdd(name + " " + ability.Level, radius);
                return radius;
            }

            if (data.Width != null)
            {
                radius = ability.GetAbilityData(data.Width, abilityName: name);
                radiusDictionary.TryAdd(name + " " + ability.Level, radius);
                return radius;
            }

            if (data.StringRadius != null)
            {
                radius = ability.GetAbilityData(data.StringRadius, abilityName: name);
                radiusDictionary.TryAdd(name + " " + ability.Level, radius);
                return radius;
            }

            if (data.Radius > 0)
            {
                radius = data.Radius;
                radiusDictionary.TryAdd(name + " " + ability.Level, radius);
                return radius;
            }

            if (!data.IsBuff)
            {
                return radius;
            }

            radius = (ability.Owner as Hero).GetAttackRange() + 150;
            radiusDictionary.TryAdd(name + " " + ability.Level, radius);
            return radius;
        }

        /// <summary>
        ///     Checks if this ability can be casted by Invoker, if the ability is not currently invoked, it is going to check for
        ///     both invoke and the ability mana cost.
        /// </summary>
        /// <param name="ability">
        ///     given ability
        /// </param>
        /// <param name="invoke">
        ///     invoker ultimate
        /// </param>
        /// <param name="spell4">
        ///     current spell on slot 4
        /// </param>
        /// <param name="spell5">
        ///     current spell on slot 5
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool InvoCanBeCasted(this Ability ability, Ability invoke, Ability spell4, Ability spell5)
        {
            if (ability == null || !ability.IsValid)
            {
                return false;
            }

            var owner = ability.Owner as Hero;
            if (owner == null)
            {
                return false;
            }

            if (!(ability is Item) && ability.StoredName() != "invoker_invoke" && ability.StoredName() != "invoker_quas"
                && ability.StoredName() != "invoker_wex" && ability.StoredName() != "invoker_exort"
                && !ability.Equals(spell4) && !ability.Equals(spell5))
            {
                return invoke.Level > 0 && invoke.Cooldown <= 0 && ability.Cooldown <= 0
                       && ability.ManaCost + invoke.ManaCost <= owner.Mana;
            }

            return ability.AbilityState == AbilityState.Ready;
        }

        /// <summary>
        ///     Checks if given ability has given ability behavior flag
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <param name="flag">
        ///     The flag.
        /// </param>
        /// <param name="abilityName">
        ///     The ability Name.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsAbilityBehavior(this Ability ability, AbilityBehavior flag, string abilityName = null)
        {
            if (ability == null || !ability.IsValid)
            {
                return false;
            }

            var name = abilityName ?? ability.StoredName();
            AbilityBehavior data;
            if (abilityBehaviorDictionary.TryGetValue(name, out data))
            {
                return data.HasFlag(flag);
            }

            data = ability.AbilityBehavior;
            abilityBehaviorDictionary.TryAdd(name, data);
            return data.HasFlag(flag);
        }

        /// <summary>
        ///     The is ability type.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <param name="type">
        ///     The type.
        /// </param>
        /// <param name="abilityName">
        ///     The ability name.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsAbilityType(this Ability ability, AbilityType type, string abilityName = null)
        {
            if (ability == null || !ability.IsValid)
            {
                return false;
            }

            var name = abilityName ?? ability.StoredName();
            var n = name + "abilityType" + type;
            if (boolDictionary.ContainsKey(n))
            {
                return boolDictionary[n];
            }

            var value = ability.AbilityType == type;
            boolDictionary.TryAdd(n, value);
            return value;
        }

        /// <summary>
        ///     The is disable.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsDisable(this Ability ability)
        {
            var data = ability.CommonProperties();
            return data != null && data.IsDisable;
        }

        /// <summary>
        ///     The is heal.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsHeal(this Ability ability)
        {
            var data = ability.CommonProperties();
            return data != null && data.IsHeal;
        }

        /// <summary>
        ///     The is invisibility.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsInvis(this Ability ability)
        {
            var data = ability.CommonProperties();
            return data != null && data.IsInvis;
        }

        /// <summary>
        ///     The is manaburn.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsManaburn(this Ability ability)
        {
            var data = ability.CommonProperties();
            return data != null && data.ManaBurn;
        }

        /// <summary>
        ///     The is nuke.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsNuke(this Ability ability)
        {
            var data = ability.CommonProperties();
            return data != null && data.IsNuke;
        }

        /// <summary>
        ///     The is purge.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsPurge(this Ability ability)
        {
            var data = ability.CommonProperties();
            return data != null && data.IsPurge;
        }

        /// <summary>
        ///     The is shield.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsShield(this Ability ability)
        {
            var data = ability.CommonProperties();
            return data != null && data.IsShield;
        }

        /// <summary>
        ///     The is silence.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsSilence(this Ability ability)
        {
            var data = ability.CommonProperties();
            return data != null && data.IsSilence;
        }

        /// <summary>
        ///     The is skill shot.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsSkillShot(this Ability ability)
        {
            var data = ability.CommonProperties();
            return data != null && data.IsSkillShot;
        }

        /// <summary>
        ///     The is slow.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsSlow(this Ability ability)
        {
            var data = ability.CommonProperties();
            return data != null && data.IsSlow;
        }

        /// <summary>
        ///     The pierces magic immunity.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool PiercesMagicImmunity(this Ability ability)
        {
            var data = ability.CommonProperties();
            return data != null && data.MagicImmunityPierce;
        }

        /// <summary>
        ///     The requires charges.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <param name="abilityName">
        ///     The ability name.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool RequiresCharges(this Ability ability, string abilityName = null)
        {
            if (ability == null || !ability.IsValid)
            {
                return false;
            }

            var name = abilityName ?? ability.StoredName();
            try
            {
                return Game.FindKeyValues(name + "/ItemRequiresCharges", KeyValueSource.Ability).IntValue == 1;
            }
            catch (KeyValuesNotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        ///     The travel distance.
        /// </summary>
        /// <param name="ability">
        ///     The ability.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float TravelDistance(this Ability ability)
        {
            var data = ability.CommonProperties();
            if (data == null)
            {
                return ability.GetCastRange();
            }

            var distance = ability.GetAbilityData(data.Distance);
            return distance > 0 ? distance : ability.GetCastRange();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The initialize.
        /// </summary>
        internal static void Init()
        {
            hitDelayDictionary = new ConcurrentDictionary<string, double>();
            dataDictionary = new ConcurrentDictionary<string, AbilitySpecialData>();
            channelDictionary = new ConcurrentDictionary<string, float>();
            castRangeDictionary = new ConcurrentDictionary<string, float>();
            castPointDictionary = new ConcurrentDictionary<string, double>();
            boolDictionary = new ConcurrentDictionary<string, bool>();
            abilityBehaviorDictionary = new ConcurrentDictionary<string, AbilityBehavior>();
            speedDictionary = new ConcurrentDictionary<string, float>();
            radiusDictionary = new ConcurrentDictionary<string, float>();
            delayDictionary = new ConcurrentDictionary<string, double>();
            canHitDictionary = new ConcurrentDictionary<uint, bool>();
            sleeper = new MultiSleeper();
        }

        #endregion
    }
}