//-----------------------------------------------------------------------
// <copyright file="RuleMatcherTests.cs" company="ImprovingEnterprises">
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

    using Rhino.Mocks;

    using RuleBender.Entity;
    using RuleBender.RuleParsers.Combined;
    using RuleBender.RuleParsers.RuleMatchers;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules",
        "SA1600:ElementsMustBeDocumented",
        Justification = "Tests are self documenting")]
    [TestFixture]
    public class RuleMatcherTests
    {
        #region [ Fields ]

        private RuleMatcher ruleMatcher;

        #endregion

        #region [ Tests ]

        [Test]
        public void ConstructorAssignsProperDefaultMatchers()
        {
            // Assemble
            this.ruleMatcher = new RuleMatcher();

            // Act
            var matchers = this.ruleMatcher.Matchers;

            // Assert
            Assert.IsTrue(matchers.Count == 7);
            Assert.IsTrue(matchers.Count(m => m.GetType() == typeof(WeeklyMatcher))                 == 1);
            Assert.IsTrue(matchers.Count(m => m.GetType() == typeof(DateOfMonthMatcher))            == 1);
            Assert.IsTrue(matchers.Count(m => m.GetType() == typeof(DayOfWeekOfMonthOfYearMatcher)) == 1);
            Assert.IsTrue(matchers.Count(m => m.GetType() == typeof(DayOfWeekOfMonthHandler))       == 1);
            Assert.IsTrue(matchers.Count(m => m.GetType() == typeof(DateOfMonthOfYearMatcher))      == 1);
            Assert.IsTrue(matchers.Count(m => m.GetType() == typeof(EveryDayMatcher))               == 1);
            Assert.IsTrue(matchers.Count(m => m.GetType() == typeof(EveryWeekDayMatcher))           == 1);
        }

        [Test]
        public void GetMatchedRulesAcceptsMailRuleIfAnyMatcherMatchesIt()
        {
            // Assemble
            var startTime = DateTime.Now;
            var mailRule1 = new MailRule();
            var mailRule2 = new MailRule();
            var mailRule3 = new MailRule();
            var mailRule4 = new MailRule();
            var mailRules = new List<MailRule> { mailRule1, mailRule2, mailRule3, mailRule4 };

            var matcher1 = MockRepository.GenerateStrictMock<IMailRuleMatcher>();
            var matcher2 = MockRepository.GenerateStrictMock<IMailRuleMatcher>();
            var matcher3 = MockRepository.GenerateStrictMock<IMailRuleMatcher>();
            var matchers = new List<IMailRuleMatcher> { matcher1, matcher2, matcher3 };

            // Matcher1 is not proper for any mail rule.
            matcher1.Expect(e1 => e1.IsProperMatcher(mailRule1)).Return(false);
            matcher1.Expect(e1 => e1.IsProperMatcher(mailRule2)).Return(false);
            matcher1.Expect(e1 => e1.IsProperMatcher(mailRule3)).Return(false);
            matcher1.Expect(e1 => e1.IsProperMatcher(mailRule4)).Return(false);

            // Matcher2 does not evaluate rule 1, accepts rule 2, refuses rules 3 and 4
            matcher2.Expect(e2 => e2.IsProperMatcher(mailRule1)).Return(false);
            matcher2.Expect(e2 => e2.IsProperMatcher(mailRule2)).Return(true);
            matcher2.Expect(e2 => e2.IsProperMatcher(mailRule3)).Return(true);
            matcher2.Expect(e2 => e2.IsProperMatcher(mailRule4)).Return(true);
            matcher2.Expect(e2 => e2.ShouldBeRun(mailRule2, startTime)).Return(true);
            matcher2.Expect(e2 => e2.ShouldBeRun(mailRule3, startTime)).Return(false);
            matcher2.Expect(e2 => e2.ShouldBeRun(mailRule4, startTime)).Return(false);

            // Matcher3 rejects rule 1, accepts rules 2 and 3, rejects rule 4
            matcher3.Expect(e3 => e3.IsProperMatcher(mailRule1)).Return(true);
            matcher3.Expect(e3 => e3.IsProperMatcher(mailRule2)).Return(true);
            matcher3.Expect(e3 => e3.IsProperMatcher(mailRule3)).Return(true);
            matcher3.Expect(e3 => e3.IsProperMatcher(mailRule4)).Return(true);
            matcher3.Expect(e3 => e3.ShouldBeRun(mailRule1, startTime)).Return(false);
            matcher3.Expect(e3 => e3.ShouldBeRun(mailRule2, startTime)).Return(true);
            matcher3.Expect(e3 => e3.ShouldBeRun(mailRule3, startTime)).Return(true);
            matcher3.Expect(e3 => e3.ShouldBeRun(mailRule4, startTime)).Return(false);

            // Act
            this.ruleMatcher = new RuleMatcher(matchers);
            var result = this.ruleMatcher.GetMatchedRules(mailRules, startTime);

            // Assert
            Assert.AreEqual(result.Count, 2);
            Assert.IsFalse(result.Contains(mailRule1));
            Assert.IsTrue(result.Contains(mailRule2));
            Assert.IsTrue(result.Contains(mailRule3));
            Assert.IsFalse(result.Contains(mailRule4));
        }

        #endregion
    }
}
