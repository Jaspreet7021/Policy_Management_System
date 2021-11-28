using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
namespace Policy_Management_System.Models
{
    public class PolicyContext:DbContext
    {
        public PolicyContext():base("name=DBCS")
        {

        }
        public DbSet<Role> Role { get; set; }
        public DbSet<Registration> Registration { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<PolicyDetail> PolicyDetail { get; set; }

    }
}