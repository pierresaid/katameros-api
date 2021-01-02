using NodaTime;
using System;

namespace Helpers.Katameros
{
    public class CopticDateHelper
    {
        /// <summary>
        /// The Date in Gregorian format
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Initialize with Today's Date
        /// </summary>
        public CopticDateHelper()
        {
            Date = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new instance of the CopticDateHelper class to the specified Gregorian dateTime
        /// </summary>
        public CopticDateHelper(DateTime dateTime)
        {
            Date = dateTime;
        }

        /// <summary>
        /// Initializes a new instance of the CopticDateHelper class from the coptic day, month and year
        /// </summary>
        public CopticDateHelper(int day, int month, int year)
        {
            var localDate = new LocalDate(year, month, day, CalendarSystem.Coptic)
                         .WithCalendar(CalendarSystem.Gregorian);
            Date = new DateTime(localDate.Year, localDate.Month, localDate.Day);
        }

        /// <summary>
        /// Returns a Datetime with the coptic Calendar
        /// </summary>
        /// <example>
        /// Console.WriteLine($"{localDate.Day}/{localDate.Month}/{localDate.Year}");
        /// </example>
        public LocalDate ToCopticDate()
        {
            var copticDate = LocalDate.FromDateTime(Date, CalendarSystem.Gregorian)
                                     .WithCalendar(CalendarSystem.Coptic);
            return copticDate;
        }

        /// <summary>
        /// Returns the date for Easter in Gregorian format
        /// </summary>
        public DateTime GetEasterDate()
        {
            var this_year = Date.Year;
            var GYear = this_year;
            var A = GYear % 19;
            var B = GYear % 4;
            var C = GYear % 7;
            var X = (19 * A + 16) % 30;
            var Y = (2 * B + 4 * C + 6 * X) % 7;
            var EasterTime = new DateTime(GYear, 3, 31, 0, 0, 0);
            EasterTime = EasterTime.AddDays(X + Y + 3);
            return EasterTime;
        }

        /// <summary>
        /// Returns the (Beginning and Ending) of the Great Lent
        /// </summary>
        public (DateTime, DateTime) GetGreatLentPeriod()
        {
            var easterTime = GetEasterDate();

            var GreatLentBeginning = easterTime.AddDays(-55);
            var GreatLentEnding = easterTime.AddDays(-9);
            return (GreatLentBeginning, GreatLentEnding);
        }

        /// <summary>
        /// Returns the Pentecost Date
        /// </summary>
        public DateTime GetPentecost()
        {
            var easterTime = GetEasterDate();

            var Pentecost = easterTime.AddDays(49);
            return Pentecost;
        }

        public static LocalDate CreateCopticDate(int copticDay, int copticMonth)
        {
            var currentYear = LocalDate.FromDateTime(DateTime.Now, CalendarSystem.Gregorian)
                                                 .WithCalendar(CalendarSystem.Coptic).Year;
            var copticDate = new LocalDate(currentYear, copticMonth, copticDay);
            return copticDate;
        }
    }
}
