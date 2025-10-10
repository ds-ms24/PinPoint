using System.ComponentModel.DataAnnotations;

namespace PinPoint.Models.Symptoms;

public class SymptomCreateVM
{
    [Required(ErrorMessage = "Symptom name is required")]
    [Display(Name = "Symptom Name")]
    [MaxLength(100, ErrorMessage = "Symptom name cannot exceed 100 characters")]
    public string Name { get; set; }

}
