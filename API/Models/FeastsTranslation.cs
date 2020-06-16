using System.ComponentModel.DataAnnotations;

namespace Katameros.Models
{
    public class FeastsTranslation
    {
        public int FeastId { get; set; }
        public int LanguageId { get; set; }
        [Required]
        public string Text { get; set; }

    }
}
