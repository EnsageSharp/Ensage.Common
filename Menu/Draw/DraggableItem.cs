// <copyright file="DraggableItem.cs" company="EnsageSharp">
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
    using System;
    using System.Collections.Generic;

    using Ensage.Common.Extensions;
    using Ensage.Common.Menu.Transitions;
    using Ensage.Common.Objects.UtilityObjects;

    using SharpDX;

    /// <summary>
    ///     The item which can be dragged.
    /// </summary>
    public abstract class DraggableItem : ResizableItem
    {
        #region Fields

        /// <summary>
        ///     The drag and drop sleeper.
        /// </summary>
        private readonly Sleeper dragAndDropSleeper;

        /// <summary>
        ///     The last click mouse position.
        /// </summary>
        private Vector2 lastClickMousePosition;

        /// <summary>
        ///     The left button down.
        /// </summary>
        private bool leftButtonDown;

        /// <summary>
        ///     The mouse position difference.
        /// </summary>
        private Vector2 mousePositionDifference;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DraggableItem" /> class.
        /// </summary>
        /// <param name="defaultResizePercentage">
        ///     The default Resize Percentage.
        /// </param>
        protected DraggableItem(float defaultResizePercentage)
            : base(defaultResizePercentage)
        {
            this.dragAndDropSleeper = new Sleeper();
            this.DragTransition = new QuadEaseOut(0.2);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether being dragged.
        /// </summary>
        public bool BeingDragged { get; set; }

        /// <summary>
        ///     Gets the dragged position.
        /// </summary>
        public Vector2 DraggedPosition
        {
            get
            {
                if (this.BeingDragged)
                {
                    return new Vector2(
                        this.DragTransition.GetPosition().X,
                        Game.MouseScreenPosition.Y - this.mousePositionDifference.Y);
                }

                return this.DragTransition.Moving ? this.DragTransition.GetPosition() : this.RealPosition;
            }
        }

        /// <summary>
        ///     Gets or sets the transition.
        /// </summary>
        public Transition DragTransition { get; set; }

        /// <summary>
        ///     Gets or sets the order number.
        /// </summary>
        public int OrderNumber { get; set; }

        /// <summary>
        ///     The position.
        /// </summary>
        public virtual Vector2 RealPosition { get; set; }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the position.
        /// </summary>
        protected internal abstract Vector2 Position { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     The on receive message.
        /// </summary>
        /// <param name="message">
        ///     The message.
        /// </param>
        /// <param name="cursorPos">
        ///     The cursor position.
        /// </param>
        /// <param name="key">
        ///     The key.
        /// </param>
        /// <param name="draggableItems">
        ///     The Items.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        protected void DraggableOnReceiveMessage(
            Utils.WindowsMessages message,
            Vector2 cursorPos,
            uint key,
            List<DraggableItem> draggableItems,
            WndEventArgs args = null)
        {
            if (!this.IsInside(cursorPos) && !this.BeingDragged)
            {
                this.OnReceiveMessage(message, cursorPos, key, args);
                return;
            }

            if (!this.BeingDragged && message == Utils.WindowsMessages.WM_MOUSEMOVE
                && cursorPos.Distance(this.lastClickMousePosition) > 1
                && Math.Abs(cursorPos.Y - this.lastClickMousePosition.Y) > 5)
            {
                if (this.leftButtonDown && !this.ResizeTransition.Moving)
                {
                    this.PrepareDraggedIcon();
                    return;
                }
            }

            if (this.BeingDragged && message == Utils.WindowsMessages.WM_MOUSEMOVE)
            {
                foreach (var draggableItem in
                    draggableItems)
                {
                    if (draggableItem.BeingDragged || draggableItem.DragTransition.Moving)
                    {
                        continue;
                    }

                    if (cursorPos.Y < draggableItem.RealPosition.Y + draggableItem.Height
                        && draggableItem.OrderNumber == this.OrderNumber - 1)
                    {
                        draggableItem.DragTransition.Start(draggableItem.RealPosition, this.RealPosition);
                        this.OrderNumber--;
                        draggableItem.OrderNumber++;
                        break;
                    }

                    if (!(cursorPos.Y > draggableItem.RealPosition.Y)
                        || draggableItem.OrderNumber != this.OrderNumber + 1)
                    {
                        continue;
                    }

                    draggableItem.DragTransition.Start(draggableItem.RealPosition, this.RealPosition);
                    this.OrderNumber++;
                    draggableItem.OrderNumber--;
                    break;
                }
            }

            if (!this.dragAndDropSleeper.Sleeping && message == Utils.WindowsMessages.WM_LBUTTONDOWN)
            {
                this.leftButtonDown = true;
                this.dragAndDropSleeper.Sleep(200);
                this.lastClickMousePosition = cursorPos;
                DelayAction.Add(
                    200,
                    () =>
                        {
                            if (!this.BeingDragged && this.leftButtonDown)
                            {
                                this.PrepareDraggedIcon();
                            }
                        });
            }

            if (message != Utils.WindowsMessages.WM_LBUTTONUP)
            {
                return;
            }

            this.leftButtonDown = false;
            if (this.BeingDragged)
            {
                this.DragTransition.Start(this.Position, this.RealPosition);
                this.ResizeBack();
                this.BeingDragged = false;
                return;
            }

            if (!this.dragAndDropSleeper.Sleeping)
            {
                return;
            }

            this.OnReceiveMessage(message, cursorPos, key, args);
        }

        /// <summary>
        ///     The is inside.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        protected abstract bool IsInside(Vector2 position);

        /// <summary>
        ///     The on receive message.
        /// </summary>
        /// <param name="message">
        ///     The message.
        /// </param>
        /// <param name="cursorPos">
        ///     The cursor position.
        /// </param>
        /// <param name="key">
        ///     The key.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        protected abstract void OnReceiveMessage(
            Utils.WindowsMessages message,
            Vector2 cursorPos,
            uint key,
            WndEventArgs args = null);

        /// <summary>
        ///     The prepare dragged icon.
        /// </summary>
        private void PrepareDraggedIcon()
        {
            var enlargePosition = this.Position
                                  - new Vector2(
                                      this.Width * (this.DefaultResizePercentage / 200),
                                      this.Height * (this.DefaultResizePercentage / 200));
            if (!this.ResizeTransition.Moving)
            {
                this.Size = new Vector2(this.Height, this.Width);
            }

            this.mousePositionDifference = this.lastClickMousePosition - enlargePosition;
            this.DragTransition.Start(this.Position, enlargePosition);
            this.RealPosition = this.Position;
            this.Resize();
            this.BeingDragged = true;
        }

        #endregion
    }
}