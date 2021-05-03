using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingApp.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string name { get; set; }
        public string blogTitle { get; set; }
        public int userId { get; set; }
        public string comment { get; set; }
        public DateTime dateTime { get; set; }
    }
}