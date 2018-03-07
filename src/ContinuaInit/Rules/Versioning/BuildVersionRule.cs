// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildVersionRule.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2018 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Rules.Versioning
{
    using System;
    using Models;

    public class BuildVersionRule : RuleBase
    {
        public override IParameter GetParameter(Context context)
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

            if (context.IsCi)
            {
                value += " ci";
            }

            var parameter = new BuildVersionParameter(value);
            return parameter;
        }
    }
}