//-----------------------------------------------------------------------
// <copyright file="IsDayOfMonthSubMatcherTests.cs" company="ImprovingEnterprises">
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
    public class IsDayOfMonthSubMatcherTests
    {
        #region [ Fields ]

        private IsDayOfMonthSubMatcher subMatcher;

        #endregion

        [SetUp]
        public void SetUp()
        {
            this.subMatcher = new IsDayOfMonthSubMatcher();
        }

        #region [ Tests ]

        [Test]
        public void ShouldBeRunReturnsFalseIfDayNumberIsGreaterThanStartTimeDay()
        {
            // Assemble
            const int DayNumber = 16;
            var mailRule = new MailRule { DayNumber = DayNumber };
            var startTime = new DateTime(2014, 6, 15);
            Assert.IsTrue(DayNumber > startTime.Day);

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeRunReturnsTrueIfDayNumberIsEqualToStartTimeDay()
        {
            // Assemble
            const int DayNumber = 15;
            var mailRule = new MailRule { DayNumber = DayNumber };
            var startTime = new DateTime(2014, 6, 15);
            Assert.IsTrue(DayNumber == startTime.Day);

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldBeRunReturnsFalseIfDayNumberIsLessThanStartTimeDay()
        {
            // Assemble
            const int DayNumber = 14;
            var mailRule = new MailRule { DayNumber = DayNumber };
            var startTime = new DateTime(2014, 6, 15);
            Assert.IsTrue(DayNumber < startTime.Day);

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion
    }
}
