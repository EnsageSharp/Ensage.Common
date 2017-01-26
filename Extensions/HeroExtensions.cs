// <copyright file="HeroExtensions.cs" company="EnsageSharp">
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
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.Common.Objects;

    /// <summary>
    ///     The hero extensions.
    /// </summary>
    public static class HeroExtensions
    {
        #region Static Fields

        /// <summary>
        ///     The boolean dictionary.
        /// </summary>
        private static Dictionary<string, bool> boolDictionary = new Dictionary<string, bool>();

        /// <summary>
        ///     The range dictionary.
        /// </summary>
        private static Dictionary<float, float> rangeDictionary = new Dictionary<float, float>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Checks if given hero has AghanimScepter
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool AghanimState(this Hero hero)
        {
            return
                hero.HasModifiers(
                    new[] { "modifier_item_ultimate_scepter_consumed", "modifier_item_ultimate_scepter" },
                    false);
        }

        /// <summary>
        ///     The attack backswing.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double AttackBackswing(this Hero hero)
        {
            return UnitDatabase.GetAttackBackswing(hero);
        }

        /// <summary>
        ///     The attack point.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double AttackPoint(this Hero hero)
        {
            return UnitDatabase.GetAttackPoint(hero);
        }

        /// <summary>
        ///     The attack rate.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double AttackRate(this Hero hero)
        {
            return UnitDatabase.GetAttackRate(hero);
        }

        /// <summary>
        ///     The can die.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
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
        public static bool CanDie(this Hero hero, string sourceAbilityName = null, bool ignoreReincarnation = false)
        {
            var cullingBlade = sourceAbilityName != null && sourceAbilityName == "axe_culling_blade";
            return !ignoreReincarnation && !hero.CanReincarnate()
                   && (cullingBlade
                           ? !hero.HasModifier("modifier_skeleton_king_reincarnation_scepter_active")
                           : !hero.HasModifiers(
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
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool CanGoInvis(this Hero hero)
        {
            var n = hero.Handle + "CanGoInvis";
            if (!Utils.SleepCheck(n))
            {
                return boolDictionary[n];
            }

            Ability invis = null;
            Ability riki = null;
            foreach (var x in hero.Spellbook.Spells)
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
                    hero.Inventory.Items.FirstOrDefault(
                        x =>
                            x.StoredName() == "item_invis_sword" || x.StoredName() == "item_silver_edge"
                            || x.StoredName() == "item_glimmer_cape");
            }

            var canGoInvis = invis != null && hero.CanCast() && invis.CanBeCasted()
                             || riki != null && riki.Level > 0 && !hero.IsSilenced();
            if (!boolDictionary.ContainsKey(n))
            {
                boolDictionary.Add(n, canGoInvis);
            }
            else
            {
                boolDictionary[n] = canGoInvis;
            }

            Utils.Sleep(150, n);
            return canGoInvis;
        }

        /// <summary>
        ///     The can reincarnate.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool CanReincarnate(this Hero hero)
        {
            return hero.FindItem("item_aegis", true) != null
                   || hero.FindSpell("skeleton_king_reincarnation", true).CanBeCasted();
        }

        /// <summary>
        ///     Returns actual attack range of a hero
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public static float GetAttackRange(this Hero hero)
        {
            var bonus = 0.0;
            float range;
            if (rangeDictionary.TryGetValue(hero.Handle, out range)
                && !Utils.SleepCheck("Common.GetAttackRange." + hero.Handle))
            {
                return range;
            }

            var classId = hero.ClassID;
            switch (classId)
            {
                case ClassID.CDOTA_Unit_Hero_Tiny:
                    var grow = hero.Spellbook.SpellR;
                    if (grow != null && grow.Level > 0 && hero.HasItem(ClassID.CDOTA_Item_UltimateScepter))
                    {
                        bonus = grow.GetAbilityData("bonus_range_scepter");
                    }

                    break;
                case ClassID.CDOTA_Unit_Hero_TemplarAssassin:
                    var psi = hero.Spellbook.SpellE;
                    if (psi != null && psi.Level > 0)
                    {
                        bonus = psi.GetAbilityData("bonus_attack_range");
                    }

                    break;
                case ClassID.CDOTA_Unit_Hero_Sniper:
                    var aim = hero.Spellbook.SpellE;
                    if (aim != null && aim.Level > 0)
                    {
                        bonus = aim.GetAbilityData("bonus_attack_range");
                    }

                    break;
                case ClassID.CDOTA_Unit_Hero_Enchantress:
                    var impetus = hero.Spellbook.SpellR;
                    if (impetus.Level > 0 && hero.AghanimState())
                    {
                        bonus = 190;
                    }

                    break;
                default:
                    if (hero.HasModifier("modifier_lone_druid_true_form"))
                    {
                        bonus = -423;
                    }
                    else if (hero.HasModifier("modifier_dragon_knight_dragon_form"))
                    {
                        bonus = 350;
                    }
                    else if (hero.HasModifier("modifier_terrorblade_metamorphosis"))
                    {
                        bonus = 400;
                    }

                    break;
            }

            if (hero.IsRanged)
            {
                var dragonLance = hero.FindItem("item_dragon_lance", true) ?? hero.FindItem("item_hurricane_pike", true);
                if (dragonLance != null && dragonLance.IsValid)
                {
                    bonus += dragonLance.GetAbilityData("base_attack_range");
                }
            }

            // talents
            var talent = hero.Spellbook.Spells.FirstOrDefault(x => x.Name.StartsWith("special_bonus_attack_range_"));
            if (talent?.Level > 0)
            {
                bonus += talent.GetAbilityData("value");
            }

            range = (float)(hero.AttackRange + bonus + hero.HullRadius);
            if (!rangeDictionary.ContainsKey(hero.Handle))
            {
                rangeDictionary.Add(hero.Handle, range);
            }
            else
            {
                rangeDictionary[hero.Handle] = range;
            }

            Utils.Sleep(500, "Common.GetAttackRange." + hero.Handle);

            return range;
        }

        /// <summary>
        ///     Returns real name of the hero
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public static string GetRealName(this Hero hero)
        {
            var classId = hero.ClassID;
            switch (classId)
            {
                case ClassID.CDOTA_Unit_Hero_DoomBringer:
                    return "Doom";
                case ClassID.CDOTA_Unit_Hero_Furion:
                    return "Nature's Prophet";
                case ClassID.CDOTA_Unit_Hero_Magnataur:
                    return "Magnus";
                case ClassID.CDOTA_Unit_Hero_Necrolyte:
                    return "Necrophos";
                case ClassID.CDOTA_Unit_Hero_Nevermore:
                    return "ShadowFiend";
                case ClassID.CDOTA_Unit_Hero_Obsidian_Destroyer:
                    return "OutworldDevourer";
                case ClassID.CDOTA_Unit_Hero_Rattletrap:
                    return "Clockwerk";
                case ClassID.CDOTA_Unit_Hero_Shredder:
                    return "Timbersaw";
                case ClassID.CDOTA_Unit_Hero_SkeletonKing:
                    return "WraithKing";
                case ClassID.CDOTA_Unit_Hero_Wisp:
                    return "Io";
                case ClassID.CDOTA_Unit_Hero_Zuus:
                    return "Zeus";
            }

            return classId.ToString().Substring("CDOTA_Unit_Hero_".Length).Replace("_", string.Empty);
        }

        /// <summary>
        ///     The is illusion.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsIllusion(this Hero hero)
        {
            return hero.IsIllusion;
        }

        /// <summary>
        ///     The player.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="Player" />.
        /// </returns>
        public static Player Player(this Hero hero)
        {
            return
                Players.All.FirstOrDefault(
                    x => x != null && x.IsValid && x.Hero != null && x.Hero.IsValid && x.Hero.Equals(hero));
        }

        /// <summary>
        ///     The projectile speed.
        /// </summary>
        /// <param name="hero">
        ///     The hero.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double ProjectileSpeed(this Hero hero)
        {
            return UnitDatabase.GetProjectileSpeed(hero);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The initialize.
        /// </summary>
        internal static void Init()
        {
            rangeDictionary = new Dictionary<float, float>();
            boolDictionary = new Dictionary<string, bool>();
        }

        #endregion
    }
}