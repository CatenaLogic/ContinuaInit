// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit
{
    using System;
    using Catel;

    public static class StringExtensions
    {
        public static bool IsOfficial(this string versionValue)
        {
            if (string.IsNullOrWhiteSpace(versionValue))
            {
                return false;
            }

            if (versionValue.ContainsIgnoreCase("-"))
            {
                return false;
            }

            Version version;
            return Version.TryParse(versionValue, out version);
        }

        public static bool IsAlpha(this string versionValue)
        {
            if (string.IsNullOrWhiteSpace(versionValue))
            {
                return false;
            }

            return versionValue.ContainsIgnoreCase("-alpha") ||
                   versionValue.ContainsIgnoreCase("-unstable");
        }

        public static bool IsBeta(this string versionValue)
        {
            if (string.IsNullOrWhiteSpace(versionValue))
            {
                return false;
            }

            return versionValue.ContainsIgnoreCase("-beta");
        }
    }
}