// <copyright file="DamageBlocks.cs" company="EnsageSharp">
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
    ///     The damage blocks.
    /// </summary>
    internal class DamageBlocks
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DamageBlocks" /> class.
        /// </summary>
        /// <param name="modifierName">
        ///     The modifier name.
        /// </param>
        /// <param name="meleeBlock">
        ///     The melee block.
        /// </param>
        /// <param name="rangedBlock">
        ///     The ranged block.
        /// </param>
        /// <param name="abilityName">
        ///     The ability name.
        /// </param>
        /// <param name="item">
        ///     The item.
        /// </param>
        public DamageBlocks(string modifierName, string meleeBlock, string rangedBlock, string abilityName, bool item)
        {
            this.ModifierName = modifierName;
            this.MeleeBlock = meleeBlock;
            this.RangedBlock = rangedBlock;
            this.AbilityName = abilityName;
            this.Item = item;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the ability name.
        /// </summary>
        public string AbilityName { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether item.
        /// </summary>
        public bool Item { get; set; }

        /// <summary>
        ///     Gets or sets the melee block.
        /// </summary>
        public string MeleeBlock { get; set; }

        /// <summary>
        ///     Gets or sets the modifier name.
        /// </summary>
        public string ModifierName { get; set; }

        /// <summary>
        ///     Gets or sets the ranged block.
        /// </summary>
        public string RangedBlock { get; set; }

        #endregion
    }
}