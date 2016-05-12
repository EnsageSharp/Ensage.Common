namespace Ensage.Common.Objects
{
    using System.Collections.Generic;

    /// <summary>
    ///     The names.
    /// </summary>
    public static class Names
    {
        #region Static Fields

        /// <summary>
        ///     The name dictionary.
        /// </summary>
        private static Dictionary<float, string> nameDictionary = new Dictionary<float, string>();

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
            nameDictionary.Add(handle, name);
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
            nameDictionary.Add(handle, name);
            return name;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The initialize.
        /// </summary>
        internal static void Init()
        {
            nameDictionary = new Dictionary<float, string>();
        }

        #endregion
    }
}