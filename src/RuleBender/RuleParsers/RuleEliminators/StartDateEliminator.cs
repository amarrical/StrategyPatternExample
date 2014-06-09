//-----------------------------------------------------------------------
// <copyright file="StartDateEliminator.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.RuleParsers.RuleEliminators
{
    using System;

    using RuleBender.Entity;

    /// <summary>
    /// Eliminates a mail rule if it has a StartDate and the StartDate is not yet met.
    /// </summary>
    public class StartDateEliminator : IMailRuleEliminator
    {
        #region [ IMailRuleEliminator Methods ]

        /// <summary>
        /// Determines if this is the proper Eliminator for the mail rule.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <returns>A value indicating whether this eliminator can handle this rule.</returns>
        public bool IsProperEliminator(MailRule rule)
        {
            // Applies to mail rules which have a start date.
            return rule.StartDate.HasValue;
        }

        /// <summary>
        /// Determines if the mail rule should be eliminated from the list.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <param name="startTime">The time at which the process started.</param>
        /// <returns>A value indicating whether the rule can be eliminated.</returns>
        public bool ShouldBeEliminated(MailRule rule, DateTime startTime)
        {
            // Eliminate if today is before the start date.
            return rule.StartDate.Value.Date < startTime.Date; 
        } 

        #endregion
    }
}