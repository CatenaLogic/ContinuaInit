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
        [TestCase]
        public void ReturnsOfficialForMasterBranch()
        {
            var context = new Context
            {
                BranchName = "master"
            };
            var rule = new PublishTypeRule();

            Assert.AreEqual("Official", rule.GetParameter(context).Value);
        }

        [TestCase]
        public void ReturnsNightlyForDevelopBranch()
        {
            var context = new Context
            {
                BranchName = "develop"
            };
            var rule = new PublishTypeRule();

            Assert.AreEqual("Nightly", rule.GetParameter(context).Value);
        }
    }
}