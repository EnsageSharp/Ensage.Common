#region LICENSE

/*
 Copyright 2014 - 2014 LeagueSharp
 Menu.cs is part of LeagueSharp.Common.
 
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

#region

#endregion

namespace Ensage.Common.Menu
{  
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Ensage.Common.Extensions.SharpDX;
    using Ensage.Common.Menu.NotificationData;
    using Ensage.Common.Properties;

    using SharpDX;
    using SharpDX.Direct3D9;

    using Color = System.Drawing.Color;
    using Font = SharpDX.Direct3D9.Font;

    internal class CommonMenu
    {
        #region Static Fields

        internal static Menu MenuConfig = new Menu("Ensage.Common", "Ensage.Common", true);

        #endregion

        #region Constructors and Destructors

        static CommonMenu()
        {
            MenuConfig.AddToMainMenu();
        }

        #endregion
    }

    [Serializable]
    public struct Circle
    {
        #region Fields

        public bool Active;

        public Color Color;

        public float Radius;

        #endregion

        #region Constructors and Destructors

        public Circle(bool enabled, Color color, float radius = 100)
        {
            this.Active = enabled;
            this.Color = color;
            this.Radius = radius;
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public struct Slider
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        public int MaxValue;

        /// <summary>
        /// 
        /// </summary>
        public int MinValue;

        private int value;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public Slider(int value = 0, int minValue = 0, int maxValue = 100)
        {
            this.MaxValue = Math.Max(maxValue, minValue);
            this.MinValue = Math.Min(maxValue, minValue);
            this.value = value;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public int Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = Math.Min(Math.Max(value, this.MinValue), this.MaxValue);
            }
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public struct StringList
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        public int SelectedIndex;

        /// <summary>
        /// 
        /// </summary>
        public string[] SList;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sList"></param>
        /// <param name="defaultSelectedIndex"></param>
        public StringList(string[] sList, int defaultSelectedIndex = 0)
        {
            this.SList = sList;
            this.SelectedIndex = defaultSelectedIndex;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public string SelectedValue
        {
            get
            {
                return this.SList[this.SelectedIndex];
            }
        }

        #endregion
    }

    /// <summary>
    /// Creates 
    /// </summary>
    [Serializable]
    public struct AbilityToggler
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, bool> Dictionary;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abilityDictionary"></param>
        public AbilityToggler(Dictionary<string, bool> abilityDictionary)
        {
            this.Dictionary = abilityDictionary;
            foreach (var v in this.Dictionary.Where(v => !Menu.TextureDictionary.ContainsKey(v.Key)))
            {
                Menu.TextureDictionary.Add(
                    v.Key,
                    v.Key.Substring(0, "item".Length) == "item"
                        ? Drawing.GetTexture("materials/ensage_ui/items/" + v.Key.Substring("item_".Length) + ".vmat")
                        : Drawing.GetTexture("materials/ensage_ui/spellicons/" + v.Key + ".vmat"));
                Menu.PositionDictionary.Add(v.Key,new float[]{0,0});
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public bool IsEnabled(string name)
        {
            return this.Dictionary.ContainsKey(name) && this.Dictionary[name];
        }

        /// <summary>
        /// 
        /// </summary>
        public void Remove(string name)
        {
            if (this.Dictionary.ContainsKey(name))
            {
                this.Dictionary.Remove(name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Add(string name, bool defaultValue = true)
        {
            if (!this.Dictionary.ContainsKey(name))
            {
                this.Dictionary.Add(name, defaultValue);
            }
        }

        #endregion
    }

    public enum KeyBindType
    {
        Toggle,

        Press
    }

    [Serializable]
    public struct KeyBind
    {
        #region Fields

        public bool Active;

        public uint Key;

        public KeyBindType Type;

        #endregion

        #region Constructors and Destructors

        public KeyBind(uint key, KeyBindType type, bool defaultValue = false)
        {
            this.Key = key;
            this.Active = defaultValue;
            this.Type = type;
        }

        #endregion
    }

    [Serializable]
    internal static class SavedSettings
    {
        #region Static Fields

        public static Dictionary<string, Dictionary<string, byte[]>> LoadedFiles =
            new Dictionary<string, Dictionary<string, byte[]>>();

        #endregion

        #region Public Methods and Operators

        public static byte[] GetSavedData(string name, string key)
        {
            Dictionary<string, byte[]> dic = null;

            dic = LoadedFiles.ContainsKey(name) ? LoadedFiles[name] : Load(name);

            if (dic == null)
            {
                return null;
            }
            return dic.ContainsKey(key) ? dic[key] : null;
        }

        public static Dictionary<string, byte[]> Load(string name)
        {
            try
            {
                var fileName = Path.Combine(MenuSettings.MenuMenuConfigPath, name + ".bin");
                if (File.Exists(fileName))
                {
                    return Utils.Deserialize<Dictionary<string, byte[]>>(File.ReadAllBytes(fileName));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        public static void Save(string name, Dictionary<string, byte[]> entries)
        {
            try
            {
                Directory.CreateDirectory(MenuSettings.MenuMenuConfigPath);
                var fileName = Path.Combine(MenuSettings.MenuMenuConfigPath, name + ".bin");
                File.WriteAllBytes(fileName, Utils.Serialize(entries));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion
    }

    public static class MenuGlobals
    {
        #region Static Fields

        public static bool DrawMenu;

        public static List<string> MenuState = new List<string>();

        #endregion
    }

    internal static class MenuSettings
    {
        #region Static Fields

        public static Vector2 BasePosition = new Vector2(10, 60);

        private static bool _drawTheMenu;

        #endregion

        #region Constructors and Destructors

        static MenuSettings()
        {
            Game.OnWndProc += Game_OnWndProc;
            _drawTheMenu = MenuGlobals.DrawMenu;
        }

        #endregion

        #region Public Properties

        public static Color ActiveBackgroundColor
        {
            get
            {
                return Color.FromArgb(160, 50,50,50);
            }
        }

        public static Color BackgroundColor
        {
            get
            {
                return Color.FromArgb(160, Color.Black);
            }
        }

        public static int MenuFontSize { get; set; }

        public static int MenuItemHeight
        {
            get
            {
                return 32;
            }
        }

        public static int MenuItemWidth
        {
            get
            {
                return 160;
            }
        }

        public static string MenuMenuConfigPath
        {
            get
            {
                return Path.Combine(MenuConfig.AppDataDirectory, "MenuConfig");
            }
        }

        #endregion

        #region Properties

        internal static bool DrawMenu
        {
            get
            {
                return _drawTheMenu;
            }
            set
            {
                _drawTheMenu = value;
                MenuGlobals.DrawMenu = value;
            }
        }

        #endregion

        #region Methods

        private static void Game_OnWndProc(WndEventArgs args)
        {
            if (!Game.IsInGame)
            {
                return;
            }   
            if (Game.IsChatOpen)
            {
                return;
            }
            if ((args.Msg == (uint)Utils.WindowsMessages.WM_KEYUP || args.Msg == (uint)Utils.WindowsMessages.WM_KEYDOWN)
                && args.WParam == CommonMenu.MenuConfig.Item("pressKey").GetValue<KeyBind>().Key)
            {
                DrawMenu = args.Msg == (uint)Utils.WindowsMessages.WM_KEYDOWN;
            }

            if (args.Msg == (uint)Utils.WindowsMessages.WM_KEYUP
                && args.WParam == CommonMenu.MenuConfig.Item("toggleKey").GetValue<KeyBind>().Key)
            {
                DrawMenu = !DrawMenu;
            }
        }

        #endregion
    }

    internal static class MenuDrawHelper
    {
        #region Static Fields

        internal static Font Font, FontBold;

        #endregion

        #region Constructors and Destructors

        static MenuDrawHelper()
        {
            Font = new Font(
                Drawing.Direct3DDevice9,
                new FontDescription
                    {
                        FaceName = "Arial", Height = 15, OutputPrecision = FontPrecision.Default,
                        Quality = FontQuality.Antialiased
                    });

            FontBold = new Font(
                Drawing.Direct3DDevice9,
                new FontDescription
                    {
                        FaceName = "Arial", Height = 15, OutputPrecision = FontPrecision.Default, Weight = FontWeight.Bold,
                        Quality = FontQuality.Antialiased
                    });

            //Drawing.OnPreReset += Drawing_OnPreReset;
            //Drawing.OnPostReset += DrawingOnOnPostReset;
            //AppDomain.CurrentDomain.DomainUnload += CurrentDomainOnDomainUnload;
        }

        #endregion

        #region Methods

        internal static void DrawArrow(string s, Vector2 position, MenuItem item, Color color)
        {
            MenuUtils.DrawBoxBordered(
                position.X,
                position.Y,
                item.Height,
                item.Height,
                1,
                Color.FromArgb(37, 37, 37).ToSharpDxColor(),
                color.ToSharpDxColor());

            //Font.DrawText(
            //    null,
            //    s,
            //    new Rectangle((int)(position.X), (int)item.Position.Y, item.Height, item.Height),
            //    FontDrawFlags.VerticalCenter | FontDrawFlags.Center,
            //    new ColorBGRA(255, 255, 255, 255));
            //var textSize = Drawing.MeasureText(s, "Arial", new Vector2(item.Height / 2, item.Height / 2), FontFlags.AntiAlias);
            var textPos = position + new Vector2(8, 8);
            Drawing.DrawText(
                s,
                textPos,
                new Vector2(item.Height / 2, item.Height / 2),
                Utils.IsUnderRectangle(Game.MouseScreenPosition,position.X,position.Y,item.Height,item.Height) ? Color.DarkOrange.ToSharpDxColor() : Color.White.ToSharpDxColor(),
                FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom | FontFlags.StrikeOut);
        }

        internal static void DrawOnOff(bool on, Vector2 position, MenuItem item)
        {
            var alpha = Utils.IsUnderRectangle(
                Game.MouseScreenPosition,
                position.X,
                position.Y,
                item.Height,
                item.Height)
                            ? 35
                            : 0;
            MenuUtils.DrawBoxBordered(
                position.X,
                position.Y,
                item.Height,
                item.Height,
                1f,
                on ? Color.FromArgb(180 + alpha, 120 + alpha, 1 + alpha).ToSharpDxColor() : Color.FromArgb(37 + alpha, 37 + alpha, 37 + alpha).ToSharpDxColor(),
                SharpDX.Color.Black);

            var s = on ? "ON" : "OFF";
            //Font.DrawText(
            //    null,
            //    s,
            //    new Rectangle(
            //        (int)(item.Position.X + item.Width - item.Height),
            //        (int)item.Position.Y,
            //        item.Height,
            //        item.Height),
            //    FontDrawFlags.VerticalCenter | FontDrawFlags.Center,
            //    new ColorBGRA(255, 255, 255, 255));
            var textSize = Drawing.MeasureText(
                s,
                "Arial",
                new Vector2(item.Height / 2, item.Width / 2),
                FontFlags.AntiAlias);
            var textPos = item.Position + new Vector2(item.Width - item.Height / 2 - textSize.X / 2, 7);
            Drawing.DrawText(
                s,
                textPos,
                new Vector2(item.Height / 2, item.Width / 2+20),
                Color.White.ToSharpDxColor(),
                FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom | FontFlags.StrikeOut);
        }

        internal static void DrawSlider(Vector2 position, MenuItem item, int width = -1, bool drawText = true)
        {
            var val = item.GetValue<Slider>();
            DrawSlider(position, item, val.MinValue, val.MaxValue, val.Value, width, drawText);
        }

        internal static void DrawSlider(
            Vector2 position,
            MenuItem item,
            int min,
            int max,
            int value,
            int width,
            bool drawText)
        {
            width = (width > 0 ? width : item.Width);
            var percentage = 100 * (value - min) / (max - min);
            var x = position.X + 3 + (percentage * (width - 3)) / 100;
            var x2D = 3 + (percentage * (width - 3)) / 100;

            MenuUtils.DrawLine(
                x,
                position.Y,
                x,
                position.Y + item.Height - 2,
                2,
                Color.FromArgb(200, 120, 60).ToSharpDxColor());
            MenuUtils.DrawBoxFilled(
                position.X,
                position.Y - 1,
                x2D - 1f,
                item.Height,
                Color.FromArgb(30, 150, 110, 0).ToSharpDxColor());

            if (drawText)
            {
                //Font.DrawText(
                //    null,
                //    value.ToString(),
                //    new Rectangle((int)position.X - 5, (int)position.Y, item.Width, item.Height),
                //    FontDrawFlags.VerticalCenter | FontDrawFlags.Right,
                //    new ColorBGRA(255, 255, 255, 255));
                var textSize = Drawing.MeasureText(
                    value.ToString(),
                    "Arial",
                    new Vector2(item.Height / 2, item.Width / 2),
                    FontFlags.AntiAlias);
                var textPos = position + new Vector2((float)(-5 + item.Width - item.Height + 15 - textSize.X / 3.5), 7);
                Drawing.DrawText(
                    value.ToString(),
                    textPos,
                    new Vector2(item.Height / 2, item.Width / 2),
                    Color.DarkOrange.ToSharpDxColor(),
                    FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom | FontFlags.StrikeOut);
            }
        }

        internal static void DrawToolTip_Button(Vector2 position, MenuItem item)
        {
            if (item.ValueType == MenuValueType.StringList || item.ValueType == MenuValueType.AbilityToggler)
            {
                return;
            }

            var s = "[?]";
            //var x = (int)item.Position.X + item.Width - item.Height - Font.MeasureText(s).Width - 7;

            //Font.DrawText(
            //    null,
            //    s,
            //    new Rectangle(x, (int)item.Position.Y, item.Width, item.Height),
            //    FontDrawFlags.VerticalCenter,
            //    new ColorBGRA(255, 255, 255, 255));
            var textSize = Drawing.MeasureText(
                s,
                "Arial",
                new Vector2(item.Height / 2, item.Width / 2),
                FontFlags.AntiAlias);
            var textPos = item.Position + new Vector2(item.Width - item.Height - textSize.X - 2, 6);
            Drawing.DrawText(
                s,
                textPos,
                new Vector2(item.Height / 2, item.Width / 2),
                Color.DarkGray.ToSharpDxColor(),
                FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom | FontFlags.StrikeOut);
        }

        internal static void DrawToolTip_Text(Vector2 position, MenuItem item, SharpDX.Color? TextColor = null)
        {
            if (item.ValueType == MenuValueType.StringList || item.ValueType == MenuValueType.AbilityToggler)
            {
                return;
            }

            MenuUtils.DrawBoxBordered(
                position.X + item.Height - 28,
                position.Y + 1,
                Font.MeasureText(item.Tooltip).Width + 8,
                item.Height,
                1,
                new SharpDX.Color(40,30,5,80), 
                SharpDX.Color.Black);

            var s = item.Tooltip;
            //Font.DrawText(
            //    null,
            //    s,
            //    new Rectangle(
            //        (int)(item.Position.X + item.Width - 33 + item.Height + 8),
            //        (int)item.Position.Y - 3,
            //        Font.MeasureText(item.Tooltip).Width + 8,
            //        item.Height + 8),
            //    FontDrawFlags.VerticalCenter,
            //    TextColor ?? SharpDX.Color.White);
            //var textSize = Drawing.MeasureText(s, "Arial", new Vector2(item.Height, item.Width), FontFlags.AntiAlias);
            var textPos = item.Position + new Vector2(item.Width - 33 + item.Height + 8, 9);
            Drawing.DrawText(
                s,
                textPos, new Vector2(15,14), 
                Color.White.ToSharpDxColor(),
                FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom | FontFlags.StrikeOut);
        }

        private static void CurrentDomainOnDomainUnload(object sender, EventArgs eventArgs)
        {
            if (Font != null)
            {
                Font.Dispose();
                Font = null;
            }
            if (FontBold != null)
            {
                FontBold.Dispose();
                FontBold = null;
            }
        }

        private static void Drawing_OnPreReset(EventArgs args)
        {
            Font.OnLostDevice();
            FontBold.OnLostDevice();
        }

        private static void DrawingOnOnPostReset(EventArgs args)
        {
            Font.OnResetDevice();
            FontBold.OnResetDevice();
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class Menu
    {
        #region Static Fields

        /// <summary>
        /// 
        /// </summary>
        public static readonly Menu Root = new Menu("Menu Settings", "Menu Settings");

        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<string, Menu> RootMenus = new Dictionary<string, Menu>();

        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<string, DotaTexture> TextureDictionary;

        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<string, float[]> PositionDictionary;

        #endregion

        #region Fields

        /// <summary>
        /// 
        /// </summary>
        public SharpDX.Color Color;

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName;

        /// <summary>
        /// 
        /// </summary>
        public List<Menu> Children = new List<Menu>();

        /// <summary>
        /// 
        /// </summary>
        public bool IsRootMenu;

        /// <summary>
        /// 
        /// </summary>
        public List<MenuItem> Items = new List<MenuItem>();

        /// <summary>
        /// 
        /// </summary>
        public string Name;

        /// <summary>
        /// 
        /// </summary>
        public Menu Parent;

        /// <summary>
        /// 
        /// </summary>
        public FontStyle Style;

        private int cachedMenuCount = 2;

        private int cachedMenuCountT;

        private bool visible;

        private string uniqueId;

        #endregion

        #region Constructors and Destructors

        static Menu()
        {
            TextureDictionary = new Dictionary<string, DotaTexture>();
            PositionDictionary = new Dictionary<string, float[]>();
            //Root.AddItem(new MenuItem("Dim", "Dim the screen").SetValue(true));
            Root.AddItem(
                new MenuItem("pressKey", "Menu hold key").SetValue(new KeyBind(16, KeyBindType.Press)));
            Root.AddItem(
                new MenuItem("toggleKey", "Menu toggle key").SetValue(new KeyBind(118, KeyBindType.Toggle)));
           //Root.AddItem(
           //     new MenuItem("FontSize", "Font Size:").SetValue(new Slider(14, 12, 20)).SetTooltip("TOOLTIP asd"));
           // var qualities = Enum.GetValues(typeof(FontQuality)).Cast<FontQuality>().Select(v => v.ToString()).ToArray();
           //Root.AddItem(
           //     new MenuItem("FontQuality", "Font Quality").SetTooltip("TOOLTIP asd")
           //         .SetValue(new StringList(qualities, 4)));
            Root.AddItem(
                new MenuItem("EnsageSharp.Common.TooltipDuration", "Tooltip Notification Duration").SetValue(
                    new Slider(1500, 0, 5000)));
            Root.AddItem(
                new MenuItem("FontInfo", "Press F5 after your change").SetFontStyle(
                    FontStyle.Bold,
                    SharpDX.Color.Yellow));
            CommonMenu.MenuConfig.AddSubMenu(Root);

        }

        public Menu(string displayName, string name, bool isRootMenu = false)
        {
            this.DisplayName = displayName;
            this.Name = name;
            this.IsRootMenu = isRootMenu;
            this.Style = FontStyle.Regular;
            this.Color = SharpDX.Color.White;

            if (isRootMenu)
            {
                AppDomain.CurrentDomain.DomainUnload += delegate { SaveAll(); };
                AppDomain.CurrentDomain.ProcessExit += delegate { SaveAll(); };

                var rootName = Assembly.GetCallingAssembly().GetName().Name + "." + name;

                if (RootMenus.ContainsKey(rootName))
                {
                    throw new ArgumentException("Root Menu [" + rootName + "] with the same name exists", "name");
                }

                RootMenus.Add(rootName, this);
            }
        }

        ~Menu()
        {
            if (RootMenus.ContainsKey(this.Name))
            {
                RootMenus.Remove(this.Name);
            }
        }

        #endregion

        #region Properties

        internal int Height
        {
            get
            {
                return MenuSettings.MenuItemHeight;
            }
        }

        internal int ChildrenMenuWidth
        {
            get
            {
                var result = this.Children.Select(item => item.NeededWidth).Concat(new[] { 0 }).Max();

                return this.Items.Select(item => item.NeededWidth).Concat(new[] { result }).Max();
            }
        }

        internal int MenuCount
        {
            get
            {
                if (Environment.TickCount - this.cachedMenuCountT < 500)
                {
                    return this.cachedMenuCount;
                }

                var globalMenuList = MenuGlobals.MenuState;
                var i = 0;
                var result = 0;

                foreach (var item in globalMenuList)
                {
                    if (item == this.uniqueId)
                    {
                        result = i;
                        break;
                    }
                    i++;
                }

                this.cachedMenuCount = result;
                this.cachedMenuCountT = Environment.TickCount;
                return result;
            }
        }

        internal Vector2 MyBasePosition
        {
            get
            {
                if (this.IsRootMenu || this.Parent == null)
                {
                    return MenuSettings.BasePosition + this.MenuCount * new Vector2(0, MenuSettings.MenuItemHeight) + new Vector2(5, 0);
                }

                return this.Parent.MyBasePosition + new Vector2(5, 0);
            }
        }

        internal int NeededWidth
        {
            get
            {
                return
                    MenuDrawHelper.Font.MeasureText(null, MultiLanguage._(this.DisplayName), FontDrawFlags.Center).Width
                    + 25;
            }
        }

        internal Vector2 Position
        {
            get
            {
                var xOffset = 0;

                if (this.Parent != null)
                {
                    xOffset = (int)(this.Parent.Position.X + this.Parent.Width);
                }
                else
                {
                    xOffset = (int)this.MyBasePosition.X;
                }

                return new Vector2(0, this.MyBasePosition.Y) + new Vector2(xOffset, 0)
                       + this.YLevel * new Vector2(0, MenuSettings.MenuItemHeight);
            }
        }

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
                //Hide all the children
                if (!this.visible)
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

        internal int Width
        {
            get
            {
                return this.Parent != null ? this.Parent.ChildrenMenuWidth : MenuSettings.MenuItemWidth;
            }
        }

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

        internal int YLevel
        {
            get
            {
                if (this.IsRootMenu || this.Parent == null)
                {
                    return 0;
                }

                return this.Parent.YLevel + this.Parent.Children.TakeWhile(test => test.Name != this.Name).Count();
            }
        }

        #endregion

        #region Public Methods and Operators

        public static Menu GetMenu(string Assemblyname, string menuname)
        {
            var menu = RootMenus.FirstOrDefault(x => x.Key == Assemblyname + "." + menuname).Value;
            return menu;
        }

        public static MenuItem GetValueGlobally(
            string Assemblyname,
            string menuname,
            string itemname,
            string submenu = null)
        {
            var menu = RootMenus.FirstOrDefault(x => x.Key == Assemblyname + "." + menuname).Value;

            if (submenu != null)
            {
                menu = menu.SubMenu(submenu);
            }

            var menuitem = menu.Item(itemname);

            return menuitem;
        }

        public static void SendMessage(uint key, Utils.WindowsMessages message)
        {
            foreach (var menu in RootMenus)
            {
                menu.Value.OnReceiveMessage(message, Game.MouseScreenPosition, key);
            }
        }

        public MenuItem AddItem(MenuItem item)
        {
            item.Parent = this;
            this.Items.Add(item);
            return item;
        }

        public Menu AddSubMenu(Menu subMenu)
        {
            subMenu.Parent = this;
            this.Children.Add(subMenu);

            return subMenu;
        }

        public void AddToMainMenu()
        {
            //Console.WriteLine("AAAA");
            this.InitMenuState(Assembly.GetCallingAssembly().GetName().Name);
            AppDomain.CurrentDomain.DomainUnload += (sender, args) => this.UnloadMenuState();
            //Drawing.OnEndScene += this.Drawing_OnEndScene;
            Drawing.OnDraw += this.Drawing_OnDraw;
            Game.OnWndProc += this.Game_OnWndProc;
        }

        public MenuItem Item(string name, bool makeChampionUniq = false)
        {
            if (makeChampionUniq)
            {
                name = ObjectMgr.LocalHero.Name + name;
            }

            //Search in our own items
            foreach (var item in this.Items.Where(item => item.Name == name))
            {
                return item;
            }

            //Search in submenus
            return
                (from subMenu in this.Children where subMenu.Item(name) != null select subMenu.Item(name))
                    .FirstOrDefault();
        }

        public Menu SetFontStyle(FontStyle fontStyle = FontStyle.Regular, SharpDX.Color? fontColor = null)
        {
            this.Style = fontStyle;
            this.Color = fontColor ?? SharpDX.Color.White;

            return this;
        }

        public Menu SubMenu(string name)
        {
            //Search in submenus and if it doesn't exist add it.
            var subMenu = this.Children.FirstOrDefault(sm => sm.Name == name);
            return subMenu ?? this.AddSubMenu(new Menu(name, name));
        }

        #endregion

        #region Methods

        internal void Drawing_OnDraw(EventArgs args)
        {
            if (!Game.IsInGame)
            {
                return;
            }
            if (!this.Visible)
            {
                return;
            }

            //if (CommonMenu.MenuConfig.Item("Dim").GetValue<bool>())
            //{
            //    //Drawing.Direct3DDevice9.SetRenderState(RenderState.AlphaBlendEnable, true);
            //    Drawing.Direct3DDevice9.SetRenderState(RenderState.AlphaTestEnable, true);
            //}
            MenuUtils.DrawBoxBordered(
                this.Position.X,
                this.Position.Y,
                this.Width,
                this.Height,
                1,
                (this.Children.Count > 0 && this.Children[0].Visible || this.Items.Count > 0 && this.Items[0].Visible)
                    ? MenuSettings.ActiveBackgroundColor.ToSharpDxColor()
                    : MenuSettings.BackgroundColor.ToSharpDxColor(), SharpDX.Color.Black);

            //MenuDrawHelper.Font.DrawText(
            //    null,
            //    MultiLanguage._(this.DisplayName),
            //    new Rectangle((int)this.Position.X + 5, (int)this.Position.Y, this.Width, this.Height),
            //    FontDrawFlags.VerticalCenter,
            //    this.Color);
            //var textSize = Drawing.MeasureText(MultiLanguage._(this.DisplayName), "Arial", new Vector2(this.Width, this.Height), FontFlags.AntiAlias);
            var textPos = this.Position + new Vector2(5, this.Height / 3 - 2);
            Drawing.DrawText(
                MultiLanguage._(this.DisplayName),
                textPos,
                new Vector2(15, 100),
                this.Color,
                FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom | FontFlags.StrikeOut);

            //MenuDrawHelper.Font.DrawText(
            //    null,
            //    ">",
            //    new Rectangle((int)this.Position.X - 5, (int)this.Position.Y, this.Width, this.Height),
            //    FontDrawFlags.Right | FontDrawFlags.VerticalCenter,
            //    this.Color);
            var textSize = Drawing.MeasureText(">", "Arial", new Vector2(18,12), FontFlags.AntiAlias);
            textPos = textPos + new Vector2(this.Width - 15- textSize.X/2, - textSize.Y/6);
            Drawing.DrawText(
                "»",
                textPos,
                new Vector2(18, 12),
                System.Drawing.Color.FromArgb(225, System.Drawing.Color.Orange).ToSharpDxColor(),
                FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom | FontFlags.StrikeOut
                | FontFlags.Outline);

            //Draw the menu submenus
            foreach (var child in this.Children.Where(child => child.Visible))  
            {
                child.Drawing_OnDraw(args);
            }

            //Draw the items
            for (var i = this.Items.Count - 1; i >= 0; i--)
            {
                var item = this.Items[i];
                if (item.Visible)
                {
                    item.Drawing_OnDraw();
                }
            }
        }

        internal void Drawing_OnEndScene(EventArgs args)
        {
            if (!this.Visible)
            {
                return;
            }

            Drawing.Direct3DDevice9.SetRenderState(RenderState.AlphaBlendEnable, true);
            MenuUtils.DrawBoxBordered(
                this.Position.X,
                this.Position.Y,
                this.Width,
                this.Height,
                1,
                (this.Children.Count > 0 && this.Children[0].Visible || this.Items.Count > 0 && this.Items[0].Visible)
                    ? MenuSettings.ActiveBackgroundColor.ToSharpDxColor()
                    : MenuSettings.BackgroundColor.ToSharpDxColor(),
                SharpDX.Color.Black);

            //MenuDrawHelper.Font.DrawText(
            //    null,
            //    MultiLanguage._(this.DisplayName),
            //    new Rectangle((int)this.Position.X + 5, (int)this.Position.Y, this.Width, this.Height),
            //    FontDrawFlags.VerticalCenter,
            //    this.Color);

            //MenuDrawHelper.Font.DrawText(
            //    null,
            //    ">",
            //    new Rectangle((int)this.Position.X - 5, (int)this.Position.Y, this.Width, this.Height),
            //    FontDrawFlags.Right | FontDrawFlags.VerticalCenter,
            //    this.Color);

            //Draw the menu submenus
            foreach (var child in this.Children.Where(child => child.Visible))
            {
                child.Drawing_OnEndScene(args);
            }

            //Draw the items
            for (var i = this.Items.Count - 1; i >= 0; i--)
            {
                var item = this.Items[i];
                if (item.Visible)
                {
                    item.Drawing_OnEndScene();
                }
            }
        }

        internal void Game_OnWndProc(WndEventArgs args)
        {
            if (!Game.IsInGame)
            {
                return;
            }
            this.OnReceiveMessage((Utils.WindowsMessages)args.Msg, Game.MouseScreenPosition, (uint)args.WParam, args);
        }

        internal bool IsInside(Vector2 position)
        {
            return Utils.IsUnderRectangle(position, this.Position.X, this.Position.Y, this.Width, this.Height);
        }

        internal void OnReceiveMessage(Utils.WindowsMessages message, Vector2 cursorPos, uint key, WndEventArgs args = null)
        {
            //Spread the message to the menu's children recursively
            
            foreach (var item in this.Items)
            {
                item.OnReceiveMessage(message, cursorPos, key, args);
                //Console.WriteLine(args != null && item.IsInside(cursorPos));
            }
            if (!this.Visible)
            {
                return;
            }
            foreach (var child in this.Children)
            {
                child.OnReceiveMessage(message, cursorPos, key);
            }
            //Handle the left clicks on the menus to hide or show the submenus.
            if (message != Utils.WindowsMessages.WM_LBUTTONDOWN)
            {
                return;
            }

            if (this.IsRootMenu && this.Visible)
            {
                if (cursorPos.X - MenuSettings.BasePosition.X < MenuSettings.MenuItemWidth)
                {
                    var n = (int)(cursorPos.Y - MenuSettings.BasePosition.Y) / MenuSettings.MenuItemHeight;
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

            if (!this.Visible)
            {
                return;
            }

            if (!this.IsInside(cursorPos))
            {
                return;
            }

            if (!this.IsRootMenu && this.Parent != null)
            {
                //Close all the submenus in the level 
                foreach (var child in this.Parent.Children.Where(child => child.Name != this.Name))
                {
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

            //Hide or Show the submenus.
            foreach (var child in this.Children)
            {
                child.Visible = !child.Visible;
            }

            //Hide or Show the items.
            foreach (var item in this.Items)
            {
                item.Visible = !item.Visible;
            }
        }

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

        private void InitMenuState(string assemblyName)
        {
            List<string> globalMenuList;
            this.uniqueId = assemblyName + "." + this.Name;

            globalMenuList = MenuGlobals.MenuState;

            if (globalMenuList == null)
            {
                globalMenuList = new List<string>();
            }
            while (globalMenuList.Contains(this.uniqueId))
            {
                this.uniqueId += ".";
            }

            globalMenuList.Add(this.uniqueId);

            MenuGlobals.MenuState = globalMenuList;
        }

        private void UnloadMenuState()
        {
            var globalMenuList = MenuGlobals.MenuState;
            globalMenuList.Remove(this.uniqueId);
            MenuGlobals.MenuState = globalMenuList;
        }

        #endregion
    }

    internal enum MenuValueType
    {
        None,

        Boolean,

        Slider,

        KeyBind,

        Integer,

        Color,

        Circle,

        StringList,

        AbilityToggler
    }

    public class OnValueChangeEventArgs
    {
        #region Fields

        private readonly object _newValue;

        private readonly object _oldValue;

        #endregion

        #region Constructors and Destructors

        public OnValueChangeEventArgs(object oldValue, object newValue)
        {
            this._oldValue = oldValue;
            this._newValue = newValue;
            this.Process = true;
        }

        #endregion

        #region Public Properties

        public bool Process { get; set; }

        #endregion

        #region Public Methods and Operators

        public T GetNewValue<T>()
        {
            return (T)this._newValue;
        }

        public T GetOldValue<T>()
        {
            return (T)this._oldValue;
        }

        #endregion
    }

    public class MenuItem
    {
        #region Fields

        public string DisplayName;

        public ColorBGRA FontColor;

        public FontStyle FontStyle;

        public int MenuFontSize;

        public string Name;

        public Menu Parent;

        public bool ShowItem;

        public int Tag;

        public string Tooltip;

        public SharpDX.Color TooltipColor;

        internal bool _valueSet;

        internal bool DrawingTooltip;

        internal bool Interacting;

        internal MenuValueType ValueType;

        private readonly string _MenuConfigName;

        private bool _dontSave;

        private bool _isShared;

        private byte[] _serialized;

        private object _value;

        private bool _visible;

        #endregion

        #region Constructors and Destructors

        public MenuItem(string name, string displayName, bool makeChampionUniq = false)
        {
            if (makeChampionUniq)
            {
                name = ObjectMgr.LocalHero.Name + name;
            }

            this.Name = name;
            this.DisplayName = displayName;
            this.FontStyle = FontStyle.Regular;
            this.FontColor = SharpDX.Color.White;
            this.ShowItem = true;
            this.Tag = 0;
            this._MenuConfigName = Assembly.GetCallingAssembly().GetName().Name
                                   + Assembly.GetCallingAssembly().GetType().GUID;
        }

        #endregion

        #region Public Events

        public event EventHandler<OnValueChangeEventArgs> ValueChanged;

        #endregion

        #region Public Properties

        public int TooltipDuration
        {
            get
            {
                return CommonMenu.MenuConfig.Item("EnsageSharp.Common.TooltipDuration").GetValue<Slider>().Value;
            }
        }

        #endregion

        #region Properties

        internal int Height
        {
            get
            {
                return MenuSettings.MenuItemHeight;
            }
        }

        internal Vector2 MyBasePosition
        {
            get
            {
                if (this.Parent == null)
                {
                    return MenuSettings.BasePosition;
                }

                return this.Parent.MyBasePosition;
            }
        }

        internal int NeededWidth
        {
            get
            {
                var extra = 0;

                if (this.ValueType == MenuValueType.StringList)
                {
                    var slVal = this.GetValue<StringList>();
                    var max =
                        slVal.SList.Select(
                            v => MenuDrawHelper.Font.MeasureText(null, v, FontDrawFlags.Center).Width + 25)
                            .Concat(new[] { 0 })
                            .Max();

                    extra += max;
                }

                if (this.ValueType == MenuValueType.AbilityToggler)
                {
                    extra += this.GetValue<AbilityToggler>().Dictionary.Count * (this.Height-10);
                }

                if (this.ValueType == MenuValueType.KeyBind)
                {
                    var val = this.GetValue<KeyBind>();
                    extra +=
                        MenuDrawHelper.Font.MeasureText(
                            null,
                            " [" + Utils.KeyToText(val.Key) + "]",
                            FontDrawFlags.Center).Width;
                }

                return
                    MenuDrawHelper.Font.MeasureText(null, MultiLanguage._(this.DisplayName), FontDrawFlags.Center).Width
                    + this.Height * 2 + 10 + extra;
            }
        }

        internal Vector2 Position
        {
            get
            {
                var xOffset = 0;

                if (this.Parent != null)
                {
                    xOffset = (int)(this.Parent.Position.X + this.Parent.Width);
                }

                return new Vector2(0, this.MyBasePosition.Y) + new Vector2(xOffset + 1, 0)
                       + this.YLevel * new Vector2(0, MenuSettings.MenuItemHeight);
            }
        }

        internal string SaveFileName
        {
            get
            {
                return (this._isShared ? "SharedMenuConfig" : this._MenuConfigName);
            }
        }

        internal string SaveKey
        {
            get
            {
                return Utils.Md5Hash("v3" + this.DisplayName + this.Name);
            }
        }

        internal bool Visible
        {
            get
            {
                return MenuSettings.DrawMenu && this._visible && this.ShowItem;
            }
            set
            {
                this._visible = value;
            }
        }

        internal int Width
        {
            get
            {
                return this.Parent != null ? this.Parent.ChildrenMenuWidth : MenuSettings.MenuItemWidth;
            }
        }

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

        public MenuItem DontSave()
        {
            this._dontSave = true;
            return this;
        }

        public T GetValue<T>()
        {
            return (T)this._value;
        }

        public bool IsActive()
        {
            switch (this.ValueType)
            {
                case MenuValueType.Boolean:
                    return this.GetValue<bool>();
                case MenuValueType.Circle:
                    return this.GetValue<Circle>().Active;
                case MenuValueType.KeyBind:
                    return this.GetValue<KeyBind>().Active;
            }
            return false;
        }

        public MenuItem SetFontStyle(FontStyle fontStyle = FontStyle.Regular, SharpDX.Color? fontColor = null)
        {
            this.FontStyle = fontStyle;
            this.FontColor = fontColor ?? SharpDX.Color.White;

            return this;
        }

        public MenuItem SetShared()
        {
            this._isShared = true;
            return this;
        }

        public MenuItem SetTag(int tag = 0)
        {
            this.Tag = tag;

            return this;
        }

        public MenuItem SetTooltip(string tooltip, SharpDX.Color? tooltipColor = null)
        {
            this.Tooltip = tooltip;
            this.TooltipColor = tooltipColor ?? SharpDX.Color.White;
            return this;
        }

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
                if (!this._valueSet && readBytes != null)
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

                        case MenuValueType.Circle:
                            var savedCircleValue = (Circle)(object)Utils.Deserialize<T>(readBytes);
                            var newCircleValue = (Circle)(object)newValue;
                            savedCircleValue.Radius = newCircleValue.Radius;
                            newValue = (T)(object)savedCircleValue;
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
                            var newDictionaryValue = ((AbilityToggler)(object)newValue);
                            var saveddict = savedDictionaryValue.Dictionary;
                            if (newDictionaryValue.Dictionary.All(b => saveddict.ContainsKey(b.Key)))
                            {
                                newValue = (T)(object)savedDictionaryValue;
                            }
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

            if (this._valueSet)
            {
                var handler = this.ValueChanged;
                if (handler != null)
                {
                    valueChangedEvent = new OnValueChangeEventArgs(this._value, newValue);
                    handler(this, valueChangedEvent);
                }
            }

            if (valueChangedEvent != null)
            {
                if (valueChangedEvent.Process)
                {
                    this._value = newValue;
                }
            }
            else
            {
                this._value = newValue;
            }
            this._valueSet = true;
            //try
            //{
                this._serialized = Utils.Serialize(this._value);
           // }
            //catch (Exception)
            //{
                //
            //}           
            return this;
        }

        public MenuItem Show(bool showItem = true)
        {
            this.ShowItem = showItem;

            return this;
        }

        public void ShowTooltip(bool hide = false)
        {
            if (!string.IsNullOrEmpty(this.Tooltip))
            {
                this.DrawingTooltip = !hide;
            }
        }

        public void ShowTooltip_Notification()
        {
            if (!string.IsNullOrEmpty(this.Tooltip))
            {
                var notif = new Notification(this.Tooltip).SetTextColor(Color.White);
                Notifications.AddNotification(notif);
                DelayAction.Add(this.TooltipDuration, () => notif.Dispose());
            }
        }

        #endregion

        #region Methods

        internal void Drawing_OnDraw()
        {
            MenuUtils.DrawBoxBordered(
                this.Position.X,
                this.Position.Y,
                this.Width,
                this.Height,
                1,
                MenuSettings.BackgroundColor.ToSharpDxColor(),
                SharpDX.Color.Black);
            var s = MultiLanguage._(this.DisplayName);
            if (this.DrawingTooltip)
            {
                MenuDrawHelper.DrawToolTip_Text(
                    new Vector2(this.Position.X + this.Width, this.Position.Y),
                    this,
                    this.TooltipColor);
            }

            //Font font;
            //switch (this.FontStyle)
            //{
            //    case FontStyle.Bold:
            //        font = MenuDrawHelper.FontBold;
            //        break;
            //    default:
            //        font = MenuDrawHelper.Font;
            //        break;
            //}

            switch (this.ValueType)
            {
                case MenuValueType.Slider:
                    MenuDrawHelper.DrawSlider(this.Position, this);
                    break;
                case MenuValueType.Boolean:
                    MenuDrawHelper.DrawOnOff(
                        this.GetValue<bool>(),
                        new Vector2(this.Position.X + this.Width - this.Height, this.Position.Y),
                        this);
                    break;

                case MenuValueType.KeyBind:
                    var val = this.GetValue<KeyBind>();
                    if (this.Interacting)
                    {
                        s = MultiLanguage._("Press new key");
                    }

                    //var x = (int)this.Position.X + this.Width - this.Height
                    //        - font.MeasureText(null, "[" + Utils.KeyToText(val.Key) + "]", FontDrawFlags.Center).Width
                    //        - 10;

                    MenuDrawHelper.DrawOnOff(
                        val.Active,
                        new Vector2(this.Position.X + this.Width - this.Height, this.Position.Y),
                        this);
                    //
                    var textSize = Drawing.MeasureText(
                        "[" + Utils.KeyToText(val.Key) + "]",
                        "Arial",
                        new Vector2(this.Height / 2 + 1, this.Width / 2 + 10),
                        FontFlags.AntiAlias);
                    var textPos = this.Position + new Vector2(this.Width - this.Height - textSize.X - 22, 6);
                    var alpha = Utils.IsUnderRectangle(
                        Game.MouseScreenPosition,
                        textPos.X,
                        textPos.Y,
                        textSize.X,
                        textSize.Y)
                                    ? 60
                                    : 0;
                    Drawing.DrawText(
                        "[" + Utils.KeyToText(val.Key) + "]",
                        textPos,
                        new Vector2(this.Height / 2+1, this.Width / 2+10),
                        new SharpDX.Color(195 + alpha, 139 + alpha, 12 + alpha, 225),
                        FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom | FontFlags.StrikeOut);
                    //font.DrawText(
                    //    null,
                    //    "[" + Utils.KeyToText(val.Key) + "]",
                    //    new Rectangle(x, (int)this.Position.Y, this.Width, this.Height),
                    //    FontDrawFlags.VerticalCenter,
                    //    new ColorBGRA(1, 169, 234, 255));
                    break;

                case MenuValueType.Integer:
                    var intVal = this.GetValue<int>();
                    //MenuDrawHelper.Font.DrawText(
                    //    null,
                    //    intVal.ToString(),
                    //    new Rectangle((int)this.Position.X + 5, (int)this.Position.Y, this.Width, this.Height),
                    //    FontDrawFlags.VerticalCenter | FontDrawFlags.Right,
                    //    new ColorBGRA(255, 255, 255, 255));
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
                        new SharpDX.Color(255, 255, 255, 225),
                        FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom | FontFlags.StrikeOut);
                    break;

                case MenuValueType.Circle:
                    var circleVal = this.GetValue<Circle>();
                    //var circlePos = Position + new Vector2(Width - Height * 2, 0);

                    //MenuUtils.DrawBoxBordered(
                    //    circlePos.X,
                    //    circlePos.Y,
                    //    this.Height,
                    //    this.Height, 1f,
                    //    circleVal.Color.ToSharpDxColor(), SharpDX.Color.Black);

                    MenuDrawHelper.DrawOnOff(
                        circleVal.Active,
                        new Vector2(this.Position.X + this.Width - this.Height, this.Position.Y),
                        this);
                    break;

                case MenuValueType.StringList:
                    var slVal = this.GetValue<StringList>();
                    var t = slVal.SList[slVal.SelectedIndex];

                    MenuDrawHelper.DrawArrow(
                        "<<",
                        this.Position + new Vector2(this.Width - this.Height * 2, 0),
                        this,
                        Color.Black);
                    MenuDrawHelper.DrawArrow(
                        ">>",
                        this.Position + new Vector2(this.Width - this.Height, 0),
                        this,
                        Color.Black);

                    //MenuDrawHelper.Font.DrawText(
                    //    null,
                    //    MultiLanguage._(t),
                    //    new Rectangle(
                    //        (int)this.Position.X - 5 - 2 * this.Height,
                    //        (int)this.Position.Y,
                    //        this.Width,
                    //        this.Height),
                    //    FontDrawFlags.VerticalCenter | FontDrawFlags.Right,
                    //    new ColorBGRA(255, 255, 255, 255));
                    //Console.WriteLine(this.Height + " " + this.Width);
                    textSize = Drawing.MeasureText(
                        MultiLanguage._(t),
                        "Arial",
                        new Vector2(14, 80),
                        FontFlags.AntiAlias);
                    textPos = this.Position + new Vector2((float)(-this.Height * 2 + this.Width - textSize.X - 3), 8);
                    Drawing.DrawText(
                        MultiLanguage._(t),
                        textPos,
                        new Vector2(14, 80),
                        new SharpDX.Color(255, 255, 255, 225),
                        FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom | FontFlags.StrikeOut);
                    break;

                case MenuValueType.AbilityToggler:

                    var width = 0f;
                    var basePosition = this.Position + new Vector2(this.Width-this.Height, 0);
                    var size = new Vector2(this.Height-6,this.Height-6);
                    var dictionary = this.GetValue<AbilityToggler>().Dictionary;
                    var positionDictionary = Menu.PositionDictionary;
                    //textSize = Drawing.MeasureText("x", "Arial", size + new Vector2(30, 30), FontFlags.AntiAlias);
                    foreach (var dotaTexture in Menu.TextureDictionary)
                    {                      
                        positionDictionary[dotaTexture.Key][0] = basePosition.X - width;
                        positionDictionary[dotaTexture.Key][1] = basePosition.Y;
                        var pos = basePosition - new Vector2(width, 0);
                        alpha = Utils.IsUnderRectangle(
                            Game.MouseScreenPosition,
                            pos.X,
                            pos.Y,
                            size.X + 6,
                            size.Y + 6)
                                        ? 35
                                        : 0;
                        Drawing.DrawRect(pos
                            ,
                            size + new Vector2(6, 6),
                            dictionary[dotaTexture.Key]
                                ? Color.FromArgb(180 + alpha, 120 + alpha, 1 + alpha).ToSharpDxColor()
                                : Color.FromArgb(37 + alpha, 37 + alpha, 37 + alpha).ToSharpDxColor());                 
                        if (dotaTexture.Key.Contains("item"))
                        {
                            Drawing.DrawRect(pos - new Vector2(- 3, -3), size + new Vector2(11, 0), dotaTexture.Value);
                        }
                        else
                        {
                            Drawing.DrawRect(pos - new Vector2(- 3, -3), size, dotaTexture.Value);
                        }
                        Drawing.DrawRect(pos - new Vector2(-3, -3), size, SharpDX.Color.Black, true);
                        Drawing.DrawRect(pos, size + new Vector2(6, 6), SharpDX.Color.Black, true);
                        //textPos = basePosition - new Vector2(width, 5) + new Vector2(this.Height / 2 - textSize.X / 2, this.Height / 2 - textSize.Y / 2);
                        //Drawing.DrawText(
                        //    "x",
                        //    textPos,
                        //    size + new Vector2(30, 30),
                        //    new SharpDX.Color(225, 150, 0),
                        //    FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom
                        //    | FontFlags.StrikeOut);
                        //
                        width += size.X + 6;
                        width += -1;
                    }
                    
                    break;
            }

            //font.DrawText(
            //    null,
            //    s,
            //    new Rectangle((int)this.Position.X + 5, (int)this.Position.Y, this.Width, this.Height),
            //    FontDrawFlags.VerticalCenter,
            //    this.FontColor);
            //var textSize1 = Drawing.MeasureText(s, "Arial", new Vector2(this.Height, this.Width), FontFlags.AntiAlias);
            var textPos1 = this.Position + new Vector2(5, 8);
            Drawing.DrawText(
                s,
                textPos1,new Vector2(15,20), 
                (s == MultiLanguage._("Press new key")) ? new SharpDX.Color(150,100,0) : (SharpDX.Color)this.FontColor,
                FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom | FontFlags.StrikeOut);

            if (!string.IsNullOrEmpty(this.Tooltip))
            {
                MenuDrawHelper.DrawToolTip_Button(new Vector2(this.Position.X + this.Width, this.Position.Y), this);
            }
        }

        internal void Drawing_OnEndScene()
        {
            var s = MultiLanguage._(this.DisplayName);
            MenuUtils.DrawBoxBordered(
                this.Position.X,
                this.Position.Y,
                this.Width,
                this.Height,
                1,
                MenuSettings.BackgroundColor.ToSharpDxColor(),
                SharpDX.Color.Black);

            if (this.DrawingTooltip)
            {
                MenuDrawHelper.DrawToolTip_Text(
                    new Vector2(this.Position.X + this.Width, this.Position.Y),
                    this,
                    this.TooltipColor);
            }

            Font font;
            switch (this.FontStyle)
            {
                case FontStyle.Bold:
                    font = MenuDrawHelper.FontBold;
                    break;
                default:
                    font = MenuDrawHelper.Font;
                    break;
            }

            switch (this.ValueType)
            {
                case MenuValueType.Slider:
                    MenuDrawHelper.DrawSlider(this.Position, this);
                    break;
                case MenuValueType.Boolean:
                    MenuDrawHelper.DrawOnOff(
                        this.GetValue<bool>(),
                        new Vector2(this.Position.X + this.Width - this.Height, this.Position.Y),
                        this);
                    break;

                case MenuValueType.KeyBind:
                    var val = this.GetValue<KeyBind>();
                    if (this.Interacting)
                    {
                        s = MultiLanguage._("Press new key");
                    }

                    var x = (int)this.Position.X + this.Width - this.Height
                            - font.MeasureText(null, "[" + Utils.KeyToText(val.Key) + "]", FontDrawFlags.Center).Width
                            - 10;

                    MenuDrawHelper.DrawOnOff(
                        val.Active,
                        new Vector2(this.Position.X + this.Width - this.Height, this.Position.Y),
                        this);

                    //font.DrawText(
                    //    null,
                    //    "[" + Utils.KeyToText(val.Key) + "]",
                    //    new Rectangle(x, (int)this.Position.Y, this.Width, this.Height),
                    //    FontDrawFlags.VerticalCenter,
                    //    new ColorBGRA(1, 169, 234, 255));
                    break;

                case MenuValueType.Integer:
                    var intVal = this.GetValue<int>();
                    //MenuDrawHelper.Font.DrawText(
                    //    null,
                    //    intVal.ToString(),
                    //    new Rectangle((int)this.Position.X + 5, (int)this.Position.Y, this.Width, this.Height),
                    //    FontDrawFlags.VerticalCenter | FontDrawFlags.Right,
                    //    new ColorBGRA(255, 255, 255, 255));
                    break;

                case MenuValueType.Circle:
                    var circleVal = this.GetValue<Circle>();
                    var circlePos = this.Position + new Vector2(this.Width - this.Height * 2, 0);

                    MenuUtils.DrawBoxBordered(
                        circlePos.X,
                        circlePos.Y,
                        this.Height,
                        this.Height,
                        1f,
                        circleVal.Color.ToSharpDxColor(),
                        SharpDX.Color.Black);

                    MenuDrawHelper.DrawOnOff(
                        circleVal.Active,
                        new Vector2(this.Position.X + this.Width - this.Height, this.Position.Y),
                        this);
                    break;

                case MenuValueType.StringList:
                    var slVal = this.GetValue<StringList>();
                    var t = slVal.SList[slVal.SelectedIndex];

                    MenuDrawHelper.DrawArrow(
                        "<<",
                        this.Position + new Vector2(this.Width - this.Height * 2, 0),
                        this,
                        Color.Black);
                    MenuDrawHelper.DrawArrow(
                        ">>",
                        this.Position + new Vector2(this.Width - this.Height, 0),
                        this,
                        Color.Black);

                    //MenuDrawHelper.Font.DrawText(
                    //    null,
                    //    MultiLanguage._(t),
                    //    new Rectangle(
                    //        (int)this.Position.X - 5 - 2 * this.Height,
                    //        (int)this.Position.Y,
                    //        this.Width,
                    //        this.Height),
                    //    FontDrawFlags.VerticalCenter | FontDrawFlags.Right,
                    //    new ColorBGRA(255, 255, 255, 255));
                    break;
            }

            //font.DrawText(
            //    null,
            //    s,
            //    new Rectangle((int)this.Position.X + 5, (int)this.Position.Y, this.Width, this.Height),
            //    FontDrawFlags.VerticalCenter,
            //    this.FontColor);

            if (!string.IsNullOrEmpty(this.Tooltip))
            {
                MenuDrawHelper.DrawToolTip_Button(new Vector2(this.Position.X + this.Width, this.Position.Y), this);
            }
        }

        internal bool IsInside(Vector2 position)
        {
            return Utils.IsUnderRectangle(
                position,
                this.Position.X,
                this.Position.Y,
                !string.IsNullOrEmpty(this.Tooltip) ? this.Width + this.Height : this.Width,
                this.Height);
        }

        internal void OnReceiveMessage(Utils.WindowsMessages message, Vector2 cursorPos, uint key, WndEventArgs wargs)
        {
            if (message == Utils.WindowsMessages.WM_MOUSEMOVE)
            {
                if (this.Visible && this.IsInside(cursorPos))
                {
                    if (cursorPos.X > this.Position.X + this.Width - 67
                        && cursorPos.X < this.Position.X + this.Width - 67 + this.Height + 8)
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

                    if (cursorPos.X > this.Position.X + this.Width - this.Height)
                    {
                        this.SetValue(!this.GetValue<bool>());
                    }

                    break;

                case MenuValueType.Slider:

                    if (!this.Visible)
                    {
                        this.Interacting = false;
                        return;
                    }

                    if (message == Utils.WindowsMessages.WM_MOUSEMOVE && this.Interacting
                        || message == Utils.WindowsMessages.WM_LBUTTONDOWN && !this.Interacting
                        && this.IsInside(cursorPos))
                    {
                        var val = this.GetValue<Slider>();
                        var t = val.MinValue
                                + ((cursorPos.X - this.Position.X) * (val.MaxValue - val.MinValue)) / this.Width;
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

                case MenuValueType.Color:

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

                    if (cursorPos.X > this.Position.X + this.Width - this.Height)
                    {
                        var c = this.GetValue<Color>();
                        ColorPicker.Load(delegate(Color args) { SetValue(args); }, c);
                    }

                    break;
                case MenuValueType.Circle:

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

                    if (cursorPos.X - this.Position.X > this.Width - this.Height)
                    {
                        var val = this.GetValue<Circle>();
                        val.Active = !val.Active;
                        this.SetValue(val);
                    }
                    else if (cursorPos.X - this.Position.X > this.Width - 2 * this.Height)
                    {
                        var c = this.GetValue<Circle>();
                        ColorPicker.Load(
                            delegate(Color args)
                                {
                                    var val = GetValue<Circle>();
                                    val.Color = args;
                                    SetValue(val);
                                },
                            c.Color);
                    }

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
                                    if (wargs != null)
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
                                    if (wargs != null)
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
                        if (wargs.Process != null)
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

                    if (cursorPos.X > this.Position.X + this.Width - this.Height)
                    {
                        var val = this.GetValue<KeyBind>();
                        val.Active = !val.Active;
                        this.SetValue(val);
                    }
                    else
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
                                                  : (slVal.SelectedIndex + 1);
                        this.SetValue(slVal);
                    }
                    else if (cursorPos.X > this.Position.X + this.Width - 2 * this.Height)
                    {
                        slVal.SelectedIndex = slVal.SelectedIndex == 0
                                                  ? slVal.SList.Length - 1
                                                  : (slVal.SelectedIndex - 1);
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
                    var positionDictionary = Menu.PositionDictionary;
                    var dictionary = this.GetValue<AbilityToggler>().Dictionary;
                    foreach (var v in from v in positionDictionary
                                      let pos = new Vector2(v.Value[0],v.Value[1])
                                      where
                                          Utils.IsUnderRectangle(
                                              cursorPos,
                                              pos.X,
                                              pos.Y,
                                              this.Height - 2,
                                              this.Height - 2)
                                      select v)
                    {
                        dictionary[v.Key] = !dictionary[v.Key];
                    }
                    this.SetValue(new AbilityToggler(dictionary));
                    break;
            }
        }

        internal void SaveToFile(ref Dictionary<string, Dictionary<string, byte[]>> dics)
        {
            if (!this._dontSave)
            {
                if (!dics.ContainsKey(this.SaveFileName))
                {
                    dics[this.SaveFileName] = new Dictionary<string, byte[]>();
                }

                dics[this.SaveFileName][this.SaveKey] = this._serialized;
            }
        }

        #endregion
    }

    public static class ColorPicker
    {
        #region Static Fields

        public static CPSlider AlphaSlider;

        public static Render.Sprite BackgroundSprite;

        public static Bitmap LuminityBitmap;

        public static Render.Sprite LuminitySprite;

        public static CPSlider LuminositySlider;

        public static OnSelectColor OnChangeColor;

        public static Bitmap OpacityBitmap;

        public static Render.Sprite OpacitySprite;

        public static Render.Rectangle PreviewRectangle;

        private static bool _moving;

        private static Vector2 _prevPos;

        private static bool _selecting;

        private static bool _visible;

        private static int _x = 100;

        private static int _y = 100;

        private static Color InitialColor;

        private static int LastBitmapUpdate;

        private static int LastBitmapUpdate2;

        private static HSLColor SColor = new HSLColor(255, 255, 255);

        private static double SHue;

        private static double SSaturation;

        #endregion

        #region Constructors and Destructors

        static ColorPicker()
        {
            LuminityBitmap = new Bitmap(9, 238);
            OpacityBitmap = new Bitmap(9, 238);

            UpdateLuminosityBitmap(Color.White, true);
            UpdateOpacityBitmap(Color.White, true);

            BackgroundSprite = (Render.Sprite)new Render.Sprite(Resources.CPForm, new Vector2(X, Y)).Add(1);

            LuminitySprite = (Render.Sprite)new Render.Sprite(LuminityBitmap, new Vector2(X + 285, Y + 40)).Add(0);
            OpacitySprite = (Render.Sprite)new Render.Sprite(OpacityBitmap, new Vector2(X + 349, Y + 40)).Add(0);

            PreviewRectangle =
                (Render.Rectangle)
                new Render.Rectangle(X + 375, Y + 44, 54, 80, new ColorBGRA(255, 255, 255, 255)).Add(0);

            LuminositySlider = new CPSlider(285 - Resources.CPActiveSlider.Width / 3, 35, 248);
            AlphaSlider = new CPSlider(350 - Resources.CPActiveSlider.Width / 3, 35, 248);

            Game.OnWndProc += Game_OnWndProc;
        }

        #endregion

        #region Delegates

        public delegate void OnSelectColor(Color color);

        #endregion

        #region Public Properties

        public static int ColorPickerH
        {
            get
            {
                return 282 - 61;
            }
        }

        public static int ColorPickerW
        {
            get
            {
                return 252 - 18;
            }
        }

        public static int ColorPickerX
        {
            get
            {
                return X + 18;
            }
        }

        public static int ColorPickerY
        {
            get
            {
                return Y + 61;
            }
        }

        public static bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                LuminitySprite.Visible = value;
                OpacitySprite.Visible = value;
                BackgroundSprite.Visible = value;
                LuminositySlider.Visible = value;
                AlphaSlider.Visible = value;
                PreviewRectangle.Visible = value;
                _visible = value;
            }
        }

        public static int X
        {
            get
            {
                return _x;
            }
            set
            {
                var oldX = _x;
                _x = value;
                BackgroundSprite.X += value - oldX;
                LuminitySprite.X += value - oldX;
                OpacitySprite.X += value - oldX;
                PreviewRectangle.X += value - oldX;
                LuminositySlider.sX += value - oldX;
                AlphaSlider.sX += value - oldX;
            }
        }

        public static int Y
        {
            get
            {
                return _y;
            }
            set
            {
                var oldY = _y;
                _y = value;
                BackgroundSprite.Y += value - oldY;
                LuminitySprite.Y += value - oldY;
                OpacitySprite.Y += value - oldY;
                PreviewRectangle.Y += value - oldY;
                LuminositySlider.sY += value - oldY;
                AlphaSlider.sY += value - oldY;
            }
        }

        #endregion

        #region Public Methods and Operators

        public static void FireOnChangeColor(Color color)
        {
            if (OnChangeColor != null)
            {
                OnChangeColor(color);
            }
        }

        public static void Load(OnSelectColor onSelectcolor, Color color)
        {
            OnChangeColor = onSelectcolor;
            SColor = color;
            SHue = ((HSLColor)color).Hue;
            SSaturation = ((HSLColor)color).Saturation;

            LuminositySlider.Percent = (float)SColor.Luminosity / 100f;
            AlphaSlider.Percent = color.A / 255f;
            X = (Drawing.Width - BackgroundSprite.Width) / 2;
            Y = (Drawing.Height - BackgroundSprite.Height) / 2;

            Visible = true;
            UpdateLuminosityBitmap(color);
            UpdateOpacityBitmap(color);
            InitialColor = color;
        }

        #endregion

        #region Methods

        private static void Close()
        {
            _selecting = false;
            _moving = false;
            AlphaSlider.Moving = false;
            LuminositySlider.Moving = false;
            AlphaSlider.Visible = false;
            LuminositySlider.Visible = false;
            Visible = false;
        }

        private static void Game_OnWndProc(WndEventArgs args)
        {

            if (!_visible)
            {
                return;
            }

            LuminositySlider.OnWndProc(args);
            AlphaSlider.OnWndProc(args);

            if (args.Msg == (uint)Utils.WindowsMessages.WM_LBUTTONDOWN)
            {
                var pos = Game.MouseScreenPosition;

                if (Utils.IsUnderRectangle(pos, X, Y, BackgroundSprite.Width, 25))
                {
                    _moving = true;
                }

                //Apply button:
                if (Utils.IsUnderRectangle(pos, X + 290, Y + 297, 74, 12))
                {
                    Close();
                    return;
                }

                //Cancel button:
                if (Utils.IsUnderRectangle(pos, X + 370, Y + 296, 73, 14))
                {
                    FireOnChangeColor(InitialColor);
                    Close();
                    return;
                }

                if (Utils.IsUnderRectangle(pos, ColorPickerX, ColorPickerY, ColorPickerW, ColorPickerH))
                {
                    _selecting = true;
                    UpdateColor();
                }
            }
            else if (args.Msg == (uint)Utils.WindowsMessages.WM_LBUTTONUP)
            {
                _moving = false;
                _selecting = false;
            }
            else if (args.Msg == (uint)Utils.WindowsMessages.WM_MOUSEMOVE)
            {
                if (_selecting)
                {
                    var pos = Game.MouseScreenPosition;
                    if (Utils.IsUnderRectangle(pos, ColorPickerX, ColorPickerY, ColorPickerW, ColorPickerH))
                    {
                        UpdateColor();
                    }
                }

                if (_moving)
                {
                    var pos = Game.MouseScreenPosition;
                    X += (int)(pos.X - _prevPos.X);
                    Y += (int)(pos.Y - _prevPos.Y);
                }
                _prevPos = Game.MouseScreenPosition;
            }
        }

        private static void UpdateColor()
        {
            if (_selecting)
            {
                var pos = Game.MouseScreenPosition;
                var color = BackgroundSprite.Bitmap.GetPixel((int)pos.X - X, (int)pos.Y - Y);
                SHue = ((HSLColor)color).Hue;
                SSaturation = ((HSLColor)color).Saturation;
                UpdateLuminosityBitmap(color);
            }

            SColor.Hue = SHue;
            SColor.Saturation = SSaturation;
            SColor.Luminosity = (LuminositySlider.Percent * 100d);
            var r = Color.FromArgb(((int)(AlphaSlider.Percent * 255)), SColor);
            PreviewRectangle.Color = new ColorBGRA(r.R, r.G, r.B, r.A);
            UpdateOpacityBitmap(r);
            FireOnChangeColor(r);
        }

        private static void UpdateLuminosityBitmap(HSLColor color, bool force = false)
        {
            if (Environment.TickCount - LastBitmapUpdate < 100 && !force)
            {
                return;
            }
            LastBitmapUpdate = Environment.TickCount;

            color.Luminosity = 0d;
            for (var y = 0; y < LuminityBitmap.Height; y++)
            {
                for (var x = 0; x < LuminityBitmap.Width; x++)
                {
                    LuminityBitmap.SetPixel(x, y, color);
                }

                color.Luminosity += 100d / LuminityBitmap.Height;
            }

            if (LuminitySprite != null)
            {
                LuminitySprite.UpdateTextureBitmap(LuminityBitmap);
            }
        }

        private static void UpdateOpacityBitmap(HSLColor color, bool force = false)
        {
            if (Environment.TickCount - LastBitmapUpdate2 < 100 && !force)
            {
                return;
            }
            LastBitmapUpdate2 = Environment.TickCount;

            color.Luminosity = 0d;
            for (var y = 0; y < OpacityBitmap.Height; y++)
            {
                for (var x = 0; x < OpacityBitmap.Width; x++)
                {
                    OpacityBitmap.SetPixel(x, y, color);
                }

                color.Luminosity += 40d / LuminityBitmap.Height;
            }

            if (OpacitySprite != null)
            {
                OpacitySprite.UpdateTextureBitmap(OpacityBitmap);
            }
        }

        #endregion

        //From: https://richnewman.wordpress.com/about/code-listings-and-diagrams/hslcolor-class/

        public class CPSlider
        {
            #region Fields

            public int Height;

            public bool Moving;

            internal Render.Sprite ActiveSprite;

            internal Render.Sprite InactiveSprite;

            private readonly int _x;

            private readonly int _y;

            private float _percent;

            private bool _visible = true;

            #endregion

            #region Constructors and Destructors

            public CPSlider(int x, int y, int height, float percent = 1)
            {
                this._x = x;
                this._y = y;
                this.Height = height - Resources.CPActiveSlider.Height;
                this.Percent = percent;
                this.ActiveSprite = new Render.Sprite(Resources.CPActiveSlider, new Vector2(this.sX, this.sY));
                this.InactiveSprite = new Render.Sprite(Resources.SliderDisabled, new Vector2(this.sX, this.sY));

                this.ActiveSprite.Add(2);
                this.InactiveSprite.Add(2);
            }

            #endregion

            #region Public Properties

            public float Percent
            {
                get
                {
                    return this._percent;
                }
                set
                {
                    var newValue = Math.Max(0f, Math.Min(1f, value));
                    this._percent = newValue;
                }
            }

            public int sX
            {
                set
                {
                    this.ActiveSprite.X = this.sX;
                    this.InactiveSprite.X = this.sX;
                }
                get
                {
                    return this._x + X;
                }
            }

            public int sY
            {
                set
                {
                    this.ActiveSprite.Y = this.sY;
                    this.InactiveSprite.Y = this.sY;
                    this.ActiveSprite.Y = this.sY + (int)(this.Percent * this.Height);
                    this.InactiveSprite.Y = this.sY + (int)(this.Percent * this.Height);
                }
                get
                {
                    return this._y + Y;
                }
            }

            public bool Visible
            {
                get
                {
                    return this._visible;
                }
                set
                {
                    this.ActiveSprite.Visible = value;
                    this.InactiveSprite.Visible = value;
                    this._visible = value;
                }
            }

            public int Width
            {
                get
                {
                    return Resources.CPActiveSlider.Width;
                }
            }

            #endregion

            #region Public Methods and Operators

            public void OnWndProc(WndEventArgs args)
            {
                switch (args.Msg)
                {
                    case (uint)Utils.WindowsMessages.WM_LBUTTONDOWN:
                        var pos = Game.MouseScreenPosition;
                        if (Utils.IsUnderRectangle(
                            pos,
                            this.sX,
                            this.sY,
                            this.Width,
                            this.Height + Resources.CPActiveSlider.Height))
                        {
                            this.Moving = true;
                            this.ActiveSprite.Visible = this.Moving;
                            this.InactiveSprite.Visible = !this.Moving;
                            this.UpdatePercent();
                        }
                        break;
                    case (uint)Utils.WindowsMessages.WM_MOUSEMOVE:
                        if (this.Moving)
                        {
                            this.UpdatePercent();
                        }
                        break;
                    case (uint)Utils.WindowsMessages.WM_LBUTTONUP:
                        this.Moving = false;
                        this.ActiveSprite.Visible = this.Moving;
                        this.InactiveSprite.Visible = !this.Moving;
                        break;
                }
            }

            #endregion

            #region Methods

            private void UpdatePercent()
            {
                var pos = Game.MouseScreenPosition;
                this.Percent = (pos.Y - Resources.CPActiveSlider.Height / 2 - this.sY) / this.Height;
                UpdateColor();
                this.ActiveSprite.Y = this.sY + (int)(this.Percent * this.Height);
                this.InactiveSprite.Y = this.sY + (int)(this.Percent * this.Height);
            }

            #endregion
        }

        public class HSLColor
        {
            #region Constants

            //from: https://richnewman.wordpress.com/about/code-listings-and-diagrams/hslcolor-class/
            // Private data members below are on scale 0-1
            // They are scaled for use externally based on scale
            private const double scale = 100.0;

            #endregion

            #region Fields

            private double hue = 1.0;

            private double luminosity = 1.0;

            private double saturation = 1.0;

            #endregion

            #region Constructors and Destructors

            public HSLColor()
            {
            }

            public HSLColor(Color color)
            {
                this.SetRGB(color.R, color.G, color.B);
            }

            public HSLColor(int red, int green, int blue)
            {
                this.SetRGB(red, green, blue);
            }

            public HSLColor(double hue, double saturation, double luminosity)
            {
                this.Hue = hue;
                this.Saturation = saturation;
                this.Luminosity = luminosity;
            }

            #endregion

            #region Public Properties

            public double Hue
            {
                get
                {
                    return this.hue * scale;
                }
                set
                {
                    this.hue = this.CheckRange(value / scale);
                }
            }

            public double Luminosity
            {
                get
                {
                    return this.luminosity * scale;
                }
                set
                {
                    this.luminosity = this.CheckRange(value / scale);
                }
            }

            public double Saturation
            {
                get
                {
                    return this.saturation * scale;
                }
                set
                {
                    this.saturation = this.CheckRange(value / scale);
                }
            }

            #endregion

            #region Public Methods and Operators

            public static implicit operator Color(HSLColor hslColor)
            {
                double r = 0, g = 0, b = 0;
                if (hslColor.luminosity != 0)
                {
                    if (hslColor.saturation == 0)
                    {
                        r = g = b = hslColor.luminosity;
                    }
                    else
                    {
                        var temp2 = GetTemp2(hslColor);
                        var temp1 = 2.0 * hslColor.luminosity - temp2;

                        r = GetColorComponent(temp1, temp2, hslColor.hue + 1.0 / 3.0);
                        g = GetColorComponent(temp1, temp2, hslColor.hue);
                        b = GetColorComponent(temp1, temp2, hslColor.hue - 1.0 / 3.0);
                    }
                }
                return Color.FromArgb((int)(255 * r), (int)(255 * g), (int)(255 * b));
            }

            public static implicit operator HSLColor(Color color)
            {
                var hslColor = new HSLColor
                                   {
                                       hue = color.GetHue() / 360.0, luminosity = color.GetBrightness(),
                                       saturation = color.GetSaturation()
                                   };
                // we store hue as 0-1 as opposed to 0-360 
                return hslColor;
            }

            public void SetRGB(int red, int green, int blue)
            {
                HSLColor hslColor = Color.FromArgb(red, green, blue);
                this.hue = hslColor.hue;
                this.saturation = hslColor.saturation;
                this.luminosity = hslColor.luminosity;
            }

            public string ToRGBString()
            {
                Color color = this;
                return String.Format("R: {0:#0.##} G: {1:#0.##} B: {2:#0.##}", color.R, color.G, color.B);
            }

            public override string ToString()
            {
                return String.Format(
                    "H: {0:#0.##} S: {1:#0.##} L: {2:#0.##}",
                    this.Hue,
                    this.Saturation,
                    this.Luminosity);
            }

            #endregion

            #region Methods

            private static double GetColorComponent(double temp1, double temp2, double temp3)
            {
                temp3 = MoveIntoRange(temp3);
                if (temp3 < 1.0 / 6.0)
                {
                    return temp1 + (temp2 - temp1) * 6.0 * temp3;
                }
                if (temp3 < 0.5)
                {
                    return temp2;
                }
                if (temp3 < 2.0 / 3.0)
                {
                    return temp1 + ((temp2 - temp1) * ((2.0 / 3.0) - temp3) * 6.0);
                }
                return temp1;
            }

            private static double GetTemp2(HSLColor hslColor)
            {
                double temp2;
                if (hslColor.luminosity < 0.5) //<=??
                {
                    temp2 = hslColor.luminosity * (1.0 + hslColor.saturation);
                }
                else
                {
                    temp2 = hslColor.luminosity + hslColor.saturation - (hslColor.luminosity * hslColor.saturation);
                }
                return temp2;
            }

            private static double MoveIntoRange(double temp3)
            {
                if (temp3 < 0.0)
                {
                    temp3 += 1.0;
                }
                else if (temp3 > 1.0)
                {
                    temp3 -= 1.0;
                }
                return temp3;
            }

            private double CheckRange(double value)
            {
                if (value < 0.0)
                {
                    value = 0.0;
                }
                else if (value > 1.0)
                {
                    value = 1.0;
                }
                return value;
            }

            #endregion
        }
    }
}