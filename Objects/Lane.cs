namespace Ensage.Common.Objects
{
    using System.Collections.Generic;

    using SharpDX;

    /// <summary>
    ///     The lane.
    /// </summary>
    public class Lane
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Lane" /> class.
        /// </summary>
        public Lane()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Lane" /> class.
        /// </summary>
        /// <param name="path">
        ///     The path.
        /// </param>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <param name="team">
        ///     The team.
        /// </param>
        public Lane(ICollection<Vector3> path, string name, Team team)
        {
            this.Path = path;
            this.Name = name;
            this.Team = team;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the path.
        /// </summary>
        public ICollection<Vector3> Path { get; set; }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        public LanePosition Position { get; set; }

        /// <summary>
        ///     Gets or sets the team.
        /// </summary>
        public Team Team { get; set; }

        #endregion
    }
}