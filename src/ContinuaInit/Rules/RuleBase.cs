// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InitRule.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Rules
{
    using Models;

    public abstract class RuleBase
    {
        public abstract Parameter GetParameter(Context context);
    }
}