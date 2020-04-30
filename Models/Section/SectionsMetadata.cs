using System.ComponentModel.DataAnnotations;

namespace Katameros.Models
{
    public class SectionsMetadata
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
