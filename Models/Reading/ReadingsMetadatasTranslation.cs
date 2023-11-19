using System.ComponentModel.DataAnnotations;

namespace Katameros.Models;

public class ReadingsMetadatasTranslation
{
    public int ReadingId { get; set; }
    public int LanguageId { get; set; }
    public int ReadingsMetadatasId { get; set; }
    [Required]
    public string Text { get; set; }
}
