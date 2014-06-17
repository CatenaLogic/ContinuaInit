// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildServerBase.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Integration
{
    using System;
    using System.Collections.Generic;
    using Catel;
    using Models;

    public abstract class BuildServerBase : IBuildServer
    {
        public abstract bool CanApplyToCurrentContext();
        public abstract string GenerateSetParameterMessage(Parameter parameter);

        public virtual void WriteIntegration(IEnumerable<Parameter> parameters, Action<string> writer)
        {
            Argument.IsNotNull(() => writer);

            foreach (var buildParameter in parameters)
            {
                var parameterString = GenerateSetParameterMessage(buildParameter);
                writer(parameterString);
            }
        }
    }
}