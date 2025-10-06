using System.ComponentModel.DataAnnotations;

namespace PinPoint.Models.Triggers
{
    public class TriggerDeleteVM : BaseTriggerVM
    {
        [Display(Name="Trigger Name")]
        public string Name { get; set; } = string.Empty;
    }
}
