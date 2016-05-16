namespace Ensage.Common.Objects.DrawObjects
{
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.Common.Extensions;
    using Ensage.Common.Extensions.SharpDX;
    using Ensage.Common.Objects.UtilityObjects;

    using SharpDX;

    /// <summary>
    ///     The polygon.
    /// </summary>
    public abstract class Polygon
    {
        #region Static Fields

        /// <summary>
        ///     The update sleeper.
        /// </summary>
        private static readonly Sleeper UpdateSleeper;

        /// <summary>
        ///     The entities.
        /// </summary>
        private static List<Entity> entities;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="Polygon" /> class.
        /// </summary>
        static Polygon()
        {
            UpdateSleeper = new Sleeper();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Polygon" /> class.
        /// </summary>
        protected Polygon()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the entities.
        /// </summary>
        internal static List<Entity> Entities
        {
            get
            {
                if (UpdateSleeper.Sleeping)
                {
                    return entities;
                }

                entities = ObjectManager.GetEntities<Entity>().ToList();
                UpdateSleeper.Sleep(3000);
                return entities;
            }
        }

        /// <summary>
        ///     Gets or sets the draw type.
        /// </summary>
        internal DrawType DrawType { get; set; }

        /// <summary>
        ///     Gets or sets the ground.
        /// </summary>
        internal float Ground { get; set; }

        /// <summary>
        ///     The points.
        /// </summary>
        internal List<Vector2> ScreenPoints { get; set; }

        /// <summary>
        ///     Gets or sets the world points.
        /// </summary>
        internal List<Vector3> WorldPoints { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The draw.
        /// </summary>
        /// <param name="color">
        ///     The color.
        /// </param>
        protected virtual void Draw(Color color)
        {
            if (this.DrawType == DrawType.Screen)
            {
                for (var i = 0; i <= this.ScreenPoints.Count - 1; i++)
                {
                    var nextIndex = (this.ScreenPoints.Count - 1 == i) ? 0 : (i + 1);
                    Drawing.DrawLine(this.ScreenPoints[i], this.ScreenPoints[nextIndex], color);
                }
            }
            else
            {
                for (var i = 0; i <= this.WorldPoints.Count - 1; i++)
                {
                    var nextIndex = (this.WorldPoints.Count - 1 == i) ? 0 : (i + 1);
                    var point1 = Drawing.WorldToScreen(this.WorldPoints[i]);
                    var point2 = Drawing.WorldToScreen(this.WorldPoints[nextIndex]);
                    Drawing.DrawLine(point1, point2, color);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The get ground vector.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        internal static float GetGround(Vector2 position)
        {
            return Entities.Where(x => x.IsValid).MinOrDefault(x => x.Position.Distance(position)).Position.Z;
        }

        #endregion
    }
}