using Katameros.DTOs;
using Katameros.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Katameros.Repositories
{
    public partial class FeastsFactory
    {
        private async Task<DayReadings> ConstructPaschaTuesday()
        {
            DayReadings dayReadings = new DayReadings();
            dayReadings.Title = await GetFeastTranslation(Feast.PaschaTuesday);

            var FirstHour = await _readingsHelper.GetSectionMeta(SectionType.FirstHour, SectionsMetadata.Title);
            var ThirdHour = await _readingsHelper.GetSectionMeta(SectionType.ThirdHour, SectionsMetadata.Title);
            var SixthHour = await _readingsHelper.GetSectionMeta(SectionType.SixthHour, SectionsMetadata.Title);
            var NinthHour = await _readingsHelper.GetSectionMeta(SectionType.NinthHour, SectionsMetadata.Title);
            var EleventhHour = await _readingsHelper.GetSectionMeta(SectionType.EleventhHour, SectionsMetadata.Title);

            var EveOf = await _readingsHelper.GetSentence(Sentence.EveOf);
            var NextDay = await GetFeastTranslation(Feast.PaschaWednesday);

            dayReadings.Sections = new List<Section>() {
                new Section
                {
                    Title = FirstHour,
                    subSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("2.19:1-9"),
                        await _readingsRepository.MakeOldTestament("18.23:2-17*@+18.24:1-25"),
                        await _readingsRepository.MakeOldTestament("28.4:1-8"),
                        await _readingsRepository.MakePsalmAndGospel("19.120:2,6,7", "43.8:21-29"),
                    }
                },
                new Section
                {
                    Title = ThirdHour,
                    subSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("5.8:11-20"),
                        await _readingsRepository.MakeOldTestament("73.2:1-9"),
                        await _readingsRepository.MakeOldTestament("18.27:1-20*@+18.28:1-2"),
                        await _readingsRepository.MakeOldTestament("11.19:9-14"),
                        await _readingsRepository.MakePsalmAndGospel("19.119:154,155", "40.23:37-39*@+40.24:1-2"),
                    }
                },
                new Section
                {
                    Title = SixthHour,
                    subSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("26.21:3-13"),
                        await _readingsRepository.MakeOldTestament("73.4:20-31*@+73.5:1-2"),
                        await _readingsRepository.MakeOldTestament("23.1:1-9"),
                        await _readingsRepository.MakePsalmAndGospel("19.18:48,17", "43.8:12-20"),
                    }
                },
                new Section
                {
                    Title = NinthHour,
                    subSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("1.6:5-22*@+1.7:1-24*@+1.8:1-22*@+1.9:1-7"),
                        await _readingsRepository.MakeOldTestament("20.9:1-11"),
                        await _readingsRepository.MakeOldTestament("23.40:9-31"),
                        await _readingsRepository.MakeOldTestament("27.7:9-15"),
                        await _readingsRepository.MakeOldTestament("20.8:1-12"),
                        await _readingsRepository.MakePsalmAndGospel("19.25:1,2,3", "40.24:3-35"),
                    }
                },
                new Section
                {
                    Title = EleventhHour,
                    subSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("23.30:25-30"),
                        await _readingsRepository.MakeOldTestament("20.6:20-35*@+20.7:1-4"),
                        await _readingsRepository.MakePsalmAndGospel("19.45:6*@+19.41:1", "40.25:14-46*@+40.26:1-2"),
                    }
                },
                new Section
                {
                    Title = $"{FirstHour} {EveOf} {NextDay}",
                    subSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("26.22:17-22"),
                        await _readingsRepository.MakeOldTestament("26.22:23-29"),
                        await _readingsRepository.MakePsalmAndGospel("19.59:16,17", "40.22:1-14"),
                    }
                },
                new Section
                {
                    Title = $"{ThirdHour} {EveOf} {NextDay}",
                    subSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("30.5:18-27"),
                        await _readingsRepository.MakePsalmAndGospel("19.65:4", "40.24:36-51"),
                    }
                },
                new Section
                {
                    Title = $"{SixthHour} {EveOf} {NextDay}",
                    subSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("24.16:9-13"),
                        await _readingsRepository.MakePsalmAndGospel("19.102:1,2", "40.25:1-13"),
                    }
                },
                new Section
                {
                    Title = $"{NinthHour} {EveOf} {NextDay}",
                    subSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("28.9:14-17*@+28.10:1-2"),
                        await _readingsRepository.MakePsalmAndGospel("19.22:20-21", "40.23:29-36"),
                    }
                },
                new Section
                {
                    Title = $"{EleventhHour} {EveOf} {NextDay}",
                    subSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("72.7:24-30"),
                        await _readingsRepository.MakePsalmAndGospel("19.57:1", "43.11:55-57"),
                    }
                }
            };
            return dayReadings;
        }
    }
}
