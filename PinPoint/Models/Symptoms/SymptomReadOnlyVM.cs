using System.ComponentModel.DataAnnotations;

namespace PinPoint.Models.Symptoms;

public class SymptomReadOnlyVM : BaseSymptomVM 
{
        public string Name { get; set; } = string.Empty;
}
