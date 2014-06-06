//-----------------------------------------------------------------------
// <copyright file="IRuleEliminator.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.RuleParsers.RuleEliminators
{
    using System;
    using System.Collections.Generic;

    using RuleBender.Entity;

    /// <summary>
    /// Interface describing methods necessary to be an all-in-one rule eliminator.
    /// </summary>
    public interface IRuleEliminator
    {
        #region [ Methods ]

        /// <summary>
        /// Determines which MailRules are not eliminated.
        /// </summary>
        /// <param name="mailRules">MailRules to be parsed for eliminated.</param>
        /// <param name="startTime">Start time of the process.</param>
        /// <returns>A collection of mail rules which have not been eliminated.</returns>
        IList<MailRule> GetMailRulesNotEliminated(IEnumerable<MailRule> mailRules, DateTime startTime); 

        #endregion
    }
}
