using System.ComponentModel.DataAnnotations;

namespace PinPoint.Data
{
    public class Location : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<PainEntryLocation> PainEntryLocations { get; set; }
    }
}
