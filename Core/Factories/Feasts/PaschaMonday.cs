using Katameros.DTOs;
using Katameros.Enums;

namespace Katameros.Repositories;

public partial class FeastsFactory
{
    private async Task<DayReadings> ConstructPaschaMonday()
    {
        DayReadings dayReadings = new DayReadings();
        dayReadings.Title = await GetFeastTranslation(Feast.PaschaMonday);

        var FirstHour = await _readingsHelper.GetSectionMeta(SectionType.FirstHour, SectionsMetadata.Title);
        var ThirdHour = await _readingsHelper.GetSectionMeta(SectionType.ThirdHour, SectionsMetadata.Title);
        var SixthHour = await _readingsHelper.GetSectionMeta(SectionType.SixthHour, SectionsMetadata.Title);
        var NinthHour = await _readingsHelper.GetSectionMeta(SectionType.NinthHour, SectionsMetadata.Title);
        var EleventhHour = await _readingsHelper.GetSectionMeta(SectionType.EleventhHour, SectionsMetadata.Title);

        var EveOf = await _readingsHelper.GetSentence(Sentence.EveOf);
        var NextDay = await GetFeastTranslation(Feast.PaschaTuesday);

        dayReadings.Sections = new List<Section>() {
            new Section(SectionType.FirstHour)
            {
                Title = FirstHour,
                SubSections = new List<SubSection>() {
                    await _readingsRepository.MakeOldTestament("1.1:1-31*@+1.2:1-3"),
                    await _readingsRepository.MakeOldTestament("23.5:1-9"),
                    await _readingsRepository.MakeOldTestament("73.1:1-19"),
                    await _readingsRepository.MakePsalmAndGospelLent("19.72:18-19", "41.11:12-24"),
                }
            },
            new Section(SectionType.ThirdHour)
            {
                Title = ThirdHour,
                SubSections = new List<SubSection>() {
                    await _readingsRepository.MakeOldTestament("23.5:20-30"),
                    await _readingsRepository.MakeOldTestament("24.9:12-19"),
                    await _readingsRepository.MakePsalmAndGospelLent("19.122:1-2", "41.11:11-19"),
                }
            },
            new Section(SectionType.SixthHour)
            {
                Title = SixthHour,
                SubSections = new List<SubSection>() {
                    await _readingsRepository.MakeOldTestament("2.32:7-15"),
                    await _readingsRepository.MakeOldTestament("72.1:1-9"),
                    await _readingsRepository.MakePsalmAndGospelLent("19.122:4", "43.2:13-17"),
                }
            },
            new Section(SectionType.NinthHour)
            {
                Title = NinthHour,
                SubSections = new List<SubSection>() {
                    await _readingsRepository.MakeOldTestament("1.2:15-25*@+1.3:1-24"),
                    await _readingsRepository.MakeOldTestament("23.40:1-5"),
                    await _readingsRepository.MakeOldTestament("20.1:1-9"),
                    await _readingsRepository.MakePsalmAndGospelLent("19.65:5,4", "40.21:23-27"),
                }
            },
            new Section(SectionType.EleventhHour)
            {
                Title = EleventhHour,
                SubSections = new List<SubSection>() {
                    await _readingsRepository.MakeOldTestament("23.50:1-3"),
                    await _readingsRepository.MakeOldTestament("73.1:20-30"),
                    await _readingsRepository.MakePsalmAndGospelLent("19.13:3-4", "43.8:51-59"),
                }
            },
            new Section(SectionType.EveFirstHour)
            {
                Title = $"{FirstHour} {EveOf} {NextDay}",
                SubSections = new List<SubSection>() {
                    await _readingsRepository.MakeOldTestament("38.1:1-6"),
                    await _readingsRepository.MakePsalmAndGospelLent("19.62:7,6", "42.13:23-30"),
                }
            },
            new Section(SectionType.EveThirdHour)
            {
                Title = $"{ThirdHour} {EveOf} {NextDay}",
                SubSections = new List<SubSection>() {
                    await _readingsRepository.MakeOldTestament("39.1:1-9"),
                    await _readingsRepository.MakePsalmAndGospelLent("19.13:3,5", "42.13:31-35"),
                }
            },
            new Section(SectionType.EveSixthHour)
            {
                Title = $"{SixthHour} {EveOf} {NextDay}",
                SubSections = new List<SubSection>() {
                    await _readingsRepository.MakeOldTestament("28.4:15-19*@+28.5:1-7"),
                    await _readingsRepository.MakePsalmAndGospelLent("19.91:2,3", "42.21:34-38"),
                }
            },
            new Section(SectionType.EveNinthHour)
            {
                Title = $"{NinthHour} {EveOf} {NextDay}",
                SubSections = new List<SubSection>() {
                    await _readingsRepository.MakeOldTestament("28.10:12-15*@+28.11:1-2"),
                    await _readingsRepository.MakePsalmAndGospelLent("19.33:10,11", "42.11:37-52"),
                }
            },
            new Section(SectionType.EveEleventhHour)
            {
                Title = $"{EleventhHour} {EveOf} {NextDay}",
                SubSections = new List<SubSection>() {
                    await _readingsRepository.MakeOldTestament("30.5:6-14"),
                    await _readingsRepository.MakePsalmAndGospelLent("19.122:4", "41.13:32-37*@+41.14:1-2"),
                }
            }
        };
        return dayReadings;
    }
}
