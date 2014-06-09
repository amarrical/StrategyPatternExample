//-----------------------------------------------------------------------
// <copyright file="DayOfMonthOfYearMatcher.cs" company="ImprovingEnterprises">
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
    /// Matches MailRules which are configured to run on a particular day of a particular month every X years. 
    /// </summary>
    public class DayOfMonthOfYearMatcher : IMailRuleMatcher
    {
        #region [ Fields ]

        /// <summary>
        /// SubMatchers which help determine which MailRules should be sent.
        /// </summary>
        private readonly IList<ISubMatcher> subMatchers; 

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="DayOfMonthOfYearMatcher"/> class.
        /// </summary>
        public DayOfMonthOfYearMatcher()
        {
            this.subMatchers = new List<ISubMatcher>
                               {
                                   new IsYearlyRecurrenceMetSubMatcher(),
                                   new IsMonthOfYearSubMatcher(),
                                   new IsDayOfWeekSubMatcher(),
                                   new IsWeekOfMonthSubMatcher()
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
            return rule.MailPattern == MailPattern.Yearly   // MailRule is set to a yearly pattern.
                   && rule.IsDayOfWeekRestricted;           // MailRule is restricted to certain days of the week.
        }

        /// <summary>
        /// Determines if the mail rule should be run at this time.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <param name="startTime">The time at which the process started.</param>
        /// <returns>A value indicating whether the rule should be ran.</returns>
        public bool ShouldBeRun(MailRule rule, DateTime startTime)
        {
            return this.subMatchers.All(sm => sm.ShouldBeRun(rule, startTime));
        } 

        #endregion
    }
}