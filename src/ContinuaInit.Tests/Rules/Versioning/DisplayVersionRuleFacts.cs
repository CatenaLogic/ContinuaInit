// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IsNightlyBuildRuleFacts.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Test.Rules
{
    using ContinuaInit.Rules;
    using NUnit.Framework;
    using Semver;

    [TestFixture]
    public class DisplayVersionRuleFacts
    {
        [TestCase]
        public void ReturnsFalseForMasterBranch()
        {
            var context = new Context
            {
                BranchName = "master",
                Version = VersionParser.Parse("1.0.0")
            };
            var rule = new DisplayVersionRule();

            Assert.AreEqual("1.0.0", rule.GetParameter(context).Value);
        }

        [TestCase]
        public void ReturnsTrueForDevelopBranch()
        {
            var context = new Context
            {
                BranchName = "develop",
                Version = VersionParser.Parse("1.0.0-unstable0001")
            };
            var rule = new DisplayVersionRule();

            Assert.AreEqual("1.0.0-alpha0001", rule.GetParameter(context).Value);
        }

        [TestCase]
        public void ReturnsTrueForDevelopBranchWithCiBuild()
        {
            var context = new Context
            {
                BranchName = "develop",
                Version = VersionParser.Parse("1.0.0-unstable0001"),
                IsCi = true
            };
            var rule = new DisplayVersionRule();

            Assert.AreEqual("1.0.0-alpha0001 ci", rule.GetParameter(context).Value);
        }
    }
}