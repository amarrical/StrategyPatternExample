//-----------------------------------------------------------------------
// <copyright file="DayOfWeekOfMonthOfYearMatcher.cs" company="ImprovingEnterprises">
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
    /// Matches MailRules which are configured to run on a particular day of week of a particular week of a particular month every X years. 
    /// </summary>
    public class DayOfWeekOfMonthOfYearMatcher : IMailRuleMatcher
    {
        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="DayOfWeekOfMonthOfYearMatcher"/> class.
        /// </summary>
        public DayOfWeekOfMonthOfYearMatcher()
        {
            this.SubMatchers = new List<ISubMatcher>
                               {
                                   new IsYearlyRecurrenceMetSubMatcher(),
                                   new IsMonthOfYearSubMatcher(),
                                   new IsDayOfWeekSubMatcher(),
                                   new IsWeekOfMonthSubMatcher()
                               };
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets the SubMatchers which help determine which MailRules should be sent.
        /// </summary>
        public IList<ISubMatcher> SubMatchers { get; private set; }

        #endregion

        #region [ IMailRuleMatcher Methods ]

        /// <summary>
        /// Determines if this is the proper Matcher for the mail rule.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <returns>A value indicating whether this rule matcher can handle this rule.</returns>
        public bool IsProperMatcher(MailRule rule)
        {
            return rule.MailPattern == MailPattern.Yearly   // MailRule is set to a yearly pattern.
                   && rule.IsDayOfWeekRestricted            // MailRule is restricted to certain days of the week.
                   && rule.DayNumber.HasValue;              // MailRule is set to a certain week of the month (when IsDayOfWeekRestricted is true)
        }

        /// <summary>
        /// Determines if the mail rule should be run at this time.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <param name="startTime">The time at which the process started.</param>
        /// <returns>A value indicating whether the rule should be ran.</returns>
        public bool ShouldBeRun(MailRule rule, DateTime startTime)
        {
            return this.SubMatchers.All(sm => sm.ShouldBeRun(rule, startTime));
        } 

        #endregion
    }
}