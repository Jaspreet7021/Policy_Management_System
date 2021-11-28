using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Policy_Management_System.Models
{
    public class PolicyDetail
    {
        public int Id { get; set; }
        [Required]
        public string PolicyName { get; set; }
        [Required]
        public double SumAssured { get; set; }
        [Required]
        public int Premium { get; set; }
        [Required]
        public int Tenure { get; set; }
        [Required]
        [AllowHtml]
        public string PolicyInfo { get; set; }
        public Registration ProposerId { get; set; }
        public Category CatId { get; set; }
        
    }
}