//-----------------------------------------------------------------------
// <copyright file="RuleEliminatorTests.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.Test.EliminatorTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using NUnit.Framework;

    using Rhino.Mocks;

    using RuleBender.Entity;
    using RuleBender.RuleParsers.RuleEliminators;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules",
        "SA1600:ElementsMustBeDocumented",
        Justification = "Tests are self documenting")]
    [TestFixture]
    public class RuleEliminatorTests
    {
        #region [ Fields ]

        private RuleEliminator ruleEliminator;

        #endregion

        #region [ Tests ]

        [Test]
        public void ConstructorAssignsproperDefaultEliminators()
        {
            // Assemble
            this.ruleEliminator = new RuleEliminator();

            // Act
            var eliminators = this.ruleEliminator.Eliminators;

            // Assert
            Assert.IsTrue(eliminators.Count == 5);
            Assert.IsTrue(eliminators.Count(e => e.GetType() == typeof(InactiveEliminator)) == 1);
            Assert.IsTrue(eliminators.Count(e => e.GetType() == typeof(MaxRecurrencesEliminator)) == 1);
            Assert.IsTrue(eliminators.Count(e => e.GetType() == typeof(PastEndDateEliminiator)) == 1);
            Assert.IsTrue(eliminators.Count(e => e.GetType() == typeof(RanTodayEliminator)) == 1);
            Assert.IsTrue(eliminators.Count(e => e.GetType() == typeof(StartDateEliminator)) == 1);
        }

        [Test]
        public void GetMailRulesRejectsMailRuleIfAnyEliminatorRejectsIt()
        {
            // Assemble
            var startTime = DateTime.Now;
            var mailRule1 = new MailRule();
            var mailRule2 = new MailRule();
            var mailRule3 = new MailRule();
            var mailRule4 = new MailRule();
            var mailRules = new List<MailRule> { mailRule1, mailRule2, mailRule3, mailRule4 };

            var eliminator1 = MockRepository.GenerateStrictMock<IMailRuleEliminator>();
            var eliminator2 = MockRepository.GenerateStrictMock<IMailRuleEliminator>();
            var eliminator3 = MockRepository.GenerateStrictMock<IMailRuleEliminator>();
            var eliminators = new List<IMailRuleEliminator> { eliminator1, eliminator2, eliminator3 };

            // Eliminator1 is not proper for any mail rule.
            eliminator1.Expect(e1 => e1.IsProperEliminator(mailRule1)).Return(false);
            eliminator1.Expect(e1 => e1.IsProperEliminator(mailRule2)).Return(false);
            eliminator1.Expect(e1 => e1.IsProperEliminator(mailRule3)).Return(false);
            eliminator1.Expect(e1 => e1.IsProperEliminator(mailRule4)).Return(false);

            // Eliminator2 does not evaluate rule 1, rejects rule 2, passes rules 3 and 4
            eliminator2.Expect(e2 => e2.IsProperEliminator(mailRule1)).Return(false);
            eliminator2.Expect(e2 => e2.IsProperEliminator(mailRule2)).Return(true);
            eliminator2.Expect(e2 => e2.IsProperEliminator(mailRule3)).Return(true);
            eliminator2.Expect(e2 => e2.IsProperEliminator(mailRule4)).Return(true);
            eliminator2.Expect(e2 => e2.ShouldBeEliminated(mailRule2, startTime)).Return(true);
            eliminator2.Expect(e2 => e2.ShouldBeEliminated(mailRule3, startTime)).Return(false);
            eliminator2.Expect(e2 => e2.ShouldBeEliminated(mailRule4, startTime)).Return(false);

            // Eliminator3 passes rule 1, rejects rules 2 and 3, passes rule 4
            eliminator3.Expect(e3 => e3.IsProperEliminator(mailRule1)).Return(true);
            eliminator3.Expect(e3 => e3.IsProperEliminator(mailRule2)).Return(true);
            eliminator3.Expect(e3 => e3.IsProperEliminator(mailRule3)).Return(true);
            eliminator3.Expect(e3 => e3.IsProperEliminator(mailRule4)).Return(true);
            eliminator3.Expect(e3 => e3.ShouldBeEliminated(mailRule1, startTime)).Return(false);
            eliminator3.Expect(e3 => e3.ShouldBeEliminated(mailRule2, startTime)).Return(true);
            eliminator3.Expect(e3 => e3.ShouldBeEliminated(mailRule3, startTime)).Return(true);
            eliminator3.Expect(e3 => e3.ShouldBeEliminated(mailRule4, startTime)).Return(false);

            // Act
            this.ruleEliminator = new RuleEliminator(eliminators);
            var result = this.ruleEliminator.GetMailRulesNotEliminated(mailRules, startTime);

            // Assert
            Assert.AreEqual(result.Count, 2);
            Assert.IsTrue(result.Contains(mailRule1));
            Assert.IsFalse(result.Contains(mailRule2));
            Assert.IsFalse(result.Contains(mailRule3));
            Assert.IsTrue(result.Contains(mailRule4));
        }

        #endregion
    }
}
