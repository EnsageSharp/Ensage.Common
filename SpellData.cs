namespace Ensage.Common
{

    public class SpellData
    {
        public string SpellName;
        public bool IsDisable;
        public bool IsSlow;
        public bool IsNuke;
        public bool IsSkillShot;
        public bool IsHeal;
        public bool IsShield;
        public double AdditionalDelay;
        public float Radius;
        public string StringRadius;
        public string Speed;
        public string Width;
        public bool AllyBlock;
        public bool EnemyBlock;
        public bool MagicImmunityPierce;
        public bool FakeCastRange;
        public string RealCastRange;
        public bool OnlyForKillSteal;


        public SpellData() { }

        public SpellData(string spellName,
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
            SpellName = spellName;
            IsDisable = isDisable;
            IsSlow = isSlow;
            IsNuke = isNuke;
            IsSkillShot = isSkillShot;
            IsHeal = isHeal;
            IsShield = isShield;
            AdditionalDelay = additionalDelay;
            Radius = radius;
            StringRadius = stringRadius;
            Speed = speed;
            Width = width;
            AllyBlock = allyBlock;
            EnemyBlock = enemyBlock;
            MagicImmunityPierce = magicImmunityPierce;
            FakeCastRange = fakeCastRange;
            RealCastRange = realCastRange;
            OnlyForKillSteal = onlyForKillSteal;
        }
    }
}
