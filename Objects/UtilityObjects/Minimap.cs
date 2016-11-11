using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensage.Common.Objects.UtilityObjects
{
    using SharpDX;

    /// <summary>
    /// The minimap.
    /// </summary>
    public class Minimap
    {
        public Minimap(Vector2 position, Vector2 size)
        {
            this.Position = position;
            this.Size = size;
        }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        public Vector2 Size { get; set; }
    }
}
