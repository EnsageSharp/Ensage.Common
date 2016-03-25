namespace Ensage.Common.Objects
{
    using System.Collections.Generic;

    using SharpDX;

    /// <summary>
    ///     The lanes.
    /// </summary>
    public class Lanes
    {
        #region Static Fields

        /// <summary>
        ///     The list.
        /// </summary>
        private static readonly ICollection<Lane> List = new List<Lane>
                                                             {
                                                                 new Lane
                                                                     {
                                                                         Path =
                                                                             new List<Vector3>
                                                                                 {
                                                                                     new Vector3(-3466, -6043, 0), 
                                                                                     new Vector3(-673, -6349, 0), 
                                                                                     new Vector3(4478, -6119, 0), 
                                                                                     new Vector3(5490, -5672, 0), 
                                                                                     new Vector3(6172, -3896, 0)
                                                                                 }, 
                                                                         Name = "Radiant Bot Lane", Team = Team.Radiant, 
                                                                         Position = LanePosition.Bottom
                                                                     }
                                                                     // add other lanes
                                                             };

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the get lanes.
        /// </summary>
        public ICollection<Lane> GetLanes
        {
            get
            {
                return List;
            }
        }

        #endregion
    }
}