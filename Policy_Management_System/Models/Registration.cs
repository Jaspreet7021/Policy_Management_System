using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Policy_Management_System.Models
{
    public class Registration
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Gender { get; set; }
        public string State { get; set; }  
        public string City { get; set; }
        public string Address { get; set; }
        [Required]
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}