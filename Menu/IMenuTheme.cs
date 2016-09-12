using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensage.Common.Menu
{
    using System.ComponentModel;
    using System.ComponentModel.Composition;

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
    }
}
