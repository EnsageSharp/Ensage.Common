namespace Ensage.Common
{
    public class SpellData
    {
        #region Fields

        public double AdditionalDelay;

        public bool AllyBlock;

        public bool EnemyBlock;

        public bool FakeCastRange;

        public bool IsDisable;

        public bool IsHeal;

        public bool IsNuke;

        public bool IsShield;

        public bool IsSkillShot;

        public bool IsSlow;

        public bool MagicImmunityPierce;

        public bool OnlyForKillSteal;

        public float Radius;

        public string RealCastRange;

        public string Speed;

        public string SpellName;

        public string StringRadius;

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