// <copyright file="ResizableItem.cs" company="EnsageSharp">
//    Copyright (c) 2017 EnsageSharp.
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>
namespace Ensage.Common.Menu.Draw
{
    using Ensage.Common.Menu.Transitions;

    using SharpDX;

    /// <summary>
    ///     The resizable item.
    /// </summary>
    public abstract class ResizableItem
    {
        #region Fields

        /// <summary>
        ///     The default resize percentage.
        /// </summary>
        protected readonly float DefaultResizePercentage;

        /// <summary>
        ///     The default resize percentage.
        /// </summary>
        private float lastResizePercentage;

        /// <summary>
        ///     The resizing back.
        /// </summary>
        private bool resizingBack;

        /// <summary>
        ///     The size.
        /// </summary>
        private Vector2 size;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResizableItem" /> class.
        /// </summary>
        /// <param name="defaultResizePercentage">
        ///     The default resize percentage.
        /// </param>
        protected ResizableItem(float defaultResizePercentage)
        {
            this.DefaultResizePercentage = defaultResizePercentage;
            this.ResizeTransition = new QuadEaseOut(0.2);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     The resize transition.
        /// </summary>
        public Transition ResizeTransition { get; private set; }

        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        public virtual Vector2 Size
        {
            get
            {
                if (!this.ResizeTransition.Moving)
                {
                    return this.size * (1 + this.ResizeTransition.GetValue() / 100);
                }

                if (this.resizingBack)
                {
                    return this.size * (1 + (this.lastResizePercentage - this.ResizeTransition.GetValue()) / 100);
                }

                return this.size * (1 + this.ResizeTransition.GetValue() / 100);
            }

            set
            {
                this.size = value;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the height.
        /// </summary>
        internal abstract int Height { get; }

        /// <summary>
        ///     Gets the width.
        /// </summary>
        internal abstract int Width { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The resize.
        /// </summary>
        /// <param name="percentage">
        ///     The percentage.
        /// </param>
        public virtual void Resize(float percentage)
        {
            this.resizingBack = false;
            this.lastResizePercentage = percentage;
            this.ResizeTransition.Start(0, this.lastResizePercentage);
        }

        /// <summary>
        ///     The resize.
        /// </summary>
        public virtual void Resize()
        {
            this.resizingBack = false;
            this.lastResizePercentage = this.DefaultResizePercentage;
            this.ResizeTransition.Start(0, this.DefaultResizePercentage);
        }

        /// <summary>
        ///     The resize back.
        /// </summary>
        public virtual void ResizeBack()
        {
            this.resizingBack = true;
            this.ResizeTransition.Start(0, this.DefaultResizePercentage);
        }

        #endregion
    }
}