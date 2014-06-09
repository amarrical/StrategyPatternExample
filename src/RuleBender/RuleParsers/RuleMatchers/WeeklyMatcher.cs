//-----------------------------------------------------------------------
// <copyright file="WeeklyMatcher.cs" company="ImprovingEnterprises">
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
    /// Matches a MailRule which runs every number of weeks on a particular day of the week.
    /// </summary>
    public class WeeklyMatcher : IMailRuleMatcher
    {
        #region [ Fields ]

        /// <summary>
        /// Sub Rules which make up the matching criteria.
        /// </summary>
        private readonly IList<ISubMatcher> subRules;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="WeeklyMatcher"/> class.
        /// </summary>
        public WeeklyMatcher()
        {
            this.subRules = new List<ISubMatcher>
                            {
                                new IsDayOfWeekSubMatcher()
                            };
        }

        #endregion

        #region [ IRuleMatcher Methods ]

        /// <summary>
        /// Determines if this is the proper Matcher for the mail rule.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <returns>A value indicating whether this rule matcher can handle this rule.</returns>
        public bool IsProperMatcher(MailRule rule)
        {
            // Is a weekly run mail pattern which runs certain numbers of weeks.
            return rule.MailPattern == MailPattern.Weekly
                   && rule.NumberOf.HasValue;
        }

        /// <summary>
        /// Determines if the mail rule should be run at this time.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <param name="startTime">The time at which the process started.</param>
        /// <returns>A value indicating whether the rule should be ran.</returns>
        public bool ShouldBeRun(MailRule rule, DateTime startTime)
        {
            return this.subRules.All(sr => sr.ShouldBeRun(rule, startTime))                             // Matches the day of week.
                   && rule.LastSent.GetValueOrDefault().AddDays(rule.NumberOf.Value * 7) <= startTime;  // Has not run this week
        } 

        #endregion
    }
}