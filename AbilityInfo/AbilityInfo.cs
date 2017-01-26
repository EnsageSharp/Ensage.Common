// <copyright file="AbilityInfo.cs" company="EnsageSharp">
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
namespace Ensage.Common.AbilityInfo
{
    /// <summary>
    ///     The ability info.
    /// </summary>
    public class AbilityInfo
    {
        #region Fields

        /// <summary>
        ///     True if ability damage is dependent on auto attack damage
        /// </summary>
        public bool AADamage;

        /// <summary>
        ///     Name of a ability
        /// </summary>
        public string AbilityName;

        /// <summary>
        ///     Spell have additional delay after being casted
        /// </summary>
        public double AdditionalDelay;

        /// <summary>
        ///     Spell is blocked by ally units in a way
        /// </summary>
        public bool AllyBlock;

        /// <summary>
        ///     Bonus damage data
        /// </summary>
        public string BonusDamageString;

        /// <summary>
        ///     Damage Multiplier
        /// </summary>
        public float DamageMultiplier;

        /// <summary>
        ///     Damage data name when holding scepter
        /// </summary>
        public string DamageScepterString;

        /// <summary>
        ///     Damage data name
        /// </summary>
        public string DamageString;

        /// <summary>
        ///     The distance.
        /// </summary>
        public string Distance;

        /// <summary>
        ///     Gets the end width.
        /// </summary>
        public string EndWidth;

        /// <summary>
        ///     Spell is blocked by enemy units in a way
        /// </summary>
        public bool EnemyBlock;

        /// <summary>
        /// </summary>
        public bool FakeCastRange;

        /// <summary>
        ///     Ability buffs unit
        /// </summary>
        public bool IsBuff;

        /// <summary>
        ///     Spell disables the target
        /// </summary>
        public bool IsDisable;

        /// <summary>
        ///     Ability is good for harrasing
        /// </summary>
        public bool IsHarras;

        /// <summary>
        /// </summary>
        public bool IsHeal;

        /// <summary>
        ///     Spell grants invisibility
        /// </summary>
        public bool IsInvis;

        /// <summary>
        ///     Spell has low castpoint and high damage input
        /// </summary>
        public bool IsNuke;

        /// <summary>
        ///     Ability purges unit
        /// </summary>
        public bool IsPurge;

        /// <summary>
        ///     Spell is applied on an ally in order to prevent them from taking damage or reduce taken damage(Increasing armor,
        ///     magic resistance etc).
        /// </summary>
        public bool IsShield;

        /// <summary>
        ///     Spell silences the target
        /// </summary>
        public bool IsSilence;

        /// <summary>
        ///     Spell needs prediction
        /// </summary>
        public bool IsSkillShot;

        /// <summary>
        ///     Spell applies movement slow to the target
        /// </summary>
        public bool IsSlow;

        /// <summary>
        ///     Spell goes through magic immunity
        /// </summary>
        public bool MagicImmunityPierce;

        /// <summary>
        ///     True if ability deals damage depending on target's mana
        /// </summary>
        public bool ManaBurn;

        /// <summary>
        ///     Spell is used only to killsteal
        /// </summary>
        public bool OnlyForKillSteal;

        /// <summary>
        ///     Radius of non targeted/aoe spell
        /// </summary>
        public float Radius;

        /// <summary>
        /// </summary>
        public string RealCastRange;

        /// <summary>
        ///     Speed of a projectile
        /// </summary>
        public string Speed;

        /// <summary>
        ///     Name of spell which affects the damage depending on its level
        /// </summary>
        public string SpellLevel;

        /// <summary>
        ///     Radius data name
        /// </summary>
        public string StringRadius;

        /// <summary>
        ///     Ability provides true sight
        /// </summary>
        public bool TrueSight;

        /// <summary>
        ///     Ability weakens enemy
        /// </summary>
        public bool WeakensEnemy;

