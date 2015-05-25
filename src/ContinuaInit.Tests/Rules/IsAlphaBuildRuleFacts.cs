// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IsNightlyBuildRuleFacts.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Test.Rules
{
    using ContinuaInit.Rules;
    using NUnit.Framework;

    [TestFixture]
    public class IsAlphaBuildRuleFacts
    {
        [TestCase("2.0.0-unstable.493", true)]
        [TestCase("2.0.0-beta.493", false)]
        [TestCase("2.0.0", false)]
        public void ReturnsRightValue(string versionInput, bool expectedOutput)
        {
            var context = new Context
            {
                Version = versionInput
            };

            var rule = new IsAlphaBuildRule();

            Assert.AreEqual(expectedOutput.ToString().ToLower(), rule.GetParameter(context).Value.ToLower());
        }
    }
}