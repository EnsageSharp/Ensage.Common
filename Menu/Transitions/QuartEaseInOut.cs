namespace Ensage.Common.Menu.Transitions
{
    /// <summary>
    ///     The expo ease in out.
    /// </summary>
    public class QuartEaseInOut : Transition
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="QuartEaseInOut" /> class.
        /// </summary>
        /// <param name="duration">
        ///     The duration.
        /// </param>
        public QuartEaseInOut(double duration)
            : base(duration)
        {
        }

        #endregion

        #region Public Methods and Operators

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
        public override double Equation(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1)
            {
                return c / 2 * t * t * t * t + b;
            }

            return -c / 2 * ((t -= 2) * t * t * t - 2) + b;
        }

        #endregion
    }
}