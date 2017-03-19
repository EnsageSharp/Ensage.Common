namespace Ensage.Common.Rendering.Overlay
{
    using System;

    using Ensage.Common.Rendering.Interface;

    using SharpDX;

    internal class RendererOverlay : IRenderer
    {
        public event EventHandlerNoSender OnDraw;

        public void DrawText2D(string text, Vector2 position, Color color)
        {
            throw new NotImplementedException();
        }

        public void DrawRect2D(Rectangle rect, Color color, bool outline = false)
        {
            throw new NotImplementedException();
        }

        public void DrawLine2D(Vector2 start, Vector2 end, Color color, float width = 1.0f)
        {
            throw new NotImplementedException();
        }
    }
}
