using System.ComponentModel.DataAnnotations;

namespace PinPoint.Models.PainEntries
{
    public class PainEntryEditVM : BasePainEntryVM
    {
        [Display(Name = "Entry Date")]
        [DataType(DataType.Date)]
        public DateOnly EntryDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [Display(Name = "Entry Time")]
        [DataType(DataType.Time)]
        public TimeOnly EntryTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);

        [Display(Name = "Symptom")]
        public int? SymptomId { get; set; }

        [Display(Name = "Symptom")]
        public string? SymptomName { get; set; }
    
        [Display(Name = "New Symptom")]
        [MaxLength(100, ErrorMessage = "Symptom name cannot exceed 100 characters")]
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

        [Required(ErrorMessage = "Pain intensity is required")]
        [Display(Name = "Pain Intensity (0-10)")]
        [Range(0, 10, ErrorMessage = "Pain intensity must be between 0 and 10")]
        public int PainIntensity { get; set; }

        [Display(Name = "Pain Description")]
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? PainDescription { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [Display(Name = "Duration")]
        public int DurationMinutes { get; set; }

        [Display(Name = "Activities Before Pain Started")]
        [MaxLength(1000, ErrorMessage = "Activities description cannot exceed 1000 characters")]
        public string? ActivitiesBeforePain { get; set; }

        [Display(Name = "Relief Methods Tried")]
        [MaxLength(1000, ErrorMessage = "Relief methods cannot exceed 1000 characters")]
        public string? ReliefMethodsTried { get; set; }

        [Required(ErrorMessage = "Relief effectiveness is required")]
        [Display(Name = "Relief Effectiveness (0-10)")]
        [Range(0, 10, ErrorMessage = "Relief Effectiveness must be between 0 and 10")]
        public int ReliefEffectiveness { get; set; }

        [Display(Name = "Additional Notes")]
        public string? AdditionalNotes { get; set; }
    }
}
