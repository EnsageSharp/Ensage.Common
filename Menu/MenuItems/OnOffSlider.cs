namespace Ensage.Common.Menu.MenuItems
{
    /// <summary>The on off slider.</summary>
    public class OnOffSlider : MenuItem
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="OnOffSlider" /> class.</summary>
        /// <param name="name">The name.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="value">The value.</param>
        /// <param name="makeChampionUniq">The make champion unique.</param>
        public OnOffSlider(string name, string displayName, bool value, bool makeChampionUniq = false)
            : base(name, displayName, makeChampionUniq)
        {
            this.SetValue(value);
        }

        #endregion
    }
}