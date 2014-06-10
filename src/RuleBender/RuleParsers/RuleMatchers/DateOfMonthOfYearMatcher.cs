//-----------------------------------------------------------------------
// <copyright file="DateOfMonthOfYearMatcher.cs" company="ImprovingEnterprises">
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
    /// Matches a rule set to run on a day of a month every X number of years (X can equal 1)
    /// </summary>
    public class DateOfMonthOfYearMatcher : IMailRuleMatcher
    {
        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="DateOfMonthOfYearMatcher"/> class.
        /// </summary>
        public DateOfMonthOfYearMatcher()
        {
            this.SubMatchers = new List<ISubMatcher>
                               {
                                   new IsDayOfMonthSubMatcher(),
                                   new IsMonthOfYearSubMatcher(),
                                   new IsYearlyRecurrenceMetSubMatcher()
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
            return rule.MailPattern == MailPattern.Yearly   // Rule runs on a yearly pattern.
                   && !rule.IsDayOfWeekRestricted;          // Rule is not restricted to a day of week.
        }

        /// <summary>
        /// Determines if the mail rule should be run at this time.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <param name="startTime">The time at which the process started.</param>
        /// <returns>A value indicating whether the rule should be ran.</returns>
        public bool ShouldBeRun(MailRule rule, DateTime startTime)
        {
            // Rule satisfies all the SubMatchers (day of month, month of year, yearly recurrence).
            return this.SubMatchers.All(sm => sm.ShouldBeRun(rule, startTime));
        }

        #endregion
    }
}