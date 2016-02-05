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