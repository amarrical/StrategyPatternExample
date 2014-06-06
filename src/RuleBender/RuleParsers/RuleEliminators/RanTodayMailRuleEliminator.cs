﻿//-----------------------------------------------------------------------
// <copyright file="RanTodayMailRuleEliminator.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.RuleParsers.RuleEliminators
{
    using System;

    using RuleBender.Entity;

    /// <summary>
    /// Eliminates a MailRule if that MailRule has already been run today.
    /// </summary>
    public class RanTodayMailRuleEliminator : IMailRuleEliminator
    {
        #region [ IMailRuleEliminator Methods ]

        /// <summary>
        /// Determines if this is the proper Eliminator for the mail rule.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <returns>A value indicating whether this eliminator can handle this rule.</returns>
        public bool IsProperEliminator(MailRule rule)
        {
            // Applies to all mail rules.
            return true;
        }

        /// <summary>
        /// Determines if the mail rule should be eliminated from the list.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <param name="startTime">The time at which the process started.</param>
        /// <returns>A value indicating whether the rule can be eliminated.</returns>
        public bool ShouldBeEliminated(MailRule rule, DateTime startTime)
        {
            // If the rule has run today, eliminate it.
            return rule.LastSent.GetValueOrDefault().Date == startTime.Date;
        } 

        #endregion
    }
}