// <copyright file="OnOffSlider.cs" company="EnsageSharp">
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

    using Ensage.Common.Menu.Transitions;
    using Ensage.Common.Objects.DrawObjects;
    using Ensage.Common.Objects.UtilityObjects;

    using SharpDX;

    /// <summary>
    ///     The on off.
    /// </summary>
    public class OnOffSlider
    {
        #region Fields

        /// <summary>
        ///     The char 1.
        /// </summary>
        private readonly DrawText char1;

        /// <summary>
        ///     The char 2.
        /// </summary>
        private readonly DrawText char2;

        /// <summary>
        ///     The hover.
        /// </summary>
        private readonly Transition hover;

        /// <summary>
        ///     The left button sleeper.
        /// </summary>
        private readonly Sleeper leftButtonSleeper;

        /// <summary>
        ///     The off color.
        /// </summary>
        private readonly Color offColor;

        /// <summary>
        ///     The on color.
        /// </summary>
        private readonly Color onColor;

        /// <summary>
        ///     The transition.
        /// </summary>
        private readonly Transition transition;

        /// <summary>
        ///     The circle position.
        /// </summary>
        private Vector2 indicatorPosition;

        /// <summary>
        ///     The left button down.
        /// </summary>
        private bool leftButtonDown;

        /// <summary>
        ///     The smoothly moving.
        /// </summary>
        private bool smoothlyMoving;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="OnOffSlider" /> class.
        /// </summary>
        /// <param name="onColor">
        ///     The on color.
        /// </param>
        /// <param name="offColor">
        ///     The off color.
        /// </param>
        /// <param name="height">
        ///     The height.
        /// </param>
        /// <param name="enabled">
        ///     The enabled.
        /// </param>
        public OnOffSlider(Color onColor, Color offColor, float height, bool enabled)
        {
            this.onColor = onColor;
            this.offColor = offColor;
            this.Height = height;
            this.transition = new CubicEaseInOut(0.30);
            this.hover = new QuadEaseInOut(0.20);
            this.leftButtonSleeper = new Sleeper();
            this.Enabled = enabled;
            this.char1 = new DrawText { FontFlags = FontFlags.AntiAlias, Text = "°", Color = new Color(50, 50, 50) };
            this.char2 = new DrawText { FontFlags = FontFlags.AntiAlias, Text = "°", Color = new Color(180, 180, 180) };
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the color.
        /// </summary>
        public Color Color
        {
            get
            {
                if (!this.hover.Moving)
                {
                    return new Color(195, 186, 173, 255) + (this.Hovered ? new Color(70, 70, 70) : new Color());
                }

                var value = this.hover.GetValue();
                if (!this.Hovered)
                {
                    value = 70 - value;
                }

                return new Color(195, 186, 173, 255) + new Color((int)value, (int)value, (int)value);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        ///     Gets or sets the height.
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether hovered.
        /// </summary>
        public bool Hovered { get; set; }

        /// <summary>
        ///     Gets or sets the circle position.
        /// </summary>
        public Vector2 IndicatorPosition
        {
            get
            {
                if (!this.smoothlyMoving)
                {
                    return this.indicatorPosition;
                }

                var movePosition = this.transition.GetPosition();
                if (this.indicatorPosition != movePosition && this.transition.Moving)
                {
                    return movePosition;
                }

                this.indicatorPosition = movePosition;
                this.smoothlyMoving = false;

                return movePosition;
            }

            set
            {
                if (!this.smoothlyMoving && value != this.indicatorPosition && !this.indicatorPosition.IsZero)
                {
                    this.smoothlyMoving = true;
                    this.transition.Start(new Vector2(this.indicatorPosition.X, this.indicatorPosition.Y), value);
                    this.indicatorPosition = value;
                    return;
                }

                if (!this.smoothlyMoving)
                {
                    this.indicatorPosition = value;
                }
            }
        }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        public Vector2 Position { get; set; }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the size inside.
        /// </summary>
        private Vector2 SizeInside
        {
            get
            {
                return new Vector2((float)(this.Height * 0.8), (float)(this.Width * 0.8));
            }
        }

        /// <summary>
        ///     Gets the width.
        /// </summary>
        private float Width
        {
            get
            {
                return this.Height / 2;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The draw.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        public void Draw(Vector2 mousePosition)
        {
            var wasHovered = this.Hovered;
            this.Hovered = Utils.IsUnderRectangle(
                mousePosition,
                this.Position.X,
                this.Position.Y,
                (float)(this.Height * 1.5),
                this.Width);

            if (!wasHovered && this.Hovered)
            {
                this.hover.Start(0, 70);
            }
            else if (wasHovered && !this.Hovered)
            {
                this.hover.Start(0, 70);
            }

            MenuUtils.RoundedRectangle(
                (int)this.Position.X,
                (int)this.Position.Y,
                (int)(this.Height * 1.5),
                (int)this.Width,
                (int)this.Width / 2,
                Color.Black);

            var circleSize = this.SizeInside.Y / 2.28;

            this.char1.TextSize = new Vector2((float)this.Height);
            var textCircleY = (float)(this.Position.Y - this.Height * 0.008 - this.char1.Size.Y * 0.015);
            this.char1.Position = new Vector2((float)(this.Position.X + this.Height * 0.08), textCircleY);
            this.char1.Color = this.onColor;
            this.char1.Draw();
            MenuUtils.RoundedRectangle(
                (float)(this.Position.X + this.Width / 2 - circleSize / 2),
                (int)(this.Position.Y + this.Width / 2 - circleSize / 2 + 1),
                (int)circleSize,
                (int)circleSize,
                (int)this.SizeInside.Y / 4,
                this.onColor);

            // Console.WriteLine((int)this.SizeInside.Y / 2);
            MenuUtils.RoundedRectangle(
                (float)(this.Position.X + this.Height * 1.25 - circleSize / 2),
                (int)(this.Position.Y + this.Width / 2 - circleSize / 2 + 1),
                (int)circleSize,
                (int)circleSize,
                (int)this.SizeInside.Y / 4,
                this.offColor);

            this.char1.Position = new Vector2((float)(this.Position.X + this.Height * 1.08), textCircleY);
            this.char1.Color = this.offColor;
            this.char1.Draw();

            this.IndicatorPosition = this.Enabled ? this.Position + new Vector2(this.Height / 2, 0) : this.Position;
            var baseCirclePosition =
                new Vector2(
                    (float)(this.IndicatorPosition.X + this.Height / 2 - this.SizeInside.X / 2),
                    this.IndicatorPosition.Y + this.Width / 2 - this.SizeInside.Y / 2);

            this.char2.TextSize = new Vector2((float)(this.Height * 1.655));
            textCircleY = (float)(this.IndicatorPosition.Y - this.Height * 0.008 - this.char2.Size.Y * 0.12);
            if (Math.Abs(baseCirclePosition.Y - this.Position.Y) < 2)
            {
                baseCirclePosition.Y += 1;

                // textCircleY += 1;
            }

            this.char2.Position = new Vector2((float)(this.IndicatorPosition.X - this.Height * 0.05), textCircleY);
            var sliderColor = new Color((int)(this.Color.R * 0.5), (int)(this.Color.G * 0.5), (int)(this.Color.B * 0.5));
            this.char2.Color = sliderColor;
            MenuUtils.RoundedRectangle(
                baseCirclePosition.X,
                (int)baseCirclePosition.Y,
                (int)this.SizeInside.X,
                (int)this.SizeInside.Y,
                (int)this.SizeInside.Y / 2,
                sliderColor);
            this.char2.Draw();
            this.char2.Position = new Vector2((float)(this.IndicatorPosition.X + this.Height * 0.5), textCircleY);
            this.char2.Draw();
        }

        /// <summary>
        ///     The left button down.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        public void LeftButtonDown(Vector2 mousePosition)
        {
            this.leftButtonDown = true;
            this.leftButtonSleeper.Sleep(200);

            // if (this.Hovered)
            // {

            // }
        }

        /// <summary>
        ///     The left button up.
        /// </summary>
        public void LeftButtonUp()
        {
            this.leftButtonDown = false;
            if (this.Hovered && this.leftButtonSleeper.Sleeping)
            {
                this.Enabled = !this.Enabled;
            }
        }

        #endregion
    }
}