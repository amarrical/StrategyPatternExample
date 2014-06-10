//-----------------------------------------------------------------------
// <copyright file="IRuleParser.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.Interface
{
    using System;
    using System.Collections.Generic;

    using RuleBender.Entity;

    /// <summary>
    /// Interface describing necessary methods to parse MailRules for the application.
    /// </summary>
    public interface IRuleParser
    {
        #region [ Methods ]

        /// <summary>
        /// Parses rules to determine which rules should be run.
        /// </summary>
        /// <param name="mailRules">The MailRules to be evaluated.</param>
        /// <param name="startTime">Time at which the process started.</param>
        /// <returns>A collection of MailRules which need to be run.</returns>
        IList<MailRule> ParseRules(IEnumerable<MailRule> mailRules, DateTime startTime); 

        #endregion
    }
}