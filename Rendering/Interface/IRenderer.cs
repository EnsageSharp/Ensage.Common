namespace Ensage.Common.Rendering.Interface
{
    using System;

    using SharpDX;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    public delegate void EventHandlerNoSender(EventArgs args);

    internal interface IRenderer
    {
        #region Events
        event EventHandlerNoSender OnDraw;
        #endregion

        #region Drawing Functions

        void DrawText2D(string text, Vector2 position, Color color);
        void DrawRect2D(Rectangle rect, Color color, bool outline = false);
        void DrawLine2D(Vector2 start, Vector2 end, Color color, float width = 1.0f);

        #endregion
    }
}
