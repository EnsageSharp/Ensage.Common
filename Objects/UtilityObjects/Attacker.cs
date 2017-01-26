// <copyright file="Attacker.cs" company="EnsageSharp">
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
namespace Ensage.Common.Objects.UtilityObjects
{
    using System;

    using Ensage.Common.Extensions;

    /// <summary>
    ///     The attacker.
    /// </summary>
    public class Attacker
    {
        #region Fields

        /// <summary>
        ///     The attack.
        /// </summary>
        private readonly Action<Unit> attack;

        /// <summary>
        ///     The use modifiers.
        /// </summary>
        private bool useModifier;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Attacker" /> class.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        public Attacker(Unit unit)
        {
            this.Unit = unit;
            switch (unit.ClassID)
            {
                case ClassID.CDOTA_Unit_Hero_Clinkz:
                    this.AttackModifier = unit.Spellbook.Spell2;
                    this.attack = (target) =>
                        {
                            if (this.useModifier && this.Unit.CanCast() && this.AttackModifier.Level > 0
                                && unit.Mana > this.AttackModifier.ManaCost)
                            {
                                this.AttackModifier.UseAbility(target);
                                return;
                            }

                            this.Unit.Attack(target);
                        };
                    break;
                case ClassID.CDOTA_Unit_Hero_DrowRanger:
                    this.AttackModifier = unit.Spellbook.Spell1;
                    this.attack = (target) =>
                        {
                            if (this.useModifier && this.Unit.CanCast() && this.AttackModifier.Level > 0
                                && unit.Mana > this.AttackModifier.ManaCost)
                            {
                                this.AttackModifier.UseAbility(target);
                                return;
                            }

                            this.Unit.Attack(target);
                        };
                    break;
                case ClassID.CDOTA_Unit_Hero_Viper:
                    this.AttackModifier = unit.Spellbook.SpellQ;
                    this.attack = (target) =>
                        {
                            if (this.useModifier && this.Unit.CanCast() && this.AttackModifier.Level > 0
                                && unit.Mana > this.AttackModifier.ManaCost)
                            {
                                this.AttackModifier.UseAbility(target);
                                return;
                            }

                            this.Unit.Attack(target);
                        };
                    break;
                case ClassID.CDOTA_Unit_Hero_Huskar:
                    this.AttackModifier = unit.Spellbook.Spell2;
                    this.attack = (target) =>
                        {
                            if (this.useModifier && this.Unit.CanCast() && this.AttackModifier.Level > 0
                                && this.Unit.Health > this.Unit.MaximumHealth * 0.35)
                            {
                                this.AttackModifier.UseAbility(target);
                                return;
                            }

                            this.Unit.Attack(target);
                        };
                    break;
                case ClassID.CDOTA_Unit_Hero_Silencer:
                    this.AttackModifier = unit.Spellbook.Spell2;
                    this.attack = (target) =>
                        {
                            if (this.useModifier && this.Unit.CanCast() && this.AttackModifier.Level > 0
                                && this.Unit.Mana > this.AttackModifier.ManaCost)
                            {
                                this.AttackModifier.UseAbility(target);
                                return;
                            }

                            this.Unit.Attack(target);
                        };
                    break;
                case ClassID.CDOTA_Unit_Hero_Jakiro:
                    this.AttackModifier = unit.Spellbook.Spell3;
                    this.attack = (target) =>
                        {
                            if (this.useModifier && this.Unit.CanCast() && this.AttackModifier.Level > 0
                                && this.AttackModifier.CanBeCasted())
                            {
                                this.AttackModifier.UseAbility(target);
                                return;
                            }

                            this.Unit.Attack(target);
                        };
                    break;
                case ClassID.CDOTA_Unit_Hero_Obsidian_Destroyer:
                    this.AttackModifier = unit.Spellbook.Spell1;
                    this.attack = (target) =>
                        {
                            if (this.useModifier && this.Unit.CanCast() && this.AttackModifier.Level > 0
                                && this.Unit.Mana > this.AttackModifier.ManaCost)
                            {
                                this.AttackModifier.UseAbility(target);
                                return;
                            }

                            this.Unit.Attack(target);
                        };
                    break;
                case ClassID.CDOTA_Unit_Hero_Enchantress:
                    this.AttackModifier = unit.Spellbook.Spell4;
                    this.attack = (target) =>
                        {
                            if (this.useModifier && this.Unit.CanCast() && this.AttackModifier.Level > 0
                                && this.Unit.Mana > this.AttackModifier.ManaCost)
                            {
                                this.AttackModifier.UseAbility(target);
                                return;
                            }

                            this.Unit.Attack(target);
                        };
                    break;
                default:
                    this.attack = (target) =>
                        {
                            if (target == null)
                            {
                                return;
                            }

                            this.Unit.Attack(target);
                        };
                    break;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the attack modifier.
        /// </summary>
        public Ability AttackModifier { get; set; }

        /// <summary>
        ///     Gets or sets the unit.
        /// </summary>
        public Unit Unit { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The attack.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="useModifier">
        ///     The use modifier.
        /// </param>
        public void Attack(Unit target, bool useModifier = true)
        {
            this.useModifier = useModifier;
            this.attack.Invoke(target);
        }

        #endregion
    }
}