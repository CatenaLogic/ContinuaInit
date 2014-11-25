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
    public class IsNightlyBuildRuleFacts
    {
        [TestCase]
        public void ReturnsFalseForMasterBranch()
        {
            var context = new Context
            {
                BranchName = "master"
            };
            var rule = new IsNightlyBuildRule();

            Assert.AreEqual("False", rule.GetParameter(context).Value);
        }

        [TestCase]
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