namespace Ensage.Common.Menu.Transitions
{
    using System;

    /// <summary>
    ///     The circular ease out in.
    /// </summary>
    public class CircEaseOutIn : Transition
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CircEaseOutIn" /> class.
        /// </summary>
        /// <param name="duration">
        ///     The duration.
        /// </param>
        public CircEaseOutIn(double duration)
            : base(duration)
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The circular ease in.
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
        public static double CircEaseIn(double t, double b, double c, double d)
        {
            return -c * (Math.Sqrt(1 - (t /= d) * t) - 1) + b;
        }

        /// <summary>
        ///     The circular ease out.
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
        public static double CircEaseOut(double t, double b, double c, double d)
        {
            return c * Math.Sqrt(1 - (t = t / d - 1) * t) + b;
        }

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
            if (t < d / 2)
            {
                return CircEaseOut(t * 2, b, c / 2, d);
            }

            return CircEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        #endregion
    }
}