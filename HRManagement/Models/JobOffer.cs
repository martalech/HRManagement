using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HRManagement.Models
{
    /// <summary>
    /// Model for job offer
    /// </summary>
    public class JobOffer
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public int Salary { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string ContractType { get; set; }
        public Company CompanyName { get; set; }
        public List<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
    }
}
