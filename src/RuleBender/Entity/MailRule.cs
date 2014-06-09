//-----------------------------------------------------------------------
// <copyright file="MailRule.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;

    /// <summary>
    /// Contains a set of rules to determine when the attached mail message should be sent.
    /// </summary>
    public class MailRule
    {
        #region [ Properties ]

        /// <summary>
        /// Gets or sets the id for the mail rule.
        /// </summary>
        public int RuleId { get; set; }

        /// <summary>
        /// Gets or sets a short description of the mail rule.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the mail pattern.
        /// </summary>
        public MailPattern MailPattern { get; set; }

        /// <summary>
        /// Gets or sets the time when the rule should start being applied.
        /// Null if not waiting for a start date.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the date at which the rule is no longer valid.
        /// Null if the rule does not have an end date.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the recurrence pattern.  Number of days/weeks/months/etc.
        /// Null if no recurrence pattern.
        /// </summary>
        public short? NumberOf { get; set; }

        /// <summary>
        /// Gets or sets a Dictionary listing which days of the week the rule will apply.
        /// </summary>
        public Dictionary<DayOfWeek, bool> DaysOfWeek { get; set; }

        /// <summary>
        /// Gets a value indicating whether the rule is restricted to a particular day of week.
        /// </summary>
        public bool IsDayOfWeekRestricted
        {
            get { return this.DaysOfWeek.Any(d => d.Value); }
        }

        /// <summary>
        /// Gets or sets a value detailing which day of the month the rule applies. 
        /// </summary>
        public int? DayNumber { get; set; }

        /// <summary>
        /// Gets or sets which month of the year the rule applies.
        /// Null if not restricted by month.
        /// </summary>
        public int? Month { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of instances the rule should send email.
        /// Null if not restricted.
        /// </summary>
        public int? MaxOccurences { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the rule is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the last time the rule was run.
        /// Null if not previously run.
        /// </summary>
        public DateTime? LastSent { get; set; }

        /// <summary>
        /// Gets or sets the number of times the rule was sent.
        /// </summary>
        public int TimesSent { get; set; }

        /// <summary>
        /// Gets or sets the mail message which should be sent if the rule is run.
        /// </summary>
        public MailMessage MailMessage { get; set; }

        #endregion
    }
}