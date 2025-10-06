namespace PinPoint.Data
{
    public class PainEntryTrigger
    {
        public int PainEntryId { get; set; }
        public PainEntry PainEntry { get; set; }

        public int TriggerId { get; set; }
        public Trigger Trigger { get; set; }
    }
}
