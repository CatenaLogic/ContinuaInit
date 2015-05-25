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

            parameter.Value = "Unknown";

            if (context.Version.IsOfficial())
            {
                parameter.Value = "Official";
            }
            else if (context.Version.IsBeta())
            {
                parameter.Value = "Beta";
            }
            else if (context.Version.IsAlpha())
            {
                parameter.Value = "Alpha";
            }
            
            return parameter;
        }
    }
}