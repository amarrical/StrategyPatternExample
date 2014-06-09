//-----------------------------------------------------------------------
// <copyright file="MaxRecurrencesEliminatorTests.cs" company="ImprovingEnterprises">
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
    public class MaxRecurrencesEliminatorTests
    {
        #region [ Fields ]

        private MaxRecurrencesEliminator eliminator;

        #endregion

        [SetUp]
        public void SetUp()
        {
            this.eliminator = new MaxRecurrencesEliminator();
        }

        #region [ Tests ]

        #region [ IsProperEliminator Tests ]

        [Test]
        public void IsProperHandlerReturnsFalseIfMaxRecurrencesNotSet()
        {
            // Assemble
            var mailRule = new MailRule { MaxOccurences = null };

            // Act
            var result = this.eliminator.IsProperEliminator(mailRule);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsProperHandlerReturnsTrueIfMaxRecurrencesSet()
        {
            // Assemble
            var mailRule = new MailRule { MaxOccurences = 4 };

            // Act
            var result = this.eliminator.IsProperEliminator(mailRule);

            // Assert
            Assert.IsTrue(result);
        }

        #endregion

        #region [ ShouldBeEliminated Tests ]

        [Test]
        public void ShouldBeEliminatedReturnsFalseIfMaxRecurrencesLessThanTimesSent()
        {
            // Assemble
            var         startTime       = DateTime.Now;
            const int   MaxRecurrence   = 4;
            const int   TimesSent       = 2;
            Assert.IsTrue(MaxRecurrence > TimesSent, "Test is not valid");

            var mailRule = new MailRule { MaxOccurences = MaxRecurrence, TimesSent = TimesSent };

            // Act
            var result = this.eliminator.ShouldBeEliminated(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeEliminatedReturnsTrueIfMaxRecurrencesEqualsTimesSent()
        {
            // Assemble
            var         startTime       = DateTime.Now;
            const int   MaxRecurrence   = 4;
            const int   TimesSent       = 4;
            Assert.IsTrue(MaxRecurrence == TimesSent, "Test is not valid");

            var mailRule = new MailRule { MaxOccurences = MaxRecurrence, TimesSent = TimesSent };

            // Act
            var result = this.eliminator.ShouldBeEliminated(mailRule, startTime);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldBeEliminatedReturnsTrueIfMaxRecurrencesGreaterThanTimesSent()
        {
            // Assemble
            var         startTime       = DateTime.Now;
            const int   MaxRecurrence   = 4;
            const int   TimesSent       = 6;
            Assert.IsTrue(MaxRecurrence < TimesSent, "Test is not valid");

            var mailRule = new MailRule { MaxOccurences = MaxRecurrence, TimesSent = TimesSent };

            // Act
            var result = this.eliminator.ShouldBeEliminated(mailRule, startTime);

            // Assert
            Assert.IsTrue(result);
        }

        #endregion

        #endregion
    }
}
