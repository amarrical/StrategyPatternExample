﻿//-----------------------------------------------------------------------
// <copyright file="DayOfMonthHandler.cs" company="ImprovingEnterprises">
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

    /// <summary>
    /// Handles matching a rule if it is set to run on a particular day of a month.
    /// </summary>
    public class DayOfMonthHandler : IMailRuleMatcher
    {
        #region [ Fields ]

        /// <summary>
        /// Sub Rules which make up the matching criteria.
        /// </summary>
        private readonly IList<ISubMatcher> subRules;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="DayOfMonthHandler"/> class.
        /// </summary>
        /// <param name="subRules">Sub Rules which make up the matching criteria.</param>
        public DayOfMonthHandler(IList<ISubMatcher> subRules = null)
        {
            this.subRules = subRules ?? new List<ISubMatcher>
                {
                    new IsDayOfMonthSubMatcher(),
                    new IsMonthlyRecurrenceMetSubMatcher()
                };
        }

        #endregion

        /// <summary>
        /// Determines if this is the proper Matcher for the mail rule.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <returns>A value indicating whether this rule matcher can handle this rule.</returns>
        public bool IsProperMatcher(MailRule rule)
        {
            return rule.MailPattern == MailPattern.Montly   // Pattern is set to monthly
                   && rule.DayNumber.HasValue               // Rule is set to a particular day of the month 
                   && !rule.DayOfWeekRestricted;            // Rule is not set to run on particular days of the week.
        }

        /// <summary>
        /// Determines if the mail rule should be run at this time.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <param name="startTime">The time at which the process started.</param>
        /// <returns>A value indicating whether the rule should be ran.</returns>
        public bool ShouldBeRun(MailRule rule, DateTime startTime)
        {
            return this.subRules.All(sr => sr.ShouldBeRun(rule, startTime));
        }
    }
}