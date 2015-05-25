// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IsOfficialBuildRule.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Rules
{
    using Models;

    public class IsAlphaBuildRule : RuleBase
    {
        public override Parameter GetParameter(Context context)
        {
            return new Parameter("IsAlphaBuild", context.Version.IsAlpha());
        }
    }
}