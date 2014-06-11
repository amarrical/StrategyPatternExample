//-----------------------------------------------------------------------
// <copyright file="DayOfWeekOfMonthMatcherTests.cs" company="ImprovingEnterprises">
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
    public class DayOfWeekOfMonthMatcherTests
    {
        #region [ Fields ]

        private DayOfWeekOfMonthMatcher matcher;

        #endregion

        [SetUp]
        public void SetUp()
        {
            this.matcher = new DayOfWeekOfMonthMatcher();
        }

        #region [ Tests ]

        [Test]
        public void MatcherContainsProperSubRules()
        {
            // Assemble
            // Act
            var subMatchers = this.matcher.SubMatchers;

            // Assert
            Assert.IsTrue(subMatchers.Count == 3);
            Assert.IsTrue(subMatchers.Count(sm => sm.GetType() == typeof(IsDayOfWeekSubMatcher))            == 1);
            Assert.IsTrue(subMatchers.Count(sm => sm.GetType() == typeof(IsWeekOfMonthSubMatcher))          == 1);
            Assert.IsTrue(subMatchers.Count(sm => sm.GetType() == typeof(IsMonthlyRecurrenceMetSubMatcher)) == 1);
        }

        #region [ IsProperMatcher ]

        [Test]
        public void IsProperMatcherReturnsTrueIfRuleMailPatternIsMonthlyAndRuleDayNumberHasValueAndRuleIsDayOfWeekRestricted()
        {
            // Assemble
            var mailRule = new MailRule
                           {
                               MailPattern  = MailPattern.Montly,
                               DayNumber    = 4,
                               DaysOfWeek   = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Wednesday, true } }
                           };

            // Act
            var result = this.matcher.IsProperMatcher(mailRule);

            // Assert
            Assert.IsTrue(result);
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
                               DaysOfWeek   = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Wednesday, true } }
                           };

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
                               DaysOfWeek   = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Wednesday, true } }
                           };

            Assert.IsFalse(mailRule.DayNumber.HasValue, "Test is not properly configured");

            // Act
            var result = this.matcher.IsProperMatcher(mailRule);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsProperMatcherReturnsFalseIfRuleIsNotDayOfWeekRestricted()
        {
            // Assemble
            var mailRule = new MailRule
                           {
                               MailPattern  = MailPattern.Montly,
                               DayNumber    = 4,
                               DaysOfWeek   = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Wednesday, false } }
                           };

            Assert.IsFalse(mailRule.IsDayOfWeekRestricted, "Test is not properly configured");

            // Act
            var result = this.matcher.IsProperMatcher(mailRule);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region [ ShouldBeRun ]

        [Test]
        public void ShouldBeRunReturnsTrueIfAllSubMatchersMet()
        {
            // Assemble
            var startTime   = new DateTime(2014, 6, 11);
            var mailRule    = new MailRule
                                  {
                                      MailPattern   = MailPattern.Montly,
                                      DayNumber     = 2,
                                      DaysOfWeek    = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Wednesday, true } },
                                      LastSent      = new DateTime(2014, 5, 7)
                                  };

            // Act
            var result = this.matcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldBeRunReturnsFalseIfDayOfWeekNotMet()
        {
            // Assemble
            var startTime   = new DateTime(2014, 6, 12);
            var mailRule    = new MailRule
                                  {
                                      MailPattern   = MailPattern.Montly,
                                      DayNumber     = 2,
                                      DaysOfWeek    = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Wednesday, true } },
                                      LastSent      = new DateTime(2014, 5, 7)
                                  };

            // Act
            var result = this.matcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeRunReturnsFalseIfWeekOfMonthNotMet()
        {
            // Assemble
            var startTime   = new DateTime(2014, 6, 11);
            var mailRule    = new MailRule
                                  {
                                      MailPattern   = MailPattern.Montly,
                                      DayNumber     = 1,
                                      DaysOfWeek    = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Wednesday, true } },
                                      LastSent      = new DateTime(2014, 5, 7)
                                  };

            // Act
            var result = this.matcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeRunReturnsFalseIfMonthlyRecurrenceNotMet()
        {
            // Assemble
            var startTime   = new DateTime(2014, 6, 11);
            var mailRule    = new MailRule
                                  {
                                      MailPattern   = MailPattern.Montly,
                                      DayNumber     = 2,
                                      DaysOfWeek    = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Wednesday, true } },
                                      LastSent      = new DateTime(2014, 5, 7),
                                      NumberOf      = 2
                                  };

            // Act
            var result = this.matcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #endregion
    }
}
