using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingApp.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Name Field can't be empty")]
        public string name { get; set; }

        [Required(ErrorMessage = "Email Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string email { get; set; }

        [Required(ErrorMessage = "Coment Field can't be empty")]
        public string commentDes { get; set; }

        public DateTime dateTime { get; set; }
        public int blogId { get; set; }
    }
}