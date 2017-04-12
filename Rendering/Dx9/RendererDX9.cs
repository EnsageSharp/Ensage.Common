namespace Ensage.Common.Rendering.DX9
{
    using System;

    using Ensage.Common.Rendering.Interface;

    using SharpDX;
    using SharpDX.Direct3D9;

    internal class RendererDx9 : IRenderer
    {
        #region Events
        event EventHandlerNoSender IRenderer.OnDraw
        {
            add
            {
                Drawing.OnEndScene += new DrawingEndScene(value);
            }
            remove
            {
                Drawing.OnEndScene -= new DrawingEndScene(value);
            }
        }
        #endregion

        #region Private Members
        private readonly Device drawingDevice;
        private readonly Font font;
        private readonly Sprite sprite;
        private readonly Line line;
        #endregion

        public RendererDx9()
        {
            this.drawingDevice = Drawing.Direct3DDevice9;

            Drawing.OnPreReset += this.Drawing_OnPreReset;
            Drawing.OnPostReset += this.Drawing_OnPostReset;

            this.line = new Line(this.drawingDevice);
            this.sprite = new Sprite(this.drawingDevice);
            this.font = new Font(this.drawingDevice,
               new FontDescription
               {
                   FaceName = "Calibri",
                   Height = 13,
                   OutputPrecision = FontPrecision.Default,
                   Quality = FontQuality.Default
               });

        }

        private void Drawing_OnPostReset(EventArgs args)
        {
            this.line.OnResetDevice();
            this.font.OnResetDevice();
            this.sprite.OnResetDevice();

        }

        private void Drawing_OnPreReset(EventArgs args)
        {
            this.font.OnLostDevice();
            this.sprite.OnLostDevice();
            this.line.OnLostDevice();
        }

        #region Drawing Functions

        public void DrawText2D(string text, Vector2 position, Color color)
        {
            this.font.DrawText(null, text, (int)position.X, (int)position.Y, color);
        }

        public void DrawRect2D(Rectangle rect, Color color, bool outline = false)
        {
            this.line.Width = outline ? 1.0f : rect.Height;
            this.line.Begin();
            if (outline)
            {
                this.line.Draw(new[] { new Vector2(rect.X, rect.Y), new Vector2(rect.X + rect.Width, rect.Y) }, color);
                this.line.Draw(new[] { new Vector2(rect.X + rect.Width, rect.Y), new Vector2(rect.X + rect.Width, rect.Y + rect.Height) }, color);
                this.line.Draw(new[] { new Vector2(rect.X + rect.Width, rect.Y + rect.Height), new Vector2(rect.X, rect.Y + rect.Height) }, color);
                this.line.Draw(new[] { new Vector2(rect.X, rect.Y + rect.Height), new Vector2(rect.X, rect.Y) }, color);
            }
            else
                this.line.Draw(new[] { new Vector2(rect.X, rect.Y + rect.Height / 2), new Vector2(rect.X + rect.Width, rect.Y + rect.Height / 2) }, color);
            this.line.End();
        }

        public void DrawLine2D(Vector2 start, Vector2 end, Color color, float width = 1.0f)
        {
            this.line.Width = width;
            this.line.Begin();
            this.line.Draw(new[] { start, end }, color);
            this.line.End();
        }

        #endregion


    }
}
