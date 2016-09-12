using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensage.Common.Menu.Themes
{
    using System.ComponentModel.Composition;

    /// <summary>
    /// The default.
    /// </summary>
    [Export(typeof(IMenuTheme))]
    internal class Default : IMenuTheme
    {
        /// <summary>
        /// Gets the theme name.
        /// </summary>
        public string ThemeName { get; } = "DefaultTheme";

        /// <summary>
        /// Gets the menu background.
        /// </summary>
        public string MenuBackground { get; } = "materials/ensage_ui/menu/menubg1.vmat_c";

        /// <summary>
        /// Gets the menu arrow.
        /// </summary>
        public string MenuArrow { get; } = "materials/ensage_ui/menu/arrowright.vmat_c";

        /// <summary>
        /// Gets the menu arrow open.
        /// </summary>
        public string MenuArrowOpen { get; } = "materials/ensage_ui/menu/arrowrighthover.vmat_c";

        /// <summary>
        /// Gets the item background.
        /// </summary>
        public string ItemBackground { get; } = "materials/ensage_ui/menu/itembg1.vmat_c";

        /// <summary>
        /// Gets the on off slider background.
        /// </summary>
        public string OnOffSliderBackground { get; } = "materials/ensage_ui/menu/sliderbgon.vmat_c";

        /// <summary>
        /// Gets the on off slider enabled.
        /// </summary>
        public string OnOffSliderEnabled { get; } = "materials/ensage_ui/menu/circleshadow.vmat_c";

        /// <summary>
        /// Gets the on off slider disabled.
        /// </summary>
        public string OnOffSliderDisabled { get; } = "materials/ensage_ui/menu/circleshadowgray.vmat_c";
    }
}
