//-----------------------------------------------------------------------
// <copyright file="RuleEliminator.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.RuleParsers.RuleEliminators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RuleBender.Entity;

    /// <summary>
    /// Implements IRuleEliminator to return a list of MailRules to be matched.
    /// </summary>
    public class RuleEliminator : IRuleEliminator
    {
        #region [ Fields ]

        /// <summary>
        /// Eliminators to determine which mail rules can be removed.
        /// </summary>
        private readonly List<IMailRuleEliminator> eliminators;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleEliminator"/> class.
        /// </summary>
        /// <param name="eliminators">Eliminators to determine which MailRules can be removed.</param>
        public RuleEliminator(List<IMailRuleEliminator> eliminators = null)
        {
            this.eliminators = eliminators ?? new List<IMailRuleEliminator>
                                              {
                                                  new InactiveMailRuleEliminator(),
                                                  new MaxRecurrencesMailRuleEliminator(),
                                                  new PastEndDateEliminiator(),
                                                  new RanTodayMailRuleEliminator()
                                              };
        } 

        #endregion

        #region [ IRuleEliminator Methods ]

        /// <summary>
        /// Determines which mail rules are not eliminated.
        /// </summary>
        /// <param name="mailRules">MailRules to be parsed for eliminated.</param>
        /// <param name="startTime">Start time of the process.</param>
        /// <returns>A collection of mail rules which have not been eliminated.</returns>
        public IList<MailRule> GetMailRulesNotEliminated(IEnumerable<MailRule> mailRules, DateTime startTime)
        {
            return mailRules.ToList().Where(rule => this.eliminators.Where(e => e.IsProperEliminator(rule)).All(e => !e.ShouldBeEliminated(rule, startTime))).ToList();
        } 

        #endregion
    }
}