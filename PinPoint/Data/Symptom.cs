namespace PinPoint.Data
{
    public class Symptom : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<PainEntrySymptom> PainEntrySymptoms { get; set; }
    }
}
