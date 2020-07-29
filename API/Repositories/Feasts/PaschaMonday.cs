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
                        await _readingsRepository.MakeOldTestament("73.1:1-24"),
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
                        await _readingsRepository.MakeOldTestament("20.10:1-9"),
                        await _readingsRepository.MakePsalmAndGospel("19.65:6,5", "40.21:23-27"),
                    }
                },
                new Section
                {
                    Title = await _readingsHelper.GetSectionMeta(SectionType.EleventhHour, SectionsMetadata.Title),
                    subSections = new List<SubSection>() {
                        await _readingsRepository.MakeOldTestament("23.50:1-3"),
                        await _readingsRepository.MakeOldTestament("73.1:20-30"),
                        await _readingsRepository.MakePsalmAndGospel("19.13:4-5", "43.8:51-59"),
                    }
                }
            };
            //dayReadings.Sections.Add(firstHour);
            //var liturgy = await _readingsRepository.MakeLitugy("58.9:11-28", "60.4:1-11", "44.28:11-31", null, null);
            //liturgy.subSections.Add(await _readingsRepository.MakePsalmAndGospel("19.81:3,1,2", "40.21:1-17"));
            //liturgy.subSections.Add(await _readingsRepository.MakePsalmAndGospel(null, "41.11:1-11"));
            //liturgy.subSections.Add(await _readingsRepository.MakePsalmAndGospel(null, "42.19:29-48"));
            //liturgy.subSections.Add(await _readingsRepository.MakePsalmAndGospel("19.65:1-2", "43.12:12-19"));
            //dayReadings.Sections.Add(liturgy);
            return dayReadings;
        }
    }
}
