namespace PinPoint.Data;

public class PainEntry : BaseEntity
{   
    public DateOnly EntryDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public TimeOnly EntryTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
    public int PainIntensity { get; set; }
    public string PainDescription { get; set; }
    public int DurationMinutes { get; set; }
    public string ActivitiesBeforePain { get; set; }
    public string ReliefMethodsTried { get; set; }
    public int ReliefEffectiveness { get; set; }
    public string? AdditionalNotes { get; set; }

    public ICollection<PainEntrySymptom> PainEntrySymptoms { get; set; } = new List<PainEntrySymptom>();
    public ICollection<PainEntryLocation> PainEntryLocations { get; set; } = new List<PainEntryLocation>();
    public ICollection<PainEntryTrigger> PainEntryTriggers { get; set; } = new List<PainEntryTrigger>();
}
