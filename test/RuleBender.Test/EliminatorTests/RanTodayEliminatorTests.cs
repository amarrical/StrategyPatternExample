//-----------------------------------------------------------------------
// <copyright file="RanTodayEliminatorTests.cs" company="ImprovingEnterprises">
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
    public class RanTodayEliminatorTests
    {
        #region [ Fields ]

        private RanTodayEliminator eliminator;

        #endregion

        [SetUp]
        public void SetUp()
        {
            this.eliminator = new RanTodayEliminator();
        }

        #region [ Tests ]

        #region [ IsProperEliminator Tests ]

        [Test]
        public void IsProperEliminatorReturnsTrueForAllRules()
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
        public void ShouldBeEliminatedReturnsFalseIfLastSentDoesNotEqualStartDate()
        {
            // Assemble
            var startTime   = new DateTime(2014, 6, 14);
            var lastSent    = new DateTime(2014, 6, 15);
            Assert.IsTrue(lastSent != startTime, "Test is not Valid");

            var mailRule = new MailRule { LastSent = lastSent };

            // Act
            var result = this.eliminator.ShouldBeEliminated(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeEliminatedReturnsTrueIfLastSentEqualsStartDate()
        {
            // Assemble
            var startTime   = new DateTime(2014, 6, 14);
            var lastSent    = new DateTime(2014, 6, 14);
            Assert.IsTrue(lastSent == startTime, "Test is not Valid");

            var mailRule = new MailRule { LastSent = lastSent };

            // Act
            var result = this.eliminator.ShouldBeEliminated(mailRule, startTime);

            // Assert
            Assert.IsTrue(result);
        }

        #endregion

        #endregion
    }
}
