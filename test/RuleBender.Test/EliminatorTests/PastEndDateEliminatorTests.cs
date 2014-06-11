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
    public class PastEndDateEliminatorTests
    {
        #region [ Fields ]

        private PastEndDateEliminiator eliminator;

        #endregion

        [SetUp]
        public void SetUp()
        {
            this.eliminator = new PastEndDateEliminiator();
        }

        #region [ Tests ]

        #region [ IsProperEliminator Tests ]

        [Test]
        public void IsProperEliminatorReturnsTrueIfRuleEndDateSet()
        {
            // Assemble
            var mailRule = new MailRule { EndDate = DateTime.Today };

            // Act
            var result = this.eliminator.IsProperEliminator(mailRule);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsProperEliminatorReturnsFalseIfRuleEndDateNotSet()
        {
            // Assemble
            var mailRule = new MailRule { EndDate = null };

            // Act
            var result = this.eliminator.IsProperEliminator(mailRule);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region [ ShouldBeEliminated Tests ]

        [Test]
        public void ShouldBeEliminatedReturnsTrueIfStartTimeIsPastToEndDate()
        {
            // Assemble
            var startTime   = new DateTime(2014, 6, 14);
            var endDate     = new DateTime(2014, 6, 13);
            Assert.IsTrue(endDate < startTime, "Test is not Valid");

            var mailRule = new MailRule { EndDate = endDate };

            // Act
            var result = this.eliminator.ShouldBeEliminated(mailRule, startTime);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldBeEliminatedReturnsFalseIfStartTimeIsEqualToEndDate()
        {
            // Assemble
            var startTime   = new DateTime(2014, 6, 14);
            var endDate     = new DateTime(2014, 6, 14);
            Assert.IsTrue(endDate.Date == startTime.Date, "Test is not Valid");

            var mailRule = new MailRule { EndDate = endDate };

            // Act
            var result = this.eliminator.ShouldBeEliminated(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeEliminatedReturnsFalseIfStartTimeIsBeforeEndDate()
        {
            // Assemble
            var startTime   = new DateTime(2014, 6, 14);
            var endDate     = new DateTime(2014, 6, 15);
            Assert.IsTrue(endDate > startTime, "Test is not Valid");

            var mailRule = new MailRule { EndDate = endDate };

            // Act
            var result = this.eliminator.ShouldBeEliminated(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #endregion
    }
}
