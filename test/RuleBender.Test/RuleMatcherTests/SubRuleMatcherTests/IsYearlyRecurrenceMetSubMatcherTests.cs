//-----------------------------------------------------------------------
// <copyright file="IsYearlyRecurrenceMetSubMatcherTests.cs" company="ImprovingEnterprises">
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
    public class IsYearlyRecurrenceMetSubMatcherTests
    {
        #region [ Fields ]

        private IsYearlyRecurrenceMetSubMatcher subMatcher;

        #endregion

        [SetUp]
        public void SetUp()
        {
            this.subMatcher = new IsYearlyRecurrenceMetSubMatcher();
        }

        #region [ Tests ]

        [Test]
        public void ShouldBeRunReturnsFalseIfStillInSameYearAsLastSent()
        {
            // Assemble
            var lastSent = new DateTime(2014, 6, 1);
            var mailRule = new MailRule { LastSent = lastSent, NumberOf = 2 };
            var startTime = new DateTime(2014, 12, 15);

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeRunReturnsFalseIfNumberOfExceedsYearsSeperatingLastSentAndStartTime()
        {
            // Assemble
            var lastSent = new DateTime(2014, 6, 1);
            var mailRule = new MailRule { LastSent = lastSent, NumberOf = 2 };
            var startTime = new DateTime(2015, 12, 15);

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeRunReturnsTrueIfStartTimeYearIsGreaterThanNumberOfYearsSinceStartTime()
        {
            // Assemble
            var lastSent = new DateTime(2014, 6, 1);
            var mailRule = new MailRule { LastSent = lastSent, NumberOf = 2 };
            var startTime = new DateTime(2018, 12, 15);

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldBeRunReturnsTrueIfStartTimeYearIsEqualToNumberOfYearsSinceStartTime()
        {
            // Assemble
            var lastSent = new DateTime(2014, 6, 1);
            var mailRule = new MailRule { LastSent = lastSent, NumberOf = 2 };
            var startTime = new DateTime(2016, 1, 4);

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsTrue(result);
        }

        #endregion
    }
}
