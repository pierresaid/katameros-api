using System.ComponentModel.DataAnnotations;

namespace Katameros.Models
{
    public class ReadingsMetadata
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
