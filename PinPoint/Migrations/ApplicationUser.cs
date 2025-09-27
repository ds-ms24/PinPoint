using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PinPoint.Migrations
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }
}
