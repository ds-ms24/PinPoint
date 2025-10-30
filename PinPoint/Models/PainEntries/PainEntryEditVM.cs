using PinPoint.Data;
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

        [Required(ErrorMessage = "Pain intensity is required")]
        [Display(Name = "Pain Intensity (0-10)")]
        [Range(0, 10, ErrorMessage = "Pain intensity must be between 0 and 10")]
        public int PainIntensity { get; set; }

        [Display(Name = "Symptoms")]
        public List<int> SymptomIds { get; set; } = new List<int>();

        [Display(Name = "Symptom")]
        public string? SymptomNames { get; set; }
    
        [Display(Name = "New Symptom")]
        [MaxLength(30, ErrorMessage = "Symptom name cannot exceed 30 characters")]
        public string? NewSymptomName { get; set; }

        [Display(Name = "Location")]
        public List<int> LocationIds { get; set; } = new List<int>();

        [Display(Name = "Location")]
        public string? LocationNames { get; set; }
    
        [Display(Name = "New Location")]
        [MaxLength(30, ErrorMessage = "Location name cannot exceed 30 characters")]
        public string? NewLocationName { get; set; }

        [Display(Name = "Trigger")]
        public List<int> TriggerIds { get; set; } = new List<int>();

        [Display(Name = "Trigger")]
        public string? TriggerNames { get; set; }
    
        [Display(Name = "New Trigger")]
        [MaxLength(30, ErrorMessage = "Trigger name cannot exceed 30 characters")]
        public string? NewTriggerName { get; set; }

        [Required(ErrorMessage = "Pain description is required")]
        [Display(Name = "Pain Description")]
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? PainDescription { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [Display(Name = "Duration")]
        public int DurationMinutes { get; set; }

        [Required(ErrorMessage = "Activities description is required")]
        [Display(Name = "Activities Before Pain Started")]
        [MaxLength(1000, ErrorMessage = "Activities description cannot exceed 1000 characters")]
        public string? ActivitiesBeforePain { get; set; }

        [Required(ErrorMessage = "Relief methods is required")]
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
