//-----------------------------------------------------------------------
// <copyright file="PastEndDateEliminiator.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.RuleParsers.RuleEliminators
{
    using System;

    using RuleBender.Entity;

    /// <summary>
    /// Eliminates a MailRule if it has an end date and the start date is past the end date of the MailRule.
    /// </summary>
    public class PastEndDateEliminiator : IMailRuleEliminator
    {
        #region [ IMailRuleEliminator Methods ]

        /// <summary>
        /// Determines if this is the proper Eliminator for the MailRule.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <returns>A value indicating whether this eliminator can handle this rule.</returns>
        public bool IsProperEliminator(MailRule rule)
        {
            // Applies to MailRules which have EndDate value.
            return rule.EndDate.HasValue;
        }

        /// <summary>
        /// Determines if the MailRule should be eliminated from the list.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <param name="startTime">The time at which the process started.</param>
        /// <returns>A value indicating whether the rule can be eliminated.</returns>
        public bool ShouldBeEliminated(MailRule rule, DateTime startTime)
        {
            // Eliminate if past end date.
            return rule.EndDate.Value.Date < startTime.Date;
        }

        #endregion
    }
}