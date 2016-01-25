namespace Ensage.Common.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Creeps
    {
        #region Static Fields

        /// <summary>
        /// </summary>
        public static List<Creep> All;

        private static bool loaded;

        private static List<Creep> tempList;

        #endregion

        #region Constructors and Destructors

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
        /// </summary>
        /// <param name="args"></param>
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
                Utils.Sleep(10000, "Common.Creeps.SpecialUpdate");
            }

            UpdateCreeps();
            Utils.Sleep(500, "Common.Creeps.Update");
        }

        /// <summary>
        /// </summary>
        public static void UpdateCreeps()
        {
            All = tempList.Where(creep => creep.IsValid && creep.IsAlive && creep.IsSpawned && creep.IsVisible).ToList();
        }

        #endregion

        #region Methods

        private static void Load()
        {
            All = new List<Creep>();
            tempList = ObjectMgr.GetEntities<Creep>().ToList();
            Game.OnUpdate += Update;
            ObjectMgr.OnAddEntity += ObjectMgr_OnAddEntity;
            ObjectMgr.OnRemoveEntity += ObjectMgr_OnRemoveEntity;
            loaded = true;
        }

        static void ObjectMgr_OnAddEntity(EntityEventArgs args)
        {
            var creep = args.Entity as Creep;
            DelayAction.Add(
                50, 
                () =>
                    {
                        if (creep != null)
                        {
                            tempList.Add(creep);
                        }
                    });
        }

        static void ObjectMgr_OnRemoveEntity(EntityEventArgs args)
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