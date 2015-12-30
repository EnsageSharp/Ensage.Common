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

    using Ensage.Common.Extensions;
    using Ensage.Common.Extensions.SharpDX;
    using Ensage.Common.Menu.NotificationData;
    using Ensage.Common.Objects;

    using SharpDX;

    using Color = System.Drawing.Color;

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

    /// <summary>
    /// </summary>
    [Serializable]
    public struct Slider
    {
        #region Fields

        /// <summary>
        /// </summary>
        public int MaxValue;

        /// <summary>
        /// </summary>
        public int MinValue;

        private int value;

        #endregion

        #region Constructors and Destructors

        /// <summary>
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
    /// </summary>
    [Serializable]
    public struct StringList
    {
        #region Fields

        /// <summary>
        /// </summary>
        public int SelectedIndex;

        /// <summary>
        /// </summary>
        public string[] SList;

        #endregion

        #region Constructors and Destructors

        /// <summary>
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
    ///     Creates
    /// </summary>
    [Serializable]
    public struct AbilityToggler
    {
        #region Fields

        /// <summary>
        /// </summary>
        public Dictionary<string, bool> Dictionary;

        /// <summary>
        /// </summary>
        public Dictionary<string, float[]> PositionDictionary;

        /// <summary>
        /// </summary>
        public Dictionary<string, bool> SValuesDictionary;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// </summary>
        /// <param name="abilityDictionary"></param>
        public AbilityToggler(Dictionary<string, bool> abilityDictionary)
        {
            this.Dictionary = abilityDictionary;
            this.PositionDictionary = new Dictionary<string, float[]>();
            this.SValuesDictionary = new Dictionary<string, bool>();
            foreach (var v in this.Dictionary.Where(v => !Menu.TextureDictionary.ContainsKey(v.Key)))
            {
                Menu.TextureDictionary.Add(
                    v.Key,
                    v.Key.Substring(0, "item".Length) == "item"
                        ? Drawing.GetTexture("materials/ensage_ui/items/" + v.Key.Substring("item_".Length) + ".vmat")
                        : Drawing.GetTexture("materials/ensage_ui/spellicons/" + v.Key + ".vmat"));
            }
            var posDict = this.PositionDictionary;
            foreach (var v in this.Dictionary.Where(v => !posDict.ContainsKey(v.Key)))
            {
                this.PositionDictionary.Add(v.Key, new float[] { 0, 0 });
            }
            var svDict = this.SValuesDictionary;
            foreach (var v in this.Dictionary.Where(v => !svDict.ContainsKey(v.Key)))
            {
                this.SValuesDictionary.Add(v.Key, v.Value);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        public void Add(string name, bool defaultValue = true)
        {
            if (this.Dictionary.ContainsKey(name))
            {
                Console.WriteLine(@"This ability(" + name + @") is already added in AbilityToggler");
                return;
            }
            if (this.SValuesDictionary.ContainsKey(name))
            {
                defaultValue = this.SValuesDictionary[name];
            }
            this.Dictionary.Add(name, defaultValue);
            if (!Menu.TextureDictionary.ContainsKey(name))
            {
                Menu.TextureDictionary.Add(
                    name,
                    name.Substring(0, "item".Length) == "item"
                        ? Drawing.GetTexture("materials/ensage_ui/items/" + name.Substring("item_".Length) + ".vmat")
                        : Drawing.GetTexture("materials/ensage_ui/spellicons/" + name + ".vmat"));
            }
            if (!this.SValuesDictionary.ContainsKey(name))
            {
                this.SValuesDictionary.Add(name, defaultValue);
            }
            if (this.PositionDictionary.ContainsKey(name))
            {
                return;
            }
            this.PositionDictionary.Add(name, new float[] { 0, 0 });
        }

        /// <summary>
        /// </summary>
        public bool IsEnabled(string name)
        {
            return this.Dictionary.ContainsKey(name) && this.Dictionary[name];
        }

        /// <summary>
        /// </summary>
        public void Remove(string name)
        {
            if (this.Dictionary.ContainsKey(name))
            {
                this.Dictionary.Remove(name);
            }
        }

        #endregion
    }

    /// <summary>
    ///     Creates
    /// </summary>
    [Serializable]
    public struct HeroToggler
    {
        #region Fields

        /// <summary>
        /// </summary>
        public bool DefaultValues;

        /// <summary>
        /// </summary>
        public Dictionary<string, bool> Dictionary;

        /// <summary>
        /// </summary>
        public Dictionary<string, float[]> PositionDictionary;

        /// <summary>
        /// </summary>
        public Dictionary<string, bool> SValuesDictionary;

        /// <summary>
        /// </summary>
        public bool UseAllyHeroes;

        /// <summary>
        /// </summary>
        public bool UseEnemyHeroes;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// </summary>
        /// <param name="heroDictionary"></param>
        /// <param name="useEnemyHeroes"></param>
        /// <param name="useAllyHeroes"></param>
        /// <param name="defaultValues"></param>
        public HeroToggler(
            Dictionary<string, bool> heroDictionary,
            bool useEnemyHeroes = false,
            bool useAllyHeroes = false,
            bool defaultValues = true)
        {
            this.Dictionary = heroDictionary;
            this.PositionDictionary = new Dictionary<string, float[]>();
            this.UseEnemyHeroes = useEnemyHeroes;
            this.UseAllyHeroes = useAllyHeroes;
            this.SValuesDictionary = new Dictionary<string, bool>();
            this.DefaultValues = defaultValues;
            foreach (var v in this.Dictionary.Where(v => !Menu.TextureDictionary.ContainsKey(v.Key)))
            {
                Menu.TextureDictionary.Add(
                    v.Key,
                    Drawing.GetTexture(
                        "materials/ensage_ui/heroes_horizontal/" + v.Key.Substring("npc_dota_hero_".Length) + ".vmat"));
            }
            var posDict = this.PositionDictionary;
            foreach (var v in this.Dictionary.Where(v => !posDict.ContainsKey(v.Key)))
            {
                this.PositionDictionary.Add(v.Key, new float[] { 0, 0 });
            }
            var svDict = this.SValuesDictionary;
            foreach (var v in this.Dictionary.Where(v => !svDict.ContainsKey(v.Key)))
            {
                this.SValuesDictionary.Add(v.Key, v.Value);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        public void Add(string name, bool defaultValue = true)
        {
            if (this.Dictionary.ContainsKey(name))
            {
                Console.WriteLine(@"This hero(" + name + @") is already added in HeroToggler");
                return;
            }
            if (this.SValuesDictionary.ContainsKey(name))
            {
                defaultValue = this.SValuesDictionary[name];
            }
            this.Dictionary.Add(name, defaultValue);
            if (!Menu.TextureDictionary.ContainsKey(name))
            {
                Menu.TextureDictionary.Add(
                    name,
                    Drawing.GetTexture(
                        "materials/ensage_ui/heroes_horizontal/" + name.Substring("npc_dota_hero_".Length) + ".vmat"));
            }
            if (!this.SValuesDictionary.ContainsKey(name))
            {
                this.SValuesDictionary.Add(name, defaultValue);
            }
            if (this.PositionDictionary.ContainsKey(name))
            {
                return;
            }
            this.PositionDictionary.Add(name, new float[] { 0, 0 });
        }

        /// <summary>
        /// </summary>
        public bool IsEnabled(string name)
        {
            return this.Dictionary.ContainsKey(name) && this.Dictionary[name];
        }

        /// <summary>
        /// </summary>
        public void Remove(string name)
        {
            if (this.Dictionary.ContainsKey(name))
            {
                this.Dictionary.Remove(name);
            }
        }

        #endregion
    }

    /// <summary>
    /// </summary>
    public enum KeyBindType
    {
        /// <summary>
        /// </summary>
        Toggle,

        /// <summary>
        /// </summary>
        Press
    }

    /// <summary>
    /// </summary>
    [Serializable]
    public struct KeyBind
    {
        #region Fields

        /// <summary>
        /// </summary>
        public bool Active;

        /// <summary>
        /// </summary>
        public uint Key;

        /// <summary>
        /// </summary>
        public KeyBindType Type;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <param name="defaultValue"></param>
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

    /// <summary>
    /// </summary>
    public static class MenuGlobals
    {
        #region Static Fields

        /// <summary>
        /// </summary>
        public static bool DrawMenu;

        /// <summary>
        /// </summary>
        public static List<string> MenuState = new List<string>();

        #endregion
    }

    internal static class MenuSettings
    {
        #region Static Fields

        public static Vector2 BasePosition = new Vector2(10, (float)(HUDInfo.ScreenSizeY() * 0.06));

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
                return Color.FromArgb(190, 48, 48, 48);
            }
        }

        public static Color BackgroundColor
        {
            get
            {
                return Color.FromArgb(180, Color.Black);
            }
        }

        public static int MenuFontSize { get; set; }

        public static int MenuItemHeight
        {
            get
            {
                return Math.Min(Math.Max((int)(HUDInfo.GetHpBarSizeY() * 3), 21), 33)
                       + Menu.Root.Item("EnsageSharp.Common.IncreaseSize").GetValue<Slider>().Value * 2; //32
            }
        }

        public static int MenuItemWidth
        {
            get
            {
                return Math.Max((int)(HUDInfo.GetHPBarSizeX() * 2), 180)
                       + Menu.Root.Item("EnsageSharp.Common.IncreaseSize").GetValue<Slider>().Value; //160
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
            var textSize = Drawing.MeasureText(
                s,
                "Arial",
                new Vector2((float)(item.Height * 0.51), item.Height / 2),
                FontFlags.AntiAlias);
            var textPos = position
                          + new Vector2(
                                (float)(item.Height * 0.5 - textSize.X * 0.5),
                                (float)(item.Height * 0.5 - textSize.Y * 0.5) + 1);
            Drawing.DrawText(
                s,
                textPos,
                new Vector2((float)(item.Height * 0.51), item.Height / 2),
                Utils.IsUnderRectangle(Game.MouseScreenPosition, position.X, position.Y, item.Height, item.Height)
                    ? Color.DarkOrange.ToSharpDxColor()
                    : Color.White.ToSharpDxColor(),
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
                on
                    ? Color.FromArgb(180 + alpha, 120 + alpha, 1 + alpha).ToSharpDxColor()
                    : Color.FromArgb(37 + alpha, 37 + alpha, 37 + alpha).ToSharpDxColor(),
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
                new Vector2((float)(item.Height * 0.52), (float)item.Width / 2),
                FontFlags.AntiAlias);
            var textPos = item.Position
                          + new Vector2(
                                item.Width - item.Height / 2 - textSize.X / 2,
                                (float)(+item.Height * 0.5 - textSize.Y / 2));
            Drawing.DrawText(
                s,
                textPos,
                new Vector2((float)(item.Height * 0.52), (float)item.Width / 2),
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
                    new Vector2((float)(item.Height * 0.52), (float)item.Width / 2),
                    FontFlags.AntiAlias);
                var textPos = position
                              + new Vector2(
                                    (float)(item.Width - item.Height * 0.5 - 2 - textSize.X * 0.5),
                                    (float)(+item.Height * 0.5 - textSize.Y * 0.5));
                Drawing.DrawText(
                    value.ToString(),
                    textPos,
                    new Vector2((float)(item.Height * 0.52), (float)item.Width / 2),
                    Color.DarkOrange.ToSharpDxColor(),
                    FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom
                    | FontFlags.StrikeOut);
            }
        }

        internal static void DrawToolTip_Button(Vector2 position, MenuItem item)
        {
            if (item.ValueType == MenuValueType.StringList || item.ValueType == MenuValueType.AbilityToggler
                || item.ValueType == MenuValueType.HeroToggler)
            {
                return;
            }

            const string s = "[?]";
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
                new Vector2((float)(item.Height * 0.53), item.Width / 2),
                FontFlags.AntiAlias);
            var textPos = item.Position
                          + new Vector2(
                                item.Width - item.Height - textSize.X - 2,
                                (float)(item.Height * 0.5 - textSize.Y * 0.5 - 1));
            Drawing.DrawText(
                s,
                textPos,
                new Vector2((float)(item.Height * 0.53), item.Width / 2),
                Color.DarkGray.ToSharpDxColor(),
                FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom | FontFlags.StrikeOut);
        }

        internal static void DrawToolTip_Text(Vector2 position, MenuItem item, SharpDX.Color? TextColor = null)
        {
            if (item.ValueType == MenuValueType.StringList || item.ValueType == MenuValueType.AbilityToggler
                || item.ValueType == MenuValueType.HeroToggler)
            {
                return;
            }
            var s = item.Tooltip;
            var textSize = Drawing.MeasureText(
                s,
                "Arial",
                new Vector2((float)(item.Height * 0.51), 14),
                FontFlags.AntiAlias);
            MenuUtils.DrawBoxBordered(
                position.X + 3,
                position.Y,
                textSize.X + 8,
                item.Height,
                1,
                new SharpDX.Color(45, 37, 13, 170),
                SharpDX.Color.Black);

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

            var textPos = position + new Vector2(6, (float)(item.Height * 0.5 - textSize.Y * 0.5));
            Drawing.DrawText(
                s,
                textPos,
                new Vector2((float)(item.Height * 0.51), 14),
                Color.White.ToSharpDxColor(),
                FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom | FontFlags.StrikeOut);
        }

        #endregion
    }

    /// <summary>
    /// </summary>
    public class Menu
    {
        #region Static Fields

        /// <summary>
        /// </summary>
        public static readonly Menu Root = new Menu("Menu Settings", "Menu Settings");

        /// <summary>
        /// </summary>
        public static Dictionary<string, MenuItem> ItemDictionary;

        /// <summary>
        /// </summary>
        public static Dictionary<string, Vector2> menuPositionDictionary = new Dictionary<string, Vector2>();

        /// <summary>
        /// </summary>
        public static Dictionary<string, Menu> RootMenus = new Dictionary<string, Menu>();

        /// <summary>
        /// </summary>
        public static Dictionary<string, DotaTexture> TextureDictionary;

        private static StringList newMessageType;

        #endregion

        #region Fields

        /// <summary>
        /// </summary>
        public List<Menu> Children = new List<Menu>();

        /// <summary>
        /// </summary>
        public SharpDX.Color Color;

        /// <summary>
        /// </summary>
        public string DisplayName;

        /// <summary>
        /// </summary>
        public bool IsRootMenu;

        /// <summary>
        /// </summary>
        public List<MenuItem> Items = new List<MenuItem>();

        /// <summary>
        /// </summary>
        public string Name;

        /// <summary>
        /// </summary>
        public Menu Parent;

        /// <summary>
        /// </summary>
        public bool ShowTextWithTexture;

        /// <summary>
        /// </summary>
        public FontStyle Style;

        /// <summary>
        /// </summary>
        public string TextureName;

        private int cachedMenuCount = 2;

        private int cachedMenuCountT;

        private string uniqueId;

        private bool visible;

        #endregion

        #region Constructors and Destructors

        static Menu()
        {
            TextureDictionary = new Dictionary<string, DotaTexture>();
            ItemDictionary = new Dictionary<string, MenuItem>();
            Root.AddItem(new MenuItem("pressKey", "Menu hold key").SetValue(new KeyBind(16, KeyBindType.Press)));
            Root.AddItem(new MenuItem("toggleKey", "Menu toggle key").SetValue(new KeyBind(118, KeyBindType.Toggle)));
            Root.AddItem(new MenuItem("showMessage", "Show OnLoad message: ").SetValue(true));
            var message =
                Root.AddItem(
                    new MenuItem("messageType", "Show the message in: ").SetValue(
                        new StringList(new[] { "SideLog", "Chat", "Console" })));
            Root.AddItem(
                new MenuItem("EnsageSharp.Common.IncreaseSize", "Size increase: ").SetValue(new Slider(0, 0, 25)))
                .SetTooltip("Increases size of text and boxes");
            Root.AddItem(
                new MenuItem("EnsageSharp.Common.TooltipDuration", "Tooltip Notification Duration").SetValue(
                    new Slider(1500, 0, 5000)));
            Root.AddItem(
                new MenuItem("EnsageSharp.Common.BlockKeys", "Block player inputs for KeyBinds: ").SetValue(true));
            Root.AddItem(
                new MenuItem("FontInfo", "Press F5 after your change").SetFontStyle(
                    FontStyle.Bold,
                    SharpDX.Color.Yellow));
            Events.OnLoad += Events_OnLoad;
            if (Game.IsInGame && ObjectMgr.LocalHero != null)
            {
                Events_OnLoad(null, null);
            }
            CommonMenu.MenuConfig.AddSubMenu(Root);
            message.ValueChanged += MessageValueChanged;
            newMessageType = Root.Item("messageType").GetValue<StringList>();
        }

        /// <summary>
        ///     Creates a Menu in Common.Menu class
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="name"></param>
        /// <param name="isRootMenu"></param>
        /// <param name="textureName"></param>
        /// <param name="showTextWithTexture"></param>
        /// <exception cref="ArgumentException"></exception>
        public Menu(
            string displayName,
            string name,
            bool isRootMenu = false,
            string textureName = null,
            bool showTextWithTexture = false)
        {
            this.DisplayName = displayName;
            this.Name = name;
            this.IsRootMenu = isRootMenu;
            this.Style = FontStyle.Regular;
            this.Color = SharpDX.Color.White;
            this.TextureName = textureName;
            this.ShowTextWithTexture = showTextWithTexture;
            if (textureName != null && !TextureDictionary.ContainsKey(textureName))
            {
                if (textureName.Contains("npc_dota_hero_"))
                {
                    TextureDictionary.Add(
                        textureName,
                        Drawing.GetTexture(
                            "materials/ensage_ui/heroes_horizontal/" + textureName.Substring("npc_dota_hero_".Length)
                            + ".vmat"));
                }
                else if (textureName.Contains("item_"))
                {
                    TextureDictionary.Add(
                        textureName,
                        Drawing.GetTexture(
                            "materials/ensage_ui/items/" + textureName.Substring("item_".Length) + ".vmat"));
                }
                else
                {
                    TextureDictionary.Add(
                        textureName,
                        Drawing.GetTexture("materials/ensage_ui/spellicons/" + textureName + ".vmat"));
                }
            }
            if (isRootMenu)
            {
                AppDomain.CurrentDomain.DomainUnload += delegate { SaveAll(); };
                AppDomain.CurrentDomain.ProcessExit += delegate { SaveAll(); };
                Events.OnClose += delegate { SaveAll(); };

                var rootName = Assembly.GetCallingAssembly().GetName().Name + "." + name;

                if (RootMenus.ContainsKey(rootName))
                {
                    throw new ArgumentException("Root Menu [" + rootName + "] with the same name exists", "name");
                }

                RootMenus.Add(rootName, this);
            }
        }

        /// <summary>
        /// </summary>
        ~Menu()
        {
            var rootName = Assembly.GetCallingAssembly().GetName().Name + "." + this.Name;
            if (RootMenus.ContainsKey(rootName))
            {
                RootMenus.Remove(rootName);
            }
        }

        #endregion

        #region Properties

        internal int ChildrenMenuWidth
        {
            get
            {
                var result = this.Children.Select(item => item.NeededWidth).Concat(new[] { 0 }).Max();

                return this.Items.Select(item => item.NeededWidth).Concat(new[] { result }).Max();
            }
        }

        internal int Height
        {
            get
            {
                return MenuSettings.MenuItemHeight;
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
                    return MenuSettings.BasePosition + this.MenuCount * new Vector2(0, MenuSettings.MenuItemHeight)
                           + new Vector2(5, 0);
                }

                return this.Parent.MyBasePosition + new Vector2(5, 0);
            }
        }

        internal int NeededWidth
        {
            get
            {
                var bonus = 0;
                if (this.TextureName == null || this.ShowTextWithTexture)
                {
                    bonus +=
                        (int)
                        Drawing.MeasureText(
                            MultiLanguage._(this.DisplayName),
                            "Arial",
                            new Vector2(
                            (float)(MenuSettings.MenuItemHeight * 0.55),
                            (float)(MenuSettings.MenuItemWidth * 0.7)),
                            FontFlags.None).X;
                }
                if (this.TextureName != null)
                {
                    var tName = this.TextureName;
                    if (tName.Contains("npc_dota_hero"))
                    {
                        bonus += 15;
                    }
                    else if (tName.Contains("item_"))
                    {
                        bonus += -4;
                    }
                    else
                    {
                        bonus += -4;
                    }
                }
                var arrow = Math.Max((int)(HUDInfo.GetHpBarSizeY() * 2.5), 17);
                if ((5 + arrow + bonus) < (float)(MenuSettings.MenuItemWidth - MenuSettings.MenuItemHeight * 0.3))
                {
                    arrow = 4;
                }
                return this.Height + bonus + arrow;
            }
        }

        internal Vector2 Position
        {
            get
            {
                var n = this.Name + this.DisplayName;
                if (!Utils.SleepCheck(n))
                {
                    return menuPositionDictionary[n];
                }
                int xOffset;

                if (this.Parent != null)
                {
                    xOffset = (int)(this.Parent.Position.X + this.Parent.Width + 1);
                }
                else
                {
                    xOffset = (int)this.MyBasePosition.X;
                }
                var pos = new Vector2(0, this.MyBasePosition.Y) + new Vector2(xOffset, 0)
                          + this.YLevel * new Vector2(0, MenuSettings.MenuItemHeight);
                if (!menuPositionDictionary.ContainsKey(n))
                {
                    menuPositionDictionary.Add(n, pos);
                }
                else
                {
                    menuPositionDictionary[n] = pos;
                }
                Utils.Sleep(1000, n);
                return pos;
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

        /// <summary>
        /// </summary>
        /// <param name="assemblyname"></param>
        /// <param name="menuname"></param>
        /// <returns></returns>
        public static Menu GetMenu(string assemblyname, string menuname)
        {
            var menu = RootMenus.FirstOrDefault(x => x.Key == assemblyname + "." + menuname).Value;
            return menu;
        }

        /// <summary>
        /// </summary>
        /// <param name="assemblyname"></param>
        /// <param name="menuname"></param>
        /// <param name="itemname"></param>
        /// <param name="submenu"></param>
        /// <returns></returns>
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
        /// </summary>
        /// <param name="key"></param>
        /// <param name="message"></param>
        public static void SendMessage(uint key, Utils.WindowsMessages message)
        {
            foreach (var menu in RootMenus)
            {
                menu.Value.OnReceiveMessage(message, Game.MouseScreenPosition, key);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public MenuItem AddItem(MenuItem item)
        {
            item.Parent = this;
            item.Visible = (this.Children.Count > 0 && this.Children[0].Visible
                            || this.Items.Count > 0 && this.Items[0].Visible);
            this.Items.Add(item);
            return item;
        }

        /// <summary>
        /// </summary>
        /// <param name="subMenu"></param>
        /// <returns></returns>
        public Menu AddSubMenu(Menu subMenu)
        {
            subMenu.Parent = this;
            subMenu.Visible = this.Children.Count > 0 && this.Children[0].Visible;
            this.Children.Add(subMenu);
            return subMenu;
        }

        /// <summary>
        /// </summary>
        public void AddToMainMenu()
        {
            this.InitMenuState(Assembly.GetCallingAssembly().GetName().Name);
            AppDomain.CurrentDomain.DomainUnload += (sender, args) => this.UnloadMenuState();
            Drawing.OnDraw += this.Drawing_OnDraw;
            Game.OnWndProc += this.Game_OnWndProc;
        }

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="makeChampionUniq"></param>
        /// <returns></returns>
        public MenuItem Item(string name, bool makeChampionUniq = false)
        {
            if (makeChampionUniq)
            {
                name = ObjectMgr.LocalHero.Name + name;
            }
            MenuItem tempItem;
            if (ItemDictionary.TryGetValue(this.Name + name, out tempItem))
            {
                return tempItem;
            }
            tempItem = this.Items.FirstOrDefault(x => x.Name == name)
                       ?? (from subMenu in this.Children where subMenu.Item(name) != null select subMenu.Item(name))
                              .FirstOrDefault();
            return tempItem;
        }

        /// <summary>
        /// </summary>
        public void RemoveFromMainMenu()
        {
            try
            {
                var rootName = Assembly.GetCallingAssembly().GetName().Name + "." + this.Name;
                if (RootMenus.ContainsKey(rootName))
                {
                    RootMenus.Remove(rootName);
                    Drawing.OnDraw -= this.Drawing_OnDraw;
                    Game.OnWndProc -= this.Game_OnWndProc;
                    this.UnloadMenuState();
                }
            }
            catch (Exception)
            {
                //
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
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
        /// </summary>
        /// <param name="fontStyle"></param>
        /// <param name="fontColor"></param>
        /// <returns></returns>
        public Menu SetFontStyle(FontStyle fontStyle = FontStyle.Regular, SharpDX.Color? fontColor = null)
        {
            this.Style = fontStyle;
            this.Color = fontColor ?? SharpDX.Color.White;

            return this;
        }

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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
            this.SetHeroTogglers();

            foreach (var child in this.Children)
            {
                child.SetHeroTogglers();
                foreach (var child1 in child.Children)
                {
                    child1.SetHeroTogglers();
                }
            }

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
                    : MenuSettings.BackgroundColor.ToSharpDxColor(),
                new SharpDX.Color(35, 30, 25, 255));

            //MenuDrawHelper.Font.DrawText(
            //    null,
            //    MultiLanguage._(this.DisplayName),
            //    new Rectangle((int)this.Position.X + 5, (int)this.Position.Y, this.Width, this.Height),
            //    FontDrawFlags.VerticalCenter,
            //    this.Color);
            var textSize = Drawing.MeasureText(
                MultiLanguage._(this.DisplayName),
                "Arial",
                new Vector2((float)(this.Height * 0.55), 100),
                FontFlags.AntiAlias);
            var textPos = this.Position + new Vector2(5, (float)(this.Height * 0.5 - textSize.Y * 0.5));
            var bonusWidth = 0;
            if (this.TextureName == null)
            {
                Drawing.DrawText(
                    MultiLanguage._(this.DisplayName),
                    textPos,
                    new Vector2((float)(this.Height * 0.55), 100),
                    this.Color,
                    FontFlags.AntiAlias | FontFlags.Additive | FontFlags.Custom);
            }
            else
            {
                var tName = this.TextureName;
                if (tName.Contains("npc_dota_hero"))
                {
                    Drawing.DrawRect(
                        this.Position + new Vector2(3, 3),
                        new Vector2(this.Height + 13, this.Height - 6),
                        TextureDictionary[tName]);
                    Drawing.DrawRect(
                        this.Position + new Vector2(2, 2),
                        new Vector2(this.Height + 15, this.Height - 4),
                        SharpDX.Color.Black,
                        true);
                    bonusWidth = this.Height + 17;
                }
                else if (tName.Contains("item_"))
                {
                    Drawing.DrawRect(
                        this.Position + new Vector2(3, 3),
                        new Vector2(this.Height + (float)(this.Height * 0.16), this.Height - 6),
                        TextureDictionary[tName]);
                    Drawing.DrawRect(
                        this.Position + new Vector2(2, 2),
                        new Vector2(this.Height - 4, this.Height - 4),
                        SharpDX.Color.Black,
                        true);
                    bonusWidth = this.Height - 2;
                }
                else
                {
                    Drawing.DrawRect(
                        this.Position + new Vector2(3, 3),
                        new Vector2(this.Height - 6, this.Height - 6),
                        TextureDictionary[tName]);
                    Drawing.DrawRect(
                        this.Position + new Vector2(2, 2),
                        new Vector2(this.Height - 4, this.Height - 4),
                        SharpDX.Color.Black,
                        true);
                    bonusWidth = this.Height - 2;
                }
                if (this.ShowTextWithTexture)
                {
                    Drawing.DrawText(
                        MultiLanguage._(this.DisplayName),
                        textPos + new Vector2(bonusWidth, 0),
                        new Vector2((float)(this.Height * 0.55), 100),
                        this.Color,
                        FontFlags.AntiAlias | FontFlags.Additive | FontFlags.Custom);
                }
            }

            //MenuDrawHelper.Font.DrawText(
            //    null,
            //    ">",
            //    new Rectangle((int)this.Position.X - 5, (int)this.Position.Y, this.Width, this.Height),
            //    FontDrawFlags.Right | FontDrawFlags.VerticalCenter,
            //    this.Color);
            //Console.WriteLine((3 + textSize.X + bonusWidth) + "   " + (float)(this.Width - this.Height * 0.5));
            if ((5 + textSize.X + bonusWidth) < (float)(this.Width - this.Height * 0.3))
            {
                textSize = Drawing.MeasureText(
                    "»",
                    "Arial",
                    new Vector2((float)(this.Height * 0.65), 12),
                    FontFlags.AntiAlias);
                textPos = this.Position
                          + new Vector2(
                                (float)(this.Width - this.Height * 0.35 - textSize.X * 0.5),
                                (float)(this.Height * 0.5 - textSize.Y * 0.5));
                Drawing.DrawText(
                    "»",
                    textPos,
                    new Vector2((float)(this.Height * 0.65), 12),
                    System.Drawing.Color.FromArgb(225, System.Drawing.Color.Orange).ToSharpDxColor(),
                    FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom
                    | FontFlags.StrikeOut | FontFlags.Outline);
            }
            Drawing.DrawRect(
                new Vector2(this.Position.X, this.Position.Y),
                new Vector2(this.Width, this.Height),
                (this.Children.Count > 0 && this.Children[0].Visible || this.Items.Count > 0 && this.Items[0].Visible)
                    ? new SharpDX.Color(0, 0, 0, 5)
                    : (Utils.IsUnderRectangle(
                        Game.MouseScreenPosition,
                        this.Position.X,
                        this.Position.Y,
                        this.Width,
                        this.Height)
                           ? new SharpDX.Color(0, 0, 0, 8)
                           : new SharpDX.Color(0, 0, 0, 120)));
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

        internal void OnReceiveMessage(
            Utils.WindowsMessages message,
            Vector2 cursorPos,
            uint key,
            WndEventArgs args = null)
        {
            //Spread the message to the menu's children recursively

            foreach (var item in this.Items)
            {
                item.OnReceiveMessage(message, cursorPos, key, args);
                //Console.WriteLine(args != null && item.IsInside(cursorPos));
            }
            foreach (var child in this.Children)
            {
                child.OnReceiveMessage(message, cursorPos, key, args);
            }
            if (!this.Visible)
            {
                return;
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

        internal void SetHeroTogglers()
        {
            foreach (var child in this.Children)
            {
                child.SetHeroTogglers();
            }
            for (var i = this.Items.Count - 1; i >= 0; i--)
            {
                var item = this.Items[i];
                if (!Utils.SleepCheck("SetHeroTogglers" + this.Name + item.Name))
                {
                    continue;
                }
                if (item.ValueType != MenuValueType.HeroToggler)
                {
                    continue;
                }
                var set = false;
                if (item.GetValue<HeroToggler>().UseEnemyHeroes && item.GetValue<HeroToggler>().Dictionary.Count < 5)
                {
                    var dict = item.GetValue<HeroToggler>().Dictionary;
                    var sdict = item.GetValue<HeroToggler>().SValuesDictionary;
                    var players =
                        Players.All.Where(
                            x =>
                            x.Hero != null && x.Hero.IsValid && x.Hero.Team == ObjectMgr.LocalHero.GetEnemyTeam()
                            && !dict.ContainsKey(x.Hero.Name)).ToList();
                    if (players.Any())
                    {
                        set = true;
                    }
                    foreach (var x in
                        players)
                    {
                        item.GetValue<HeroToggler>()
                            .Add(
                                x.Hero.Name,
                                sdict.ContainsKey(x.Hero.Name)
                                    ? sdict[x.Hero.Name]
                                    : item.GetValue<HeroToggler>().DefaultValues);
                    }
                    item.SetValue(
                        new HeroToggler(
                            item.GetValue<HeroToggler>().Dictionary,
                            true,
                            false,
                            item.GetValue<HeroToggler>().DefaultValues));
                }
                else if (item.GetValue<HeroToggler>().UseAllyHeroes && item.GetValue<HeroToggler>().Dictionary.Count < 4)
                {
                    var dict = item.GetValue<HeroToggler>().Dictionary;
                    var sdict = item.GetValue<HeroToggler>().SValuesDictionary;
                    var players =
                        Players.All.Where(
                            x =>
                            x.Hero != null && x.Hero.IsValid && x.Hero.Team == ObjectMgr.LocalHero.Team
                            && !dict.ContainsKey(x.Hero.Name)).ToList();
                    if (players.Any())
                    {
                        set = true;
                    }
                    foreach (var x in players)
                    {
                        item.GetValue<HeroToggler>()
                            .Add(
                                x.Hero.Name,
                                sdict.ContainsKey(x.Hero.Name)
                                    ? sdict[x.Hero.Name]
                                    : item.GetValue<HeroToggler>().DefaultValues);
                    }
                    item.SetValue(
                        new HeroToggler(
                            item.GetValue<HeroToggler>().Dictionary,
                            false,
                            true,
                            item.GetValue<HeroToggler>().DefaultValues));
                }
                Utils.Sleep(set ? 20000 : 2000, "SetHeroTogglers" + this.Name + item.Name);
            }
        }

        private static void Events_OnLoad(object sender, EventArgs e)
        {
            //var console = newMessageType.SelectedIndex == 2;
            //if (Root.Item("showMessage").GetValue<bool>() && !console)
            //{
            //var msg =
            //    "<font face='Verdana' color='#ff7700'>[</font>Menu Hotkeys<font face='Verdana' color='#ff7700'>]</font> Press: <font face='Verdana' color='#ff7700'>"
            //    + Utils.KeyToText(Root.Item("toggleKey").GetValue<KeyBind>().Key)
            //    + "</font> Hold: <font face='Verdana' color='#ff7700'>"
            //    + Utils.KeyToText(Root.Item("pressKey").GetValue<KeyBind>().Key) + "</font>";
            const string Msg1 =
                "<font face='Verdana' color='#b8e4ff'><font color='#ff3c33'>*</font><font color='#009e00'>~</font><font color='#ff3c33'>*</font><font color='#009e00'>~</font><font color='#ff3c33'>*</font> Merry Christmas and Happy New Year <font color='#ff3c33'>*</font><font color='#009e00'>~</font><font color='#ff3c33'>*</font><font color='#009e00'>~</font><font color='#ff3c33'>*</font></font>";
            const string Msg2 =
                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font face='Verdana' color='#b8e4ff'><font color='#ff3c33'>*</font><font color='#009e00'>~</font><font color='#ff3c33'>*</font><font color='#009e00'>~</font><font color='#ff3c33'>*</font> EnsageSharp Team <font color='#ff3c33'>*</font><font color='#009e00'>~</font><font color='#ff3c33'>*</font><font color='#009e00'>~</font><font color='#ff3c33'>*</font></font>";
            Game.PrintMessage(Msg1, MessageType.LogMessage);
            Game.PrintMessage(Msg2, MessageType.LogMessage);
            //Game.PrintMessage(Msg,
            //    (newMessageType.SelectedIndex == 2 || newMessageType.SelectedIndex == 0)
            //        ? MessageType.LogMessage
            //        : MessageType.ChatMessage);
            //}
            //else if (console && Root.Item("showMessage").GetValue<bool>())
            //{
            //    var msg = @"[Menu Hotkeys] Press: " + Utils.KeyToText(Root.Item("toggleKey").GetValue<KeyBind>().Key)
            //              + @" Hold: " + Utils.KeyToText(Root.Item("pressKey").GetValue<KeyBind>().Key);
            //    Console.WriteLine(msg);
            //}
        }

        private static void MessageValueChanged(object sender, OnValueChangeEventArgs e)
        {
            newMessageType = e.GetNewValue<StringList>();
            Events_OnLoad(null, null);
        }

        private void InitMenuState(string assemblyName)
        {
            this.uniqueId = assemblyName + "." + this.Name;

            var globalMenuList = MenuGlobals.MenuState;

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

        AbilityToggler,

        HeroToggler
    }

    /// <summary>
    /// </summary>
    public class OnValueChangeEventArgs
    {
        #region Fields

        private readonly object _newValue;

        private readonly object _oldValue;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public OnValueChangeEventArgs(object oldValue, object newValue)
        {
            this._oldValue = oldValue;
            this._newValue = newValue;
            this.Process = true;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// </summary>
        public bool Process { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetNewValue<T>()
        {
            return (T)this._newValue;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetOldValue<T>()
        {
            return (T)this._oldValue;
        }

        #endregion
    }

    /// <summary>
    /// </summary>
    public class MenuItem
    {
        #region Fields

        /// <summary>
        /// </summary>
        public string DisplayName;

        /// <summary>
        /// </summary>
        public ColorBGRA FontColor;

        /// <summary>
        /// </summary>
        public FontStyle FontStyle;

        /// <summary>
        /// </summary>
        public int MenuFontSize;

        /// <summary>
        /// </summary>
        public string Name;

        /// <summary>
        /// </summary>
        public Menu Parent;

        /// <summary>
        /// </summary>
        public bool ShowItem;

        /// <summary>
        /// </summary>
        public int Tag;

        /// <summary>
        /// </summary>
        public string Tooltip;

        /// <summary>
        /// </summary>
        public SharpDX.Color TooltipColor;

        internal bool DrawingTooltip;

        internal bool Interacting;

        internal bool ValueSet;

        internal MenuValueType ValueType;

        private readonly string _MenuConfigName;

        private bool _dontSave;

        private bool _isShared;

        private byte[] _serialized;

        private object _value;

        private bool _visible;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        /// <param name="makeChampionUniq"></param>
        public MenuItem(string name, string displayName, bool makeChampionUniq = false)
        {
            if (makeChampionUniq)
            {
                name = ObjectMgr.LocalHero.Name + name;
            }

            this.Name = name;
            this.DisplayName = displayName;
            this.FontStyle = FontStyle.Regular;
            this.FontColor = new SharpDX.Color(195, 186, 173, 255);
            this.ShowItem = true;
            this.Tag = 0;
            this._MenuConfigName = Assembly.GetCallingAssembly().GetName().Name
                                   + Assembly.GetCallingAssembly().GetType().GUID;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// </summary>
        public event EventHandler<OnValueChangeEventArgs> ValueChanged;

        #endregion

        #region Public Properties

        /// <summary>
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
                    extra += this.GetValue<AbilityToggler>().Dictionary.Count * (this.Height - 10);
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
                }

                return
                    (int)
                    Drawing.MeasureText(
                        MultiLanguage._(this.DisplayName),
                        "Arial",
                        new Vector2((float)(this.Height * 0.51), 20),
                        FontFlags.AntiAlias).X + this.Height * 2 + Math.Max((int)(HUDInfo.GetHpBarSizeY() * 1.8), 8)
                    + extra;
            }
        }

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
                          + this.YLevel * new Vector2(0, MenuSettings.MenuItemHeight);
                if (!Menu.menuPositionDictionary.ContainsKey(n))
                {
                    Menu.menuPositionDictionary.Add(n, pos);
                }
                else
                {
                    Menu.menuPositionDictionary[n] = pos;
                }
                Utils.Sleep(500, n);
                return pos;
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

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public MenuItem DontSave()
        {
            this._dontSave = true;
            return this;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetValue<T>()
        {
            return (T)this._value;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
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
        /// </summary>
        /// <param name="fontStyle"></param>
        /// <param name="fontColor"></param>
        /// <returns></returns>
        public MenuItem SetFontStyle(FontStyle fontStyle = FontStyle.Regular, SharpDX.Color? fontColor = null)
        {
            this.FontStyle = fontStyle;
            this.FontColor = fontColor ?? SharpDX.Color.White;

            return this;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public MenuItem SetShared()
        {
            this._isShared = true;
            return this;
        }

        /// <summary>
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public MenuItem SetTag(int tag = 0)
        {
            this.Tag = tag;

            return this;
        }

        /// <summary>
        /// </summary>
        /// <param name="tooltip"></param>
        /// <param name="tooltipColor"></param>
        /// <returns></returns>
        public MenuItem SetTooltip(string tooltip, SharpDX.Color? tooltipColor = null)
        {
            this.Tooltip = tooltip;
            this.TooltipColor = tooltipColor ?? SharpDX.Color.White;
            return this;
        }

        /// <summary>
        /// </summary>
        /// <param name="newValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
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
                            var newDictionaryValue = ((AbilityToggler)(object)newValue);
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
                            var newHeroDictionaryValue = ((HeroToggler)(object)newValue);
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
            this.ValueSet = true;
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

        /// <summary>
        /// </summary>
        /// <param name="showItem"></param>
        /// <returns></returns>
        public MenuItem Show(bool showItem = true)
        {
            this.ShowItem = showItem;

            return this;
        }

        /// <summary>
        /// </summary>
        /// <param name="hide"></param>
        public void ShowTooltip(bool hide = false)
        {
            if (!string.IsNullOrEmpty(this.Tooltip))
            {
                this.DrawingTooltip = !hide;
            }
        }

        /// <summary>
        /// </summary>
        public void ShowTooltipNotification()
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
                Color.FromArgb(140, 28, 28, 28).ToSharpDxColor(),
                SharpDX.Color.Black);
            var s = MultiLanguage._(this.DisplayName);
            if (this.DrawingTooltip)
            {
                MenuDrawHelper.DrawToolTip_Text(
                    new Vector2(this.Position.X + this.Width, this.Position.Y),
                    this,
                    this.TooltipColor);
            }
            //if (this.ValueType == MenuValueType.HeroToggler)
            //{
            //    if (this.GetValue<HeroToggler>().UseEnemyHeroes && this.GetValue<HeroToggler>().Dictionary.Count < 5)
            //    {
            //        var dict = this.GetValue<HeroToggler>().Dictionary;
            //        var sdict = this.GetValue<HeroToggler>().SValuesDictionary;
            //        var players =
            //            ObjectMgr.GetEntities<Player>()
            //                .Where(
            //                    x =>
            //                    x.Hero != null && x.Hero.Team == ObjectMgr.LocalHero.GetEnemyTeam()
            //                    && !dict.ContainsKey(x.Hero.Name));
            //        foreach (var x in
            //            players)
            //        {
            //            this.GetValue<HeroToggler>()
            //                .Add(x.Hero.Name, !sdict.ContainsKey(x.Hero.Name) || sdict[x.Hero.Name]);
            //        }
            //        this.SetValue(new HeroToggler(this.GetValue<HeroToggler>().Dictionary, true));
            //    }
            //    else if (this.GetValue<HeroToggler>().UseAllyHeroes && this.GetValue<HeroToggler>().Dictionary.Count < 4)
            //    {
            //        var dict = this.GetValue<HeroToggler>().Dictionary;
            //        var sdict = this.GetValue<HeroToggler>().SValuesDictionary;
            //        foreach (var x in
            //            ObjectMgr.GetEntities<Player>()
            //                .Where(
            //                    x =>
            //                    x.Hero != null && x.Hero.Team == ObjectMgr.LocalHero.Team
            //                    && !dict.ContainsKey(x.Hero.Name)))
            //        {
            //            this.GetValue<HeroToggler>()
            //                .Add(x.Hero.Name, !sdict.ContainsKey(x.Hero.Name) || sdict[x.Hero.Name]);
            //        }
            //        this.SetValue(new HeroToggler(this.GetValue<HeroToggler>().Dictionary, false, true));
            //    }
            //}
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
                    var textPos = this.Position
                                  + new Vector2(
                                        this.Width - this.Height - textSize.X - 22,
                                        (float)(this.Height * 0.5 - textSize.Y * 0.5) - 1);
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
                        new Vector2(this.Height / 2 + 1, this.Width / 2 + 10),
                        new SharpDX.Color(195 + alpha, 139 + alpha, 12 + alpha, 225),
                        FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom
                        | FontFlags.StrikeOut);
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
                        FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom
                        | FontFlags.StrikeOut);
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
                        new Vector2(this.Height / 2 + 1, this.Width / 2 + 10),
                        FontFlags.AntiAlias);
                    textPos = this.Position
                              + new Vector2(
                                    (float)(-this.Height * 2 + this.Width - textSize.X - 5),
                                    (float)(this.Height * 0.5 - textSize.Y * 0.509));
                    Drawing.DrawText(
                        MultiLanguage._(t),
                        textPos,
                        new Vector2(this.Height / 2 + 1, this.Width / 2 + 10),
                        new SharpDX.Color(255, 255, 255, 225),
                        FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom
                        | FontFlags.StrikeOut);
                    break;

                case MenuValueType.AbilityToggler:

                    var width = 0f;
                    var basePosition = this.Position + new Vector2(this.Width - this.Height, 0);
                    var size = new Vector2(this.Height - 6, this.Height - 6);
                    var dictionary = new Dictionary<string, bool>(this.GetValue<AbilityToggler>().Dictionary);
                    var positionDictionary =
                        new Dictionary<string, float[]>(this.GetValue<AbilityToggler>().PositionDictionary);
                    var textureDictionary = new Dictionary<string, DotaTexture>(Menu.TextureDictionary);
                    //textSize = Drawing.MeasureText("x", "Arial", size + new Vector2(30, 30), FontFlags.AntiAlias);
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
                                ? Color.FromArgb(180 + alpha, 120 + alpha, 1 + alpha).ToSharpDxColor()
                                : Color.FromArgb(37 + alpha, 37 + alpha, 37 + alpha).ToSharpDxColor());
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

                case MenuValueType.HeroToggler:

                    width = 0f;
                    basePosition = this.Position + new Vector2(this.Width - this.Height - 16, 0);
                    size = new Vector2(this.Height + 10, this.Height - 6);
                    dictionary = new Dictionary<string, bool>(this.GetValue<HeroToggler>().Dictionary);
                    positionDictionary = new Dictionary<string, float[]>(
                        this.GetValue<HeroToggler>().PositionDictionary);
                    textureDictionary = new Dictionary<string, DotaTexture>(Menu.TextureDictionary);
                    //textSize = Drawing.MeasureText("x", "Arial", size + new Vector2(30, 30), FontFlags.AntiAlias);
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
                                ? Color.FromArgb(180 + alpha, 120 + alpha, 1 + alpha).ToSharpDxColor()
                                : Color.FromArgb(37 + alpha, 37 + alpha, 37 + alpha).ToSharpDxColor());
                        Drawing.DrawRect(pos - new Vector2(-3, -3), size, textureDictionary[v.Key]);
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
            var textSize1 = Drawing.MeasureText(
                s,
                "Arial",
                new Vector2((float)(this.Height * 0.51), 20),
                FontFlags.AntiAlias);
            var textPos1 = this.Position + new Vector2(5, (float)(this.Height * 0.5 - textSize1.Y * 0.5));
            Drawing.DrawText(
                s,
                textPos1,
                new Vector2((float)(this.Height * 0.51), 20),
                (s == MultiLanguage._("Press new key")) ? new SharpDX.Color(150, 100, 0) : (SharpDX.Color)this.FontColor,
                FontFlags.AntiAlias | FontFlags.DropShadow | FontFlags.Additive | FontFlags.Custom | FontFlags.StrikeOut);

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
}