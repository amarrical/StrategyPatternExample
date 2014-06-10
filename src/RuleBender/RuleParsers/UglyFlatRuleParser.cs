//-----------------------------------------------------------------------
// <copyright file="UglyFlatRuleParser.cs" company="ImprovingEnterprises">
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
    /// </summary>
    public class UglyFlatRuleParser : IRuleParser
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
            foreach (var rule in mailRules)
            {
                var keepRule = rule.IsActive;
                if (rule.MaxOccurences.HasValue)
                    if (rule.TimesSent >= rule.MaxOccurences)
                        keepRule = false;

                if (rule.EndDate.HasValue)
                    if (rule.EndDate > startTime)
                        keepRule = false;

                if (rule.LastSent.GetValueOrDefault().Date == startTime.Date)
                    keepRule = false;

                if (rule.StartDate.HasValue)
                    if (rule.StartDate.Value > startTime)
                        keepRule = false;

                if (keepRule)
                    nonEliminatedRules.Add(rule);
            }

            var rulesToRun = new List<MailRule>();
            foreach (var rule in nonEliminatedRules)
            {
                var runRule = false;
                switch (rule.MailPattern)
                {
                    case MailPattern.Daily:
                        if (rule.IsDayOfWeekRestricted)
                        {
                            if (rule.DaysOfWeek.Any(d => d.Key == startTime.DayOfWeek && d.Value))
                                runRule = true;
                        }

                        if (!rule.IsDayOfWeekRestricted)
                        {
                            if (rule.LastSent.GetValueOrDefault().AddDays(rule.NumberOf.GetValueOrDefault(1)).Date >= startTime.Date)
                                runRule = true;
                        }

                        break;

                    case MailPattern.Weekly:
                        if (rule.IsDayOfWeekRestricted)
                        {
                            if (rule.DaysOfWeek.Any(d => d.Key == startTime.DayOfWeek && d.Value)
                                && rule.LastSent.GetValueOrDefault().AddDays(rule.NumberOf.GetValueOrDefault(1) * 7) <= startTime)
                                runRule = true;
                        }

                        break;

                    case MailPattern.Montly:
                        if (rule.DayNumber.HasValue && !rule.IsDayOfWeekRestricted)
                        {
                            if (rule.DayNumber.Value == startTime.Day 
                                && rule.LastSent.GetValueOrDefault().AddMonths(rule.NumberOf.GetValueOrDefault(1)).Month <= startTime.Month)
                                runRule = true;
                        }

                        if (rule.DayNumber.HasValue && rule.IsDayOfWeekRestricted)
                        {
                            if (rule.DaysOfWeek.Any(d => d.Key == startTime.DayOfWeek && d.Value) 
                                && rule.DayNumber.Value == startTime.GetWeekOfMonth()
                                && rule.LastSent.GetValueOrDefault().AddMonths(rule.NumberOf.GetValueOrDefault(1)).Month <= startTime.Month)
                                runRule = true;
                        }

                        break;

                    case MailPattern.Yearly:
                        if (rule.DayNumber.HasValue && !rule.IsDayOfWeekRestricted)
                        {
                            if (rule.DayNumber.Value == startTime.Day 
                                && rule.Month == startTime.Month
                                && rule.LastSent.GetValueOrDefault().AddYears(rule.NumberOf.GetValueOrDefault(1)).Year <= startTime.Year)
                                runRule = true;
                        }

                        if (rule.IsDayOfWeekRestricted && rule.DayNumber.HasValue)
                        {
                            if (rule.LastSent.GetValueOrDefault().AddYears(rule.NumberOf.GetValueOrDefault(1)).Year <= startTime.Year
                                && rule.Month == startTime.Month
                                && rule.DayNumber.Value == startTime.GetWeekOfMonth()
                                && rule.DaysOfWeek.Any(d => d.Key == startTime.DayOfWeek && d.Value))
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