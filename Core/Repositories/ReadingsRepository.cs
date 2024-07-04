using Humanizer;
using Katameros.DTOs;
using Katameros.Enums;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Katameros.Repositories;

public class ReadingsRepository
{
    private readonly DatabaseContext _context;
    private readonly ReadingsHelper _readingsHelper;

    public ReadingsRepository(DatabaseContext context, ReadingsHelper readingsHelper)
    {
        _context = context;
        _readingsHelper = readingsHelper;
    }

    #region PeriodsReadingsRef
    public async Task<Models.IReadingRefs> GePentecostReadingsRef(DateTime date, int easterDaysDiff)
    {
        Models.IReadingRefs readingRefs;

        var dayNumber = date.DayOfWeek;
        var weekNumber = (easterDaysDiff / 7) + 1 - (dayNumber == DayOfWeek.Sunday ? 1 : 0);

        readingRefs = await _context.PentecostReadings.Where(ar => ar.Week == weekNumber && ar.DayOfWeek == (int)dayNumber).FirstAsync();

        return readingRefs;
    }

    public async Task<Models.IReadingRefs> GetGreatLentReadingsRef(DateTime date, DateTime lentBeginning)
    {
        Models.IReadingRefs readingRefs;
        int weekNumber = ((date - lentBeginning).Days / 7) + 1;
        int dayNumber = (int)date.DayOfWeek;

        readingRefs = await _context.GreatLentReadings.Where(ar => ar.Week == weekNumber && ar.DayOfWeek == dayNumber).FirstAsync();

        return readingRefs;
    }

    public async Task<(Models.IReadingRefs, int)> GetSundayReadingsRef(LocalDate copticDate)
    {
        Models.IReadingRefs readingRefs;
        int i = 0;
        int nbSunday = 0;

        while (i < copticDate.Day)
        {
            if (copticDate.PlusDays(i).DayOfWeek == IsoDayOfWeek.Sunday)
                nbSunday += 1;
            ++i;
        }

        readingRefs = await _context.SundayReadings.Where(ar => ar.Month_Number == copticDate.Month && ar.Day == nbSunday).FirstOrDefaultAsync();
        return (readingRefs, nbSunday);
    }

    public async Task<Models.IReadingRefs> GetAnnualReadingsRef(LocalDate copticDate)
    {
        return await _context.AnnualReadings.Where(ar => ar.Month_Number == copticDate.Month && ar.Day == copticDate.Day).FirstAsync();
    }
    #endregion

    public async Task<DayReadings> GetFromRef(Models.IReadingRefs readingRefs)
    {
        var sections = new List<Section>();
        if (readingRefs.V_Psalm_Ref != null)
            sections.Add(await MakeVespers(readingRefs.V_Psalm_Ref, readingRefs.V_Gospel_Ref));
        sections.Add(await MakeMatins(readingRefs.M_Psalm_Ref, readingRefs.M_Gospel_Ref, readingRefs.Prophecy));
        sections.Add(await MakeLitugy(readingRefs.P_Gospel_Ref, readingRefs.C_Gospel_Ref, readingRefs.X_Gospel_Ref, readingRefs.L_Psalm_Ref, readingRefs.L_Gospel_Ref));
        return new DayReadings
        {
            Sections = sections
        };
    }

    #region Period DayReadings

    public async Task<DayReadings> GetReadingsForAnnual(LocalDate copticDate, Feast feast = Feast.NONE)
    {
        var refs = await GetAnnualReadingsRef(copticDate);
        DayReadings dayReadings = await GetFromRef(refs);
        if (feast != Feast.NONE)
            dayReadings.Title = await _readingsHelper.GetFeastTranslation(feast);
        return dayReadings;
    }

    public async Task<DayReadings> GetReadingsForSunday(int copticMonth, int nbSunday)
    {
        var refs = await _context.SundayReadings.Where(ar => ar.Month_Number == copticMonth && ar.Day == nbSunday).FirstOrDefaultAsync();
        DayReadings dayReadings = await GetFromRef(refs);
        return dayReadings;
    }

    #endregion

