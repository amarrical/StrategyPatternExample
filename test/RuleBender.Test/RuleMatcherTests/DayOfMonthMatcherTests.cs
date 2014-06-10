//-----------------------------------------------------------------------
// <copyright file="DayOfMonthMatcherTests.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.Test.RuleMatcherTests
{
    using System.Diagnostics.CodeAnalysis;

    using NUnit.Framework;

    using RuleBender.RuleParsers.RuleMatchers;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules",
        "SA1600:ElementsMustBeDocumented",
        Justification = "Tests are self documenting")]
    [TestFixture]
    public class DayOfMonthMatcherTests
    {
        #region [ Fields ]

        private DayOfMonthMatcher matcher;

        #endregion

        [SetUp]
        public void SetUp()
        {
            this.matcher = new DayOfMonthMatcher();
        }

        #region [ Tests ]

        [Test]
        public void MatcherContainsProperSubRules()
        {
            // Assemble

            // Act

            // Assert
        }

        #region [ IsProperMatcher ]



        #endregion

        #region [ ShouldBeRun ]



        #endregion

        #endregion
    }
}
