// <copyright file="PriorityIcon.cs" company="EnsageSharp">
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
    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;

    /// <summary>
    ///     The priority icon.
    /// </summary>
    public class PriorityIcon
    {
        #region Fields

        /// <summary>
        ///     The priority number.
        /// </summary>
        private readonly DrawText priorityNumber;

        /// <summary>
        ///     The texture.
        /// </summary>
        private readonly DotaTexture texture;

        /// <summary>
        ///     The quad ease in out.
        /// </summary>
        private readonly QuadEaseInOut transition;

        /// <summary>
        ///     The position.
        /// </summary>
        private Vector2 position;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PriorityIcon" /> class.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <param name="height">
        ///     The height.
        /// </param>
        /// <param name="enabled">
        ///     The enabled.
        /// </param>
        public PriorityIcon(string name, float height, bool enabled)
        {
            this.texture = name.Substring(0, "item".Length) == "item"
                               ? Textures.GetTexture(
                                   "materials/ensage_ui/items/" + name.Substring("item_".Length) + ".vmat")
                               : Textures.GetTexture("materials/ensage_ui/spellicons/" + name + ".vmat");
            this.Height = height;
            this.Name = name;
            this.Item = this.Name.Contains("item");
            this.priorityNumber = new DrawText
                                      {
                                          FontFlags = FontFlags.AntiAlias, Color = Color.White,
                                          Position = this.Position + new Vector2(4, 3)
                                      };
            this.transition = new QuadEaseInOut(0.35);
            this.Hover = new QuadEaseInOut(0.35);
            this.Hover.Start(0, 40);
            this.Enabled = enabled;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the color.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        ///     Gets or sets the dictionary position.
        /// </summary>
        public uint DictionaryPosition { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        ///     Gets the fixed position.
        /// </summary>
        public Vector2 FixedPosition
        {
            get
            {
                return this.position;
            }
        }

        /// <summary>
        ///     Gets or sets the height.
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        ///     Gets or sets the hover.
        /// </summary>
        public QuadEaseInOut Hover { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether hovered.
        /// </summary>
        public bool Hovered { get; set; }

        /// <summary>
        ///     Gets or sets the icon position.
        /// </summary>
        public Vector2 IconPosition { get; set; }

        /// <summary>
        ///     Gets or sets the icon size.
        /// </summary>
        public Vector2 IconSize { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether item.
        /// </summary>
        public bool Item { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether moving.
        /// </summary>
        public bool Moving { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                if (!this.SmoothlyMoving)
                {
                    return this.position;
                }

                var movePosition = this.transition.GetPosition();
                if (this.position == movePosition || !this.transition.Moving)
                {
                    this.position = movePosition;
                    this.SmoothlyMoving = false;
                }

                return movePosition;
            }

            set
            {
                if (!this.SmoothlyMoving && value != this.position && !this.position.IsZero && !this.Moving)
                {
                    this.SmoothlyMoving = true;
                    this.transition.Start(new Vector2(this.position.X, this.position.Y), value);
                    this.position = value;
                    return;
                }

                if (!this.SmoothlyMoving)
                {
                    this.position = value;
                }
            }
        }

        /// <summary>
        ///     Gets or sets the priority.
        /// </summary>
        public uint Priority { get; set; }

        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        public Vector2 Size { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether smoothly moving.
        /// </summary>
        public bool SmoothlyMoving { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The draw.
        /// </summary>
        /// <param name="opacity">
        ///     The opacity.
        /// </param>
        public void Draw(float opacity = 0)
        {
            Drawing.DrawRect(this.Position, this.Size, this.Color - new Color(0, 0, 0, opacity));
            Drawing.DrawRect(this.Position, this.Size, Color.Black - new Color(0, 0, 0, opacity), true);
            var iconSize = this.IconSize;
            if (this.Item)
            {
                iconSize += new Vector2((float)(this.Height * 0.33) - 1, 0);
            }

            Drawing.DrawRect(this.IconPosition + new Vector2(2, 2), iconSize - new Vector2(3, 3), this.texture);
            Drawing.DrawRect(
                this.IconPosition + new Vector2(1, 1),
                this.IconSize - new Vector2(1, 1),
                Color.Black - new Color(0, 0, 0, opacity),
                true);
        }

        /// <summary>
        ///     The draw priority number.
        /// </summary>
        public void DrawPriorityNumber()
        {
            var move = this.Priority >= 10 ? (float)(this.IconSize.X * 0.04) : 0;
            this.priorityNumber.Text = this.Priority.ToString();
            this.priorityNumber.TextSize = new Vector2((float)(this.Height * 0.4), 100);
            this.priorityNumber.Position = this.Position + new Vector2(5 - move, 3);
            Drawing.DrawRect(this.priorityNumber.Position, this.priorityNumber.Size, Color.Black);
            this.priorityNumber.Draw();
        }

        /// <summary>
        ///     The return from drag.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        public void ReturnFromDrag(Vector2 position)
        {
            this.position = position;
        }

        #endregion
    }
}