    #region Liturgy
    public async Task<SubSection> MakePauline(string paulineRef)
    {
        var subSection = new SubSection(SubSectionType.Pauline);
        Reading reading = await _readingsHelper.MakeReading(paulineRef, ReadingType.Pauline);
        var firstPassage = reading.Passages.First();
        var recipient = firstPassage.BookTranslation != null ? string.Concat(firstPassage.BookTranslation.Where(char.IsLetter)) : "";

        if (reading.Introduction != null)
        {
            reading.Introduction = reading.Introduction.Replace("$", recipient);

            reading.Introduction = NumeriseEpistle(reading.Introduction, firstPassage, reading.Introduction.IndexOf("] "));

            var first = reading.Introduction.IndexOf('[');
            var last = reading.Introduction.LastIndexOf(']');

            if (first != -1 && last != -1)
            {
                Regex regex = new Regex("(?<=\\[).*?(?=\\])");
                var matches = regex.Matches(reading.Introduction);

                var singular = matches[0].Value;
                var plural = matches[1].Value;

                var input = reading.Introduction;
                var noun = plural;
                // Titus, Philemon, James, Timothy
                if (new[] { 56, 57, 59, 55 }.Contains(firstPassage.BookId))
                {
                    noun = singular;
                }
                var output = input.Substring(0, first) + noun + input.Substring(last + 1, input.Length - 1 - last);
                reading.Introduction = output;
            }
        }
        subSection.Title = await _readingsHelper.GetReadingMeta(ReadingType.Pauline, ReadingsMetadata.Title);
        subSection.Readings = new List<Reading>() { reading };
        return subSection;
    }

    public async Task<SubSection> MakeCatholic(string catholicRef)
    {
        var subSection = new SubSection(SubSectionType.Catholic);
        Reading reading = await _readingsHelper.MakeReading(catholicRef, ReadingType.Catholic);
        var firstPassage = reading.Passages.First();
        var author = firstPassage.BookTranslation != null ? string.Concat(firstPassage.BookTranslation.Where(char.IsLetter)) : "";
        reading.Introduction = reading.Introduction?.Replace("$", author);
        reading.Introduction = NumeriseEpistle(reading.Introduction, firstPassage);

        subSection.Readings = new List<Reading>() { reading };
        subSection.Title = await _readingsHelper.GetReadingMeta(ReadingType.Catholic, ReadingsMetadata.Title);
        return subSection;
    }

    private string NumeriseEpistle(string Introduction, Passage firstPassage, int lastIndex = -1)
    {
        if (Introduction == null)
            return "";
        var first = Introduction.IndexOf('[');
        var last = lastIndex != -1 ? lastIndex : Introduction.LastIndexOf(']');
        if (first == -1 || last == -1)
        {
            return Introduction;
        }

        Regex regex = new Regex("(?<=\\[).*?(?=\\])");
        var matches = regex.Matches(Introduction);

        int matchesCount = matches.Count;

        var firstEpistle = matchesCount >= 1 ? matches[0]?.Value : null;
        var secondEpistle = matchesCount >= 2 ? matches[1]?.Value : null;
        var thirdEpistle = matchesCount >= 3 ? matches[2]?.Value : null;

        var input = Introduction;
        var epistleNumber = "";
        // First Corinthians, First Thessalonians, First Timothy, First Peter, First John
        if (new[] { 46, 52, 54, 60, 62 }.Contains(firstPassage.BookId))
        {
            epistleNumber = firstEpistle;
        }
        else if (new[] { 47, 53, 55, 61, 63 }.Contains(firstPassage.BookId)) // Second Corinthians, Second Thessalonians, Second Timothy, Second Peter, Second John
        {
            epistleNumber = secondEpistle;
        }
        else if (firstPassage.BookId == 64) // Third John
        {
            epistleNumber = thirdEpistle;
        }
        var output = input.Substring(0, first) + epistleNumber + input.Substring(last + 1, input.Length - 1 - last);
        return output;
    }

    public async Task<SubSection> MakeActs(string actsRef)
    {
        return new SubSection(SubSectionType.Acts)
        {
            Title = await _readingsHelper.GetReadingMeta(ReadingType.Acts, ReadingsMetadata.Title),
            Readings = new List<Reading>() { await _readingsHelper.MakeReading(actsRef, ReadingType.Acts) }
        };
    }

