namespace Ensage.Common.Menu
{
    using System.Collections.Generic;

    using Ensage.Common.Menu.Draw;

    /// <summary>
    ///     The menu variables.
    /// </summary>
    public static class MenuVariables
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the drag and drop dictionary.
        /// </summary>
        public static Dictionary<string, DragAndDrop> DragAndDropDictionary { get; set; }

        /// <summary>
        ///     Gets or sets the on off dictionary.
        /// </summary>
        public static Dictionary<string, OnOffCircleSlider> OnOffDictionary { get; set; }

        #endregion
    }
}