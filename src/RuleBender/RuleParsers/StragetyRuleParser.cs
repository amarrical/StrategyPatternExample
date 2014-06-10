//-----------------------------------------------------------------------
// <copyright file="StragetyRuleParser.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.RuleParsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RuleBender.Entity;
    using RuleBender.Interface;
    using RuleBender.RuleParsers.RuleEliminators;
    using RuleBender.RuleParsers.RuleMatchers;

    /// <summary>
    /// IRuleParser which uses strategy pattern, but isn't all that easy to test as a unit.
    /// </summary>
    public class StragetyRuleParser : IRuleParser
    {
        #region [ Fields ]

        /// <summary>
        /// Collection of eliminators to determine which rules should not be run.
        /// </summary>
        private readonly List<IMailRuleEliminator> eliminators;

        /// <summary>
        /// Collection of matchers to determine which rules should be run.
        /// </summary>
        private readonly List<IMailRuleMatcher> matchers;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="StragetyRuleParser"/> class.
        /// </summary>
        public StragetyRuleParser()
        {
            this.eliminators = new List<IMailRuleEliminator>
                               {
                                   new InactiveEliminator(),
                                   new MaxRecurrencesEliminator(),
                                   new PastEndDateEliminiator(),
                                   new RanTodayEliminator()
                               };

            this.matchers = new List<IMailRuleMatcher>
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

        #region [ IRuleParser Methods ]

        /// <summary>
        /// Parses rules to determine which rules should be run.
        /// </summary>
        /// <param name="mailRules">The mail rules to be evaluated.</param>
        /// <param name="startTime">Time at which the process started.</param>
        /// <returns>A collection of MailRules which need to be run.</returns>
        public IList<MailRule> ParseRules(IEnumerable<MailRule> mailRules, DateTime startTime)
        {
            return
                mailRules.ToList()
                         .Where(rule => this.eliminators.Where(e => e.IsProperEliminator(rule)).All(e => !e.ShouldBeEliminated(rule, startTime)))
                         .ToList()
                         .Where(rule => this.matchers.Where(m => m.IsProperMatcher(rule)).Any(m => m.ShouldBeRun(rule, startTime)))
                         .ToList();

            /*
            var rules = mailRules.ToList();
            var rulesToMatch = rules.Where(rule => this.eliminators.Where(e => e.IsProperEliminator(rule)).All(e => !e.ShouldBeEliminated(rule, startTime))).ToList();
            var matchedRules = rulesToMatch.Where(rule => this.matchers.Where(m => m.IsProperMatcher(rule)).Any(m => m.ShouldBeRun(rule, startTime))).ToList();
            return matchedRules;
            */

            /*
            var rulesToMatch = new List<MailRule>();
            foreach (var rule in mailRules)
            {
                var isGood = true;
                foreach (var eliminator in this.eliminators)
                {
                    if (eliminator.IsProperEliminator(rule))
                        if (eliminator.ShouldBeEliminated(rule, startTime))
                        {
                            isGood = false;
                            break;
                        }
                }

                if (isGood)
                {
                    rulesToMatch.Add(rule);
                }
            }

            var rulesToRun = new List<MailRule>();
            foreach (var rule in rulesToMatch)
            {
                foreach (var matcher in this.matchers)
                {
                    if(matcher.IsProperMatcher(rule))
                        if (matcher.ShouldBeRun(rule, startTime))
                        {
                            rulesToRun.Add(rule);
                            break;
                        }
                }
            }

            return rulesToRun;
            */
        }

        #endregion
    }
}