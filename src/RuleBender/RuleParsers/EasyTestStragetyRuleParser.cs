//-----------------------------------------------------------------------
// <copyright file="EasyTestStragetyRuleParser.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.RuleParsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RuleBender.Entity;
    using RuleBender.Interface;
    using RuleBender.RuleParsers.RuleEliminators;
    using RuleBender.RuleParsers.RuleMatchers;

    /// <summary>
    /// IRuleParser which uses strategy pattern and is easy to test.
    /// </summary>
    public class EasyTestStragetyRuleParser : IRuleParser
    {
        #region [ Fields ]

        /// <summary>
        /// Logic for eliminating MailRules which don't need to be checked for matching.
        /// </summary>
        private readonly IRuleEliminator ruleEliminator;

        /// <summary>
        /// Logic to match MailRules which need to be run.
        /// </summary>
        private readonly IRuleMatcher ruleMatcher; 

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="EasyTestStragetyRuleParser"/> class.
        /// </summary>
        /// <param name="ruleEliminator">Logic for eliminating MailRules which don't need to be checked for matching.</param>
        /// <param name="ruleMatcher">Logic to match MailRules which need to be run.</param>
        public EasyTestStragetyRuleParser(IRuleEliminator ruleEliminator = null, IRuleMatcher ruleMatcher = null)
        {
            this.ruleEliminator = ruleEliminator ?? new RuleEliminator();
            this.ruleMatcher = ruleMatcher ?? new RuleMatcher();
        }

        #endregion

        #region [ IRuleParser Methods ]

        /// <summary>
        /// Parses rules to determine which rules should be run.
        /// Based on those rules, it pulls/creates the mail messages to send out.
        /// </summary>
        /// <param name="mailRules">The mail rules to be evaluated.</param>
        /// <param name="startTime">Time at which the process started.</param>
        /// <returns>A collection of MailRules which need to be run.</returns>
        public IList<MailRule> ParseRules(IList<MailRule> mailRules, DateTime startTime)
        {
            var rules = this.ruleEliminator.GetMailRulesNotEliminated(mailRules, startTime);
            return this.ruleMatcher.GetMatchedRules(rules, startTime);
        } 

        #endregion
    }
}