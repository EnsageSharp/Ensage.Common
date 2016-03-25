namespace Ensage.Common.Menu
{
    using System;

    /// <summary>
    ///     The slider.
    /// </summary>
    [Serializable]
    public struct Slider
    {
        #region Fields

        /// <summary>
        ///     The max value.
        /// </summary>
        public int MaxValue;

        /// <summary>
        ///     The min value.
        /// </summary>
        public int MinValue;

        /// <summary>
        ///     The value.
        /// </summary>
        private int value;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Slider" /> struct.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <param name="minValue">
        ///     The min value.
        /// </param>
        /// <param name="maxValue">
        ///     The max value.
        /// </param>
        public Slider(int value = 0, int minValue = 0, int maxValue = 100)
        {
            this.MaxValue = Math.Max(maxValue, minValue);
            this.MinValue = Math.Min(maxValue, minValue);
            this.value = value;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        public int Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = Math.Min(Math.Max(value, this.MinValue), this.MaxValue);
            }
        }

        #endregion
    }
}