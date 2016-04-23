namespace Ensage.Common.Menu.Transitions
{
    using System;

    /// <summary>
    ///     The elastic ease in out.
    /// </summary>
    public class ElasticEaseInOut : Transition
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ElasticEaseInOut" /> class.
        /// </summary>
        /// <param name="duration">
        ///     The duration.
        /// </param>
        public ElasticEaseInOut(double duration)
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
            if ((t /= d / 2) == 2)
            {
                return b + c;
            }

            var p = d * (.3 * 1.5);
            var s = p / 4;

            if (t < 1)
            {
                return -.5 * (c * Math.Pow(2, 10 * (t -= 1)) * Math.Sin((t * d - s) * (2 * Math.PI) / p)) + b;
            }

            return c * Math.Pow(2, -10 * (t -= 1)) * Math.Sin((t * d - s) * (2 * Math.PI) / p) * .5 + c + b;
        }

        #endregion
    }
}