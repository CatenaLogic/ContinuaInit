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
    public class NuGetVersionRuleFacts
    {
        [TestCase("2.1.0", "2.1.0")]
        [TestCase("2.1.0-unstable.2", "2.1.0-alpha0002")]
        [TestCase("2.1.0-alpha.2", "2.1.0-alpha0002")]
        [TestCase("2.1.0-beta.2", "2.1.0-beta0002")]
        public void ReturnsVersion(string input, string expectedOutput)
        {
            var context = new Context
            {
                BranchName = "master",
                Version = VersionParser.Parse(input)
            };

            var rule = new NuGetVersionRule();
            var actualOutput = rule.GetParameter(context).Value;

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}