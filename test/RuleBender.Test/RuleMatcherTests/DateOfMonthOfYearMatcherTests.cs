//-----------------------------------------------------------------------
// <copyright file="DateOfMonthOfYearMatcherTests.cs" company="ImprovingEnterprises">
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
    public class DateOfMonthOfYearMatcherTests
    {
        #region [ Fields ]

        private DateOfMonthOfYearMatcher matcher;

        #endregion

        [SetUp]
        public void SetUp()
        {
            this.matcher = new DateOfMonthOfYearMatcher();
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
            Assert.IsTrue(subMatchers.Count(sm => sm.GetType() == typeof(IsDayOfMonthSubMatcher))           == 1);
            Assert.IsTrue(subMatchers.Count(sm => sm.GetType() == typeof(IsMonthOfYearSubMatcher))          == 1);
            Assert.IsTrue(subMatchers.Count(sm => sm.GetType() == typeof(IsYearlyRecurrenceMetSubMatcher))  == 1);
        }

        #region [ IsProperMatcher ]

        [Test]
        public void IsProperHandlerReturnsTrueIfRuleMailPatternIsYearlyAndRuleIsNotDayOfWeekRestricted()
        {
            // Assemble
            var mailRule = new MailRule { MailPattern = MailPattern.Yearly };

            Assert.IsFalse(mailRule.IsDayOfWeekRestricted, "Test is not configured properly");

            // Act
            var result = this.matcher.IsProperMatcher(mailRule);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsProperHandlerReturnsFalseIfRuleIsDayOfWeekRestricted()
        {
            // Assemble
            var mailRule = new MailRule
                           {
                               MailPattern  = MailPattern.Yearly,
                               DaysOfWeek   = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Tuesday, true } }
                           };

            Assert.IsTrue(mailRule.IsDayOfWeekRestricted, "Test is not configured properly");

            // Act
            var result = this.matcher.IsProperMatcher(mailRule);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        [TestCase(MailPattern.Daily)]
        [TestCase(MailPattern.Weekly)]
        [TestCase(MailPattern.Montly)]
        public void IsProperHandlerReturnsFalseIfRuleMailPatternIsNotYearly(MailPattern mailPattern)
        {
            // Assemble
            var mailRule = new MailRule { MailPattern = mailPattern };

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
            var startTime   = new DateTime(2014, 6, 15);
            var mailRule    = new MailRule
                                  {
                                      MailPattern   = MailPattern.Yearly,
                                      LastSent      = new DateTime(2012, 6, 15),
                                      Month         = 6,
                                      DayNumber     = 15,
                                      NumberOf      = 2
                                  };

            // Act
            var result = this.matcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldBeRunReturnsFalseIfDayOfMonthNotMet()
        {
            // Assemble
            var startTime   = new DateTime(2014, 6, 18);
            var mailRule    = new MailRule
                                  {
                                      MailPattern   = MailPattern.Yearly,
                                      LastSent      = new DateTime(2012, 6, 15),
                                      Month         = 6,
                                      DayNumber     = 15,
                                      NumberOf      = 2
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
            var startTime   = new DateTime(2014, 7, 15);
            var mailRule    = new MailRule
                                  {
                                      MailPattern   = MailPattern.Yearly,
                                      LastSent      = new DateTime(2012, 6, 15),
                                      Month         = 6,
                                      DayNumber     = 15,
                                      NumberOf      = 2
                                  };

            // Act
            var result = this.matcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeRunReturnsFalseIfYearlyRecurrenceNotMet()
        {
            // Assemble
            var startTime   = new DateTime(2014, 6, 15);
            var mailRule    = new MailRule
                                  {
                                      MailPattern   = MailPattern.Yearly,
                                      LastSent      = new DateTime(2012, 6, 15),
                                      Month         = 6,
                                      DayNumber     = 15,
                                      NumberOf      = 3
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