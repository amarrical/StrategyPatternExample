//-----------------------------------------------------------------------
// <copyright file="DateOfMonthMatcherTests.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.Test.RuleMatcherTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using NUnit.Framework;

    using RuleBender.Entity;
    using RuleBender.RuleParsers.RuleMatchers;
    using RuleBender.RuleParsers.RuleMatchers.SubMatchers;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules",
        "SA1600:ElementsMustBeDocumented",
        Justification = "Tests are self documenting")]
    [TestFixture]
    public class DateOfMonthMatcherTests
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
            var subMatchers = this.matcher.SubMatchers;

            // Assert
            Assert.IsTrue(subMatchers.Count == 2);
            Assert.IsTrue(subMatchers.Count(sm => sm.GetType() == typeof(IsDayOfMonthSubMatcher))           == 1);
            Assert.IsTrue(subMatchers.Count(sm => sm.GetType() == typeof(IsMonthlyRecurrenceMetSubMatcher)) == 1);
        }

        #region [ IsProperMatcher ]

        [Test]
        public void IsProperHandlerReturnsTrueIfRuleMailPatternIsMonthlyAndRuleDayNumberHasValueAndRuleIsNotDayOfWeekRestricted()
        {
            // Assemble
            var mailRule = new MailRule
                           {
                               MailPattern  = MailPattern.Montly,
                               DayNumber    = 4,
                               DaysOfWeek   = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Monday, false } }
                           };

            // Act
            var result = this.matcher.IsProperMatcher(mailRule);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsProperMatcherReturnsFalseIfRuleIsDayOfWeekRestricted()
        {
            // Assemble
            var mailRule = new MailRule
                           {
                               MailPattern  = MailPattern.Montly,
                               DayNumber    = 4,
                               DaysOfWeek   = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Monday, true } }
                           };

            Assert.IsTrue(mailRule.IsDayOfWeekRestricted, "Test is not properly configured");

            // Act
            var result = this.matcher.IsProperMatcher(mailRule);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsProperMatcherReturnsFalseIfRuleDayNumberDoesNotHaveValue()
        {
            // Assemble
            var mailRule = new MailRule
                           {
                               MailPattern  = MailPattern.Montly,
                               DayNumber    = null,
                               DaysOfWeek   = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Monday, false } }
                           };

            Assert.IsFalse(mailRule.DayNumber.HasValue, "Test is not properly configured");

            // Act
            var result = this.matcher.IsProperMatcher(mailRule);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        [TestCase(MailPattern.Daily)]
        [TestCase(MailPattern.Weekly)]
        [TestCase(MailPattern.Yearly)]
        public void IsProperMatcherReturnsFalseIfRuleMailPatternIsNotMonthly(MailPattern mailPattern)
        {
            // Assemble
            var mailRule = new MailRule
                           {
                               MailPattern  = mailPattern,
                               DayNumber    = 4,
                               DaysOfWeek   = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Monday, false } }
                           };

            // Act
            var result = this.matcher.IsProperMatcher(mailRule);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region [ ShouldBeRun ]

        [Test]
        public void ShouldBeRunReturnsFalseIfDayOfMonthIsMetButRecurrenceIsNot()
        {
            // Assemble
            var startTime   = new DateTime(2014, 6, 14);
            var mailRule    = new MailRule
                                  {
                                      MailPattern  = MailPattern.Montly,
                                      DayNumber    = 14,
                                      LastSent     = new DateTime(2014, 5, 14),
                                      NumberOf     = 2
                                  };

            // Act
            var result = this.matcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeRunReturnsFalseIfRecurrenceIsMetButDayOfMonthIsNot()
        {
            // Assemble
            var startTime   = new DateTime(2014, 7, 10);
            var mailRule    = new MailRule
                                  {
                                      MailPattern  = MailPattern.Montly,
                                      DayNumber    = 14,
                                      LastSent     = new DateTime(2014, 5, 14),
                                      NumberOf     = 2
                                  };

            // Act
            var result = this.matcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeRunReturnsTrueIfRecurrenceAndDayOfMonthIsMet()
        {
            // Assemble
            var startTime   = new DateTime(2014, 7, 14);
            var mailRule    = new MailRule
                                  {
                                      MailPattern  = MailPattern.Montly,
                                      DayNumber    = 14,
                                      LastSent     = new DateTime(2014, 5, 14),
                                      NumberOf     = 2
                                  };

            // Act
            var result = this.matcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsTrue(result);
        }

        #endregion

        #endregion
    }
}
