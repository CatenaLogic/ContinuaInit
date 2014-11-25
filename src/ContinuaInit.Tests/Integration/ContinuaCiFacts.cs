// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContinuaCiTests.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Test.Integration
{
    using ContinuaInit.Integration;
    using Models;
    using NUnit.Framework;

    [TestFixture]
    public class ContinuaCiFacts
    {
        [TestCase]
        public void GenerateBuildVersion()
        {
            var versionBuilder = new ContinuaCi();

            var continuaCiCommand = versionBuilder.GenerateSetParameterMessage(new Parameter("Var1", true));
            Assert.AreEqual("@@continua[setVariable name='Var1' value='True' skipIfNotDefined='true']", continuaCiCommand);
        }
    }
}