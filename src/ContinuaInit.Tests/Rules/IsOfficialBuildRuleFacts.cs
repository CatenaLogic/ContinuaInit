﻿// --------------------------------------------------------------------------------------------------------------------
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
    public class IsOfficialBuildRuleFacts
    {
        [TestCase("2.0.0-unstable.493", false)]
        [TestCase("2.0.0-beta.493", false)]
        [TestCase("2.0.0", true)]
        public void ReturnsRightValue(string versionInput, bool expectedOutput)
        {
            var context = new Context
            {
                Version = VersionParser.Parse(versionInput)
            };

            var rule = new IsOfficialBuildRule();

            var expected = expectedOutput.ToString().ToLower();
            var actual = rule.GetParameter(context).Value.ToLower();

            Assert.AreEqual(expected, actual);
        }
    }
}