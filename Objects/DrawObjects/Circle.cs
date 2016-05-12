namespace Ensage.Common.Objects.DrawObjects
{
    using System;
    using System.Collections.Generic;

    using Ensage.Common.Extensions;
    using Ensage.Common.Objects.UtilityObjects;

    using SharpDX;

    /// <summary>
    ///     The circle.
    /// </summary>
    public class Circle : Polygon
    {
        #region Fields

        /// <summary>
        ///     The circle line segment.
        /// </summary>
        private readonly int circleLineSegment;

        /// <summary>
        ///     The world update sleeper.
        /// </summary>
        private readonly Sleeper worldUpdateSleeper;

        /// <summary>
        ///     The center.
        /// </summary>
        private Vector2 center;

        /// <summary>
        ///     The radius.
        /// </summary>
        private float radius;

        /// <summary>
        ///     The screen position.
        /// </summary>
        private Vector2 screenPosition;

        /// <summary>
        ///     The world position.
        /// </summary>
        private Vector3 worldPosition;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Circle" /> class.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        /// <param name="radius">
        ///     The radius.
        /// </param>
        public Circle(Vector2 position, float radius)
        {
            this.DrawType = DrawType.Screen;
            this.circleLineSegment = (int)(radius / 4);
            this.ScreenPosition = position;
            this.Radius = radius;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Circle" /> class.
        /// </summary>
        /// <param name="position">
        ///     The position.
        /// </param>
        /// <param name="radius">
        ///     The radius.
        /// </param>
        public Circle(Vector3 position, float radius)
        {
            this.worldUpdateSleeper = new Sleeper();
            this.DrawType = DrawType.World;
            this.circleLineSegment = (int)(radius / 4);
            this.Ground = position.Z;
            this.WorldPosition = position;
            this.Radius = radius;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the radius.
        /// </summary>
        public float Radius
        {
            get
            {
                return this.radius;
            }

            set
            {
                if (Math.Abs(this.radius - value) < 2)
                {
                    return;
                }

                this.radius = value;
                this.UpdatePolygon();
            }
        }

        /// <summary>
        ///     Gets or sets the screen position.
        /// </summary>
        public Vector2 ScreenPosition
        {
            get
            {
                return this.screenPosition;
            }

            set
            {
                if (this.screenPosition == value)
                {
                    return;
                }

                this.screenPosition = value;
                this.center = value;
                this.UpdatePolygon();
            }
        }

        /// <summary>
        ///     Gets or sets the world position.
        /// </summary>
        public Vector3 WorldPosition
        {
            get
            {
                return this.worldPosition;
            }

            set
            {
                if (this.worldPosition == value)
                {
                    return;
                }

                this.worldPosition = value;
                this.center = value.ToVector2();
                this.UpdatePolygon();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The update polygon.
        /// </summary>
        private void UpdatePolygon()
        {
            if (this.DrawType == DrawType.Screen)
            {
                this.ScreenPoints = new List<Vector2>();
                var outRadius = this.Radius / (float)Math.Cos(2 * Math.PI / this.circleLineSegment);

                var step = 2 * Math.PI / this.circleLineSegment;
                var angle = (double)this.Radius;
                for (var i = 0; i <= this.circleLineSegment; i++)
                {
                    angle += step;
                    var point = new Vector2(
                        this.center.X + (outRadius * (float)Math.Cos(angle)), 
                        this.center.Y + (outRadius * (float)Math.Sin(angle)));
                    this.ScreenPoints.Add(point);
                }
            }
            else
            {
                if (!this.worldUpdateSleeper.Sleeping)
                {
                    this.Ground = Polygon.GetGround(this.center);
                    this.worldUpdateSleeper.Sleep(500);
                }

                this.WorldPoints = new List<Vector3>();
                var outRadius = this.Radius / (float)Math.Cos(2 * Math.PI / this.circleLineSegment);

                var step = 2 * Math.PI / this.circleLineSegment;
                var angle = (double)this.Radius;
                for (var i = 0; i <= this.circleLineSegment; i++)
                {
                    angle += step;
                    var point = new Vector3(
                        this.center.X + (outRadius * (float)Math.Cos(angle)), 
                        this.center.Y + (outRadius * (float)Math.Sin(angle)), 
                        this.Ground);
                    this.WorldPoints.Add(point);
                }
            }
        }

        #endregion
    }
}