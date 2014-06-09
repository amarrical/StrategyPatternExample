//-----------------------------------------------------------------------
// <copyright file="IsDayOfMonthSubMatcher.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.RuleParsers.RuleMatchers.SubMatchers
{
    using System;

    using RuleBender.Entity;

    /// <summary>
    /// Matches if the start time is equal to the day of month the MailRule is configured to run on.
    /// </summary>
    public class IsDayOfMonthSubMatcher : ISubMatcher
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
            return rule.DayNumber.GetValueOrDefault() == startTime.Day;
        }

        #endregion
    }
}