namespace Ensage.Common.Menu.Transitions
{
    using Ensage.Common.Extensions.SharpDX;

    using SharpDX;

    /// <summary>
    ///     The quad ease in out.
    /// </summary>
    public class QuadEaseInOut
    {
        #region Fields

        /// <summary>
        ///     The end position.
        /// </summary>
        private Vector2 endPosition;

        /// <summary>
        ///     The final value.
        /// </summary>
        private float finalValue;

        /// <summary>
        ///     Gets or sets the last position.
        /// </summary>
        private Vector2 startPosition;

        /// <summary>
        ///     The start value.
        /// </summary>
        private float startValue;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="QuadEaseInOut" /> class.
        /// </summary>
        /// <param name="duration">
        ///     The duration.
        /// </param>
        public QuadEaseInOut(double duration)
        {
            this.Duration = duration;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the duration.
        /// </summary>
        public double Duration { get; set; }

        /// <summary>
        ///     Gets or sets the start time.
        /// </summary>
        public double StartTime { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The get position.
        /// </summary>
        /// <returns>
        ///     The <see cref="Vector2" />.
        /// </returns>
        public Vector2 GetPosition()
        {
            if (!this.Moving())
            {
                return this.endPosition;
            }

            return this.startPosition.Extend(
                this.endPosition, 
                (float)
                Equation(
                    Game.GameTime - this.StartTime, 
                    0, 
                    this.endPosition.Distance(this.startPosition), 
                    this.Duration));
        }

        /// <summary>
        ///     The get value.
        /// </summary>
        /// <returns>
        ///     The <see cref="float" />.
        /// </returns>
        public float GetValue()
        {
            if (!this.Moving())
            {
                return this.finalValue;
            }

            return (float)Equation(Game.GameTime - this.StartTime, this.startValue, this.finalValue, this.Duration);
        }

        /// <summary>
        ///     The moving.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool Moving()
        {
            return Game.GameTime < this.StartTime + this.Duration;
        }

        /// <summary>
        ///     The start.
        /// </summary>
        /// <param name="from">
        ///     The from.
        /// </param>
        /// <param name="to">
        ///     The ton.
        /// </param>
        public void Start(Vector2 from, Vector2 to)
        {
            this.startPosition = from;
            this.endPosition = to;
            this.StartTime = Game.GameTime;
        }

        /// <summary>
        ///     The start.
        /// </summary>
        /// <param name="from">
        ///     The from.
        /// </param>
        /// <param name="to">
        ///     The to.
        /// </param>
        public void Start(float from, float to)
        {
            this.startValue = from;
            this.finalValue = to;
            this.StartTime = Game.GameTime;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The equation.
        /// </summary>
        /// <param name="t">
        ///     The t.
        /// </param>
        /// <param name="b">
        ///     The b.
        /// </param>
        /// <param name="c">
        ///     The c.
        /// </param>
        /// <param name="d">
        ///     The d.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        private static double Equation(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1)
            {
                return c / 2 * t * t + b;
            }

            return -c / 2 * ((--t) * (t - 2) - 1) + b;
        }

        #endregion
    }
}