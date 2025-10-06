using System.ComponentModel.DataAnnotations;

namespace PinPoint.Models.Symptoms;

public class SymptomCreateVM
{
    [Display(Name="Symptom Name")]
    public string Name { get; set; } = string.Empty;

}
