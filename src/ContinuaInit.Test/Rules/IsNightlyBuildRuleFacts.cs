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
    public class IsNightlyBuildRuleFacts
    {
        [TestMethod]
        public void ReturnsFalseForMasterBranch()
        {
            var context = new Context
            {
                BranchName = "master"
            };
            var rule = new IsNightlyBuildRule();

            Assert.AreEqual("False", rule.GetParameter(context).Value);
        }

        [TestMethod]
        public void ReturnsTrueForDevelopBranch()
        {
            var context = new Context
            {
                BranchName = "develop"
            };
            var rule = new IsNightlyBuildRule();

            Assert.AreEqual("True", rule.GetParameter(context).Value);
        }
    }
}