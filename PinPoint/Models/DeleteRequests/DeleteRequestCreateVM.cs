using System.ComponentModel.DataAnnotations;

namespace PinPoint.Models.DeleteRequests
{
    public class DeleteRequestCreateVM
    {
        [Required]
        public int PainEntryId { get; set; }
        
        [Display(Name = "Reason for Deletion Request")]
        [MaxLength(500)]
        public string? Reason { get; set; }
    }
}
