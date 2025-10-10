namespace PinPoint.Data
{
    public class PainEntry : BaseEntity
    {   
        public DateOnly EntryDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public TimeOnly EntryTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
        public int? SymptomId { get; set; }
        public Symptom? Symptom { get; set; }
        public int? LocationId { get; set; }
        public Location? Location { get; set; }
        public int? TriggerId { get; set; }
        public Trigger? Trigger { get; set; }
        public int PainIntensity { get; set; }
        public string PainDescription { get; set; }
        public string ActivitiesBeforePain { get; set; }
        public string ReliefMethodsTried { get; set; }
        public int ReliefEffectiveness { get; set; }
        public string AdditionalNotes { get; set; }



        public ICollection<PainEntryTrigger> PainEntryTriggers { get; set; }
        public ICollection<PainEntrySymptom> PainEntrySymptoms { get; set; }
        public ICollection<PainEntryLocation> PainEntryLocations { get; set; }

        
    }
}
