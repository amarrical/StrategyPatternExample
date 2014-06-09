//-----------------------------------------------------------------------
// <copyright file="IsWeekOfMonthSubMatcher.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.RuleParsers.RuleMatchers
{
    using System;

    using RuleBender.Entity;

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
            // ToDo: Test the crap out of this.
            var targetWeek = rule.DayNumber.GetValueOrDefault();
            var monthStart = new DateTime(startTime.Year, startTime.Month, 1).DayOfWeek;
            var dayOfMonth = monthStart - startTime.DayOfWeek;
            if (dayOfMonth < 0)
                dayOfMonth += 7;

            return startTime.AddDays(dayOfMonth * targetWeek).Month == startTime.Month;
        }

        #endregion
    }
}