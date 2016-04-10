namespace Ensage.Common
{
    using System;
    using System.Threading;

    /// <summary>
    ///     Class that contains all of the needed information for delaying an action.
    /// </summary>
    public class DelayActionItem
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DelayActionItem" /> class.
        /// </summary>
        /// <param name="time">The time(in milliseconds) to call the function..</param>
        /// <param name="func">The function to call once the <paramref name="time" /> has expired.</param>
        /// <param name="token">The cancelation token.</param>
        public DelayActionItem(int time, Action func, CancellationToken token)
        {
            this.Time = time + (Environment.TickCount & int.MaxValue);
            this.Function = func;
            this.Token = token;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the function.
        /// </summary>
        /// <value>
        ///     The function.
        /// </value>
        public Action Function { get; set; }

        /// <summary>
        ///     Gets or sets the time the function will be executed at.
        /// </summary>
        /// <value>
        ///     The time the function will be executed at.
        /// </value>
        public int Time { get; set; }

        /// <summary>
        ///     Gets or sets the cancelation token.
        /// </summary>
        /// <value>
        ///     The cancelation token.
        /// </value>
        /// <example>
        ///     <see cref="CancellationTokenSource" />
        /// </example>
        public CancellationToken Token { get; set; }

        #endregion
    }
}