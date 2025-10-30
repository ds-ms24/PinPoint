using PinPoint.Data;
using System.ComponentModel.DataAnnotations;

namespace PinPoint.Models.PainEntries
{
    public class PainEntryReadOnlyVM : BasePainEntryVM
    {
        [Display(Name = "Entry Date")]
        [DataType(DataType.Date)]
        public DateOnly EntryDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [Display(Name = "Entry Time")]
        [DataType(DataType.Time)]
        public TimeOnly EntryTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);

        [Display(Name = "Symptom")]
        public List<int> SymptomIds { get; set; } = new List<int>();

        [Display(Name = "Symptom")]
        public string? SymptomNames { get; set; }
    
        [Display(Name = "New Symptom")]
        public string? NewSymptomName { get; set; }

        [Display(Name = "Location")]
        public List<int> LocationIds { get; set; } = new List<int>();

        [Display(Name = "Location")]
        public string? LocationNames { get; set; }
    
        [Display(Name = "New Location")]
        public string? NewLocationName { get; set; }

        [Display(Name = "Trigger")]
        public List<int> TriggerIds { get; set; } = new List<int>();

        [Display(Name = "Trigger")]
        public string? TriggerNames { get; set; }
    
        [Display(Name = "New Trigger")]
        public string? NewTriggerName { get; set; }

        [Display(Name = "Pain Intensity (0-10)")]
        public int PainIntensity { get; set; }

        [Display(Name = "Pain Description")]
        public string? PainDescription { get; set; }

        [Display(Name = "Duration")]
        public int DurationMinutes { get; set; }

        [Display(Name = "Activities Before Pain Started")]
        public string? ActivitiesBeforePain { get; set; }

        [Display(Name = "Relief Methods Tried")]
        public string? ReliefMethodsTried { get; set; }

        [Display(Name = "Relief Effectiveness (0-10)")]
        public int ReliefEffectiveness { get; set; }

        [Display(Name = "Additional Notes")]
        public string? AdditionalNotes { get; set; }
    }
}
