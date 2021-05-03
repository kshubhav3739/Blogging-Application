using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingApp.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required(ErrorMessage = "Name Field can't be empty")]
        public string name { get; set; }
        [Required(ErrorMessage = "Email Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string email { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered Mobile No is not valid.")]
        public Int64 mobile { get; set; }

        [Required(ErrorMessage = "Password Field can't be empty")]
        public string password { get; set; }
        public DateTime dateTime { get; set; }

    }
}