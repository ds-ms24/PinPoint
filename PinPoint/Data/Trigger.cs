namespace PinPoint.Data
{
    public class Trigger : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<PainEntryTrigger> PainEntryTriggers { get; set; }

        public virtual ICollection<PainEntry> PainEntries { get; set; } = new List<PainEntry>();


    }
}
