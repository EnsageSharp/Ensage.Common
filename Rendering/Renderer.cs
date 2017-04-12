namespace Ensage.Common.Rendering
{
    using System;

    using Ensage.Common.Rendering.DX11;
    using Ensage.Common.Rendering.DX9;
    using Ensage.Common.Rendering.Interface;
    using Ensage.Common.Rendering.Overlay;

    using SharpDX;

    /// <summary>
    /// 
    /// </summary>
    public class Renderer
    {
        #region Events
        /// <summary>
        /// 
        /// </summary>
        public static event EventHandlerNoSender OnDraw
        {
            add
            {
                activeRenderer.OnDraw += value;
            }
            remove
            {
                activeRenderer.OnDraw += value;
            }
        }
        #endregion

        #region Private Members

        private static bool isUsingOverlay;

        private static readonly IRenderer GraphicsRenderer;
        private static readonly IRenderer RendererOverlay;
        private static IRenderer activeRenderer;
        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public static bool IsUsingOverlay
        {
            get
            {
                return isUsingOverlay;
            }
            set
            {
                isUsingOverlay = value;
                activeRenderer = isUsingOverlay ? RendererOverlay : GraphicsRenderer;
            }
        }
        #endregion


        #region CTor
        static Renderer()
        {
            RendererOverlay = new RendererOverlay();
            switch (Drawing.RenderMode)
            {
                case RenderMode.Vulkan:
                    Console.WriteLine("Renderer doesn't support Vulkan yet");
                    break;
                case RenderMode.OpenGL:
                    Console.WriteLine("Renderer doesn't support OpenGL yet");
                    break;
                case RenderMode.Dx11:
                    GraphicsRenderer = new RendererDx11();
                    break;
                case RenderMode.Dx9:
                    GraphicsRenderer = new RendererDx9();
                    break;
                default:
                    Console.WriteLine("Renderer received an unknown render mode: {0}", Drawing.RenderMode);
                    break;
            }
            activeRenderer = IsUsingOverlay ? RendererOverlay : GraphicsRenderer;
        }
        #endregion

        #region Drawing Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        public static void DrawText2D(string text, Vector2 position, Color color)
        {
            activeRenderer.DrawText2D(text, position, color);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="color"></param>
        public static void DrawLine2D(Vector2 start, Vector2 end, Color color)
        {
            activeRenderer.DrawLine2D(start, end, color);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="color"></param>
        /// <param name="outline"></param>
        public static void DrawRect2D(Rectangle rect, Color color, bool outline = false)
        {
            activeRenderer.DrawRect2D(rect, color, outline);
        }
        #endregion

    }
}
