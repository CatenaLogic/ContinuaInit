// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DisplayVersion.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Rules
{
    using Models;

    public class DisplayVersionRule : RuleBase
    {
        public override Parameter GetParameter(Context context)
        {
            var parameter = new Parameter("DisplayVersion", context.Version);

            if (context.IsCi)
            {
                parameter.Value += " ci";
            }
            //else if (!context.BranchName.IsOfficial())
            //{
            //    parameter.Value += " nightly";
            //}

            return parameter;
        }
    }
}