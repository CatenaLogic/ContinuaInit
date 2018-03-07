// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContinuaCi.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Integration
{
    using Catel;
    using Microsoft.Win32;
    using Models;

    public class ContinuaCi : BuildServerBase
    {
        public override bool CanApplyToCurrentContext()
        {
#if DEBUG
            return true;
#endif

            const string KeyName = @"Software\VSoft Technologies\Continua CI Agent";

            if (RegistryKeyExists(KeyName, RegistryView.Registry32))
            {
                return true;
            }

            if (RegistryKeyExists(KeyName, RegistryView.Registry64))
            {
                return true;
            }

            return false;
        }

        public override string GenerateSetVersionMessage(BuildVersionParameter parameter)
        {
            Argument.IsNotNull(() => parameter);

            return string.Format("@@continua[setBuildVersion value='{0}']", parameter.Value);
        }

        public override string GenerateSetParameterMessage(Parameter parameter)
        {
            Argument.IsNotNull(() => parameter);

            return string.Format("@@continua[setVariable name='{0}' value='{1}' skipIfNotDefined='true']", parameter.Name, parameter.Value);
        }

        private static bool RegistryKeyExists(string keyName, RegistryView registryView)
        {
            var localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView);
            localKey = localKey.OpenSubKey(keyName);

            return localKey != null;
        }
    }
}