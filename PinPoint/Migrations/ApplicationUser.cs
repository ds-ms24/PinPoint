using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PinPoint.Migrations
{
    public class ApplicationUser : IdentityUser
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }
}
