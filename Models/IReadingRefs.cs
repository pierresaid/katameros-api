namespace Katameros.Models
{
    public interface IReadingRefs
    {
        int Id { get; }
        public string V_Psalm_Ref { get; }
        public string V_Gospel_Ref { get; }
        public string M_Psalm_Ref { get; }
        public string M_Gospel_Ref { get; }
        public string P_Gospel_Ref { get; }
        public string C_Gospel_Ref { get; }
        public string X_Gospel_Ref { get; }
        public string L_Psalm_Ref { get; }
        public string L_Gospel_Ref { get; }
        public string Prophecy { get; }
    }
}