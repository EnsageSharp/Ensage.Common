namespace Ensage.Common.Menu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     The hero toggler.
    /// </summary>
    [Serializable]
    public struct HeroToggler
    {
        #region Fields

        /// <summary>
        ///     The default values.
        /// </summary>
        public bool DefaultValues;

        /// <summary>
        ///     The dictionary.
        /// </summary>
        public Dictionary<string, bool> Dictionary;

        /// <summary>
        ///     The position dictionary.
        /// </summary>
        public Dictionary<string, float[]> PositionDictionary;

        /// <summary>
        ///     The s values dictionary.
        /// </summary>
        public Dictionary<string, bool> SValuesDictionary;

        /// <summary>
        ///     The use ally heroes.
        /// </summary>
        public bool UseAllyHeroes;

        /// <summary>
        ///     The use enemy heroes.
        /// </summary>
        public bool UseEnemyHeroes;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HeroToggler" /> struct.
        /// </summary>
        /// <param name="heroDictionary">
        ///     The hero dictionary.
        /// </param>
        /// <param name="useEnemyHeroes">
        ///     The use enemy heroes.
        /// </param>
        /// <param name="useAllyHeroes">
        ///     The use ally heroes.
        /// </param>
        /// <param name="defaultValues">
        ///     The default values.
        /// </param>
        public HeroToggler(
            Dictionary<string, bool> heroDictionary, 
            bool useEnemyHeroes = false, 
            bool useAllyHeroes = false, 
            bool defaultValues = true)
        {
            this.Dictionary = heroDictionary;
            this.PositionDictionary = new Dictionary<string, float[]>();
            this.UseEnemyHeroes = useEnemyHeroes;
            this.UseAllyHeroes = useAllyHeroes;
            this.SValuesDictionary = new Dictionary<string, bool>();
            this.DefaultValues = defaultValues;
            foreach (var v in this.Dictionary.Where(v => !Menu.TextureDictionary.ContainsKey(v.Key)))
            {
                Menu.TextureDictionary.Add(
                    v.Key, 
                    Drawing.GetTexture(
                        "materials/ensage_ui/heroes_horizontal/" + v.Key.Substring("npc_dota_hero_".Length) + ".vmat"));
            }

            var posDict = this.PositionDictionary;
            foreach (var v in this.Dictionary.Where(v => !posDict.ContainsKey(v.Key)))
            {
                this.PositionDictionary.Add(v.Key, new float[] { 0, 0 });
            }

            var svDict = this.SValuesDictionary;
            foreach (var v in this.Dictionary.Where(v => !svDict.ContainsKey(v.Key)))
            {
                this.SValuesDictionary.Add(v.Key, v.Value);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        public void Add(string name, bool defaultValue = true)
        {
            if (this.Dictionary.ContainsKey(name))
            {
                Console.WriteLine(@"This hero(" + name + @") is already added in HeroToggler");
                return;
            }

            if (this.SValuesDictionary.ContainsKey(name))
            {
                defaultValue = this.SValuesDictionary[name];
            }

            this.Dictionary.Add(name, defaultValue);
            if (!Menu.TextureDictionary.ContainsKey(name))
            {
                Menu.TextureDictionary.Add(
                    name, 
                    Drawing.GetTexture(
                        "materials/ensage_ui/heroes_horizontal/" + name.Substring("npc_dota_hero_".Length) + ".vmat"));
            }

            if (!this.SValuesDictionary.ContainsKey(name))
            {
                this.SValuesDictionary.Add(name, defaultValue);
            }

            if (this.PositionDictionary.ContainsKey(name))
            {
                return;
            }

            this.PositionDictionary.Add(name, new float[] { 0, 0 });
        }

        /// <summary>
        ///     The is enabled.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool IsEnabled(string name)
        {
            return this.Dictionary.ContainsKey(name) && this.Dictionary[name];
        }

        /// <summary>
        ///     The remove.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        public void Remove(string name)
        {
            if (this.Dictionary.ContainsKey(name))
            {
                this.Dictionary.Remove(name);
            }
        }

        #endregion
    }
}