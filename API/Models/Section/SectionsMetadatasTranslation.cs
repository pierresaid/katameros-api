using System.ComponentModel.DataAnnotations;

namespace Katameros.Models
{
    public class SectionsMetadatasTranslation
    {
        public int SectionsId { get; set; }
        public int LanguageId { get; set; }
        public int SectionsMetadatasId { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
