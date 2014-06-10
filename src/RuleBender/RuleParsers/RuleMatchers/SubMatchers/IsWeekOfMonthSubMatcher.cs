//-----------------------------------------------------------------------
// <copyright file="IsWeekOfMonthSubMatcher.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.RuleParsers.RuleMatchers.SubMatchers
{
    using System;

    using RuleBender.Entity;
    using RuleBender.Extensions;

    /// <summary>
    /// Matches if the start time is the proper week of the month configured in the MailRule.
    /// </summary>
    public class IsWeekOfMonthSubMatcher : ISubMatcher
    {
        #region [ ISubMatcher Methods ]

        /// <summary>
        /// Determines if a rule matches the SubRule.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <param name="startTime">The time at which the process started.</param>
        /// <returns>A value indicating whether the rule matches the SubRule.</returns>
        public bool ShouldBeRun(MailRule rule, DateTime startTime)
        {
            return rule.DayNumber.Value == startTime.GetWeekOfMonth();
        }

        #endregion
    }
}