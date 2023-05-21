using System.ComponentModel.DataAnnotations;

namespace SMSAPI.DTO
{
    public class LogInDTO
    {

        [Required]
        [EmailAddress]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
