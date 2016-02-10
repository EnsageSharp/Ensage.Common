namespace Ensage.Common.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     The creeps.
    /// </summary>
    public class Creeps
    {
        #region Static Fields

        /// <summary>
        /// The all.
        /// </summary>
        public static List<Creep> All;

        /// <summary>
        ///     The loaded.
        /// </summary>
        private static bool loaded;

        /// <summary>
        ///     The temp list.
        /// </summary>
        private static List<Creep> tempList;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="Creeps" /> class.
        /// </summary>
        static Creeps()
        {
            All = new List<Creep>();
            Events.OnLoad += (sender, args) =>
                {
                    if (loaded)
                    {
                        return;
                    }

                    Load();
                };
            if (!loaded && ObjectMgr.LocalHero != null && Game.IsInGame)
            {
                Load();
            }

            Events.OnClose += (sender, args) =>
                {
                    Game.OnUpdate -= Update;
                    ObjectMgr.OnAddEntity -= ObjectMgr_OnAddEntity;
                    ObjectMgr.OnRemoveEntity -= ObjectMgr_OnRemoveEntity;
                    loaded = false;
                };
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The update.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        public static void Update(EventArgs args)
        {
            if (!Game.IsInGame || Game.IsPaused)
            {
                return;
            }

            if (!Utils.SleepCheck("Common.Creeps.Update"))
            {
                return;
            }

            if (Utils.SleepCheck("Common.Creeps.SpecialUpdate"))
            {
                tempList = ObjectMgr.GetEntities<Creep>().ToList();
                Utils.Sleep(1700, "Common.Creeps.SpecialUpdate");
                return;
            }

            UpdateCreeps();
            Utils.Sleep(500, "Common.Creeps.Update");
        }

        /// <summary>
        ///     The update creeps.
        /// </summary>
        public static void UpdateCreeps()
        {
            All = tempList.Where(creep => creep.IsValid).ToList();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The load.
        /// </summary>
        private static void Load()
        {
            All = new List<Creep>();
            tempList = ObjectMgr.GetEntities<Creep>().ToList();
            Game.OnUpdate += Update;
            ObjectMgr.OnAddEntity += ObjectMgr_OnAddEntity;
            ObjectMgr.OnRemoveEntity += ObjectMgr_OnRemoveEntity;
            loaded = true;
        }

        /// <summary>
        ///     The object manager on add entity.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private static void ObjectMgr_OnAddEntity(EntityEventArgs args)
        {
            DelayAction.Add(
                50, 
                () =>
                    {
                        var creep = args.Entity as Creep;
                        if (creep != null)
                        {
                            tempList.Add(creep);
                        }
                    });
        }

        /// <summary>
        ///     The object manager on remove entity.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private static void ObjectMgr_OnRemoveEntity(EntityEventArgs args)
        {
            var creep = args.Entity as Creep;
            if (creep != null)
            {
                tempList.Remove(creep);
            }
        }

        #endregion
    }
}