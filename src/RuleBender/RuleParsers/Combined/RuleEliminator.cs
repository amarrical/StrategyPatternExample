//-----------------------------------------------------------------------
// <copyright file="RuleEliminator.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.RuleParsers.Combined
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RuleBender.Entity;
    using RuleBender.RuleParsers.RuleEliminators;

    /// <summary>
    /// Implements IRuleEliminator to return a list of MailRules to be matched.
    /// </summary>
    public class RuleEliminator : IRuleEliminator
    {
        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleEliminator"/> class.
        /// </summary>
        /// <param name="eliminators">Eliminators to determine which MailRules can be removed.</param>
        public RuleEliminator(List<IMailRuleEliminator> eliminators = null)
        {
            this.Eliminators = eliminators ?? new List<IMailRuleEliminator>
                                              {
                                                  new InactiveEliminator(),
                                                  new MaxRecurrencesEliminator(),
                                                  new PastEndDateEliminiator(),
                                                  new RanTodayEliminator(),
                                                  new StartDateEliminator()
                                              };
        } 

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets the eliminators which determine which mail rules can be removed.
        /// </summary>
        public List<IMailRuleEliminator> Eliminators { get; private set; }

        #endregion

        #region [ IRuleEliminator Methods ]

        /// <summary>
        /// Determines which mail rules are not eliminated.
        /// </summary>
        /// <param name="mailRules">MailRules to be parsed for eliminated.</param>
        /// <param name="startTime">Start time of the process.</param>
        /// <returns>A collection of MailRules which have not been eliminated.</returns>
        public IList<MailRule> GetMailRulesNotEliminated(IEnumerable<MailRule> mailRules, DateTime startTime)
        {
            return
                mailRules.ToList()
                         .Where(rule => this.Eliminators.Where(e => e.IsProperEliminator(rule)).All(e => !e.ShouldBeEliminated(rule, startTime)))
                         .ToList();
        } 

        #endregion
    }
}