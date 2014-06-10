//-----------------------------------------------------------------------
// <copyright file="IsDayOfWeekSubMatcherTests.cs" company="ImprovingEnterprises">
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
    public class IsDayOfWeekSubMatcherTests
    {
        #region [ Fields ]

        private IsDayOfWeekSubMatcher subMatcher;

        #endregion

        [SetUp]
        public void SetUp()
        {
            this.subMatcher = new IsDayOfWeekSubMatcher();
        }

        #region [ Tests ]

        [Test]
        public void ShouldBeRunReturnsTrueIfStartTimeDayOfWeekIsInMailRuleDaysOfWeek()
        {
            // Assemble
            var mailRule = new MailRule();
            mailRule.DaysOfWeek.Add(DayOfWeek.Monday, true);
            mailRule.DaysOfWeek.Add(DayOfWeek.Friday, true);
            var startTime = new DateTime(2014, 6, 23);

            Assert.IsTrue(startTime.DayOfWeek == DayOfWeek.Monday, "Test is not valid");

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldBeRunReturnsFalseIfStartTimeDayOfWeekDoesNotExistInMailRule()
        {
            // Assemble
            var mailRule = new MailRule();
            mailRule.DaysOfWeek.Add(DayOfWeek.Monday, true);
            mailRule.DaysOfWeek.Add(DayOfWeek.Friday, true);
            var startTime = new DateTime(2014, 6, 24);

            Assert.IsTrue(startTime.DayOfWeek == DayOfWeek.Tuesday, "Test is not valid");

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeRunReturnsFalseIfStartTimeDayOfWeekIsListedAsDoNotRunInMailRule()
        {
            // Assemble
            var mailRule = new MailRule();
            mailRule.DaysOfWeek.Add(DayOfWeek.Tuesday, false);
            mailRule.DaysOfWeek.Add(DayOfWeek.Friday, true);
            var startTime = new DateTime(2014, 6, 24);

            Assert.IsTrue(startTime.DayOfWeek == DayOfWeek.Tuesday, "Test is not valid");

            // Act
            var result = this.subMatcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion
    }
}
