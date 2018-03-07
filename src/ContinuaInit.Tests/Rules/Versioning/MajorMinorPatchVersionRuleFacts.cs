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
    public class MajorMinorPatchVersionRuleFacts
    {
        [TestCase("2.1.0", "2.1.0")]
        [TestCase("2.1.0-unstable.2", "2.1.0")]
        [TestCase("2.1.0-alpha.2", "2.1.0")]
        [TestCase("2.1.0-beta.2", "2.1.0")]
        public void ReturnsVersion(string input, string expectedOutput)
        {
            var context = new Context
            {
                BranchName = "master",
                Version = VersionParser.Parse(input)
            };

            var rule = new MajorMinorPatchVersionRule();
            var actualOutput = rule.GetParameter(context).Value;

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}