// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBuildServer.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Integration
{
    using System;
    using System.Collections.Generic;
    using Models;

    public interface IBuildServer
    {
        bool CanApplyToCurrentContext();

        void WriteIntegration(IEnumerable<IParameter> parameters, Action<string> writer);
    }
}