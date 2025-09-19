using System.ComponentModel.DataAnnotations;

namespace PinPoint.Models.Symptoms
{
    public class SymptomEditVM : BaseSymptomVM
    {   
        [Required]
        public string Name { get; set; } = string.Empty;

        // Placeholder before refactor to different durations
        [Required]
        [Range(1, 90, ErrorMessage = "You must select a number between 1 and 90")]
        public int NumberOfDays { get; set; }
    }
}
