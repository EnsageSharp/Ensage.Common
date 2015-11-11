namespace Ensage.Common.AbilityInfo
{
    /// <summary>
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

        public AbilityInfo()
        {
        }

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
            bool trueSight)
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
        }

        #endregion
    }
}