//-----------------------------------------------------------------------
// <copyright file="EveryDayMatcher.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.RuleParsers.RuleMatchers
{
    using System;

    using RuleBender.Entity;

    /// <summary>
    /// Matches a MailRule that runs every day or every X number of days.
    /// </summary>
    public class EveryDayMatcher : IMailRuleMatcher
    {
        #region [ IRuleMatcher Methods ]

        /// <summary>
        /// Determines if this is the proper Matcher for the mail rule.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <returns>A value indicating whether this rule matcher can handle this rule.</returns>
        public bool IsProperMatcher(MailRule rule)
        {
            return rule.MailPattern == MailPattern.Daily    // Runs on the daily pattern.
                   && !rule.IsDayOfWeekRestricted;            // Is not restricted to certain days of the week.
        }

        /// <summary>
        /// Determines if the mail rule should be run at this time.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <param name="startTime">The time at which the process started.</param>
        /// <returns>A value indicating whether the rule should be ran.</returns>
        public bool ShouldBeRun(MailRule rule, DateTime startTime)
        {
            return !rule.NumberOf.HasValue                                                                      // Rule is not restricted to run on multiples of days.
                   || rule.LastSent.GetValueOrDefault().AddDays(rule.NumberOf.Value).Date >= startTime.Date;    // or Rule has exceeded number of days since last run.
        } 

        #endregion
    }
}