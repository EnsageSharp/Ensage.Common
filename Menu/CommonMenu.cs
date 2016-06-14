// <copyright file="CommonMenu.cs" company="EnsageSharp">
//    Copyright (c) 2016 EnsageSharp.
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
namespace Ensage.Common.Menu
{
    using System;

    using SharpDX;

    /// <summary>
    ///     The common menu.
    /// </summary>
    internal class CommonMenu : Menu
    {
        /// <summary>
        /// Gets or sets a value indicating whether increase menu size.
        /// </summary>
        internal static float IncreaseMenuSize { get; set; }

        #region Fields

        /// <summary>
        ///     The new message type.
        /// </summary>
        private StringList newMessageType;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommonMenu" /> class.
        /// </summary>
        public CommonMenu()
            : base("GeneralSettings", "Ensage.Common", true, null, false)
        {
            var menuSettings = new Menu("MenuSettings", "Common.menuSettings");
            menuSettings.AddItem(new MenuItem("pressKey", "Menu hold key").SetValue(new KeyBind(16, KeyBindType.Press)));
            menuSettings.AddItem(
                new MenuItem("toggleKey", "Menu toggle key").SetValue(new KeyBind(118, KeyBindType.Toggle)));
            menuSettings.AddItem(new MenuItem("showMessage", "Show OnLoad message: ").SetValue(true)).SetTooltip("Show message with menu hotkeys on game load");
            var message =
                menuSettings.AddItem(
                    new MenuItem("messageType", "Show the message in: ").SetValue(
                        new StringList(new[] { "SideLog", "Chat", "Console" })));
            menuSettings.AddItem(
                new MenuItem("EnsageSharp.Common.IncreaseSize", "Size increase: ").SetValue(new Slider(0, 0, 250)))
                .SetTooltip(
                    "Increases size of the menu, it can take up to 20 sec before the menu gets fully resized")
                .ValueChanged += (sender, args) =>
                    {
                        DelayAction.Add(250, () => IncreaseMenuSize = args.GetNewValue<Slider>().Value);
                    };
            IncreaseMenuSize = menuSettings.Item("EnsageSharp.Common.IncreaseSize").GetValue<Slider>().Value;
            menuSettings.AddItem(
                new MenuItem("positionX", "Position X").SetValue(
                    new Slider((int)MenuSettings.BasePosition.X, 10, Drawing.Height / 3)))
                .SetTooltip("Change position by dragging the 'EnsageSharp Menu' top panel").SetFontColor(Color.GreenYellow);
            menuSettings.AddItem(
                new MenuItem("positionY", "Position Y").SetValue(
                    new Slider((int)MenuSettings.BasePosition.Y, (int)(HUDInfo.ScreenSizeY() * 0.08), Drawing.Width / 4)))
                .SetTooltip("Change position by dragging the 'EnsageSharp Menu' top panel").SetFontColor(Color.GreenYellow);
            MenuSettings.BasePosition = new Vector2(
                menuSettings.Item("positionX").GetValue<Slider>().Value,
                menuSettings.Item("positionY").GetValue<Slider>().Value);
            this.AddSubMenu(menuSettings);
            var hacks = new Menu("Hacks", "Common.Hacks");
            hacks.AddItem(
                new MenuItem("showSpawnBoxes", "Show SpawnBoxes").SetValue(Config.ShowSpawnBoxes)
                    .SetTooltip("Makes SpawnBoxes always visible")).ValueChanged += (sender, args) =>
                        {
                            Config.ShowSpawnBoxes = args.GetNewValue<bool>();
                        };
            Config.ShowSpawnBoxes = hacks.Item("showSpawnBoxes").GetValue<bool>();
            hacks.AddItem(
                new MenuItem("showTowerRange", "Show TowerRange").SetValue(Config.ShowTowerRange)
                    .SetTooltip("Makes TowerRange always visible")).ValueChanged += (sender, args) =>
                        {
                            Config.ShowTowerRange = args.GetNewValue<bool>();
                        };
            Config.ShowTowerRange = hacks.Item("showTowerRange").GetValue<bool>();
            hacks.AddItem(
                new MenuItem("autoAccept", "AutoAccept").SetValue(Config.AutoAccept)
                    .SetTooltip("Automatically clicks on accept after game was found")).ValueChanged +=
                (sender, args) =>
                    {
                        Config.AutoAccept = args.GetNewValue<bool>();
                    };
            Config.AutoAccept = hacks.Item("autoAccept").GetValue<bool>();
            this.AddSubMenu(hacks);
            this.AddItem(
                new MenuItem("EnsageSharp.Common.BlockKeys", "Block player inputs for KeyBinds: ").SetValue(true))
                .SetTooltip("When a assembly uses a key, dota will ignore it");
            this.AddItem(
                new MenuItem("showConsole", "Show Console").SetValue(Config.DebugConsole)
                    .SetTooltip("Enable if you wanna see console window")).ValueChanged += (sender, args) =>
                        {
                            Config.DebugConsole = args.GetNewValue<bool>();
                        };
            Config.DebugConsole = this.Item("showConsole").GetValue<bool>();
            Config.DisableDrawings = false;
            this.AddItem(
                new MenuItem("disableDrawings", "Disable Drawings").SetValue(Config.DisableDrawings)
                    .DontSave()
                    .SetTooltip(
                        "This option will HIDE menu and all other drawings and particles. This option will get disabled after you press F5",
                        Color.Red)).ValueChanged += (sender, args) =>
                            {
                                Config.DisableDrawings = args.GetNewValue<bool>();
                            };
            this.Item("disableDrawings").SetValue(false);
            message.ValueChanged += this.MessageValueChanged;
            Events.OnLoad += this.Events_OnLoad;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The message value changed.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        internal void MessageValueChanged(object sender, OnValueChangeEventArgs e)
        {
            this.newMessageType = e.GetNewValue<StringList>();
            this.Events_OnLoad(null, null);
        }

        /// <summary>
        ///     The events_ on load.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void Events_OnLoad(object sender, EventArgs e)
        {
            var console = this.newMessageType.SelectedIndex == 2;

            if (Root.Item("showMessage").GetValue<bool>() && !console)
            {
                var msg =
                    "<font face='Verdana' color='#ff7700'>[</font>Menu Hotkeys<font face='Verdana' color='#ff7700'>]</font> Press: <font face='Verdana' color='#ff7700'>"
                    + Utils.KeyToText(Root.Item("toggleKey").GetValue<KeyBind>().Key)
                    + "</font> Hold: <font face='Verdana' color='#ff7700'>"
                    + Utils.KeyToText(Root.Item("pressKey").GetValue<KeyBind>().Key) + "</font>";
                Game.PrintMessage(
                    msg, 
                    this.newMessageType.SelectedIndex == 2 || this.newMessageType.SelectedIndex == 0
                        ? MessageType.LogMessage
                        : MessageType.ChatMessage);
            }
            else if (console && Root.Item("showMessage").GetValue<bool>())
            {
                var msg = @"[Menu Hotkeys] Press: " + Utils.KeyToText(Root.Item("toggleKey").GetValue<KeyBind>().Key)
                          + @" Hold: " + Utils.KeyToText(Root.Item("pressKey").GetValue<KeyBind>().Key);
                Console.WriteLine(msg);
            }
        }

        #endregion
    }
}