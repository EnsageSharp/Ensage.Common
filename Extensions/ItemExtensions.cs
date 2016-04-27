namespace Ensage.Common.Extensions
{
    using System;

    /// <summary>
    ///     The item extensions.
    /// </summary>
    public static class ItemExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Checks if given item can be used
        /// </summary>
        /// <param name="item">
        ///     The item.
        /// </param>
        /// <param name="bonusMana">
        ///     The bonus Mana.
        /// </param>
        /// <returns>
        ///     returns true in case item can be used
        /// </returns>
        public static bool CanBeCasted(this Item item, float bonusMana = 0)
        {
            if (item == null || !item.IsValid)
            {
                return false;
            }

            try
            {
                var owner = item.Owner as Unit;
                bool canBeCasted;
                if (owner == null)
                {
                    canBeCasted = item.Level > 0 && item.Cooldown <= Math.Max((Game.Ping / 1000) - 0.1, 0);
                    if (item.IsRequiringCharges)
                    {
                        canBeCasted = canBeCasted && item.CurrentCharges > 0;
                    }

                    return canBeCasted;
                }

                canBeCasted = item.Level > 0 && owner.Mana + bonusMana >= item.ManaCost
                              && item.Cooldown <= Math.Max((Game.Ping / 1000) - 0.1, 0);
                if (item.IsRequiringCharges)
                {
                    canBeCasted = canBeCasted && item.CurrentCharges > 0;
                }

                return canBeCasted;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}