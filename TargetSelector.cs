// <copyright file="TargetSelector.cs" company="EnsageSharp">
//    Copyright (c) 2015 EnsageSharp.
// 
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
// 
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
// 
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>

namespace Ensage.Common
{
    using System;
    using System.Linq;

    using Ensage.Common.Extensions;

    /// <summary>
    ///     Class used to find targets based on conditions
    /// </summary>
    public class TargetSelector
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Find enemy hero that takes least hits to kill
        /// </summary>
        /// <param name="source">Source hero</param>
        /// <returns></returns>
        public static Hero BestAutoAttackTarget(Hero source)
        {
            var attackRange = source.GetAttackRange();
            var enemyHeroes =
                ObjectMgr.GetEntities<Hero>()
                    .Where(
                        x =>
                        x.Team == source.GetEnemyTeam() && !x.IsIllusion && x.IsAlive && x.IsVisible
                        && x.Distance2D(source) <= (attackRange + x.HullRadius + source.HullRadius + 50));
            var aaDmg = source.MinimumDamage + source.BonusDamage;
            Hero bestTarget = null;
            var lastHitsToKill = 0f;
            foreach (var enemyHero in enemyHeroes)
            {
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
        /// <param name="source"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public static Hero ClosestToMouse(Hero source, float range = 1000)
        {
            var mousePosition = Game.MousePosition;
            var enemyHeroes =
                ObjectMgr.GetEntities<Hero>()
                    .Where(
                        x =>
                        x.Team == source.GetEnemyTeam() && !x.IsIllusion && x.IsAlive && x.IsVisible
                        && x.Distance2D(mousePosition) <= range);
            Hero closestHero = null;
            foreach (var enemyHero in enemyHeroes)
            {
                if (closestHero == null || closestHero.Distance2D(mousePosition) > enemyHero.Distance2D(mousePosition))
                {
                    closestHero = enemyHero;
                }
            }
            return closestHero;
        }

        /// <summary>
        ///     Checks for lowest hp creep in attack range
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Unit GetLowestHPCreep(Hero source)
        {
            try
            {
                var attackRange = source.GetAttackRange();
                var lowestHp =
                    ObjectMgr.GetEntities<Unit>()
                        .Where(
                            x =>
                            (x.ClassID == ClassID.CDOTA_BaseNPC_Tower || x.ClassID == ClassID.CDOTA_BaseNPC_Creep_Lane
                             || x.ClassID == ClassID.CDOTA_BaseNPC_Creep
                             || x.ClassID == ClassID.CDOTA_BaseNPC_Creep_Neutral
                             || x.ClassID == ClassID.CDOTA_BaseNPC_Creep_Siege
                             || x.ClassID == ClassID.CDOTA_BaseNPC_Additive
                             || x.ClassID == ClassID.CDOTA_BaseNPC_Barracks
                             || x.ClassID == ClassID.CDOTA_BaseNPC_Building
                             || x.ClassID == ClassID.CDOTA_BaseNPC_Creature) && x.IsAlive && x.IsVisible
                            && x.Team != source.Team && x.Distance2D(source) < (attackRange + 100))
                        .OrderBy(creep => creep.Health)
                        .DefaultIfEmpty(null)
                        .FirstOrDefault();
                return lowestHp;
            }
            catch (Exception)
            {
                //no   
            }
            return null;
        }

        #endregion
    }
}