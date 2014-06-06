//-----------------------------------------------------------------------
// <copyright file="MailSender.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.Logic
{
    using System;

    using RuleBender.Interface;

    /// <summary>
    /// Sends mail for the application.
    /// This is what we are testing.
    /// </summary>
    public class MailSender
    {
        #region [ Fields ]

        /// <summary>
        /// Repository to handle persistence of email sending rules.
        /// </summary>
        private readonly IRuleRepo ruleRepo;

        /// <summary>
        /// Service to send emails.
        /// </summary>
        private readonly IEmailService emailService;

        /// <summary>
        /// Parser to determine which rules should be sent.
        /// </summary>
        private readonly IRuleParser ruleParser;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the MailSender class.
        /// </summary>
        /// <param name="ruleRepo">Repository to handle persistence of email sending rules.</param>
        /// <param name="emailService">Service to send emails.</param>
        /// <param name="ruleParser">Parser to determine which rules should be sent.</param>
        public MailSender(IRuleRepo ruleRepo, IEmailService emailService, IRuleParser ruleParser)
        {
            this.ruleRepo = ruleRepo;
            this.emailService = emailService;
            this.ruleParser = ruleParser;
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Sends out email messages according to the existing rules.
        /// </summary>
        /// <param name="startTime">The start time of the process.</param>
        public void SendMessages(DateTime startTime)
        {
            var rules = this.ruleRepo.GetMailRules();
            var rulesToRun = this.ruleParser.ParseRules(rules, startTime);
            var sentRules = this.emailService.Send(rulesToRun, startTime);
            this.ruleRepo.SaveRunRules(sentRules);
        }

        #endregion
    }
}
