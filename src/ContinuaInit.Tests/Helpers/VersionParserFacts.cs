// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VersionParserFacts.cs" company="CatenaLogic">
//   Copyright (c) 2014 - 2018 CatenaLogic. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace ContinuaInit.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class VersionParserFacts
    {
        [TestCase("1.0.0", "", "")]
        [TestCase("1.0.0-unstable.4", "alpha", "4")]
        public void ParsesVersionCorrectly(string input, string expectedPrerelease, string expectedBuild)
        {
            var version = VersionParser.Parse(input);

            var output = version.ToString();

            Assert.AreEqual(expectedPrerelease, version.Prerelease);
            Assert.AreEqual(expectedBuild, version.Build);
        }
    }
}