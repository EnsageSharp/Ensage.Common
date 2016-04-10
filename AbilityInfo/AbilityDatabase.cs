// <copyright file="AbilityDatabase.cs" company="EnsageSharp">
//    Copyright (c) 2015 EnsageSharp.
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
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="AbilityDatabase" /> class.
        /// </summary>
        static AbilityDatabase()
        {
            JToken @object;
            if (JObject.Parse(Encoding.Default.GetString(Resources.AbilityDatabase))
                .TryGetValue("Abilities", out @object))
            {
                Spells = JsonConvert.DeserializeObject<AbilityInfo[]>(@object.ToString()).ToList();
            }

            AbilityinfoDictionary = new Dictionary<string, AbilityInfo>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the ability info dictionary.
        /// </summary>
        public static Dictionary<string, AbilityInfo> AbilityinfoDictionary { get; private set; }

        /// <summary>
        ///     Gets the spells.
        /// </summary>
        public static List<AbilityInfo> Spells { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Find a spell.
        /// </summary>
        /// <param name="abilityName">
        ///     The ability Name.
        /// </param>
        /// <returns>
        ///     The <see cref="AbilityInfo" />.
        /// </returns>
        public static AbilityInfo Find(string abilityName)
        {
            AbilityInfo info;
            if (AbilityinfoDictionary.TryGetValue(abilityName, out info))
            {
                return info;
            }

            info = Spells.FirstOrDefault(data => data.AbilityName == abilityName);
            if (info != null)
            {
                AbilityinfoDictionary.Add(abilityName, info);
            }

            return info;
        }

        #endregion
    }
}