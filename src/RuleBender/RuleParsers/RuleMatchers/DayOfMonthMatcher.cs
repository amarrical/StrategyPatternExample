//-----------------------------------------------------------------------
// <copyright file="DayOfMonthMatcher.cs" company="ImprovingEnterprises">
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
    /// Matches a MailRule configured to run on a particular day every X months.
    /// </summary>
    public class DayOfMonthMatcher : IMailRuleMatcher
    {
        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="DayOfMonthMatcher"/> class.
        /// </summary>
        public DayOfMonthMatcher()
        {
            this.SubRules = new List<ISubMatcher>
                {
                    new IsDayOfMonthSubMatcher(),
                    new IsMonthlyRecurrenceMetSubMatcher()
                };
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets the SubMatchers which make up the matching criteria.
        /// </summary>
        public IList<ISubMatcher> SubRules { get; private set; }

        #endregion

        #region [ IMailRuleMatcher Methods ]

        /// <summary>
        /// Determines if this is the proper Matcher for the mail rule.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <returns>A value indicating whether this rule matcher can handle this rule.</returns>
        public bool IsProperMatcher(MailRule rule)
        {
            return rule.MailPattern == MailPattern.Montly   // Pattern is set to monthly
                   && rule.DayNumber.HasValue               // Rule is set to a particular day of the month 
                   && !rule.IsDayOfWeekRestricted;            // Rule is not set to run on particular days of the week.
        }

        /// <summary>
        /// Determines if the mail rule should be run at this time.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <param name="startTime">The time at which the process started.</param>
        /// <returns>A value indicating whether the rule should be ran.</returns>
        public bool ShouldBeRun(MailRule rule, DateTime startTime)
        {
            return this.SubRules.All(sr => sr.ShouldBeRun(rule, startTime)); // All SubMatchers satisfied (DayOfMonth, MonthlyRecurrence).
        } 

        #endregion
    }
}