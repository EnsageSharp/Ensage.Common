namespace Ensage.Common.Extensions
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using Ensage.Heroes;

    using global::SharpDX;

    /// <summary>
    ///     The meepo extensions.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", 
        Justification = "Reviewed. Meepo is OK here.")]
    public static class MeepoExtensions
    {
        #region Static Fields

        /// <summary>
        ///     The meepos.
        /// </summary>
        private static List<Meepo> meeposList = new List<Meepo>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The chain earthbind.
        /// </summary>
        /// <param name="meepos">
        ///     The meepos.
        /// </param>
        /// <param name="target">
        ///     The target.
        /// </param>
        public static void ChainEarthbind(this List<Meepo> meepos, Unit target)
        {
            var closestMeepo =
                meepos.Where(x => x.IsAlive && x.CanCast() && x.Spellbook.Spell1.CanBeCasted())
                    .MinOrDefault(x => x.Distance2D(target));
            if (closestMeepo == null)
            {
                return;
            }

            closestMeepo.Spellbook.Spell1.CastStun(target);
        }

        /// <summary>
        ///     The is illusion.
        /// </summary>
        /// <param name="meepo">
        ///     The meepo.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public static bool IsIllusion(this Meepo meepo)
        {
            return meepo.IsIllusion;
        }

        /// <summary>
        ///     The poof all.
        /// </summary>
        /// <param name="meepos">
        ///     The meepos.
        /// </param>
        /// <param name="position">
        ///     The position.
        /// </param>
        public static void PoofAll(this List<Meepo> meepos, Vector3 position)
        {
            foreach (var poof in
                meepos.Where(x => x.IsValid && x.IsAlive && x.CanCast())
                    .Select(otherMeepo => otherMeepo.Spellbook.Spell2)
                    .Where(poof => poof.CanBeCasted()))
            {
                poof.UseAbility(position);
            }
        }

        /// <summary>
        ///     Poofs all other meepos to this ones position.
        /// </summary>
        /// <param name="meepo">
        ///     The meepo.
        /// </param>
        public static void PoofAllToThisMeepo(this Meepo meepo)
        {
            if (meeposList.Count(x => x.IsValid) <= 0)
            {
                meeposList = ObjectManager.GetEntities<Meepo>().ToList();
            }

            foreach (var poof in
                meeposList.Where(x => x.IsValid && !x.Equals(meepo) && x.IsAlive && x.CanCast())
                    .Select(otherMeepo => otherMeepo.Spellbook.Spell2)
                    .Where(poof => poof.CanBeCasted()))
            {
                poof.UseAbility(meepo.Position);
            }
        }

        #endregion
    }
}