namespace PinPoint.Models.PainEntries
{
    public class PainEntryDeleteVM : BasePainEntryVM
    {
        public DateOnly EntryDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public TimeOnly EntryTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
        public int PainIntensity { get; set; }
        public string PainDescription { get; set; }
        public string ActivitiesBeforePain { get; set; }
        public string ReliefMethodsTried { get; set; }
        public int ReliefEffectiveness { get; set; }
        public string AdditionalNotes { get; set; }
    }
}
