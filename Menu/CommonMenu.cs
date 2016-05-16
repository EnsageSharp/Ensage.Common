// <copyright file="CommonMenu.cs" company="EnsageSharp">
//    Copyright (c) 2015 EnsageSharp.
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
    using System.Drawing;

    using SharpDX;

    using Color = SharpDX.Color;

    /// <summary>
    ///     The common menu.
    /// </summary>
    public class CommonMenu : Menu
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommonMenu" /> class.
        /// </summary>
        public CommonMenu()
            : base("Ensage.Common", "Ensage.Common", true, null, false)
        {
            var positionMenu = new Menu("MenuPosition", "menuPosition");
            positionMenu.AddItem(
                new MenuItem("positionX", "Position X").SetValue(
                    new Slider((int)MenuSettings.BasePosition.X, 10, Drawing.Height / 3)));
            positionMenu.AddItem(
                new MenuItem("positionY", "Position Y").SetValue(
                    new Slider((int)MenuSettings.BasePosition.Y, (int)(HUDInfo.ScreenSizeY() * 0.08), Drawing.Width / 4)));
            MenuSettings.BasePosition = new Vector2(
                positionMenu.Item("positionX").GetValue<Slider>().Value, 
                positionMenu.Item("positionY").GetValue<Slider>().Value);
            this.AddSubMenu(positionMenu);
            this.AddItem(new MenuItem("pressKey", "Menu hold key").SetValue(new KeyBind(16, KeyBindType.Press)));
            this.AddItem(new MenuItem("toggleKey", "Menu toggle key").SetValue(new KeyBind(118, KeyBindType.Toggle)));
            this.AddItem(new MenuItem("showMessage", "Show OnLoad message: ").SetValue(true));
            var message =
                this.AddItem(
                    new MenuItem("messageType", "Show the message in: ").SetValue(
                        new StringList(new[] { "SideLog", "Chat", "Console" })));
            this.AddItem(
                new MenuItem("EnsageSharp.Common.IncreaseSize", "Size increase: ").SetValue(new Slider(0, 0, 25)))
                .SetTooltip("Increases size of text and boxes");
            this.AddItem(
                new MenuItem("EnsageSharp.Common.TooltipDuration", "Tooltip Notification Duration").SetValue(
                    new Slider(1500, 0, 5000)));
            this.AddItem(
                new MenuItem("EnsageSharp.Common.BlockKeys", "Block player inputs for KeyBinds: ").SetValue(true));
            this.AddItem(
                new MenuItem("FontInfo", "Press F5 after your change").SetFontStyle(FontStyle.Bold, Color.Yellow));
            message.ValueChanged += MessageValueChanged;
        }

        #endregion
    }
}