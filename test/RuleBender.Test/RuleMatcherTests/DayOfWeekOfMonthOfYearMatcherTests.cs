//-----------------------------------------------------------------------
// <copyright file="DayOfWeekOfMonthOfYearMatcherTests.cs" company="ImprovingEnterprises">
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
    public class DayOfWeekOfMonthOfYearMatcherTests
    {
        #region [ Fields ]

        private DayOfWeekOfMonthOfYearMatcher matcher;

        #endregion

        [SetUp]
        public void SetUp()
        {
            this.matcher = new DayOfWeekOfMonthOfYearMatcher();
        }

        #region [ Tests ]

        [Test]
        public void MatcherContainsProperSubRules()
        {
            // Assemble
            // Act
            var subMatchers = this.matcher.SubMatchers;

            // Assert
            Assert.IsTrue(subMatchers.Count == 4);
            Assert.IsTrue(subMatchers.Count(sm => sm.GetType() == typeof(IsYearlyRecurrenceMetSubMatcher))  == 1);
            Assert.IsTrue(subMatchers.Count(sm => sm.GetType() == typeof(IsMonthOfYearSubMatcher))          == 1);
            Assert.IsTrue(subMatchers.Count(sm => sm.GetType() == typeof(IsDayOfWeekSubMatcher))            == 1);
            Assert.IsTrue(subMatchers.Count(sm => sm.GetType() == typeof(IsWeekOfMonthSubMatcher))          == 1);
        }

        #region [ IsProperMatcher ]

        [Test]
        public void IsProperHandlerReturnsTrueIfRuleDayNumberHasValueAndRuleIsDayOfWeekRestrictedAndRuleMailPatternIsYearly()
        {
            // Assemble
            var mailRule = new MailRule
                           {
                               MailPattern  = MailPattern.Yearly,
                               DaysOfWeek   = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Tuesday, true } },
                               DayNumber    = 4
                           };

            // Act
            var result = this.matcher.IsProperMatcher(mailRule);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsProperHandlerReturnsFalseIfRuleDayNumberDoesNotHaveValue()
        {
            // Assemble
            var mailRule = new MailRule
                           {
                               MailPattern  = MailPattern.Yearly,
                               DaysOfWeek   = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Tuesday, true } },
                               DayNumber    = null
                           };

            // Act
            var result = this.matcher.IsProperMatcher(mailRule);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsProperHandlerReturnsFalseIfRuleIsNotDayOfWeekRestricted()
        {
            // Assemble
            var mailRule = new MailRule
                           {
                               MailPattern  = MailPattern.Yearly,
                               DaysOfWeek   = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Tuesday, false } },
                               DayNumber    = 4
                           };

            // Act
            var result = this.matcher.IsProperMatcher(mailRule);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        [TestCase(MailPattern.Daily)]
        [TestCase(MailPattern.Weekly)]
        [TestCase(MailPattern.Montly)]
        public void IsProperHandlerReturnsFalseIfMailPatternIsNotYearly(MailPattern mailPattern)
        {
            // Assemble
            var mailRule = new MailRule
                           {
                               MailPattern  = mailPattern,
                               DaysOfWeek   = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Tuesday, true } },
                               DayNumber    = 4
                           };

            // Act
            var result = this.matcher.IsProperMatcher(mailRule);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region [ ShouldBeRun ]

        [Test]
        public void ShouldBeRunReturnsFalseIfYearlyRecurrenceNotMet()
        {
            // Assemble
            var startTime   = new DateTime(2014, 6, 10);
            var mailRule    = new MailRule
                                  {
                                      MailPattern   = MailPattern.Yearly,
                                      DaysOfWeek    = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Tuesday, true } },
                                      DayNumber     = 2,
                                      Month         = 6,
                                      NumberOf      = 4,
                                      LastSent      = new DateTime(2012, 6, 5)
                                  };

            // Act
            var result = this.matcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeRunReturnsFalseIfMonthOfYearNotMet()
        {
            // Assemble
            // Assemble
            var startTime   = new DateTime(2014, 6, 10);
            var mailRule    = new MailRule
                                  {
                                      MailPattern   = MailPattern.Yearly,
                                      DaysOfWeek    = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Tuesday, true } },
                                      DayNumber     = 2,
                                      Month         = 7,
                                      NumberOf      = 2,
                                      LastSent      = new DateTime(2012, 6, 5)
                                  };

            // Act
            var result = this.matcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeRunReturnsFalseIfDayOfWeekNotMet()
        {
            // Assemble
            var startTime   = new DateTime(2014, 6, 9);
            var mailRule    = new MailRule
                                  {
                                      MailPattern   = MailPattern.Yearly,
                                      DaysOfWeek    = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Tuesday, true } },
                                      DayNumber     = 2,
                                      Month         = 6,
                                      NumberOf      = 2,
                                      LastSent      = new DateTime(2012, 6, 5)
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
            var startTime   = new DateTime(2014, 6, 10);
            var mailRule    = new MailRule
                                  {
                                      MailPattern   = MailPattern.Yearly,
                                      DaysOfWeek    = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Tuesday, true } },
                                      DayNumber     = 1,
                                      Month         = 6,
                                      NumberOf      = 2,
                                      LastSent      = new DateTime(2012, 6, 5)
                                  };

            // Act
            var result = this.matcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeRunReturnsTrueIfAllSubMatchersMet()
        {
            // Assemble
            var startTime   = new DateTime(2014, 6, 10);
            var mailRule    = new MailRule
                                  {
                                      MailPattern   = MailPattern.Yearly,
                                      DaysOfWeek    = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Tuesday, true } },
                                      DayNumber     = 2,
                                      Month         = 6,
                                      NumberOf      = 2,
                                      LastSent      = new DateTime(2012, 6, 5)
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