//-----------------------------------------------------------------------
// <copyright file="IEmailService.cs" company="ImprovingEnterprises">
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
    /// Interface wrapping mail send.
    /// </summary>
    public interface IEmailService
    {
        #region [ Methods ]

        /// <summary>
        /// Sends mail messages.
        /// </summary>
        /// <param name="mailRules">The mail rules containing messages which should be run.</param>
        /// <param name="startTime">Time at which the process started.</param>
        /// <returns>Updated rules based on usage.</returns>
        IList<MailRule> Send(IList<MailRule> mailRules, DateTime startTime);

        #endregion
    }
}