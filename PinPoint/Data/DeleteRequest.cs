using System.ComponentModel.DataAnnotations;

namespace PinPoint.Data
{
    public class DeleteRequest : BaseEntity
    {        
        [Required]
        public int PainEntryId { get; set; }
        public PainEntry PainEntry { get; set; } = null!;
        
        [Required]
        [MaxLength(450)]
        public string RequestedByUserId { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string? Reason { get; set; }
        
        public DateOnly RequestedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public TimeOnly RequestedTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
        
        [Required]
        public DeleteRequestStatusEnum Status { get; set; } = DeleteRequestStatusEnum.Pending;
        
        [MaxLength(450)]
        public string? ReviewedByUserId { get; set; }
        
        public DateTime? ReviewedDate { get; set; }
        
        [MaxLength(500)]
        public string? ReviewerNotes { get; set; }
    }
}
