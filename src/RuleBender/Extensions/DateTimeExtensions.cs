//-----------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="ImprovingEnterprises">
//     Copyright (c) ImprovingEnterprises. All rights reserved.
// </copyright>
// <author>Anthony Marrical</author>
//-----------------------------------------------------------------------
namespace RuleBender.Extensions
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Extensions to DateTime objects.
    /// </summary>
    public static class DateTimeExtensions
    {
        #region [ Fields ]

        /// <summary>
        /// Calendar to help make calculations.
        /// </summary>
        private static readonly GregorianCalendar Calendar = new GregorianCalendar();

        #endregion

        #region [ Extensions ]

        /// <summary>
        /// Gets which week in the month a date represents.
        /// Calculated on first day of the month and week starting on Sunday.
        /// </summary>
        /// <param name="date">The date being evaluated.</param>
        /// <returns>an integer representing which week of the month the date is in.</returns>
        public static int GetWeekOfMonth(this DateTime date)
        {
            var firstDay = new DateTime(date.Year, date.Month, 1);
            return date.GetWeekOfYear() - firstDay.GetWeekOfYear() + 1;
        }

        /// <summary>
        /// Gets which week of the year a DateTime is.
        /// Calculated on first day of the month and week starting on Sunday.
        /// </summary>
        /// <param name="date">The date being evaluated.</param>
        /// <returns>An integer representing the week of the year.</returns>
        public static int GetWeekOfYear(this DateTime date)
        {
            return Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }

        #endregion
    }
}
