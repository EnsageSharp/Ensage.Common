// <copyright file="Textures.cs" company="EnsageSharp">
//    Copyright (c) 2016 EnsageSharp.
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
namespace Ensage.Common.Objects
{
    using System.Collections.Generic;

    /// <summary>
    ///     The textures.
    /// </summary>
    public class Textures
    {
        #region Static Fields

        /// <summary>
        ///     The texture dictionary.
        /// </summary>
        private static readonly Dictionary<string, DotaTexture> TextureDictionary =
            new Dictionary<string, DotaTexture>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The get hero texture.
        /// </summary>
        /// <param name="heroName">
        ///     The hero name.
        /// </param>
        /// <returns>
        ///     The <see cref="DotaTexture" />.
        /// </returns>
        public static DotaTexture GetHeroTexture(string heroName)
        {
            var name = "materials/ensage_ui/heroes_horizontal/" + heroName.Substring("npc_dota_hero_".Length) + ".vmat";
            DotaTexture texture;
            if (TextureDictionary.TryGetValue(name, out texture))
            {
                return texture;
            }

            texture = Drawing.GetTexture(name);
            TextureDictionary.Add(name, texture);

            return texture;
        }

        /// <summary>
        ///     The get item texture.
        /// </summary>
        /// <param name="itemName">
        ///     The item name.
        /// </param>
        /// <returns>
        ///     The <see cref="DotaTexture" />.
        /// </returns>
        public static DotaTexture GetItemTexture(string itemName)
        {
            var name = "materials/ensage_ui/items/" + itemName.Substring("item_".Length) + ".vmat";
            DotaTexture texture;
            if (TextureDictionary.TryGetValue(name, out texture))
            {
                return texture;
            }

            texture = Drawing.GetTexture(name);
            TextureDictionary.Add(name, texture);

            return texture;
        }

        /// <summary>
        ///     The get spell texture.
        /// </summary>
        /// <param name="spellName">
        ///     The spell name.
        /// </param>
        /// <returns>
        ///     The <see cref="DotaTexture" />.
        /// </returns>
        public static DotaTexture GetSpellTexture(string spellName)
        {
            var name = "materials/ensage_ui/spellicons/" + spellName + ".vmat";
            DotaTexture texture;
            if (TextureDictionary.TryGetValue(name, out texture))
            {
                return texture;
            }

            texture = Drawing.GetTexture(name);
            TextureDictionary.Add(name, texture);

            return texture;
        }

        /// <summary>
        ///     The get texture.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="DotaTexture" />.
        /// </returns>
        public static DotaTexture GetTexture(string name)
        {
            DotaTexture texture;
            if (TextureDictionary.TryGetValue(name, out texture))
            {
                return texture;
            }

            texture = Drawing.GetTexture(name);
            TextureDictionary.Add(name, texture);

            return texture;
        }

        #endregion
    }
}