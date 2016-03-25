namespace Ensage.Common.Menu
{
    /// <summary>
    ///     The common menu.
    /// </summary>
    internal class CommonMenu
    {
        #region Static Fields

        /// <summary>
        ///     The menu config.
        /// </summary>
        public static readonly Menu MenuConfig = new Menu("Ensage.Common", "Ensage.Common", true);

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="CommonMenu" /> class.
        /// </summary>
        static CommonMenu()
        {
            MenuConfig.AddToMainMenu();
        }

        #endregion
    }
}