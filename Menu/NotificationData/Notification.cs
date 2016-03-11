#region LICENSE

/*
 Copyright 2014 - 2014 LeagueSharp
 Notification.cs is part of LeagueSharp.Common.
 
 LeagueSharp.Common is free software: you can redistribute it and/or modify
 it under the terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 (at your option) any later version.
 
 LeagueSharp.Common is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 GNU General Public License for more details.
 
 You should have received a copy of the GNU General Public License
 along with LeagueSharp.Common. If not, see <http://www.gnu.org/licenses/>.
*/
#endregion

namespace Ensage.Common.Menu.NotificationData
{
    using System;
    using System.IO;

    using Ensage.Common.Extensions;

    using SharpDX;
    using SharpDX.Direct3D9;

    using Color = System.Drawing.Color;

    /// <summary>
    ///     Basic Notification
    /// </summary>
    public class Notification : INotification, IDisposable
    {
        #region Fields

        /// <summary>
        ///     Notification's Border Color
        /// </summary>
        public ColorBGRA BorderColor = new ColorBGRA(255f, 255f, 255f, 255f);

        /// <summary>
        ///     Notification's Box Color
        /// </summary>
        public ColorBGRA BoxColor = new ColorBGRA(0f, 0f, 0f, 255f);

        /// <summary>
        ///     Notification's Font
        /// </summary>
        public Font Font = new Font(
            Drawing.Direct3DDevice9, 
            0xE, 
            0x0, 
            FontWeight.DoNotCare, 
            0x0, 
            false, 
            FontCharacterSet.Default, 
            FontPrecision.Default, 
            FontQuality.Antialiased, 
            FontPitchAndFamily.DontCare | FontPitchAndFamily.Decorative, 
            "Tahoma");

        /// <summary>
        ///     Notification's Text
        /// </summary>
        public string Text;

        /// <summary>
        ///     Notification's Text Color
        /// </summary>
        public ColorBGRA TextColor = new ColorBGRA(255f, 255f, 255f, 255f);

        /// <summary>
        ///     Locally saved bool which indicates if notification will be disposed after finishing
        /// </summary>
        private readonly bool autoDispose;

        /// <summary>
        ///     Locally saved bytes which contain old ALPHA values
        /// </summary>
        private readonly byte[] flashingBytes = new byte[3];

        /// <summary>
        ///     Locally saved Global Unique Identification (GUID)
        /// </summary>
        private readonly string id;

        /// <summary>
        ///     Locally saved Line
        /// </summary>
        private readonly Line line = new Line(Drawing.Direct3DDevice9)
                                         {
                                            Antialias = false, GLLines = true, Width = 190f 
                                         };

        /// <summary>
        ///     Locally saved Sprite
        /// </summary>
        private readonly Sprite sprite = new Sprite(Drawing.Direct3DDevice9);

        /// <summary>
        ///     Locally saved bool which indicates if border should be drawn
        /// </summary>
        private bool border;

        /// <summary>
        ///     Locally saved int which contains data of the last tick.
        /// </summary>
        private int clickTick;

        /// <summary>
        ///     Locally saved value, indicating when next decreasment tick should happen.
        /// </summary>
        private int decreasementTick;

        /// <summary>
        ///     Locally saved Notification's Duration
        /// </summary>
        private int duration;

        /// <summary>
        ///     Locally saved bool which indicates if flashing mode is on or off.
        /// </summary>
        private bool flashing;

        /// <summary>
        ///     Locally saved int which contains an internval for flash mode
        /// </summary>
        private int flashInterval;

        /// <summary>
        ///     Locally saved int which contains next flash mode tick
        /// </summary>
        private int flashTick;

        /// <summary>
        ///     Locally saved handler for FileStream.
        /// </summary>
        private FileStream handler;

        /// <summary>
        ///     Locally saved position
        /// </summary>
        private Vector2 position;

        /// <summary>
        ///     Locally saved Notification State
        /// </summary>
        private NotificationState state;

        /// <summary>
        ///     Locally saved boolean for Text Fix
        /// </summary>
        private Vector2 textFix;

