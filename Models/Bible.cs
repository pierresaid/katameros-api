using System.ComponentModel.DataAnnotations;

namespace Katameros.Models
{
    public class Bible
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int LanguageId { get; set; }
    }
}
