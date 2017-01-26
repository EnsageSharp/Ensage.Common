// <copyright file="Composer.cs" company="EnsageSharp">
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
namespace Ensage.Common
{
    using System;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Security.Permissions;

    /// <summary>
    ///     The composer.
    /// </summary>
    public class Composer
    {
        #region Static Fields

        /// <summary>
        ///     The container.
        /// </summary>
        private static readonly CompositionContainer Container;

        /// <summary>
        ///     The catalog.
        /// </summary>
        private static AggregateCatalog catalog;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="Composer" /> class.
        /// </summary>
        [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
        static Composer()
        {
            // An aggregate catalog that combines multiple catalogs
            catalog = new AggregateCatalog();

            // Adds all the parts found in the assembly
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Composer).Assembly));

            Container = new CompositionContainer(catalog);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Composer" /> class.
        /// </summary>
        public Composer()
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The compose parts.
        /// </summary>
        /// <param name="composeObject">
        ///     The compose object.
        /// </param>
        [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
        public static void ComposeParts(object composeObject)
        {
            // Fill the imports of this object
            try
            {
                Container.ComposeParts(composeObject);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }

        #endregion
    }
}