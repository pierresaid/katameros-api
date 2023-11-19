using System.ComponentModel.DataAnnotations;

namespace Katameros.Models;

public class SentencesTranslation
{
    public int SentenceId { get; set; }
    public int LanguageId { get; set; }
    [Required]
    public string Text { get; set; }

}
