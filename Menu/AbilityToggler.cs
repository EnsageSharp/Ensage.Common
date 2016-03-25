namespace Ensage.Common.Menu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     The ability toggler.
    /// </summary>
    [Serializable]
    public struct AbilityToggler
    {
        #region Fields

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

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AbilityToggler" /> struct.
        /// </summary>
        /// <param name="abilityDictionary">
        ///     The ability dictionary.
        /// </param>
        public AbilityToggler(Dictionary<string, bool> abilityDictionary)
        {
            this.Dictionary = abilityDictionary;
            this.PositionDictionary = new Dictionary<string, float[]>();
            this.SValuesDictionary = new Dictionary<string, bool>();
            foreach (var v in this.Dictionary.Where(v => !Menu.TextureDictionary.ContainsKey(v.Key)))
            {
                Menu.TextureDictionary.Add(
                    v.Key, 
                    v.Key.Substring(0, "item".Length) == "item"
                        ? Drawing.GetTexture("materials/ensage_ui/items/" + v.Key.Substring("item_".Length) + ".vmat")
                        : Drawing.GetTexture("materials/ensage_ui/spellicons/" + v.Key + ".vmat"));
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
                Console.WriteLine(@"This ability(" + name + @") is already added in AbilityToggler");
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
                    name.Substring(0, "item".Length) == "item"
                        ? Drawing.GetTexture("materials/ensage_ui/items/" + name.Substring("item_".Length) + ".vmat")
                        : Drawing.GetTexture("materials/ensage_ui/spellicons/" + name + ".vmat"));
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