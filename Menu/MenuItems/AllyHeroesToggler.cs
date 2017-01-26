// <copyright file="AllyHeroesToggler.cs" company="EnsageSharp">
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
namespace Ensage.Common.Menu.MenuItems
{
    using System.Collections.Generic;

    /// <summary>The ally heroes toggler.</summary>
    public class AllyHeroesToggler : MenuItem
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="AllyHeroesToggler" /> class.</summary>
        /// <param name="name">The name.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="heroDictionary">The hero dictionary.</param>
        /// <param name="defaultValues">The default values.</param>
        /// <param name="makeChampionUniq">The make champion unique.</param>
        public AllyHeroesToggler(
            string name,
            string displayName,
            Dictionary<string, bool> heroDictionary,
            bool defaultValues = true,
            bool makeChampionUniq = false)
            : base(name, displayName, makeChampionUniq)
        {
            this.SetValue(new HeroToggler(heroDictionary, useAllyHeroes: true, defaultValues: defaultValues));
        }

        #endregion
    }
}