using System.ComponentModel.DataAnnotations;

namespace PinPoint.Models.Locations
{
    public class LocationDeleteVM : BaseLocationVM
    {
        [Display(Name="Location Name")]
        public string Name { get; set; } = string.Empty;
    }
}
