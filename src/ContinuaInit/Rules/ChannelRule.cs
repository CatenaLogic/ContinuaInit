// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IsOfficialBuildRule.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Rules
{
    using Models;

    public class ChannelRule : RuleBase
    {
        public override IParameter GetParameter(Context context)
        {
            var channel = "stable";

            if (context.Version.IsAlpha())
            {
                channel = "alpha";
            }
            else if (context.Version.IsBeta())
            {
                channel = "beta";
            }

            return new Parameter("Channel", channel);
        }
    }
}
