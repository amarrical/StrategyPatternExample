//-----------------------------------------------------------------------
// <copyright file="EveryWeekDayMatcherTests.cs" company="ImprovingEnterprises">
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
    public class EveryWeekDayMatcherTests
    {
        #region [ Fields ]

        private EveryWeekDayMatcher matcher;

        #endregion

        [SetUp]
        public void SetUp()
        {
            this.matcher = new EveryWeekDayMatcher();
        }

        #region [ Tests ]

        [Test]
        public void MatcherContainsProperSubRules()
        {
            // Assemble
            // Act
            var subMatchers = this.matcher.SubMatchers;

            // Assert
            Assert.IsTrue(subMatchers.Count == 1);
            Assert.IsTrue(subMatchers.Count(sm => sm.GetType() == typeof(IsDayOfWeekSubMatcher)) == 1);
        }

        #region [ IsProperMatcher ]

        [Test]
        public void IsProperMatcherReturnsTrueIfRuleMailPatternIsDailyAndRuleIsDaysOfWeekRestricted()
        {
            // Assemble
            var mailRule = new MailRule
                           {
                               MailPattern  = MailPattern.Daily,
                               DaysOfWeek   = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Friday, true } }
                           };

            Assert.IsTrue(mailRule.IsDayOfWeekRestricted, "Test is not configured properly");

            // Act
            var result = this.matcher.IsProperMatcher(mailRule);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsProperMatcherReturnsFalseIfRuleIsNotDaysOfWeekRestricted()
        {
            // Assemble
            var mailRule = new MailRule
                           {
                               MailPattern  = MailPattern.Daily,
                               DaysOfWeek   = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Friday, false } }
                           };

            Assert.IsFalse(mailRule.IsDayOfWeekRestricted, "Test is not configured properly");

            // Act
            var result = this.matcher.IsProperMatcher(mailRule);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        [TestCase(MailPattern.Weekly)]
        [TestCase(MailPattern.Montly)]
        [TestCase(MailPattern.Yearly)]
        public void IsProperHandlerReturnsFalseIfRuleMailPatternIsNotDaily(MailPattern mailPattern)
        {
            // Assemble
            var mailRule = new MailRule
                           {
                               MailPattern  = mailPattern,
                               DaysOfWeek   = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Friday, true } }
                           };

            Assert.IsTrue(mailRule.IsDayOfWeekRestricted, "Test is not configured properly");

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
            var startTime   = new DateTime(2014, 5, 13);
            var mailRule    = new MailRule
                                  {
                                      MailPattern   = MailPattern.Daily,
                                      DaysOfWeek    = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Friday, true } },
                                      LastSent      = new DateTime(2014, 6, 6)
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
            var startTime   = new DateTime(2014, 5, 12);
            var mailRule    = new MailRule
                                  {
                                      MailPattern   = MailPattern.Daily,
                                      DaysOfWeek    = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Friday, true } },
                                      LastSent      = new DateTime(2014, 6, 6)
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