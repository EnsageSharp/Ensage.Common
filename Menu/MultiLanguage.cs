// <copyright file="MultiLanguage.cs" company="EnsageSharp">
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
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;

    using Ensage.Common.Properties;

    using EnsageSharp.Sandbox;

    /// <summary>
    /// </summary>
    public static class MultiLanguage
    {
        #region Static Fields

        private static Dictionary<string, string> translations = new Dictionary<string, string>();

        #endregion

        #region Constructors and Destructors

        static MultiLanguage()
        {
            LoadLanguage(MenuConfig.SelectedLanguage);
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        /// <param name="textToTranslate"></param>
        /// <returns></returns>
        public static string _(string textToTranslate)
        {
            var textToTranslateToLower = textToTranslate.ToLower();

            if (translations.ContainsKey(textToTranslateToLower) && !string.IsNullOrEmpty(translations[textToTranslateToLower]))
            {
                return translations[textToTranslateToLower];
            }

            return textToTranslate;
        }

        /// <summary>
        /// </summary>
        /// <param name="languageName"></param>
        /// <returns></returns>
        public static bool LoadLanguage(string languageName)
        {
            try
            {
                if (SandboxConfig.SelectedLanguage.StartsWith("zh"))
                {
                    translations = TranslationChinese.Values;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        #endregion
    }
}
