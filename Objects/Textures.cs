// <copyright file="Textures.cs" company="EnsageSharp">
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
namespace Ensage.Common.Objects
{
    using System;
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
        ///     The get hero round texture.
        /// </summary>
        /// <param name="heroName">
        ///     The hero name.
        /// </param>
        /// <returns>
        ///     The <see cref="DotaTexture" />.
        /// </returns>
        public static DotaTexture GetHeroRoundTexture(string heroName)
        {
            var name = "materials/ensage_ui/heroes_round/" + heroName.Substring("npc_dota_hero_".Length) + ".vmat";
            DotaTexture texture;
            if (TextureDictionary.TryGetValue(name, out texture))
            {
                return texture;
            }

            texture = FindTexture(name);
            TextureDictionary.Add(name, texture);
            return texture;
        }

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

            texture = FindTexture(name);
            TextureDictionary.Add(name, texture);
            return texture;
        }

        /// <summary>
        ///     The get hero vertical texture.
        /// </summary>
        /// <param name="heroName">
        ///     The hero name.
        /// </param>
        /// <returns>
        ///     The <see cref="DotaTexture" />.
        /// </returns>
        public static DotaTexture GetHeroVerticalTexture(string heroName)
        {
            var name = "materials/ensage_ui/heroes_vertical/" + heroName.Substring("npc_dota_hero_".Length) + ".vmat";
            DotaTexture texture;
            if (TextureDictionary.TryGetValue(name, out texture))
            {
                return texture;
            }

            texture = FindTexture(name);
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

            texture = FindTexture(name);
            TextureDictionary.Add(name, texture);
            return texture;
        }

        /// <summary>
        ///     The get neutral creep texture.
        /// </summary>
        /// <param name="creepName">
        ///     The creep name.
        /// </param>
        /// <returns>
        ///     The <see cref="DotaTexture" />.
        /// </returns>
        public static DotaTexture GetNeutralCreepTexture(string creepName)
        {
            var name = "materials/ensage_ui/neutrals_vertical/" + creepName.Substring("npc_dota_neutral_".Length)
                       + ".vmat";
            DotaTexture texture;
            if (TextureDictionary.TryGetValue(name, out texture))
            {
                return texture;
            }

            texture = FindTexture(name);
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

            texture = FindTexture(name);
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

            texture = FindTexture(name);
            TextureDictionary.Add(name, texture);
            return texture;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The find texture.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="DotaTexture" />.
        /// </returns>
        private static DotaTexture FindTexture(string name)
        {
            DotaTexture texture;

            try
            {
                texture = Drawing.GetTexture(name);
            }
            catch (DotaTextureNotFoundException e)
            {
                // First exception occurs if caller is trying to load non-existing texture, in that case try to replace it with blank texture
                try
                {
                    Game.PrintMessage(
                        "<font color='#dddddd'>[Ensage]: Texture '" + e.TextureName + "' was not found</font>",
                        MessageType.LogMessage);
                    Console.WriteLine(@"Texture '" + e.TextureName + @"' was not found");
                    texture = Drawing.GetTexture("materials/ensage_ui/spellicons/doom_bringer_empty1");
                }
                catch (DotaTextureNotFoundException)
                {
                    // Second exception occurs in case user doesnt have texture pack installed, notify the user and replace it with internal texture
                    Game.PrintMessage(
                        "<font color='#dd3333'>!!!!!!! Texture Pack not found !!!!!!!!</font>",
                        MessageType.LogMessage);
                    Game.PrintMessage(
                        "<font color='#dddddd'>Get texture pack @ Forum->Community->Developer's Talk->EnsageSharp</font>",
                        MessageType.LogMessage);
                    Game.PrintMessage(
                        "<font color='#dd3333'>!!!!!!! Texture Pack not found !!!!!!!!</font>",
                        MessageType.ChatMessage);
                    Game.PrintMessage(
                        "<font color='#dddddd'>Get texture pack @ Forum->Community->Developer's Talk->EnsageSharp</font>",
                        MessageType.ChatMessage);

                    Console.WriteLine(@"!!!!!!! Texture Pack not found !!!!!!!!");
                    Console.WriteLine(@"Get texture pack @ Forum->Community->Developer's Talk->EnsageSharp");

                    texture = Drawing.GetTexture("materials/console_background.vmat_c");
                }
            }

            return texture;
        }

        #endregion
    }
}