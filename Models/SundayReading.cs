using System.ComponentModel.DataAnnotations;

namespace Katameros.Models
{
    public class SundayReading : IReadingRefs
    {
        public int Id { get; set; }
        [Required]
        public int Month_Number { get; set; }
        [Required]
        public string Month_Name { get; set; }
        [Required]
        public int Day { get; set; }

        public string Other { get; set; }
        public string Day_Tune { get; set; }
        public string DayName { get; set; }
        public string Season { get; set; }
        [Required]
        public string V_Psalm_Ref { get; set; }
        [Required]
        public string V_Gospel_Ref { get; set; }
        [Required]
        public string M_Psalm_Ref { get; set; }
        [Required]
        public string M_Gospel_Ref { get; set; }
        [Required]
        public string P_Gospel_Ref { get; set; }
        [Required]
        public string C_Gospel_Ref { get; set; }
        [Required]
        public string X_Gospel_Ref { get; set; }
        [Required]
        public string L_Psalm_Ref { get; set; }
        [Required]
        public string L_Gospel_Ref { get; set; }
        public string Prophecy { get => null; }
    }
}
