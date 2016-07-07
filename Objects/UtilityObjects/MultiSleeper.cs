// <copyright file="MultiSleeper.cs" company="EnsageSharp">
//    Copyright (c) 2016 EnsageSharp.
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
namespace Ensage.Common.Objects.UtilityObjects
{
    using System.Collections.Generic;

    /// <summary>
    ///     The sleeper.
    /// </summary>
    public class MultiSleeper
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MultiSleeper" /> class.
        /// </summary>
        public MultiSleeper()
        {
            this.LastSleepTickDictionary = new Dictionary<string, float>();
            this.LastSleepTickDictionaryUint = new Dictionary<uint, float>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the last sleep tick dictionary.
        /// </summary>
        public Dictionary<string, float> LastSleepTickDictionary { get; set; }

        /// <summary>
        ///     Gets or sets the last sleep tick dictionary uint.
        /// </summary>
        public Dictionary<uint, float> LastSleepTickDictionaryUint { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The reset.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        public void Reset(string id)
        {
            if (!this.LastSleepTickDictionary.ContainsKey(id))
            {
                return;
            }

            this.LastSleepTickDictionary[id] = 0;
        }

        /// <summary>
        ///     The reset.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        public void Reset(uint id)
        {
            if (!this.LastSleepTickDictionaryUint.ContainsKey(id))
            {
                return;
            }

            this.LastSleepTickDictionaryUint[id] = 0;
        }

        /// <summary>
        ///     The sleep.
        /// </summary>
        /// <param name="duration">
        ///     The duration.
        /// </param>
        /// <param name="id">
        ///     The id.
        /// </param>
        public void Sleep(float duration, string id)
        {
            if (!this.LastSleepTickDictionary.ContainsKey(id))
            {
                this.LastSleepTickDictionary.Add(id, 0);
            }

            this.LastSleepTickDictionary[id] = Utils.TickCount + duration;
        }

        /// <summary>
        ///     The sleep.
        /// </summary>
        /// <param name="duration">
        ///     The duration.
        /// </param>
        /// <param name="id">
        ///     The id.
        /// </param>
        public void Sleep(float duration, uint id)
        {
            if (!this.LastSleepTickDictionaryUint.ContainsKey(id))
            {
                this.LastSleepTickDictionaryUint.Add(id, 0);
            }

            this.LastSleepTickDictionaryUint[id] = Utils.TickCount + duration;
        }

        /// <summary>
        ///     The sleeping.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool Sleeping(string id)
        {
            float lastSleepTick;
            return this.LastSleepTickDictionary.TryGetValue(id, out lastSleepTick) && Utils.TickCount < lastSleepTick;
        }

        /// <summary>
        ///     The sleeping.
        /// </summary>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool Sleeping(uint id)
        {
            float lastSleepTick;
            return this.LastSleepTickDictionaryUint.TryGetValue(id, out lastSleepTick)
                   && Utils.TickCount < lastSleepTick;
        }

        #endregion
    }
}