        /// <summary>
        ///     Locally saved update position
        /// </summary>
        private Vector2 updatePosition;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Notification Constructor
        /// </summary>
        /// <param name="text">Display Text</param>
        /// <param name="duration">Duration (-1 for Infinite)</param>
        /// <param name="dispose">Auto Dispose after notification duration end</param>
        public Notification(string text, int duration = -0x1, bool dispose = false)
        {
            // Setting GUID
            this.id = Guid.NewGuid().ToString("N");

            // Setting main values
            this.Text = text;
            this.state = NotificationState.Idle;
            this.border = true;
            this.autoDispose = dispose;

            // Preload Text
            this.Font.PreloadText(text);

            // Calling Show
            this.Show(duration);
        }

        /// <summary>
        ///     Finalization
        /// </summary>
        ~Notification()
        {
            this.Dispose(false);
        }

        #endregion

        #region Enums

        /// <summary>
        /// </summary>
        public enum NotificationState
        {
            /// <summary>
            /// </summary>
            Idle, 

            /// <summary>
            /// </summary>
            AnimationMove, 

            /// <summary>
            /// </summary>
            AnimationShowShrink, 

            /// <summary>
            /// </summary>
            AnimationShowMove, 

            /// <summary>
            /// </summary>
            AnimationShowGrow
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Indicates if notification should be drawn
        /// </summary>
        public bool Draw { get; set; }

        /// <summary>
        ///     Indicates if notification should be updated
        /// </summary>
        public bool Update { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Toggles the notification border
        /// </summary>
        public Notification Border()
        {
            this.border = !this.border;

            return this;
        }

        /// <summary>
        ///     Sets the notification border toggle value
        /// </summary>
        /// <param name="value">bool value</param>
        public Notification Border(bool value)
        {
            this.border = value;

            return this;
        }

        /// <summary>
        ///     IDisposable callback
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        ///     Enters Notification's flashing mode
        /// </summary>
        /// <param name="interval">Flash Interval</param>
        public Notification Flash(int interval = 0xFA)
        {
            this.flashing = !this.flashing;
            if (this.flashing)
            {
                this.flashInterval = interval;
            }

            return this;
        }

        /// <summary>
        ///     Returns the notification's global unique identification (GUID)
        /// </summary>
        /// <returns>GUID</returns>
        public string GetId()
        {
            return this.id;
        }

        /// <summary>
        ///     Called for Drawing onto screen
        /// </summary>
        public void OnDraw()
        {
            if (!this.Draw)
            {
                return;
            }

            this.line.Begin();

            var vertices = new[]
                               {
                                   new Vector2(this.position.X + this.line.Width / 0x2, this.position.Y), 
                                   new Vector2(this.position.X + this.line.Width / 0x2, this.position.Y + 25f)
                               };

            this.line.Draw(vertices, this.BoxColor);
            this.line.End();

            if (this.border)
            {
                var x = this.position.X;
                var y = this.position.Y;
                var w = this.line.Width;
                const float h = 25f;
                const float px = 1f;

                this.line.Begin();
                this.line.Draw(GetBorder(x, y, w, px), this.BorderColor); // TOP
                this.line.End();

                var oWidth = this.line.Width;
                this.line.Width = px;

                this.line.Begin();
                this.line.Draw(GetBorder(x, y, px, h), this.BorderColor); // LEFT
                this.line.Draw(GetBorder(x + w, y, 1, h), this.BorderColor); // RIGHT
                this.line.End();

                this.line.Width = oWidth;

                this.line.Begin();
                this.line.Draw(GetBorder(x, y + h, w, 1), this.BorderColor); // BOTTOM
                this.line.End();
            }

            this.sprite.Begin();

            var textDimension = this.Font.MeasureText(this.sprite, this.Text, FontDrawFlags.Center);
            var finalText = this.Text;

            if (textDimension.Width + 0x5 > this.line.Width)
            {
                for (var i = this.Text.Length; i > 0x0; --i)
                {
                    var text = this.Text.Substring(0x0, i);
                    var textWidth = this.Font.MeasureText(this.sprite, text, FontDrawFlags.Center).Width;

                    if (textWidth + 0x5 > this.line.Width)
                    {
                        continue;
                    }

                    finalText = text == this.Text ? text : text.Substring(0x0, text.Length - 0x3) + "...";
                    break;
                }
            }

            textDimension = this.Font.MeasureText(this.sprite, finalText, FontDrawFlags.Center);

            var rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, (int)this.line.Width, 0x19);

            this.Font.DrawText(
                this.sprite, 
                finalText, 
                rectangle.TopLeft.X + (rectangle.Width - textDimension.Width) / 0x2, 
                rectangle.TopLeft.Y + (rectangle.Height - textDimension.Height) / 0x2, 
                this.TextColor);

            this.sprite.End();
        }

