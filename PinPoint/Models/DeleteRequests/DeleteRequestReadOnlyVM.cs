using PinPoint.Data;

namespace PinPoint.Models.DeleteRequests
{
    public class DeleteRequestReadOnlyVM
    {
        public int Id { get; set; }
        public int PainEntryId { get; set; }
        public string RequestedByUserId { get; set; } = string.Empty;
        public string? RequestedByUserEmail { get; set; }
        public string? Reason { get; set; }
        public DateOnly RequestedDate { get; set; }
        public DeleteRequestStatusEnum Status { get; set; }
        public string? ReviewedByUserId { get; set; }
        public string? ReviewedByUserEmail { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public string? ReviewerNotes { get; set; }
        
        // Pain entry details
        public DateOnly? EntryDate { get; set; }
        public TimeOnly? EntryTime { get; set; }
        public string? SymptomNames { get; set; }
        public string? LocationNames { get; set; }
        public string? TriggerNames { get; set; }
        public int? PainIntensity { get; set; }
        public string? PainDescription { get; set; }
        public int? DurationMinutes { get; set; }
        public string? ActivitiesBeforePain { get; set; }
        public string? ReliefMethodsTried { get; set; }
        public int? ReliefEffectiveness { get; set; }
        public string? AdditionalNotes { get; set; }
    }
}
