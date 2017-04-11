// <copyright file="TargetSelector.cs" company="EnsageSharp">
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
namespace Ensage.Common
{
    using System;
    using System.Linq;

    using Ensage.Common.Extensions;
    using Ensage.Common.Objects;

    /// <summary>
    ///     Class used to find targets based on conditions
    /// </summary>
    public class TargetSelector
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Find enemy hero that takes least hits to kill
        /// </summary>
        /// <param name="source">
        ///     Source hero
        /// </param>
        /// <param name="bonusRange">
        ///     The bonus Range.
        /// </param>
        /// <returns>
        ///     The <see cref="Hero" />.
        /// </returns>
        public static Hero BestAutoAttackTarget(Unit source, float bonusRange = 0)
        {
            var attackRange = source.GetAttackRange();
            var aaDmg = source.MinimumDamage + source.BonusDamage;
            Hero bestTarget = null;
            var lastHitsToKill = 0f;
            foreach (var enemyHero in Heroes.All)
            {
                if (
                    !(enemyHero.IsValid && enemyHero.Team != source.Team && enemyHero.IsAlive && enemyHero.IsVisible
                      && enemyHero.Distance2D(source)
                      <= attackRange + enemyHero.HullRadius + bonusRange + source.HullRadius + 50
                      && !enemyHero.IsInvul()
                      && !enemyHero.HasModifier("modifier_skeleton_king_reincarnation_scepter_active")))
                {
                    continue;
                }

                var takenDmg = enemyHero.DamageTaken(aaDmg, DamageType.Physical, source, false);
                var hitsToKill = enemyHero.Health / takenDmg;
                if (bestTarget != null && !(lastHitsToKill > hitsToKill))
                {
                    continue;
                }

                bestTarget = enemyHero;
                lastHitsToKill = hitsToKill;
            }

            return bestTarget;
        }

        /// <summary>
        ///     Finds target closest to mouse in specified range
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
        public static Hero ClosestToMouse(Unit source, float range = 1000)
        {
            var mousePosition = Game.MousePosition;
            Hero closestHero = null;
            foreach (var x in Heroes.All)
            {
                if (
                    !(x.IsValid && x.Team != source.Team && !x.IsIllusion && x.IsAlive && x.IsVisible
                      && x.Distance2D(mousePosition) <= range
                      && !x.HasModifier("modifier_skeleton_king_reincarnation_scepter_active")))
                {
                    continue;
                }

                if (closestHero == null || closestHero.Distance2D(mousePosition) > x.Distance2D(mousePosition))
                {
                    closestHero = x;
                }
            }

            return closestHero;
        }

        /// <summary>
        ///     Checks for lowest health creep in attack range
        /// </summary>
        /// <param name="source">
        ///     The source.
        /// </param>
        /// <param name="bonusRange">
        ///     The bonus Range.
        /// </param>
        /// <returns>
        ///     The <see cref="Unit" />.
        /// </returns>
        public static Unit GetLowestHPCreep(Unit source, float bonusRange = 0)
        {
            try
            {
                var attackRange = source.GetAttackRange() + bonusRange;
                var lowestHp =
                    Creeps.All.Where(
                            x =>
                                x.IsValid && x.IsSpawned
                                && (x.ClassId == ClassId.CDOTA_BaseNPC_Tower
                                    || x.ClassId == ClassId.CDOTA_BaseNPC_Creep_Lane
                                    || x.ClassId == ClassId.CDOTA_BaseNPC_Creep
                                    || x.ClassId == ClassId.CDOTA_BaseNPC_Creep_Neutral
                                    || x.ClassId == ClassId.CDOTA_BaseNPC_Creep_Siege
                                    || x.ClassId == ClassId.CDOTA_BaseNPC_Additive
                                    || x.ClassId == ClassId.CDOTA_BaseNPC_Barracks
                                    || x.ClassId == ClassId.CDOTA_BaseNPC_Building
                                    || x.ClassId == ClassId.CDOTA_BaseNPC_Creature) && x.IsAlive && x.IsVisible
                                && x.Team != source.Team && x.Distance2D(source) < attackRange + 100)
                        .MinOrDefault(creep => creep.Health);
                return lowestHp;
            }
            catch (Exception)
            {
                // no   
            }

            return null;
        }

        /// <summary>
        ///     The highest health points target.
        /// </summary>
        /// <param name="source">
        ///     The source hero (LocalHero).
        /// </param>
        /// <param name="range">
        ///     The range.
        /// </param>
        /// <returns>
        ///     The <see cref="Hero" />.
        /// </returns>
        public static Hero HighestHealthPointsTarget(Unit source, float range)
        {
            return
                Heroes.GetByTeam(source.GetEnemyTeam())
                    .Where(
                        hero =>
                            hero.IsValid && hero.IsAlive && hero.IsVisible && hero.Distance2D(source) <= range
                            && !hero.IsInvul()
                            && !hero.HasModifier("modifier_skeleton_king_reincarnation_scepter_active"))
                    .MaxOrDefault(hero => hero.Health);
        }

        #endregion
    }
}