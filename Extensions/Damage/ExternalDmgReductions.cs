// <copyright file="ExternalDmgReductions.cs" company="EnsageSharp">
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
namespace Ensage.Common.Extensions.Damage
{
    /// <summary>
    ///     The external damage reductions.
    /// </summary>
    internal class ExternalDmgReductions
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExternalDmgReductions" /> class.
        /// </summary>
        public ExternalDmgReductions()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExternalDmgReductions" /> class.
        /// </summary>
        /// <param name="modifierName">
        ///     The modifier name.
        /// </param>
        /// <param name="sourceTeam">
        ///     The source team.
        /// </param>
        /// <param name="reduce">
        ///     The reduce.
        /// </param>
        /// <param name="sourceSpellName">
        ///     The source spell name.
        /// </param>
        /// <param name="heroId">
        ///     The hero id.
        /// </param>
        /// <param name="type">
        ///     The type.
        /// </param>
        /// <param name="magicOnly">
        ///     The magic only.
        /// </param>
        public ExternalDmgReductions(
            string modifierName,
            double sourceTeam,
            string reduce,
            string sourceSpellName,
            ClassId heroId,
            float type,
            bool magicOnly)
        {
            this.ModifierName = modifierName;
            this.SourceTeam = sourceTeam;
            this.Reduce = reduce;
            this.SourceSpellName = sourceSpellName;
            this.HeroID = heroId;
            this.Type = type;
            this.MagicOnly = magicOnly;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the hero id.
        /// </summary>
        public ClassId HeroID { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether magic only.
        /// </summary>
        public bool MagicOnly { get; set; }

        /// <summary>
        ///     Gets or sets the modifier name.
        /// </summary>
        public string ModifierName { get; set; }

        /// <summary>
        ///     Gets or sets the reduce.
        /// </summary>
        public string Reduce { get; set; }

        /// <summary>
        ///     Gets or sets the source spell name.
        /// </summary>
        public string SourceSpellName { get; set; }

        /// <summary>
        ///     Gets or sets the source team.
        /// </summary>
        public double SourceTeam { get; set; }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        public float Type { get; set; }

        #endregion
    }
}