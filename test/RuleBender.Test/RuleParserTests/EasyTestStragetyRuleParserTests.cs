//-----------------------------------------------------------------------
// <copyright file="EasyTestStragetyRuleParserTests.cs" company="ImprovingEnterprises">
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

    using Rhino.Mocks;

    using RuleBender.Entity;
    using RuleBender.RuleParsers;
    using RuleBender.RuleParsers.Combined;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules",
        "SA1600:ElementsMustBeDocumented",
        Justification = "Tests are self documenting")]
    [TestFixture]
    public class EasyTestStragetyRuleParserTests
    {
        #region [ Fields ]

        private EasyTestStragetyRuleParser parser;

        private IRuleEliminator eliminator;
        private IRuleMatcher    matcher;

        #endregion

        [SetUp]
        public void SetUp()
        {
            this.eliminator = MockRepository.GenerateStrictMock<IRuleEliminator>();
            this.matcher    = MockRepository.GenerateStrictMock<IRuleMatcher>();

            this.parser = new EasyTestStragetyRuleParser(this.eliminator, this.matcher);
        }

        [TearDown]
        public void TearDown()
        {
            this.eliminator .VerifyAllExpectations();
            this.matcher    .VerifyAllExpectations();
        }

        #region [ Tests ]

        [Test]
        public void ParserReturnsResultsOfEliminatorAndMatcher()
        {
            // Assemble
            var startTime       = DateTime.Now;
            var mailRules       = new List<MailRule>();
            var nonEliminated   = new List<MailRule>();
            var matchedRules    = new List<MailRule>();

            this.eliminator .Expect(e => e.GetMailRulesNotEliminated(mailRules, startTime)) .Return(nonEliminated);
            this.matcher    .Expect(e => e.GetMatchedRules(nonEliminated, startTime))       .Return(matchedRules);

            // Act
            var result = this.parser.ParseRules(mailRules, startTime);

            // Assert
            Assert.AreEqual(matchedRules, result);
        }

        #endregion
    }
}
