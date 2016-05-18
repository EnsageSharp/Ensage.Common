namespace Ensage.Common.Menu.MenuItems
{
    using System.Collections.Generic;

    /// <summary>The enemy heroes toggler.</summary>
    public class EnemyHeroesToggler : MenuItem
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="EnemyHeroesToggler" /> class.</summary>
        /// <param name="name">The name.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="heroDictionary">The hero dictionary.</param>
        /// <param name="defaultValues">The default values.</param>
        /// <param name="makeChampionUniq">The make champion unique.</param>
        public EnemyHeroesToggler(
            string name, 
            string displayName, 
            Dictionary<string, bool> heroDictionary, 
            bool defaultValues = true, 
            bool makeChampionUniq = false)
            : base(name, displayName, makeChampionUniq)
        {
            this.SetValue(new HeroToggler(heroDictionary, true, defaultValues: defaultValues));
        }

        #endregion
    }
}