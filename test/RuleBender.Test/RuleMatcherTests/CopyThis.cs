//-----------------------------------------------------------------------
// <copyright file="CopyThis.cs" company="ImprovingEnterprises">
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
    public class CopyThis
    {
        #region [ Fields ]

        private DateOfMonthMatcher matcher;

        #endregion

        [SetUp]
        public void SetUp()
        {
            this.matcher = new DateOfMonthMatcher();
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
