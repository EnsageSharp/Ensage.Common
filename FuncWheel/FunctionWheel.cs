namespace Ensage.Common.FuncWheel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Ensage.Common.Extensions;
    using Ensage.Common.Threading;

    using SharpDX;

    /// <summary>
    ///     The function wheel. Read the ctor for more infos about it.
    /// </summary>
    public class FunctionWheel : List<IWheelEntry>, IDisposable
    {
        #region Constants

        private const uint WM_KEYDOWN = 0x0100;

        private const uint WM_KEYUP = 0x0101;

        private const uint WM_SYSKEYDOWN = 0x0104;

        private const uint WM_SYSKEYUP = 0x0105;

        #endregion

        #region Static Fields

        private static readonly DotaTexture ArrowD;

        private static readonly DotaTexture ArrowL;

        private static readonly DotaTexture ArrowLD;

        private static readonly DotaTexture ArrowLU;

        private static readonly Vector2[] ArrowPos4 =
            {
                new Vector2(30, -25), // Up
                new Vector2(80, 25), // Right
                new Vector2(30, 80), // Down
                new Vector2(-20, 25) // Left
            };

        private static readonly Vector2[] ArrowPos8 =
            {
                new Vector2(30, -25), // Up
                new Vector2(64, -5), // Up Right
                new Vector2(80, 25), // Right
                new Vector2(64, 60), // Down Right
                new Vector2(30, 80), // Down
                new Vector2(-4, 60), // Down Left
                new Vector2(-20, 25), // Left
                new Vector2(-4, -5) // UpLeft
            };

        private static readonly DotaTexture ArrowR;

        private static readonly DotaTexture ArrowRD;

        private static readonly DotaTexture[] ArrowTextures4;

        private static readonly DotaTexture[] ArrowTextures8;

        private static readonly DotaTexture ArrowU;

        private static readonly DotaTexture ArrowUR;

        private static readonly DotaTexture Circle;

        private static readonly DotaTexture CircleBg;

        private static readonly DotaTexture CircleBg2;

        private static readonly DotaTexture CirclePtr;

        private static readonly Vector2 CircleSize = new Vector2(100, 100);

        private static readonly DotaTexture Cursor;

        private static readonly Vector2[] TextPos4 =
            {
                new Vector2(50, -50), // Up
                new Vector2(115, 30), // Right
                new Vector2(50, 115), // Down
                new Vector2(-15, 30) // Left
            };

        private static readonly Vector2[] TextPos8 =
            {
                new Vector2(50, -60), // Up
                new Vector2(105, -20), // Up Right
                new Vector2(130, 30), // Right
                new Vector2(105, 80), // Down Right
                new Vector2(50, 125), // Down
                new Vector2(0, 80), // Down Left
                new Vector2(-25, 30), // Left
                new Vector2(0, -20) // UpLeft
            };

        private static readonly Vector2 TextSize = new Vector2(24, 200);

        #endregion

        #region Fields

        private uint key;

        private bool keyDown;

        private IWheelEntry selectedAction;

        private Vector2 startPos;

        #endregion

        #region Constructors and Destructors

        static FunctionWheel()
        {
            Circle = Drawing.GetTexture("materials/ensage_ui/other/chat_wheel/circle.vmat");
            CircleBg = Drawing.GetTexture("materials/ensage_ui/other/chat_wheel/circle_bg.vmat");
            CircleBg2 = Drawing.GetTexture("materials/ensage_ui/other/chat_wheel/circle_bg2.vmat");
            CirclePtr = Drawing.GetTexture("materials/ensage_ui/other/chat_wheel/circle_pointer.vmat");

            Cursor = Drawing.GetTexture("materials/ensage_ui/other/chat_wheel/cursor.vmat");
            ArrowR = Drawing.GetTexture("materials/ensage_ui/other/chat_wheel/arrow_1.vmat");
            ArrowRD = Drawing.GetTexture("materials/ensage_ui/other/chat_wheel/arrow_2.vmat");
            ArrowD = Drawing.GetTexture("materials/ensage_ui/other/chat_wheel/arrow_3.vmat");
            ArrowLD = Drawing.GetTexture("materials/ensage_ui/other/chat_wheel/arrow_4.vmat");
            ArrowL = Drawing.GetTexture("materials/ensage_ui/other/chat_wheel/arrow_5.vmat");
            ArrowLU = Drawing.GetTexture("materials/ensage_ui/other/chat_wheel/arrow_6.vmat");
            ArrowU = Drawing.GetTexture("materials/ensage_ui/other/chat_wheel/arrow_7.vmat");
            ArrowUR = Drawing.GetTexture("materials/ensage_ui/other/chat_wheel/arrow_8.vmat");

            ArrowTextures4 = new[] { ArrowU, ArrowR, ArrowD, ArrowL };

            ArrowTextures8 = new[] { ArrowU, ArrowUR, ArrowR, ArrowRD, ArrowD, ArrowLD, ArrowL, ArrowLU };
        }

        /// <summary>
        ///     Creates a chat wheel with custom bound functions, like the dota 2 chatwheel.
        /// </summary>
        /// <param name="key">Hotkey which will show the function wheel</param>
        public FunctionWheel(Key key)
            : this((uint)KeyInterop.VirtualKeyFromKey(key))
        {}

        /// <summary>
        ///     Creates a chat wheel with custom bound functions, like the dota 2 chatwheel.
        /// </summary>
        /// <param name="keyCode">Hotkey which will show the function wheel</param>
        public FunctionWheel(uint keyCode)
        {
            this.key = keyCode;
            Game.OnWndProc += this.Game_OnWndProc;
            Drawing.OnDraw += this.Drawing_OnDraw;
        }

        #endregion

        #region Public Properties
        /// <summary>
        ///     The key associated with this function wheel.
        /// </summary>
        public Key Key
        {
            get
            {
                return KeyInterop.KeyFromVirtualKey((int)this.key);
            }
            set
            {
                this.key = (uint)KeyInterop.VirtualKeyFromKey(value);
            }
        }

        /// <summary>
        ///     The keycode associated with this function wheel.
        /// </summary>
        public uint KeyCode
        {
            get
            {
                return this.key;
            }
            set
            {
                this.key = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        public new void Add(IWheelEntry entry)
        {
            if (this.Count > 8)
            {
                throw new IndexOutOfRangeException(
                    $"Too many actions for the FunctionWheel. Maximum is 8 but {this.Count + 1} were given.");
            }

            base.Add(entry);
        }

        public void Add(string displayName, Func<Task> func)
        {
            this.Add(new WheelEntry(displayName, func));
        }

        public void Add(string displayName, Func<Task> func, bool isEnabled)
        {
            this.Add(new WheelEntry(displayName, func, isEnabled));
        }

        public void Dispose()
        {
            Game.OnWndProc -= this.Game_OnWndProc;
            Drawing.OnDraw -= this.Drawing_OnDraw;
        }

        #endregion

        #region Methods

        private void Drawing_OnDraw(EventArgs args)
        {
            if (!this.keyDown) return;

            var count = this.Count;
            //if (count == 0) return;

            Drawing.DrawRect(this.startPos, CircleSize, CircleBg);
            Drawing.DrawRect(this.startPos, CircleSize, CircleBg2);
            Drawing.DrawRect(this.startPos, CircleSize, Circle);

            // Drawing.DrawRect(this._startPos, new Vector2(100,100), CirclePtr);

            var selectedSomething = false;
            var cursorPos = Game.MouseScreenPosition;

            var drawCursorPos = cursorPos;
            var middle = this.startPos + new Vector2(50, 48);
            var distance = drawCursorPos.Distance(middle);
            if (distance > 20)
            {
                drawCursorPos = drawCursorPos - middle;
                drawCursorPos.Normalize();
                drawCursorPos *= 20;
                drawCursorPos = drawCursorPos + middle;
                selectedSomething = true;
            }
            else
            {
                this.selectedAction = null;
            }
            drawCursorPos -= new Vector2(18, 18);
            Drawing.DrawRect(drawCursorPos, new Vector2(36, 36), Cursor);

            if (count <= 4)
            {
                var selectedItem = 0;
                if (selectedSomething)
                {
                    // check which item is selected
                    var minDistance = float.MaxValue;
                    for (var i = 0; i < count; ++i)
                    {
                        // skip empty keys
                        if (this[i].DisplayName == string.Empty || !this[i].IsEnabled) continue;

                        var itemDistance = cursorPos.Distance(this.startPos + TextPos4[i]);
                        if (itemDistance < minDistance)
                        {
                            minDistance = itemDistance;
                            selectedItem = i;
                        }
                    }
                    // draw arrow
                    var arrowTexture = ArrowTextures4[selectedItem];
                    Drawing.DrawRect(this.startPos + ArrowPos4[selectedItem], new Vector2(38, 38), arrowTexture);

                    // execute action
                    this.selectedAction = this[selectedItem];
                }

                // draw text items
                for (var i = 0; i < count; ++i)
                {
                    // selected items have a different color
                    var color = Color.White;
                    if (selectedSomething && i == selectedItem) color = Color.Yellow;
                    if (!this[i].IsEnabled) color = Color.Gray;

                    // since we are drawing left, we need to measure the text
                    if (i == 3)
                    {
                        var size = Drawing.MeasureText(
                            this[i].DisplayName,
                            "Arial",
                            TextSize,
                            FontFlags.AntiAlias | FontFlags.DropShadow);
                        size.Y = 0;
                        Drawing.DrawText(
                             this[i].DisplayName,
                            this.startPos + TextPos4[i] - size,
                            TextSize,
                            color,
                            FontFlags.AntiAlias | FontFlags.DropShadow);
                    }
                    // drawing on top or bot should center the text
                    else if (i % 2 == 0)
                    {
                        var size = Drawing.MeasureText(
                             this[i].DisplayName,
                            "Arial",
                            TextSize,
                            FontFlags.AntiAlias | FontFlags.DropShadow);
                        size.X /= 2;
                        size.Y = 0;
                        Drawing.DrawText(
                             this[i].DisplayName,
                            this.startPos + TextPos4[i] - size,
                            TextSize,
                            color,
                            FontFlags.AntiAlias | FontFlags.DropShadow);
                    }
                    else
                    {
                        Drawing.DrawText(
                             this[i].DisplayName,
                            this.startPos + TextPos4[i],
                            TextSize,
                            color,
                            FontFlags.AntiAlias | FontFlags.DropShadow);
                    }
                }
            }
            else
            {
                var selectedItem = 0;
                if (selectedSomething)
                {
                    // check which item is selected
                    var minDistance = float.MaxValue;
                    for (var i = 0; i < count; ++i)
                    {
                        // skip empty keys
                        if (this[i].DisplayName == string.Empty || !this[i].IsEnabled) continue;

                        var itemDistance = cursorPos.Distance(this.startPos + TextPos8[i]);
                        if (itemDistance < minDistance)
                        {
                            minDistance = itemDistance;
                            selectedItem = i;
                        }
                    }
                    // draw arrow
                    var arrowTexture = ArrowTextures8[selectedItem];
                    Drawing.DrawRect(this.startPos + ArrowPos8[selectedItem], new Vector2(38, 38), arrowTexture);

                    // execute action
                    this.selectedAction = this[selectedItem];
                }
                // draw text items
                for (var i = 0; i < count; ++i)
                {
                    var color = Color.White;
                    if (selectedSomething && i == selectedItem) color = Color.Yellow;
                    if (!this[i].IsEnabled) color = Color.Gray;
                    // since we are drawing left, we need to measure the text
                    if (i > 4)
                    {
                        var size = Drawing.MeasureText(
                             this[i].DisplayName,
                            "Arial",
                            TextSize,
                            FontFlags.AntiAlias | FontFlags.DropShadow);
                        size.Y = 0;
                        Drawing.DrawText(
                             this[i].DisplayName,
                            this.startPos + TextPos8[i] - size,
                            TextSize,
                            color,
                            FontFlags.AntiAlias | FontFlags.DropShadow);
                    }
                    // drawing on top or bot should center the text
                    else if (i == 0 || i == 4)
                    {
                        var size = Drawing.MeasureText(
                             this[i].DisplayName,
                            "Arial",
                            TextSize,
                            FontFlags.AntiAlias | FontFlags.DropShadow);
                        size.X /= 2;
                        size.Y = 0;
                        Drawing.DrawText(
                             this[i].DisplayName,
                            this.startPos + TextPos8[i] - size,
                            TextSize,
                            color,
                            FontFlags.AntiAlias | FontFlags.DropShadow);
                    }
                    else
                    {
                        Drawing.DrawText(
                             this[i].DisplayName,
                            this.startPos + TextPos8[i],
                            TextSize,
                            color,
                            FontFlags.AntiAlias | FontFlags.DropShadow);
                    }
                }
            }
            if (selectedSomething)
            {
                Drawing.DrawRect(this.startPos + new Vector2(0, -4), CircleSize, CirclePtr);
            }
        }

        private void Game_OnWndProc(WndEventArgs args)
        {
            if (Game.IsChatOpen || args.WParam != this.key) return;

            if (args.Msg == WM_KEYDOWN || args.Msg == WM_SYSKEYDOWN)
            {
                if (!this.keyDown)
                {
                    this.keyDown = true;
                    this.startPos = Game.MouseScreenPosition - CircleSize / 2;
                }
            }
            else if (args.Msg == WM_KEYUP || args.Msg == WM_SYSKEYUP)
            {
                if (this.selectedAction != null)
                {
                    GameDispatcher.BeginInvoke(
                        () =>
                            {
                                this.selectedAction.Execute();
                                this.selectedAction = null;
                            });
                }
                this.keyDown = false;
            }
        }

        #endregion
    }
}