using System.ComponentModel.DataAnnotations;

namespace PinPoint.Models.Triggers
{
    public class TriggerEditVM : BaseTriggerVM
    {
        [Display(Name="Trigger Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
    }
}
