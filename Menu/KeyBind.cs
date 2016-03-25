namespace Ensage.Common.Menu
{
    using System;

    /// <summary>
    ///     The key bind.
    /// </summary>
    [Serializable]
    public struct KeyBind
    {
        #region Fields

        /// <summary>
        ///     The active.
        /// </summary>
        public bool Active;

        /// <summary>
        ///     The key.
        /// </summary>
        public uint Key;

        /// <summary>
        ///     The type.
        /// </summary>
        public KeyBindType Type;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="KeyBind" /> struct.
        /// </summary>
        /// <param name="key">
        ///     The key.
        /// </param>
        /// <param name="type">
        ///     The type.
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        public KeyBind(uint key, KeyBindType type, bool defaultValue = false)
        {
            this.Key = key;
            this.Active = defaultValue;
            this.Type = type;
        }

        #endregion
    }
}