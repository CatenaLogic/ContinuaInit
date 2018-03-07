// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NuGetVersionRule.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2018 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Rules
{
    using System;
    using Models;

    public class NuGetVersionRule : RuleBase
    {
        public override Parameter GetParameter(Context context)
        {
            var version = context.Version;

            var realVersion = new Version(version.Major, version.Minor, version.Patch);
            var value = realVersion.ToString(3);

            if (!string.IsNullOrWhiteSpace(version.Prerelease))
            {
                value += $"-{version.Prerelease}";
            }

            if (!string.IsNullOrWhiteSpace(version.Build))
            {
                var buildVersion = version.Build;

                while (buildVersion.Length < 4)
                {
                    buildVersion = $"0{buildVersion}";
                }

                value += buildVersion;
            }

            var parameter = new Parameter("GitVersion_NuGetVersion", value);
            return parameter;
        }
    }
}