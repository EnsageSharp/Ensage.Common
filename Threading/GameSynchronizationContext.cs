// <copyright file="GameSynchronizationContext.cs" company="EnsageSharp">
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
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;

    public class GameSynchronizationContext : SynchronizationContext
    {
        #region Static Fields

        private static readonly object SyncRoot = new object();

        private static GameSynchronizationContext instance;

        #endregion

        #region Fields

        private readonly ConcurrentQueue<KeyValuePair<SendOrPostCallback, object>> queue =
            new ConcurrentQueue<KeyValuePair<SendOrPostCallback, object>>();

        #endregion

        #region Public Properties

        public static GameSynchronizationContext Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new GameSynchronizationContext();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion

        #region Public Methods and Operators

        public override SynchronizationContext CreateCopy()
        {
            return new GameSynchronizationContext();
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            this.queue.Enqueue(new KeyValuePair<SendOrPostCallback, object>(d, state));
        }

        #endregion

        #region Methods

        internal void RunOnCurrentThread()
        {
            KeyValuePair<SendOrPostCallback, object> workItem;

            while (!this.queue.IsEmpty && this.queue.TryDequeue(out workItem))
            {
                workItem.Key(workItem.Value);
            }
        }

        #endregion
    }
}