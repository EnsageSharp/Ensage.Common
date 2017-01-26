// <copyright file="DragAndDrop.cs" company="EnsageSharp">
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
namespace Ensage.Common.Menu.Draw
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.Common.Extensions.SharpDX;
    using Ensage.Common.Menu.Transitions;
    using Ensage.Common.Objects.UtilityObjects;

    using SharpDX;

    /// <summary>
    ///     The drag and drop.
    /// </summary>
    public class DragAndDrop
    {
        #region Fields

        /// <summary>
        ///     The double click sleeper.
        /// </summary>
        private readonly Sleeper doubleClickSleeper;

        /// <summary>
        ///     The icon count sleeper.
        /// </summary>
        private readonly Sleeper iconCountSleeper;

        /// <summary>
        ///     The icon size sleeper.
        /// </summary>
        private readonly Sleeper iconSizeSleeper;

        /// <summary>
        ///     The item list.
        /// </summary>
        private readonly List<string> itemList;

        /// <summary>
        ///     The transition.
        /// </summary>
        private readonly QuadEaseInOut transition;

        /// <summary>
        ///     The using ability toggler.
        /// </summary>
        private readonly bool usingAbilityToggler;

        /// <summary>
        ///     The ability toggler.
        /// </summary>
        private AbilityToggler abilityToggler;

        /// <summary>
        ///     The icon count.
        /// </summary>
        private int iconCount;

        /// <summary>
        ///     The icon size.
        /// </summary>
        private Vector2 iconSize;

        /// <summary>
        ///     The left button down.
        /// </summary>
        private bool leftButtonDown;

        /// <summary>
        ///     The texture icon size.
        /// </summary>
        private Vector2 textureIconSize;

        /// <summary>
        ///     The updated icons.
        /// </summary>
        private bool updatedIcons;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DragAndDrop" /> class.
        /// </summary>
        /// <param name="height">
        ///     The height.
        /// </param>
        /// <param name="itemList">
        ///     The item list.
        /// </param>
        public DragAndDrop(float height, List<string> itemList)
        {
            this.Height = height;
            var count = 0u;
            this.PriorityIconsDictionary = new Dictionary<PriorityIcon, uint>();
            this.itemList = new List<string>(itemList);
            foreach (var s in itemList)
            {
                this.PriorityIconsDictionary.Add(new PriorityIcon(s, height, true), count);
                count++;
            }

            this.doubleClickSleeper = new Sleeper();
            this.iconCountSleeper = new Sleeper();
            this.iconSizeSleeper = new Sleeper();
            this.transition = new QuadEaseInOut(0.35);
            this.transition.Start(0, 150);
            this.abilityToggler = new AbilityToggler(new Dictionary<string, bool>());
            this.Width = itemList.Count * this.Height;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DragAndDrop" /> class.
        /// </summary>
        /// <param name="height">
        ///     The height.
        /// </param>
        /// <param name="itemList">
        ///     The item list.
        /// </param>
        /// <param name="abilityToggler">
        ///     The ability toggler.
        /// </param>
        public DragAndDrop(float height, List<string> itemList, AbilityToggler abilityToggler)
        {
            this.Height = height;
            var count = 0u;
            this.PriorityIconsDictionary = new Dictionary<PriorityIcon, uint>();
            this.itemList = new List<string>(itemList);
            foreach (var s in itemList)
            {
                this.PriorityIconsDictionary.Add(new PriorityIcon(s, height, abilityToggler.IsEnabled(s)), count);
                count++;
            }

            this.doubleClickSleeper = new Sleeper();
            this.iconCountSleeper = new Sleeper();
            this.iconSizeSleeper = new Sleeper();
            this.transition = new QuadEaseInOut(0.35);
            this.transition.Start(0, 150);
            this.abilityToggler = abilityToggler;
            this.usingAbilityToggler = true;
            this.Width = itemList.Count * this.Height;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the base position.
        /// </summary>
        public Vector2 BasePosition { get; set; }

        /// <summary>
        ///     Gets the count.
        /// </summary>
        public int Count
        {
            get
            {
                if (this.iconCountSleeper.Sleeping)
                {
                    return this.iconCount;
                }

                this.iconCount = this.PriorityIconsDictionary.Count;
                this.iconCountSleeper.Sleep(1000);
                return this.iconCount;
            }
        }

        /// <summary>
        ///     Gets or sets the height.
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        ///     Gets or sets the icon size.
        /// </summary>
        public Vector2 IconSize
        {
            get
            {
                return this.iconSize;
            }

            set
            {
                if (this.iconSizeSleeper.Sleeping)
                {
                    return;
                }

                this.iconSize = value;
                this.textureIconSize = new Vector2((float)(value.X * 0.85), (float)(value.X * 0.85));
                this.iconSizeSleeper.Sleep(2000);
            }
        }

        /// <summary>
        ///     Gets or sets the mouse position difference.
        /// </summary>
        public Vector2 MousePositionDifference { get; set; }

        /// <summary>
        ///     Gets or sets the moving icon.
        /// </summary>
        public PriorityIcon MovingIcon { get; set; }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        ///     Gets or sets the priority icons dictionary.
        /// </summary>
        public Dictionary<PriorityIcon, uint> PriorityIconsDictionary { get; set; }

        /// <summary>
        ///     Gets or sets the width.
        /// </summary>
        public float Width { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The add.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <param name="customPriority">
        ///     The custom priority.
        /// </param>
        /// <param name="enabled">
        ///     The enabled.
        /// </param>
        public void Add(string name, uint customPriority = 0, bool enabled = true)
        {
            this.PriorityIconsDictionary.Add(
                new PriorityIcon(name, this.Height, true),
                customPriority != 0 ? customPriority : (uint)this.PriorityIconsDictionary.Count);
            this.UpdateOrder();
            this.itemList.Add(name);
        }

        /// <summary>
        ///     The draw.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        /// <param name="menuItem">
        ///     The menu Item.
        /// </param>
        public void Draw(Vector2 mousePosition, MenuItem menuItem)
        {
            this.IconSize = new Vector2(this.Height, this.Height);
            var move = 0f;
            var priorityChanger = menuItem.GetValue<PriorityChanger>();

            var dictionary = new Dictionary<PriorityIcon, uint>(this.PriorityIconsDictionary);
            var count = 0u;
            foreach (var u in dictionary)
            {
                if (u.Key.Enabled)
                {
                    continue;
                }

                u.Key.Priority = priorityChanger.GetPriority(u.Key.Name);
                u.Key.DictionaryPosition = count;
                this.HandlePriorityIcon(u.Key, mousePosition, count, move, menuItem);
                count++;
                this.PriorityIconsDictionary[u.Key] = u.Key.Priority;
                if (u.Key.Moving)
                {
                    move += this.IconSize.X;
                    move += -1;
                    continue;
                }

                u.Key.Draw();
                u.Key.DrawPriorityNumber();
                move += u.Key.Size.X;
                move += -1;
            }

            var enabledIcons = dictionary.OrderBy(x => priorityChanger.GetPriority(x.Key.Name));

            if (count > 0 && enabledIcons.Any(u => u.Key.Enabled))
            {
                Drawing.DrawRect(
                    this.BasePosition - new Vector2(move - this.IconSize.X / 2 + this.Height / 10, 0),
                    new Vector2(this.Height / 5, this.Height),
                    new Color(180, 120, 10));
                Drawing.DrawRect(
                    this.BasePosition - new Vector2(move - this.IconSize.X / 2 + this.Height / 10, 0),
                    new Vector2(this.Height / 5, this.Height),
                    Color.Black,
                    true);
                move += this.IconSize.X;
            }

            foreach (var u in enabledIcons)
            {
                if (!u.Key.Enabled)
                {
                    continue;
                }

                u.Key.Priority = priorityChanger.GetPriority(u.Key.Name);
                u.Key.DictionaryPosition = count;
                count++;
                this.PriorityIconsDictionary[u.Key] = u.Key.Priority;
                this.HandlePriorityIcon(u.Key, mousePosition, count, move, menuItem);
                if (u.Key.Moving)
                {
                    move += this.IconSize.X;
                    move += -1;
                    continue;
                }

                u.Key.Draw();
                u.Key.DrawPriorityNumber();
                move += u.Key.Size.X;
                move += -1;
            }

            this.Width = move;

            this.DrawShadow(
                this.BasePosition - new Vector2(move - this.IconSize.X, 0),
                new Vector2(move, this.IconSize.Y));
            if (this.MovingIcon == null)
            {
                return;
            }

            this.MovingIcon.Draw();
            this.MovingIcon.DrawPriorityNumber();

            // if (!this.MovingIcon.Moving && this.MovingIcon.FixedPosition == this.MovingIcon.Position
            // && !this.doubleClickSleeper.Sleeping)
            // {
            // this.MovingIcon = null;
            // }
        }

        /// <summary>
        ///     The left button down.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse Position.
        /// </param>
        public void LeftButtonDown(Vector2 mousePosition)
        {
            foreach (var u in this.PriorityIconsDictionary)
            {
                if (!u.Key.Hovered)
                {
                    continue;
                }

                if (this.usingAbilityToggler)
                {
                    this.doubleClickSleeper.Sleep(200);
                }

                this.leftButtonDown = true;
                this.MovingIcon = u.Key;
                DelayAction.Add(
                    this.usingAbilityToggler ? 100 : 10,
                    () =>
                        {
                            if (this.leftButtonDown == false || this.MovingIcon == null)
                            {
                                return;
                            }

                            this.transition.Start(0, 150);
                            this.MovingIcon.Moving = true;
                            this.MovingIcon.Size = this.MovingIcon.Size * new Vector2((float)1.22);
                            this.MovingIcon.IconSize = this.MovingIcon.IconSize * new Vector2((float)1.22)
                                                       - new Vector2(3, 3);
                            this.MovingIcon.Height = this.MovingIcon.Height * (float)1.22;
                            this.MovingIcon.Position =
                                new Vector2(
                                    this.MovingIcon.Position.X - (this.MovingIcon.Size.X - this.IconSize.X) / 2,
                                    this.BasePosition.Y - this.MovingIcon.Size.Y / 20);
                            this.MovingIcon.IconPosition = this.MovingIcon.Position
                                                           + new Vector2(
                                                               this.MovingIcon.Size.X / 2
                                                               - this.MovingIcon.IconSize.X / 2,
                                                               this.MovingIcon.Size.Y / 2
                                                               - this.MovingIcon.IconSize.Y / 2);
                            this.MousePositionDifference = mousePosition - this.MovingIcon.Position;
                        });
                break;
            }
        }

        /// <summary>
        ///     The left button up.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse Position.
        /// </param>
        /// <param name="menuItem">
        ///     The menu Item.
        /// </param>
        public bool LeftButtonUp(Vector2 mousePosition, MenuItem menuItem)
        {
            this.leftButtonDown = false;
            if (this.MovingIcon == null)
            {
                return false;
            }

            if (this.doubleClickSleeper.Sleeping && this.usingAbilityToggler)
            {
                this.MovingIcon.Enabled = !this.MovingIcon.Enabled;
                this.MovingIcon.DictionaryPosition = (uint)(this.PriorityIconsDictionary.Count(x => !x.Key.Enabled) + 1);
                this.abilityToggler.SValuesDictionary[this.MovingIcon.Name] = this.MovingIcon.Enabled;
                this.abilityToggler.Dictionary[this.MovingIcon.Name] = this.MovingIcon.Enabled;
                this.MovingIcon.ReturnFromDrag(
                    this.MovingIcon.Position
                    + new Vector2(
                        (this.MovingIcon.Size.X - this.IconSize.X) / 2,
                        (this.MovingIcon.Size.Y - this.IconSize.Y) / 2));
                this.MovingIcon.Moving = false;
                this.MovingIcon = null;
                this.UpdateOrder();
                return true;
            }

            this.MovingIcon.ReturnFromDrag(
                this.MovingIcon.Position
                + new Vector2(
                    (this.MovingIcon.Size.X - this.IconSize.X) / 2,
                    (this.MovingIcon.Size.Y - this.IconSize.Y) / 2));
            this.transition.Start(0, 150);
            this.MovingIcon.Moving = false;
            this.MovingIcon = null;
            return true;
        }

        /// <summary>
        ///     The remove.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        public void Remove(string name)
        {
            if (!this.itemList.Contains(name))
            {
                return;
            }

            this.PriorityIconsDictionary.Remove(
                this.PriorityIconsDictionary.FirstOrDefault(x => x.Key.Name == name).Key);
            this.UpdateOrder();
            this.itemList.Remove(name);
        }

        /// <summary>
        ///     The update item list.
        /// </summary>
        /// <param name="newList">
        ///     The new list.
        /// </param>
        /// <param name="menuItem">
        ///     The menu Item.
        /// </param>
        public void UpdateItemList(List<string> newList, MenuItem menuItem)
        {
            var count = 0u;

            foreach (var s in newList)
            {
                if (this.itemList.Contains(s))
                {
                    continue;
                }

                this.PriorityIconsDictionary.Add(new PriorityIcon(s, this.Height, true), count);
                menuItem.GetValue<PriorityChanger>().Dictionary[s] = count;
                this.itemList.Add(s);
                count++;
            }
        }

        /// <summary>
        ///     The update order.
        /// </summary>
        public void UpdateOrder()
        {
            var count = 0u;
            var dictionary = new Dictionary<PriorityIcon, uint>(this.PriorityIconsDictionary);
            foreach (var u in dictionary)
            {
                if (u.Key.Enabled)
                {
                    continue;
                }

                u.Key.DictionaryPosition = count;
                count++;
            }

            var count2 = 0u;
            foreach (var u in dictionary.OrderBy(x => x.Value))
            {
                if (!u.Key.Enabled)
                {
                    continue;
                }

                u.Key.DictionaryPosition = count;
                this.PriorityIconsDictionary[u.Key] = count2;
                count2++;
                count++;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The check moving icon position.
        /// </summary>
        /// <param name="icon">
        ///     The icon.
        /// </param>
        /// <param name="menuItem">
        ///     The menu item.
        /// </param>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        private void CheckMovingIconPosition(PriorityIcon icon, MenuItem menuItem, Vector2 mousePosition)
        {
            if (this.usingAbilityToggler && icon.Position.X > this.BasePosition.X)
            {
                icon.Enabled = false;
                this.abilityToggler.SValuesDictionary[this.MovingIcon.Name] = false;
                this.abilityToggler.Dictionary[this.MovingIcon.Name] = false;
                return;
            }

            var dict = new Dictionary<PriorityIcon, uint>(this.PriorityIconsDictionary);
            foreach (var u2 in dict)
            {
                if (!(Utils.SleepCheck(u2.Key.Name + "DragAndDropChangePriority") && u2.Key.Name != icon.Name))
                {
                    continue;
                }

                if ((u2.Key.DictionaryPosition == icon.DictionaryPosition - 1
                     || this.usingAbilityToggler && !u2.Key.Enabled
                     && u2.Key.DictionaryPosition > icon.DictionaryPosition) && mousePosition.X > u2.Key.FixedPosition.X)
                {
                    if (!u2.Key.Enabled)
                    {
                        icon.Enabled = false;
                        u2.Key.DictionaryPosition = icon.DictionaryPosition;
                        icon.DictionaryPosition = u2.Key.DictionaryPosition;
                        this.abilityToggler.SValuesDictionary[this.MovingIcon.Name] = false;
                        this.abilityToggler.Dictionary[this.MovingIcon.Name] = false;
                        Utils.Sleep(500, u2.Key.Name + "DragAndDropChangePriority");
                        break;
                    }

                    var menuItemDict = menuItem.GetValue<PriorityChanger>().Dictionary;
                    menuItemDict[u2.Key.Name] = icon.Priority;
                    menuItemDict[icon.Name] = u2.Key.Priority;
                    var menuItemDict2 = menuItem.GetValue<PriorityChanger>().SValuesDictionary;
                    menuItemDict2[u2.Key.Name] = icon.Priority;
                    menuItemDict2[icon.Name] = u2.Key.Priority;
                    this.PriorityIconsDictionary[u2.Key] = icon.Priority;
                    this.PriorityIconsDictionary[icon] = u2.Key.Priority;
                    u2.Key.DictionaryPosition = icon.Priority;
                    icon.DictionaryPosition = u2.Key.Priority;
                    Utils.Sleep(500, u2.Key.Name + "DragAndDropChangePriority");
                    break;
                }

                if ((u2.Key.DictionaryPosition == icon.DictionaryPosition + 1
                     || this.usingAbilityToggler && !icon.Enabled && u2.Key.DictionaryPosition > icon.DictionaryPosition)
                    && mousePosition.X < u2.Key.FixedPosition.X + u2.Key.Size.X / 1.2)
                {
                    {
                        if (!icon.Enabled)
                        {
                            icon.Enabled = u2.Key.Enabled;
                            u2.Key.DictionaryPosition = icon.DictionaryPosition;
                            icon.DictionaryPosition = u2.Key.DictionaryPosition;
                            this.abilityToggler.SValuesDictionary[this.MovingIcon.Name] = true;
                            this.abilityToggler.Dictionary[this.MovingIcon.Name] = true;
                            Utils.Sleep(500, u2.Key.Name + "DragAndDropChangePriority");
                            break;
                        }

                        var menuItemDict = menuItem.GetValue<PriorityChanger>().Dictionary;
                        menuItemDict[u2.Key.Name] = icon.Priority;
                        menuItemDict[icon.Name] = u2.Key.Priority;
                        var menuItemDict2 = menuItem.GetValue<PriorityChanger>().SValuesDictionary;
                        menuItemDict2[u2.Key.Name] = icon.Priority;
                        menuItemDict2[icon.Name] = u2.Key.Priority;
                        this.PriorityIconsDictionary[u2.Key] = icon.Priority;
                        this.PriorityIconsDictionary[icon] = u2.Key.Priority;
                        u2.Key.DictionaryPosition = icon.Priority;
                        icon.DictionaryPosition = u2.Key.Priority;
                        Utils.Sleep(500, u2.Key.Name + "DragAndDropChangePriority");
                        break;
                    }
                }
            }
        }

        /// <summary>
        ///     The draw shadow.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        /// <param name="size">
        ///     The size.
        /// </param>
        private void DrawShadow(Vector2 position, Vector2 size)
        {
            Drawing.DrawRect(
                position,
                size,
                new Color(
                    0,
                    0,
                    0,
                    this.MovingIcon != null && this.MovingIcon.Moving
                        ? (int)Math.Min(this.transition.GetValue(), 255)
                        : 150 - (int)Math.Min(this.transition.GetValue(), 255)));
        }

        /// <summary>
        ///     The handle priority icon.
        /// </summary>
        /// <param name="icon">
        ///     The icon.
        /// </param>
        /// <param name="mousePosition">
        ///     The mouse position.
        /// </param>
        /// <param name="priority">
        ///     The priority.
        /// </param>
        /// <param name="move">
        ///     The move.
        /// </param>
        /// <param name="menuItem">
        ///     The menu Item.
        /// </param>
        private void HandlePriorityIcon(
            PriorityIcon icon,
            Vector2 mousePosition,
            uint priority,
            float move,
            MenuItem menuItem)
        {
            var wasHovered = icon.Hovered;
            icon.Hovered = Utils.IsUnderRectangle(
                mousePosition,
                icon.Position.X,
                icon.Position.Y,
                icon.Size.X,
                icon.Size.Y);
            var onHover = !wasHovered && icon.Hovered;
            var onUnHover = wasHovered && !icon.Hovered;

            if (onHover)
            {
                icon.Hover.Start(0, 40);
            }

            if (onUnHover)
            {
                icon.ReturnFromDrag(
                    icon.Position
                    + new Vector2((icon.Size.X - this.IconSize.X) / 2, (icon.Size.Y - this.IconSize.Y) / 2));
                icon.Hover.Start(0, 40);
            }

            var alpha = icon.Hovered ? icon.Hover.GetValue() : 40 - icon.Hover.GetValue();
            var brightness = (float)priority / this.Count * 10;
            icon.Color = icon.Enabled
                             ? System.Drawing.Color.FromArgb(
                                 (int)Math.Max(Math.Min(210 + alpha, 255), 0),
                                 (int)
                                 Math.Max(
                                     Math.Min(
                                         Menu.Root.SelectedTheme.TogglerEnabledColor.R / 10 * 4 + alpha
                                         + brightness * 18,
                                         255),
                                     0),
                                 (int)
                                 Math.Max(
                                     Math.Min(
                                         Menu.Root.SelectedTheme.TogglerEnabledColor.G / 10 * 4 + alpha
                                         + brightness * 12,
                                         255),
                                     0),
                                 (int)
                                 Math.Max(
                                     Math.Min(
                                         Menu.Root.SelectedTheme.TogglerEnabledColor.B / 10 + alpha + brightness,
                                         255),
                                     0)).ToSharpDxColor()
                             : System.Drawing.Color.FromArgb(
                                 (int)Math.Max(Math.Min(210 + alpha, 255), 0),
                                 (int)
                                 Math.Max(
                                     Math.Min(
                                         Menu.Root.SelectedTheme.TogglerDisabledColor.R / 10 + alpha + brightness * 5,
                                         255),
                                     0),
                                 (int)
                                 Math.Max(
                                     Math.Min(
                                         Menu.Root.SelectedTheme.TogglerDisabledColor.G / 10 + alpha + brightness * 5,
                                         255),
                                     0),
                                 (int)
                                 Math.Max(
                                     Math.Min(
                                         Menu.Root.SelectedTheme.TogglerDisabledColor.B / 10 + alpha + brightness * 5,
                                         255),
                                     0)).ToSharpDxColor();

            if (icon.Moving)
            {
                icon.Position = new Vector2(
                    mousePosition.X - this.MousePositionDifference.X,
                    this.BasePosition.Y - icon.Size.Y / 20);
                icon.IconPosition = icon.Position
                                    + new Vector2(
                                        icon.Size.X / 2 - icon.IconSize.X / 2,
                                        icon.Size.Y / 2 - icon.IconSize.Y / 2);
                this.CheckMovingIconPosition(icon, menuItem, mousePosition);
                return;
            }

            if (!icon.Hovered || icon.FixedPosition != this.BasePosition - new Vector2(move, 0)
                || this.MovingIcon != null && (!this.MovingIcon.Moving || !this.MovingIcon.Equals(icon)))
            {
                icon.Position = this.BasePosition - new Vector2(move, 0);
            }

            icon.Height = this.Height;
            icon.IconSize = this.textureIconSize;
            icon.Size = this.iconSize;
            icon.IconPosition = icon.Position
                                + new Vector2(
                                    icon.Size.X / 2 - icon.IconSize.X / 2,
                                    icon.Size.Y / 2 - icon.IconSize.Y / 2);
        }

        #endregion
    }
}