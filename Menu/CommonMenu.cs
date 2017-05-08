// <copyright file="CommonMenu.cs" company="EnsageSharp">
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
namespace Ensage.Common.Menu
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Security.Permissions;

    using SharpDX;

    /// <summary>
    ///     The common menu.
    /// </summary>
    public class CommonMenu : Menu
    {
        #region Fields

        /// <summary>
        ///     The hacks.
        /// </summary>
        private Menu hacks;

        /// <summary>
        ///     The message.
        /// </summary>
        private MenuItem message;

        /// <summary>
        ///     The new message type.
        /// </summary>
        private StringList newMessageType;

        /// <summary>
        ///     The settings.
        /// </summary>
        private Menu settings;

        /// <summary>
        ///     The x pos.
        /// </summary>
        private MenuItem xPos;

        /// <summary>
        ///     The y pos.
        /// </summary>
        private MenuItem yPos;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommonMenu" /> class.
        /// </summary>
        [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
        internal CommonMenu()
            : base("GeneralSettings", "Ensage.Common", true, null, false)
        {
            this.Settings();
            this.Hacks();
            this.Initialize();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the selected theme.
        /// </summary>
        public IMenuTheme SelectedTheme { get; private set; }

        /// <summary>
        ///     Gets or sets the themes.
        /// </summary>
        [ImportMany(typeof(IMenuTheme))]
        public IEnumerable<Lazy<IMenuTheme>> Themes { get; set; }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets a value indicating whether increase menu size.
        /// </summary>
        internal static float IncreaseMenuSize { get; set; }

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
            var currentX = this.Item("positionX").GetValue<Slider>().Value;
            var currentY = this.Item("positionY").GetValue<Slider>().Value;
            this.Item("positionX")
                .SetValue(new Slider(Math.Max(Math.Min(currentX, Drawing.Height / 3), 10), 10, Drawing.Height / 3));
            this.Item("positionY")
                .SetValue(
                    new Slider(
                        Math.Max(Math.Min(currentY, Drawing.Width / 4), (int)(HUDInfo.ScreenSizeY() * 0.08)),
                        (int)(HUDInfo.ScreenSizeY() * 0.08),
                        Drawing.Width / 4));
            var console = this.newMessageType.SelectedIndex == 2;

            if (this.Item("showMessage").GetValue<bool>() && !console)
            {
                var msg =
                    "<font face='Verdana' color='#ff7700'>[</font>Menu Hotkeys<font face='Verdana' color='#ff7700'>]</font> Press: <font face='Verdana' color='#ff7700'>"
                    + Utils.KeyToText(this.Item("toggleKey").GetValue<KeyBind>().Key)
                    + "</font> Hold: <font face='Verdana' color='#ff7700'>"
                    + Utils.KeyToText(this.Item("pressKey").GetValue<KeyBind>().Key) + "</font>";
                Game.PrintMessage(
                    msg);
            }
            else if (console && this.Item("showMessage").GetValue<bool>())
            {
                var msg = @"[Menu Hotkeys] Press: " + Utils.KeyToText(this.Item("toggleKey").GetValue<KeyBind>().Key)
                          + @" Hold: " + Utils.KeyToText(this.Item("pressKey").GetValue<KeyBind>().Key);
                Console.WriteLine(msg);
            }
        }

        /// <summary>
        ///     The hacks.
        /// </summary>
        private void Hacks()
        {
            this.hacks = new Menu("Hacks", "Common.Hacks");
            this.hacks.AddItem(
                    new MenuItem("showSpawnBoxes", "Show SpawnBoxes").SetValue(Config.ShowSpawnBoxes)
                        .SetTooltip("Makes SpawnBoxes always visible")).ValueChanged +=
                (sender, args) => { Config.ShowSpawnBoxes = args.GetNewValue<bool>(); };
            Config.ShowSpawnBoxes = this.hacks.Item("showSpawnBoxes").GetValue<bool>();
            this.hacks.AddItem(
                    new MenuItem("showTowerRange", "Show TowerRange").SetValue(Config.ShowTowerRange)
                        .SetTooltip("Makes TowerRange always visible")).ValueChanged +=
                (sender, args) => { Config.ShowTowerRange = args.GetNewValue<bool>(); };
            Config.ShowTowerRange = this.hacks.Item("showTowerRange").GetValue<bool>();
            this.hacks.AddItem(
                    new MenuItem("autoAccept", "AutoAccept").SetValue(Config.AutoAccept)
                        .SetTooltip("Automatically clicks on accept after game was found")).ValueChanged +=
                (sender, args) => { Config.AutoAccept = args.GetNewValue<bool>(); };
            Config.AutoAccept = this.hacks.Item("autoAccept").GetValue<bool>();
            this.AddSubMenu(this.hacks);
        }

        /// <summary>
        ///     The initialize.
        /// </summary>
        private void Initialize()
        {
            this.AddItem(new MenuItem("EnsageSharp.Common.BlockKeys", "Block player inputs for KeyBinds: ")
                .SetValue(false))
                .SetTooltip("When a assembly uses a key, dota will ignore it");

            Config.DisableDrawings = false;
            this.AddItem(
                    new MenuItem("disableDrawings", "Disable Drawings").SetValue(Config.DisableDrawings)
                        .DontSave()
                        .SetTooltip(
                            "This option will HIDE menu and all other drawings and particles. This option will get disabled after you press F5",
                            Color.Red)).ValueChanged +=
                (sender, args) => { Config.DisableDrawings = args.GetNewValue<bool>(); };
            this.Item("disableDrawings").SetValue(false);
            this.message.ValueChanged += this.MessageValueChanged;
            Events.OnLoad += this.Events_OnLoad;

            Composer.ComposeParts(this);

            var themes = this.Themes.Select(theme => theme.Value.ThemeName);
            var themeSelect =
                new MenuItem("Common.Menu.Themes", "Theme").SetValue(new StringList(themes.OrderBy(x => x).ToArray()));
            themeSelect.ValueChanged += (sender, args) =>
                {
                    var theme =
                        this.Themes.FirstOrDefault(
                            x => x.Value.ThemeName == args.GetNewValue<StringList>().SelectedValue);
                    if (theme == null)
                    {
                        return;
                    }

                    foreach (var rootMenu in RootMenus)
                    {
                        foreach (var menuItem in rootMenu.Value.Items)
                        {
                            if (menuItem.FontColor != this.SelectedTheme.ItemDefaultTextColor)
                            {
                                continue;
                            }

                            menuItem.SetFontColor(theme.Value.ItemDefaultTextColor);
                        }

                        foreach (var child in rootMenu.Value.Children)
                        {
                            if (child.Color == this.SelectedTheme.ItemDefaultTextColor)
                            {
                                child.SetFontColor(theme.Value.MenuDefaultTextColor);
                            }

                            foreach (var menuItem in child.Items)
                            {
                                if (menuItem.FontColor != this.SelectedTheme.ItemDefaultTextColor)
                                {
                                    continue;
                                }

                                menuItem.SetFontColor(theme.Value.ItemDefaultTextColor);
                            }
                        }
                    }

                    this.SelectedTheme = theme.Value;
                };
            var defaultTheme =
                this.Themes.FirstOrDefault(x => x.Value.ThemeName == themeSelect.GetValue<StringList>().SelectedValue);
            if (defaultTheme != null)
            {
                this.SelectedTheme = defaultTheme.Value;
            }

            this.settings.AddItem(themeSelect);

            this.SetFontColor(this.SelectedTheme.MenuDefaultTextColor);

            // if (Game.IsInGame)
            // {
            // this.Events_OnLoad(null, null);
            // }
            foreach (var menuItem in this.Items)
            {
                menuItem.SetFontColor(this.SelectedTheme.ItemDefaultTextColor);
            }

            foreach (var child in this.Children)
            {
                child.SetFontColor(this.SelectedTheme.MenuDefaultTextColor);
                foreach (var menuItem in child.Items)
                {
                    menuItem.SetFontColor(this.SelectedTheme.ItemDefaultTextColor);
                }
            }
        }

        /// <summary>
        ///     The settings.
        /// </summary>
        private void Settings()
        {
            this.settings = new Menu("MenuSettings", "Common.menuSettings");
            this.settings.AddItem(
                new MenuItem("pressKey", "Menu hold key").SetValue(new KeyBind(16, KeyBindType.Press)));
            this.settings.AddItem(
                new MenuItem("toggleKey", "Menu toggle key").SetValue(new KeyBind(118, KeyBindType.Toggle)));
            this.settings.AddItem(new MenuItem("showMessage", "Show OnLoad message: ").SetValue(true))
                .SetTooltip("Show message with menu hotkeys on game load");
            this.message =
                this.settings.AddItem(
                    new MenuItem("messageType", "Show the message in: ").SetValue(
                        new StringList(new[] { "SideLog", "Chat", "Console" })));
            this.settings.AddItem(
                        new MenuItem("EnsageSharp.Common.IncreaseSize", "Size increase: ").SetValue(new Slider(0, 0, 250)))
                    .SetTooltip("Increases size of the menu, it can take up to 20 sec before the menu gets fully resized")
                    .ValueChanged +=
                (sender, args) => { DelayAction.Add(250, () => IncreaseMenuSize = args.GetNewValue<Slider>().Value); };
            IncreaseMenuSize = this.settings.Item("EnsageSharp.Common.IncreaseSize").GetValue<Slider>().Value;
            this.xPos =
                this.settings.AddItem(
                        new MenuItem("positionX", "Position X").SetValue(
                            new Slider((int)MenuSettings.BasePosition.X, 10, Drawing.Height / 3)))
                    .SetTooltip("Change position by dragging the 'EnsageSharp Menu' top panel")
                    .SetFontColor(Color.GreenYellow);
            this.yPos =
                this.settings.AddItem(
                        new MenuItem("positionY", "Position Y").SetValue(
                            new Slider(
                                (int)MenuSettings.BasePosition.Y,
                                (int)(HUDInfo.ScreenSizeY() * 0.08),
                                Drawing.Width / 4)))
                    .SetTooltip("Change position by dragging the 'EnsageSharp Menu' top panel")
                    .SetFontColor(Color.GreenYellow);
            this.AddSubMenu(this.settings);

            var currentX = this.xPos.GetValue<Slider>().Value;
            var currentY = this.yPos.GetValue<Slider>().Value;
            this.xPos.SetValue(new Slider(Math.Max(Math.Min(currentX, Drawing.Height / 3), 10), 10, Drawing.Height / 3));
            this.yPos.SetValue(
                new Slider(
                    Math.Max(Math.Min(currentY, Drawing.Width / 4), (int)(HUDInfo.ScreenSizeY() * 0.08)),
                    (int)(HUDInfo.ScreenSizeY() * 0.08),
                    Drawing.Width / 4));
            MenuSettings.BasePosition = new Vector2(
                this.xPos.GetValue<Slider>().Value,
                this.yPos.GetValue<Slider>().Value);
        }

        #endregion
    }
}