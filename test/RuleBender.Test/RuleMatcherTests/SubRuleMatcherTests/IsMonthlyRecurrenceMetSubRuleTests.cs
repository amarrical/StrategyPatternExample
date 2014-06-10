//-----------------------------------------------------------------------
// <copyright file="IsMonthlyRecurrenceMetSubRuleTests.cs" company="ImprovingEnterprises">
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
    public class IsMonthlyRecurrenceMetSubRuleTests
    {
        #region [ Fields ]

        private IsMonthlyRecurrenceMetSubMatcher subMatcher;

        #endregion

        [SetUp]
        public void SetUp()
        {
            this.subMatcher = new IsMonthlyRecurrenceMetSubMatcher();
        }

        #region [ Tests ]

        [Test]
        public void ShouldBeRunReturnsFalseIfStillInSameMonthAsLastSent()
        {
            // Assemble
            var lastSent = new DateTime(2014, 6, 1);
            var mailRule = new MailRule { LastSent = lastSent, NumberOf = 1 };
            var startTime = new DateTime(2014, 6, 23);

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeRunReturnsFalseIfNumberOfExceedsMonthsSeperatingLastSentAndStartTime()
        {
            // Assemble
            var lastSent = new DateTime(2014, 6, 1);
            var mailRule = new MailRule { LastSent = lastSent, NumberOf = 2 };
            var startTime = new DateTime(2014, 7, 23);

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeRunReturnsTrueIfStartTimeMonthIsGreaterThanNumberOfMonthsSinceStartTime()
        {
            // Assemble
            var lastSent = new DateTime(2014, 6, 1);
            var mailRule = new MailRule { LastSent = lastSent, NumberOf = 1 };
            var startTime = new DateTime(2014, 8, 23);

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldBeRunReturnsTrueIfStartTimeMonthIsEqualToNumberOfMonthsSinceStartTime()
        {
            // Assemble
            var lastSent = new DateTime(2014, 6, 23);
            var mailRule = new MailRule { LastSent = lastSent, NumberOf = 1 };
            var startTime = new DateTime(2014, 7, 1);

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsTrue(result);
        }

        #endregion
    }
}
