namespace Ensage.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class SpellData
    {
        #region Fields

        /// <summary>
        /// Spell have additional delay after being casted
        /// </summary>
        public double AdditionalDelay;

        /// <summary>
        /// Spell is blocked by ally units in a way
        /// </summary>
        public bool AllyBlock;

        /// <summary>
        /// Spell is blocked by enemy units in a way
        /// </summary>
        public bool EnemyBlock;

        /// <summary>
        /// 
        /// </summary>
        public bool FakeCastRange;

        /// <summary>
        /// Spell disables the target
        /// </summary>
        public bool IsDisable;

        /// <summary>
        /// 
        /// </summary>
        public bool IsHeal;

        /// <summary>
        /// Spell has low castpoint and high damage input
        /// </summary>
        public bool IsNuke;

        /// <summary>
        /// 
        /// </summary>
        public bool IsShield;

        /// <summary>
        /// Spell needs prediction
        /// </summary>
        public bool IsSkillShot;

        /// <summary>
        /// Spell applies movement slow to the target
        /// </summary>
        public bool IsSlow;

        /// <summary>
        /// Spell goes through magic immunity
        /// </summary>
        public bool MagicImmunityPierce;

        /// <summary>
        /// Spell is used only to killsteal
        /// </summary>
        public bool OnlyForKillSteal;

        /// <summary>
        /// Radius of non targeted/aoe spell
        /// </summary>
        public float Radius;

        /// <summary>
        /// 
        /// </summary>
        public string RealCastRange;

        /// <summary>
        /// Speed of a projectile
        /// </summary>
        public string Speed;

        /// <summary>
        /// Name of a spell
        /// </summary>
        public string SpellName;

        /// <summary>
        /// Radius data name
        /// </summary>
        public string StringRadius;

        /// <summary>
        /// Width of a projectile
        /// </summary>
        public string Width;

        #endregion

        #region Constructors and Destructors

        public SpellData()
        {
        }

        public SpellData(
            string spellName,
            bool isDisable,
            bool isSlow,
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
            bool onlyForKillSteal)
        {
            this.SpellName = spellName;
            this.IsDisable = isDisable;
            this.IsSlow = isSlow;
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
        }

        #endregion
    }
}