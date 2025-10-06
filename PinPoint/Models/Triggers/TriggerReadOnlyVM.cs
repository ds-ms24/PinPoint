using System.ComponentModel.DataAnnotations;

namespace PinPoint.Models.Triggers
{
    public class TriggerReadOnlyVM : BaseTriggerVM
    {
        public string Name { get; set; } = string.Empty;
    }
}
