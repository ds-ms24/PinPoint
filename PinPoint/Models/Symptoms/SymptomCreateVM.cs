using System.ComponentModel.DataAnnotations;

namespace PinPoint.Models.Symptoms;

public class SymptomCreateVM
{
    public string Name { get; set; } = string.Empty;
    [Required]
    [Range(1, 90, ErrorMessage = "You must select a number between 1 and 90")]
    [Display(Name="Pain Duration (Days)")]
    public int NumberOfDays { get; set; }
}
