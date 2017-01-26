// <copyright file="EdgeTrigger.cs" company="EnsageSharp">
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
namespace Ensage.Common.Threading
{
    using System;

    public class EdgeTrigger
    {
        #region Fields

        private bool value;

        #endregion

        #region Public Events

        public event EventHandler Fallen;

        public event EventHandler Falling;

        public event EventHandler Risen;

        public event EventHandler Rising;

        #endregion

        #region Public Properties

        public bool Value
        {
            get
            {
                return this.value;
            }

            set
            {
                if (this.value == value)
                {
                    return;
                }

                if (value == true)
                {
                    this.Rising?.Invoke(this, EventArgs.Empty);
                    this.value = true;
                    this.Risen?.Invoke(this, EventArgs.Empty);
                }

                if (value == false)
                {
                    this.Falling?.Invoke(this, EventArgs.Empty);
                    this.value = false;
                    this.Fallen?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        #endregion
    }
}