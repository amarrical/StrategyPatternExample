//-----------------------------------------------------------------------
// <copyright file="CommentedFlatRuleParser.cs" company="ImprovingEnterprises">
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
    using RuleBender.Extensions;
    using RuleBender.Interface;

    /// <summary>
    /// Implements IRuleParser with no strategy pattern.
    /// But at least it's commented, right?
    /// </summary>
    public class CommentedFlatRuleParser : IRuleParser
    {
        #region [ IRuleParser Methods ]

        /// <summary>
        /// Parses rules to determine which rules should be run.
        /// </summary>
        /// <param name="mailRules">The mail rules to be evaluated.</param>
        /// <param name="startTime">Time at which the process started.</param>
        /// <returns>A collection of MailRules which need to be run.</returns>
        public IList<MailRule> ParseRules(IEnumerable<MailRule> mailRules, DateTime startTime)
        {
            var nonEliminatedRules = new List<MailRule>();

            // Eliminate MailRules
            foreach (var rule in mailRules)
            {
                var keepRule = true;
                
                // Eliminate if not Active.
                if (!rule.IsActive)
                    keepRule = false;

                // Eliminate if rule has max occurrences and has been exceeded.
                if (rule.MaxOccurences.HasValue)
                    if (rule.TimesSent >= rule.MaxOccurences)
                        keepRule = false;

                // Eliminate if rule has end date and start time is past end date.
                if (rule.EndDate.HasValue)
                    if (rule.EndDate > startTime)
                        keepRule = false;

                // Eliminate if rule ran today.
                if (rule.LastSent.GetValueOrDefault().Date == startTime.Date)
                    keepRule = false;

                // Eliminate if rule has start date and start time is before start date.
                if (rule.StartDate.HasValue)
                    if (rule.StartDate.Value > startTime)
                        keepRule = false;

                if (keepRule)
                    nonEliminatedRules.Add(rule);
            }

            var rulesToRun = new List<MailRule>();

            // Find mail rules which need to be run.
            foreach (var rule in nonEliminatedRules)
            {
                var runRule = false;
                switch (rule.MailPattern)
                {
                    case MailPattern.Daily:
                        // Rule is configured to run on particular days of the week.
                        if (rule.IsDayOfWeekRestricted)
                        {
                            // Correct day of week.
                            if (rule.DaysOfWeek.Any(d => d.Key == startTime.DayOfWeek && d.Value))
                                runRule = true;
                        }

                        // Rule is configured to run every NumberOf days
                        if (!rule.IsDayOfWeekRestricted)
                        {
                            // Met daily recurrence.
                            var rule1 = rule.LastSent.GetValueOrDefault().AddDays(rule.NumberOf.GetValueOrDefault(1)).Date >= startTime.Date;

                            if (rule1)
                                runRule = true;
                        }

                        break;

                    case MailPattern.Weekly:
                        // Rule is configured to run on a day of week every NumberOf weeks.
                        if (rule.IsDayOfWeekRestricted)
                        {
                            // Correct day of week.
                            var rule1 = rule.DaysOfWeek.Any(d => d.Key == startTime.DayOfWeek && d.Value);

                            // Met weekly recurrence.
                            var rule2 = rule.LastSent.GetValueOrDefault().AddDays(rule.NumberOf.GetValueOrDefault(1) * 7) <= startTime;

                            if (rule1 && rule2)
                                runRule = true;
                        }

                        break;

                    case MailPattern.Montly:
                        // Rule is configured to run on a day of month every NumberOf months.
                        if (rule.DayNumber.HasValue && !rule.IsDayOfWeekRestricted)
                        {
                            // Correct day of month.
                            var rule1 = rule.DayNumber.Value == startTime.Day;

                            // Met monthly recurrence.
                            var rule2 = rule.LastSent.GetValueOrDefault().AddMonths(rule.NumberOf.GetValueOrDefault(1)).Month <= startTime.Month;

                            if (rule1 && rule2)
                                runRule = true;
                        }

                        // Rule is configured to run on a day of a week every NumberOf months.
                        if (rule.DayNumber.HasValue && rule.IsDayOfWeekRestricted)
                        {
                            // Correct day of week.
                            var rule1 = rule.DaysOfWeek.Any(d => d.Key == startTime.DayOfWeek && d.Value);

                            // Correct week of month.
                            var rule2 = rule.DayNumber.Value == startTime.GetWeekOfMonth();

                            // Met monthly recurrence.
                            var rule3 = rule.LastSent.GetValueOrDefault().AddMonths(rule.NumberOf.GetValueOrDefault(1)).Month <= startTime.Month;

                            if (rule1 && rule2 && rule3)
                                runRule = true;
                        }

                        break;

                    case MailPattern.Yearly:
                        // Rule is configured to run on a day of a month every NumberOf years.
                        if (rule.DayNumber.HasValue && !rule.IsDayOfWeekRestricted)
                        {
                            // Correct day of month.
                            var rule1 = rule.DayNumber.Value == startTime.Day;

                            // Correct month of year.
                            var rule2 = rule.Month == startTime.Month;

                            // Met yearly recurrence.
                            var rule3 = rule.LastSent.GetValueOrDefault().AddYears(rule.NumberOf.GetValueOrDefault(1)).Year <= startTime.Year;

                            if (rule1 && rule2 && rule3)
                                runRule = true;
                        }

                        // Rule is configured to run on a day of a week of a month ever NumberOf years.
                        if (rule.IsDayOfWeekRestricted && rule.DayNumber.HasValue)
                        {
                            // Met yearly recurrence.
                            var rule1 = rule.LastSent.GetValueOrDefault().AddYears(rule.NumberOf.GetValueOrDefault(1)).Year <= startTime.Year;

                            // Correct month of year.
                            var rule2 = rule.Month == startTime.Month;

                            // Correct week of month.
                            var rule3 = rule.DayNumber.Value == startTime.GetWeekOfMonth();

                            // Correct day of week.
                            var rule4 = rule.DaysOfWeek.Any(d => d.Key == startTime.DayOfWeek && d.Value);

                            if (rule1 && rule2 && rule3 && rule4)
                                runRule = true;
                        }

                        break;
                }

                if (runRule)
                    rulesToRun.Add(rule);
            }

            return rulesToRun;
        }

        #endregion
    }
}