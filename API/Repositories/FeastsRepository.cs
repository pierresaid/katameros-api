using Katameros.DTOs;
using Katameros.Enums;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Katameros.Repositories
{
    public partial class FeastsRepository
    {
        private readonly DatabaseContext _context;
        private readonly ReadingsRepository _readingsRepository;
        public FeastsRepository(DatabaseContext context, ReadingsRepository readingsRepository)
        {
            _context = context;
            _readingsRepository = readingsRepository;
        }
        public FeastCalc GetDayFeast(DateTime gregorianDate, LocalDate copticDate, int easterDaysDiff)
        {
            var feastCalcs = new List<FeastCalc>
            {
                new FeastCalc(Feast.Christmas, new DateTime(1, 01, 07), ConstructChristmas),
                new FeastCalc(Feast.Ascension, 39, null),
                new FeastCalc(Feast.LazarusSaturday, -8, ConstructLazarusSaturday),
                new FeastCalc(Feast.PalmSunday, -7, ConstructPalmSunday),
            };
            foreach (FeastCalc feastCalc in feastCalcs)
            {
                if (feastCalc.EasterDaysDiff.HasValue && feastCalc.EasterDaysDiff == easterDaysDiff)
                    return feastCalc;
                else if (feastCalc.CopticDate.HasValue && feastCalc.CopticDate.Value.Day == copticDate.Day && feastCalc.CopticDate.Value.Month == copticDate.Month)
                    return feastCalc;
                else if (feastCalc.GregorianDate.HasValue && feastCalc.GregorianDate.Value.Day == gregorianDate.Day && feastCalc.GregorianDate.Value.Month == gregorianDate.Month)
                    return feastCalc;
            }
            return null;
        }

        public async Task<string> GetFeastTranslation(Feast feastId)
        {
            return (await _context.FeastsTranslations.FindAsync((int)feastId, _context.LanguageId))?.Text;
        }
    }
}
