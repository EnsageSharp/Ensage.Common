namespace Ensage.Common.Menu.MenuItems
{
    using System.Collections.Generic;

    /// <summary>The ally heroes toggler.</summary>
    public class AllyHeroesToggler : MenuItem
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="AllyHeroesToggler" /> class.</summary>
        /// <param name="name">The name.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="heroDictionary">The hero dictionary.</param>
        /// <param name="defaultValues">The default values.</param>
        /// <param name="makeChampionUniq">The make champion unique.</param>
        public AllyHeroesToggler(
            string name, 
            string displayName, 
            Dictionary<string, bool> heroDictionary, 
            bool defaultValues = true, 
            bool makeChampionUniq = false)
            : base(name, displayName, makeChampionUniq)
        {
            this.SetValue(new HeroToggler(heroDictionary, useAllyHeroes: true, defaultValues: defaultValues));
        }

        #endregion
    }
}