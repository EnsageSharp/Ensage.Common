namespace Ensage.Common.Menu
{
    using System.Collections.Generic;

    /// <summary>
    ///     The menu globals.
    /// </summary>
    public static class MenuGlobals
    {
        #region Static Fields

        /// <summary>
        ///     The menu state.
        /// </summary>
        private static List<string> menuState = new List<string>();

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether draw menu.
        /// </summary>
        public static bool DrawMenu { get; set; }

        /// <summary>
        ///     Gets or sets the menu state.
        /// </summary>
        public static List<string> MenuState
        {
            get
            {
                return menuState;
            }

            set
            {
                menuState = value;
            }
        }

        #endregion
    }
}