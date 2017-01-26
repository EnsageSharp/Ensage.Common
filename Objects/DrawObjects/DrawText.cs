// <copyright file="DrawText.cs" company="EnsageSharp">
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
namespace Ensage.Common.Objects.DrawObjects
{
    using Ensage.Common.Objects.UtilityObjects;

    using SharpDX;

    /// <summary>
    ///     The draw text.
    /// </summary>
    public class DrawText : DrawObject
    {
        #region Fields

        /// <summary>
        ///     The sleeper.
        /// </summary>
        private readonly Sleeper sleeper;

        private Vector2 position;

        /// <summary>
        ///     The shadow position.
        /// </summary>
        private Vector2 shadowPosition;

        private Vector2 shadowPosition2;

        /// <summary>
        ///     The text.
        /// </summary>
        private string text;

        /// <summary>
        ///     The text size.
        /// </summary>
        private Vector2 textSize;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DrawText" /> class.
        /// </summary>
        public DrawText()
        {
            this.sleeper = new Sleeper();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the color.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        ///     Gets or sets the font flags.
        /// </summary>
        public FontFlags FontFlags { get; set; }

        /// <summary>Gets or sets the position.</summary>
        public override Vector2 Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
                if (this.Shadow)
                {
                    this.shadowPosition = this.position + new Vector2(1, 1); // this.Size * (float)0.045);
                    this.shadowPosition2 = this.position - new Vector2(1, 1); // this.Size * (float)0.045);
                }
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether shadow.
        /// </summary>
        public bool Shadow { get; set; }

        public Color ShadowColor { get; set; } = Color.Black;

        /// <summary>
        ///     Gets or sets the text.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (this.text == value && this.Size != Vector2.Zero)
                {
                    return;
                }

                this.text = value;
                if (this.sleeper.Sleeping && this.Size != Vector2.Zero)
                {
                    return;
                }

                this.Size = Drawing.MeasureText(this.text, "Arial", this.textSize, this.FontFlags)
                            + (this.Shadow ? new Vector2(2) : Vector2.Zero);
            }
        }

        /// <summary>
        ///     Gets or sets the text size.
        /// </summary>
        public Vector2 TextSize
        {
            get
            {
                return this.textSize;
            }

            set
            {
                if (this.textSize == value && this.Size != Vector2.Zero)
                {
                    return;
                }

                this.textSize = value;
                this.Size = Drawing.MeasureText(this.text, "Arial", this.textSize, this.FontFlags)
                            + (this.Shadow ? new Vector2(2) : Vector2.Zero);
                this.sleeper.Sleep(200);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The draw.
        /// </summary>
        public override void Draw()
        {
            if (this.Shadow)
            {
                Drawing.DrawText(
                    this.Text,
                    this.position - new Vector2(1, 0),
                    this.textSize,
                    this.ShadowColor,
                    this.FontFlags);
                Drawing.DrawText(
                    this.Text,
                    this.position - new Vector2(0, 1),
                    this.textSize,
                    this.ShadowColor,
                    this.FontFlags);
                for (var i = 1; i <= this.textSize.X / 7; i++)
                {
                    Drawing.DrawText(
                        this.Text,
                        this.position + new Vector2(i, i / 2),
                        this.textSize,
                        this.ShadowColor,
                        this.FontFlags);
                    Drawing.DrawText(
                        this.Text,
                        this.position + new Vector2(i / 2, i),
                        this.textSize,
                        this.ShadowColor,
                        this.FontFlags);
                }

                // for (var i = 1; i <= this.textSize.X / 6; i++)
                // {
                // Drawing.DrawText(this.Text, this.position + new Vector2(0,i), this.textSize, this.ShadowColor, this.FontFlags);
                // }
                // Drawing.DrawText(this.Text, this.shadowPosition, this.textSize, this.ShadowColor, this.FontFlags);
                // Drawing.DrawText(this.Text, this.shadowPosition2, this.textSize, this.ShadowColor, this.FontFlags);
            }

            Drawing.DrawText(this.text, this.Position, this.textSize, this.Color, this.FontFlags);
        }

        #endregion
    }
}