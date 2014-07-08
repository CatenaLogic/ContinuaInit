// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContinuaCiTests.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Test.Integration
{
    using ContinuaInit.Integration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;

    [TestClass]
    public class ContinuaCiFacts
    {
        [TestMethod]
        public void GenerateBuildVersion()
        {
            var versionBuilder = new ContinuaCi();

            var continuaCiCommand = versionBuilder.GenerateSetParameterMessage(new Parameter("Var1", true));
            Assert.AreEqual("@@continua[setVariable name='Var1' value='True' skipIfNotDefined='true']", continuaCiCommand);
        }
    }
}