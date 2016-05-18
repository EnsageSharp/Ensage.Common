namespace Ensage.Common.Menu.MenuItems
{
    /// <summary>The string switcher.</summary>
    public class StringSwitcher : MenuItem
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="StringSwitcher" /> class.</summary>
        /// <param name="name">The name.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="list">The list.</param>
        /// <param name="defaultSelectedIndex">The default selected index.</param>
        /// <param name="makeChampionUniq">The make champion unique.</param>
        public StringSwitcher(
            string name, 
            string displayName, 
            string[] list, 
            int defaultSelectedIndex = 0, 
            bool makeChampionUniq = false)
            : base(name, displayName, makeChampionUniq)
        {
            this.SetValue(new StringList(list, defaultSelectedIndex));
        }

        #endregion
    }
}