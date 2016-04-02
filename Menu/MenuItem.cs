// <copyright file="MenuItem.cs" company="EnsageSharp">
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
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;

    using Ensage.Common.Extensions.SharpDX;
    using Ensage.Common.Menu.NotificationData;
    using Ensage.Common.Objects;

    using SharpDX;

    using Color = SharpDX.Color;

    /// <summary>
    ///     The menu item.
    /// </summary>
    public class MenuItem
    {
        #region Fields

        /// <summary>
        ///     The display name.
        /// </summary>
        public string DisplayName;

        /// <summary>
        ///     The font color.
        /// </summary>
        public ColorBGRA FontColor;

        /// <summary>
        ///     The font style.
        /// </summary>
        public FontStyle FontStyle;

        /// <summary>
        ///     The menu font size.
        /// </summary>
        public int MenuFontSize;

        /// <summary>
        ///     The name.
        /// </summary>
        public string Name;

        /// <summary>
        ///     The parent.
        /// </summary>
        public Menu Parent;

        /// <summary>
        ///     The show item.
        /// </summary>
        public bool ShowItem;

        /// <summary>
        ///     The tag.
        /// </summary>
        public int Tag;

        /// <summary>
        ///     The tooltip.
        /// </summary>
        public string Tooltip;

        /// <summary>
        ///     The tooltip color.
        /// </summary>
        public Color TooltipColor;

        /// <summary>
        ///     The drawing tooltip.
        /// </summary>
        internal bool DrawingTooltip;

        /// <summary>
        ///     The interacting.
        /// </summary>
        internal bool Interacting;

        /// <summary>
        ///     The value set.
        /// </summary>
        internal bool ValueSet;

        /// <summary>
        ///     The value type.
        /// </summary>
        internal MenuValueType ValueType;

        /// <summary>
        ///     The _ menu config name.
        /// </summary>
        private readonly string menuConfigName;

        /// <summary>
        ///     The _dont save.
        /// </summary>
        private bool dontSave;

        /// <summary>
        ///     The _is shared.
        /// </summary>
        private bool isShared;

        /// <summary>
        ///     The _serialized.
        /// </summary>
        private byte[] serialized;

        /// <summary>
        ///     The _value.
        /// </summary>
        private object value;

        /// <summary>
        ///     The _visible.
        /// </summary>
        private bool visible;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MenuItem" /> class.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <param name="displayName">
        ///     The display name.
        /// </param>
        /// <param name="makeChampionUniq">
        ///     The make champion unique.
        /// </param>
        public MenuItem(string name, string displayName, bool makeChampionUniq = false)
        {
            if (makeChampionUniq)
            {
                name = ObjectManager.LocalHero.StoredName() + name;
            }

            this.Name = name;
            this.DisplayName = displayName;
            this.FontStyle = FontStyle.Regular;
            this.FontColor = new Color(195, 186, 173, 255);
            this.ShowItem = true;
            this.Tag = 0;
            this.menuConfigName = Assembly.GetCallingAssembly().GetName().Name
                                  + Assembly.GetCallingAssembly().GetType().GUID;
        }

        #endregion

        #region Public Events

        /// <summary>
        ///     The value changed.
        /// </summary>
        public event EventHandler<OnValueChangeEventArgs> ValueChanged;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the tooltip duration.
        /// </summary>
        public int TooltipDuration
        {
            get
            {
                return CommonMenu.MenuConfig.Item("EnsageSharp.Common.TooltipDuration").GetValue<Slider>().Value;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the height.
        /// </summary>
        internal int Height
        {
            get
            {
                return MenuSettings.MenuItemHeight;
            }
        }

        /// <summary>
        ///     Gets the my base position.
        /// </summary>
        internal Vector2 MyBasePosition
        {
            get
            {
                return this.Parent == null ? MenuSettings.BasePosition : this.Parent.MyBasePosition;
            }
        }

        /// <summary>
        ///     Gets the needed width.
        /// </summary>
        internal int NeededWidth
        {
            get
            {
                var n = this.Parent.Name + this.Name + this.DisplayName + "Width";

                if (!Utils.SleepCheck(n))
                {
                    return (int)Menu.menuPositionDictionary[n].X;
                }

                var extra = 0;

                if (this.ValueType == MenuValueType.StringList)
                {
                    var slVal = this.GetValue<StringList>();
                    var max =
                        slVal.SList.Select(
                            v =>
                            (int)
                            Drawing.MeasureText(
                                v, 
                                "Arial", 
                                new Vector2(
                                (float)(MenuSettings.MenuItemHeight * 0.51), 
                                (float)(MenuSettings.MenuItemWidth * 0.7)), 
                                FontFlags.None).X + Math.Max((int)(HUDInfo.GetHpBarSizeY() * 2.5), 17))
                            .Concat(new[] { 0 })
                            .Max();

                    extra += max;
                }

                if (this.ValueType == MenuValueType.AbilityToggler)
                {
                    extra += this.GetValue<AbilityToggler>().Dictionary.Count * (this.Height - 5);
                }

                if (this.ValueType == MenuValueType.HeroToggler)
                {
                    extra += this.GetValue<HeroToggler>().Dictionary.Count * (this.Height + 5);
                }

                if (this.ValueType == MenuValueType.KeyBind)
                {
                    var val = this.GetValue<KeyBind>();
                    extra +=
                        (int)
                        Drawing.MeasureText(
                            " [" + Utils.KeyToText(val.Key) + "]", 
                            "Arial", 
                            new Vector2(
                            (float)(MenuSettings.MenuItemHeight * 0.51), 
                            (float)(MenuSettings.MenuItemWidth * 0.7)), 
                            FontFlags.None).X;
                    if (val.Type == KeyBindType.Toggle)
                    {
                        extra +=
                            (int)
                            Drawing.MeasureText(
                                val.Active ? " (on)" : " (off)", 
                                "Arial", 
                                new Vector2(
                                (float)(MenuSettings.MenuItemHeight * 0.51), 
                                (float)(MenuSettings.MenuItemWidth * 0.7)), 
                                FontFlags.None).X;
                    }
                }

                if (!string.IsNullOrEmpty(this.Tooltip))
                {
                    extra += this.Height;
                }

                if (!Menu.menuPositionDictionary.ContainsKey(n))
                {
                    Menu.menuPositionDictionary.Add(
                        n, 
                        new Vector2(
                            (int)
                            (Drawing.MeasureText(
                                MultiLanguage._(this.DisplayName), 
                                "Arial", 
                                new Vector2((float)(this.Height * 0.51), 20), 
                                FontFlags.AntiAlias).X + this.Height * 1.4
                             + Math.Max((int)(HUDInfo.GetHpBarSizeY() * 1.8), 8) + extra)));
                }
                else
                {
                    Menu.menuPositionDictionary[n] =
                        new Vector2(
                            (int)
                            (Drawing.MeasureText(
                                MultiLanguage._(this.DisplayName), 
                                "Arial", 
                                new Vector2((float)(this.Height * 0.51), 20), 
                                FontFlags.AntiAlias).X + this.Height * 1.4
                             + Math.Max((int)(HUDInfo.GetHpBarSizeY() * 1.8), 8) + extra));
                }

                Utils.Sleep(20000, n);

                return
                    (int)
                    (Drawing.MeasureText(
                        MultiLanguage._(this.DisplayName), 
                        "Arial", 
                        new Vector2((float)(this.Height * 0.51), 20), 
                        FontFlags.AntiAlias).X + this.Height * 1.4 + Math.Max((int)(HUDInfo.GetHpBarSizeY() * 1.8), 8)
                     + extra);
            }
        }

        /// <summary>
        ///     Gets the position.
        /// </summary>
        internal Vector2 Position
        {
            get
            {
                var n = this.Parent.Name + this.DisplayName + this.Name + "position";
                if (!Utils.SleepCheck(n))
                {
                    return Menu.menuPositionDictionary[n];
                }

                var xOffset = 0;

                if (this.Parent != null)
                {
                    xOffset = (int)(this.Parent.Position.X + this.Parent.Width);
                }

                var pos = new Vector2(0, this.MyBasePosition.Y) + new Vector2(xOffset + 1, 0)
                          + (this.YLevel * new Vector2(0, MenuSettings.MenuItemHeight));
                if (!Menu.menuPositionDictionary.ContainsKey(n))
                {
                    Menu.menuPositionDictionary.Add(n, pos);
                }
                else
                {
                    Menu.menuPositionDictionary[n] = pos;
                }

                Utils.Sleep(20000, n);
                return pos;
            }
        }

        /// <summary>
        ///     Gets the save file name.
        /// </summary>
        internal string SaveFileName
        {
            get
            {
                return this.isShared ? "SharedMenuConfig" : this.menuConfigName;
            }
        }

        /// <summary>
        ///     Gets the save key.
        /// </summary>
        internal string SaveKey
        {
            get
            {
                return Utils.Md5Hash("v3" + this.DisplayName + this.Name);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether visible.
        /// </summary>
        internal bool Visible
        {
            get
            {
                return MenuSettings.DrawMenu && this.visible && this.ShowItem;
            }

            set
            {
                this.visible = value;
            }
        }

        /// <summary>
        ///     Gets the width.
        /// </summary>
        internal int Width
        {
            get
            {
                return this.Parent != null ? this.Parent.ChildrenMenuWidth : MenuSettings.MenuItemWidth;
            }
        }

        /// <summary>
        ///     Gets the y level.
        /// </summary>
        internal int YLevel
        {
            get
            {
                if (this.Parent == null)
                {
                    return 0;
                }

                return this.Parent.YLevel + this.Parent.Children.Count
                       + this.Parent.Items.TakeWhile(test => test.Name != this.Name).Count(c => c.ShowItem);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The dont save.
        /// </summary>
        /// <returns>
        ///     The <see cref="MenuItem" />.
        /// </returns>
        public MenuItem DontSave()
        {
            this.dontSave = true;
            return this;
        }

        /// <summary>
        ///     The get value.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="T" />.
        /// </returns>
        public T GetValue<T>()
        {
            return (T)this.value;
        }

        /// <summary>
        ///     The is active.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool IsActive()
        {
            switch (this.ValueType)
            {
                case MenuValueType.Boolean:
                    return this.GetValue<bool>();
                case MenuValueType.KeyBind:
                    return this.GetValue<KeyBind>().Active;
            }

            return false;
        }

        /// <summary>
        ///     The set font style.
        /// </summary>
        /// <param name="fontStyle">
        ///     The font style.
        /// </param>
        /// <param name="fontColor">
        ///     The font color.
        /// </param>
        /// <returns>
        ///     The <see cref="MenuItem" />.
        /// </returns>
        public MenuItem SetFontStyle(FontStyle fontStyle = FontStyle.Regular, Color? fontColor = null)
        {
            this.FontStyle = fontStyle;
            this.FontColor = fontColor ?? Color.White;

            return this;
        }

        /// <summary>
        ///     The set shared.
        /// </summary>
        /// <returns>
        ///     The <see cref="MenuItem" />.
        /// </returns>
        public MenuItem SetShared()
        {
            this.isShared = true;
            return this;
        }

        /// <summary>
        ///     The set tag.
        /// </summary>
        /// <param name="tag">
        ///     The tag.
        /// </param>
        /// <returns>
        ///     The <see cref="MenuItem" />.
        /// </returns>
        public MenuItem SetTag(int tag = 0)
        {
            this.Tag = tag;

            return this;
        }

        /// <summary>
        ///     The set tooltip.
        /// </summary>
        /// <param name="tooltip">
        ///     The tooltip.
        /// </param>
        /// <param name="tooltipColor">
        ///     The tooltip color.
        /// </param>
        /// <returns>
        ///     The <see cref="MenuItem" />.
        /// </returns>
        public MenuItem SetTooltip(string tooltip, Color? tooltipColor = null)
        {
            this.Tooltip = tooltip;
            this.TooltipColor = tooltipColor ?? Color.White;
            return this;
        }

        /// <summary>
        ///     The set value.
        /// </summary>
        /// <param name="newValue">
        ///     The new value.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="MenuItem" />.
        /// </returns>
        public MenuItem SetValue<T>(T newValue)
        {
            this.ValueType = MenuValueType.None;
            if (newValue.GetType().ToString().Contains("Boolean"))
            {
                this.ValueType = MenuValueType.Boolean;
            }
            else if (newValue.GetType().ToString().Contains("Slider"))
            {
                this.ValueType = MenuValueType.Slider;
            }
            else if (newValue.GetType().ToString().Contains("KeyBind"))
            {
                this.ValueType = MenuValueType.KeyBind;
            }
            else if (newValue.GetType().ToString().Contains("Int"))
            {
                this.ValueType = MenuValueType.Integer;
            }
            else if (newValue.GetType().ToString().Contains("Circle"))
            {
                this.ValueType = MenuValueType.Circle;
            }
            else if (newValue.GetType().ToString().Contains("StringList"))
            {
                this.ValueType = MenuValueType.StringList;
            }
            else if (newValue.GetType().ToString().Contains("AbilityToggler"))
            {
                this.ValueType = MenuValueType.AbilityToggler;
            }
            else if (newValue.GetType().ToString().Contains("HeroToggler"))
            {
                this.ValueType = MenuValueType.HeroToggler;
            }
            else if (newValue.GetType().ToString().Contains("Color"))
            {
                this.ValueType = MenuValueType.Color;
            }
            else
            {
                Console.WriteLine("CommonLibMenu: Data type not supported");
            }

            var readBytes = SavedSettings.GetSavedData(this.SaveFileName, this.SaveKey);

            var v = newValue;
            try
            {
                if (!this.ValueSet && readBytes != null)
                {
                    switch (this.ValueType)
                    {
                        case MenuValueType.KeyBind:
                            var savedKeyValue = (KeyBind)(object)Utils.Deserialize<T>(readBytes);
                            if (savedKeyValue.Type == KeyBindType.Press)
                            {
                                savedKeyValue.Active = false;
                            }

                            newValue = (T)(object)savedKeyValue;
                            break;

                        case MenuValueType.Slider:
                            var savedSliderValue = (Slider)(object)Utils.Deserialize<T>(readBytes);
                            var newSliderValue = (Slider)(object)newValue;
                            if (savedSliderValue.MinValue == newSliderValue.MinValue
                                && savedSliderValue.MaxValue == newSliderValue.MaxValue)
                            {
                                newValue = (T)(object)savedSliderValue;
                            }

                            break;

                        case MenuValueType.StringList:
                            var savedListValue = (StringList)(object)Utils.Deserialize<T>(readBytes);
                            var newListValue = (StringList)(object)newValue;
                            if (savedListValue.SList.SequenceEqual(newListValue.SList))
                            {
                                newValue = (T)(object)savedListValue;
                            }

                            break;

                        case MenuValueType.AbilityToggler:
                            var savedDictionaryValue = (AbilityToggler)(object)Utils.Deserialize<T>(readBytes);
                            var newDictionaryValue = (AbilityToggler)(object)newValue;
                            var tempValue = newDictionaryValue;
                            if (savedDictionaryValue.SValuesDictionary != null)
                            {
                                foreach (var b in savedDictionaryValue.SValuesDictionary)
                                {
                                    if (!tempValue.SValuesDictionary.ContainsKey(b.Key))
                                    {
                                        tempValue.SValuesDictionary.Add(b.Key, b.Value);
                                    }
                                    else
                                    {
                                        tempValue.SValuesDictionary[b.Key] = b.Value;
                                    }

                                    if (tempValue.Dictionary.ContainsKey(b.Key))
                                    {
                                        tempValue.Dictionary[b.Key] = b.Value;
                                    }
                                }
                            }

                            newValue = (T)(object)tempValue;
                            break;

                        case MenuValueType.HeroToggler:
                            var savedHeroDictionaryValue = (HeroToggler)(object)Utils.Deserialize<T>(readBytes);
                            var newHeroDictionaryValue = (HeroToggler)(object)newValue;
                            var tempHValue = newHeroDictionaryValue;
                            if (savedHeroDictionaryValue.SValuesDictionary != null)
                            {
                                foreach (var b in savedHeroDictionaryValue.SValuesDictionary)
                                {
                                    if (!tempHValue.SValuesDictionary.ContainsKey(b.Key))
                                    {
                                        tempHValue.SValuesDictionary.Add(b.Key, b.Value);
                                    }
                                    else
                                    {
                                        tempHValue.SValuesDictionary[b.Key] = b.Value;
                                    }

                                    if (tempHValue.Dictionary.ContainsKey(b.Key))
                                    {
                                        tempHValue.Dictionary[b.Key] = b.Value;
                                    }
                                }
                            }

                            newValue = (T)(object)tempHValue;
                            break;

                        default:
                            newValue = Utils.Deserialize<T>(readBytes);
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                newValue = v;
                Console.WriteLine(e);
            }

            OnValueChangeEventArgs valueChangedEvent = null;

            if (this.ValueSet)
            {
                var handler = this.ValueChanged;
                if (handler != null)
                {
                    valueChangedEvent = new OnValueChangeEventArgs(this.value, newValue);
                    handler(this, valueChangedEvent);
                }
            }

            if (valueChangedEvent != null)
            {
                if (valueChangedEvent.Process)
                {
                    this.value = newValue;
                }
            }
            else
            {
                this.value = newValue;
            }

            this.ValueSet = true;
            this.serialized = Utils.Serialize(this.value);

            return this;
        }

        /// <summary>
        ///     The show.
        /// </summary>
        /// <param name="showItem">
        ///     The show item.
        /// </param>
        /// <returns>
        ///     The <see cref="MenuItem" />.
        /// </returns>
        public MenuItem Show(bool showItem = true)
        {
            this.ShowItem = showItem;

            return this;
        }

        /// <summary>
        ///     The show tooltip.
        /// </summary>
        /// <param name="hide">
        ///     The hide.
        /// </param>
        public void ShowTooltip(bool hide = false)
        {
            if (!string.IsNullOrEmpty(this.Tooltip))
            {
                this.DrawingTooltip = !hide;
            }
        }

        /// <summary>
        ///     The show tooltip notification.
        /// </summary>
        public void ShowTooltipNotification()
        {
            if (!string.IsNullOrEmpty(this.Tooltip))
            {
                var notif = new Notification(this.Tooltip).SetTextColor(System.Drawing.Color.White);
                Notifications.AddNotification(notif);
                DelayAction.Add(this.TooltipDuration, () => notif.Dispose());
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The drawing_ on draw.
        /// </summary>
        internal void Drawing_OnDraw()
        {
            DotaTexture abg;
            const string ABgName = "menu_button.vmat_c";

            if (!Menu.TextureDictionary.TryGetValue(ABgName, out abg))
            {
                abg = Drawing.GetTexture("materials/ensage_ui/ensagemenu/" + ABgName);
                Menu.TextureDictionary.Add(ABgName, abg);
            }

            var s = MultiLanguage._(this.DisplayName);
            if (this.DrawingTooltip)
            {
                MenuDrawHelper.DrawToolTipText(
                    new Vector2(this.Position.X + this.Width, this.Position.Y), 
                    this, 
                    this.TooltipColor);
            }

            switch (this.ValueType)
            {
                case MenuValueType.None:
                    MenuUtils.DrawBoxBordered(
                        this.Position.X, 
                        this.Position.Y, 
                        this.Width, 
                        this.Height, 
                        1, 
                        abg, 
                        new Color(20, 20, 20, 200));
                    Drawing.DrawRect(
                        this.Position, 
                        new Vector2(this.Width, this.Height), 
                        Utils.IsUnderRectangle(
                            Game.MouseScreenPosition, 
                            this.Position.X, 
                            this.Position.Y, 
                            this.Width, 
                            this.Height)
                            ? new Color(30, 19, 5, 220)
                            : new Color(10, 10, 5, 210));
                    var textSize1 = Drawing.MeasureText(
                        s, 
                        "Arial", 
                        new Vector2((float)(this.Height * 0.51), 20), 
                        FontFlags.AntiAlias);
                    var textPos1 = this.Position + new Vector2(5, (float)((this.Height * 0.5) - (textSize1.Y * 0.5)));
                    Drawing.DrawText(
                        s, 
                        textPos1, 
                        new Vector2((float)(this.Height * 0.51), 20), 
                        this.FontColor, 
                        FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom
                        | FontFlags.StrikeOut);
                    break;
                case MenuValueType.Slider:
                    MenuUtils.DrawBoxBordered(
                        this.Position.X, 
                        this.Position.Y, 
                        this.Width, 
                        this.Height, 
                        1, 
                        abg, 
                        new Color(20, 20, 20, 200));
                    Drawing.DrawRect(
                        this.Position, 
                        new Vector2(this.Width, this.Height), 
                        Utils.IsUnderRectangle(
                            Game.MouseScreenPosition, 
                            this.Position.X, 
                            this.Position.Y, 
                            this.Width, 
                            this.Height)
                            ? new Color(30, 19, 5, 220)
                            : new Color(10, 10, 5, 210));
                    MenuDrawHelper.DrawSlider(this.Position, this);
                    textSize1 = Drawing.MeasureText(
                        s, 
                        "Arial", 
                        new Vector2((float)(this.Height * 0.51), 20), 
                        FontFlags.AntiAlias);
                    textPos1 = this.Position + new Vector2(5, (float)((this.Height * 0.5) - (textSize1.Y * 0.5)));
                    Drawing.DrawText(
                        s, 
                        textPos1, 
                        new Vector2((float)(this.Height * 0.51), 20), 
                        this.FontColor, 
                        FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom
                        | FontFlags.StrikeOut);
                    break;
                case MenuValueType.Boolean:
                    MenuUtils.DrawBoxBordered(
                        this.Position.X, 
                        this.Position.Y, 
                        this.Width, 
                        this.Height, 
                        1, 
                        abg, 
                        new Color(20, 20, 20, 200));
                    Drawing.DrawRect(
                        this.Position, 
                        new Vector2(this.Width, this.Height), 
                        Utils.IsUnderRectangle(
                            Game.MouseScreenPosition, 
                            this.Position.X, 
                            this.Position.Y, 
                            this.Width, 
                            this.Height)
                            ? new Color(30, 19, 5, 220)
                            : (this.GetValue<bool>() ? new Color(10, 10, 5, 210) : new Color(5, 5, 0, 235)));
                    MenuDrawHelper.DrawOnOff(
                        this.GetValue<bool>(), 
                        new Vector2(this.Position.X + this.Width - this.Height, this.Position.Y), 
                        this);
                    textSize1 = Drawing.MeasureText(
                        s, 
                        "Arial", 
                        new Vector2((float)(this.Height * 0.51), 20), 
                        FontFlags.AntiAlias);
                    textPos1 = this.Position + new Vector2(5, (float)((this.Height * 0.5) - (textSize1.Y * 0.5)));
                    Drawing.DrawText(
                        s, 
                        textPos1, 
                        new Vector2((float)(this.Height * 0.51), 20), 
                        this.GetValue<bool>()
                            ? (Color)this.FontColor
                            : new Color(this.FontColor.R - 70, this.FontColor.G - 70, this.FontColor.B - 70), 
                        FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom
                        | FontFlags.StrikeOut);
                    break;

                case MenuValueType.KeyBind:
                    var val = this.GetValue<KeyBind>();
                    MenuUtils.DrawBoxBordered(
                        this.Position.X, 
                        this.Position.Y, 
                        this.Width, 
                        this.Height, 
                        1, 
                        abg, 
                        new Color(20, 20, 20, 200));
                    Drawing.DrawRect(
                        this.Position, 
                        new Vector2(this.Width, this.Height), 
                        Utils.IsUnderRectangle(
                            Game.MouseScreenPosition, 
                            this.Position.X, 
                            this.Position.Y, 
                            this.Width, 
                            this.Height)
                            ? new Color(30, 19, 5, 220)
                            : (val.Type == KeyBindType.Toggle
                                   ? val.Active ? new Color(10, 10, 5, 210) : new Color(5, 5, 0, 235)
                                   : new Color(10, 10, 5, 210)));
                    var te = Utils.KeyToText(val.Key);
                    var sizet = new Vector2((float)(this.Height / 1.9), this.Width / 2);
                    if (this.Interacting)
                    {
                        te = MultiLanguage._("?");
                        sizet = new Vector2(this.Height / 2, this.Width / 2);
                    }

                    var rpos = this.Position + new Vector2(this.Width - (this.Height * 2), this.Height / 6);
                    var rsize = new Vector2((float)(this.Height * 1.9), this.Height - ((this.Height / 6) * 2));
                    var alpha = Utils.IsUnderRectangle(Game.MouseScreenPosition, rpos.X, rpos.Y, rsize.X, rsize.Y)
                                    ? 40
                                    : 0;

                    var acolor = val.Type == KeyBindType.Toggle
                                     ? val.Active
                                           ? new Color(45 + alpha, 45 + alpha, 45 + alpha)
                                           : new Color(28 + alpha, 28 + alpha, 28 + alpha)
                                     : new Color(45 + alpha, 45 + alpha, 45 + alpha);

                    var aborder = val.Type == KeyBindType.Toggle
                                      ? val.Active ? new Color(40, 120, 40, 255) : new Color(0, 0, 0, 0)
                                      : new Color(0, 0, 0, 0);

                    var height = this.Height - ((this.Height / 6) * 2);

                    var apos = val.Type == KeyBindType.Toggle
                                   ? val.Active ? rpos : rpos - new Vector2(height / 10, height / 10)
                                   : rpos - new Vector2(height / 10, height / 10);

                    var asize = val.Type == KeyBindType.Toggle
                                    ? val.Active ? rsize : rsize + new Vector2((height / 10) * 2, (height / 10) * 2)
                                    : rsize + new Vector2((height / 10) * 2, (height / 10) * 2);

                    if (this.Interacting)
                    {
                        apos = rpos;
                        asize = rsize;
                    }

                    MenuUtils.DrawBoxBordered(
                        apos.X, 
                        apos.Y, 
                        rsize.X, 
                        rsize.Y, 
                        1f, 
                        this.Interacting ? new Color(150, 100, 80) : aborder, 
                        new Color(0, 0, 0, 0));

                    Drawing.DrawRect(
                        apos + new Vector2(height / 10, height / 10), 
                        new Vector2(asize.X - ((height / 10) * 2), asize.Y - ((height / 10) * 2)), 
                        this.Interacting ? new Color(48 + alpha, 38 + alpha, 28 + alpha) : acolor);

                    var textSize = Drawing.MeasureText(te, "Arial", sizet, FontFlags.AntiAlias);
                    var textPos = this.Position
                                  + new Vector2(
                                        this.Width - this.Height - (textSize.X / 2), 
                                        (float)((this.Height * 0.5) - (textSize.Y * 0.5)));

                    Drawing.DrawText(
                        te, 
                        textPos, 
                        sizet, 
                        new Color(195 + alpha, 139 + alpha, 12 + alpha, 225), 
                        FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom
                        | FontFlags.StrikeOut);

                    if (val.Type == KeyBindType.Toggle)
                    {
                        s += val.Active ? " (on)" : " (off)";
                    }

                    textSize1 = Drawing.MeasureText(
                        s, 
                        "Arial", 
                        new Vector2((float)(this.Height * 0.51), 20), 
                        FontFlags.AntiAlias);
                    textPos1 = this.Position + new Vector2(5, (float)((this.Height * 0.5) - (textSize1.Y * 0.5)));
                    Drawing.DrawText(
                        s, 
                        textPos1, 
                        new Vector2((float)(this.Height * 0.51), 20), 
                        val.Type == KeyBindType.Toggle
                            ? val.Active
                                  ? (Color)this.FontColor
                                  : new Color(this.FontColor.R - 70, this.FontColor.G - 70, this.FontColor.B - 70)
                            : (Color)this.FontColor, 
                        FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom
                        | FontFlags.StrikeOut);
                    break;

                case MenuValueType.Integer:
                    MenuUtils.DrawBoxBordered(
                        this.Position.X, 
                        this.Position.Y, 
                        this.Width, 
                        this.Height, 
                        1, 
                        abg, 
                        new Color(20, 20, 20, 200));
                    Drawing.DrawRect(
                        this.Position, 
                        new Vector2(this.Width, this.Height), 
                        Utils.IsUnderRectangle(
                            Game.MouseScreenPosition, 
                            this.Position.X, 
                            this.Position.Y, 
                            this.Width, 
                            this.Height)
                            ? new Color(30, 19, 5, 220)
                            : new Color(10, 10, 5, 210));
                    var intVal = this.GetValue<int>();

                    textSize = Drawing.MeasureText(
                        intVal.ToString(), 
                        "Arial", 
                        new Vector2(this.Height / 2, this.Width / 2), 
                        FontFlags.AntiAlias);
                    textPos = this.Position + new Vector2(this.Width - textSize.X - 1, 3);
                    Drawing.DrawText(
                        intVal.ToString(), 
                        textPos, 
                        new Vector2(this.Height / 2, this.Width / 2), 
                        new Color(255, 255, 255, 225), 
                        FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom
                        | FontFlags.StrikeOut);
                    textSize1 = Drawing.MeasureText(
                        s, 
                        "Arial", 
                        new Vector2((float)(this.Height * 0.51), 20), 
                        FontFlags.AntiAlias);
                    textPos1 = this.Position + new Vector2(5, (float)((this.Height * 0.5) - (textSize1.Y * 0.5)));
                    Drawing.DrawText(
                        s, 
                        textPos1, 
                        new Vector2((float)(this.Height * 0.51), 20), 
                        this.FontColor, 
                        FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom
                        | FontFlags.StrikeOut);
                    break;

                case MenuValueType.StringList:
                    MenuUtils.DrawBoxBordered(
                        this.Position.X, 
                        this.Position.Y, 
                        this.Width, 
                        this.Height, 
                        1, 
                        abg, 
                        new Color(20, 20, 20, 200));
                    Drawing.DrawRect(
                        this.Position, 
                        new Vector2(this.Width, this.Height), 
                        Utils.IsUnderRectangle(
                            Game.MouseScreenPosition, 
                            this.Position.X, 
                            this.Position.Y, 
                            this.Width, 
                            this.Height)
                            ? new Color(30, 19, 5, 220)
                            : new Color(10, 10, 5, 210));
                    var slVal = this.GetValue<StringList>();
                    var t = slVal.SList[slVal.SelectedIndex];

                    MenuDrawHelper.DrawArrow(
                        true, 
                        this.Position + new Vector2(this.Width - (this.Height * 2), 0), 
                        this, 
                        System.Drawing.Color.Black);
                    MenuDrawHelper.DrawArrow(
                        false, 
                        this.Position + new Vector2(this.Width - this.Height, 0), 
                        this, 
                        System.Drawing.Color.Black);

                    textSize = Drawing.MeasureText(
                        MultiLanguage._(t), 
                        "Arial", 
                        new Vector2((float)(this.Height / 2.05), (this.Width / 2) + 10), 
                        FontFlags.AntiAlias);
                    textPos = this.Position
                              + new Vector2(
                                    -(this.Height * 2) + this.Width - textSize.X - 5, 
                                    (float)((this.Height * 0.5) - (textSize.Y * 0.5)));
                    Drawing.DrawText(
                        MultiLanguage._(t), 
                        textPos, 
                        new Vector2((float)(this.Height / 2.05), (this.Width / 2) + 10), 
                        new Color(230, 210, 200, 225), 
                        FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom
                        | FontFlags.StrikeOut);
                    textSize1 = Drawing.MeasureText(
                        s, 
                        "Arial", 
                        new Vector2((float)(this.Height * 0.51), 20), 
                        FontFlags.AntiAlias);
                    textPos1 = this.Position + new Vector2(5, (float)((this.Height * 0.5) - (textSize1.Y * 0.5)));
                    Drawing.DrawText(
                        s, 
                        textPos1, 
                        new Vector2((float)(this.Height * 0.51), 20), 
                        this.FontColor, 
                        FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom
                        | FontFlags.StrikeOut);
                    break;

                case MenuValueType.AbilityToggler:
                    MenuUtils.DrawBoxBordered(
                        this.Position.X, 
                        this.Position.Y, 
                        this.Width, 
                        this.Height, 
                        1, 
                        abg, 
                        new Color(20, 20, 20, 200));
                    Drawing.DrawRect(
                        this.Position, 
                        new Vector2(this.Width, this.Height), 
                        Utils.IsUnderRectangle(
                            Game.MouseScreenPosition, 
                            this.Position.X, 
                            this.Position.Y, 
                            this.Width, 
                            this.Height)
                            ? new Color(30, 19, 5, 220)
                            : new Color(10, 10, 5, 210));
                    var width = 0f;
                    var basePosition = this.Position + new Vector2(this.Width - this.Height, 0);
                    var size = new Vector2(this.Height - 6, this.Height - 6);
                    var dictionary = new Dictionary<string, bool>(this.GetValue<AbilityToggler>().Dictionary);
                    var positionDictionary =
                        new Dictionary<string, float[]>(this.GetValue<AbilityToggler>().PositionDictionary);
                    var textureDictionary = new Dictionary<string, DotaTexture>(Menu.TextureDictionary);

                    foreach (var v in new Dictionary<string, bool>(dictionary))
                    {
                        positionDictionary[v.Key][0] = basePosition.X - width;
                        positionDictionary[v.Key][1] = basePosition.Y;
                        var pos = basePosition - new Vector2(width, 0);
                        alpha = Utils.IsUnderRectangle(Game.MouseScreenPosition, pos.X, pos.Y, size.X + 6, size.Y + 6)
                                    ? 35
                                    : 0;
                        Drawing.DrawRect(
                            pos, 
                            size + new Vector2(6, 6), 
                            v.Value
                                ? System.Drawing.Color.FromArgb(180 + alpha, 120 + alpha, 1 + alpha).ToSharpDxColor()
                                : System.Drawing.Color.FromArgb(37 + alpha, 37 + alpha, 37 + alpha).ToSharpDxColor());
                        if (v.Key.Contains("item"))
                        {
                            Drawing.DrawRect(
                                pos - new Vector2(-3, -3), 
                                size + new Vector2((float)(this.Height * 0.35), 0), 
                                textureDictionary[v.Key]);
                        }
                        else
                        {
                            Drawing.DrawRect(pos - new Vector2(-3, -3), size, textureDictionary[v.Key]);
                        }

                        Drawing.DrawRect(pos - new Vector2(-3, -3), size, Color.Black, true);
                        Drawing.DrawRect(pos, size + new Vector2(6, 6), Color.Black, true);

                        width += size.X + 6;
                        width += -1;
                    }

                    textSize1 = Drawing.MeasureText(
                        s, 
                        "Arial", 
                        new Vector2((float)(this.Height * 0.51), 20), 
                        FontFlags.AntiAlias);
                    textPos1 = this.Position + new Vector2(5, (float)((this.Height * 0.5) - (textSize1.Y * 0.5)));
                    Drawing.DrawText(
                        s, 
                        textPos1, 
                        new Vector2((float)(this.Height * 0.51), 20), 
                        this.FontColor, 
                        FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom
                        | FontFlags.StrikeOut);
                    break;

                case MenuValueType.HeroToggler:
                    MenuUtils.DrawBoxBordered(
                        this.Position.X, 
                        this.Position.Y, 
                        this.Width, 
                        this.Height, 
                        1, 
                        abg, 
                        new Color(20, 20, 20, 200));
                    Drawing.DrawRect(
                        this.Position, 
                        new Vector2(this.Width, this.Height), 
                        Utils.IsUnderRectangle(
                            Game.MouseScreenPosition, 
                            this.Position.X, 
                            this.Position.Y, 
                            this.Width, 
                            this.Height)
                            ? new Color(30, 19, 5, 220)
                            : new Color(10, 10, 5, 210));
                    width = 0f;
                    basePosition = this.Position + new Vector2(this.Width - this.Height - 16, 0);
                    size = new Vector2(this.Height + 10, this.Height - 6);
                    dictionary = new Dictionary<string, bool>(this.GetValue<HeroToggler>().Dictionary);
                    positionDictionary = new Dictionary<string, float[]>(
                        this.GetValue<HeroToggler>().PositionDictionary);
                    textureDictionary = new Dictionary<string, DotaTexture>(Menu.TextureDictionary);

                    foreach (var v in dictionary)
                    {
                        positionDictionary[v.Key][0] = basePosition.X - width;
                        positionDictionary[v.Key][1] = basePosition.Y;
                        var pos = basePosition - new Vector2(width, 0);
                        alpha = Utils.IsUnderRectangle(Game.MouseScreenPosition, pos.X, pos.Y, size.X + 6, size.Y + 6)
                                    ? 35
                                    : 0;
                        Drawing.DrawRect(
                            pos, 
                            size + new Vector2(6, 6), 
                            v.Value
                                ? System.Drawing.Color.FromArgb(180 + alpha, 120 + alpha, 1 + alpha).ToSharpDxColor()
                                : System.Drawing.Color.FromArgb(37 + alpha, 37 + alpha, 37 + alpha).ToSharpDxColor());
                        Drawing.DrawRect(pos - new Vector2(-3, -3), size, textureDictionary[v.Key]);
                        Drawing.DrawRect(pos - new Vector2(-3, -3), size, Color.Black, true);
                        Drawing.DrawRect(pos, size + new Vector2(6, 6), Color.Black, true);

                        width += size.X + 6;
                        width += -1;
                    }

                    textSize1 = Drawing.MeasureText(
                        s, 
                        "Arial", 
                        new Vector2((float)(this.Height * 0.51), 20), 
                        FontFlags.AntiAlias);
                    textPos1 = this.Position + new Vector2(5, (float)((this.Height * 0.5) - (textSize1.Y * 0.5)));
                    Drawing.DrawText(
                        s, 
                        textPos1, 
                        new Vector2((float)(this.Height * 0.51), 20), 
                        this.FontColor, 
                        FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom
                        | FontFlags.StrikeOut);
                    break;
            }

            if (!string.IsNullOrEmpty(this.Tooltip))
            {
                MenuDrawHelper.DrawToolTipButton(new Vector2(this.Position.X + this.Width, this.Position.Y), this);
            }
        }

        /// <summary>
        ///     The is inside.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        internal bool IsInside(Vector2 position)
        {
            return Utils.IsUnderRectangle(
                position, 
                this.Position.X, 
                this.Position.Y, 
                !string.IsNullOrEmpty(this.Tooltip) ? this.Width + this.Height : this.Width, 
                this.Height);
        }

        /// <summary>
        ///     The on receive message.
        /// </summary>
        /// <param name="message">
        ///     The message.
        /// </param>
        /// <param name="cursorPos">
        ///     The cursor pos.
        /// </param>
        /// <param name="key">
        ///     The key.
        /// </param>
        /// <param name="wargs">
        ///     The wargs.
        /// </param>
        internal void OnReceiveMessage(Utils.WindowsMessages message, Vector2 cursorPos, uint key, WndEventArgs wargs)
        {
            if (message == Utils.WindowsMessages.WM_MOUSEMOVE)
            {
                if (this.Visible && this.IsInside(cursorPos))
                {
                    if (cursorPos.X > this.Position.X + this.Width - (this.Height * 2)
                        && cursorPos.X < this.Position.X + this.Width - this.Height)
                    {
                        this.ShowTooltip();
                    }
                }
                else
                {
                    this.ShowTooltip(true);
                }
            }

            switch (this.ValueType)
            {
                case MenuValueType.Boolean:

                    if (message != Utils.WindowsMessages.WM_LBUTTONDOWN)
                    {
                        return;
                    }

                    if (!this.Visible)
                    {
                        return;
                    }

                    if (!this.IsInside(cursorPos))
                    {
                        return;
                    }

                    if (cursorPos.X > this.Position.X + this.Width)
                    {
                        break;
                    }

                    this.SetValue(!this.GetValue<bool>());

                    break;

                case MenuValueType.Slider:

                    if (!this.Visible)
                    {
                        this.Interacting = false;
                        return;
                    }

                    if ((message == Utils.WindowsMessages.WM_MOUSEMOVE && this.Interacting)
                        || (message == Utils.WindowsMessages.WM_LBUTTONDOWN && !this.Interacting
                            && this.IsInside(cursorPos)))
                    {
                        var val = this.GetValue<Slider>();
                        var t = val.MinValue
                                + (cursorPos.X - this.Position.X) * (val.MaxValue - val.MinValue) / this.Width;
                        val.Value = (int)t;
                        this.SetValue(val);
                    }

                    if (message != Utils.WindowsMessages.WM_LBUTTONDOWN && message != Utils.WindowsMessages.WM_LBUTTONUP)
                    {
                        return;
                    }

                    if (!this.IsInside(cursorPos) && message == Utils.WindowsMessages.WM_LBUTTONDOWN)
                    {
                        return;
                    }

                    this.Interacting = message == Utils.WindowsMessages.WM_LBUTTONDOWN;
                    break;
                case MenuValueType.KeyBind:

                    if (!Game.IsChatOpen)
                    {
                        switch (message)
                        {
                            case Utils.WindowsMessages.WM_KEYDOWN:
                                var val = this.GetValue<KeyBind>();
                                if (key == val.Key)
                                {
                                    if (val.Type == KeyBindType.Press)
                                    {
                                        if (!val.Active)
                                        {
                                            val.Active = true;
                                            this.SetValue(val);
                                        }
                                    }

                                    if (wargs != null && Menu.Root.Item("EnsageSharp.Common.BlockKeys").GetValue<bool>())
                                    {
                                        wargs.Process = false;
                                    }
                                }

                                break;
                            case Utils.WindowsMessages.WM_KEYUP:

                                var val2 = this.GetValue<KeyBind>();
                                if (key == val2.Key)
                                {
                                    if (val2.Type == KeyBindType.Press)
                                    {
                                        val2.Active = false;
                                        this.SetValue(val2);
                                    }
                                    else
                                    {
                                        val2.Active = !val2.Active;
                                        this.SetValue(val2);
                                    }

                                    if (wargs != null && Menu.Root.Item("EnsageSharp.Common.BlockKeys").GetValue<bool>())
                                    {
                                        wargs.Process = false;
                                    }
                                }

                                break;
                        }
                    }

                    if (message == Utils.WindowsMessages.WM_KEYUP && this.Interacting)
                    {
                        var val = this.GetValue<KeyBind>();
                        val.Key = key;
                        this.SetValue(val);
                        this.Interacting = false;
                        if (wargs != null)
                        {
                            wargs.Process = false;
                        }
                    }

                    if (!this.Visible)
                    {
                        return;
                    }

                    if (message != Utils.WindowsMessages.WM_LBUTTONDOWN)
                    {
                        return;
                    }

                    if (!this.IsInside(cursorPos))
                    {
                        return;
                    }

                    if (cursorPos.X > this.Position.X + this.Width)
                    {
                        break;
                    }

                    var rpos = this.Position + new Vector2(this.Width - (this.Height * 2), this.Height / 6);
                    var rsize = new Vector2((float)(this.Height * 1.9), this.Height - ((this.Height / 6) * 2));

                    if (Utils.IsUnderRectangle(Game.MouseScreenPosition, rpos.X, rpos.Y, rsize.X, rsize.Y))
                    {
                        this.Interacting = !this.Interacting;
                    }

                    break;
                case MenuValueType.StringList:
                    if (!this.Visible)
                    {
                        return;
                    }

                    if (message != Utils.WindowsMessages.WM_LBUTTONDOWN)
                    {
                        return;
                    }

                    if (!this.IsInside(cursorPos))
                    {
                        return;
                    }

                    if (cursorPos.X > this.Position.X + this.Width)
                    {
                        break;
                    }

                    var slVal = this.GetValue<StringList>();
                    if (cursorPos.X > this.Position.X + this.Width - this.Height)
                    {
                        slVal.SelectedIndex = slVal.SelectedIndex == slVal.SList.Length - 1
                                                  ? 0
                                                  : slVal.SelectedIndex + 1;
                        this.SetValue(slVal);
                    }
                    else if (cursorPos.X > this.Position.X + this.Width - 2 * this.Height)
                    {
                        slVal.SelectedIndex = slVal.SelectedIndex == 0
                                                  ? slVal.SList.Length - 1
                                                  : slVal.SelectedIndex - 1;
                        this.SetValue(slVal);
                    }

                    break;

                case MenuValueType.AbilityToggler:
                    if (!this.Visible)
                    {
                        return;
                    }

                    if (message != Utils.WindowsMessages.WM_LBUTTONDOWN)
                    {
                        return;
                    }

                    var positionDictionary = this.GetValue<AbilityToggler>().PositionDictionary;
                    var dictionary = this.GetValue<AbilityToggler>().Dictionary;
                    foreach (var v in from v in dictionary
                                      let pos = new Vector2(positionDictionary[v.Key][0], positionDictionary[v.Key][1])
                                      where
                                          Utils.IsUnderRectangle(
                                              cursorPos, 
                                              pos.X, 
                                              pos.Y, 
                                              this.Height - 2, 
                                              this.Height - 2)
                                      select v)
                    {
                        this.GetValue<AbilityToggler>().Dictionary[v.Key] = !dictionary[v.Key];
                        break;
                    }

                    this.SetValue(new AbilityToggler(dictionary));
                    break;

                case MenuValueType.HeroToggler:
                    if (!this.Visible)
                    {
                        return;
                    }

                    if (message != Utils.WindowsMessages.WM_LBUTTONDOWN)
                    {
                        return;
                    }

                    positionDictionary = this.GetValue<HeroToggler>().PositionDictionary;
                    dictionary = this.GetValue<HeroToggler>().Dictionary;
                    foreach (var v in from v in dictionary
                                      let pos = new Vector2(positionDictionary[v.Key][0], positionDictionary[v.Key][1])
                                      where
                                          Utils.IsUnderRectangle(
                                              cursorPos, 
                                              pos.X, 
                                              pos.Y, 
                                              this.Height + 15, 
                                              this.Height - 2)
                                      select v)
                    {
                        this.GetValue<HeroToggler>().Dictionary[v.Key] = !dictionary[v.Key];
                        break;
                    }

                    this.SetValue(
                        new HeroToggler(
                            dictionary, 
                            this.GetValue<HeroToggler>().UseEnemyHeroes, 
                            this.GetValue<HeroToggler>().UseAllyHeroes));
                    break;
            }
        }

        /// <summary>
        ///     The save to file.
        /// </summary>
        /// <param name="dics">
        ///     The dics.
        /// </param>
        internal void SaveToFile(ref Dictionary<string, Dictionary<string, byte[]>> dics)
        {
            if (!this.dontSave)
            {
                if (!dics.ContainsKey(this.SaveFileName))
                {
                    dics[this.SaveFileName] = new Dictionary<string, byte[]>();
                }

                dics[this.SaveFileName][this.SaveKey] = this.serialized;
            }
        }

        #endregion
    }
}