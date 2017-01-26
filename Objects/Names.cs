// <copyright file="Names.cs" company="EnsageSharp">
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
    using System.Collections.Concurrent;

    /// <summary>
    ///     The names.
    /// </summary>
    public static class Names
    {
        #region Static Fields

        /// <summary>
        ///     The name dictionary.
        /// </summary>
        private static ConcurrentDictionary<float, string> nameDictionary = new ConcurrentDictionary<float, string>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The stored name.
        /// </summary>
        /// <param name="entity">
        ///     The entity.
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public static string StoredName(this Entity entity)
        {
            if (entity == null || !entity.IsValid)
            {
                return string.Empty;
            }

            var handle = entity.Handle;
            string name;
            if (nameDictionary.TryGetValue(handle, out name))
            {
                return name;
            }

            name = entity.Name;
            nameDictionary.TryAdd(handle, name);
            return name;
        }

        /// <summary>
        ///     The stored name.
        /// </summary>
        /// <param name="entity">
        ///     The entity.
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public static string StoredName(this Ability entity)
        {
            if (entity == null || !entity.IsValid)
            {
                return string.Empty;
            }

            var handle = entity.Handle;
            string name;
            if (nameDictionary.TryGetValue(handle, out name))
            {
                return name;
            }

            name = entity.Name;
            nameDictionary.TryAdd(handle, name);
            return name;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The initialize.
        /// </summary>
        internal static void Init()
        {
            nameDictionary = new ConcurrentDictionary<float, string>();
        }

        #endregion
    }
}