// <copyright file="Combo.cs" company="EnsageSharp">
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
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Ensage.Common.Threading;

    /// <summary>
    ///     Executing a combo with async functions
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "StyleCop.SA1310")]
    public class Combo : ICombo, IEquatable<Combo>
    {
        #region Constants

        private const uint WM_KEYUP = 0x0101;

        private const uint WM_SYSKEYUP = 0x0105;

        #endregion

        #region Fields

        private readonly Func<CancellationToken, Task> comboFunction;

        private Task currentExecution;

        private bool disposed;

        private Key key;

        private CancellationTokenSource token;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Creates a combo.
        /// </summary>
        /// <param name="comboFunction">This function will be executed while the key is pressed.</param>
        /// <param name="key">While this key is pressed your combofunction will be executed.</param>
        public Combo(Func<CancellationToken, Task> comboFunction, Key key)
        {
            this.comboFunction = comboFunction;
            this.key = key;
            this.VirtualKey = (ulong)KeyInterop.VirtualKeyFromKey(key);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Returns true if the current execution is completed.
        /// </summary>
        public bool IsCompleted => this.currentExecution == null || this.currentExecution.IsCompleted;

        /// <summary>
        ///     Gets a value indicating whether the current execution is running.
        /// </summary>
        public bool IsRunning => this.currentExecution != null && !this.currentExecution.IsCompleted;

        /// <summary>
        ///     Gets execution key.
        /// </summary>
        public Key Key
        {
            get
            {
                return this.key;
            }

            set
            {
                this.key = value;
                this.VirtualKey = (ulong)KeyInterop.VirtualKeyFromKey(this.key);
            }
        }

        /// <summary>
        ///     Gets virtual execution key.
        /// </summary>
        public ulong VirtualKey { get; private set; }

        #endregion

        #region Public Methods and Operators

        public void Activate()
        {
            GameDispatcher.OnIngameUpdate += this.OnUpdate;
        }

        /// <summary>
        ///     Cancels the execution of the current combo.
        /// </summary>
        public void Cancel()
        {
            if (this.token == null)
            {
                return;
            }

            if (!this.token.IsCancellationRequested)
            {
                this.token.Cancel();
            }
        }

        public void Deactivate()
        {
            this.Cancel();

            GameDispatcher.OnIngameUpdate -= this.OnUpdate;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        public bool Equals(Combo other)
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

            var other = obj as Combo;
            return other != null && this.Equals(other);
        }

        /// <summary>
        ///     Executes your combo until it's either finished or canceled.
        /// </summary>
        /// <returns></returns>
        public async Task Execute()
        {
            if (!Game.IsKeyDown(this.Key))
            {
                return;
            }

            if (!this.IsCompleted)
            {
                return;
            }

            try
            {
                this.Prepare();
                await this.currentExecution;
            }
            catch (OperationCanceledException)
            {
                // canceled
            }
            finally
            {
                this.Finish();
            }
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (int)this.Key;
        }

        #endregion

        #region Methods

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.Finish();
            }

            this.disposed = true;
        }

        private void Finish()
        {
            this.Cancel();

            Game.OnWndProc -= this.Game_OnWndProc;
            this.currentExecution = null;
        }

        private void Game_OnWndProc(WndEventArgs args)
        {
            if (this.currentExecution == null)
            {
                return;
            }

            if ((args.Msg == WM_KEYUP || args.Msg == WM_SYSKEYUP) && args.WParam == this.VirtualKey)
            {
                this.Cancel();
            }
        }

        private async void OnUpdate(EventArgs args)
        {
            await this.Execute();
        }

        private void Prepare()
        {
            Game.OnWndProc += this.Game_OnWndProc;
            this.token = new CancellationTokenSource();
            this.currentExecution = this.comboFunction(this.token.Token);
        }

        #endregion
    }
}