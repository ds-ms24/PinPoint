namespace PinPoint.Data
{
    public class PainEntrySymptom
    {
        public int PainEntryId { get; set; }
        public PainEntry PainEntry { get; set; }

        public int SymptomId { get; set; }
        public Symptom Symptom { get; set; }
    }
}
