namespace Ensage.Common.Menu.Transitions
{
    /// <summary>
    ///     The quad ease in out.
    /// </summary>
    public class QuadEaseOut : Transition
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="QuadEaseOut" /> class.
        /// </summary>
        /// <param name="duration">
        ///     The duration.
        /// </param>
        public QuadEaseOut(double duration)
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
            return -c * (t /= d) * (t - 2) + b;
        }

        #endregion
    }
}