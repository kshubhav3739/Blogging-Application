using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingApp.Models
{
    public class Blog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required(ErrorMessage = "Title Field can't be empty")]
        public string title { get; set; }
        [Required(ErrorMessage = "Description Field can't be empty")]
        public string description { get; set; }
        public DateTime dateTime { get; set; }
        public int userId { get; set; }
    }
}