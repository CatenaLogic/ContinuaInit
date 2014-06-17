// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContextFacts.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2014 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Test
{
    using Catel.Test;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class ContextFacts
    {
        [TestClass]
        public class TheValidateContextMethod
        {
            [TestMethod]
            public void ThrowsExceptionForMissingSolutionDirectory()
            {
                var context = new Context();

                ExceptionTester.CallMethodAndExpectException<ContinuaInitException>(() => context.ValidateContext());
            }

            [TestMethod]
            public void ThrowsExceptionForMissingBranchName()
            {
                var context = new Context
                {
                    Version = "1.0"
                };

                ExceptionTester.CallMethodAndExpectException<ContinuaInitException>(() => context.ValidateContext());
            }

            [TestMethod]
            public void ThrowsExceptionForMissingVersion()
            {
                var context = new Context
                {
                    BranchName = "master"
                };

                ExceptionTester.CallMethodAndExpectException<ContinuaInitException>(() => context.ValidateContext());
            }

            [TestMethod]
            public void SucceedsForValidContext()
            {
                var context = new Context
                {
                    BranchName = "master",
                    Version = "1.0"
                };

                // should not throw
                context.ValidateContext();
            }
        }
    }
}