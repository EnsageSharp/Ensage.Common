// <copyright file="Gray.cs" company="EnsageSharp">
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
namespace Ensage.Common.Menu.Themes
{
    using System.ComponentModel.Composition;

    using SharpDX;

    /// <summary>
    ///     The gray.
    /// </summary>
    [Export(typeof(IMenuTheme))]
    internal class Gray : IMenuTheme
    {
        #region Public Properties

        /// <summary>
        ///     Gets the item background.
        /// </summary>
        public string ItemBackground { get; } = "materials/ensage_ui/menu/themes/gray/itembg1.vmat_c";

        /// <summary>
        ///     Gets the item default text color.
        /// </summary>
        public Color ItemDefaultTextColor { get; } = new Color(200, 200, 200, 255);

        /// <summary>
        ///     Gets the item overlay color.
        /// </summary>
        public Color ItemOverlayColor { get; } = new Color(38, 38, 38, 190);

        /// <summary>
        ///     Gets the key bind activated overlay color.
        /// </summary>
        public Color KeyBindActivatedBorderColor { get; } = new Color(180, 120, 0, 30);

        /// <summary>
        ///     Gets the key bind activated overlay color.
        /// </summary>
        public Color KeyBindActivatedOverlayColor { get; } = new Color(48, 38, 28, 30);

        /// <summary>
        ///     Gets the key bind text color.
        /// </summary>
        public Color KeyBindTextColor { get; } = new Color(195, 139, 12, 225);

        /// <summary>
        ///     Gets the menu arrow.
        /// </summary>
        public string MenuArrow { get; } = "materials/ensage_ui/menu/themes/gray/arrowright.vmat_c";

        /// <summary>
        ///     Gets the menu arrow open.
        /// </summary>
        public string MenuArrowOpen { get; } = "materials/ensage_ui/menu/themes/gray/arrowrighthover.vmat_c";

        /// <summary>
        ///     Gets the menu background.
        /// </summary>
        public string MenuBackground { get; } = "materials/ensage_ui/menu/themes/gray/menubg1.vmat_c";

        /// <summary>
        ///     Gets the menu default text color.
        /// </summary>
        public Color MenuDefaultTextColor { get; } = new Color(195, 186, 173, 255);

        /// <summary>
        ///     Gets the on off slider background.
        /// </summary>
        public string OnOffSliderBackground { get; } = "materials/ensage_ui/menu/themes/gray/sliderbgon.vmat_c";

        /// <summary>
        ///     Gets the on off slider disabled.
        /// </summary>
        public string OnOffSliderDisabled { get; } = "materials/ensage_ui/menu/themes/gray/circleshadowgray.vmat_c";

        /// <summary>
        ///     Gets the on off slider enabled.
        /// </summary>
        public string OnOffSliderEnabled { get; } = "materials/ensage_ui/menu/themes/gray/circleshadow.vmat_c";

        /// <summary>
        ///     Gets the root menu background color (color under menu background texture).
        /// </summary>
        public Color RootMenuBackgroundColor { get; } = new Color(35, 35, 35, 150);

        /// <summary>
        ///     Gets the menu side line activated color.
        /// </summary>
        public Color RootMenuOpenSideLineColor { get; } = new Color(220, 120, 20);

        /// <summary>
        ///     Gets the root menu overlay color.
        /// </summary>
        public Color RootMenuOverlayColor { get; } = new Color(20, 20, 20, 190);

        /// <summary>
        ///     Gets the root menu side line color.
        /// </summary>
        public Color RootMenuSideLineColor { get; } = new Color(20, 20, 20);

        /// <summary>
        ///     Gets the slider color.
        /// </summary>
        public Color SliderColor { get; } = new Color(200, 120, 60);

        /// <summary>
        ///     Gets the slider fill color.
        /// </summary>
        public Color SliderFillColor { get; } = new Color(150, 110, 15, 20);

        /// <summary>
        ///     Gets the slider text color.
        /// </summary>
        public Color SliderTextColor { get; } = new Color(255, 140, 0);

        /// <summary>
        ///     Gets the string list arrow color.
        /// </summary>
        public Color StringListArrowColor { get; } = new Color(255, 165, 0);

        /// <summary>
        ///     Gets the string list arrow hovered color.
        /// </summary>
        public Color StringListArrowHoveredColor { get; } = new Color(255, 140, 0);

        /// <summary>
        ///     Gets the string list arrow hovered overlay color.
        /// </summary>
        public Color StringListArrowHoveredOverlayColor { get; } = new Color(70, 70, 30, 70);

        /// <summary>
        ///     Gets or sets the string list arrow overlay color.
        /// </summary>
        public Color StringListArrowOverlayColor { get; set; } = new Color(0, 37, 37, 37);

        /// <summary>
        ///     Gets the string list text color.
        /// </summary>
        public Color StringListTextColor { get; } = new Color(230, 210, 200, 225);

        /// <summary>
        ///     Gets the sub menu open side line color.
        /// </summary>
        public Color SubMenuOpenSideLineColor { get; } = new Color(220, 120, 20);

        /// <summary>
        ///     Gets the sub menu overlay color.
        /// </summary>
        public Color SubMenuOverlayColor { get; } = new Color(20, 20, 20, 190);

        /// <summary>
        ///     Gets the sub menu side line color.
        /// </summary>
        public Color SubMenuSideLineColor { get; } = new Color(20, 20, 20);

        /// <summary>
        ///     Gets the theme name.
        /// </summary>
        public string ThemeName { get; } = "Gray";

        /// <summary>
        ///     Gets the ability/hero toggler disabled color.
        /// </summary>
        public Color TogglerDisabledColor { get; } = new Color(37, 37, 37);

        /// <summary>
        ///     Gets the ability/hero toggler enabled color.
        /// </summary>
        public Color TogglerEnabledColor { get; } = new Color(180, 120, 1);

        /// <summary>
        ///     Gets the tool tip background color.
        /// </summary>
        public Color ToolTipBackgroundColor { get; } = new Color(45, 45, 45);

        /// <summary>
        ///     Gets the tool tip text color.
        /// </summary>
        public Color ToolTipTextColor { get; } = new Color(255, 255, 255);

        /// <summary>
        ///     Gets the top panel background color.
        /// </summary>
        public Color TopPanelBackgroundColor { get; } = new Color(35, 35, 35);

        /// <summary>
        ///     Gets the top panel text color.
        /// </summary>
        public Color TopPanelTextColor { get; } = new Color(255, 255, 255);

        #endregion
    }
}