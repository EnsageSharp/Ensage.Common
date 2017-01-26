// <copyright file="SideMessage.cs" company="EnsageSharp">
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
namespace Ensage.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SharpDX;

    /// <summary>
    ///     Provides side message notifications
    /// </summary>
    public class SideMessage
    {
        #region Static Fields

        private static bool mSideMessageInitialized;

        private static IDictionary<string, SideMessage> sideMessages;

        #endregion

        #region Fields

        /// <summary>
        ///     Position of side message
        /// </summary>
        public Vector2 MessagePosition;

        private readonly IDictionary<int, AMessageComponent> components;

        private int elements;

        #endregion

        #region Constructors and Destructors

        static SideMessage()
        {
            Initialize();
        }

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="bgColor"></param>
        /// <param name="bdColor"></param>
        /// <param name="enterTime"></param>
        /// <param name="stayTime"></param>
        /// <param name="exitTime"></param>
        public SideMessage(
            string name,
            Vector2 size,
            Color? bgColor = null,
            Color? bdColor = null,
            int? enterTime = null,
            int? stayTime = null,
            int? exitTime = null)
        {
            this.MessageName = name;
            this.Size = size;
            this.MessagePosition = new Vector2(Drawing.Width, (float)(Drawing.Height * 0.64));
            this.BackgroundColor = bgColor ?? new Color(0xC0111111);
            this.BackgroundOutlineColor = bdColor ?? new Color(0xFF444444);
            this.EnterTime = enterTime ?? 650;
            this.StayTime = stayTime ?? 2500;
            this.ExitTime = exitTime ?? 650;
            this.components = new Dictionary<int, AMessageComponent>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// </summary>
        public static int? LastTick { get; set; }

        /// <summary>
        /// </summary>
        public Color BackgroundColor { get; private set; }

        /// <summary>
        /// </summary>
        public Color BackgroundOutlineColor { get; private set; }

        /// <summary>
        /// </summary>
        public int CreateTick { get; set; }

        /// <summary>
        /// </summary>
        public int EnterTime { get; private set; }

        /// <summary>
        /// </summary>
        public int ExitTime { get; private set; }

        /// <summary>
        /// </summary>
        public string MessageName { get; set; }

        /// <summary>
        /// </summary>
        public Vector2 Size { get; set; }

        /// <summary>
        /// </summary>
        public int StayTime { get; private set; }

        /// <summary>
        /// </summary>
        public bool Visible { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        public static void Initialize()
        {
            if (mSideMessageInitialized)
            {
                return;
            }

            sideMessages = new Dictionary<string, SideMessage>();
            mSideMessageInitialized = true;
            Drawing.OnDraw += OnDraw;
        }

        /// <summary>
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        /// <param name="outline"></param>
        public void AddElement(Vector2 position, Vector2 size, Color color, bool outline = false)
        {
            var element = new AMessageComponent(position, size, color, outline)
                              {
                                 Parent = this, ComponentType = "DrawRect_Color" 
                              };
            this.components.Add(this.elements, element);
            this.elements++;
        }

        /// <summary>
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="texture"></param>
        public void AddElement(Vector2 position, Vector2 size, DotaTexture texture)
        {
            var element = new AMessageComponent(position, size, texture)
                              {
                                 Parent = this, ComponentType = "DrawRect_Texture" 
                              };
            this.components.Add(this.elements, element);
            this.elements++;
        }

        /// <summary>
        /// </summary>
        /// <param name="text"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="fontFlags"></param>
        public void AddElement(string text, Vector2 position, Color color, FontFlags fontFlags)
        {
            var element = new AMessageComponent(text, position, color, fontFlags)
                              {
                                 Parent = this, ComponentType = "DrawText" 
                              };
            this.components.Add(this.elements, element);
            this.elements++;
        }

        /// <summary>
        /// </summary>
        /// <param name="text"></param>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        /// <param name="fontFlags"></param>
        public void AddElement(string text, Vector2 position, Vector2 size, Color color, FontFlags fontFlags)
        {
            var element = new AMessageComponent(text, position, size, color, fontFlags)
                              {
                                 Parent = this, ComponentType = "DrawText_Size" 
                              };
            this.components.Add(this.elements, element);
            this.elements++;
        }

        /// <summary>
        /// </summary>
        /// <param name="text"></param>
        /// <param name="fontName"></param>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        /// <param name="fontFlags"></param>
        public void AddElement(
            string text,
            string fontName,
            Vector2 position,
            Vector2 size,
            Color color,
            FontFlags fontFlags)
        {
            var element = new AMessageComponent(text, fontName, position, size, color, fontFlags)
                              {
                                 Parent = this, ComponentType = "DrawText_Font" 
                              };
            this.components.Add(this.elements, element);
            this.elements++;
        }

        /// <summary>
        /// </summary>
        /// <param name="position1"></param>
        /// <param name="position2"></param>
        /// <param name="color"></param>
        public void AddElement(Vector2 position1, Vector2 position2, Color color)
        {
            var element = new AMessageComponent(position1, position2, color)
                              {
                                 Parent = this, ComponentType = "DrawLine" 
                              };
            this.components.Add(this.elements, element);
            this.elements++;
        }

        /// <summary>
        /// </summary>
        public void CreateMessage()
        {
            this.CreateTick = (int)Utils.TickCount;
            foreach (var message in sideMessages.Where(message => message.Value.Visible))
            {
                message.Value.ShiftVec(new Vector2(0, -message.Value.Size.Y - 3));
            }

            this.Visible = true;
            sideMessages[this.MessageName] = this;
        }

        /// <summary>
        /// </summary>
        public void DestroyMessage()
        {
            this.Visible = false;
            this.components.Clear();
            sideMessages.Remove(this.MessageName);
        }

        #endregion

        #region Methods

        private static void OnDraw(EventArgs args)
        {
            if (!Game.IsInGame || !sideMessages.Any())
            {
                return;
            }

            if (LastTick != null)
            {
                for (var i = 0; i < sideMessages.Count; i++)
                {
                    var message = sideMessages.ElementAt(i).Value;
                    if (!message.Visible)
                    {
                        continue;
                    }

                    var span = Utils.TickCount - message.CreateTick;
                    if (span < message.EnterTime)
                    {
                        message.SetX(Drawing.Width - (message.Size.X - 1) * span / message.EnterTime);
                    }
                    else if (span < message.EnterTime + message.StayTime)
                    {
                        message.SetX(Drawing.Width - message.Size.X + 1);
                    }
                    else if (span < message.EnterTime + message.StayTime + message.ExitTime)
                    {
                        message.SetX(
                            Drawing.Width
                            - (message.Size.X - 1) * (message.EnterTime + message.StayTime + message.ExitTime - span)
                            / message.ExitTime);
                    }
                    else
                    {
                        message.DestroyMessage();
                    }
                }

                for (var i = 0; i < sideMessages.Count; i++)
                {
                    var message = sideMessages.ElementAt(i).Value;
                    if (!message.Visible)
                    {
                        continue;
                    }

                    Drawing.DrawRect(message.MessagePosition, message.Size, message.BackgroundColor);
                    Drawing.DrawRect(message.MessagePosition, message.Size, message.BackgroundOutlineColor, true);
                    foreach (var component in message.components)
                    {
                        component.Value.Draw();
                    }
                }
            }

            LastTick = (int?)Utils.TickCount;
        }

        private void SetX(float x)
        {
            this.MessagePosition.X = x;
        }

        private void ShiftVec(Vector2 vector)
        {
            this.MessagePosition += vector;
        }

        #endregion

        /// <summary>
        /// </summary>
        public class AMessageComponent
        {
            #region Constructors and Destructors

            /// <summary>
            /// </summary>
            /// <param name="position"></param>
            /// <param name="size"></param>
            /// <param name="color"></param>
            /// <param name="outline"></param>
            public AMessageComponent(Vector2 position, Vector2 size, Color color, bool outline = false)
            {
                this.Position = position;
                this.Size = size;
                this.Color = color;
                this.IsOutline = outline;
            }

            /// <summary>
            /// </summary>
            /// <param name="position"></param>
            /// <param name="size"></param>
            /// <param name="texture"></param>
            public AMessageComponent(Vector2 position, Vector2 size, DotaTexture texture)
            {
                this.Position = position;
                this.Size = size;
                this.Texture = texture;
            }

            /// <summary>
            /// </summary>
            /// <param name="text"></param>
            /// <param name="position"></param>
            /// <param name="color"></param>
            /// <param name="fontFlags"></param>
            public AMessageComponent(string text, Vector2 position, Color color, FontFlags fontFlags)
            {
                this.Text = text;
                this.Position = position;
                this.Color = color;
                this.Flags = fontFlags;
            }

            /// <summary>
            /// </summary>
            /// <param name="text"></param>
            /// <param name="position"></param>
            /// <param name="size"></param>
            /// <param name="color"></param>
            /// <param name="fontFlags"></param>
            public AMessageComponent(string text, Vector2 position, Vector2 size, Color color, FontFlags fontFlags)
            {
                this.Text = text;
                this.Position = position;
                this.Size = size;
                this.Color = color;
                this.Flags = fontFlags;
            }

            /// <summary>
            /// </summary>
            /// <param name="text"></param>
            /// <param name="fontName"></param>
            /// <param name="position"></param>
            /// <param name="size"></param>
            /// <param name="color"></param>
            /// <param name="fontFlags"></param>
            public AMessageComponent(
                string text,
                string fontName,
                Vector2 position,
                Vector2 size,
                Color color,
                FontFlags fontFlags)
            {
                this.Text = text;
                this.Position = position;
                this.FontFace = fontName;
                this.Size = size;
                this.Color = color;
                this.Flags = fontFlags;
            }

            /// <summary>
            /// </summary>
            /// <param name="position"></param>
            /// <param name="size"></param>
            /// <param name="color"></param>
            public AMessageComponent(Vector2 position, Vector2 size, Color color)
            {
                this.Position = position;
                this.Size = size;
                this.Color = color;
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// </summary>
            public string ComponentType { get; set; }

            /// <summary>
            /// </summary>
            public SideMessage Parent { get; set; }

            #endregion

            #region Properties

            private Color Color { get; set; }

            private FontFlags Flags { get; set; }

            private string FontFace { get; set; }

            private bool IsOutline { get; set; }

            private Vector2 Position { get; set; }

            private Vector2 Size { get; set; }

            private string Text { get; set; }

            private DotaTexture Texture { get; set; }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            /// </summary>
            public void Draw()
            {
                // Console.Write("Called " + Texture + "; ");
                if (this.ComponentType.Equals("DrawRect_Color"))
                {
                    Drawing.DrawRect(this.Parent.MessagePosition + this.Position, this.Size, this.Color, this.IsOutline);
                }
                else if (this.ComponentType.Equals("DrawRect_Texture"))
                {
                    Drawing.DrawRect(this.Parent.MessagePosition + this.Position, this.Size, this.Texture);
                }
                else if (this.ComponentType.Equals("DrawText"))
                {
                    Drawing.DrawText(this.Text, this.Parent.MessagePosition + this.Position, this.Color, this.Flags);
                }
                else if (this.ComponentType.Equals("DrawText_Size"))
                {
                    Drawing.DrawText(
                        this.Text,
                        this.Parent.MessagePosition + this.Position,
                        this.Size,
                        this.Color,
                        this.Flags);
                }
                else if (this.ComponentType.Equals("DrawText_Font"))
                {
                    Drawing.DrawText(
                        this.Text,
                        this.FontFace,
                        this.Parent.MessagePosition + this.Position,
                        this.Size,
                        this.Color,
                        this.Flags);
                }
                else if (this.ComponentType.Equals("DrawLine"))
                {
                    Drawing.DrawLine(
                        this.Parent.MessagePosition + this.Position,
                        this.Parent.MessagePosition + this.Size,
                        this.Color);
                }
            }

            #endregion
        }
    }
}