//-----------------------------------------------------------------------
// <copyright file="IsMonthOfYearSubMatcherTests.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.Test.RuleMatcherTests.SubRuleMatcherTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using NUnit.Framework;

    using RuleBender.Entity;
    using RuleBender.RuleParsers.RuleMatchers.SubMatchers;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules",
        "SA1600:ElementsMustBeDocumented",
        Justification = "Tests are self documenting")]
    [TestFixture]
    public class IsMonthOfYearSubMatcherTests
    {
        #region [ Fields ]

        private IsMonthOfYearSubMatcher subMatcher;

        #endregion

        [SetUp]
        public void SetUp()
        {
            this.subMatcher = new IsMonthOfYearSubMatcher();
        }

        #region [ Tests ]

        [Test]
        public void ShouldBeRunReturnsFalseIfStartTimeMonthIsLessThanMailRuleMonth()
        {
            // Assemble
            var startTime = new DateTime(2014, 6, 14);
            const int Month = 7;
            var mailRule = new MailRule { Month = Month };

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeRunReturnsTrueIfStartTimeMonthIsEqualToMailRuleMonth()
        {
            // Assemble
            var startTime = new DateTime(2014, 6, 14);
            const int Month = 6;
            var mailRule = new MailRule { Month = Month };

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldBeRunReturnsFalseIfStartTimeMonthIsGreaterThanMailRuleMonth()
        {
            // Assemble
            var startTime = new DateTime(2014, 6, 14);
            const int Month = 5;
            var mailRule = new MailRule { Month = Month };

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion
    }
}
