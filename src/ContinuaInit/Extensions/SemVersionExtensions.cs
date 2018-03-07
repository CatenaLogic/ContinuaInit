// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit
{
    using System;
    using System.Collections.Generic;
    using Catel;
    using Semver;

    public static class SemVersionExtensions
    {
        public static bool IsOfficial(this SemVersion versionValue)
        {
            if (versionValue == null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(versionValue.Prerelease))
            {
                return false;
            }

            return true;
        }

        public static bool IsAlpha(this SemVersion versionValue)
        {
            if (versionValue == null)
            {
                return false;
            }

            var prerelease = versionValue.Prerelease;
            if (string.IsNullOrWhiteSpace(prerelease))
            {
                return false;
            }

            return prerelease.ContainsIgnoreCase("alpha") ||
                   prerelease.ContainsIgnoreCase("unstable");
        }

        public static bool IsBeta(this SemVersion versionValue)
        {
            if (versionValue == null)
            {
                return false;
            }

            var prerelease = versionValue.Prerelease;
            if (string.IsNullOrWhiteSpace(prerelease))
            {
                return false;
            }

            return prerelease.ContainsIgnoreCase("beta");
        }
    }
}