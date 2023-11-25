using Katameros.Models;

namespace Katameros.DTOs;

public class Passage
{
    public int BookId { get; set; }
    public string BookTranslation { get; set; }
    public int Chapter { get; set; }
    public string Ref { get; set; }
    public List<Verse> Verses { get; set; }
}
