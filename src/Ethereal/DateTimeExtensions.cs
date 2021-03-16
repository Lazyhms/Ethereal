// Copyright (c) Ethereal. All rights reserved.
//

namespace System
{
    /// <summary>
    /// DateTimeExtensions
    /// </summary>
    public static class DateTimeExtensions
    {
        #region DayOf
        /// <summary>
        /// FirstTimeOfToday
        /// </summary>
        public static DateTime FirstTimeOfToday(this DateTime date) =>
            date.Date;

        /// <summary>
        /// LastTimeOfToday
        /// </summary>
        public static DateTime LastTimeOfToday(this DateTime date) =>
            date.Date.AddDays(1).AddSeconds(-1);

        /// <summary>
        /// FirstDayOfWeek
        /// </summary>
        public static DateTime FirstDayOfWeek(this DateTime date) =>
            date.AddDays(1 - (int)date.DayOfWeek).Date;

        /// <summary>
        /// LastDayOfWeek
        /// </summary>
        public static DateTime LastDayOfWeek(this DateTime date) =>
            date.AddDays(7 - (int)date.DayOfWeek).LastTimeOfToday();

        /// <summary>
        /// FirstDayOfMonth
        /// </summary>
        public static DateTime FirstDayOfMonth(this DateTime date) =>
            date.AddDays(1 - date.Day).Date;

        /// <summary>
        /// LastDayOfMonth
        /// </summary>
        public static DateTime LastDayOfMonth(this DateTime date) =>
            date.AddDays(1 - date.Day).AddMonths(1).AddDays(-1).LastTimeOfToday();

        /// <summary>
        /// FirstDayOfQuarter
        /// </summary>
        public static DateTime FirstDayOfQuarter(this DateTime date) =>
            date.AddMonths(0 - (date.Month - 1) % 3).AddDays(1 - date.Day).Date;

        /// <summary>
        /// LastDayOfQuarter
        /// </summary>
        public static DateTime LastDayOfQuarter(this DateTime date) =>
            date.AddMonths(3 - (date.Month - 1) % 3).AddDays(1 - date.Day).Date.AddSeconds(-1);

        /// <summary>
        /// FirstDayOfYear
        /// </summary>
        public static DateTime FirstDayOfYear(this DateTime date) =>
            date.AddDays(1 - date.Day).AddMonths(1 - date.Month).Date;

        /// <summary>
        /// LastDayOfYear
        /// </summary>
        public static DateTime LastDayOfYear(this DateTime date) =>
            date.AddDays(1 - date.Day).AddMonths(1 - date.Month).Date.AddYears(1).AddSeconds(-1);
        #endregion

        #region Diff
        /// <summary>
        /// DateDiffMicrosecond
        /// </summary>
        public static int DateDiffMicrosecond(this DateTime startDate, DateTime endDate)
        {
            checked
            {
                return (int)((endDate.Ticks - startDate.Ticks) / 10);
            }
        }

        /// <summary>
        /// DateDiffMillisecond
        /// </summary>
        public static int DateDiffMillisecond(this DateTime startDate, DateTime endDate)
        {
            checked
            {
                return DateDiffSecond(startDate, endDate) * 1000 + endDate.Millisecond - startDate.Millisecond;
            }
        }

        /// <summary>
        /// DateDiffSecond
        /// </summary>
        public static int DateDiffSecond(this DateTime startDate, DateTime endDate)
        {
            checked
            {
                return DateDiffMinute(startDate, endDate) * 60 + endDate.Second - startDate.Second;
            }
        }

        /// <summary>
        /// DateDiffMinute
        /// </summary>
        public static int DateDiffMinute(this DateTime startDate, DateTime endDate)
        {
            checked
            {
                return DateDiffHour(startDate, endDate) * 60 + endDate.Minute - startDate.Minute;
            }
        }

        /// <summary>
        /// DateDiffHour
        /// </summary>
        public static int DateDiffHour(this DateTime startDate, DateTime endDate)
        {
            checked
            {
                return DateDiffDay(startDate, endDate) * 24 + endDate.Hour - startDate.Hour;
            }
        }

        /// <summary>
        /// DateDiffDay
        /// </summary>
        public static int DateDiffDay(this DateTime startDate, DateTime endDate)
            => (endDate.Date - startDate.Date).Days;

        /// <summary>
        /// DateDiffMonth
        /// </summary>
        public static int DateDiffMonth(this DateTime startDate, DateTime endDate)
            => 12 * (endDate.Year - startDate.Year) + endDate.Month - startDate.Month;

        /// <summary>
        /// DateDiffYear
        /// </summary>
        public static int DateDiffYear(this DateTime startDate, DateTime endDate)
            => endDate.Year - startDate.Year;
        #endregion
    }
}