        /// <summary>
        /// </summary>
        public void OnPostReset()
        {
            this.line.OnResetDevice();
            this.Font.OnResetDevice();
            this.sprite.OnResetDevice();
        }

        /// <summary>
        /// </summary>
        public void OnPreReset()
        {
            this.Font.OnLostDevice();
            this.line.OnLostDevice();
            this.sprite.OnLostDevice();
        }

        /// <summary>
        ///     Called per game tick for update
        /// </summary>
        public void OnUpdate()
        {
            if (!this.Update)
            {
                return;
            }

            switch (this.state)
            {
                case NotificationState.Idle:
                    {
                        if (!this.flashing && this.duration > 0x0 && this.TextColor.A == 0x0 && this.BoxColor.A == 0x0
                            && this.BorderColor.A == 0x0)
                        {
                            this.Update = this.Draw = false;
                            if (this.autoDispose)
                            {
                                this.Dispose();
                            }

                            Notifications.Free(this.handler);

                            return;
                        }

                        if (!this.flashing && this.duration > 0x0
                            && (Environment.TickCount & int.MaxValue) - this.decreasementTick > 0x0)
                        {
                            if (this.TextColor.A > 0x0)
                            {
                                this.TextColor.A--;
                            }

                            if (this.BoxColor.A > 0x0)
                            {
                                this.BoxColor.A--;
                            }

                            if (this.BorderColor.A > 0x0)
                            {
                                this.BorderColor.A--;
                            }

                            this.decreasementTick = this.GetNextDecreasementTick();
                        }

                        if (this.flashing)
                        {
                            if ((Environment.TickCount & int.MaxValue) - this.flashTick > 0x0)
                            {
                                if (this.TextColor.A > 0x0 && this.BoxColor.A > 0x0 && this.BorderColor.A > 0x0)
                                {
                                    if (this.duration > 0x0)
                                    {
                                        if (this.TextColor.A == 0x0 && this.BoxColor.A == 0x0
                                            && this.BorderColor.A == 0x0)
                                        {
                                            this.Update = this.Draw = false;
                                            if (this.autoDispose)
                                            {
                                                this.Dispose();
                                            }

                                            Notifications.Free(this.handler);

                                            return;
                                        }
                                    }

                                    this.flashingBytes[0x0] = --this.TextColor.A;
                                    this.flashingBytes[0x1] = --this.BoxColor.A;
                                    this.flashingBytes[0x2] = --this.BorderColor.A;

                                    this.TextColor.A = 0x0;
                                    this.BoxColor.A = 0x0;
                                    this.BorderColor.A = 0x0;
                                }
                                else
                                {
                                    this.TextColor.A = this.flashingBytes[0x0];
                                    this.BoxColor.A = this.flashingBytes[0x1];
                                    this.BorderColor.A = this.flashingBytes[0x2];

                                    if (this.TextColor.A > 0x0)
                                    {
                                        this.TextColor.A--;
                                    }

                                    if (this.BoxColor.A > 0x0)
                                    {
                                        this.BoxColor.A--;
                                    }

                                    if (this.BorderColor.A > 0x0)
                                    {
                                        this.BorderColor.A--;
                                    }

                                    if (this.duration > 0x0)
                                    {
                                        if (this.TextColor.A == 0x0 && this.BoxColor.A == 0x0
                                            && this.BorderColor.A == 0x0)
                                        {
                                            this.Update = this.Draw = false;
                                            if (this.autoDispose)
                                            {
                                                this.Dispose();
                                            }

                                            Notifications.Free(this.handler);

                                            return;
                                        }
                                    }
                                }

                                this.flashTick = (Environment.TickCount & int.MaxValue) + this.flashInterval;
                            }
                        }

                        

                        var mouseLocation = Game.MouseScreenPosition;
                        if (Utils.IsUnderRectangle(
                            mouseLocation, 
                            this.position.X, 
                            this.position.Y, 
                            this.line.Width, 
                            25f))
                        {
                            this.TextColor.A = 0xFF;
                            this.BoxColor.A = 0xFF;
                            this.BorderColor.A = 0xFF;

                            var textDimension = this.Font.MeasureText(this.sprite, this.Text, FontDrawFlags.Center);
                            if (textDimension.Width + 0x10 > this.line.Width)
                            {
                                var extra = textDimension.Width - 0xB4;
                                if (this.updatePosition == Vector2.Zero)
                                {
                                    this.textFix = new Vector2(this.position.X, this.position.Y);
                                    this.updatePosition = new Vector2(this.position.X - extra, this.position.Y);
                                }

                                if (this.updatePosition != Vector2.Zero && this.position.X > this.updatePosition.X)
                                {
                                    this.position.X -= 1f;
                                    this.line.Width += 1f;
                                }
                            }
                        }
                        else if (this.updatePosition != Vector2.Zero)
                        {
                            if (this.position.X < this.textFix.X)
                            {
                                this.position.X += 1f;
                                this.line.Width -= 1f;
                            }
                            else
                            {
                                this.textFix = Vector2.Zero;
                                this.updatePosition = Vector2.Zero;
                            }
                        }

                        

                        #region Movement

                        var location = Notifications.GetLocation();
                        if (location != -0x1 && this.position.Y > location)
                        {
                            if (Notifications.IsFirst((int)this.position.Y))
                            {
                                this.handler = Notifications.Reserve(this.GetId(), this.handler);
                                if (this.handler != null)
                                {
                                    if (this.updatePosition != Vector2.Zero && this.textFix != Vector2.Zero)
                                    {
                                        this.position.X = this.textFix.X;
                                        this.textFix = Vector2.Zero;
                                        this.line.Width = 190f;
                                    }

                                    this.updatePosition = new Vector2(
                                        this.position.X, 
                                        Notifications.GetLocation(this.handler));
                                    this.state = NotificationState.AnimationMove;
                                }
                            }
                        }

                        #endregion

                        break;
                    }

                case NotificationState.AnimationMove:
                    {
                        if (Math.Abs(this.position.Y - this.updatePosition.Y) > float.Epsilon)
                        {
                            var value = this.updatePosition.Distance(
                                new Vector2(this.position.X, this.position.Y - 0x1))
                                        < this.updatePosition.Distance(
                                            new Vector2(this.position.X, this.position.Y + 0x1))
                                            ? -0x1
                                            : 0x1;
                            this.position.Y += value;
                        }
                        else
                        {
                            this.updatePosition = Vector2.Zero;
                            this.state = NotificationState.Idle;
                        }

                        break;
                    }

                case NotificationState.AnimationShowShrink:
                    {
                        if (Math.Abs(this.line.Width - 0xB9) < float.Epsilon)
                        {
                            this.handler = Notifications.Reserve(this.GetId(), this.handler);
                            if (this.handler != null)
                            {
                                this.state = NotificationState.AnimationShowMove;
                                this.updatePosition = new Vector2(
                                    this.position.X, 
                                    Notifications.GetLocation(this.handler));
                            }

                            return;
                        }

                        this.line.Width--;
                        this.position.X++;

                        break;
                    }

                case NotificationState.AnimationShowMove:
                    {
                        if (Math.Abs(Notifications.GetLocation() + 0x1E - this.updatePosition.Y) < float.Epsilon)
                        {
                            this.updatePosition.Y = Notifications.GetLocation();
                        }

                        if (Math.Abs(this.position.Y - this.updatePosition.Y) > float.Epsilon)
                        {
                            var value =
                                this.updatePosition.Distance(new Vector2(this.position.X, this.position.Y - 0.5f))
                                < this.updatePosition.Distance(new Vector2(this.position.X, this.position.Y + 0.5f))
                                    ? -0.5f
                                    : 0.5f;
                            this.position.Y += value;
                        }
                        else
                        {
                            this.updatePosition = Vector2.Zero;
                            this.state = NotificationState.AnimationShowGrow;
                        }

                        break;
                    }

                case NotificationState.AnimationShowGrow:
                    {
                        if (Math.Abs(this.line.Width - 0xBE) < float.Epsilon)
                        {
                            this.state = NotificationState.Idle;
                            return;
                        }

                        this.line.Width++;
                        this.position.X--;

                        break;
                    }
            }
        }

