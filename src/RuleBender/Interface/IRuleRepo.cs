//-----------------------------------------------------------------------
// <copyright file="IRuleRepo.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.Interface
{
    using System.Collections.Generic;

    using RuleBender.Entity;

    /// <summary>
    /// Interface which describes necessary methods for handling MailRule persistence.
    /// </summary>
    public interface IRuleRepo
    {
        #region [ Methods ]

        /// <summary>
        /// Gets all mail rules from store.
        /// </summary>
        /// <returns>A collection of MailRules</returns>
        IList<MailRule> GetMailRules();

        /// <summary>
        /// Saves the updated rules which were run.
        /// </summary>
        /// <param name="mailRules">The rules which need to be updated.</param>
        void SaveRunRules(IList<MailRule> mailRules);

        #endregion
    }
}