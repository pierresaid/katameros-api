﻿using Katameros.Enums;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Katameros.Repositories
{
    public partial class FeastsFactory
    {
        private readonly DatabaseContext _context;
        private readonly ReadingsRepository _readingsRepository;
        private readonly ReadingsHelper _readingsHelper;

        public FeastsFactory(DatabaseContext context, ReadingsRepository readingsRepository, ReadingsHelper readingsHelper)
        {
            _context = context;
            _readingsRepository = readingsRepository;
            _readingsHelper = readingsHelper;
        }
        public FeastCalc GetDayFeast(DateTime gregorianDate, LocalDate copticDate, int easterDaysDiff)
        {
            var feastCalcs = new List<FeastCalc>
            {
                new FeastCalc(Feast.Christmas, new DateTime(1, 01, 07), ConstructChristmas),
                new FeastCalc(Feast.Paramoun, IsParamoun, ConstructParamoun),
                new FeastCalc(Feast.Ascension, 39, null),
                new FeastCalc(Feast.LazarusSaturday, -8, ConstructLazarusSaturday),
                new FeastCalc(Feast.TempleEntrance, new LocalDate(1, 6, CopticMonths.Baramoudah, CalendarSystem.Coptic), ConstructTempleEntrance),
                new FeastCalc(Feast.EgyptEntrance, new LocalDate(1, CopticMonths.Bashans, 24, CalendarSystem.Coptic), ConstructEgyptEntrance),
                new FeastCalc(Feast.Annunciation, new LocalDate(1, CopticMonths.Baramhat, 29, CalendarSystem.Coptic), ConstructAnnunciation),
                new FeastCalc(Feast.Cross, new LocalDate(1, CopticMonths.Baramhat, 10, CalendarSystem.Coptic), ConstructFeastOfTheCross),
                new FeastCalc(Feast.Cross, new LocalDate(1, CopticMonths.Thout, 17, CalendarSystem.Coptic), ConstructFeastOfTheCross),
                new FeastCalc(Feast.FastOfNinevah, -69, ConstructFastOfNinevah1),
                new FeastCalc(Feast.FastOfNinevah, -68, ConstructFastOfNinevah2),
                new FeastCalc(Feast.FastOfNinevah, -67, ConstructFastOfNinevah3),
                new FeastCalc(Feast.Jonas, -66, ConstructFeastOfJonas),
                new FeastCalc(Feast.PalmSunday, -7, ConstructPalmSunday),
                new FeastCalc(Feast.PaschaMonday, -6, ConstructPaschaMonday),
                new FeastCalc(Feast.PaschaTuesday, -5, ConstructPaschaTuesday),
            };
            foreach (FeastCalc feastCalc in feastCalcs)
            {
                if (feastCalc.EasterDaysDiff.HasValue && feastCalc.EasterDaysDiff == easterDaysDiff)
                    return feastCalc;
                else if (feastCalc.CopticDate.HasValue && feastCalc.CopticDate.Value.Day == copticDate.Day && feastCalc.CopticDate.Value.Month == copticDate.Month)
                    return feastCalc;
                else if (feastCalc.GregorianDate.HasValue && feastCalc.GregorianDate.Value.Day == gregorianDate.Day && feastCalc.GregorianDate.Value.Month == gregorianDate.Month)
                    return feastCalc;
                else if (feastCalc.Validator != null && feastCalc.Validator(gregorianDate, copticDate))
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
