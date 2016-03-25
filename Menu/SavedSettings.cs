namespace Ensage.Common.Menu
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    ///     The saved settings.
    /// </summary>
    [Serializable]
    internal static class SavedSettings
    {
        #region Static Fields

        /// <summary>
        ///     The loaded files.
        /// </summary>
        private static readonly Dictionary<string, Dictionary<string, byte[]>> LoadedFiles =
            new Dictionary<string, Dictionary<string, byte[]>>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The get saved data.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <param name="key">
        ///     The key.
        /// </param>
        /// <returns>
        ///     The <see cref="byte[]" />.
        /// </returns>
        public static byte[] GetSavedData(string name, string key)
        {
            Dictionary<string, byte[]> dic = null;

            dic = LoadedFiles.ContainsKey(name) ? LoadedFiles[name] : Load(name);

            if (dic == null)
            {
                return null;
            }

            return dic.ContainsKey(key) ? dic[key] : null;
        }

        /// <summary>
        ///     The load.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="Dictionary" />.
        /// </returns>
        public static Dictionary<string, byte[]> Load(string name)
        {
            try
            {
                var fileName = Path.Combine(MenuSettings.MenuMenuConfigPath, name + ".bin");
                if (File.Exists(fileName))
                {
                    return Utils.Deserialize<Dictionary<string, byte[]>>(File.ReadAllBytes(fileName));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        /// <summary>
        ///     The save.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <param name="entries">
        ///     The entries.
        /// </param>
        public static void Save(string name, Dictionary<string, byte[]> entries)
        {
            try
            {
                Directory.CreateDirectory(MenuSettings.MenuMenuConfigPath);
                var fileName = Path.Combine(MenuSettings.MenuMenuConfigPath, name + ".bin");
                File.WriteAllBytes(fileName, Utils.Serialize(entries));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion
    }
}