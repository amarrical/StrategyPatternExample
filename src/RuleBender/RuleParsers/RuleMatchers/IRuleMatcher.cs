//-----------------------------------------------------------------------
// <copyright file="IRuleMatcher.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.RuleParsers.RuleMatchers
{
    using System;
    using System.Collections.Generic;

    using RuleBender.Entity;

    /// <summary>
    /// Interface describing methods necessary to be an all-in-one rule matcher.
    /// </summary>
    public interface IRuleMatcher
    {
        #region [ Methods ]

        /// <summary>
        /// Determines which MailRules need to send out their messages.
        /// </summary>
        /// <param name="mailRules">MailRules to be evaluated to determine if they should send messages.</param>
        /// <param name="startTime">Start time of the process.</param>
        /// <returns>A collection of MailRules which should send messages.</returns>
        IList<MailRule> GetMatchedRules(IList<MailRule> mailRules, DateTime startTime);

        #endregion
    }
}