        /// <summary>
        ///     Called per Windows Message.
        /// </summary>
        /// <param name="args">WndEventArgs</param>
        public void OnWndProc(WndEventArgs args)
        {
            if (Utils.IsUnderRectangle(Game.MouseScreenPosition, this.position.X, this.position.Y, this.line.Width, 25f))
            {
                var message = (Utils.WindowsMessages)args.Msg;
                if (message == Utils.WindowsMessages.WM_LBUTTONDOWN)
                {
                    if ((Environment.TickCount & int.MaxValue) - this.clickTick < 0x5DC)
                    {
                        this.clickTick = Environment.TickCount & int.MaxValue;

                        Notifications.Free(this.handler);

                        this.Draw = this.Update = false;
                        if (this.autoDispose)
                        {
                            this.Dispose();
                        }

                        return;
                    }

                    this.clickTick = Environment.TickCount & int.MaxValue;
                }
            }
        }

        /// <summary>
        ///     Sets the notification border color
        /// </summary>
        /// <param name="color">System Drawing Color</param>
        public Notification SetBorderColor(Color color)
        {
            this.BorderColor = new ColorBGRA(color.R, color.G, color.B, color.A);

            return this;
        }

        /// <summary>
        ///     Sets the notification box color
        /// </summary>
        /// <param name="color">System Drawing Color</param>
        public Notification SetBoxColor(Color color)
        {
            this.BoxColor = new ColorBGRA(color.R, color.G, color.B, color.A);

            return this;
        }

