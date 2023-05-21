using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SMSAPI.Model
{
    public class ApplicationUser : IdentityUser
    {
     
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }
        public string? Address { get; set; }
    }
}