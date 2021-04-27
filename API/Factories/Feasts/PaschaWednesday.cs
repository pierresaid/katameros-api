using Katameros.DTOs;
using Katameros.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Katameros.Repositories
{
    public partial class FeastsFactory
    {
        private async Task<DayReadings> ConstructPaschaWednesday()
        {
            DayReadings dayReadings = new DayReadings();
            dayReadings.Title = await GetFeastTranslation(Feast.PaschaWednesday);

            var FirstHour = await _readingsHelper.GetSectionMeta(SectionType.FirstHour, SectionsMetadata.Title);
            var ThirdHour = await _readingsHelper.GetSectionMeta(SectionType.ThirdHour, SectionsMetadata.Title);
            var SixthHour = await _readingsHelper.GetSectionMeta(SectionType.SixthHour, SectionsMetadata.Title);
            var NinthHour = await _readingsHelper.GetSectionMeta(SectionType.NinthHour, SectionsMetadata.Title);
            var EleventhHour = await _readingsHelper.GetSectionMeta(SectionType.EleventhHour, SectionsMetadata.Title);

            var EveOf = await _readingsHelper.GetSentence(Sentence.EveOf);
            var NextDay = await GetFeastTranslation(Feast.PaschaWednesday);

            dayReadings.Sections = new List<Section>() {
                new Section(SectionType.FirstHour)
                {
                    Title = FirstHour,
                    SubSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("2.17:1-7"),
                        await _readingsRepository.MakeOldTestament("20.3:5-15"),
                        await _readingsRepository.MakeOldTestament("28.5:13-6:3"),
                        await _readingsRepository.MakeOldTestament("73.1:20-2:15"),
                        await _readingsRepository.MakeOldTestament("73.3:12-24"),
                        await _readingsRepository.MakePsalmAndGospel("19.51:4*@+19.33:10", "43.11:46-57"),
                    }
                },
                new Section(SectionType.ThirdHour)
                {
                    Title = ThirdHour,
                    SubSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("2.13:17-22"),
                        await _readingsRepository.MakeOldTestament("73.22:7-18"),
                        await _readingsRepository.MakeOldTestament("18.27:16-20*@+18.28:1-2"),
                        await _readingsRepository.MakeOldTestament("20.4:4-5:4"),
                        await _readingsRepository.MakePsalmAndGospel("19.41:6,1", "42.22:1-6"),
                    }
                },
                new Section(SectionType.SixthHour)
                {
                    Title = SixthHour,
                    SubSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("2.14:13-15:1"),
                        await _readingsRepository.MakeOldTestament("23.48:1-6"),
                        await _readingsRepository.MakeOldTestament("73.23:7-14"),
                        await _readingsRepository.MakePsalmAndGospel("19.83:2,5", "43.12:1-8"),
                    }
                },
                new Section(SectionType.NinthHour)
                {
                    Title = NinthHour,
                    SubSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("1.24:1-9"),
                        await _readingsRepository.MakeOldTestament("4.20:1-13"),
                        await _readingsRepository.MakeOldTestament("20.1:10-23"),
                        await _readingsRepository.MakeOldTestament("23.59:1-17"),
                        await _readingsRepository.MakeOldTestament("38.11:11-14"),
                        await _readingsRepository.MakePsalmAndGospel("19.41:5-6", "40.26:3-16"),
                    }
                },
                new Section(SectionType.EleventhHour)
                {
                    Title = EleventhHour,
                    SubSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("23.28:16-29"),
                        await _readingsRepository.MakePsalmAndGospel("19.6:2*@+19.69:17", "43.12:27-36"),
                    }
                },
                new Section(SectionType.EveFirstHour)
                {
                    Title = $"{FirstHour} {EveOf} {NextDay}",
                    SubSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("26.43:5-11"),
                        await _readingsRepository.MakePsalmAndGospel("19.69:1,16", "43.10:17-21"),
                    }
                },
                new Section(SectionType.EveThirdHour)
                {
                    Title = $"{ThirdHour} {EveOf} {NextDay}",
                    SubSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("30.4:4-13"),
                        await _readingsRepository.MakePsalmAndGospel("19.55:21,1", "41.14:3-11"),
                    }
                },
                new Section(SectionType.EveSixthHour)
                {
                    Title = $"{SixthHour} {EveOf} {NextDay}",
                    SubSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("30.3:1-11"),
                        await _readingsRepository.MakePsalmAndGospel("19.140:1,2", "43.12:36-43"),
                    }
                },
                new Section(SectionType.EveNinthHour)
                {
                    Title = $"{NinthHour} {EveOf} {NextDay}",
                    SubSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("26.20:27-33"),
                        await _readingsRepository.MakePsalmAndGospel("19.7:1-2", "43.10:29-38"),
                    }
                },
                new Section(SectionType.EveEleventhHour)
                {
                    Title = $"{EleventhHour} {EveOf} {NextDay}",
                    SubSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("24.8:4-10"),
                        await _readingsRepository.MakePsalmAndGospel("19.62:7,6", "43.12:44-50"),
                    }
                }
            };
            return dayReadings;
        }
    }
}
