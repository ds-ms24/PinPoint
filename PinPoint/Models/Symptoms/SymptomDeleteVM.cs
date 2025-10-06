using System.ComponentModel.DataAnnotations;

namespace PinPoint.Models.Symptoms
{
    public class SymptomDeleteVM : BaseSymptomVM
    {
        [Display(Name="Symptom Name")]
        public string Name { get; set; } = string.Empty;
    }
}
