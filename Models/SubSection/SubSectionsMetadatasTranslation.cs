using System.ComponentModel.DataAnnotations;

namespace Katameros.Models
{
    public class SubSectionsMetadatasTranslation
    {
        public int SubSectionsId { get; set; }
        public int LanguageId { get; set; }
        public int SubSectionsMetadatasId { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
