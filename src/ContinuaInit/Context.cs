// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Context.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit
{
    using Catel.Logging;
    using Semver;

    public class Context
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public Context()
        {
        }

        public bool IsHelp { get; set; }
        public string LogFile { get; set; }

        public string BranchName { get; set; }
        public SemVersion Version { get; set; }
        public bool IsCi { get; set; }

        public void ValidateContext()
        {
            if (string.IsNullOrEmpty(BranchName))
            {
                throw Log.ErrorAndCreateException<ContinuaInitException>("Branch name is missing");
            }

            if (Version == null)
            {
                throw Log.ErrorAndCreateException<ContinuaInitException>("Version name is missing");
            }
        }
    }
}