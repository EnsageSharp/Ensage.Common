// <copyright file="IMenuTheme.cs" company="EnsageSharp">
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
namespace Ensage.Common.Menu
{
    using SharpDX;

    /// <summary>
    ///     The MenuTheme interface contains properties for paths to theme textures.
    /// </summary>
    public interface IMenuTheme
    {
        #region Public Properties

        /// <summary>
        ///     Gets the item background.
        /// </summary>
        string ItemBackground { get; }

        /// <summary>
        ///     Gets the item default text color.
        /// </summary>
        Color ItemDefaultTextColor { get; }

        /// <summary>
        ///     Gets the item overlay color.
        /// </summary>
        Color ItemOverlayColor { get; }

        /// <summary>
        ///     Gets the key bind activated overlay color.
        /// </summary>
        Color KeyBindActivatedBorderColor { get; }

        /// <summary>
        ///     Gets the key bind activated overlay color.
        /// </summary>
        Color KeyBindActivatedOverlayColor { get; }

        /// <summary>
        ///     Gets the key bind text color.
        /// </summary>
        Color KeyBindTextColor { get; }

        /// <summary>
        ///     Gets the menu arrow.
        /// </summary>
        string MenuArrow { get; }

        /// <summary>
        ///     Gets the menu arrow open.
        /// </summary>
        string MenuArrowOpen { get; }

        /// <summary>
        ///     Gets the menu background.
        /// </summary>
        string MenuBackground { get; }

        /// <summary>
        ///     Gets the menu default text color.
        /// </summary>
        Color MenuDefaultTextColor { get; }

        /// <summary>
        ///     Gets the on off slider background.
        /// </summary>
        string OnOffSliderBackground { get; }

        /// <summary>
        ///     Gets the on off slider disabled.
        /// </summary>
        string OnOffSliderDisabled { get; }

        /// <summary>
        ///     Gets the on off slider enabled.
        /// </summary>
        string OnOffSliderEnabled { get; }

        /// <summary>
        ///     Gets the root menu background color (color under menu background texture).
        /// </summary>
        Color RootMenuBackgroundColor { get; }

        /// <summary>
        ///     Gets the menu side line activated color.
        /// </summary>
        Color RootMenuOpenSideLineColor { get; }

        /// <summary>
        ///     Gets the root menu overlay color.
        /// </summary>
        Color RootMenuOverlayColor { get; }

        /// <summary>
        ///     Gets the root menu side line color.
        /// </summary>
        Color RootMenuSideLineColor { get; }

        /// <summary>
        ///     Gets the slider color.
        /// </summary>
        Color SliderColor { get; }

        /// <summary>
        ///     Gets the slider fill color.
        /// </summary>
        Color SliderFillColor { get; }

        /// <summary>
        ///     Gets the slider text color.
        /// </summary>
        Color SliderTextColor { get; }

        /// <summary>
        ///     Gets the string list arrow color.
        /// </summary>
        Color StringListArrowColor { get; }

        /// <summary>
        ///     Gets the string list arrow hovered color.
        /// </summary>
        Color StringListArrowHoveredColor { get; }

        /// <summary>
        ///     Gets the string list arrow hovered overlay color.
        /// </summary>
        Color StringListArrowHoveredOverlayColor { get; }

        /// <summary>
        ///     Gets the string list arrow overlay color.
        /// </summary>
        Color StringListArrowOverlayColor { get; }

        /// <summary>
        ///     Gets the string list text color.
        /// </summary>
        Color StringListTextColor { get; }

        /// <summary>
        ///     Gets the sub menu open side line color.
        /// </summary>
        Color SubMenuOpenSideLineColor { get; }

        /// <summary>
        ///     Gets the sub menu overlay color.
        /// </summary>
        Color SubMenuOverlayColor { get; }

        /// <summary>
        ///     Gets the sub menu side line color.
        /// </summary>
        Color SubMenuSideLineColor { get; }

        /// <summary>
        ///     Gets the theme name.
        /// </summary>
        string ThemeName { get; }

        /// <summary>
        ///     Gets the ability/hero toggler disabled color.
        /// </summary>
        Color TogglerDisabledColor { get; }

        /// <summary>
        ///     Gets the ability/hero toggler enabled color.
        /// </summary>
        Color TogglerEnabledColor { get; }

        /// <summary>
        ///     Gets the tool tip background color.
        /// </summary>
        Color ToolTipBackgroundColor { get; }

        /// <summary>
        ///     Gets the tool tip text color.
        /// </summary>
        Color ToolTipTextColor { get; }

        /// <summary>
        ///     Gets the top panel background color.
        /// </summary>
        Color TopPanelBackgroundColor { get; }

        /// <summary>
        ///     Gets the top panel text color.
        /// </summary>
        Color TopPanelTextColor { get; }

        #endregion
    }
}