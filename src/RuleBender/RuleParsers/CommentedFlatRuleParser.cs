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

    using RuleBender.Entity;
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
            throw new NotImplementedException();
        }

        #endregion
    }
}