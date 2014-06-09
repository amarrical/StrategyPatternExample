//-----------------------------------------------------------------------
// <copyright file="StartDateEliminatorTests.cs" company="ImprovingEnterprises">
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
    public class StartDateEliminatorTests
    {
        #region [ Fields ]

        private StartDateEliminator eliminator;

        #endregion

        [SetUp]
        public void SetUp()
        {
            this.eliminator = new StartDateEliminator();
        }

        #region [ Tests ]

        #region [ IsProperEliminator Tests ]

        [Test]
        public void IsProperEliminatorReturnsFalseIfStartDateNotSet()
        {
            // Assemble
            var mailRule = new MailRule { StartDate = null };

            // Act
            var result = this.eliminator.IsProperEliminator(mailRule);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsProperHandlerReturnsTrueIfStartDateIsSet()
        {
            // Assemble
            var mailRule = new MailRule { StartDate = DateTime.Now };

            // Act
            var result = this.eliminator.IsProperEliminator(mailRule);

            // Assert
            Assert.IsTrue(result);
        }

        #endregion

        #region [ ShouldBeEliminated Tests ]

        [Test]
        public void ShouldBeEliminatedReturnsFalseIfStartDateIsPastStartTime()
        {
            // Assemble
            var startTime = new DateTime(2014, 6, 16);
            var startDate = new DateTime(2014, 6, 17);
            Assert.IsTrue(startDate > startTime, "Test is not Valid");

            var mailRule = new MailRule { StartDate = startDate };

            // Act
            var result = this.eliminator.ShouldBeEliminated(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeEliminatedReturnsFalseIfStartDateIsEqualToStartTime()
        {
            // Assemble
            var startTime = new DateTime(2014, 6, 16);
            var startDate = new DateTime(2014, 6, 16);
            Assert.IsTrue(startDate.Date == startTime.Date, "Test is not Valid");

            var mailRule = new MailRule { StartDate = startDate };

            // Act
            var result = this.eliminator.ShouldBeEliminated(mailRule, startTime);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldBeEliminatedReturnsTrueIfStartDateIsBeforeStartTime()
        {
            // Assemble
            var startTime = new DateTime(2014, 6, 16);
            var startDate = new DateTime(2014, 6, 15);
            Assert.IsTrue(startDate < startTime, "Test is not Valid");

            var mailRule = new MailRule { StartDate = startDate };

            // Act
            var result = this.eliminator.ShouldBeEliminated(mailRule, startTime);

            // Assert
            Assert.IsTrue(result);
        }

        #endregion

        #endregion
    }
}
