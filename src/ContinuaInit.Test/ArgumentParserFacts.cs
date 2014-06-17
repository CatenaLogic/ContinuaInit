// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentParserFacts.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Test
{
    using Catel.Test;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ArgumentParserFacts
    {
        [TestMethod]
        public void ThrowsExceptionForEmptyParameters()
        {
            ExceptionTester.CallMethodAndExpectException<ContinuaInitException>(() => ArgumentParser.ParseArguments(string.Empty));
        }

        [TestMethod]
        public void CorrectlyParsesBranchName()
        {
            var context = ArgumentParser.ParseArguments("-b develop");

            Assert.AreEqual("develop", context.BranchName);
        }

        [TestMethod]
        public void CorrectlyParsesVersion()
        {
            var context = ArgumentParser.ParseArguments("-v 1.0.0-unstable001");

            Assert.AreEqual("1.0.0-unstable001", context.Version);
        }

        [TestMethod]
        public void CorrectlyParsesCi()
        {
            var context = ArgumentParser.ParseArguments("-ci true");

            Assert.IsTrue(context.IsCi);
        }

        [TestMethod]
        public void CorrectlyParsesLogFilePath()
        {
            var context = ArgumentParser.ParseArguments("-l logFilePath");

            Assert.AreEqual("logFilePath", context.LogFile);
        }

        [TestMethod]
        public void CorrectlyParsesHelp()
        {
            var context = ArgumentParser.ParseArguments("-h");

            Assert.IsTrue(context.IsHelp);
        }

        [TestMethod]
        public void CorrectlyParsesBranchNameAndVersion()
        {
            var context = ArgumentParser.ParseArguments("-b develop -v 1.0.0-unstable001");

            Assert.AreEqual("develop", context.BranchName);
            Assert.AreEqual("1.0.0-unstable001", context.Version);
        }

        [TestMethod]
        public void ThrowsExceptionForInvalidNumberOfArguments()
        {
            ExceptionTester.CallMethodAndExpectException<ContinuaInitException>(() => ArgumentParser.ParseArguments("-l logFilePath extraArg"));
        }

        [TestMethod]
        public void ThrowsExceptionForUnknownArgument()
        {
            ExceptionTester.CallMethodAndExpectException<ContinuaInitException>(() => ArgumentParser.ParseArguments("solutionDirectory -x logFilePath"));
        }
    }
}