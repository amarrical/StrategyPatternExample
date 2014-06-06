//-----------------------------------------------------------------------
// <copyright file="MailSenderTests.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.Test.MailSenderTests
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using NUnit.Framework;

    using Rhino.Mocks;

    using RuleBender.Interface;
    using RuleBender.Logic;
    using RuleBender.RuleParsers;

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
            var ruleParsers = new List<IRuleParser>
                              {
                                  new UglyFlatRuleParser(),
                                  new CommentedFlatRuleParser(),
                                  new StragetyRuleParser(),
                                  new EasyTestStragetyRuleParser()
                              };

            this.ruleRepo       = MockRepository.GenerateStrictMock<IRuleRepo>();
            this.emailService   = MockRepository.GenerateStrictMock<IEmailService>();
            this.ruleParser     = ruleParsers[0];

            this.sender = new MailSender(this.ruleRepo, this.emailService, this.ruleParser);
        }

        [TearDown]
        public void TearDown()
        {
            this.ruleRepo       .VerifyAllExpectations();
            this.emailService   .VerifyAllExpectations();
        }

        #region [ Tests ]



        #endregion
    }
}
