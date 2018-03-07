// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IsOfficialBuildRule.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Rules
{
    using Models;

    public class IsOfficialBuildRule : RuleBase
    {
        public override IParameter GetParameter(Context context)
        {
            return new Parameter("IsOfficialBuild", context.Version.IsOfficial());
        }
    }
}