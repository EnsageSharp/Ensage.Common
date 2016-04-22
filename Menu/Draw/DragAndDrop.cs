namespace Ensage.Common.Menu.Draw
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.Common.Extensions.SharpDX;
    using Ensage.Common.Menu.Transitions;
    using Ensage.Common.Objects.UtilityObjects;

    using SharpDX;

    using Color = System.Drawing.Color;

    /// <summary>
    ///     The drag and drop.
    /// </summary>
    public class DragAndDrop
    {
        #region Fields

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
        ///     The icon size.
        /// </summary>
        private Vector2 iconSize;

        /// <summary>
        ///     The texture icon size.
        /// </summary>
        private Vector2 textureIconSize;

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
                this.PriorityIconsDictionary.Add(new PriorityIcon(s, height), count);
                count++;
            }

            this.iconSizeSleeper = new Sleeper();
            this.transition = new QuadEaseInOut(0.35);
            this.transition.Start(0, 150);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the base position.
        /// </summary>
        public Vector2 BasePosition { get; set; }

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
        public void Add(string name, uint customPriority = 0)
        {
            this.PriorityIconsDictionary.Add(
                new PriorityIcon(name, this.Height), 
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

            var dictionary = new Dictionary<PriorityIcon, uint>(this.PriorityIconsDictionary);
            foreach (var u in dictionary.OrderBy(x => menuItem.GetValue<PriorityChanger>().GetPriority(x.Key.Name)))
            {
                u.Key.Priority = menuItem.GetValue<PriorityChanger>().GetPriority(u.Key.Name);
                this.PriorityIconsDictionary[u.Key] = u.Key.Priority;

                var wasHovered = u.Key.Hovered;
                u.Key.Hovered = Utils.IsUnderRectangle(
                    mousePosition, 
                    u.Key.Position.X, 
                    u.Key.Position.Y, 
                    u.Key.Size.X, 
                    u.Key.Size.Y);
                var onHover = !wasHovered && u.Key.Hovered;
                var onUnHover = wasHovered && !u.Key.Hovered;

                if (onHover)
                {
                    u.Key.Hover.Start(0, 40);
                }

                if (onUnHover)
                {
                    u.Key.ReturnFromDrag(
                        u.Key.Position
                        + new Vector2((u.Key.Size.X - this.IconSize.X) / 2, (u.Key.Size.Y - this.IconSize.Y) / 2));
                    u.Key.Hover.Start(0, 40);
                }

                if (!u.Key.Moving)
                {
                    if (!u.Key.Hovered
                        || (this.MovingIcon != null && (!this.MovingIcon.Moving || !this.MovingIcon.Equals(u.Key))))
                    {
                        u.Key.Position = this.BasePosition - new Vector2(move, 0);
                    }

                    u.Key.Height = this.Height;
                    u.Key.IconSize = this.textureIconSize;
                    u.Key.Size = this.iconSize;
                }

                if (u.Key.Moving)
                {
                    u.Key.Position = new Vector2(
                        mousePosition.X - this.MousePositionDifference.X, 
                        this.BasePosition.Y - (u.Key.Size.Y / 20));
                    u.Key.IconPosition = u.Key.Position
                                         + new Vector2(
                                               (u.Key.Size.X / 2) - (u.Key.IconSize.X / 2), 
                                               (u.Key.Size.Y / 2) - (u.Key.IconSize.Y / 2));
                    u.Key.Hovered = Utils.IsUnderRectangle(
                        mousePosition, 
                        u.Key.Position.X, 
                        u.Key.Position.Y, 
                        u.Key.Size.X, 
                        u.Key.Size.Y);
                    var a = u.Key.Hovered ? 35 : 0;
                    u.Key.Color =
                        Color.FromArgb(
                            (int)Math.Max(Math.Min(210 + a, 255), 0),
                            (int)Math.Max(Math.Min(18 + a + (u.Value * 18), 255), 0),
                            (int)Math.Max(Math.Min(12 + a + (u.Value * 12), 255), 0),
                            (int)Math.Max(Math.Min(1 + a + u.Value, 255), 0)).ToSharpDxColor();
                    var dict = new Dictionary<PriorityIcon, uint>(this.PriorityIconsDictionary);
                    foreach (var u2 in
                        dict.Where(
                            x =>
                            Utils.SleepCheck(x.Key.Name + "DragAndDropChangePriority") && x.Key.Name != u.Key.Name
                            && (x.Key.Priority == u.Key.Priority - 1 || x.Key.Priority == u.Key.Priority + 1)))
                    {
                        if (u2.Key.Priority == u.Key.Priority - 1 && mousePosition.X > u2.Key.FixedPosition.X)
                        {
                            var menuItemDict = menuItem.GetValue<PriorityChanger>().Dictionary;
                            menuItemDict[u2.Key.Name] = u.Key.Priority;
                            menuItemDict[u.Key.Name] = u2.Key.Priority;
                            var menuItemDict2 = menuItem.GetValue<PriorityChanger>().SValuesDictionary;
                            menuItemDict2[u2.Key.Name] = u.Key.Priority;
                            menuItemDict2[u.Key.Name] = u2.Key.Priority;
                            this.PriorityIconsDictionary[u2.Key] = u.Key.Priority;
                            this.PriorityIconsDictionary[u.Key] = u2.Key.Priority;
                            Utils.Sleep(200, u2.Key.Name + "DragAndDropChangePriority");
                            break;
                        }

                        if (u2.Key.Priority == u.Key.Priority + 1
                            && mousePosition.X < u2.Key.FixedPosition.X + (u2.Key.Size.X / 1.2))
                        {
                            var menuItemDict = menuItem.GetValue<PriorityChanger>().Dictionary;
                            menuItemDict[u2.Key.Name] = u.Key.Priority;
                            menuItemDict[u.Key.Name] = u2.Key.Priority;
                            var menuItemDict2 = menuItem.GetValue<PriorityChanger>().SValuesDictionary;
                            menuItemDict2[u2.Key.Name] = u.Key.Priority;
                            menuItemDict2[u.Key.Name] = u2.Key.Priority;
                            this.PriorityIconsDictionary[u2.Key] = u.Key.Priority;
                            this.PriorityIconsDictionary[u.Key] = u2.Key.Priority;
                            Utils.Sleep(200, u2.Key.Name + "DragAndDropChangePriority");
                            break;
                        }
                    }

                    move += this.IconSize.X;
                    move += -1;
                    continue;
                }

                u.Key.IconPosition = u.Key.Position
                                     + new Vector2(
                                           (u.Key.Size.X / 2) - (u.Key.IconSize.X / 2), 
                                           (u.Key.Size.Y / 2) - (u.Key.IconSize.Y / 2));

                // if (u.Key.Hovered && this.MovingIcon == null)
                // {
                // u.Key.Size = u.Key.Size * new Vector2((float)1.1);
                // u.Key.IconSize = (u.Key.IconSize * new Vector2((float)1.1)) - new Vector2(3, 3);
                // u.Key.Height = u.Key.Height * (float)1.1;
                // if (onHover)
                // {
                // u.Key.ReturnFromDrag(
                // new Vector2(
                // u.Key.Position.X - ((u.Key.Size.X - this.IconSize.X)),
                // this.BasePosition.Y - (u.Key.Size.Y / 30)));
                // }

                // u.Key.IconPosition = u.Key.Position
                // + new Vector2(
                // (u.Key.Size.X / 2) - (u.Key.IconSize.X / 2),
                // (u.Key.Size.Y / 2) - (u.Key.IconSize.Y / 2));
                // }
                var alpha = u.Key.Hovered ? u.Key.Hover.GetValue() : 40 - u.Key.Hover.GetValue();
                u.Key.Color =
                    Color.FromArgb(
                        (int)Math.Max(Math.Min(210 + alpha, 255), 0),
                        (int)Math.Max(Math.Min(18 + alpha + (u.Value * 18), 255), 0),
                        (int)Math.Max(Math.Min(12 + alpha + (u.Value * 12), 255), 0),
                        (int)Math.Max(Math.Min(1 + alpha + u.Value, 255), 0)).ToSharpDxColor();
                u.Key.Draw();
                u.Key.DrawPriorityNumber();
                move += u.Key.Size.X;
                move += -1;
            }

            Drawing.DrawRect(
                this.BasePosition - new Vector2(move - this.IconSize.X, 0), 
                new Vector2(move, this.IconSize.Y), 
                new SharpDX.Color(
                    0, 
                    0, 
                    0, 
                    this.MovingIcon != null && this.MovingIcon.Moving
                        ? (int)Math.Min(this.transition.GetValue(), 255)
                        : 150 - (int)Math.Min(this.transition.GetValue(), 255)));
            if (this.MovingIcon != null)
            {
                this.MovingIcon.Draw();
                this.MovingIcon.DrawPriorityNumber();
                if (!this.MovingIcon.Moving && this.MovingIcon.FixedPosition == this.MovingIcon.Position)
                {
                    this.MovingIcon = null;
                }
            }
        }

        /// <summary>
        ///     The left button down.
        /// </summary>
        /// <param name="mousePosition">
        ///     The mouse Position.
        /// </param>
        public void LeftButtonDown(Vector2 mousePosition)
        {
            foreach (var u in this.PriorityIconsDictionary.Where(u => u.Key.Hovered))
            {
                this.transition.Start(0, 150);
                u.Key.Moving = true;
                this.MovingIcon = u.Key;

                this.MovingIcon.Size = this.MovingIcon.Size * new Vector2((float)1.22);
                this.MovingIcon.IconSize = (this.MovingIcon.IconSize * new Vector2((float)1.22)) - new Vector2(3, 3);
                this.MovingIcon.Height = this.MovingIcon.Height * (float)1.22;
                this.MovingIcon.Position =
                    new Vector2(
                        this.MovingIcon.Position.X - ((this.MovingIcon.Size.X - this.IconSize.X) / 2), 
                        this.BasePosition.Y - (this.MovingIcon.Size.Y / 20));
                this.MovingIcon.IconPosition = this.MovingIcon.Position
                                               + new Vector2(
                                                     (this.MovingIcon.Size.X / 2) - (this.MovingIcon.IconSize.X / 2), 
                                                     (this.MovingIcon.Size.Y / 2) - (this.MovingIcon.IconSize.Y / 2));
                this.MousePositionDifference = mousePosition - this.MovingIcon.Position;
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
        public void LeftButtonUp(Vector2 mousePosition, MenuItem menuItem)
        {
            if (this.MovingIcon == null)
            {
                return;
            }

            this.MovingIcon.ReturnFromDrag(
                this.MovingIcon.Position
                + new Vector2(
                      (this.MovingIcon.Size.X - this.IconSize.X) / 2, 
                      (this.MovingIcon.Size.Y - this.IconSize.Y) / 2));
            this.transition.Start(0, 150);
            this.MovingIcon.Moving = false;
            menuItem.SetValue(
                new PriorityChanger(
                    this.itemList.OrderBy(x => menuItem.GetValue<PriorityChanger>().Dictionary[x]).ToList(), 
                    menuItem.GetValue<PriorityChanger>().Name));
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

            foreach (var s in newList.Where(x => !this.itemList.Contains(x)))
            {
                Console.WriteLine(s);
                this.PriorityIconsDictionary.Add(new PriorityIcon(s, this.Height), count);
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
            foreach (var u in new Dictionary<PriorityIcon, uint>(this.PriorityIconsDictionary).OrderBy(x => x.Value))
            {
                this.PriorityIconsDictionary[u.Key] = count;
                count++;
            }
        }

        #endregion
    }
}