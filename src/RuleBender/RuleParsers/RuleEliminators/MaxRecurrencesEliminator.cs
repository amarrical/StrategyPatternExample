//-----------------------------------------------------------------------
// <copyright file="MaxRecurrencesEliminator.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.RuleParsers.RuleEliminators
{
    using System;

    using RuleBender.Entity;

    /// <summary>
    /// Eliminates a rule if it has a max number of occurrences and it has been run that many times or more.
    /// </summary>
    public class MaxRecurrencesEliminator : IMailRuleEliminator
    {
        #region [ IMailRuleEliminator Methods ]

        /// <summary>
        /// Determines if this is the proper Eliminator for the mail rule.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <returns>A value indicating whether this eliminator can handle this rule.</returns>
        public bool IsProperEliminator(MailRule rule)
        {
            // Applies to MailRules with MaxOccurences set.
            return rule.MaxOccurences.HasValue;
        }

        /// <summary>
        /// Determines if the mail rule should be eliminated from the list.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <param name="startTime">The time at which the process started.</param>
        /// <returns>A value indicating whether the rule can be eliminated.</returns>
        public bool ShouldBeEliminated(MailRule rule, DateTime startTime)
        {
            // Eliminate if the rule has been it's maximum number of times.
            return rule.MaxOccurences <= rule.TimesSent;
        } 

        #endregion
    }
}