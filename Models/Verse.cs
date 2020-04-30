using System.ComponentModel.DataAnnotations;

namespace Katameros.Models
{
    public class Verse
    {
        public int Id { get; set; }
        [Required]
        public int BibleId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int Chapter { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
