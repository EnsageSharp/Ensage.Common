// <copyright file="OnOffCircleSlider.cs" company="EnsageSharp">
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
    using Ensage.Common.Objects;
    using Ensage.Common.Objects.UtilityObjects;

    using SharpDX;

    /// <summary>
    ///     The on off.
    /// </summary>
    public class OnOffCircleSlider
    {
        #region Fields

        /// <summary>
        ///     The color change.
        /// </summary>
        private readonly Transition colorChange;

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
        ///     The background color.
        /// </summary>
        private Color backgroundColor;

        /// <summary>
        ///     The enabled.
        /// </summary>
        private bool enabled;

        /// <summary>
        ///     The circle position.
        /// </summary>
        private Vector2 indicatorPosition;

        /// <summary>
        ///     The left button down.
        /// </summary>
        private bool leftButtonDown;

        /// <summary>
        ///     The off position.
        /// </summary>
        private Vector2 offPosition;

        /// <summary>
        ///     The on position.
        /// </summary>
        private Vector2 onPosition;

        /// <summary>
        ///     The smoothly moving.
        /// </summary>
        private bool smoothlyMoving;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="OnOffCircleSlider" /> class.
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
        public OnOffCircleSlider(Color onColor, Color offColor, float height, bool enabled)
        {
            this.onColor = onColor;
            this.offColor = offColor;
            this.Height = height;
            this.transition = new CubicEaseInOut(0.30);
            this.hover = new QuadEaseInOut(0.20);
            this.leftButtonSleeper = new Sleeper();
            this.enabled = enabled;
            this.colorChange = new QuadEaseInOut(0.35);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the background color.
        /// </summary>
        public Color BackgroundColor
        {
            get
            {
                if (!this.colorChange.Moving)
                {
                    var a = 120f;
                    if (!this.Enabled)
                    {
                        a = 120 - a;
                    }

                    this.backgroundColor = new Color(60, 60, 0, 255)
                                           + new Color((int)(a * 0.6), (int)(a * 0.25), this.Enabled ? 0 : 60);

                    return this.backgroundColor;
                }

                var value = this.colorChange.GetValue();
                if (!this.Enabled)
                {
                    value = 120 - value;
                }

                this.backgroundColor = new Color(60, 60, 0, 255)
                                       + new Color((int)(value * 0.55), (int)(value * 0.2), this.Enabled ? 0 : 60);

                return this.backgroundColor;
            }
        }

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
        public bool Enabled
        {
            get
            {
                return this.enabled;
            }

            set
            {
                if (this.enabled == value)
                {
                    return;
                }

                this.enabled = value;
                this.IndicatorPosition = this.enabled ? this.onPosition : this.offPosition;
            }
        }

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
                    if (this.indicatorPosition.IsZero)
                    {
                        var size = new Vector2((float)(this.Height * 1.4), (float)(this.Height / 1.35));
                        var bgpos = this.Position + new Vector2(0, this.Height / 2 - size.Y / 2);
                        var circleSize = new Vector2((float)(size.Y * 0.8));
                        this.onPosition = bgpos
                                          + new Vector2((float)(size.X * 0.97 - size.Y), size.Y / 2 - circleSize.Y / 2);
                        this.offPosition = bgpos + new Vector2((float)(size.X * 0.13), size.Y / 2 - circleSize.Y / 2);
                        this.indicatorPosition = this.enabled ? this.onPosition : this.offPosition;
                    }

                    return this.indicatorPosition;
                }

                var movePosition = this.transition.GetPosition();
                if (this.indicatorPosition != movePosition && this.transition.Moving)
                {
                    return new Vector2(movePosition.X, this.indicatorPosition.Y);
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
                (float)(this.Height * 1.7),
                this.Width);

            if (!wasHovered && this.Hovered)
            {
                this.hover.Start(0, 90);
            }
            else if (wasHovered && !this.Hovered)
            {
                this.hover.Start(0, 90);
            }

            var size = new Vector2((float)(this.Height * 1.4), (float)(this.Height / 1.35));
            var bgpos = this.Position + new Vector2(0, this.Height / 2 - size.Y / 2);
            Drawing.DrawRect(bgpos, size, Textures.GetTexture(Menu.Root.SelectedTheme.OnOffSliderBackground));
            var circleSize = new Vector2((float)(size.Y * 0.8));
            this.onPosition = bgpos + new Vector2((float)(size.X * 0.97 - size.Y), size.Y / 2 - circleSize.Y / 2);
            this.offPosition = bgpos + new Vector2((float)(size.X * 0.13), size.Y / 2 - circleSize.Y / 2);
            this.indicatorPosition = this.enabled ? this.onPosition : this.offPosition;
            Drawing.DrawRect(
                this.IndicatorPosition,
                circleSize,
                this.Enabled && !this.transition.Moving
                    ? Textures.GetTexture(Menu.Root.SelectedTheme.OnOffSliderEnabled)
                    : Textures.GetTexture(Menu.Root.SelectedTheme.OnOffSliderDisabled));
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