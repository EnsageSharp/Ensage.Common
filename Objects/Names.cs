namespace Ensage.Common.Objects
{
    using System.Collections.Generic;

    internal static class Names
    {
        #region Static Fields

        public static Dictionary<float, string> NameDictionary = new Dictionary<float, string>();

        #endregion

        #region Public Methods and Operators

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