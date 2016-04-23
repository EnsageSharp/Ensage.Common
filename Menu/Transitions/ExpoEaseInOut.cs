namespace Ensage.Common.Menu.Transitions
{
    using System;

    /// <summary>
    ///     The expo ease in out.
    /// </summary>
    public class ExpoEaseInOut : Transition
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpoEaseInOut" /> class.
        /// </summary>
        /// <param name="duration">
        ///     The duration.
        /// </param>
        public ExpoEaseInOut(double duration)
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
            if (t == 0)
            {
                return b;
            }

            if (t == d)
            {
                return b + c;
            }

            if ((t /= d / 2) < 1)
            {
                return c / 2 * Math.Pow(2, 10 * (t - 1)) + b;
            }

            return c / 2 * (-Math.Pow(2, -10 * --t) + 2) + b;
        }

        #endregion
    }
}