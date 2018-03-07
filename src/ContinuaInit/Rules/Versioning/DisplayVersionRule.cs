// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DisplayVersion.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Rules
{
    using System;
    using Models;

    public class DisplayVersionRule : RuleBase
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

            if (context.IsCi)
            {
                value += " ci";
            }

            var parameter = new Parameter("DisplayVersion", value);
            return parameter;
        }
    }
}