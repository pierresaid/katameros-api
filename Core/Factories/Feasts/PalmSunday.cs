using Katameros.DTOs;
using Katameros.Enums;

namespace Katameros.Repositories;

// TODO add 9, 11 hours + check psalms
public partial class FeastsFactory
{
    private async Task<DayReadings> ConstructPalmSunday()
    {
        DayReadings dayReadings = new DayReadings
        {
            Title = await GetFeastTranslation(Feast.PalmSunday),
            Sections = new List<Section>
            {
                await _readingsRepository.MakeVespers("19.118:26-27", "43.12:1-11"),
                await _readingsRepository.MakeMatins("19.68:19,35", "42.19:1-10", null),

            }
        };
        Section liturgy = await _readingsRepository.MakeLitugy("58.9:11-28", "60.4:1-11", "44.28:11-31", null, null);
        liturgy.SubSections.Add(await _readingsRepository.MakePsalmAndGospel("19.81:3,1,2", "40.21:1-17"));
        liturgy.SubSections.Add(await _readingsRepository.MakePsalmAndGospel(null, "41.11:1-11"));
        liturgy.SubSections.Add(await _readingsRepository.MakePsalmAndGospel(null, "42.19:29-48"));
        liturgy.SubSections.Add(await _readingsRepository.MakePsalmAndGospel("19.65:1-2", "43.12:12-19"));
        dayReadings.Sections.Add(liturgy);


        //var FirstHour = await _readingsHelper.GetSectionMeta(SectionType.FirstHour, SectionsMetadata.Title);
        //var ThirdHour = await _readingsHelper.GetSectionMeta(SectionType.ThirdHour, SectionsMetadata.Title);
        //var SixthHour = await _readingsHelper.GetSectionMeta(SectionType.SixthHour, SectionsMetadata.Title);
        //var NinthHour = await _readingsHelper.GetSectionMeta(SectionType.NinthHour, SectionsMetadata.Title);
        //var EleventhHour = await _readingsHelper.GetSectionMeta(SectionType.EleventhHour, SectionsMetadata.Title);


        //var EveOf = await _readingsHelper.GetSentence(Sentence.EveOf);
        //var NextDay = await GetFeastTranslation(Feast.PaschaMonday);

        //var nextSections = new List<Section>()
        //{
        //    new Section(SectionType.NinthHour)
        //    {
        //        Title = $"{FirstHour} {EveOf} {NextDay}",
        //        SubSections = new List<SubSection>() {
        //            await _readingsRepository.MakeOldTestament("25.1:1-4"),
        //            await _readingsRepository.MakeOldTestament("36.3:11-20"),
        //            await _readingsRepository.MakePsalmAndGospelLent("19.8:2-3", "40.21:10-17"),
        //        }
        //    },
        //    new Section(SectionType.EleventhHour)
        //    {
        //        Title = $"{ThirdHour} {EveOf} {NextDay}",
        //        SubSections = new List<SubSection>() {
        //            await _readingsRepository.MakeOldTestament("23.48:12-22"),
        //            await _readingsRepository.MakeOldTestament("34.1:2-8"),
        //            await _readingsRepository.MakePsalmAndGospelLent("19.8:2-3", "40.20:20-28"),
        //        }
        //    },
        //    new Section(SectionType.EveFirstHour)
        //    {
        //        Title = $"{FirstHour} {EveOf} {NextDay}",
        //        SubSections = new List<SubSection>() {
        //            await _readingsRepository.MakeOldTestament("36.1:2-12"),
        //            await _readingsRepository.MakePsalmAndGospelLent("19.27:6-8", "43.12:20-36"),
        //        }
        //    },
        //    new Section(SectionType.EveThirdHour)
        //    {
        //        Title = $"{ThirdHour} {EveOf} {NextDay}",
        //        SubSections = new List<SubSection>() {
        //            await _readingsRepository.MakeOldTestament("36.1:14-18*@+36.2:1-2"),
        //            await _readingsRepository.MakePsalmAndGospelLent("19.27:10", "42.9:18-22"),
        //        }
        //    },
        //    new Section(SectionType.EveSixthHour)
        //    {
        //        Title = $"{SixthHour} {EveOf} {NextDay}",
        //        SubSections = new List<SubSection>() {
        //            await _readingsRepository.MakeOldTestament("29.1:5-15"),
        //            await _readingsRepository.MakePsalmAndGospelLent("19.28:1,2", "41.10:32-34"),
        //        }
        //    },
        //    new Section(SectionType.EveNinthHour)
        //    {
        //        Title = $"{NinthHour} {EveOf} {NextDay}",
        //        SubSections = new List<SubSection>() {
        //            await _readingsRepository.MakeOldTestament("33.2:3-10"),
        //            await _readingsRepository.MakePsalmAndGospelLent("19.16:1,6", "41.8:27-33"),
        //        }
        //    },
        //    new Section(SectionType.EveEleventhHour)
        //    {
        //        Title = $"{EleventhHour} {EveOf} {NextDay}",
        //        SubSections = new List<SubSection>() {
        //            await _readingsRepository.MakeOldTestament("33.3:1-4"),
        //            await _readingsRepository.MakePsalmAndGospelLent("19.17:16,17", "40.17:19-23"),
        //        }
        //    }
        //};

        //foreach (var section in nextSections)
        //{
        //    dayReadings.Sections.Add(section);
        //}

        return dayReadings;
    }
}
