//-----------------------------------------------------------------------
// <copyright file="IMailRuleEliminator.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.RuleParsers.RuleEliminators
{
    using System;

    using RuleBender.Entity;

    /// <summary>
    /// Interface describing necessary methods to eliminate mail rules.
    /// </summary>
    public interface IMailRuleEliminator
    {
        #region [ Methods ]

        /// <summary>
        /// Determines if this is the proper Eliminator for the mail rule.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <returns>A value indicating whether this eliminator can handle this rule.</returns>
        bool IsProperEliminator(MailRule rule);

        /// <summary>
        /// Determines if the mail rule should be eliminated from the list.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <param name="startTime">The time at which the process started.</param>
        /// <returns>A value indicating whether the rule can be eliminated.</returns>
        bool ShouldBeEliminated(MailRule rule, DateTime startTime); 

        #endregion
    }
}