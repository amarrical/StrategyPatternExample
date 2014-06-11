//-----------------------------------------------------------------------
// <copyright file="MailSenderTests.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.Test.MailSenderTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using NUnit.Framework;

    using Rhino.Mocks;

    using RuleBender.Entity;
    using RuleBender.Interface;
    using RuleBender.Logic;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules",
        "SA1600:ElementsMustBeDocumented",
        Justification = "Tests are self documenting")]
    [TestFixture]
    public class MailSenderTests
    {
        #region [ Fields ]

        private MailSender sender;

        private IRuleRepo       ruleRepo;
        private IEmailService   emailService;
        private IRuleParser     ruleParser;

        #endregion

        [SetUp]
        public void SetUp()
        {
            this.ruleRepo       = MockRepository.GenerateStrictMock<IRuleRepo>();
            this.emailService   = MockRepository.GenerateStrictMock<IEmailService>();
            this.ruleParser     = MockRepository.GenerateStrictMock<IRuleParser>();

            this.sender = new MailSender(this.ruleRepo, this.emailService, this.ruleParser);
        }

        [TearDown]
        public void TearDown()
        {
            this.ruleRepo       .VerifyAllExpectations();
            this.emailService   .VerifyAllExpectations();
        }

        #region [ Tests ]

        [Test]
        public void SendMessagesSendsMessages()
        {
            // Assemble
            var startTime       = DateTime.Now;
            var mailRules       = new List<MailRule>();
            var matchedRules    = new List<MailRule>();
            var sentRules       = new List<MailRule>();

            this.ruleRepo       .Expect(rr => rr.GetMailRules())                    .Return(mailRules);
            this.ruleParser     .Expect(rp => rp.ParseRules(mailRules, startTime))  .Return(matchedRules);
            this.emailService   .Expect(es => es.Send(matchedRules, startTime))     .Return(sentRules);
            this.ruleRepo       .Expect(rr => rr.SaveRunRules(sentRules));

            // Act
            this.sender.SendMessages(startTime);

            // Assert
        }

        #endregion
    }
}
