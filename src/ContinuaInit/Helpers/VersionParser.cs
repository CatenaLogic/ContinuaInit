// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VersionParser.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2018 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit
{
    using System;
    using Semver;

    public static class VersionParser
    {
        public static SemVersion Parse(string input)
        {
            input = input.Replace("unstable", "alpha")
                         .Replace("Unstable", "Alpha")
                         .Replace("UNSTABLE", "ALPHA");

            var version = SemVersion.Parse(input);

            var stuffToSplit = version.Prerelease;
            if (!string.IsNullOrWhiteSpace(stuffToSplit))
            {
                var prerelease = string.Empty;
                var build = string.Empty;

                var splitted = stuffToSplit.Split(new string[] { ".", "-" }, StringSplitOptions.RemoveEmptyEntries);
                prerelease = splitted[0];

                if (splitted.Length > 1)
                {
                    build = splitted[1];
                }

                version = version.Change(prerelease: prerelease, build: build);
            }

            return version;
        }
    }
}