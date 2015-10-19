namespace Ensage.Common.AbilityInfo
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Ensage.Common.Properties;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    ///     The ability database.
    /// </summary>
    public static class AbilityDatabase
    {
        #region Static Fields

        /// <summary>
        ///     The abilities.
        /// </summary>
        public static List<AbilityInfo> Spells;

        #endregion

        #region Constructors and Destructors

        static AbilityDatabase()
        {
            JToken @object;
            if (JObject.Parse(Encoding.Default.GetString(Resources.AbilityDatabase))
                .TryGetValue("Abilities", out @object))
            {
                Spells = JsonConvert.DeserializeObject<AbilityInfo[]>(@object.ToString()).ToList();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Find a spell.
        /// </summary>
        /// <param name="abilityName"></param>
        /// <returns>
        ///     The <see cref="AbilityInfo" />.
        /// </returns>
        public static AbilityInfo Find(string abilityName)
        {
            return Spells.FirstOrDefault(data => data.AbilityName == abilityName);
        }

        #endregion
    }
}