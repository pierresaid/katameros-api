using Katameros.DTOs;
using Katameros.Enums;

namespace Katameros.Repositories;

public partial class FeastsFactory
{
    private async Task<DayReadings> ConstructPaschaFriday()
    {
        DayReadings dayReadings = new DayReadings();
        dayReadings.Title = await GetFeastTranslation(Feast.PaschaFriday);

        var FirstHour = await _readingsHelper.GetSectionMeta(SectionType.FirstHour, SectionsMetadata.Title);
        var ThirdHour = await _readingsHelper.GetSectionMeta(SectionType.ThirdHour, SectionsMetadata.Title);
        var SixthHour = await _readingsHelper.GetSectionMeta(SectionType.SixthHour, SectionsMetadata.Title);
        var NinthHour = await _readingsHelper.GetSectionMeta(SectionType.NinthHour, SectionsMetadata.Title);
        var EleventhHour = await _readingsHelper.GetSectionMeta(SectionType.EleventhHour, SectionsMetadata.Title);
        var TwelfthHour = await _readingsHelper.GetSectionMeta(SectionType.TwelfthHour, SectionsMetadata.Title);

        var EveOf = await _readingsHelper.GetSentence(Sentence.EveOf);
        var NextDay = await GetFeastTranslation(Feast.PaschaFriday);

        dayReadings.Sections = new List<Section>() {
            new Section(SectionType.FirstHour)
            {
                Title = FirstHour,
                SubSections = new List<SubSection>() {
                    await _readingsRepository.MakeOldTestament("5.8:19-9:24"),
                    await _readingsRepository.MakeOldTestament("23.1:2-9"),
                    await _readingsRepository.MakeOldTestament("23.2:10-21"),
                    await _readingsRepository.MakeOldTestament("24.22:29-23:6"),
                    await _readingsRepository.MakeOldTestament("23.24:1-13"),
                    await _readingsRepository.MakeOldTestament("72.2:12-22"),
                    await _readingsRepository.MakeOldTestament("18.12:18-13:1"),
                    await _readingsRepository.MakeOldTestament("38.11:11-14"),
                    await _readingsRepository.MakeOldTestament("33.1:16-2:3"),
                    await _readingsRepository.MakeOldTestament("33.7:1-8"),
                    await _readingsRepository.MakePsalmsAndGospels(Tk.A("19.27:12*@+19.35:11,12,16"), Tk.A("40.27:1-14", "41.15:1-5", "42.22:66-23:12", "43.18:28-40")),
                }
            },
            new Section(SectionType.ThirdHour)
            {
                Title = ThirdHour,
                SubSections = new List<SubSection>() {
                    await _readingsRepository.MakeOldTestament("1.48:1-19"),
                    await _readingsRepository.MakeOldTestament("23.50:4-9"),
                    await _readingsRepository.MakeOldTestament("23.3:9-15"),
                    await _readingsRepository.MakeOldTestament("23.63:1-7"),
                    await _readingsRepository.MakeOldTestament("30.9:4-6*@+30.9:8-10"),
                    await _readingsRepository.MakeOldTestament("18.29:21-30:10"),
                    await _readingsRepository.MakePsalmsAndGospels(Tk.A("19.38:17*@+19.22:16"), Tk.A("40.27:15-26", "41.15:6-25", "42.23:13-25", "43.19:1-12")),

                }
            },
            new Section(SectionType.SixthHour)
            {
                Title = SixthHour,
                SubSections = new List<SubSection>() {
                    await _readingsRepository.MakeOldTestament("4.21:1-9"),
                    await _readingsRepository.MakeOldTestament("23.53:7-12"),
                    await _readingsRepository.MakeOldTestament("23.12:2-13:10"),
                    await _readingsRepository.MakeOldTestament("30.8:9-12"),
                    await _readingsRepository.MakePauline("48.6:14-18"),
                    await _readingsRepository.MakePsalmsAndGospels(Tk.A("19.38:21,22*@+19.22:17,18,19,8,9"), Tk.A("40.27:27-45", "41.15:26-33", "42.23:26-44", "43.19:1-12")),
                }
            },
            new Section(SectionType.NinthHour)
            {
                Title = NinthHour,
                SubSections = new List<SubSection>() {
                    await _readingsRepository.MakeOldTestament("24.11:18-12:13"),
                    await _readingsRepository.MakeOldTestament("38.14:5-11"),
                    await _readingsRepository.MakeOldTestament("29.2:1-3*@+29.2:10-11"),
                    await _readingsRepository.MakePauline("50.2:5-11"),
                    await _readingsRepository.MakePsalmsAndGospels(Tk.A("19.69:2,3,21"), Tk.A("40.27:46-50", "41.15:34-37", "42.23:45-46", "43.19:28-30")),

                }
            },
            new Section(SectionType.EleventhHour)
            {
                Title = EleventhHour,
                SubSections = new List<SubSection>() {
                    await _readingsRepository.MakeOldTestament("2.12:1-14"),
                    await _readingsRepository.MakeOldTestament("3.23:5-12"),
                    await _readingsRepository.MakePsalmsAndGospels(Tk.A("19.143:6,7*@+19.31:5"), Tk.A("40.27:51-56", "41.15:38-41", "42.23:47-49", "43.19:31-37")),
                }
            },
            new Section(SectionType.TwelfthHour)
            {
                Title = TwelfthHour,
                SubSections = new List<SubSection>() {
                    await _readingsRepository.MakeOldTestament("25.3:1-66"),
                    await _readingsRepository.MakeOldTestament("32.1:10-2:8"),
                    await _readingsRepository.MakePsalmsAndGospels(Tk.A("19.88:6*@+19.23:4*@+19.45:6,8"), Tk.A("40.27:57-61", "41.15:42-16:1", "42.23:50-56", "43.19:38-42")),

                }
            }
        };
        return dayReadings;
    }
}
