//-----------------------------------------------------------------------
// <copyright file="IsWeekOfMonthSubMatcherTests.cs" company="ImprovingEnterprises">
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
    public class IsWeekOfMonthSubMatcherTests
    {
        // ToDo: Rework this as example.
        #region [ Fields ]

        private IsWeekOfMonthSubMatcher subMatcher;

        #endregion

        [SetUp]
        public void SetUp()
        {
            this.subMatcher = new IsWeekOfMonthSubMatcher();
        }

        #region [ Tests ]

        [Test]
        public void ShouldBeRunReturnsFalseInFirstWeekOfMonthWhenDayNumberIsTwo()
        {
            // Assemble
            var startTime = new DateTime(2014, 6, 1);
            var mailRule = new MailRule { DayNumber = 2 };

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeRunReturnsTrueInSecondWeekOfMonthWhenDayNumberIsTwo()
        {
            // Assemble
            var startTime = new DateTime(2014, 6, 8);
            var mailRule = new MailRule { DayNumber = 2 };

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldBeRunReturnsFalseInThirdWeekOfMonthWhenDayNumberIsTwo()
        {
            // Assemble
            var startTime = new DateTime(2014, 6, 15);
            var mailRule = new MailRule { DayNumber = 2 };

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeRunReturnsFalseInFourthWeekOfMonthWhenDayNumberIsTwo()
        {
            // Assemble
            var startTime = new DateTime(2014, 6, 22);
            var mailRule = new MailRule { DayNumber = 2 };

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeRunReturnsFalseInFifthWeekOfMonthWhenDayNumberIsTwo()
        {
            // Assemble
            var startTime = new DateTime(2014, 6, 29);
            var mailRule = new MailRule { DayNumber = 2 };

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        [Ignore("This test is for after fix")]
        public void ShouldBeRunReturnsFalseOnFirstMondayOfMonthWhenDayNumberIsTwo()
        {
            // Assemble
            var startTime = new DateTime(2014, 7, 7);
            var mailRule = new MailRule { DayNumber = 2 };

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        [Ignore("This test is for after fix")]
        public void ShouldBeRunReturnsTrueOnSecondMondayOfMonthWhenDayNumberIsTwo()
        {
            // Assemble
            var startTime = new DateTime(2014, 7, 14);
            var mailRule = new MailRule { DayNumber = 2 };

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        [Ignore("This test is for after fix")]
        public void ShouldBeRunReturnsFalseOnThirdMondayOfMonthWhenDayNumberIsTwo()
        {
            // Assemble
            var startTime = new DateTime(2014, 7, 21);
            var mailRule = new MailRule { DayNumber = 2 };

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion
    }
}