        /// <summary>
        ///     Sets the notification text color
        /// </summary>
        /// <param name="color">System Drawing Color</param>
        public Notification SetTextColor(Color color)
        {
            this.TextColor = new ColorBGRA(color.R, color.G, color.B, color.A);

            return this;
        }

        /// <summary>
        ///     Show an inactive Notification, returns boolean if successful or not.
        /// </summary>
        /// <param name="newDuration">Duration (-1 for Infinite)</param>
        /// <returns></returns>
        public bool Show(int newDuration = -0x1)
        {
            if (this.Draw || this.Update)
            {
                this.state = NotificationState.AnimationShowShrink;
                return false;
            }

            this.handler = Notifications.Reserve(this.GetId(), this.handler);
            if (this.handler != null)
            {
                this.duration = newDuration;

                this.TextColor.A = 0xFF;
                this.BoxColor.A = 0xFF;
                this.BorderColor.A = 0xFF;

                this.position = new Vector2(Drawing.Width - 200f, Notifications.GetLocation(this.handler));

                this.decreasementTick = this.GetNextDecreasementTick();

                return this.Draw = this.Update = true;
            }

            return false;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Calculate the border into vertices
        /// </summary>
        /// <param name="x">X axis</param>
        /// <param name="y">Y axis</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <returns>Vector2 Array</returns>
        private static Vector2[] GetBorder(float x, float y, float w, float h)
        {
            return new[] { new Vector2(x + w / 0x2, y), new Vector2(x + w / 0x2, y + h) };
        }

        /// <summary>
        ///     Safe disposal callback
        /// </summary>
        /// <param name="safe">Is Pre-Finailized / Safe (values not cleared by GC)</param>
        private void Dispose(bool safe)
        {
            if (Notifications.IsValidNotification(this))
            {
                Notifications.RemoveNotification(this);
            }

            if (safe)
            {
                this.Text = null;

                this.TextColor = new ColorBGRA();
                this.BoxColor = new ColorBGRA();
                this.BorderColor = new ColorBGRA();

                this.Font.Dispose();
                this.Font = null;

                this.line.Dispose();
                this.sprite.Dispose();
                this.Draw = false;
                this.Update = false;

                this.duration = 0;

                if (this.handler != null)
                {
                    Notifications.Free(this.handler);
                }

                this.position = Vector2.Zero;
                this.updatePosition = Vector2.Zero;

                this.state = 0;
                this.decreasementTick = 0;

                this.textFix = Vector2.Zero;

                this.flashing = false;
                this.flashInterval = 0;
                this.flashTick = 0;
                this.clickTick = 0;

                this.border = false;
            }
        }

        /// <summary>
        ///     Calculate the next decreasement tick.
        /// </summary>
        /// <returns>Decreasement Tick</returns>
        private int GetNextDecreasementTick()
        {
            return (Environment.TickCount & int.MaxValue) + (this.duration / 0xFF);
        }

        #endregion
    }
}