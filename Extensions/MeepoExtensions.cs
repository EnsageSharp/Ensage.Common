// <copyright file="MeepoExtensions.cs" company="EnsageSharp">
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
            var meepoCount = 1 + meepo.Spellbook.SpellR.Level + (meepo.AghanimState() ? 1 : 0);
            if (meeposList.Count(x => x.IsValid) <= meepoCount)
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