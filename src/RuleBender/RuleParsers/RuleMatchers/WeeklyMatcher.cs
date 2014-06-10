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
        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="WeeklyMatcher"/> class.
        /// </summary>
        public WeeklyMatcher()
        {
            this.SubMatchers = new List<ISubMatcher>
                            {
                                new IsDayOfWeekSubMatcher()
                            };
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets the SubMatchers which make up the matching criteria.
        /// </summary>
        public IList<ISubMatcher> SubMatchers { get; private set; }

        #endregion

        #region [ IRuleMatcher Methods ]

        /// <summary>
        /// Determines if this is the proper Matcher for the mail rule.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <returns>A value indicating whether this rule matcher can handle this rule.</returns>
        public bool IsProperMatcher(MailRule rule)
        {
            return rule.MailPattern == MailPattern.Weekly   // Rule follows the Weekly pattern.
                   && rule.IsDayOfWeekRestricted;           // Rule is restricted to a day of week.
        }

        /// <summary>
        /// Determines if the mail rule should be run at this time.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <param name="startTime">The time at which the process started.</param>
        /// <returns>A value indicating whether the rule should be ran.</returns>
        public bool ShouldBeRun(MailRule rule, DateTime startTime)
        {
            return this.SubMatchers.All(sr => sr.ShouldBeRun(rule, startTime))                                          // Matches the day of week.
                   && rule.LastSent.GetValueOrDefault().AddDays(rule.NumberOf.GetValueOrDefault(1) * 7) <= startTime;   // Has not run this week
        } 

        #endregion
    }
}