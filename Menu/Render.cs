#region LICENSE

/*
 Copyright 2014 - 2014 LeagueSharp
 Render.cs is part of LeagueSharp.Common.
 
 LeagueSharp.Common is free software: you can redistribute it and/or modify
 it under the terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 (at your option) any later version.
 
 LeagueSharp.Common is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 GNU General Public License for more details.
 
 You should have received a copy of the GNU General Public License
 along with LeagueSharp.Common. If not, see <http://www.gnu.org/licenses/>.
*/

#endregion

#region

#endregion

namespace Ensage.Common.Menu
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Threading;

    using Ensage.Common.Extensions;

    using SharpDX;
    using SharpDX.Direct3D9;

    using Color = System.Drawing.Color;
    using Font = SharpDX.Direct3D9.Font;
    using Rectangle = SharpDX.Rectangle;

    /// <summary>
    ///     The render class allows you to draw stuff using SharpDX easier.
    /// </summary>
    public static class Render
    {
        #region Static Fields

        private static readonly List<RenderObject> RenderObjects = new List<RenderObject>();

        private static readonly object RenderObjectsLock = new object();

        private static bool _cancelThread;

        private static List<RenderObject> _renderVisibleObjects = new List<RenderObject>();

        #endregion

        #region Constructors and Destructors

        static Render()
        {
            //Drawing.OnEndScene += Drawing_OnEndScene;
            //Drawing.OnPreReset += DrawingOnOnPreReset;
            //Drawing.OnPostReset += DrawingOnOnPostReset;
            //Drawing.OnDraw += Drawing_OnDraw;
            //AppDomain.CurrentDomain.DomainUnload += CurrentDomainOnDomainUnload;
            //AppDomain.CurrentDomain.ProcessExit += CurrentDomainOnDomainUnload;
            //var thread = new Thread(PrepareObjects);
            //thread.SetApartmentState(ApartmentState.STA);
            //thread.Start();
        }

        #endregion

        #region Public Properties

        public static Device Device
        {
            get
            {
                return Drawing.Direct3DDevice9;
            }
        }

        #endregion

        #region Public Methods and Operators

        public static RenderObject Add(this RenderObject renderObject, float layer = float.MaxValue)
        {
            renderObject.Layer = !layer.Equals(float.MaxValue) ? layer : renderObject.Layer;
            lock (RenderObjectsLock)
            {
                RenderObjects.Add(renderObject);
            }
            return renderObject;
        }

        public static bool OnScreen(Vector2 point)
        {
            return point.X > 0 && point.Y > 0 && point.X < Drawing.Width && point.Y < Drawing.Height;
        }

        public static void Remove(this RenderObject renderObject)
        {
            lock (RenderObjectsLock)
            {
                RenderObjects.Remove(renderObject);
            }
        }

        #endregion

        #region Methods

        private static void CurrentDomainOnDomainUnload(object sender, EventArgs eventArgs)
        {
            _cancelThread = true;
            foreach (var renderObject in RenderObjects)
            {
                renderObject.Dispose();
            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Device == null || Device.IsDisposed)
            {
                return;
            }

            foreach (var renderObject in _renderVisibleObjects)
            {
                renderObject.OnDraw();
            }
        }

        private static void Drawing_OnEndScene(EventArgs args)
        {
            if (Device == null || Device.IsDisposed)
            {
                return;
            }

            Device.SetRenderState(RenderState.AlphaBlendEnable, true);

            foreach (var renderObject in _renderVisibleObjects)
            {
                renderObject.OnEndScene();
            }
        }

        private static void DrawingOnOnPostReset(EventArgs args)
        {
            foreach (var renderObject in RenderObjects)
            {
                renderObject.OnPostReset();
            }
        }

        private static void DrawingOnOnPreReset(EventArgs args)
        {
            foreach (var renderObject in RenderObjects)
            {
                renderObject.OnPreReset();
            }
        }

        private static void PrepareObjects()
        {
            while (!_cancelThread)
            {
                try
                {
                    Thread.Sleep(1);
                    lock (RenderObjectsLock)
                    {
                        _renderVisibleObjects =
                            RenderObjects.Where(obj => obj.Visible && obj.HasValidLayer())
                                .OrderBy(obj => obj.Layer)
                                .ToList();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(@"Cannot prepare RenderObjects for drawing. Ex:" + e);
                }
            }
        }

        #endregion

        public class Circle : RenderObject
        {
            #region Static Fields

            private static Effect _effect;

            private static bool _initialized;

            private static Vector3 _offset = new Vector3(0, 0, 0);

            private static EffectHandle _technique;

            private static VertexDeclaration _vertexDeclaration;

            private static VertexElement[] _vertexElements;

            private static VertexBuffer _vertices;

            #endregion

            #region Constructors and Destructors

            public Circle(Unit unit, float radius, Color color, int width = 1, bool zDeep = false)
            {
                this.Color = color;
                this.Unit = unit;
                this.Radius = radius;
                this.Width = width;
                this.ZDeep = zDeep;
            }

            public Circle(Unit unit, Vector3 offset, float radius, Color color, int width = 1, bool zDeep = false)
            {
                this.Color = color;
                this.Unit = unit;
                this.Radius = radius;
                this.Width = width;
                this.ZDeep = zDeep;
                this.Offset = offset;
            }

            public Circle(
                Vector3 position,
                Vector3 offset,
                float radius,
                Color color,
                int width = 1,
                bool zDeep = false)
            {
                this.Color = color;
                this.Position = position;
                this.Radius = radius;
                this.Width = width;
                this.ZDeep = zDeep;
                this.Offset = offset;
            }

            public Circle(Vector3 position, float radius, Color color, int width = 1, bool zDeep = false)
            {
                this.Color = color;
                this.Position = position;
                this.Radius = radius;
                this.Width = width;
                this.ZDeep = zDeep;
            }

            #endregion

            #region Public Properties

            public Color Color { get; set; }

            public Vector3 Offset
            {
                get
                {
                    return _offset;
                }
                set
                {
                    _offset = value;
                }
            }

            public Vector3 Position { get; set; }

            public float Radius { get; set; }

            public Unit Unit { get; set; }

            public int Width { get; set; }

            public bool ZDeep { get; set; }

            #endregion

            #region Public Methods and Operators

            public static void CreateVertexes()
            {
                const float x = 6000f;
                _vertices = new VertexBuffer(
                    Device,
                    Utilities.SizeOf<Vector4>() * 2 * 6,
                    Usage.WriteOnly,
                    VertexFormat.None,
                    Pool.Managed);

                _vertices.Lock(0, 0, LockFlags.None).WriteRange(
                    new[]
                        {
                            //T1
                            new Vector4(-x, 0f, -x, 1.0f), new Vector4(), new Vector4(-x, 0f, x, 1.0f), new Vector4(),
                            new Vector4(x, 0f, -x, 1.0f), new Vector4(),

                            //T2
                            new Vector4(-x, 0f, x, 1.0f), new Vector4(), new Vector4(x, 0f, x, 1.0f), new Vector4(),
                            new Vector4(x, 0f, -x, 1.0f), new Vector4()
                        });
                _vertices.Unlock();

                _vertexElements = new[]
                                      {
                                          new VertexElement(
                                              0,
                                              0,
                                              DeclarationType.Float4,
                                              DeclarationMethod.Default,
                                              DeclarationUsage.Position,
                                              0),
                                          new VertexElement(
                                              0,
                                              16,
                                              DeclarationType.Float4,
                                              DeclarationMethod.Default,
                                              DeclarationUsage.Color,
                                              0),
                                          VertexElement.VertexDeclarationEnd
                                      };

                _vertexDeclaration = new VertexDeclaration(Device, _vertexElements);

                #region Effect

                try
                {
                    /*   
                    _effect = Effect.FromString(Device, @"
                    struct VS_S
                     {
                         float4 Position : POSITION;
                         float4 Color : COLOR0;
                         float4 Position3D : TEXCOORD0;
                     };

                     float4x4 ProjectionMatrix;
                     float4 CircleColor;
                     float Radius;
                     float Border;
                     bool zEnabled;
                     VS_S VS( VS_S input )
                     {
                         VS_S output = (VS_S)0;
	
                         output.Position = mul(input.Position, ProjectionMatrix);
                         output.Color = input.Color;
                         output.Position3D = input.Position;
                         return output;
                     }

                     float4 PS( VS_S input ) : COLOR
                     {
                         VS_S output = (VS_S)0;
                         output = input;

                         float4 v = output.Position3D; 
                         float distance = Radius - sqrt(v.x * v.x + v.z*v.z); // Distance to the circle arc.
    
                         output.Color.x = CircleColor.x;
                         output.Color.y = CircleColor.y;
                         output.Color.z = CircleColor.z;
                            
                         if(distance < Border && distance > -Border)
                         {
                             output.Color.w = (CircleColor.w - CircleColor.w * abs(distance * 1.75 / Border));
                         }
                         else
                         {
                             output.Color.w = 0;
                         }
                            
                         if(Border < 1 && distance >= 0)
                         {
                             output.Color.w = CircleColor.w;
                         }

                         return output.Color;
                     }

                     technique Main {
                         pass P0 {
                             ZEnable = zEnabled;
                             AlphaBlendEnable = TRUE;
                             DestBlend = INVSRCALPHA;
                             SrcBlend = SRCALPHA;
                             VertexShader = compile vs_2_0 VS();
                             PixelShader  = compile ps_2_0 PS();
                         }
                     }", ShaderFlags.None);
                    */
                    var compiledEffect = new byte[]
                                             {
                                                 0x01, 0x09, 0xFF, 0xFE, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x03, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x60, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00,
                                                 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x11, 0x00, 0x00, 0x00,
                                                 0x50, 0x72, 0x6F, 0x6A, 0x65, 0x63, 0x74, 0x69, 0x6F, 0x6E, 0x4D, 0x61,
                                                 0x74, 0x72, 0x69, 0x78, 0x00, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00,
                                                 0x01, 0x00, 0x00, 0x00, 0xA4, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00, 0x43, 0x69, 0x72, 0x63,
                                                 0x6C, 0x65, 0x43, 0x6F, 0x6C, 0x6F, 0x72, 0x00, 0x03, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0xD4, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x52, 0x61, 0x64, 0x69,
                                                 0x75, 0x73, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x07, 0x00, 0x00, 0x00, 0x42, 0x6F, 0x72, 0x64, 0x65, 0x72, 0x00, 0x00,
                                                 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x2C, 0x01, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                                                 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x09, 0x00, 0x00, 0x00,
                                                 0x7A, 0x45, 0x6E, 0x61, 0x62, 0x6C, 0x65, 0x64, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                                                 0x02, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                                                 0x01, 0x00, 0x00, 0x00, 0x06, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00,
                                                 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                                                 0x05, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                                                 0x10, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00,
                                                 0x0F, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00,
                                                 0x50, 0x30, 0x00, 0x00, 0x05, 0x00, 0x00, 0x00, 0x4D, 0x61, 0x69, 0x6E,
                                                 0x00, 0x00, 0x00, 0x00, 0x05, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                                                 0x03, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00,
                                                 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x78, 0x00, 0x00, 0x00, 0x94, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0xB4, 0x00, 0x00, 0x00, 0xD0, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xE0, 0x00, 0x00, 0x00,
                                                 0xFC, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x0C, 0x01, 0x00, 0x00, 0x28, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0xF4, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x01, 0x00, 0x00, 0x00, 0xEC, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x06, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x40, 0x01, 0x00, 0x00, 0x3C, 0x01, 0x00, 0x00, 0x0D, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x60, 0x01, 0x00, 0x00, 0x5C, 0x01, 0x00, 0x00,
                                                 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x01, 0x00, 0x00,
                                                 0x7C, 0x01, 0x00, 0x00, 0x06, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0xA0, 0x01, 0x00, 0x00, 0x9C, 0x01, 0x00, 0x00, 0x92, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0xC0, 0x01, 0x00, 0x00, 0xBC, 0x01, 0x00, 0x00,
                                                 0x93, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xD8, 0x01, 0x00, 0x00,
                                                 0xD4, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF,
                                                 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x4C, 0x04, 0x00, 0x00,
                                                 0x00, 0x02, 0xFF, 0xFF, 0xFE, 0xFF, 0x38, 0x00, 0x43, 0x54, 0x41, 0x42,
                                                 0x1C, 0x00, 0x00, 0x00, 0xAA, 0x00, 0x00, 0x00, 0x00, 0x02, 0xFF, 0xFF,
                                                 0x03, 0x00, 0x00, 0x00, 0x1C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20,
                                                 0xA3, 0x00, 0x00, 0x00, 0x58, 0x00, 0x00, 0x00, 0x02, 0x00, 0x05, 0x00,
                                                 0x01, 0x00, 0x00, 0x00, 0x60, 0x00, 0x00, 0x00, 0x70, 0x00, 0x00, 0x00,
                                                 0x80, 0x00, 0x00, 0x00, 0x02, 0x00, 0x03, 0x00, 0x01, 0x00, 0x00, 0x00,
                                                 0x8C, 0x00, 0x00, 0x00, 0x70, 0x00, 0x00, 0x00, 0x9C, 0x00, 0x00, 0x00,
                                                 0x02, 0x00, 0x04, 0x00, 0x01, 0x00, 0x00, 0x00, 0x60, 0x00, 0x00, 0x00,
                                                 0x70, 0x00, 0x00, 0x00, 0x42, 0x6F, 0x72, 0x64, 0x65, 0x72, 0x00, 0xAB,
                                                 0x00, 0x00, 0x03, 0x00, 0x01, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x43, 0x69, 0x72, 0x63,
                                                 0x6C, 0x65, 0x43, 0x6F, 0x6C, 0x6F, 0x72, 0x00, 0x01, 0x00, 0x03, 0x00,
                                                 0x01, 0x00, 0x04, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x52, 0x61, 0x64, 0x69, 0x75, 0x73, 0x00, 0x70, 0x73, 0x5F, 0x32, 0x5F,
                                                 0x30, 0x00, 0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x20,
                                                 0x28, 0x52, 0x29, 0x20, 0x48, 0x4C, 0x53, 0x4C, 0x20, 0x53, 0x68, 0x61,
                                                 0x64, 0x65, 0x72, 0x20, 0x43, 0x6F, 0x6D, 0x70, 0x69, 0x6C, 0x65, 0x72,
                                                 0x20, 0x39, 0x2E, 0x32, 0x39, 0x2E, 0x39, 0x35, 0x32, 0x2E, 0x33, 0x31,
                                                 0x31, 0x31, 0x00, 0xAB, 0xFE, 0xFF, 0x7C, 0x00, 0x50, 0x52, 0x45, 0x53,
                                                 0x01, 0x02, 0x58, 0x46, 0xFE, 0xFF, 0x30, 0x00, 0x43, 0x54, 0x41, 0x42,
                                                 0x1C, 0x00, 0x00, 0x00, 0x8B, 0x00, 0x00, 0x00, 0x01, 0x02, 0x58, 0x46,
                                                 0x02, 0x00, 0x00, 0x00, 0x1C, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x20,
                                                 0x88, 0x00, 0x00, 0x00, 0x44, 0x00, 0x00, 0x00, 0x02, 0x00, 0x01, 0x00,
                                                 0x01, 0x00, 0x00, 0x00, 0x4C, 0x00, 0x00, 0x00, 0x5C, 0x00, 0x00, 0x00,
                                                 0x6C, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                                                 0x78, 0x00, 0x00, 0x00, 0x5C, 0x00, 0x00, 0x00, 0x42, 0x6F, 0x72, 0x64,
                                                 0x65, 0x72, 0x00, 0xAB, 0x00, 0x00, 0x03, 0x00, 0x01, 0x00, 0x01, 0x00,
                                                 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x43, 0x69, 0x72, 0x63, 0x6C, 0x65, 0x43, 0x6F, 0x6C, 0x6F, 0x72, 0x00,
                                                 0x01, 0x00, 0x03, 0x00, 0x01, 0x00, 0x04, 0x00, 0x01, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x74, 0x78, 0x00, 0x4D, 0x69, 0x63, 0x72, 0x6F,
                                                 0x73, 0x6F, 0x66, 0x74, 0x20, 0x28, 0x52, 0x29, 0x20, 0x48, 0x4C, 0x53,
                                                 0x4C, 0x20, 0x53, 0x68, 0x61, 0x64, 0x65, 0x72, 0x20, 0x43, 0x6F, 0x6D,
                                                 0x70, 0x69, 0x6C, 0x65, 0x72, 0x20, 0x39, 0x2E, 0x32, 0x39, 0x2E, 0x39,
                                                 0x35, 0x32, 0x2E, 0x33, 0x31, 0x31, 0x31, 0x00, 0xFE, 0xFF, 0x0C, 0x00,
                                                 0x50, 0x52, 0x53, 0x49, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0xFE, 0xFF, 0x1A, 0x00, 0x43, 0x4C, 0x49, 0x54, 0x0C, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xF0, 0xBF,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0xFE, 0xFF, 0x1F, 0x00, 0x46, 0x58, 0x4C, 0x43, 0x03, 0x00, 0x00, 0x00,
                                                 0x01, 0x00, 0x30, 0x10, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x02, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x40, 0xA0,
                                                 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00,
                                                 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                                                 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00,
                                                 0x04, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x10, 0x01, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00,
                                                 0xF0, 0xF0, 0xF0, 0xF0, 0x0F, 0x0F, 0x0F, 0x0F, 0xFF, 0xFF, 0x00, 0x00,
                                                 0x51, 0x00, 0x00, 0x05, 0x06, 0x00, 0x0F, 0xA0, 0x00, 0x00, 0xE0, 0x3F,
                                                 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x80, 0xBF, 0x00, 0x00, 0x00, 0x00,
                                                 0x1F, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x07, 0xB0,
                                                 0x05, 0x00, 0x00, 0x03, 0x00, 0x00, 0x08, 0x80, 0x00, 0x00, 0xAA, 0xB0,
                                                 0x00, 0x00, 0xAA, 0xB0, 0x04, 0x00, 0x00, 0x04, 0x00, 0x00, 0x01, 0x80,
                                                 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0xFF, 0x80,
                                                 0x07, 0x00, 0x00, 0x02, 0x00, 0x00, 0x01, 0x80, 0x00, 0x00, 0x00, 0x80,
                                                 0x06, 0x00, 0x00, 0x02, 0x00, 0x00, 0x01, 0x80, 0x00, 0x00, 0x00, 0x80,
                                                 0x02, 0x00, 0x00, 0x03, 0x00, 0x00, 0x01, 0x80, 0x00, 0x00, 0x00, 0x81,
                                                 0x04, 0x00, 0x00, 0xA0, 0x02, 0x00, 0x00, 0x03, 0x00, 0x00, 0x02, 0x80,
                                                 0x00, 0x00, 0x00, 0x81, 0x05, 0x00, 0x00, 0xA1, 0x58, 0x00, 0x00, 0x04,
                                                 0x00, 0x00, 0x02, 0x80, 0x00, 0x00, 0x55, 0x80, 0x06, 0x00, 0x55, 0xA0,
                                                 0x06, 0x00, 0xAA, 0xA0, 0x02, 0x00, 0x00, 0x03, 0x00, 0x00, 0x04, 0x80,
                                                 0x00, 0x00, 0x00, 0x80, 0x05, 0x00, 0x00, 0xA1, 0x58, 0x00, 0x00, 0x04,
                                                 0x00, 0x00, 0x02, 0x80, 0x00, 0x00, 0xAA, 0x80, 0x06, 0x00, 0x55, 0xA0,
                                                 0x00, 0x00, 0x55, 0x80, 0x05, 0x00, 0x00, 0x03, 0x00, 0x00, 0x04, 0x80,
                                                 0x00, 0x00, 0x00, 0x80, 0x06, 0x00, 0x00, 0xA0, 0x58, 0x00, 0x00, 0x04,
                                                 0x00, 0x00, 0x01, 0x80, 0x00, 0x00, 0x00, 0x80, 0x06, 0x00, 0xAA, 0xA0,
                                                 0x06, 0x00, 0x55, 0xA0, 0x01, 0x00, 0x00, 0x02, 0x00, 0x00, 0x08, 0x80,
                                                 0x06, 0x00, 0x55, 0xA0, 0x58, 0x00, 0x00, 0x04, 0x00, 0x00, 0x01, 0x80,
                                                 0x01, 0x00, 0x00, 0xA0, 0x00, 0x00, 0xFF, 0x80, 0x00, 0x00, 0x00, 0x80,
                                                 0x05, 0x00, 0x00, 0x03, 0x00, 0x00, 0x04, 0x80, 0x00, 0x00, 0xAA, 0x80,
                                                 0x00, 0x00, 0x00, 0xA0, 0x23, 0x00, 0x00, 0x02, 0x00, 0x00, 0x04, 0x80,
                                                 0x00, 0x00, 0xAA, 0x80, 0x04, 0x00, 0x00, 0x04, 0x00, 0x00, 0x04, 0x80,
                                                 0x03, 0x00, 0xFF, 0xA0, 0x00, 0x00, 0xAA, 0x81, 0x03, 0x00, 0xFF, 0xA0,
                                                 0x58, 0x00, 0x00, 0x04, 0x00, 0x00, 0x02, 0x80, 0x00, 0x00, 0x55, 0x80,
                                                 0x06, 0x00, 0xFF, 0xA0, 0x00, 0x00, 0xAA, 0x80, 0x58, 0x00, 0x00, 0x04,
                                                 0x00, 0x00, 0x08, 0x80, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x55, 0x80,
                                                 0x03, 0x00, 0xFF, 0xA0, 0x01, 0x00, 0x00, 0x02, 0x00, 0x00, 0x07, 0x80,
                                                 0x02, 0x00, 0xE4, 0xA0, 0x01, 0x00, 0x00, 0x02, 0x00, 0x08, 0x0F, 0x80,
                                                 0x00, 0x00, 0xE4, 0x80, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0x04, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x4C, 0x01, 0x00, 0x00, 0x00, 0x02, 0xFE, 0xFF,
                                                 0xFE, 0xFF, 0x34, 0x00, 0x43, 0x54, 0x41, 0x42, 0x1C, 0x00, 0x00, 0x00,
                                                 0x9B, 0x00, 0x00, 0x00, 0x00, 0x02, 0xFE, 0xFF, 0x01, 0x00, 0x00, 0x00,
                                                 0x1C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20, 0x94, 0x00, 0x00, 0x00,
                                                 0x30, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00,
                                                 0x44, 0x00, 0x00, 0x00, 0x54, 0x00, 0x00, 0x00, 0x50, 0x72, 0x6F, 0x6A,
                                                 0x65, 0x63, 0x74, 0x69, 0x6F, 0x6E, 0x4D, 0x61, 0x74, 0x72, 0x69, 0x78,
                                                 0x00, 0xAB, 0xAB, 0xAB, 0x03, 0x00, 0x03, 0x00, 0x04, 0x00, 0x04, 0x00,
                                                 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x76, 0x73, 0x5F, 0x32, 0x5F, 0x30, 0x00, 0x4D, 0x69, 0x63, 0x72, 0x6F,
                                                 0x73, 0x6F, 0x66, 0x74, 0x20, 0x28, 0x52, 0x29, 0x20, 0x48, 0x4C, 0x53,
                                                 0x4C, 0x20, 0x53, 0x68, 0x61, 0x64, 0x65, 0x72, 0x20, 0x43, 0x6F, 0x6D,
                                                 0x70, 0x69, 0x6C, 0x65, 0x72, 0x20, 0x39, 0x2E, 0x32, 0x39, 0x2E, 0x39,
                                                 0x35, 0x32, 0x2E, 0x33, 0x31, 0x31, 0x31, 0x00, 0x1F, 0x00, 0x00, 0x02,
                                                 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x0F, 0x90, 0x1F, 0x00, 0x00, 0x02,
                                                 0x0A, 0x00, 0x00, 0x80, 0x01, 0x00, 0x0F, 0x90, 0x09, 0x00, 0x00, 0x03,
                                                 0x00, 0x00, 0x01, 0xC0, 0x00, 0x00, 0xE4, 0x90, 0x00, 0x00, 0xE4, 0xA0,
                                                 0x09, 0x00, 0x00, 0x03, 0x00, 0x00, 0x02, 0xC0, 0x00, 0x00, 0xE4, 0x90,
                                                 0x01, 0x00, 0xE4, 0xA0, 0x09, 0x00, 0x00, 0x03, 0x00, 0x00, 0x04, 0xC0,
                                                 0x00, 0x00, 0xE4, 0x90, 0x02, 0x00, 0xE4, 0xA0, 0x09, 0x00, 0x00, 0x03,
                                                 0x00, 0x00, 0x08, 0xC0, 0x00, 0x00, 0xE4, 0x90, 0x03, 0x00, 0xE4, 0xA0,
                                                 0x01, 0x00, 0x00, 0x02, 0x00, 0x00, 0x0F, 0xD0, 0x01, 0x00, 0xE4, 0x90,
                                                 0x01, 0x00, 0x00, 0x02, 0x00, 0x00, 0x0F, 0xE0, 0x00, 0x00, 0xE4, 0x90,
                                                 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0xE0, 0x00, 0x00, 0x00, 0x00, 0x02, 0x58, 0x46, 0xFE, 0xFF, 0x25, 0x00,
                                                 0x43, 0x54, 0x41, 0x42, 0x1C, 0x00, 0x00, 0x00, 0x5F, 0x00, 0x00, 0x00,
                                                 0x00, 0x02, 0x58, 0x46, 0x01, 0x00, 0x00, 0x00, 0x1C, 0x00, 0x00, 0x00,
                                                 0x00, 0x01, 0x00, 0x20, 0x5C, 0x00, 0x00, 0x00, 0x30, 0x00, 0x00, 0x00,
                                                 0x02, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x3C, 0x00, 0x00, 0x00,
                                                 0x4C, 0x00, 0x00, 0x00, 0x7A, 0x45, 0x6E, 0x61, 0x62, 0x6C, 0x65, 0x64,
                                                 0x00, 0xAB, 0xAB, 0xAB, 0x00, 0x00, 0x01, 0x00, 0x01, 0x00, 0x01, 0x00,
                                                 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x74, 0x78, 0x00, 0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74,
                                                 0x20, 0x28, 0x52, 0x29, 0x20, 0x48, 0x4C, 0x53, 0x4C, 0x20, 0x53, 0x68,
                                                 0x61, 0x64, 0x65, 0x72, 0x20, 0x43, 0x6F, 0x6D, 0x70, 0x69, 0x6C, 0x65,
                                                 0x72, 0x20, 0x39, 0x2E, 0x32, 0x39, 0x2E, 0x39, 0x35, 0x32, 0x2E, 0x33,
                                                 0x31, 0x31, 0x31, 0x00, 0xFE, 0xFF, 0x02, 0x00, 0x43, 0x4C, 0x49, 0x54,
                                                 0x00, 0x00, 0x00, 0x00, 0xFE, 0xFF, 0x0C, 0x00, 0x46, 0x58, 0x4C, 0x43,
                                                 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x10, 0x01, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                 0xF0, 0xF0, 0xF0, 0xF0, 0x0F, 0x0F, 0x0F, 0x0F, 0xFF, 0xFF, 0x00, 0x00
                                             };
                    _effect = Effect.FromMemory(Device, compiledEffect, ShaderFlags.None);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return;
                }

                #endregion

                _technique = _effect.GetTechnique(0);

                if (!_initialized)
                {
                    _initialized = true;
                    //Drawing.OnPreReset += OnPreReset;
                    //Drawing.OnPreReset += OnPostReset;
                    //AppDomain.CurrentDomain.DomainUnload += Dispose;
                }
            }

            public static void DrawCircle(
                Vector3 position,
                float radius,
                Color color,
                int width = 5,
                bool zDeep = false)
            {
                try
                {
                    if (Device == null || Device.IsDisposed)
                    {
                        return;
                    }

                    if (_vertices == null)
                    {
                        CreateVertexes();
                    }

                    if (_vertices == null || _vertices.IsDisposed || _vertexDeclaration.IsDisposed || _effect.IsDisposed
                        || _technique.IsDisposed)
                    {
                        return;
                    }

                    var olddec = Device.VertexDeclaration;

                    _effect.Technique = _technique;

                    _effect.Begin();
                    _effect.BeginPass(0);
                    _effect.SetValue(
                        "ProjectionMatrix",
                        Matrix.Translation(position.SwitchYZ()) * Drawing.View * Drawing.Projection);
                    _effect.SetValue(
                        "CircleColor",
                        new Vector4(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f));
                    _effect.SetValue("Radius", radius);
                    _effect.SetValue("Border", 2f + width);
                    _effect.SetValue("zEnabled", zDeep);

                    Device.SetStreamSource(0, _vertices, 0, Utilities.SizeOf<Vector4>() * 2);
                    Device.VertexDeclaration = _vertexDeclaration;

                    Device.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);

                    _effect.EndPass();
                    _effect.End();

                    Device.VertexDeclaration = olddec;
                }
                catch (Exception e)
                {
                    _vertices = null;
                    Console.WriteLine(@"DrawCircle: " + e);
                }
            }

            public override void OnDraw()
            {
                try
                {
                    if (this.Unit != null && this.Unit.IsValid)
                    {
                        DrawCircle(this.Unit.Position + _offset, this.Radius, this.Color, this.Width, this.ZDeep);
                    }
                    else if ((this.Position + _offset).IsValid())
                    {
                        DrawCircle(this.Position + _offset, this.Radius, this.Color, this.Width, this.ZDeep);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(@"Common.Render.Circle.OnEndScene: " + e);
                }
            }

            #endregion

            #region Methods

            private static void Dispose(object sender, EventArgs e)
            {
                if (_effect != null && !_effect.IsDisposed)
                {
                    _effect.Dispose();
                }

                if (_vertices != null && !_vertices.IsDisposed)
                {
                    _vertices.Dispose();
                }

                if (_vertexDeclaration != null && !_vertexDeclaration.IsDisposed)
                {
                    _vertexDeclaration.Dispose();
                }
            }

            private static void OnPostReset(EventArgs args)
            {
                if (_effect != null && !_effect.IsDisposed)
                {
                    _effect.OnResetDevice();
                }
            }

            private static void OnPreReset(EventArgs args)
            {
                if (_effect != null && !_effect.IsDisposed)
                {
                    _effect.OnLostDevice();
                }
            }

            #endregion
        }

        public class Line : RenderObject
        {
            #region Fields

            public ColorBGRA Color;

            private readonly SharpDX.Direct3D9.Line _line;

            private int _width;

            #endregion

            #region Constructors and Destructors

            public Line(Vector2 start, Vector2 end, int width, ColorBGRA color)
            {
                this._line = new SharpDX.Direct3D9.Line(Device);
                this.Width = width;
                this.Color = color;
                this.Start = start;
                this.End = end;
                Game.OnUpdate += this.GameOnOnUpdate;
            }

            #endregion

            #region Delegates

            public delegate Vector2 PositionDelegate();

            #endregion

            #region Public Properties

            public Vector2 End { get; set; }

            public PositionDelegate EndPositionUpdate { get; set; }

            public Vector2 Start { get; set; }

            public PositionDelegate StartPositionUpdate { get; set; }

            public int Width
            {
                get
                {
                    return this._width;
                }
                set
                {
                    this._line.Width = value;
                    this._width = value;
                }
            }

            #endregion

            #region Public Methods and Operators

            public override void Dispose()
            {
                if (!this._line.IsDisposed)
                {
                    this._line.Dispose();
                }
                Game.OnUpdate -= this.GameOnOnUpdate;
            }

            public override void OnEndScene()
            {
                try
                {
                    if (this._line.IsDisposed)
                    {
                        return;
                    }

                    this._line.Begin();
                    this._line.Draw(new[] { this.Start, this.End }, this.Color);
                    this._line.End();
                }
                catch (Exception e)
                {
                    Console.WriteLine(@"Common.Render.Line.OnEndScene: " + e);
                }
            }

            public override void OnPostReset()
            {
                this._line.OnResetDevice();
            }

            public override void OnPreReset()
            {
                this._line.OnLostDevice();
            }

            #endregion

            #region Methods

            private void GameOnOnUpdate(EventArgs args)
            {
                if (this.StartPositionUpdate != null)
                {
                    this.Start = this.StartPositionUpdate();
                }

                if (this.EndPositionUpdate != null)
                {
                    this.End = this.EndPositionUpdate();
                }
            }

            #endregion
        }

        public class Rectangle : RenderObject
        {
            #region Fields

            public ColorBGRA Color;

            private readonly SharpDX.Direct3D9.Line _line;

            #endregion

            #region Constructors and Destructors

            public Rectangle(int x, int y, int width, int height, ColorBGRA color)
            {
                this.X = x;
                this.Y = y;
                this.Width = width;
                this.Height = height;
                this.Color = color;
                this._line = new SharpDX.Direct3D9.Line(Device) { Width = height };
                Game.OnUpdate += this.Game_OnUpdate;
            }

            #endregion

            #region Delegates

            public delegate Vector2 PositionDelegate();

            #endregion

            #region Public Properties

            public int Height { get; set; }

            public PositionDelegate PositionUpdate { get; set; }

            public int Width { get; set; }

            public int X { get; set; }

            public int Y { get; set; }

            #endregion

            #region Public Methods and Operators

            public override void Dispose()
            {
                if (!this._line.IsDisposed)
                {
                    this._line.Dispose();
                }
                Game.OnUpdate -= this.Game_OnUpdate;
            }

            public override void OnEndScene()
            {
                try
                {
                    if (this._line.IsDisposed)
                    {
                        return;
                    }

                    this._line.Begin();
                    this._line.Draw(
                        new[]
                            {
                                new Vector2(this.X, this.Y + this.Height / 2),
                                new Vector2(this.X + this.Width, this.Y + this.Height / 2)
                            },
                        this.Color);
                    this._line.End();
                }
                catch (Exception e)
                {
                    Console.WriteLine(@"Common.Render.Rectangle.OnEndScene: " + e);
                }
            }

            public override void OnPostReset()
            {
                this._line.OnResetDevice();
            }

            public override void OnPreReset()
            {
                this._line.OnLostDevice();
            }

            #endregion

            #region Methods

            private void Game_OnUpdate(EventArgs args)
            {
                if (this.PositionUpdate != null)
                {
                    var pos = this.PositionUpdate();
                    this.X = (int)pos.X;
                    this.Y = (int)pos.Y;
                }
            }

            #endregion
        }

        public class RenderObject : IDisposable
        {
            #region Fields

            public float Layer = 0.0f;

            public VisibleConditionDelegate VisibleCondition;

            private bool _visible = true;

            #endregion

            #region Delegates

            public delegate bool VisibleConditionDelegate(RenderObject sender);

            #endregion

            #region Public Properties

            public bool Visible
            {
                get
                {
                    return this.VisibleCondition != null ? this.VisibleCondition(this) : this._visible;
                }
                set
                {
                    this._visible = value;
                }
            }

            #endregion

            #region Public Methods and Operators

            public virtual void Dispose()
            {
            }

            public bool HasValidLayer()
            {
                return this.Layer >= -5 && this.Layer <= 5;
            }

            public virtual void OnDraw()
            {
            }

            public virtual void OnEndScene()
            {
            }

            public virtual void OnPostReset()
            {
            }

            public virtual void OnPreReset()
            {
            }

            #endregion
        }

        public class Sprite : RenderObject
        {
            #region Fields

            private readonly SharpDX.Direct3D9.Sprite _sprite = new SharpDX.Direct3D9.Sprite(Device);

            private ColorBGRA _color = SharpDX.Color.White;

            private SharpDX.Rectangle? _crop;

            private bool _hide;

            private Texture _originalTexture;

            private Vector2 _scale = new Vector2(1, 1);

            private Texture _texture;

            #endregion

            #region Constructors and Destructors

            public Sprite(Bitmap bitmap, Vector2 position)
                : this()
            {
                this.UpdateTextureBitmap(bitmap, position);
            }

            public Sprite(BaseTexture texture, Vector2 position)
                : this()
            {
                this.UpdateTextureBitmap(
                    (Bitmap)Image.FromStream(BaseTexture.ToStream(texture, ImageFileFormat.Bmp)),
                    position);
            }

            public Sprite(Stream stream, Vector2 position)
                : this()
            {
                this.UpdateTextureBitmap(new Bitmap(stream), position);
            }

            public Sprite(byte[] bytesArray, Vector2 position)
                : this()
            {
                this.UpdateTextureBitmap((Bitmap)Image.FromStream(new MemoryStream(bytesArray)), position);
            }

            public Sprite(string fileLocation, Vector2 position)
                : this()
            {
                if (!File.Exists((fileLocation)))
                {
                    return;
                }

                this.UpdateTextureBitmap(new Bitmap(fileLocation), position);
            }

            private Sprite()
            {
                Game.OnUpdate += this.Game_OnUpdate;
            }

            #endregion

            #region Delegates

            public delegate void OnResetting(Sprite sprite);

            public delegate Vector2 PositionDelegate();

            #endregion

            #region Public Events

            public event OnResetting OnReset;

            #endregion

            #region Public Properties

            public Bitmap Bitmap { get; set; }

            public ColorBGRA Color
            {
                set
                {
                    this._color = value;
                }
                get
                {
                    return this._color;
                }
            }

            public int Height
            {
                get
                {
                    return (int)(this.Bitmap.Height * this._scale.Y);
                }
            }

            public Vector2 Position
            {
                set
                {
                    this.X = (int)value.X;
                    this.Y = (int)value.Y;
                }

                get
                {
                    return new Vector2(this.X, this.Y);
                }
            }

            public PositionDelegate PositionUpdate { get; set; }

            public float Rotation { set; get; }

            public Vector2 Scale
            {
                set
                {
                    this._scale = value;
                }
                get
                {
                    return this._scale;
                }
            }

            public Vector2 Size
            {
                get
                {
                    return new Vector2(this.Bitmap.Width, this.Bitmap.Height);
                }
            }

            public int Width
            {
                get
                {
                    return (int)(this.Bitmap.Width * this._scale.X);
                }
            }

            public int X { get; set; }

            public int Y { get; set; }

            #endregion

            #region Public Methods and Operators

            public void Complement()
            {
                this.SetSaturation(-1.0f);
            }

            public void Crop(int x, int y, int w, int h, bool scale = false)
            {
                this._crop = new SharpDX.Rectangle(x, y, w, h);

                if (scale)
                {
                    this._crop = new SharpDX.Rectangle(
                        (int)(this._scale.X * x),
                        (int)(this._scale.Y * y),
                        (int)(this._scale.X * w),
                        (int)(this._scale.Y * h));
                }
            }

            public void Crop(SharpDX.Rectangle rect, bool scale = false)
            {
                this._crop = rect;

                if (scale)
                {
                    this._crop = new SharpDX.Rectangle(
                        (int)(this._scale.X * rect.X),
                        (int)(this._scale.Y * rect.Y),
                        (int)(this._scale.X * rect.Width),
                        (int)(this._scale.Y * rect.Height));
                }
            }

            public override void Dispose()
            {
                Game.OnUpdate -= this.Game_OnUpdate;
                if (!this._sprite.IsDisposed)
                {
                    this._sprite.Dispose();
                }

                if (!this._texture.IsDisposed)
                {
                    this._texture.Dispose();
                }

                if (!this._originalTexture.IsDisposed)
                {
                    this._originalTexture.Dispose();
                }
            }

            public void Fade()
            {
                this.SetSaturation(0.5f);
            }

            public void GrayScale()
            {
                this.SetSaturation(0.0f);
            }

            public void Hide()
            {
                this._hide = true;
            }

            public override void OnEndScene()
            {
                try
                {
                    if (this._sprite.IsDisposed || this._texture.IsDisposed || !this.Position.IsValid() || this._hide)
                    {
                        return;
                    }

                    this._sprite.Begin();
                    var matrix = this._sprite.Transform;
                    var nMatrix = (Matrix.Scaling(this.Scale.X, this.Scale.Y, 0)) * Matrix.RotationZ(this.Rotation)
                                  * Matrix.Translation(this.Position.X, this.Position.Y, 0);
                    this._sprite.Transform = nMatrix;
                    this._sprite.Draw(this._texture, this._color, this._crop);
                    this._sprite.Transform = matrix;
                    this._sprite.End();
                }
                catch (Exception e)
                {
                    this.Reset();
                    Console.WriteLine(@"Common.Render.Sprite.OnEndScene: " + e);
                }
            }

            public override void OnPostReset()
            {
                this._sprite.OnResetDevice();
            }

            public override void OnPreReset()
            {
                this._sprite.OnLostDevice();
            }

            public void Reset()
            {
                this.UpdateTextureBitmap(
                    (Bitmap)Image.FromStream(BaseTexture.ToStream(this._originalTexture, ImageFileFormat.Bmp)));

                if (this.OnReset != null)
                {
                    this.OnReset(this);
                }
            }

            public void SetSaturation(float saturiation)
            {
                this.UpdateTextureBitmap(SaturateBitmap(this.Bitmap, saturiation));
            }

            public void Show()
            {
                this._hide = false;
            }

            public void UpdateTextureBitmap(Bitmap newBitmap, Vector2 position = new Vector2())
            {
                if (position.IsValid())
                {
                    this.Position = position;
                }

                if (this.Bitmap != null)
                {
                    this.Bitmap.Dispose();
                }
                this.Bitmap = newBitmap;

                this._texture = Texture.FromMemory(
                    Device,
                    (byte[])new ImageConverter().ConvertTo(newBitmap, typeof(byte[])),
                    this.Width,
                    this.Height,
                    0,
                    Usage.None,
                    Format.A1,
                    Pool.Managed,
                    Filter.Default,
                    Filter.Default,
                    0);

                if (this._originalTexture == null)
                {
                    this._originalTexture = this._texture;
                }
            }

            #endregion

            #region Methods

            private static Bitmap SaturateBitmap(Image original, float saturation)
            {
                const float rWeight = 0.3086f;
                const float gWeight = 0.6094f;
                const float bWeight = 0.0820f;

                var a = (1.0f - saturation) * rWeight + saturation;
                var b = (1.0f - saturation) * rWeight;
                var c = (1.0f - saturation) * rWeight;
                var d = (1.0f - saturation) * gWeight;
                var e = (1.0f - saturation) * gWeight + saturation;
                var f = (1.0f - saturation) * gWeight;
                var g = (1.0f - saturation) * bWeight;
                var h = (1.0f - saturation) * bWeight;
                var i = (1.0f - saturation) * bWeight + saturation;

                var newBitmap = new Bitmap(original.Width, original.Height);
                var gr = Graphics.FromImage(newBitmap);

                // ColorMatrix elements
                float[][] ptsArray =
                    {
                        new[] { a, b, c, 0, 0 }, new[] { d, e, f, 0, 0 }, new[] { g, h, i, 0, 0 },
                        new float[] { 0, 0, 0, 1, 0 }, new float[] { 0, 0, 0, 0, 1 }
                    };
                // Create ColorMatrix
                var clrMatrix = new ColorMatrix(ptsArray);
                // Create ImageAttributes
                var imgAttribs = new ImageAttributes();
                // Set color matrix
                imgAttribs.SetColorMatrix(clrMatrix, ColorMatrixFlag.Default, ColorAdjustType.Default);
                // Draw Image with no effects
                gr.DrawImage(original, 0, 0, original.Width, original.Height);
                // Draw Image with image attributes
                gr.DrawImage(
                    original,
                    new System.Drawing.Rectangle(0, 0, original.Width, original.Height),
                    0,
                    0,
                    original.Width,
                    original.Height,
                    GraphicsUnit.Pixel,
                    imgAttribs);
                gr.Dispose();

                return newBitmap;
            }

            private void Game_OnUpdate(EventArgs args)
            {
                if (this.PositionUpdate != null)
                {
                    var pos = this.PositionUpdate();
                    this.X = (int)pos.X;
                    this.Y = (int)pos.Y;
                }
            }

            #endregion
        }

        /// <summary>
        ///     Object used to draw text on the screen.
        /// </summary>
        public class Text : RenderObject
        {
            #region Fields

            /// <summary>
            /// </summary>
            public bool Centered = false;

            /// <summary>
            /// </summary>
            public Vector2 Offset;

            /// <summary>
            /// </summary>
            public bool OutLined = false;

            /// <summary>
            /// </summary>
            public PositionDelegate PositionUpdate;

            /// <summary>
            /// </summary>
            public TextDelegate TextUpdate;

            /// <summary>
            /// </summary>
            public Unit Unit;

            private string _text;

            private Font textFont;

            private int x;

            private int xCalculated;

            private int y;

            private int yCalculated;

            #endregion

            #region Constructors and Destructors

            public Text(string text, int x, int y, int size, ColorBGRA color, string fontName = "Calibri")
                : this(text, fontName, size, color)
            {
                this.x = x;
                this.y = y;
            }

            public Text(string text, Vector2 position, int size, ColorBGRA color, string fontName = "Calibri")
                : this(text, fontName, size, color)
            {
                this.x = (int)position.X;
                this.y = (int)position.Y;
            }

            public Text(string text, Unit unit, Vector2 offset, int size, ColorBGRA color, string fontName = "Calibri")
                : this(text, fontName, size, color)
            {
                this.Unit = unit;
                this.Offset = offset;

                var pos = unit.HealthBarOffset + offset;

                this.x = (int)pos.X;
                this.y = (int)pos.Y;
            }

            public Text(int x, int y, string text, int size, ColorBGRA color, string fontName = "Calibri")
                : this(text, fontName, size, color)
            {
                this.x = x;
                this.y = y;
            }

            public Text(Vector2 position, string text, int size, ColorBGRA color, string fontName = "Calibri")
                : this(text, fontName, size, color)
            {
                this.x = (int)position.X;
                this.y = (int)position.Y;
            }

            private Text(string text, string fontName, int size, ColorBGRA color)
            {
                this.textFont = new Font(
                    Device,
                    new FontDescription
                        {
                            FaceName = fontName, Height = size, OutputPrecision = FontPrecision.Default,
                            Quality = FontQuality.Default
                        });
                this.Color = color;
                this.text = text;
                Game.OnUpdate += this.Game_OnUpdate;
            }

            #endregion

            #region Delegates

            /// <summary>
            /// </summary>
            public delegate Vector2 PositionDelegate();

            /// <summary>
            /// </summary>
            public delegate string TextDelegate();

            #endregion

            #region Public Properties

            public ColorBGRA Color { get; set; }

            public int Height { get; private set; }

            public string text
            {
                get
                {
                    return this._text;
                }
                set
                {
                    if (value != this._text && this.textFont != null && !this.textFont.IsDisposed
                        && !string.IsNullOrEmpty(value))
                    {
                        var size = this.textFont.MeasureText(null, value, 0);
                        this.Width = size.Width;
                        this.Height = size.Height;
                        this.textFont.PreloadText(value);
                    }
                    this._text = value;
                }
            }

            /// <summary>
            /// </summary>
            public FontDescription TextFontDescription
            {
                get
                {
                    return this.textFont.Description;
                }

                set
                {
                    this.textFont.Dispose();
                    this.textFont = new Font(Device, value);
                }
            }

            /// <summary>
            /// </summary>
            public int Width { get; private set; }

            /// <summary>
            /// </summary>
            public int X
            {
                get
                {
                    if (this.PositionUpdate != null)
                    {
                        return this.xCalculated;
                    }
                    return this.x + this.XOffset;
                }
                set
                {
                    this.x = value;
                }
            }

            /// <summary>
            /// </summary>
            public int Y
            {
                get
                {
                    if (this.PositionUpdate != null)
                    {
                        return this.yCalculated;
                    }
                    return this.y + this.YOffset;
                }
                set
                {
                    this.y = value;
                }
            }

            #endregion

            #region Properties

            private int XOffset
            {
                get
                {
                    return this.Centered ? -this.Width / 2 : 0;
                }
            }

            private int YOffset
            {
                get
                {
                    return this.Centered ? -this.Height / 2 : 0;
                }
            }

            #endregion

            #region Public Methods and Operators

            public override void Dispose()
            {
                Game.OnUpdate -= this.Game_OnUpdate;
                if (!this.textFont.IsDisposed)
                {
                    this.textFont.Dispose();
                }
            }

            public override void OnEndScene()
            {
                try
                {
                    if (this.textFont.IsDisposed || this.text == "")
                    {
                        return;
                    }

                    if (this.Unit != null && this.Unit.IsValid)
                    {
                        var pos = this.Unit.HealthBarOffset + this.Offset;
                        this.X = (int)pos.X;
                        this.Y = (int)pos.Y;
                    }

                    var xP = this.X;
                    var yP = this.Y;
                    if (this.OutLined)
                    {
                        var outlineColor = new ColorBGRA(0, 0, 0, 255);
                        this.textFont.DrawText(null, this.text, xP - 1, yP - 1, outlineColor);
                        this.textFont.DrawText(null, this.text, xP + 1, yP + 1, outlineColor);
                        this.textFont.DrawText(null, this.text, xP - 1, yP, outlineColor);
                        this.textFont.DrawText(null, this.text, xP + 1, yP, outlineColor);
                    }
                    this.textFont.DrawText(null, this.text, xP, yP, this.Color);
                }
                catch (Exception e)
                {
                    Console.WriteLine(@"Common.Render.Text.OnEndScene: " + e);
                }
            }

            public override void OnPostReset()
            {
                this.textFont.OnResetDevice();
            }

            public override void OnPreReset()
            {
                this.textFont.OnLostDevice();
            }

            #endregion

            #region Methods

            private void Game_OnUpdate(EventArgs args)
            {
                if (this.Visible)
                {
                    if (this.TextUpdate != null)
                    {
                        this.text = this.TextUpdate();
                    }

                    if (this.PositionUpdate != null && !string.IsNullOrEmpty(this.text))
                    {
                        var pos = this.PositionUpdate();
                        this.xCalculated = (int)pos.X + this.XOffset;
                        this.yCalculated = (int)pos.Y + this.YOffset;
                    }
                }
            }

            #endregion
        }
    }

    public static class FontExtension
    {
        #region Static Fields

        private static readonly Dictionary<Font, Dictionary<string, Rectangle>> Widths =
            new Dictionary<Font, Dictionary<string, Rectangle>>();

        #endregion

        #region Public Methods and Operators

        public static Rectangle MeasureText(this Font font, Sprite sprite, string text)
        {
            Dictionary<string, Rectangle> rectangles;
            if (!Widths.TryGetValue(font, out rectangles))
            {
                rectangles = new Dictionary<string, Rectangle>();
                Widths[font] = rectangles;
            }

            Rectangle rectangle;
            if (rectangles.TryGetValue(text, out rectangle))
            {
                return rectangle;
            }
            rectangle = font.MeasureText(sprite, text, 0);
            rectangles[text] = rectangle;
            return rectangle;
        }

        public static Rectangle MeasureText(this Font font, string text)
        {
            return font.MeasureText(null, text);
        }

        #endregion
    }
}