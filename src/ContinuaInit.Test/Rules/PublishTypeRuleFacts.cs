// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IsNightlyBuildRuleFacts.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Test.Rules
{
    using ContinuaInit.Rules;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PublishTypeRuleFacts
    {
        [TestMethod]
        public void ReturnsOfficialForMasterBranch()
        {
            var context = new Context
            {
                BranchName = "master"
            };
            var rule = new PublishTypeRule();

            Assert.AreEqual("Official", rule.GetParameter(context).Value);
        }

        [TestMethod]
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