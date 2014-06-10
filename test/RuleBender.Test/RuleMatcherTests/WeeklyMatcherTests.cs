//-----------------------------------------------------------------------
// <copyright file="WeeklyMatcherTests.cs" company="ImprovingEnterprises">
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
    public class WeeklyMatcherTests
    {
        #region [ Fields ]

        private WeeklyMatcher matcher;

        #endregion

        [SetUp]
        public void SetUp()
        {
            this.matcher = new WeeklyMatcher();
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
        public void IsProperHandlerReturnsTrueIfRuleMailPatternIsWeeklyAndRuleIsDayOfWeekRestricted()
        {
            // Assemble
            var mailRule = new MailRule
                           {
                               MailPattern  = MailPattern.Weekly,
                               DaysOfWeek   = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Thursday, true } }
                           };

            Assert.IsTrue(mailRule.IsDayOfWeekRestricted, "Test is not configured properly");

            // Act
            var result = this.matcher.IsProperMatcher(mailRule);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        [TestCase(MailPattern.Daily)]
        [TestCase(MailPattern.Montly)]
        [TestCase(MailPattern.Yearly)]
        public void IsProperHandlerReturnsFalseIfRuleMailPatternIsNotWeekly(MailPattern mailPattern)
        {
            // Assemble
            var mailRule = new MailRule
                           {
                               MailPattern  = mailPattern,
                               DaysOfWeek   = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Thursday, true } }
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
                               MailPattern  = MailPattern.Weekly,
                               DaysOfWeek   = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Thursday, false } }
                           };

            Assert.IsFalse(mailRule.IsDayOfWeekRestricted, "Test is not configured properly");

            // Act
            var result = this.matcher.IsProperMatcher(mailRule);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region [ ShouldBeRun ]

        [Test]
        public void ShouldBeRunReturnsFalseIfDayOfWeekNotMet()
        {
            // Assemble
            var startTime   = new DateTime(2014, 6, 27);
            var mailRule    = new MailRule
                                  {
                                      MailPattern   = MailPattern.Weekly,
                                      DaysOfWeek    = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Thursday, true } },
                                      LastSent      = new DateTime(2014, 6, 5),
                                      NumberOf      = 2
                                  };

            // Act
            var result = this.matcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeRunReturnsFalseIfWeeklyRecurrenceNotMet()
        {
            // Assemble
            var startTime   = new DateTime(2014, 6, 26);
            var mailRule    = new MailRule
                                  {
                                      MailPattern   = MailPattern.Weekly,
                                      DaysOfWeek    = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Thursday, true } },
                                      LastSent      = new DateTime(2014, 6, 5),
                                      NumberOf      = 4
                                  };

            // Act
            var result = this.matcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeRunReturnsTrueIfSubMatchersMetAndWeeklyRecurrenceMet()
        {
            // Assemble
            var startTime   = new DateTime(2014, 6, 26);
            var mailRule    = new MailRule
                                  {
                                      MailPattern   = MailPattern.Weekly,
                                      DaysOfWeek    = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Thursday, true } },
                                      LastSent      = new DateTime(2014, 6, 5),
                                      NumberOf      = 2
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