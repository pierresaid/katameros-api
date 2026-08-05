using System.ComponentModel.DataAnnotations;

namespace Katameros.Models;

public class FastsTranslation
{
    public int FastId { get; set; }
    public int LanguageId { get; set; }
    [Required]
    public string Text { get; set; }

}
