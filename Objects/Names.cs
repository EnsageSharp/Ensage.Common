namespace Ensage.Common.Objects
{
    using System.Collections.Generic;

    /// <summary>
    ///     The names.
    /// </summary>
    internal static class Names
    {
        #region Static Fields

        /// <summary>
        ///     The name dictionary.
        /// </summary>
        public static Dictionary<float, string> NameDictionary = new Dictionary<float, string>();

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
            var handle = entity.Handle;
            string name;
            if (NameDictionary.TryGetValue(handle, out name))
            {
                return name;
            }

            name = entity.Name;
            NameDictionary.Add(handle, name);
            return name;
        }

        #endregion
    }
}