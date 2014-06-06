//-----------------------------------------------------------------------
// <copyright file="IMailRuleMatcher.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.RuleParsers.RuleMatchers
{
    using System;

    using RuleBender.Entity;

    /// <summary>
    /// Interface describing necessary methods to match mail rules which should be run.
    /// </summary>
    public interface IMailRuleMatcher
    {
        #region [ Methods ]

        /// <summary>
        /// Determines if this is the proper Matcher for the mail rule.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <returns>A value indicating whether this rule matcher can handle this rule.</returns>
        bool IsProperMatcher(MailRule rule);

        /// <summary>
        /// Determines if the mail rule should be run at this time.
        /// </summary>
        /// <param name="rule">The MailRule to be evaluated.</param>
        /// <param name="startTime">The time at which the process started.</param>
        /// <returns>A value indicating whether the rule should be ran.</returns>
        bool ShouldBeRun(MailRule rule, DateTime startTime); 

        #endregion
    }
}