        /// <summary>
        ///     Width of a projectile
        /// </summary>
        public string Width;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AbilityInfo" /> class.
        /// </summary>
        public AbilityInfo()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AbilityInfo" /> class.
        /// </summary>
        /// <param name="abilityName">
        ///     The ability name.
        /// </param>
        /// <param name="isDisable">
        ///     The is disable.
        /// </param>
        /// <param name="isSlow">
        ///     The is slow.
        /// </param>
        /// <param name="isSilence">
        ///     The is silence.
        /// </param>
        /// <param name="isNuke">
        ///     The is nuke.
        /// </param>
        /// <param name="isSkillShot">
        ///     The is skill shot.
        /// </param>
        /// <param name="isHeal">
        ///     The is heal.
        /// </param>
        /// <param name="isShield">
        ///     The is shield.
        /// </param>
        /// <param name="additionalDelay">
        ///     The additional delay.
        /// </param>
        /// <param name="radius">
        ///     The radius.
        /// </param>
        /// <param name="stringRadius">
        ///     The string radius.
        /// </param>
        /// <param name="speed">
        ///     The speed.
        /// </param>
        /// <param name="width">
        ///     The width.
        /// </param>
        /// <param name="allyBlock">
        ///     The ally block.
        /// </param>
        /// <param name="enemyBlock">
        ///     The enemy block.
        /// </param>
        /// <param name="magicImmunityPierce">
        ///     The magic immunity pierce.
        /// </param>
        /// <param name="fakeCastRange">
        ///     The fake cast range.
        /// </param>
        /// <param name="realCastRange">
        ///     The real cast range.
        /// </param>
        /// <param name="onlyForKillSteal">
        ///     The only for kill steal.
        /// </param>
        /// <param name="damageString">
        ///     The damage string.
        /// </param>
        /// <param name="damageMultiplier">
        ///     The damage multiplier.
        /// </param>
        /// <param name="bonusDamagestring">
        ///     The bonus damagestring.
        /// </param>
        /// <param name="aaDamage">
        ///     The aa damage.
        /// </param>
        /// <param name="damageScepterString">
        ///     The damage scepter string.
        /// </param>
        /// <param name="spellLevel">
        ///     The spell level.
        /// </param>
        /// <param name="manaBurn">
        ///     The mana burn.
        /// </param>
        /// <param name="isBuff">
        ///     The is buff.
        /// </param>
        /// <param name="weakensEnemy">
        ///     The weakens enemy.
        /// </param>
        /// <param name="isPurge">
        ///     The is purge.
        /// </param>
        /// <param name="isHarras">
        ///     The is harras.
        /// </param>
        /// <param name="trueSight">
        ///     The true sight.
        /// </param>
        /// <param name="isInvis">
        ///     The is invisibility.
        /// </param>
        /// <param name="endWidth">
        ///     The end Width.
        /// </param>
        /// <param name="distance">
        ///     The distance.
        /// </param>
        public AbilityInfo(
            string abilityName,
            bool isDisable,
            bool isSlow,
            bool isSilence,
            bool isNuke,
            bool isSkillShot,
            bool isHeal,
            bool isShield,
            double additionalDelay,
            float radius,
            string stringRadius,
            string speed,
            string width,
            bool allyBlock,
            bool enemyBlock,
            bool magicImmunityPierce,
            bool fakeCastRange,
            string realCastRange,
            bool onlyForKillSteal,
            string damageString,
            float damageMultiplier,
            string bonusDamagestring,
            bool aaDamage,
            string damageScepterString,
            string spellLevel,
            bool manaBurn,
            bool isBuff,
            bool weakensEnemy,
            bool isPurge,
            bool isHarras,
            bool trueSight,
            bool isInvis,
            string endWidth,
            string distance)
        {
            this.ManaBurn = manaBurn;
            this.SpellLevel = spellLevel;
            this.AADamage = aaDamage;
            this.DamageScepterString = damageScepterString;
            this.AbilityName = abilityName;
            this.IsDisable = isDisable;
            this.IsSlow = isSlow;
            this.IsSilence = isSilence;
            this.IsNuke = isNuke;
            this.IsSkillShot = isSkillShot;
            this.IsHeal = isHeal;
            this.IsShield = isShield;
            this.AdditionalDelay = additionalDelay;
            this.Radius = radius;
            this.StringRadius = stringRadius;
            this.Speed = speed;
            this.Width = width;
            this.AllyBlock = allyBlock;
            this.EnemyBlock = enemyBlock;
            this.MagicImmunityPierce = magicImmunityPierce;
            this.FakeCastRange = fakeCastRange;
            this.RealCastRange = realCastRange;
            this.OnlyForKillSteal = onlyForKillSteal;
            this.DamageString = damageString;
            this.DamageMultiplier = damageMultiplier;
            this.BonusDamageString = bonusDamagestring;
            this.IsBuff = isBuff;
            this.WeakensEnemy = weakensEnemy;
            this.IsPurge = isPurge;
            this.IsHarras = isHarras;
            this.TrueSight = trueSight;
            this.IsInvis = isInvis;
            this.EndWidth = endWidth;
            this.Distance = distance;
        }

        #endregion
    }
}