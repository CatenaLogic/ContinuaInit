// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentParserFacts.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Test
{
    using Catel.Test;
    using NUnit.Framework;

    [TestFixture]
    public class ArgumentParserFacts
    {
        [TestCase]
        public void ThrowsExceptionForEmptyParameters()
        {
            ExceptionTester.CallMethodAndExpectException<ContinuaInitException>(() => ArgumentParser.ParseArguments(string.Empty));
        }

        [TestCase]
        public void CorrectlyParsesBranchName()
        {
            var context = ArgumentParser.ParseArguments("-b develop");

            Assert.AreEqual("develop", context.BranchName);
        }

        [TestCase]
        public void CorrectlyParsesVersion()
        {
            var context = ArgumentParser.ParseArguments("-v 1.0.0-unstable001");

            Assert.AreEqual("1.0.0-alpha001", context.Version.ToString());
        }

        [TestCase]
        public void CorrectlyParsesCi()
        {
            var context = ArgumentParser.ParseArguments("-ci true");

            Assert.IsTrue(context.IsCi);
        }

        [TestCase]
        public void CorrectlyParsesLogFilePath()
        {
            var context = ArgumentParser.ParseArguments("-l logFilePath");

            Assert.AreEqual("logFilePath", context.LogFile);
        }

        [TestCase]
        public void CorrectlyParsesHelp()
        {
            var context = ArgumentParser.ParseArguments("-h");

            Assert.IsTrue(context.IsHelp);
        }

        [TestCase]
        public void CorrectlyParsesBranchNameAndVersion()
        {
            var context = ArgumentParser.ParseArguments("-b develop -v 1.0.0-unstable001");

            Assert.AreEqual("develop", context.BranchName);
            Assert.AreEqual("1.0.0-alpha001", context.Version.ToString());
        }

        [TestCase]
        public void ThrowsExceptionForInvalidNumberOfArguments()
        {
            ExceptionTester.CallMethodAndExpectException<ContinuaInitException>(() => ArgumentParser.ParseArguments("-l logFilePath extraArg"));
        }

        [TestCase]
        public void ThrowsExceptionForUnknownArgument()
        {
            ExceptionTester.CallMethodAndExpectException<ContinuaInitException>(() => ArgumentParser.ParseArguments("solutionDirectory -x logFilePath"));
        }
    }
}