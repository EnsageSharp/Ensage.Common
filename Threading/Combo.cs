using System;
using System.Threading.Tasks;

namespace Ensage.Common.Threading
{
    using System.Threading;
    using System.Windows.Input;

    /// <summary>
    /// Executing a combo with async functions
    /// </summary>
    public class Combo
    {
        // ReSharper disable once InconsistentNaming
        private const uint WM_KEYUP = 0x0101;
        // ReSharper disable once InconsistentNaming
        private const uint WM_SYSKEYUP = 0x0105;

        private readonly Func<CancellationToken, Task> comboFunction;
        private readonly Key key;
        private Task currentExecution;

        private CancellationTokenSource token;

        /// <summary>
        /// Creates a combo.
        /// </summary>
        /// <param name="comboFunction">This function will be executed while the key is pressed.</param>
        /// <param name="key">While this key is pressed your combofunction will be executed.</param>
        public Combo(Func<CancellationToken, Task> comboFunction, Key key)
        {
            this.comboFunction = comboFunction;
            this.key = key;
            Game.OnWndProc += this.Game_OnWndProc;
        }

        private void Game_OnWndProc(WndEventArgs args)
        {
            if (this.currentExecution == null)
                return;

            if (((args.Msg == WM_KEYUP) || (args.Msg == WM_SYSKEYUP)) &&
                ((int)args.WParam == KeyInterop.VirtualKeyFromKey(this.key)))
            {
                this.token?.Cancel();
            }
        }

        /// <summary>
        /// Executes your combo until it's either finished or canceled.
        /// </summary>
        /// <returns></returns>
        public async Task Execute()
        {
            if (!Game.IsKeyDown(this.key))
                return;

            if ((this.currentExecution != null) && !this.currentExecution.IsCompleted)
                return;

            try
            {
                this.token = new CancellationTokenSource();
                this.currentExecution = this.comboFunction(this.token.Token);
                await this.currentExecution;
                this.currentExecution = null;
            }
            catch (OperationCanceledException)
            {
                this.currentExecution = null;
            }
        }
    }
}