    public async Task<Section> MakeLitugy(string paulineRef, string catholicRef, string actsRef, string psalmRef, string gospelRef)
    {
        var section = new Section(SectionType.Liturgy);
        var subSections = new List<SubSection>();

        section.Title = await _readingsHelper.GetSectionMeta(SectionType.Liturgy, SectionsMetadata.Title);

        subSections.Add(await MakePauline(paulineRef));
        subSections.Add(await MakeCatholic(catholicRef));
        subSections.Add(await MakeActs(actsRef));
        if (_context.LanguageId == 1 || _context.LanguageId == 2 || _context.LanguageId == 3 || _context.LanguageId == 4)
            subSections.Add(await MakeSynaxarium(_context.CopticDate.Day, _context.CopticDate.Month));
        if (gospelRef != null)
            subSections.Add(await MakePsalmAndGospel(psalmRef, gospelRef));

        section.SubSections = subSections;

        return section;
    }
    #endregion

    #region Psalm And Gospel
    public async Task<SubSection> MakePsalmAndGospel(string psalmRef, string gospelRef)
    {
        SubSection subSection = new SubSection(SubSectionType.PsalmAndGospel);
        List<Reading> readings = new List<Reading>();

        subSection.Introduction = await _readingsHelper.GetSubSectionMeta(SubSectionType.PsalmAndGospel, SubSectionsMetadata.Introduction);
        if (psalmRef != null)
            readings.Add(await _readingsHelper.MakeReading(psalmRef, ReadingType.Psalm));
        Reading gospel = await _readingsHelper.MakeReading(gospelRef, ReadingType.Gospel);
        var evangelist = gospel.Passages.First().BookTranslation != null ? string.Concat(gospel.Passages.First().BookTranslation.Where(char.IsLetter)) : "";
        if (psalmRef == null)
            gospel.Introduction = null;
        else if (gospel.Introduction != null && gospel.Introduction.Contains("$"))
        {
            gospel.Introduction = gospel.Introduction.Replace("$", evangelist);
        }
        readings.Add(gospel);
        subSection.Title = await _readingsHelper.GetSubSectionMeta(SubSectionType.PsalmAndGospel, SubSectionsMetadata.Title);
        subSection.Introduction = subSection.Introduction?.Replace("$", evangelist);
        subSection.Readings = readings;
        return subSection;
    }

    public async Task<SubSection> MakePsalmsAndGospels(IEnumerable<string> psalmRefs, IEnumerable<string> gospelRefs)
    {
        SubSection subSection = new SubSection(SubSectionType.PsalmAndGospel);
        List<Reading> readings = new List<Reading>();

        //subSection.Introduction = await _readingsHelper.GetSubSectionMeta(SubSectionType.PsalmAndGospel, SubSectionsMetadata.Introduction);
        foreach (var psalmRef in psalmRefs)
            readings.Add(await _readingsHelper.MakeReading(psalmRef, ReadingType.Psalm, ReadingMode.Simple));
        foreach (var gospelRef in gospelRefs)
            readings.Add(await _readingsHelper.MakeReading(gospelRef, ReadingType.Gospel, ReadingMode.Simple));

        subSection.Title = await _readingsHelper.GetSubSectionMeta(SubSectionType.PsalmAndGospel, SubSectionsMetadata.Title);
        subSection.Readings = readings;
        return subSection;
    }

    public async Task<SubSection> MakePsalmAndGospel(string psalmRef, string psalmRef2, string gospelRef)
    {
        var subSection = await this.MakePsalmAndGospel(psalmRef, gospelRef);
        var psalm2 = await _readingsHelper.MakeReading(psalmRef2, ReadingType.Psalm);
        subSection.Readings.Insert(1, psalm2);
        return subSection;
    }

    public async Task<SubSection> MakePsalm(string psalmRef)
    {
        SubSection subSection = new SubSection(SubSectionType.Psalm);
        List<Reading> readings = new List<Reading>();
        readings.Add(await _readingsHelper.MakeReading(psalmRef, ReadingType.Psalm));
        subSection.Readings = readings;
        return subSection;
    }
    #endregion


