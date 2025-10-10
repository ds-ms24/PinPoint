using System.ComponentModel.DataAnnotations;

namespace PinPoint.Models.PainEntries
{
    public class PainEntryDetailsVM : BasePainEntryVM
    {
        [Display(Name = "Entry Date")]
        [DataType(DataType.Date)]
        public DateOnly EntryDate { get; set; }
    
        [Display(Name = "Entry Time")]
        [DataType(DataType.Time)]
        public TimeOnly EntryTime { get; set; }

        public int? SymptomId { get; set; }

        [Display(Name = "Symptom")]
        public string? SymptomName { get; set; }

        [Display(Name = "New Symptom")]
        public string? NewSymptomName { get; set; }

        [Display(Name = "Location")]
        public int? LocationId { get; set; }

        [Display(Name = "Location")]
        public string? LocationName { get; set; }

        [Display(Name = "New Location")]
        [MaxLength(50, ErrorMessage = "Location name cannot exceed 50 characters")]
        public string? NewLocationName { get; set; }

        [Display(Name = "Trigger")]
        public int? TriggerId { get; set; }

        [Display(Name = "Trigger")]
        public string? TriggerName { get; set; }

        [Display(Name = "New Trigger")]
        [MaxLength(50, ErrorMessage = "Trigger name cannot exceed 50 characters")]
        public string? NewTriggerName { get; set; }

        [Display(Name = "Pain Intensity (0-10)")]
        public int PainIntensity { get; set; }
    
        [Display(Name = "Pain Description")]
        public string? PainDescription { get; set; }
    
        [Display(Name = "Duration (Minutes)")]
        public int DurationMinutes { get; set; }
    
        [Display(Name = "Activities Before Pain")]
        public string? ActivitiesBeforePain { get; set; }
    
        [Display(Name = "Relief Methods Tried")]
        public string? ReliefMethodsTried { get; set; }
    
        [Display(Name = "Relief Effectiveness (0-10)")]
        public int ReliefEffectiveness { get; set; }
    
        [Display(Name = "Additional Notes")]
        public string? AdditionalNotes { get; set; }
    }
}
