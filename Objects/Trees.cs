namespace Ensage.Common.Objects
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     The trees.
    /// </summary>
    public class Trees
    {
        #region Static Fields

        /// <summary>
        ///     The all.
        /// </summary>
        private static List<Tree> all;

        /// <summary>
        ///     The loaded.
        /// </summary>
        private static bool loaded;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="Trees" /> class.
        /// </summary>
        static Trees()
        {
            all = ObjectMgr.GetEntities<Tree>().ToList();
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
                    ObjectMgr.OnRemoveEntity -= ObjectMgr_OnRemoveEntity;
                    loaded = false;
                };
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The load.
        /// </summary>
        private static void Load()
        {
            all = ObjectMgr.GetEntities<Tree>().ToList();
            ObjectMgr.OnRemoveEntity += ObjectMgr_OnRemoveEntity;
            loaded = true;
        }

        /// <summary>
        ///     The object manager on remove entity.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private static void ObjectMgr_OnRemoveEntity(EntityEventArgs args)
        {
            var tree = args.Entity as Tree;
            if (tree != null)
            {
                all.Remove(tree);
            }
        }

        #endregion
    }
}