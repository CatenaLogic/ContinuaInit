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
    public class PublishTypeRuleFacts
    {
        [TestCase("2.0.0-unstable.493", "Alpha")]
        [TestCase("2.0.0-beta.493", "Beta")]
        [TestCase("2.0.0", "Official")]
        public void ReturnsRightValue(string versionInput, string expectedOutput)
        {
            var context = new Context
            {
                Version = versionInput
            };

            var rule = new PublishTypeRule();

            Assert.AreEqual(expectedOutput.ToLower(), rule.GetParameter(context).Value.ToLower());
        }
    }
}