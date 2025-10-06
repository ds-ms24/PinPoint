using System.ComponentModel.DataAnnotations;

namespace PinPoint.Models.Locations
{
    public class LocationReadOnlyVM : BaseLocationVM
    {
        public string Name { get; set; } = string.Empty;
    }
}
