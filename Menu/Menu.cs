// <copyright file="Menu.cs" company="EnsageSharp">
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
    using System.Drawing;
    using System.Linq;
    using System.Reflection;

    using Ensage.Common.Menu.Draw;
    using Ensage.Common.Menu.Transitions;
    using Ensage.Common.Objects;
    using Ensage.Common.Objects.DrawObjects;

    using SharpDX;

    using Color = SharpDX.Color;

    /// <summary>
    ///     The menu.
    /// </summary>
    public class Menu : DraggableItem
    {
        #region Static Fields

        /// <summary>
        ///     The root menus.
        /// </summary>
        internal static readonly Dictionary<string, Menu> RootMenus = new Dictionary<string, Menu>();

        /// <summary>
        ///     The menu position dictionary.
        /// </summary>
        private static readonly Dictionary<string, Vector2> MenuPositionDictionary = new Dictionary<string, Vector2>();

        /// <summary>The panel text.</summary>
        private static readonly DrawText PanelText;

        /// <summary>
        ///     The root menus draggable.
        /// </summary>
        private static readonly List<DraggableItem> RootMenusDraggable = new List<DraggableItem>();

        /// <summary>
        ///     The menu count.
        /// </summary>
        private static int menuCount;

        #endregion

        #region Fields

        /// <summary>
        ///     The cached menu count.
        /// </summary>
        public int CachedMenuCount = 2;

        /// <summary>
        ///     The children.
        /// </summary>
        public List<Menu> Children = new List<Menu>();

        /// <summary>
        ///     The items.
        /// </summary>
        public List<MenuItem> Items = new List<MenuItem>();

        /// <summary>
        ///     The transition.
        /// </summary>
        private readonly Transition transition;

        /// <summary>
        ///     The hovered.
        /// </summary>
        private bool hovered;

        /// <summary>
        ///     The visible.
        /// </summary>
        private bool visible;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="Menu" /> class.
        /// </summary>
        static Menu()
        {
            if (MenuVariables.DragAndDropDictionary == null)
            {
                MenuVariables.DragAndDropDictionary = new Dictionary<string, DragAndDrop>();
            }

            MenuPanel = new DrawRect(Color.Black);
            PanelText = new DrawText { Text = "EnsageSharp Menu", FontFlags = FontFlags.AntiAlias };
            TextureDictionary = new Dictionary<string, DotaTexture>();
            ItemDictionary = new Dictionary<string, MenuItem>();
            Drawing.OnDraw += OnDraw;
            ActivateCommonMenu();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Menu" /> class.
        /// </summary>
        /// <param name="displayName">
        ///     The display name.
        /// </param>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <param name="isRootMenu">
        ///     The is root menu.
        /// </param>
        /// <param name="textureName">
        ///     The texture name.
        /// </param>
        /// <param name="showTextWithTexture">
        ///     The show text with texture.
        /// </param>
        public Menu(
            string displayName,
            string name,
            bool isRootMenu = false,
            string textureName = null,
            bool showTextWithTexture = false)
            : base(20)
        {
            this.DisplayName = displayName;
            this.Name = name;
            this.IsRootMenu = isRootMenu;
            this.Style = FontStyle.Regular;
            this.Color = Root?.SelectedTheme.MenuDefaultTextColor ?? new Color(195, 186, 173, 255);
            this.TextureName = textureName;
            this.ShowTextWithTexture = showTextWithTexture;
            this.transition = new ExpoEaseInOut(0.25);

            AppDomain.CurrentDomain.DomainUnload += delegate { this.SaveAll(); };
            AppDomain.CurrentDomain.ProcessExit += delegate { this.SaveAll(); };
            Events.OnClose += delegate { this.SaveAll(); };
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="Menu" /> class.
        /// </summary>
        ~Menu()
        {
            var rootName = Assembly.GetCallingAssembly().GetName().Name + "." + this.Name;
            if (RootMenus.ContainsKey(rootName))
            {
                this.RemoveFromMainMenu();
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     The color.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        ///     The display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///     Gets a value indicating whether is open.
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return this.Children.Any(x => x.Visible) || this.Items.Any(x => x.Visible);
            }
        }

        /// <summary>
        ///     The is root menu.
        /// </summary>
        public bool IsRootMenu { get; set; }

        /// <summary>
        ///     The name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The parent.
        /// </summary>
        public Menu Parent { get; set; }

        /// <summary>
        ///     Gets the real position.
        /// </summary>
        public override Vector2 RealPosition
        {
            get
            {
                var n = this.Name + this.DisplayName;
                if (this.Parent != null)
                {
                    n += this.Parent.Name;
                }

                if (!Utils.SleepCheck(n))
                {
                    return MenuPositionDictionary[n];
                }

                int xOffset;

                if (this.Parent != null)
                {
                    xOffset = (int)(this.Parent.Position.X + this.Parent.Width);
                }
                else
                {
                    xOffset = (int)this.MyBasePosition.X;
                }

                var basePos = new Vector2(0, this.MyBasePosition.Y);

                if (this.Parent != null)
                {
                    basePos = new Vector2(0, this.Parent.Position.Y);
                }

                var pos = basePos + new Vector2(xOffset, 0) + this.YLevel * new Vector2(0, MenuSettings.MenuItemHeight);
                if (!MenuPositionDictionary.ContainsKey(n))
                {
                    MenuPositionDictionary.Add(n, pos);
                }
                else
                {
                    MenuPositionDictionary[n] = pos;
                }

                Utils.Sleep(0, n);
                return pos;
            }
        }

        /// <summary>
        ///     The show text with texture.
        /// </summary>
        public bool ShowTextWithTexture { get; set; }

        /// <summary>
        ///     The style.
        /// </summary>
        public FontStyle Style { get; set; }

        /// <summary>
        ///     The texture name.
        /// </summary>
        public string TextureName { get; set; }

        /// <summary>
        ///     The unique id.
        /// </summary>
        public string UniqueId { get; set; }

        #endregion

        #region Properties

        /// <summary>
        ///     The item dictionary.
        /// </summary>
        internal static Dictionary<string, MenuItem> ItemDictionary { get; set; }

        /// <summary>Gets or sets the menu panel.</summary>
        internal static DrawRect MenuPanel { get; set; }

        /// <summary>
        ///     The root.
        /// </summary>
        internal static CommonMenu Root { get; set; }

        /// <summary>
        ///     The texture dictionary.
        /// </summary>
        internal static Dictionary<string, DotaTexture> TextureDictionary { get; set; }

        /// <summary>
        ///     Gets the children menu width.
        /// </summary>
        internal int ChildrenMenuWidth
        {
            get
            {
                var result = this.Children.Select(item => item.NeededWidth).Concat(new[] { 0 }).Max();

                return this.Items.Select(item => item.NeededWidth).Concat(new[] { result }).Max();
            }
        }

        /// <summary>
        ///     Gets the height.
        /// </summary>
        internal override int Height
        {
            get
            {
                if (this.ResizeTransition.Moving || this.BeingDragged)
                {
                    return (int)this.Size.X;
                }

                return MenuSettings.MenuItemHeight;
            }
        }

        /// <summary>
        ///     Gets the menu count.
        /// </summary>
        internal int MenuCount
        {
            get
            {
                return this.OrderNumber;
            }
        }

        /// <summary>
        ///     Gets the my base position.
        /// </summary>
        internal Vector2 MyBasePosition
        {
            get
            {
                if (this.IsRootMenu || this.Parent == null)
                {
                    return MenuSettings.BasePosition + this.OrderNumber * new Vector2(0, MenuSettings.MenuItemHeight);
                }

                return this.Parent.MyBasePosition;
            }
        }

        /// <summary>
        ///     Gets the needed width.
        /// </summary>
        internal int NeededWidth
        {
            get
            {
                var n = this.Name + this.DisplayName + "Width";
                if (!Utils.SleepCheck(n))
                {
                    return (int)MenuPositionDictionary[n].X;
                }

                var bonus = this.Height;
                if (this.TextureName == null || this.ShowTextWithTexture)
                {
                    bonus +=
                        (int)
                        Drawing.MeasureText(
                            MultiLanguage._(this.DisplayName),
                            "Arial",
                            new Vector2((float)(this.Height * 0.48), 100),
                            FontFlags.None).X;
                }

                if (this.TextureName != null)
                {
                    var tName = this.TextureName;
                    if (tName.Contains("npc_dota_hero"))
                    {
                        bonus += (int)(this.Height * 1.5);
                    }
                    else if (tName.Contains("item_"))
                    {
                        bonus += (int)(this.Height * 0.35);
                    }
                    else
                    {
                        bonus += (int)(this.Height * 0.35);
                    }
                }

                if (!MenuPositionDictionary.ContainsKey(n))
                {
                    MenuPositionDictionary.Add(n, new Vector2(this.Height + bonus));
                }
                else
                {
                    MenuPositionDictionary[n] = new Vector2(this.Height + bonus);
                }

                Utils.Sleep(20000, n);
                return this.Height + bonus;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether visible.
        /// </summary>
        internal bool Visible
        {
            get
            {
                if (!MenuSettings.DrawMenu)
                {
                    return false;
                }

                return this.IsRootMenu || this.visible;
            }

            set
            {
                this.visible = value;

                // Hide all the children
                if (this.visible)
                {
                    return;
                }

                foreach (var schild in this.Children)
                {
                    schild.Visible = false;
                }

                foreach (var sitem in this.Items)
                {
                    sitem.Visible = false;
                }
            }
        }

        /// <summary>
        ///     Gets the width.
        /// </summary>
        internal override int Width
        {
            get
            {
                if (this.ResizeTransition.Moving || this.BeingDragged)
                {
                    return (int)this.Size.Y;
                }

                return this.Parent != null ? this.Parent.ChildrenMenuWidth : MenuSettings.MenuWidth + this.Height;
            }
        }

        /// <summary>
        ///     Gets the x level.
        /// </summary>
        internal int XLevel
        {
            get
            {
                var result = 0;
                var m = this;
                while (m.Parent != null)
                {
                    m = m.Parent;
                    result++;
                }

                return result;
            }
        }

        /// <summary>
        ///     Gets the y level.
        /// </summary>
        internal int YLevel
        {
            get
            {
                if (this.IsRootMenu || this.Parent == null)
                {
                    return 0;
                }

                return this.Parent.Children.TakeWhile(test => test.Name != this.Name).Count();
            }
        }

        /// <summary>
        ///     Gets the position.
        /// </summary>
        protected internal override Vector2 Position
        {
            get
            {
                if (this.DragTransition.Moving)
                {
                    return this.DraggedPosition;
                }

                if (this.BeingDragged)
                {
                    return this.DraggedPosition;
                }

                return this.RealPosition;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The get menu.
        /// </summary>
        /// <param name="assemblyname">
        ///     The assembly name.
        /// </param>
        /// <param name="menuname">
        ///     The menu name.
        /// </param>
        /// <returns>
        ///     The <see cref="Menu" />.
        /// </returns>
        public static Menu GetMenu(string assemblyname, string menuname)
        {
            var menu = RootMenus.FirstOrDefault(x => x.Key == assemblyname + "." + menuname).Value;
            return menu;
        }

        /// <summary>
        ///     The get value globally.
        /// </summary>
        /// <param name="assemblyname">
        ///     The assembly name.
        /// </param>
        /// <param name="menuname">
        ///     The menu name.
        /// </param>
        /// <param name="itemname">
        ///     The item name.
        /// </param>
        /// <param name="submenu">
        ///     The submenu.
        /// </param>
        /// <returns>
        ///     The <see cref="MenuItem" />.
        /// </returns>
        public static MenuItem GetValueGlobally(
            string assemblyname,
            string menuname,
            string itemname,
            string submenu = null)
        {
            var menu = RootMenus.FirstOrDefault(x => x.Key == assemblyname + "." + menuname).Value;

            if (submenu != null)
            {
                menu = menu.SubMenu(submenu);
            }

            var menuitem = menu.Item(itemname);

            return menuitem;
        }

        /// <summary>
        ///     The send message.
        /// </summary>
        /// <param name="key">
        ///     The key.
        /// </param>
        /// <param name="message">
        ///     The message.
        /// </param>
        public static void SendMessage(uint key, Utils.WindowsMessages message)
        {
            foreach (var menu in RootMenus)
            {
                menu.Value.OnReceiveMessage(message, Game.MouseScreenPosition, key);
            }
        }

        /// <summary>
        ///     The add item.
        /// </summary>
        /// <param name="item">
        ///     The item.
        /// </param>
        /// <returns>
        ///     The <see cref="MenuItem" />.
        /// </returns>
        public MenuItem AddItem(MenuItem item)
        {
            item.Parent = this;
            item.Visible = this.IsOpen;
            this.Items.Add(item);
            if (item.ValueType == MenuValueType.HeroToggler)
            {
                DelayAction.Add(2000, item.SetHeroToggler);
            }

            return item;
        }

        /// <summary>
        ///     The add sub menu.
        /// </summary>
        /// <param name="subMenu">
        ///     The sub menu.
        /// </param>
        /// <returns>
        ///     The <see cref="Menu" />.
        /// </returns>
        public Menu AddSubMenu(Menu subMenu)
        {
            subMenu.Parent = this;
            subMenu.Visible = this.Children.Count > 0 && this.Children[0].Visible;
            this.Children.Add(subMenu);
            return subMenu;
        }

        /// <summary>
        ///     The add to main menu.
        /// </summary>
        public void AddToMainMenu()
        {
            var rootName = Assembly.GetCallingAssembly().GetName().Name + "." + this.Name;
            if (!RootMenus.ContainsKey(rootName))
            {
                RootMenus.Add(rootName, this);
                if (!(this is CommonMenu))
                {
                    RootMenusDraggable.Add(this);
                }

                AppDomain.CurrentDomain.DomainUnload += (sender, args) => this.UnloadMenuState();
                ObjectManager.OnAddEntity += this.ObjectMgr_OnAddEntity;
                Game.OnWndProc += this.Game_OnWndProc;
            }

            this.InitMenuState(Assembly.GetCallingAssembly().GetName().Name);

            DelayAction.Add(2000, this.SetHeroTogglers);
            var bonus = 0f;
            if (this.TextureName != null)
            {
                var tName = this.TextureName;
                if (tName.Contains("npc_dota_hero"))
                {
                    bonus = (int)(this.Height * 1.5);
                }
                else if (tName.Contains("item_"))
                {
                    bonus = (int)(this.Height * 0.9);
                }
                else
                {
                    bonus = (int)(this.Height * 0.9);
                }
            }

            MenuSettings.RootMenuWidthIncrease =
                (int)
                Math.Max(
                    MenuSettings.RootMenuWidthIncrease,
                    Drawing.MeasureText(
                        MultiLanguage._(this.DisplayName),
                        "Arial",
                        new Vector2((float)(this.Height * 0.48), 100),
                        FontFlags.AntiAlias).X + bonus);
            this.OrderNumber = menuCount;
            menuCount++;
        }

        /// <summary>
        ///     The drawing_ on draw.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        public void Drawing_OnDraw(EventArgs args)
        {
            if (!Game.IsInGame)
            {
                return;
            }

            if (!this.Visible)
            {
                return;
            }

            var wasHovered = this.hovered;
            this.hovered = Utils.IsUnderRectangle(
                Game.MouseScreenPosition,
                this.Position.X,
                this.Position.Y,
                this.Width,
                this.Height);
            if (!wasHovered && this.hovered)
            {
                this.transition.Start(0, this.Height);
            }
            else if (wasHovered && !this.hovered)
            {
                this.transition.Start(0, this.Height);
            }

            var add = this.hovered
                          ? this.transition.GetValue() * 0.1
                          : this.transition.GetValue() > 0 || this.transition.Moving
                              ? (this.Height - this.transition.GetValue()) * 0.1
                              : 0;
            MenuUtils.MainMenuDraw(this, add);
        }

        /// <summary>
        ///     The item.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <param name="makeChampionUniq">
        ///     The make champion unique.
        /// </param>
        /// <returns>
        ///     The <see cref="MenuItem" />.
        /// </returns>
        public MenuItem Item(string name, bool makeChampionUniq = false)
        {
            if (makeChampionUniq && ObjectManager.LocalHero != null)
            {
                name = ObjectManager.LocalHero.StoredName() + name;
            }

            var id = this.Name + name + (this.Parent != null ? this.Parent.Name : string.Empty);
            MenuItem tempItem;
            if (ItemDictionary.TryGetValue(id, out tempItem))
            {
                return tempItem;
            }

            tempItem = this.Items.FirstOrDefault(x => x.Name == name)
                       ?? (from subMenu in this.Children where subMenu.Item(name) != null select subMenu.Item(name))
                       .FirstOrDefault();
            if (tempItem != null)
            {
                ItemDictionary.Add(id, tempItem);
            }

            return tempItem;
        }

        /// <summary>
        ///     The remove from main menu.
        /// </summary>
        public void RemoveFromMainMenu()
        {
            try
            {
                var rootName = Assembly.GetCallingAssembly().GetName().Name + "." + this.Name;
                if (RootMenus.ContainsKey(rootName))
                {
                    RootMenus.Remove(rootName);
                    if (!(this is CommonMenu))
                    {
                        RootMenusDraggable.Remove(this);
                    }

                    Drawing.OnDraw -= this.Drawing_OnDraw;
                    Game.OnWndProc -= this.Game_OnWndProc;
                    this.UnloadMenuState();
                    menuCount--;
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        ///     The remove sub menu.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        public void RemoveSubMenu(string name)
        {
            var subMenu = this.Children.FirstOrDefault(x => x.Name == name);
            if (subMenu == null)
            {
                return;
            }

            subMenu.Parent = null;
            this.Children.Remove(subMenu);
        }

        /// <summary>
        ///     Sets custom text color
        /// </summary>
        /// <param name="fontColor">
        ///     The font color.
        /// </param>
        /// <returns>
        ///     The <see cref="Menu" />.
        /// </returns>
        public Menu SetFontColor(Color fontColor)
        {
            this.Color = fontColor;
            return this;
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
        ///     The <see cref="Menu" />.
        /// </returns>
        [Obsolete("SetFontStyle is deprecated, please use SetFontColor instead")]
        public Menu SetFontStyle(FontStyle fontStyle = FontStyle.Regular, Color? fontColor = null)
        {
            this.Style = fontStyle;
            this.Color = fontColor ?? Color.White;
            return this;
        }

        /// <summary>
        ///     The sub menu.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="Menu" />.
        /// </returns>
        public Menu SubMenu(string name)
        {
            // Search in submenus and if it doesn't exist add it.
            var subMenu = this.Children.FirstOrDefault(sm => sm.Name == name);
            return subMenu ?? this.AddSubMenu(new Menu(name, name));
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The game_ on wnd proc.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        internal void Game_OnWndProc(WndEventArgs args)
        {
            if (!Game.IsInGame)
            {
                return;
            }

            if (this.IsRootMenu && !(this is CommonMenu))
            {
                if (!this.Visible || Config.DisableDrawings)
                {
                    this.OnReceiveMessage(
                        (Utils.WindowsMessages)args.Msg,
                        Game.MouseScreenPosition,
                        (uint)args.WParam,
                        args);
                    return;
                }

                this.DraggableOnReceiveMessage(
                    (Utils.WindowsMessages)args.Msg,
                    Game.MouseScreenPosition,
                    (uint)args.WParam,
                    RootMenusDraggable,
                    args);
                return;
            }

            this.OnReceiveMessage((Utils.WindowsMessages)args.Msg, Game.MouseScreenPosition, (uint)args.WParam, args);
        }

        /// <summary>
        ///     The recursive save all.
        /// </summary>
        /// <param name="dics">
        ///     The dics.
        /// </param>
        internal void RecursiveSaveAll(ref Dictionary<string, Dictionary<string, byte[]>> dics)
        {
            foreach (var child in this.Children)
            {
                child.RecursiveSaveAll(ref dics);
            }

            foreach (var item in this.Items)
            {
                item.SaveToFile(ref dics);
            }
        }

        /// <summary>
        ///     The save all.
        /// </summary>
        internal void SaveAll()
        {
            var dic = new Dictionary<string, Dictionary<string, byte[]>>();
            this.RecursiveSaveAll(ref dic);

            foreach (var dictionary in dic)
            {
                var dicToSave = SavedSettings.Load(dictionary.Key) ?? new Dictionary<string, byte[]>();

                foreach (var entry in dictionary.Value)
                {
                    dicToSave[entry.Key] = entry.Value;
                }

                SavedSettings.Save(dictionary.Key, dicToSave);
            }
        }

        /// <summary>
        ///     The set hero togglers.
        /// </summary>
        internal void SetHeroTogglers()
        {
            foreach (var child in this.Children)
            {
                child.SetHeroTogglers();
            }

            foreach (var item in this.Items)
            {
                item.SetHeroToggler();
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
        protected override bool IsInside(Vector2 position)
        {
            return Utils.IsUnderRectangle(position, this.Position.X, this.Position.Y, this.Width, this.Height);
        }

        /// <summary>
        ///     The on receive message.
        /// </summary>
        /// <param name="message">
        ///     The message.
        /// </param>
        /// <param name="cursorPos">
        ///     The cursor position.
        /// </param>
        /// <param name="key">
        ///     The key.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        protected override void OnReceiveMessage(
            Utils.WindowsMessages message,
            Vector2 cursorPos,
            uint key,
            WndEventArgs args = null)
        {
            // Spread the message to the menu's children recursively
            foreach (var item in this.Items)
            {
                item.OnReceiveMessage(message, cursorPos, key, args);

                // Console.WriteLine(args != null && item.IsInside(cursorPos));
            }

            foreach (var child in this.Children)
            {
                child.OnReceiveMessage(message, cursorPos, key, args);
            }

            if (!this.Visible || Config.DisableDrawings)
            {
                return;
            }

            if (message != Utils.WindowsMessages.WM_LBUTTONUP)
            {
                return;
            }

            if (this.IsRootMenu && this.Visible)
            {
                if (cursorPos.X - MenuSettings.BasePosition.X <= MenuSettings.MenuWidth + this.Height + 4)
                {
                    var n = (cursorPos.Y - MenuSettings.BasePosition.Y) / MenuSettings.MenuItemHeight;
                    if (n < 0)
                    {
                        return;
                    }

                    n = (int)n;
                    if (this.MenuCount != n)
                    {
                        foreach (var schild in this.Children)
                        {
                            schild.Visible = false;
                        }

                        foreach (var sitem in this.Items)
                        {
                            sitem.Visible = false;
                        }
                    }
                }
            }

            if (!this.IsInside(cursorPos))
            {
                return;
            }

            if (!this.IsRootMenu && this.Parent != null)
            {
                // Close all the submenus in the level 
                foreach (var child in this.Parent.Children)
                {
                    if (child.Name == this.Name)
                    {
                        continue;
                    }

                    foreach (var schild in child.Children)
                    {
                        schild.Visible = false;
                    }

                    foreach (var sitem in child.Items)
                    {
                        sitem.Visible = false;
                    }
                }
            }

            // Hide or Show the submenus.
            foreach (var child in this.Children)
            {
                child.Visible = !child.Visible;
            }

            // Hide or Show the items.
            foreach (var item in this.Items)
            {
                item.Visible = !item.Visible;
            }
        }

        /// <summary>
        ///     The activate common menu.
        /// </summary>
        private static void ActivateCommonMenu()
        {
            if (Root != null)
            {
                return;
            }

            Root = new CommonMenu();
            Root.AddToMainMenu();
        }

        /// <summary>
        ///     The on draw.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private static void OnDraw(EventArgs args)
        {
            if (!MenuSettings.DrawMenu || Config.DisableDrawings)
            {
                return;
            }

            Menu draggedMenu = null;
            var bgsize = new Vector2(4, (float)(MenuSettings.MenuItemHeight / 1.2));
            Drawing.DrawRect(
                MenuSettings.BasePosition - new Vector2(MenuSettings.MenuItemHeight / 7, bgsize.Y - bgsize.X),
                new Vector2(
                    MenuSettings.MenuWidth + MenuSettings.MenuItemHeight + MenuSettings.MenuItemHeight / 7,
                    MenuSettings.MenuItemHeight * menuCount + bgsize.Y - bgsize.X),
                Root.SelectedTheme.RootMenuBackgroundColor);
            MenuPanel.Position = MenuSettings.BasePosition - new Vector2(MenuSettings.MenuItemHeight / 7, bgsize.Y - 1);
            MenuPanel.Size =
                new Vector2(
                    MenuSettings.MenuWidth + MenuSettings.MenuItemHeight + MenuSettings.MenuItemHeight / 7,
                    bgsize.Y);
            MenuPanel.Color = Root.SelectedTheme.TopPanelBackgroundColor;
            MenuPanel.Draw();
            PanelText.Color = Root.SelectedTheme.TopPanelTextColor;
            PanelText.TextSize = new Vector2((float)(MenuSettings.MenuItemHeight * 0.5));
            PanelText.CenterOnRectangleHorizontally(MenuPanel, (float)(MenuSettings.MenuItemHeight * 0.26));
            PanelText.Draw();

            foreach (var rootMenu in RootMenus.OrderBy(x => x.Value.OrderNumber))
            {
                if (rootMenu.Value.BeingDragged)
                {
                    draggedMenu = rootMenu.Value;
                    continue;
                }

                rootMenu.Value.Drawing_OnDraw(args);
            }

            if (draggedMenu == null)
            {
                return;
            }

            draggedMenu.Drawing_OnDraw(args);
        }

        /// <summary>
        ///     The init menu state.
        /// </summary>
        /// <param name="assemblyName">
        ///     The assembly name.
        /// </param>
        private void InitMenuState(string assemblyName)
        {
            this.UniqueId = assemblyName + "." + this.Name;
            var globalMenuList = MenuGlobals.MenuState ?? new List<string>();
            while (globalMenuList.Contains(this.UniqueId))
            {
                this.UniqueId += ".";
            }

            globalMenuList.Add(this.UniqueId);
            MenuGlobals.MenuState = globalMenuList;
        }

        /// <summary>
        ///     The object mgr_ on add entity.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void ObjectMgr_OnAddEntity(EntityEventArgs args)
        {
            DelayAction.Add(
                2000,
                () =>
                    {
                        var hero = args.Entity as Hero;
                        if (hero != null)
                        {
                            this.SetHeroTogglers();
                        }
                    });
        }

        /// <summary>
        ///     The unload menu state.
        /// </summary>
        private void UnloadMenuState()
        {
            var globalMenuList = MenuGlobals.MenuState;
            globalMenuList.Remove(this.UniqueId);
            MenuGlobals.MenuState = globalMenuList;
        }

        #endregion
    }
}