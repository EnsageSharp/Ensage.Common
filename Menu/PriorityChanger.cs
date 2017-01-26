// <copyright file="PriorityChanger.cs" company="EnsageSharp">
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
    using System.Linq;

    using Ensage.Common.Menu.Draw;
    using Ensage.Common.Objects;

    using SharpDX;

    /// <summary>
    ///     The order changer.
    /// </summary>
    [Serializable]
    public struct PriorityChanger
    {
        #region Static Fields

        /// <summary>
        ///     The id.
        /// </summary>
        private static uint id;

        #endregion

        #region Fields

        /// <summary>
        ///     The ability toggler.
        /// </summary>
        public AbilityToggler AbilityToggler;

        /// <summary>
        ///     The dictionary.
        /// </summary>
        public Dictionary<string, uint> Dictionary;

        /// <summary>
        ///     The position dictionary.
        /// </summary>
        public Dictionary<string, float[]> PositionDictionary;

        /// <summary>
        ///     The s values dictionary.
        /// </summary>
        public Dictionary<string, uint> SValuesDictionary;

        /// <summary>
        ///     The default priority.
        /// </summary>
        private readonly uint defaultPriority;

        /// <summary>
        ///     The max priority.
        /// </summary>
        private readonly uint maxPriority;

        /// <summary>
        ///     The min priority.
        /// </summary>
        private readonly uint minPriority;

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        private readonly string name;

        /// <summary>
        ///     The using ability toggler.
        /// </summary>
        private readonly bool usingAbilityToggler;

        /// <summary>
        ///     The item list.
        /// </summary>
        private List<string> itemList;

        /// <summary>
        ///     The random.
        /// </summary>
        private Random random;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PriorityChanger" /> struct.
        /// </summary>
        /// <param name="itemList">
        ///     The item List.
        /// </param>
        /// <param name="defaultPriority">
        ///     The default Priority.
        /// </param>
        /// <param name="changerName">
        ///     The changer Name.
        /// </param>
        /// <param name="useAbilityToggler">
        ///     The use Ability Toggler.
        /// </param>
        public PriorityChanger(
            List<string> itemList,
            uint defaultPriority,
            string changerName = "",
            bool useAbilityToggler = false)
        {
            this.random = new Random();
            this.itemList = new List<string>();
            this.defaultPriority = defaultPriority;
            this.maxPriority = (uint)itemList.Count();
            this.minPriority = 0;
            this.Dictionary = new Dictionary<string, uint>();
            this.PositionDictionary = new Dictionary<string, float[]>();
            this.SValuesDictionary = new Dictionary<string, uint>();
            this.AbilityToggler = new AbilityToggler(new Dictionary<string, bool>());
            this.name = changerName != string.Empty ? changerName : id.ToString();
            var count = 0u;
            this.usingAbilityToggler = useAbilityToggler;
            foreach (var s in itemList)
            {
                this.Add(s, count);
                count++;
            }

            // foreach (var v in this.Dictionary.Where(v => !Menu.TextureDictionary.ContainsKey(v.Key)))
            // {
            // Menu.TextureDictionary.Add(
            // v.Key, 
            // v.Key.Substring(0, "item".Length) == "item"
            // ? Textures.GetTexture("materials/ensage_ui/items/" + v.Key.Substring("item_".Length) + ".vmat")
            // : Textures.GetTexture("materials/ensage_ui/spellicons/" + v.Key + ".vmat"));
            // }

            // var posDict = this.PositionDictionary;
            // foreach (var v in this.Dictionary.Where(v => !posDict.ContainsKey(v.Key)))
            // {
            // this.PositionDictionary.Add(v.Key, new float[] { 0, 0 });
            // }

            // var saveDict = this.SValuesDictionary;
            // foreach (var v in this.Dictionary.Where(v => !saveDict.ContainsKey(v.Key)))
            // {
            // this.SValuesDictionary.Add(v.Key, v.Value);
            // }
            if (MenuVariables.DragAndDropDictionary == null)
            {
                MenuVariables.DragAndDropDictionary = new Dictionary<string, DragAndDrop>();
            }

            // if (useAbilityToggler)
            // {
            // foreach (var item in itemList)
            // {
            // this.AbilityToggler.Add(item);
            // }
            // }
            id++;
            if (!MenuVariables.DragAndDropDictionary.ContainsKey(this.name))
            {
                MenuVariables.DragAndDropDictionary.Add(
                    this.name,
                    useAbilityToggler
                        ? new DragAndDrop(MenuSettings.MenuItemHeight, itemList, this.AbilityToggler)
                        : new DragAndDrop(MenuSettings.MenuItemHeight, itemList));
            }
            else
            {
                foreach (var u in
                    new Dictionary<PriorityIcon, uint>(
                        MenuVariables.DragAndDropDictionary[this.name].PriorityIconsDictionary).Where(
                        u => !itemList.Contains(u.Key.Name)))
                {
                    MenuVariables.DragAndDropDictionary[this.name].Remove(u.Key.Name);
                }

                MenuVariables.DragAndDropDictionary[this.name].UpdateOrder();
            }

            this.UpdatePriorities();
        }

        public PriorityChanger(List<string> itemList, string changerName = "", bool useAbilityToggler = false)
        {
            this.random = new Random();
            this.itemList = new List<string>();
            this.defaultPriority = 4;
            this.maxPriority = (uint)itemList.Count();
            this.minPriority = 0;
            this.Dictionary = new Dictionary<string, uint>();
            var count = 0u;
            this.usingAbilityToggler = useAbilityToggler;
            this.PositionDictionary = new Dictionary<string, float[]>();
            this.SValuesDictionary = new Dictionary<string, uint>();
            this.name = changerName != string.Empty ? changerName : id.ToString();

            this.AbilityToggler = new AbilityToggler(new Dictionary<string, bool>());
            foreach (var s in itemList)
            {
                this.Add(s, count);
                count++;
            }

            // foreach (var v in this.Dictionary.Where(v => !Menu.TextureDictionary.ContainsKey(v.Key)))
            // {
            // Menu.TextureDictionary.Add(
            // v.Key, 
            // v.Key.Substring(0, "item".Length) == "item"
            // ? Textures.GetTexture("materials/ensage_ui/items/" + v.Key.Substring("item_".Length) + ".vmat")
            // : Textures.GetTexture("materials/ensage_ui/spellicons/" + v.Key + ".vmat"));
            // }

            // var posDict = this.PositionDictionary;
            // foreach (var v in this.Dictionary.Where(v => !posDict.ContainsKey(v.Key)))
            // {
            // this.PositionDictionary.Add(v.Key, new float[] { 0, 0 });
            // }

            // var saveDict = this.SValuesDictionary;
            // foreach (var v in this.Dictionary.Where(v => !saveDict.ContainsKey(v.Key)))
            // {
            // this.SValuesDictionary.Add(v.Key, v.Value);
            // }
            if (MenuVariables.DragAndDropDictionary == null)
            {
                MenuVariables.DragAndDropDictionary = new Dictionary<string, DragAndDrop>();
            }

            // if (useAbilityToggler)
            // {
            // foreach (var item in itemList)
            // {
            // this.AbilityToggler.Add(item);
            // }
            // }
            id++;
            if (!MenuVariables.DragAndDropDictionary.ContainsKey(this.name))
            {
                MenuVariables.DragAndDropDictionary.Add(
                    this.name,
                    useAbilityToggler
                        ? new DragAndDrop(MenuSettings.MenuItemHeight, itemList, this.AbilityToggler)
                        : new DragAndDrop(MenuSettings.MenuItemHeight, itemList));
            }
            else
            {
                foreach (var u in
                    new Dictionary<PriorityIcon, uint>(
                        MenuVariables.DragAndDropDictionary[this.name].PriorityIconsDictionary).Where(
                        u => !itemList.Contains(u.Key.Name)))
                {
                    MenuVariables.DragAndDropDictionary[this.name].Remove(u.Key.Name);
                }

                MenuVariables.DragAndDropDictionary[this.name].UpdateOrder();
            }

            this.UpdatePriorities();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PriorityChanger" /> struct.
        /// </summary>
        /// <param name="itemList">
        ///     The item list.
        /// </param>
        /// <param name="abilityToggler">
        ///     The ability toggler.
        /// </param>
        /// <param name="changerName">
        ///     The changer name.
        /// </param>
        /// <param name="defaultPriority">
        ///     The default Priority.
        /// </param>
        /// <param name="useAbilityToggler">
        ///     The use ability toggler.
        /// </param>
        public PriorityChanger(
            List<string> itemList,
            AbilityToggler abilityToggler,
            string changerName = "",
            uint defaultPriority = 4,
            bool useAbilityToggler = true)
        {
            this.random = new Random();
            this.itemList = new List<string>();
            this.defaultPriority = defaultPriority;
            this.maxPriority = (uint)itemList.Count();
            this.minPriority = 0;
            this.Dictionary = new Dictionary<string, uint>();
            var count = 0u;
            this.usingAbilityToggler = useAbilityToggler;
            this.PositionDictionary = new Dictionary<string, float[]>();
            this.SValuesDictionary = new Dictionary<string, uint>();
            this.name = changerName != string.Empty ? changerName : id.ToString();
            this.AbilityToggler = abilityToggler;
            foreach (var s in itemList)
            {
                this.Add(s, count);
                count++;
            }

            // foreach (var v in this.Dictionary.Where(v => !Menu.TextureDictionary.ContainsKey(v.Key)))
            // {
            // Menu.TextureDictionary.Add(
            // v.Key, 
            // v.Key.Substring(0, "item".Length) == "item"
            // ? Textures.GetTexture("materials/ensage_ui/items/" + v.Key.Substring("item_".Length) + ".vmat")
            // : Textures.GetTexture("materials/ensage_ui/spellicons/" + v.Key + ".vmat"));
            // }

            // var posDict = this.PositionDictionary;
            // foreach (var v in this.Dictionary.Where(v => !posDict.ContainsKey(v.Key)))
            // {
            // this.PositionDictionary.Add(v.Key, new float[] { 0, 0 });
            // }

            // var saveDict = this.SValuesDictionary;
            // foreach (var v in this.Dictionary.Where(v => !saveDict.ContainsKey(v.Key)))
            // {
            // this.SValuesDictionary.Add(v.Key, v.Value);
            // }
            if (MenuVariables.DragAndDropDictionary == null)
            {
                MenuVariables.DragAndDropDictionary = new Dictionary<string, DragAndDrop>();
            }

            id++;
            if (!MenuVariables.DragAndDropDictionary.ContainsKey(this.name))
            {
                MenuVariables.DragAndDropDictionary.Add(
                    this.name,
                    new DragAndDrop(MenuSettings.MenuItemHeight, itemList, abilityToggler));
            }

            this.UpdatePriorities();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the item list.
        /// </summary>
        public List<string> ItemList
        {
            get
            {
                return this.itemList;
            }

            set
            {
                this.itemList = value;
            }
        }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add.
        /// </summary>
        /// <param name="itemName">
        ///     The item name.
        /// </param>
        /// <param name="defaultValue">
        ///     The default value.
        /// </param>
        /// <param name="enabled">
        ///     The enabled.
        /// </param>
        public void Add(string itemName, uint defaultValue = 0, bool enabled = true)
        {
            var textureName = itemName;
            if (this.ItemList.Contains(itemName))
            {
                return;
            }

            if (this.SValuesDictionary.ContainsKey(itemName))
            {
                defaultValue = this.SValuesDictionary[itemName];
            }

            if (!this.Dictionary.ContainsKey(itemName))
            {
                this.Dictionary.Add(itemName, defaultValue);
            }

            this.ItemList.Add(itemName);
            if (!Menu.TextureDictionary.ContainsKey(itemName))
            {
                Menu.TextureDictionary.Add(
                    itemName,
                    textureName.Substring(0, "item".Length) == "item"
                        ? Textures.GetTexture(
                            "materials/ensage_ui/items/" + textureName.Substring("item_".Length) + ".vmat")
                        : Textures.GetTexture("materials/ensage_ui/spellicons/" + textureName + ".vmat"));
            }

            if (!this.SValuesDictionary.ContainsKey(itemName))
            {
                this.SValuesDictionary.Add(itemName, defaultValue);
            }

            if (this.usingAbilityToggler)
            {
                this.AbilityToggler.Add(itemName, textureName, enabled);
            }

            this.UpdatePriorities();
            if (!MenuVariables.DragAndDropDictionary.ContainsKey(this.name))
            {
                MenuVariables.DragAndDropDictionary.Add(
                    this.name,
                    new DragAndDrop(MenuSettings.MenuItemHeight, this.itemList));
            }
            else
            {
                MenuVariables.DragAndDropDictionary[this.name].Add(itemName, defaultValue, enabled);
            }

            MenuVariables.DragAndDropDictionary[this.name].UpdateOrder();

            if (this.PositionDictionary.ContainsKey(itemName))
            {
                return;
            }

            this.PositionDictionary.Add(itemName, new float[] { 0, 0 });
        }

        /// <summary>
        ///     The decrease priority.
        /// </summary>
        /// <param name="itemName">
        ///     The item name.
        /// </param>
        public void DecreasePriority(string itemName)
        {
            if (!this.Dictionary.ContainsKey(itemName))
            {
                return;
            }

            if (this.Dictionary[itemName] <= this.minPriority)
            {
                return;
            }

            this.Dictionary[itemName] = this.Dictionary[itemName] - 1;
        }

        /// <summary>
        ///     The draw.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        /// <param name="width">
        ///     The width.
        /// </param>
        /// <param name="height">
        ///     The height.
        /// </param>
        /// <param name="mouseScreenPosition">
        ///     The mouse screen position.
        /// </param>
        /// <param name="menuItem">
        ///     The menu item.
        /// </param>
        public void Draw(Vector2 position, float width, float height, Vector2 mouseScreenPosition, MenuItem menuItem)
        {
            if (!MenuVariables.DragAndDropDictionary.ContainsKey(this.name))
            {
                MenuVariables.DragAndDropDictionary.Add(
                    this.name,
                    new DragAndDrop(MenuSettings.MenuItemHeight, this.itemList));
            }

            MenuVariables.DragAndDropDictionary[this.name].BasePosition = position + new Vector2(width - height, 0);
            MenuVariables.DragAndDropDictionary[this.name].Height = height;
            MenuVariables.DragAndDropDictionary[this.name].Draw(mouseScreenPosition, menuItem);
        }

        /// <summary>
        ///     The is enabled.
        /// </summary>
        /// <param name="itemName">
        ///     The item name.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public uint GetPriority(string itemName)
        {
            return this.Dictionary.ContainsKey(itemName) ? this.Dictionary[itemName] : this.defaultPriority;
        }

        /// <summary>
        ///     The increase priority.
        /// </summary>
        /// <param name="itemName">
        ///     The item name.
        /// </param>
        public void IncreasePriority(string itemName)
        {
            if (!this.Dictionary.ContainsKey(itemName))
            {
                return;
            }

            if (this.Dictionary[itemName] >= this.maxPriority - 1)
            {
                return;
            }

            this.Dictionary[itemName] = this.Dictionary[itemName] + 1;
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
        /// <param name="menuItem">
        ///     The menu item.
        /// </param>
        public bool OnReceiveMessage(Utils.WindowsMessages message, Vector2 cursorPos, MenuItem menuItem)
        {
            if (!MenuVariables.DragAndDropDictionary.ContainsKey(this.name))
            {
                MenuVariables.DragAndDropDictionary.Add(
                    this.name,
                    new DragAndDrop(MenuSettings.MenuItemHeight, this.itemList));
            }

            if (message == Utils.WindowsMessages.WM_LBUTTONDOWN)
            {
                MenuVariables.DragAndDropDictionary[this.name].LeftButtonDown(cursorPos);
                return false;
            }

            if (message == Utils.WindowsMessages.WM_LBUTTONUP)
            {
                return MenuVariables.DragAndDropDictionary[this.name].LeftButtonUp(cursorPos, menuItem);
            }

            return false;
        }

        /// <summary>
        ///     The remove.
        /// </summary>
        /// <param name="itemName">
        ///     The item name.
        /// </param>
        public void Remove(string itemName)
        {
            if (!this.Dictionary.ContainsKey(itemName))
            {
                return;
            }

            this.Dictionary.Remove(itemName);
            this.ItemList.Remove(itemName);
            MenuVariables.DragAndDropDictionary[this.name].Remove(itemName);
            MenuVariables.DragAndDropDictionary[this.name].UpdateOrder();
        }

        /// <summary>
        ///     The set priority.
        /// </summary>
        /// <param name="itemName">
        ///     The item name.
        /// </param>
        /// <param name="priorityValue">
        ///     The priority value.
        /// </param>
        public void SetPriority(string itemName, uint priorityValue)
        {
            if (!this.Dictionary.ContainsKey(itemName))
            {
                return;
            }

            this.Dictionary[itemName] = priorityValue;
        }

        /// <summary>
        ///     The update priorities.
        /// </summary>
        public void UpdatePriorities()
        {
            var count = 0u;
            foreach (var u in new Dictionary<string, uint>(this.Dictionary).OrderBy(x => x.Value))
            {
                this.Dictionary[u.Key] = count;
                this.SValuesDictionary[u.Key] = count;
                count++;
            }
        }

        #endregion
    }
}