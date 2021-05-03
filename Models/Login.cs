using System.ComponentModel.DataAnnotations;

namespace BloggingApp.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Email Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password Field can't be empty")]
        public string password { get; set; }
    }
}