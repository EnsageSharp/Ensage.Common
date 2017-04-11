// <copyright file="MenuConfig.cs" company="EnsageSharp">
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
namespace Ensage.Common.Menu
{
    using System;
    using System.IO;

    using EnsageSharp.Sandbox;

    /// <summary>
    /// </summary>
    public static class MenuConfig
    {
        #region Static Fields

        private static string appDataDirectory;

        private static string selectedLanguage;

        private static byte showMenuHotkey;

        private static byte showMenuToggleHotkey;

        #endregion

        #region Public Properties

        /// <summary>
        /// </summary>
        public static string AppDataDirectory
        {
            get
            {
                if (appDataDirectory == null)
                {
                    appDataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "game");

                    if (!Directory.Exists(appDataDirectory))
                    {
                        Directory.CreateDirectory(appDataDirectory);
                    }
                }

                return appDataDirectory;
            }
        }

        /// <summary>
        /// </summary>
        public static string SelectedLanguage
        {
            get
            {
                if (selectedLanguage == null)
                {
                    try
                    {
                        selectedLanguage = SandboxConfig.SelectedLanguage;
                        if (selectedLanguage == "Traditional-Chinese")
                        {
                            selectedLanguage = "Chinese";
                        }
                    }
                    catch (Exception)
                    {
                        selectedLanguage = string.Empty;
                        Console.WriteLine(@"Could not get the menu language");
                    }
                }

                return selectedLanguage;
            }
        }

        /// <summary>
        /// </summary>
        public static byte ShowMenuPressKey
        {
            get
            {
                if (showMenuHotkey == 0)
                {
                    try
                    {
                        showMenuHotkey = (byte)SandboxConfig.MenuKey;
                        showMenuHotkey = showMenuHotkey == 0 ? (byte)16 : showMenuHotkey;
                        showMenuHotkey = Utils.FixVirtualKey(showMenuHotkey);
                        Console.WriteLine(@"Menu press key set to {0}", showMenuHotkey);
                    }
                    catch
                    {
                        showMenuHotkey = 16;
                        Console.WriteLine(@"Could not get the menu press key");
                    }
                }

                return showMenuHotkey;
            }
        }

        /// <summary>
        /// </summary>
        public static byte ShowMenuToggleKey
        {
            get
            {
                if (showMenuToggleHotkey == 0)
                {
                    try
                    {
                        showMenuToggleHotkey = (byte)SandboxConfig.MenuToggleKey;
                        showMenuToggleHotkey = showMenuToggleHotkey == 0 ? (byte)120 : showMenuToggleHotkey;
                        showMenuToggleHotkey = Utils.FixVirtualKey(showMenuToggleHotkey);
                        Console.WriteLine(@"Menu toggle key set to {0}", showMenuToggleHotkey);
                    }
                    catch
                    {
                        showMenuToggleHotkey = 120;
                        Console.WriteLine(@"Could not get the menu toggle key");
                    }
                }

                return showMenuToggleHotkey;
            }
        }

        #endregion
    }
}
