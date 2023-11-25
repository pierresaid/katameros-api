using Katameros.Enums;

namespace Katameros.DTOs;

public class Section
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<SubSection> SubSections { get; set; }
    public Section(SectionType sectionType)
    {
        Id = (int)sectionType;
    }
}
