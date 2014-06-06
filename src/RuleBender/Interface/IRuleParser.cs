//-----------------------------------------------------------------------
// <copyright file="IRuleParser.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.Logic
{
    using System;
    using System.Collections.Generic;

    using RuleBender.Entity;

    /// <summary>
    /// Interface describing necessary methods to parse mail rules for the application.
    /// </summary>
    public interface IRuleParser
    {
        #region [ Methods ]

        /// <summary>
        /// Parses rules to determine which rules should be run.
        /// Based on those rules, it pulls/creates the mail messages to send out.
        /// </summary>
        /// <param name="mailRules">The mail rules to be evaluated.</param>
        /// <param name="startTime">Time at which the process started.</param>
        /// <returns>A collection of MailRules which need to be run.</returns>
        IList<MailRule> ParseRules(IList<MailRule> mailRules, DateTime startTime); 

        #endregion
    }
}