// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildServerList.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Integration
{
    using System;
    using System.Collections.Generic;
    using Catel.Logging;

    public static class BuildServerList
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private static List<IBuildServer> BuildServers;

        public static Func<Context, IEnumerable<IBuildServer>> Selector = context => DefaultSelector(context);

        public static void ResetSelector()
        {
            Selector = DefaultSelector;
        }

        public static IEnumerable<IBuildServer> GetApplicableBuildServers(Context context)
        {
            return Selector(context);
        }

        private static IEnumerable<IBuildServer> DefaultSelector(Context context)
        {
            if (BuildServers == null)
            {
                BuildServers = new List<IBuildServer>
                {
                    new ContinuaCi(),
                    //new TeamCity()
                };
            }

            var buildServices = new List<IBuildServer>();

            foreach (var buildServer in BuildServers)
            {
                try
                {
                    if (buildServer.CanApplyToCurrentContext())
                    {
                        Log.Info("Applicable build agent found: '{0}'.", buildServer.GetType().Name);
                        buildServices.Add(buildServer);
                    }
                }
                catch (Exception ex)
                {
                    Log.Info("Failed to check build server '{0}': {1}", buildServer.GetType().Name, ex.Message);
                }
            }

            return buildServices;
        }
    }
}