using System.ComponentModel.DataAnnotations;

namespace Katameros.Models
{
    public class BooksTranslation
    {
        public int BookId { get; set; }
        public int LanguageId { get; set; }
        [Required]
        public string Text { get; set; }

    }
}
