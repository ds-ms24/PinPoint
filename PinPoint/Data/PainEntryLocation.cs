namespace PinPoint.Data
{
    public class PainEntryLocation
    {
        public int PainEntryId { get; set; }
        public PainEntry PainEntry { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
