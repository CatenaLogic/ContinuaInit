// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IsNightlyBuildRule.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Rules
{
    using Models;

    public class PublishTypeRule : RuleBase
    {
        public override Parameter GetParameter(Context context)
        {
            var parameter = new Parameter("PublishType");

            parameter.Value = context.BranchName.IsMaster() ? "Official" : "Nightly";

            return parameter;
        }
    }
}