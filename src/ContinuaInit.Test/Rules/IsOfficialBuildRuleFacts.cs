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
    public class IsOfficialBuildRuleFacts
    {
        [TestMethod]
        public void ReturnsTrueForMasterBranch()
        {
            var context = new Context
            {
                BranchName = "master"
            };
            var rule = new IsOfficialBuildRule();

            Assert.AreEqual("True", rule.GetParameter(context).Value);
        }

        [TestMethod]
        public void ReturnsFalseForDevelopBranch()
        {
            var context = new Context
            {
                BranchName = "develop"
            };
            var rule = new IsOfficialBuildRule();

            Assert.AreEqual("False", rule.GetParameter(context).Value);
        }
    }
}