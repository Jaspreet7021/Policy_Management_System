using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Policy_Management_System.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public Registration UserId { get; set; }
    }
}