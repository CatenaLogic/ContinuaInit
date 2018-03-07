// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NuGetVersionRule.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2018 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Rules
{
    using System;
    using Models;

    public class FullSemVerVersionRule : RuleBase
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
                value += $".{version.Build}";
            }

            var parameter = new Parameter("GitVersion_FullSemVer", value);
            return parameter;
        }
    }
}