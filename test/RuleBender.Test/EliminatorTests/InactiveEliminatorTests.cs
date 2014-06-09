//-----------------------------------------------------------------------
// <copyright file="PastEndDateEliminatorTests.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.Test.EliminatorTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using NUnit.Framework;

    using RuleBender.Entity;
    using RuleBender.RuleParsers.RuleEliminators;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules",
        "SA1600:ElementsMustBeDocumented",
        Justification = "Tests are self documenting")]
    [TestFixture]
    public class InactiveEliminatorTests
    {
        #region [ Fields ]

        private InactiveEliminator eliminator;

        #endregion

        [SetUp]
        public void SetUp()
        {
            this.eliminator = new InactiveEliminator();
        }

        #region [ Tests ]

        #region [ IsProperEliminator Tests ]

        [Test]
        public void IsProperHandlerReturnsTrueForAllMailRules()
        {
            // Assemble
            var mailRule = new MailRule();

            // Act
            var result = this.eliminator.IsProperEliminator(mailRule);

            // Assert
            Assert.IsTrue(result);
        } 

        #endregion

        #region [ ShouldBeEliminated Tests ]

        [Test]
        public void ShouldBeEliminatedReturnsTrueIfMailRuleIsNotActive()
        {
            // Assemble
            var mailRule = new MailRule { IsActive = false };
            var startTime = DateTime.Today;

            // Act
            var result = this.eliminator.ShouldBeEliminated(mailRule, startTime);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldBeEliminatedReturnsFalseIfMailRuleIsActive()
        {
            // Assemble
            var mailRule = new MailRule { IsActive = true };
            var startTime = DateTime.Today;

            // Act
            var result = this.eliminator.ShouldBeEliminated(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        } 

        #endregion

        #endregion
    }
}
