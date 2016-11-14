// <copyright file="Combo.cs" company="EnsageSharp">
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
namespace Ensage.Common.Combo
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    ///     Executing a combo with async functions
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "StyleCop.SA1310")]
    public class Combo : IDisposable
    {
        #region Constants

        private const uint WM_KEYUP = 0x0101;

        private const uint WM_SYSKEYUP = 0x0105;

        #endregion

        #region Fields

        private readonly Func<CancellationToken, Task> comboFunction;

        private readonly Key key;

        private readonly ulong virtualKey;

        private Task currentExecution;

        private bool disposed;

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
            this.virtualKey = (ulong)KeyInterop.VirtualKeyFromKey(key);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Returns true if the current execution is completed.
        /// </summary>
        public bool IsCompleted
        {
            get
            {
                return this.currentExecution == null || this.currentExecution.IsCompleted;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the current execution is running. 
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return this.currentExecution != null && !this.currentExecution.IsCompleted;
            }
        }

        #endregion

        #region Public Methods and Operators

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

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Executes your combo until it's either finished or canceled.
        /// </summary>
        /// <returns></returns>
        public async Task Execute()
        {
            if (!Game.IsKeyDown(this.key))
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

            if (((args.Msg == WM_KEYUP) || (args.Msg == WM_SYSKEYUP)) && (args.WParam == this.virtualKey))
            {
                this.Cancel();
            }
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