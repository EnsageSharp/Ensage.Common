// <copyright file="AbilityDatabase.cs" company="EnsageSharp">
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
namespace Ensage.Common.AbilityInfo
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Permissions;
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
        ///     Gets the ability info dictionary.
        /// </summary>
        private static ConcurrentDictionary<string, AbilityInfo> abilityinfoDictionary;

        /// <summary>
        ///     The loaded.
        /// </summary>
        private static bool loaded;

        /// <summary>
        ///     Gets the spells.
        /// </summary>
        private static List<AbilityInfo> spells;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="AbilityDatabase" /> class.
        /// </summary>
        static AbilityDatabase()
        {
            Init();
        }

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
            if (abilityinfoDictionary.TryGetValue(abilityName, out info))
            {
                return info;
            }

            info = spells.FirstOrDefault(data => data.AbilityName == abilityName);
            abilityinfoDictionary.TryAdd(abilityName, info);

            return info;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The initialization.
        /// </summary>
        [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
        internal static void Init()
        {
            if (loaded)
            {
                return;
            }

            loaded = true;
            JToken @object;
            if (JObject.Parse(Encoding.Default.GetString(Resources.AbilityDatabase))
                .TryGetValue("Abilities", out @object))
            {
                spells = JsonConvert.DeserializeObject<AbilityInfo[]>(@object.ToString()).ToList();
            }

            abilityinfoDictionary = new ConcurrentDictionary<string, AbilityInfo>();
        }

        #endregion
    }
}