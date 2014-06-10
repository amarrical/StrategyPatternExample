//-----------------------------------------------------------------------
// <copyright file="RuleMatcher.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.RuleParsers.Combined
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RuleBender.Entity;
    using RuleBender.RuleParsers.RuleMatchers;

    /// <summary>
    /// Implements IRuleMatcher to return a list of MailRules which send their messages.
    /// </summary>
    public class RuleMatcher : IRuleMatcher
    {
        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleMatcher"/> class.
        /// </summary>
        /// <param name="matchers">A collection of IMailRuleMatchers to determine which MailRules should send messages.</param>
        public RuleMatcher(List<IMailRuleMatcher> matchers = null)
        {
            this.Matchers = matchers ?? new List<IMailRuleMatcher>
                                        {
                                            new WeeklyMatcher(),
                                            new DateOfMonthMatcher(),
                                            new DayOfWeekOfMonthOfYearMatcher(),
                                            new DayOfWeekOfMonthHandler(),
                                            new DateOfMonthOfYearMatcher(),
                                            new EveryDayMatcher(),
                                            new EveryWeekDayMatcher()
                                        };
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets the Matchers used to determine which MailRules should send messages.
        /// </summary>
        public List<IMailRuleMatcher> Matchers { get; private set; }

        #endregion

        #region [ IRuleMatcher Methods ]

        /// <summary>
        /// Determines which MailRules need to send out their messages.
        /// </summary>
        /// <param name="mailRules">MailRules to be evaluated to determine if they should send messages.</param>
        /// <param name="startTime">Start time of the process.</param>
        /// <returns>A collection of MailRules which should send messages.</returns>
        public IList<MailRule> GetMatchedRules(IEnumerable<MailRule> mailRules, DateTime startTime)
        {
            return
                mailRules.ToList()
                         .Where(rule => this.Matchers.Where(m => m.IsProperMatcher(rule)).Any(m => m.ShouldBeRun(rule, startTime)))
                         .ToList();
        } 

        #endregion
    }
}