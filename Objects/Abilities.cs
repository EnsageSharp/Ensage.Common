namespace Ensage.Common.Objects
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     The abilities.
    /// </summary>
    public class Abilities
    {
        #region Static Fields

        /// <summary>
        ///     The ability dictionary.
        /// </summary>
        public static Dictionary<string, Ability> AbilityDictionary = new Dictionary<string, Ability>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The find ability.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <param name="team">
        ///     The team.
        /// </param>
        /// <returns>
        ///     The <see cref="Ability" />.
        /// </returns>
        public static Ability FindAbility(string name, Team team)
        {
            Ability ability;
            var found = AbilityDictionary.TryGetValue(name + team, out ability);
            if (found && ability.IsValid)
            {
                return ability;
            }

            ability =
                ObjectManager.GetEntities<Ability>().FirstOrDefault(x => x.StoredName() == name && x.Owner.Team == team);
            
            if (ability == null)
            {
                return null;
            }

            if (found)
            {
                AbilityDictionary[name + team] = ability;
            }
            else
            {
                AbilityDictionary.Add(name + team, ability);
            }

            return ability;
        }

        /// <summary>
        ///     The find ability.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="Ability" />.
        /// </returns>
        public static Ability FindAbility(string name)
        {
            Ability ability;
            if (AbilityDictionary.TryGetValue(name, out ability) && ability.IsValid)
            {
                return ability;
            }

            ability = ObjectManager.GetEntities<Ability>().FirstOrDefault(x => x.StoredName() == name);
            if (ability != null)
            {
                AbilityDictionary.Add(name, ability);
            }

            return ability;
        }

        #endregion
    }
}