//-----------------------------------------------------------------------
// <copyright file="ISubMatcher.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.RuleParsers.RuleMatchers.SubMatchers
{
    using System;

    using RuleBender.Entity;

    /// <summary>
    /// Interface describing sub rules to matching rules.
    /// </summary>
    public interface ISubMatcher
    {
        /// <summary>
        /// Determines if a rule matches the SubRule.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <param name="startTime">The time at which the process started.</param>
        /// <returns>A value indicating whether the rule matches the SubRule.</returns>
        bool ShouldBeRun(MailRule rule, DateTime startTime);
    }
}