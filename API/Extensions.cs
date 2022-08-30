using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Katameros
{
    public static class Extensions
    {
        /// <summary>
        /// Returns a Datetime with the coptic Calendar
        /// </summary>
        public static LocalDate ToCopticDate(this DateTime date)
        {
            var copticDate = LocalDate.FromDateTime(date, CalendarSystem.Gregorian)
                                     .WithCalendar(CalendarSystem.Coptic);
            return copticDate;
        }

        public static void Deconstruct<T>(this IList<T> list, out T first, out IList<T> rest)
        {

            first = list.Count > 0 ? list[0] : default(T);
            rest = list.Skip(1).ToList();
        }

        public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out IList<T> rest)
        {
            first = list.Count > 0 ? list[0] : default(T);
            second = list.Count > 1 ? list[1] : default(T);
            rest = list.Skip(2).ToList();
        }

        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }
    }
}
