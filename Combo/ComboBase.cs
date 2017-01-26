// <copyright file="ComboBase.cs" company="EnsageSharp">
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
namespace Ensage.Common.Combo
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Ensage.Common.Threading;

    /// <summary>
    ///     Combo base.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "StyleCop.SA1310")]
    public abstract class ComboBase : ICombo, IEquatable<ComboBase>
    {
        #region Constants

        protected const uint WM_KEYUP = 0x0101;

        protected const uint WM_SYSKEYUP = 0x0105;

        #endregion

        #region Fields

        private Key key;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ComboBase" /> class.
        /// </summary>
        /// <param name="key">activation key</param>
        /// <exception cref="InvalidEnumArgumentException">Throws on invalid key</exception>
        protected ComboBase(Key key)
        {
            if (!Enum.IsDefined(typeof(Key), key))
            {
                throw new InvalidEnumArgumentException(nameof(key), (int)key, typeof(Key));
            }

            this.Key = key;
        }

        #endregion

        #region Public Properties

        /// <inheritdoc />
        public bool IsCompleted
        {
            get
            {
                return this.ExecutorTask == null || this.ExecutorTask.IsCompleted;
            }
        }

        /// <inheritdoc />
        public bool IsRunning
        {
            get
            {
                return this.ExecutorTask != null && !this.ExecutorTask.IsCompleted;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets Executor <seealso cref="Task" />
        /// </summary>
        protected Task ExecutorTask { get; set; }

        /// <summary>
        ///     Gets execution key.
        /// </summary>
        protected Key Key
        {
            get
            {
                return this.key;
            }

            set
            {
                this.key = value;
                this.VirtualKey = (ulong)KeyInterop.VirtualKeyFromKey(value);
            }
        }

        /// <summary>
        ///     Gets or sets <seealso cref="Task" /> control <seealso cref="CancellationTokenSource" />
        /// </summary>
        protected CancellationTokenSource TokenSource { get; set; }

        /// <summary>
        ///     Gets virtual execution key.
        /// </summary>
        protected ulong VirtualKey { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <inheritdoc />
        public static bool operator ==(ComboBase left, ComboBase right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc />
        public static bool operator !=(ComboBase left, ComboBase right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc />
        public virtual void Activate()
        {
            GameDispatcher.OnIngameUpdate += this.OnUpdate;
        }

        /// <inheritdoc />
        public virtual void Cancel()
        {
            if (this.TokenSource == null)
            {
                return;
            }

            if (!this.TokenSource.IsCancellationRequested)
            {
                this.TokenSource.Cancel();
            }
        }

        /// <inheritdoc />
        public virtual void Deactivate()
        {
            this.Cancel();

            GameDispatcher.OnIngameUpdate -= this.OnUpdate;
        }

        /// <inheritdoc />
        public bool Equals(ComboBase other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.Key == other.Key;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = obj as ComboBase;
            return other != null && this.Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (int)this.Key;
        }

        #endregion

        #region Methods

        protected virtual bool CanExecute()
        {
            if (Game.IsChatOpen)
            {
                return false;
            }

            if (!Game.IsKeyDown(this.Key))
            {
                return false;
            }

            return true;
        }

        protected abstract Task Execute(CancellationToken token);

        private void BeginExecution()
        {
            Game.OnWndProc += this.OnWndProc;
            this.TokenSource = new CancellationTokenSource();
            this.ExecutorTask = this.Execute(this.TokenSource.Token);
        }

        private void EndExecution()
        {
            this.Cancel();

            Game.OnWndProc -= this.OnWndProc;
            this.ExecutorTask = null;
        }

        private async void OnUpdate(EventArgs args)
        {
            if (!this.CanExecute())
            {
                return;
            }

            if (!this.IsCompleted)
            {
                return;
            }

            try
            {
                this.BeginExecution();
                await this.ExecutorTask;
            }
            catch (OperationCanceledException)
            {
                // canceled
            }
            finally
            {
                this.EndExecution();
            }
        }

        private void OnWndProc(WndEventArgs args)
        {
            if (this.ExecutorTask == null)
            {
                return;
            }

            if ((args.Msg == WM_KEYUP || args.Msg == WM_SYSKEYUP) && args.WParam == this.VirtualKey)
            {
                this.Cancel();
                args.Process = false;
            }
        }

        #endregion
    }
}