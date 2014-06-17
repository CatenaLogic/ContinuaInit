﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IsNightlyBuildRuleFacts.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Test.Rules
{
    using ContinuaInit.Rules;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DisplayVersionRuleFacts
    {
        [TestMethod]
        public void ReturnsFalseForMasterBranch()
        {
            var context = new Context
            {
                BranchName = "master",
                Version = "1.0.0"
            };
            var rule = new DisplayVersionRule();

            Assert.AreEqual("1.0.0", rule.GetParameter(context).Value);
        }

        [TestMethod]
        public void ReturnsTrueForDevelopBranch()
        {
            var context = new Context
            {
                BranchName = "develop",
                Version = "1.0.0-unstable0001"
            };
            var rule = new DisplayVersionRule();

            Assert.AreEqual("1.0.0-unstable0001 nightly", rule.GetParameter(context).Value);
        }

        [TestMethod]
        public void ReturnsTrueForDevelopBranchWithCiBuild()
        {
            var context = new Context
            {
                BranchName = "develop",
                Version = "1.0.0-unstable0001",
                IsCi = true
            };
            var rule = new DisplayVersionRule();

            Assert.AreEqual("1.0.0-unstable0001 ci", rule.GetParameter(context).Value);
        }
    }
}