    #region Synaxarium
    private async Task<SubSection> MakeSynaxarium(int day, int month)
    {
        SubSection subSection = new SubSection(SubSectionType.Synaxarium);

        var synaxs = await _context.Synaxarium.Where(x => x.LanguageId == _context.LanguageId && x.Day == day && x.Month == month).OrderBy(x => x.Order).ToListAsync();

        List<Reading> readings = synaxs.Select(x => new Reading(ReadingType.Synaxarium)
        {
            Title = x.Title,
            Html = x.Text
        }).ToList();


        subSection.Introduction = await _readingsHelper.GetSubSectionMeta(SubSectionType.Synaxarium, SubSectionsMetadata.Introduction);
        subSection.Introduction = subSection.Introduction.ReplaceFirst("$", getOrdinalizeWithLanguage(day));
        subSection.Introduction = subSection.Introduction.ReplaceFirst("$", getCopticMonth(month));


        var first = subSection.Introduction.IndexOf('[');
        var last = subSection.Introduction.LastIndexOf(']');

        if (first != -1 && last != -1) {
            Regex regex = new Regex("(?<=\\[).*?(?=\\])");
            var matches = regex.Matches(subSection.Introduction);

            int matchesCount = matches.Count;

            var opt1 = matchesCount >= 1 ? matches[0]?.Value : null;
            var opt2 = matchesCount >= 1 ? matches[1]?.Value : null;

            subSection.Introduction = $"{subSection.Introduction.Substring(0, first)}{(day <= 15 ? opt1 : opt2)}{subSection.Introduction.Substring(last + 1, subSection.Introduction.Length - 1 - last)}";
        }
        subSection.Title = await _readingsHelper.GetReadingMeta(ReadingType.Synaxarium, ReadingsMetadata.Title);
        subSection.Readings = readings;
        return subSection;
    }

    private string getCopticMonth(int month)
    {
        var coptic_months = new List<string>() {
            "Thout",
            "Babah",
            "Hatour",
            "Kiahk",
            "Toubah",
            "Amshir",
            "Baramhat",
            "Baramoudah",
            "Bashans",
            "Baounah",
            "Abib",
            "Misra",
            "Nasie",
        };
        return coptic_months[month - 1];
    }
    private string getOrdinalizeWithLanguage(int day)
    {
        string langStr = _context.LanguageId switch
        {
            1 => "fr",
            2 => "en",
            3 => "ar",
            4 => "it",
            _ => "",
        };
        return day.Ordinalize(culture: CultureInfo.CreateSpecificCulture(langStr));
    }
    #endregion


    #region Matins
    public async Task<Section> MakeMatins(string psalmRef, string gospelRef, string prophecyRef = null)
    {
        var section = new Section(SectionType.Matins);
        var subSections = new List<SubSection>();

        section.Title = (await _context.SectionsMetadatasTranslations.FindAsync((int)SectionType.Matins, (int)SectionsMetadata.Title, _context.LanguageId))?.Text;

        if (prophecyRef != null)
            subSections.Add(await MakeProphecies(prophecyRef));
        subSections.Add(await MakePsalmAndGospel(psalmRef, gospelRef));

        section.SubSections = subSections;

        return section;
    }
    #endregion

