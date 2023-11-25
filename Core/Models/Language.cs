using System.ComponentModel.DataAnnotations;

namespace Katameros.Models;

public class Language
{
    public int Id { get; set; }
    [Required]
    public string Code { get; set; }
    [Required]
    public string Name { get; set; }
}
