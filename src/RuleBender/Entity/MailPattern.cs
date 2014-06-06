//-----------------------------------------------------------------------
// <copyright file="MailPattern.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.Entity
{
    /// <summary>
    /// Enumeration for mail patterns.
    /// </summary>
    public enum MailPattern
    {
        /// <summary>
        /// Pattern that should be run on a daily basis.
        /// </summary>
        Daily = 0,

        /// <summary>
        /// Pattern that should be run on a weekly basis.
        /// </summary>
        Weekly = 1,

        /// <summary>
        /// Pattern that should be run on a Monthly basis.
        /// </summary>
        Montly = 2,

        /// <summary>
        /// Pattern that should be run on a Yearly basis.
        /// </summary>
        Yearly = 3
    }
}