    #region Prophecies
    public async Task<SubSection> MakeProphecies(string prophecyRef)
    {
        var subSection = new SubSection(SubSectionType.Prophecy);
        var readings = new List<Reading>();

        subSection.Title = (await _context.SubSectionsMetadatasTranslations.FindAsync((int)SubSectionType.Prophecy, (int)SubSectionsMetadata.Title, _context.LanguageId))?.Text;
        var prophecyConclusion = (await _context.ReadingsMetadatasTranslations.FindAsync((int)ReadingType.Prophecy, (int)ReadingsMetadata.Conclusion, _context.LanguageId))?.Text;
        var refs = _readingsHelper.GetRefs(prophecyRef);

        foreach (var readingRef in refs)
        {
            Reading reading = await _readingsHelper.MakeReading(readingRef, ReadingType.Prophecy);
            reading.Conclusion = prophecyConclusion;
            var last = readings.LastOrDefault();
            var lastNumber = last == null ? null : _context.Verses.Where(v => v.BibleId == this._context.BibleId
                                                   && v.BookId == last.Passages.FirstOrDefault().BookId
                                                   && v.Chapter == last.Passages.FirstOrDefault().Chapter)
                                       .OrderByDescending(v => v.Number).Take(1);
            if (last != null
                && last.Passages.FirstOrDefault().BookId == reading.Passages.FirstOrDefault().BookId
                && last.Passages.FirstOrDefault().Chapter == reading.Passages.FirstOrDefault().Chapter - 1
                && reading.Passages.FirstOrDefault().Verses.FirstOrDefault()?.Number == 1
                && last.Passages.FirstOrDefault().Verses.LastOrDefault()?.Number == lastNumber?.FirstOrDefault()?.Number)
            {
                last.Passages.LastOrDefault().Verses.AddRange(reading.Passages.SelectMany(x => x.Verses));
                last.Passages.LastOrDefault().Ref = last.Passages.LastOrDefault().Ref.Split("-")[0] + "-" + reading.Passages.LastOrDefault().Ref.Split(":")[0] + ":" + reading.Passages.LastOrDefault().Ref.Split("-")[1];
            }
            else
                readings.Add(reading);
        }

        subSection.Readings = readings;

        return subSection;
    }
    #endregion

    #region OldTestament
    public async Task<SubSection> MakeOldTestament(string passageRef)
    {
        var subSection = new SubSection(SubSectionType.Prophecy);
        var readings = new List<Reading>();

        var conclusion = await _readingsHelper.GetReadingMeta(ReadingType.Prophecy, ReadingsMetadata.Conclusion);
        var refs = _readingsHelper.GetRefs(passageRef);

        foreach (var readingRef in refs)
        {
            Reading reading = await _readingsHelper.MakeReading(readingRef, ReadingType.Prophecy);
            reading.Conclusion = conclusion;
            var last = readings.LastOrDefault();
            var lastNumber = last == null ? null : _context.Verses.Where(v => v.BibleId == this._context.BibleId
                                                   && v.BookId == last.Passages.FirstOrDefault().BookId
                                                   && v.Chapter == last.Passages.FirstOrDefault().Chapter)
                                       .OrderByDescending(v => v.Number).Take(1);
            if (last != null
                && last.Passages.FirstOrDefault().BookId == reading.Passages.FirstOrDefault().BookId
                && last.Passages.FirstOrDefault().Chapter == reading.Passages.FirstOrDefault().Chapter - 1
                && reading.Passages.FirstOrDefault().Verses.FirstOrDefault()?.Number == 1
                && last.Passages.FirstOrDefault().Verses.LastOrDefault()?.Number == lastNumber.FirstOrDefault().Number)
            {
                last.Passages.LastOrDefault().Verses.AddRange(reading.Passages.SelectMany(x => x.Verses));
                last.Passages.LastOrDefault().Ref = last.Passages.LastOrDefault().Ref.Split("-")[0] + "-" + reading.Passages.LastOrDefault().Ref.Split(":")[0] + ":" + reading.Passages.LastOrDefault().Ref.Split("-")[1];
            }
            else
                readings.Add(reading);
        }

        subSection.Readings = readings;
        return subSection;
    }
    #endregion

    #region Vespers
    public async Task<Section> MakeVespers(string psalmRef, string gospelRef, string prophecyRef = null)
    {
        var section = new Section(SectionType.Vespers);
        var subSections = new List<SubSection>();

        section.Title = (await _context.SectionsMetadatasTranslations.FindAsync((int)SectionType.Vespers, (int)SectionsMetadata.Title, _context.LanguageId))?.Text;

        if (prophecyRef != null)
            subSections.Add(await MakeProphecies(prophecyRef));

        subSections.Add(await MakePsalmAndGospel(psalmRef, gospelRef));
        section.SubSections = subSections;

        return section;
    }
    #endregion
}
