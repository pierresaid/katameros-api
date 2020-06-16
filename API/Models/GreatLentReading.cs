using System.ComponentModel.DataAnnotations;

namespace Katameros.Models
{
    public class GreatLentReading : IReadingRefs
    {
        public int Id { get; set; }
        [Required]
        public int Week { get; set; }
        [Required]
        public int DayOfWeek { get; set; }
        public string DayName { get; set; }
        public string Seasonal_Tune { get; set; }
        public string Weather_Prayers { get; set; }
        public string V_Psalm_Ref { get; set; }
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
        public string Prophecy { get; set; }
    }
}
