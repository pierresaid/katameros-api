using System.ComponentModel.DataAnnotations;

namespace Katameros.Models
{
    public class Reading
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
