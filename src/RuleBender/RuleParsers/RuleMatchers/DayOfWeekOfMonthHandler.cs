//-----------------------------------------------------------------------
// <copyright file="DayOfWeekOfMonthHandler.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.RuleParsers.RuleMatchers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RuleBender.Entity;
    using RuleBender.RuleParsers.RuleMatchers.SubMatchers;

    /// <summary>
    /// Matches a MailRule configure to be run on a certain days of week of a week in a month.
    /// </summary>
    public class DayOfWeekOfMonthHandler : IMailRuleMatcher
    {
        #region [ Fields ]

        /// <summary>
        /// SubMatchers which help determine which MailRules should be sent.
        /// </summary>
        private readonly IList<ISubMatcher> subMatchers;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="DayOfWeekOfMonthHandler"/> class.
        /// </summary>
        public DayOfWeekOfMonthHandler()
        {
            this.subMatchers = new List<ISubMatcher>
                               {
                                   new IsDayOfWeekSubMatcher(),
                                   new IsWeekOfMonthSubMatcher(),
                                   new IsMonthlyRecurrenceMetSubMatcher()
                               };
        }

        #endregion

        #region [ IMailRuleMatcher Methods ]

        /// <summary>
        /// Determines if this is the proper Matcher for the mail rule.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <returns>A value indicating whether this rule matcher can handle this rule.</returns>
        public bool IsProperMatcher(MailRule rule)
        {
            return rule.MailPattern == MailPattern.Montly   // Rule follows the monthly pattern.
                   && rule.DayNumber.HasValue               // Rule is set to repeat on weekly pattern.
                   && rule.IsDayOfWeekRestricted;             // Rule is restricted to run on certain days of the week.
        }

        /// <summary>
        /// Determines if the mail rule should be run at this time.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <param name="startTime">The time at which the process started.</param>
        /// <returns>A value indicating whether the rule should be ran.</returns>
        public bool ShouldBeRun(MailRule rule, DateTime startTime)
        {
            return this.subMatchers.All(sm => sm.ShouldBeRun(rule, startTime)); // All SubMatchers satisfied (day of week, week of month, monthly recurrence)
        } 

        #endregion
    }
}