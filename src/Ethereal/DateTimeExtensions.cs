// Copyright (c) Ethereal. All rights reserved.

namespace System
{
    /// <summary>
    /// DateTimeExtensions
    /// </summary>
    public static class DateTimeExtensions
    {
        #region StartAndEnd

        /// <summary>
        /// Start and end date of the momth
        /// </summary>
        public static (DateTime start, DateTime end) DayOfMonth(this DateTime date) => (date.FirstDayOfMonth(), date.LastDayOfMonth());

        /// <summary>
        /// Start and end date of the quarter
        /// </summary>
        public static (DateTime start, DateTime end) DayOfQuarter(this DateTime date) => (date.FirstDayOfQuarter(), date.LastDayOfQuarter());

        /// <summary>
        /// Start and end date of the week
        /// </summary>
        public static (DateTime start, DateTime end) DayOfWeek(this DateTime date) => (date.FirstDayOfWeek(), date.LastDayOfWeek());

        /// <summary>
        /// Start and end date of the year
        /// </summary>
        public static (DateTime start, DateTime end) DayOfYear(this DateTime date) => (date.FirstDayOfYear(), date.LastDayOfYear());

        /// <summary>
        /// Start and end date of the day
        /// </summary>
        public static (DateTime start, DateTime end) TimeOfDay(this DateTime date) => (date.FirstTimeOfToday(), date.LastTimeOfToday());

        #endregion StartAndEnd

        #region DayOf

        /// <summary>
        /// FirstDayOfMonth
        /// </summary>
        public static DateTime FirstDayOfMonth(this DateTime date) =>
            date.AddDays(1 - date.Day).Date;

        /// <summary>
        /// FirstDayOfQuarter
        /// </summary>
        public static DateTime FirstDayOfQuarter(this DateTime date) =>
            date.AddMonths(0 - (date.Month - 1) % 3).AddDays(1 - date.Day).Date;

        /// <summary>
        /// FirstDayOfWeek
        /// </summary>
        public static DateTime FirstDayOfWeek(this DateTime date) =>
            date.AddDays(1 - (int)date.DayOfWeek).Date;

        /// <summary>
        /// FirstDayOfYear
        /// </summary>
        public static DateTime FirstDayOfYear(this DateTime date) =>
            date.AddDays(1 - date.Day).AddMonths(1 - date.Month).Date;

        /// <summary>
        /// FirstTimeOfToday
        /// </summary>
        public static DateTime FirstTimeOfToday(this DateTime date) =>
            date.Date;

        /// <summary>
        /// LastDayOfMonth
        /// </summary>
        public static DateTime LastDayOfMonth(this DateTime date) =>
            date.AddDays(1 - date.Day).AddMonths(1).AddDays(-1).LastTimeOfToday();

        /// <summary>
        /// LastDayOfQuarter
        /// </summary>
        public static DateTime LastDayOfQuarter(this DateTime date) =>
            date.AddMonths(3 - (date.Month - 1) % 3).AddDays(1 - date.Day).Date.AddSeconds(-1);

        /// <summary>
        /// LastDayOfWeek
        /// </summary>
        public static DateTime LastDayOfWeek(this DateTime date) =>
            date.AddDays(7 - (int)date.DayOfWeek).LastTimeOfToday();

        /// <summary>
        /// LastDayOfYear
        /// </summary>
        public static DateTime LastDayOfYear(this DateTime date) =>
            date.AddDays(1 - date.Day).AddMonths(1 - date.Month).Date.AddYears(1).AddSeconds(-1);

        /// <summary>
        /// LastTimeOfToday
        /// </summary>
        public static DateTime LastTimeOfToday(this DateTime date) =>
            date.Date.AddDays(1).AddSeconds(-1);

        #endregion DayOf

        #region Diff

        /// <summary>
        /// DateDiffDay
        /// </summary>
        public static int DateDiffDay(this DateTime startDate, DateTime endDate)
            => (endDate.Date - startDate.Date).Days;

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
        /// DateDiffMicrosecond
        /// </summary>
        public static long DateDiffMicrosecond(this DateTime startDate, DateTime endDate)
        {
            checked
            {
                return (endDate.Ticks - startDate.Ticks) / 10;
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
        /// DateDiffMonth
        /// </summary>
        public static int DateDiffMonth(this DateTime startDate, DateTime endDate)
            => 12 * (endDate.Year - startDate.Year) + endDate.Month - startDate.Month;

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
        /// DateDiffYear
        /// </summary>
        public static int DateDiffYear(this DateTime startDate, DateTime endDate)
            => endDate.Year - startDate.Year;

        #endregion Diff
    }
}