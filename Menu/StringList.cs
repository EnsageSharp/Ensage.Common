namespace Ensage.Common.Menu
{
    using System;

    /// <summary>
    ///     The string list.
    /// </summary>
    [Serializable]
    public struct StringList
    {
        #region Fields

        /// <summary>
        ///     The selected index.
        /// </summary>
        public int SelectedIndex;

        /// <summary>
        ///     The s list.
        /// </summary>
        public string[] SList;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="StringList" /> struct.
        /// </summary>
        /// <param name="list">
        ///     The list.
        /// </param>
        /// <param name="defaultSelectedIndex">
        ///     The default selected index.
        /// </param>
        public StringList(string[] list, int defaultSelectedIndex = 0)
        {
            this.SList = list;
            this.SelectedIndex = defaultSelectedIndex;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the selected value.
        /// </summary>
        public string SelectedValue
        {
            get
            {
                return this.SList[this.SelectedIndex];
            }
        }

        #endregion
    }
}