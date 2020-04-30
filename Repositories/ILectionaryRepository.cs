using Katameros.DTOs;
using System;
using System.Threading.Tasks;

namespace Katameros.Repositories
{
    public interface ILectionaryRepository
    {
        /// <summary>
        /// Configure the Language and/or Bible the repository should use while fetching readings.
        /// <br/>
        /// The repository will configure itself to fetch the corresponding bible if the language alone is set and vice-versa.
        /// </summary>
        /// <param name="LanguageId">Id of the language</param>
        /// <param name="BibleId">Id of the bible</param>
        /// <returns>true if the language and/or the bible are found</returns>
        public Task<bool> Configure(int LanguageId = -1, int BibleId = -1);

        /// <summary>
        /// Returns the readings for the given date
        /// </summary>
        /// <param name="date">The date</param>
        public Task<DayReadings> GetForDay(DateTime date);
    }
}
