namespace Ensage.Common.Menu
{
    /// <summary>
    ///     The on value change event args.
    /// </summary>
    public class OnValueChangeEventArgs
    {
        #region Fields

        /// <summary>
        ///     The _new value.
        /// </summary>
        private readonly object newValue;

        /// <summary>
        ///     The _old value.
        /// </summary>
        private readonly object oldValue;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="OnValueChangeEventArgs" /> class.
        /// </summary>
        /// <param name="oldValue">
        ///     The old value.
        /// </param>
        /// <param name="newValue">
        ///     The new value.
        /// </param>
        public OnValueChangeEventArgs(object oldValue, object newValue)
        {
            this.oldValue = oldValue;
            this.newValue = newValue;
            this.Process = true;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether process.
        /// </summary>
        public bool Process { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The get new value.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="T" />.
        /// </returns>
        public T GetNewValue<T>()
        {
            return (T)this.newValue;
        }

        /// <summary>
        ///     The get old value.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="T" />.
        /// </returns>
        public T GetOldValue<T>()
        {
            return (T)this.oldValue;
        }

        #endregion
    }
}