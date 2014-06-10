//-----------------------------------------------------------------------
// <copyright file="EveryWeekDayMatcher.cs" company="ImprovingEnterprises">
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
    /// Matches a rule set to run every week on particular days of the week (usually weekdays).
    /// </summary>
    public class EveryWeekDayMatcher : IMailRuleMatcher
    {
        #region [ Constructor ]

        /// <summary>
        /// Initializes a new instance of the <see cref="EveryWeekDayMatcher"/> class.
        /// </summary>
        public EveryWeekDayMatcher()
        {
            this.SubMatchers = new List<ISubMatcher>
                {
                    new IsDayOfWeekSubMatcher()
                };
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets the SubMatchers which help determine which MailRules should be sent.
        /// </summary>
        public IList<ISubMatcher> SubMatchers { get; private set; }

        #endregion

        #region [ IMailRuleMatcherMethods ]

        /// <summary>
        /// Determines if this is the proper Matcher for the mail rule.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <returns>A value indicating whether this rule matcher can handle this rule.</returns>
        public bool IsProperMatcher(MailRule rule)
        {
            return rule.MailPattern == MailPattern.Daily    // Rule follows the daily pattern
                   && rule.IsDayOfWeekRestricted;           // Rule is restricted to certain days of the week.
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