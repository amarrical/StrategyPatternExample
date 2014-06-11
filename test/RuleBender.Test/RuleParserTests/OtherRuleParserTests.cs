//-----------------------------------------------------------------------
// <copyright file="OtherRuleParserTests.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.Test.RuleParserTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using NUnit.Framework;

    using RuleBender.Entity;
    using RuleBender.Interface;
    using RuleBender.RuleParsers;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules",
        "SA1600:ElementsMustBeDocumented",
        Justification = "Tests are self documenting")]
    [TestFixture]
    public class OtherRuleParserTests
    {
        #region [ Fields ]

        private IRuleParser parser;

        #endregion

        [SetUp]
        public void SetUp()
        {
            var ruleParsers = new List<IRuleParser>
                              {
                                  new UglyFlatRuleParser(),
                                  new CommentedFlatRuleParser(),
                                  new StragetyRuleParser(),
                                  new EasyTestStragetyRuleParser()
                              };

            this.parser = ruleParsers[1];
        }

        #region [ Tests ]

        [Test]
        [Ignore("Demonstration test to run through all parsers")]
        public void CombinedTests()
        {
            // Assemble
            var ruleParsers = new List<IRuleParser>
                              {
                                  new UglyFlatRuleParser(),
                                  new CommentedFlatRuleParser(),
                                  new StragetyRuleParser(),
                                  new EasyTestStragetyRuleParser()
                              };

            // Act
            // Assert
            ruleParsers.ForEach(
                rp =>
                    {
                        this.parser = rp;
                        this.ParseRulesReturnsCorrectRules();
                    });
        }

        [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1500:CurlyBracketsForMultiLineStatementsMustNotShareLine", Justification = "Trying to save vertical space in a test.")]
        [Test]
        public void ParseRulesReturnsCorrectRules()
        {
            // Assemble
            var startTime = new DateTime(2014, 6, 26); // Thursday, 4th week

            // Rules which will be eliminated.
            var eliminatedRule1 = new MailRule { Description = "eliminatedRule1", IsActive = false };
            var eliminatedRule2 = new MailRule { Description = "eliminatedRule2", IsActive = true, LastSent = startTime };
            var eliminatedRule3 = new MailRule { Description = "eliminatedRule3", IsActive = true, MaxOccurences = 4, TimesSent = 4 };
            var eliminatedRule4 = new MailRule { Description = "eliminatedRule4", IsActive = true, EndDate = startTime.AddDays(-4) };
            var eliminatedRule5 = new MailRule { Description = "eliminatedRule5", IsActive = true, StartDate = startTime.AddDays(4) };

            // Rules which will not be matched.
            var rejectedRule1 = new MailRule { Description = "rejectedRule1", IsActive = true, MailPattern = MailPattern.Daily, DaysOfWeek = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Tuesday, true } } };
            var rejectedRule2 = new MailRule { Description = "rejectedRule2", IsActive = true, MailPattern = MailPattern.Daily, NumberOf = 4, LastSent = startTime.AddDays(-2) };
            var rejectedRule3 = new MailRule { Description = "rejectedRule3", IsActive = true, MailPattern = MailPattern.Weekly, DaysOfWeek = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Tuesday, true } } };
            var rejectedRule4 = new MailRule { Description = "rejectedRule4", IsActive = true, MailPattern = MailPattern.Weekly, DaysOfWeek = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Thursday, true } }, NumberOf = 2, LastSent = startTime.AddDays(-3) };
            var rejectedRule5 = new MailRule { Description = "rejectedRule5", IsActive = true, MailPattern = MailPattern.Montly, DayNumber = 14 };
            var rejectedRule6 = new MailRule { Description = "rejectedRule6", IsActive = true, MailPattern = MailPattern.Montly, DayNumber = 26, NumberOf = 2, LastSent = startTime.AddMonths(-1) };
            var rejectedRule7 = new MailRule { Description = "rejectedRule7", IsActive = true, MailPattern = MailPattern.Montly, DaysOfWeek = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Tuesday, true } } };
            var rejectedRule8 = new MailRule { Description = "rejectedRule8", IsActive = true, MailPattern = MailPattern.Montly, DaysOfWeek = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Thursday, true } }, DayNumber = 2 };
            var rejectedRule9 = new MailRule { Description = "rejectedRule9", IsActive = true, MailPattern = MailPattern.Montly, DaysOfWeek = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Thursday, true } }, DayNumber = 4, NumberOf = 2, LastSent = startTime.AddMonths(-1) };
            var rejectedRule10 = new MailRule { Description = "rejectedRule10", IsActive = true, MailPattern = MailPattern.Yearly, DayNumber = 14 };
            var rejectedRule11 = new MailRule { Description = "rejectedRule11", IsActive = true, MailPattern = MailPattern.Yearly, DayNumber = 26, Month = 4 };
            var rejectedRule12 = new MailRule { Description = "rejectedRule12", IsActive = true, MailPattern = MailPattern.Yearly, DayNumber = 26, Month = 6, NumberOf = 2, LastSent = startTime.AddYears(-1) };
            var rejectedRule13 = new MailRule { Description = "rejectedRule13", IsActive = true, MailPattern = MailPattern.Yearly, DaysOfWeek = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Tuesday, true } } };
            var rejectedRule14 = new MailRule { Description = "rejectedRule14", IsActive = true, MailPattern = MailPattern.Yearly, DaysOfWeek = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Thursday, true } }, Month = 4 };
            var rejectedRule15 = new MailRule { Description = "rejectedRule15", IsActive = true, MailPattern = MailPattern.Yearly, DaysOfWeek = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Thursday, true } }, Month = 6, DayNumber = 2 };
            var rejectedRule16 = new MailRule { Description = "rejectedRule16", IsActive = true, MailPattern = MailPattern.Yearly, DaysOfWeek = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Thursday, true } }, Month = 6, DayNumber = 4, NumberOf = 2, LastSent = startTime.AddYears(-1) };

            // Rules which will be matched.
            var matchedRule1 = new MailRule { Description = "matchedRule1", IsActive = true, MailPattern = MailPattern.Daily, DaysOfWeek = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Thursday, true } } };
            var matchedRule2 = new MailRule { Description = "matchedRule2", IsActive = true, MailPattern = MailPattern.Daily, NumberOf = 4, LastSent = startTime.AddDays(-5) };
            var matchedRule3 = new MailRule { Description = "matchedRule3", IsActive = true, MailPattern = MailPattern.Weekly, DaysOfWeek = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Thursday, true } }, NumberOf = 2, LastSent = startTime.AddDays(-15) };
            var matchedRule4 = new MailRule { Description = "matchedRule4", IsActive = true, MailPattern = MailPattern.Montly, DayNumber = 26, NumberOf = 2, LastSent = startTime.AddMonths(-3) };
            var matchedRule5 = new MailRule { Description = "matchedRule5", IsActive = true, MailPattern = MailPattern.Montly, DaysOfWeek = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Thursday, true } }, DayNumber = 4, NumberOf = 2, LastSent = startTime.AddMonths(-3) };
            var matchedRule6 = new MailRule { Description = "matchedRule6", IsActive = true, MailPattern = MailPattern.Yearly, DayNumber = 26, Month = 6, NumberOf = 2, LastSent = startTime.AddYears(-3) };
            var matchedRule7 = new MailRule { Description = "matchedRule7", IsActive = true, MailPattern = MailPattern.Yearly, DaysOfWeek = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Thursday, true } }, Month = 6, DayNumber = 4, NumberOf = 2, LastSent = startTime.AddYears(-3) };

            var mailRules = new List<MailRule> { eliminatedRule1, eliminatedRule2, eliminatedRule3, eliminatedRule4, eliminatedRule5, rejectedRule1, 
                                                 rejectedRule2, rejectedRule3, rejectedRule4, rejectedRule5, rejectedRule6, rejectedRule7, rejectedRule8, 
                                                 rejectedRule9, rejectedRule10, rejectedRule11, rejectedRule12, rejectedRule13, rejectedRule14, 
                                                 rejectedRule15, rejectedRule16, matchedRule1, matchedRule2, matchedRule3, matchedRule4, matchedRule5,
                                                 matchedRule6, matchedRule7 };

            // Act
            var result = this.parser.ParseRules(mailRules, startTime);

            // Assert
            Assert.IsTrue(result.Count == 7);
            Assert.IsTrue(result.Contains(matchedRule1));
            Assert.IsTrue(result.Contains(matchedRule2));
            Assert.IsTrue(result.Contains(matchedRule3));
            Assert.IsTrue(result.Contains(matchedRule4));
            Assert.IsTrue(result.Contains(matchedRule5));
            Assert.IsTrue(result.Contains(matchedRule6));
            Assert.IsTrue(result.Contains(matchedRule7));
        }

        [Test]
        [Ignore("Demonstration test to show bug being fixed.")]
        public void WillNotPassUntilBugIsFixed()
        {
            // Assemble
            var startTime   = new DateTime(2014, 7, 14);
            var mailRule    = new MailRule
                                  {
                                      IsActive      = true,
                                      Description   = "This will fail until the week of month bug is fixed",
                                      MailPattern   = MailPattern.Montly,
                                      DayNumber     = 2,
                                      DaysOfWeek    = new Dictionary<DayOfWeek, bool> { { DayOfWeek.Monday, true } }
                                  };

            var mailRules = new List<MailRule> { mailRule };

            // Act
            var result = this.parser.ParseRules(mailRules, startTime);

            // Assert
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.Contains(mailRule));
        }

        #endregion
    }
}
