using System;

namespace Ensage.Common.Rendering.DX11
{
    using Interface;

    using SharpDX;
    using SharpDX.Direct2D1;
    using SharpDX.Direct3D11;
    using SharpDX.DXGI;

    using Resource = SharpDX.Direct3D11.Resource;

    /// <summary>
    /// 
    /// </summary>
    internal class RendererDx11 : IRenderer
    {
        #region Events
        event EventHandlerNoSender IRenderer.OnDraw
        {
            add
            {
                //Drawing.OnEndScene += new DrawingEndScene(value);
            }
            remove
            {
                //Drawing.OnEndScene -= new DrawingEndScene(value);
            }
        }
        #endregion

        #region Private Members
        private readonly SharpDX.Direct3D11.Device device;
        private readonly SharpDX.Direct3D11.DeviceContext context;
        private readonly SwapChain swapChain;

        private readonly RenderTarget d2dRenderTarget;

        #endregion

        public RendererDx11()
        {
            var d2dFactory = new SharpDX.Direct2D1.Factory();
            Texture2D backBuffer = Resource.FromSwapChain<Texture2D>(swapChain, 0);
            Surface surface = backBuffer.QueryInterface<Surface>();
            this.d2dRenderTarget = new RenderTarget(d2dFactory, surface, new RenderTargetProperties(new PixelFormat(Format.Unknown, AlphaMode.Premultiplied)));
        }

        public void DrawText2D(string text, Vector2 position, Color color)
        {
            throw new NotImplementedException();
        }

        public void DrawRect2D(Rectangle rect, Color color, bool outline = false)
        {
            var solidColorBrush = new SolidColorBrush(this.d2dRenderTarget, color);
            if(outline)
                this.d2dRenderTarget.DrawRectangle(rect, solidColorBrush);
            else
                this.d2dRenderTarget.FillRectangle(rect,solidColorBrush);
            
        }

        public void DrawLine2D(Vector2 start, Vector2 end, Color color, float width = 1.0f)
        {
            var solidColorBrush = new SolidColorBrush(this.d2dRenderTarget, color);
            this.d2dRenderTarget.DrawLine(start, end, solidColorBrush, width);
        }
    }
}
