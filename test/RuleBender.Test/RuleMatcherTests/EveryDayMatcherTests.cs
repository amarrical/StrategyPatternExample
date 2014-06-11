//-----------------------------------------------------------------------
// <copyright file="EveryDayMatcherTests.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.Test.RuleMatcherTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using NUnit.Framework;

    using RuleBender.Entity;
    using RuleBender.RuleParsers.RuleMatchers;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules",
        "SA1600:ElementsMustBeDocumented",
        Justification = "Tests are self documenting")]
    [TestFixture]
    public class EveryDayMatcherTests
    {
        #region [ Fields ]

        private EveryDayMatcher matcher;

        #endregion

        [SetUp]
        public void SetUp()
        {
            this.matcher = new EveryDayMatcher();
        }

        #region [ Tests ]

        #region [ IsProperMatcher ]

        [Test]
        public void IsProperMatcherReturnsTrueIfRuleMailPatternIsDailyAndRuleIsNotDayOfWeekRestricted()
        {
            // Assemble
            var mailRule = new MailRule
                           {
                               MailPattern  = MailPattern.Daily,
                               DaysOfWeek   = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Monday, false } }
                           };

            Assert.IsFalse(mailRule.IsDayOfWeekRestricted, "Test is not configured properly");

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
                               MailPattern  = MailPattern.Daily,
                               DaysOfWeek   = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Monday, true } }
                           };

            Assert.IsTrue(mailRule.IsDayOfWeekRestricted, "Test is not configured properly");

            // Act
            var result = this.matcher.IsProperMatcher(mailRule);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        [TestCase(MailPattern.Weekly)]
        [TestCase(MailPattern.Montly)]
        [TestCase(MailPattern.Yearly)]
        public void IsProperMatcherReturnsFalseIfRuleMailPatternIsNotDaily(MailPattern mailPattern)
        {
            // Assemble
            var mailRule = new MailRule
                           {
                               MailPattern  = mailPattern,
                               DaysOfWeek   = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Monday, false } }
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
        public void ShouldBeRunReturnsTrueIfRuleNumberOfDoesNotHaveValue()
        {
            // Assemble
            var startTime   = new DateTime(2014, 6, 19);
            var mailRule    = new MailRule
                                  {
                                      MailPattern   = MailPattern.Daily,
                                      DaysOfWeek    = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Monday, false } },
                                      LastSent      = new DateTime(2014, 6, 18),
                                      NumberOf      = null
                                  };

            // Act
            var result = this.matcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldBeRunReturnsTrueIfLastSentPreviousToRecurrence()
        {
            // Assemble
            var startTime   = new DateTime(2014, 6, 19);
            var mailRule    = new MailRule
                                  {
                                      MailPattern   = MailPattern.Daily,
                                      DaysOfWeek    = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Monday, false } },
                                      LastSent      = new DateTime(2014, 6, 17),
                                      NumberOf      = 2
                                  };

            // Act
            var result = this.matcher.ShouldBeRun(mailRule, startTime);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldBeRunReturnsFalseIfRuleNumberOfHasValueAndLastSentGreaterThanRecurrence()
        {
            // Assemble
            var startTime   = new DateTime(2014, 6, 19);
            var mailRule    = new MailRule
                                  {
                                      MailPattern   = MailPattern.Daily,
                                      DaysOfWeek    = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Monday, false } },
                                      LastSent      = new DateTime(2014, 6, 17),
                                      NumberOf      = 4
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
