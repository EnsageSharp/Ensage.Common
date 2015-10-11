namespace Ensage.Common
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Ensage.Common.Properties;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    ///     The spell database.
    /// </summary>
    public static class SpellDatabase
    {
        #region Static Fields

        /// <summary>
        ///     The spells.
        /// </summary>
        public static List<SpellData> Spells;

        #endregion

        #region Constructors and Destructors

        static SpellDatabase()
        {
            Spells =
                JsonConvert.DeserializeObject<SpellData[]>(
                    JObject.Parse(Encoding.Default.GetString(Resources.SpellDatabase)).ToString()).ToList();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Find a spell.
        /// </summary>
        /// <param name="spellName">
        ///     The spell name.
        /// </param>
        /// <returns>
        ///     The <see cref="SpellData" />.
        /// </returns>
        public static SpellData Find(string spellName)
        {
            return Spells.FirstOrDefault(data => data.SpellName == spellName);
        }

        #endregion
    }
}