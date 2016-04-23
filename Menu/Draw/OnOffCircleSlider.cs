namespace Ensage.Common.Menu.Draw
{
    using Ensage.Common.Menu.Transitions;
    using Ensage.Common.Objects.DrawObjects;
    using Ensage.Common.Objects.UtilityObjects;

    using SharpDX;

    /// <summary>
    ///     The on off.
    /// </summary>
    public class OnOffCircleSlider
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
        /// The background color.
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
            this.char1 = new DrawText { FontFlags = FontFlags.AntiAlias, Text = "°", Color = new Color(50, 50, 50) };
            this.char2 = new DrawText { FontFlags = FontFlags.AntiAlias, Text = "°", Color = new Color(180, 180, 180) };
            this.colorChange = new CircEaseOutIn(0.3);
            this.colorChange.Start(0, 120);
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
                                           + new Color((int)(a * 0.7), (int)(a * 0.25), this.Enabled ? 0 : 60);

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
                this.colorChange.Start(0, 180);
                this.enabled = value;
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

            this.char1.TextSize = new Vector2((float)(this.Height * 2.6));
            var textCircleY = (float)(this.Position.Y - this.Height * 0.008 - this.char1.Size.Y * 0.17);
            this.char1.Position = new Vector2((float)(this.Position.X + this.Height * 0.08), textCircleY);
            this.char1.Color = this.BackgroundColor;
            var move = (float)(this.Height * 0.1);
            this.char1.Draw();
            this.char1.Position = new Vector2(this.char1.Position.X + move, this.char1.Position.Y);
            this.char1.Draw();
            this.char1.Position = new Vector2(this.char1.Position.X + move, this.char1.Position.Y);
            this.char1.Draw();
            this.char1.Position = new Vector2(this.char1.Position.X + move, this.char1.Position.Y);
            this.char1.Draw();
            this.char1.Position = new Vector2(this.char1.Position.X + move, this.char1.Position.Y);
            this.char1.Draw();
            this.char1.Position = new Vector2(this.char1.Position.X + move, this.char1.Position.Y);
            this.char1.Draw();
            this.IndicatorPosition = this.Enabled
                                         ? this.Position + new Vector2((float)(this.Height * 0.61), 0)
                                         : this.Position + new Vector2((float)(this.Height * 0.15), 0);
            this.char2.TextSize = new Vector2((float)(this.Height * 2.2));
            textCircleY = (float)(this.IndicatorPosition.Y - this.Height * 0.008 - this.char2.Size.Y * 0.151);
            this.char2.Position = new Vector2((float)this.IndicatorPosition.X, textCircleY);
            var sliderColor = this.Color;
            this.char2.Color = sliderColor;
            MenuUtils.RoundedRectangle(
                (float)(this.IndicatorPosition.X + this.Height * 0.15), 
                (int)this.IndicatorPosition.Y, 
                (int)(this.Width * 0.9), 
                (int)(this.Width * 0.9), 
                (int)this.Width / 2, 
                sliderColor);
            this.char2.Draw();
            this.char2.Color = sliderColor;
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