// <copyright file="JungleCamp.cs" company="EnsageSharp">
//    Copyright (c) 2017 EnsageSharp.
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
namespace Ensage.Common.Objects
{
    using SharpDX;

    /// <summary>
    ///     The jungle camp class.
    /// </summary>
    public class JungleCamp
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="JungleCamp" /> class.
        /// </summary>
        public JungleCamp()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JungleCamp" /> class.
        /// </summary>
        /// <param name="campPosition">
        ///     The camp position.
        /// </param>
        /// <param name="stackPosition">
        ///     The stack position.
        /// </param>
        /// <param name="waitPosition">
        ///     The wait position.
        /// </param>
        /// <param name="team">
        ///     The team.
        /// </param>
        /// <param name="id">
        ///     The id.
        /// </param>
        /// <param name="stackTime">
        ///     The stack time.
        /// </param>
        /// <param name="ancients">
        ///     The ancients.
        /// </param>
        /// <param name="name">
        ///     The name.
        /// </param>
        public JungleCamp(
            Vector3 campPosition,
            Vector3 stackPosition,
            Vector3 waitPosition,
            Team team,
            uint id,
            double stackTime,
            bool ancients,
            string name)
        {
            this.CampPosition = campPosition;
            this.StackPosition = stackPosition;
            this.WaitPosition = waitPosition;
            this.Team = team;
            this.Id = id;
            this.StackTime = stackTime;
            this.Ancients = ancients;
            this.Name = name;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether it is ancient camp.
        /// </summary>
        public bool Ancients { get; set; }

        /// <summary>
        ///     Gets or sets the camp position.
        /// </summary>
        public Vector3 CampPosition { get; set; }

        /// <summary>
        ///     Gets or sets the camp id.
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the stack position.
        /// </summary>
        public Vector3 StackPosition { get; set; }

        /// <summary>
        ///     Gets or sets the stack time (time when creeps should be pulled out of the camp.
        /// </summary>
        public double StackTime { get; set; }

        /// <summary>
        ///     Gets or sets the camp team.
        /// </summary>
        public Team Team { get; set; }

        /// <summary>
        ///     Gets or sets the wait position (position where you have vision over creeps but does not block it from spawning if
        ///     possible).
        /// </summary>
        public Vector3 WaitPosition { get; set; }

        #endregion

        public override bool Equals(object obj)
        {
            JungleCamp p = obj as JungleCamp;
            return p?.Id == this.Id;
        }

        public bool Equals(JungleCamp obj)
        {
            return obj?.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return (int)this.Id;
        }
    }
}