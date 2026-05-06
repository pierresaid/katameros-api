using System.ComponentModel.DataAnnotations;

namespace Katameros.Models;

public class VersificationScheme
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}
