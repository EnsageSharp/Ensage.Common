// <copyright file="ItemExtensions.cs" company="EnsageSharp">
//    Copyright (c) 2017 EnsageSharp.
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>
namespace Ensage.Common.Extensions
{
    using System;

    using Ensage.Common.Enums;

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
                    canBeCasted = item.Level > 0 && item.Cooldown <= Math.Max(Game.Ping / 1000 - 0.1, 0);
                    if (item.IsRequiringCharges)
                    {
                        canBeCasted = canBeCasted && item.CurrentCharges > 0;
                    }

                    return canBeCasted;
                }

                canBeCasted = item.Level > 0 && owner.Mana + bonusMana >= item.ManaCost
                              && item.Cooldown <= Math.Max(Game.Ping / 1000 - 0.1, 0);
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

        /// <summary>
        ///     Returns the Item ID.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static ItemId GetItemId(this Item item)
        {
            return (ItemId)item.AbilityData2.ID;
        }

        #endregion
    }
}