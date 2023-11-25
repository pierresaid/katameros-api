using Katameros.DTOs;
using Katameros.Enums;

namespace Katameros.Repositories;

public partial class FeastsFactory
{
    private async Task<DayReadings> ConstructEaster()
    {
        Section liturgy = await MakeLiturgy();

        DayReadings dayReadings = new DayReadings
        {
            Title = await GetFeastTranslation(Feast.Easter),
            Sections = new List<Section>
            {
                await _readingsRepository.MakeMatins("19.78:65,69", "41.16:2-11"),
                liturgy
            }
        };
        return dayReadings;
    }

    private async Task<Section> MakeLiturgy()
    {
        var paulineRef = "46.15:23-50";
        var catholicRef = "60.3:15-4:6";
        var actsRef = "44.2:22-28";
        var psalmRef = "19.118:24,25,27";
        var gospelRef = "43.20:1-18";
        var psalmRef2 = "19.96:10";
        var gospelRef2 = "43.20:19-23";

        var liturgy = new Section(SectionType.Liturgy);
        var subSections = new List<SubSection>();

        liturgy.Title = await _readingsHelper.GetSectionMeta(SectionType.Liturgy, SectionsMetadata.Title);

        subSections.Add(await _readingsRepository.MakePauline(paulineRef));
        subSections.Add(await _readingsRepository.MakeCatholic(catholicRef));
        subSections.Add(await _readingsRepository.MakeActs(actsRef));
        subSections.Add(await _readingsRepository.MakePsalmAndGospel(psalmRef, gospelRef));
        subSections.Add(await _readingsRepository.MakePsalmAndGospel(psalmRef2, gospelRef2));

        liturgy.SubSections = subSections;
        return liturgy;
    }
}
