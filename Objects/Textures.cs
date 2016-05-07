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