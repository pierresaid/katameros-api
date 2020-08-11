using Katameros.DTOs;
using Katameros.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Katameros.Repositories
{
    public partial class FeastsRepository
    {
        private async Task<DayReadings> ConstructPaschaMonday()
        {
            DayReadings dayReadings = new DayReadings();
            dayReadings.Title = await GetFeastTranslation(Feast.PaschaMonday);
            dayReadings.Sections = new List<Section>() {
                new Section
                {
                    Title = await _readingsHelper.GetSectionMeta(SectionType.FirstHour, SectionsMetadata.Title),
                    subSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("1.1:1-31*@+1.2:1-3"),
                        await _readingsRepository.MakeOldTestament("23.5:1-9"),
                        await _readingsRepository.MakeOldTestament("73.1:1-19"),
                        await _readingsRepository.MakePsalmAndGospel("19.72:18-19", "41.11:12-24"),
                    }
                },
                new Section
                {
                    Title = await _readingsHelper.GetSectionMeta(SectionType.ThirdHour, SectionsMetadata.Title),
                    subSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("23.5:20-30"),
                        await _readingsRepository.MakeOldTestament("24.9:12-19"),
                        await _readingsRepository.MakePsalmAndGospel("19.122:1-2", "41.11:11-19"),
                    }
                },
                new Section
                {
                    Title = await _readingsHelper.GetSectionMeta(SectionType.SixthHour, SectionsMetadata.Title),
                    subSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("2.32:7-15"),
                        await _readingsRepository.MakeOldTestament("72.1:1-9"),
                        await _readingsRepository.MakePsalmAndGospel("19.122:4", "43.2:13-17"),
                    }
                },
                new Section
                {
                    Title = await _readingsHelper.GetSectionMeta(SectionType.NinthHour, SectionsMetadata.Title),
                    subSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("1.2:15-25*@+1.3:1-24"),
                        await _readingsRepository.MakeOldTestament("23.40:1-5"),
                        await _readingsRepository.MakeOldTestament("20.1:1-9"),
                        await _readingsRepository.MakePsalmAndGospel("19.65:5,4", "40.21:23-27"),
                    }
                },
                new Section
                {
                    Title = await _readingsHelper.GetSectionMeta(SectionType.EleventhHour, SectionsMetadata.Title),
                    subSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("23.50:1-3"),
                        await _readingsRepository.MakeOldTestament("73.1:20-30"),
                        await _readingsRepository.MakePsalmAndGospel("19.13:3-4", "43.8:51-59"),
                    }
                }
            };
            return dayReadings;
        }
    }
}
