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

        [Required(ErrorMessage = "Pain intensity is required")]
        [Display(Name = "Pain Intensity (0-10)")]
        [Range(0, 10, ErrorMessage = "Pain intensity must be between 0 and 10")]
        public int PainIntensity { get; set; }

        [Display(Name = "Pain Description")]
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string PainDescription { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [Display(Name = "Duration")]
        public int DurationMinutes { get; set; }

        [Display(Name = "Activities Before Pain Started")]
        [MaxLength(1000, ErrorMessage = "Activities description cannot exceed 1000 characters")]
        public string ActivitiesBeforePain { get; set; }

        [Display(Name = "Relief Methods Tried")]
        [MaxLength(1000, ErrorMessage = "Relief methods cannot exceed 1000 characters")]
        public string ReliefMethodsTried { get; set; }

        [Required(ErrorMessage = "Relief effectiveness is required")]
        [Display(Name = "Relief Effectiveness (0-10)")]
        [Range(0, 10, ErrorMessage = "Relief effectiveness must be between 0 and 10")]
        public int ReliefEffectiveness { get; set; }

        [Display(Name = "Additional Notes")]
        public string AdditionalNotes { get; set; }
    }
}
