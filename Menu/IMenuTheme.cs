using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensage.Common.Menu
{
    using System.ComponentModel;
    using System.ComponentModel.Composition;

    using SharpDX;

    /// <summary>
    /// The MenuTheme interface contains properties for paths to theme textures.
    /// </summary>
    public interface IMenuTheme
    {
        /// <summary>
        /// Gets the theme name.
        /// </summary>
        string ThemeName { get; }

        /// <summary>
        /// Gets the menu background.
        /// </summary>
        string MenuBackground { get; }

        /// <summary>
        /// Gets the menu arrow.
        /// </summary>
        string MenuArrow { get; }

        /// <summary>
        /// Gets the menu arrow open.
        /// </summary>
        string MenuArrowOpen { get; }

        /// <summary>
        /// Gets the item background.
        /// </summary>
        string ItemBackground { get; }

        /// <summary>
        /// Gets the on off slider background.
        /// </summary>
        string OnOffSliderBackground { get; }

        /// <summary>
        /// Gets the on off slider enabled.
        /// </summary>
        string OnOffSliderEnabled { get; }

        /// <summary>
        /// Gets the on off slider disabled.
        /// </summary>
        string OnOffSliderDisabled { get; }

        /// <summary>
        /// Gets the root menu background color (color under menu background texture).
        /// </summary>
        Color RootMenuBackgroundColor { get; }

        /// <summary>
        /// Gets the menu side line activated color.
        /// </summary>
        Color RootMenuOpenSideLineColor { get; }

        /// <summary>
        /// Gets the root menu side line color.
        /// </summary>
        Color RootMenuSideLineColor { get; }

        /// <summary>
        /// Gets the sub menu open side line color.
        /// </summary>
        Color SubMenuOpenSideLineColor { get; }

        /// <summary>
        /// Gets the sub menu side line color.
        /// </summary>
        Color SubMenuSideLineColor { get; }

        /// <summary>
        /// Gets the top panel background color.
        /// </summary>
        Color TopPanelBackgroundColor { get; }

        /// <summary>
        /// Gets the top panel text color.
        /// </summary>
        Color TopPanelTextColor { get; }

        /// <summary>
        /// Gets the root menu overlay color.
        /// </summary>
        Color RootMenuOverlayColor { get; }

        /// <summary>
        /// Gets the sub menu overlay color.
        /// </summary>
        Color SubMenuOverlayColor { get; }

        /// <summary>
        /// Gets the menu default text color.
        /// </summary>
        Color MenuDefaultTextColor { get; }

        /// <summary>
        /// Gets the item overlay color.
        /// </summary>
        Color ItemOverlayColor { get; }

        /// <summary>
        /// Gets the slider color.
        /// </summary>
        Color SliderColor { get; }

        /// <summary>
        /// Gets the slider fill color.
        /// </summary>
        Color SliderFillColor { get; }

        /// <summary>
        /// Gets the slider text color.
        /// </summary>
        Color SliderTextColor { get; }

        /// <summary>
        /// Gets the key bind activated overlay color.
        /// </summary>
        Color KeyBindActivatedBorderColor { get; }

        /// <summary>
        /// Gets the key bind activated overlay color.
        /// </summary>
        Color KeyBindActivatedOverlayColor { get; }

        /// <summary>
        /// Gets the key bind text color.
        /// </summary>
        Color KeyBindTextColor { get; }

        /// <summary>
        /// Gets the string list arrow hovered overlay color.
        /// </summary>
        Color StringListArrowHoveredOverlayColor { get; }

        /// <summary>
        /// Gets the string list arrow overlay color.
        /// </summary>
        Color StringListArrowOverlayColor { get; }

        /// <summary>
        /// Gets the string list arrow hovered color.
        /// </summary>
        Color StringListArrowHoveredColor { get; }

        /// <summary>
        /// Gets the string list arrow color.
        /// </summary>
        Color StringListArrowColor { get; }

        /// <summary>
        /// Gets the string list text color.
        /// </summary>
        Color StringListTextColor { get; }

        /// <summary>
        /// Gets the ability/hero toggler enabled color.
        /// </summary>
        Color TogglerEnabledColor { get; }

        /// <summary>
        /// Gets the ability/hero toggler disabled color.
        /// </summary>
        Color TogglerDisabledColor { get; }

        /// <summary>
        /// Gets the item default text color.
        /// </summary>
        Color ItemDefaultTextColor { get; }

        /// <summary>
        /// Gets the tool tip background color.
        /// </summary>
        Color ToolTipBackgroundColor { get; }

        /// <summary>
        /// Gets the tool tip text color.
        /// </summary>
        Color ToolTipTextColor { get; }
    }
}
