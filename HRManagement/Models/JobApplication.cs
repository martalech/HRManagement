using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRManagement.Models
{
    /// <summary>
    /// Model for job application
    /// </summary>
    public class JobApplication
    {

        public int Id { get; set; }
        public int OfferId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public bool ContactAgreement { get; set; }
        [Required]
        [Url]
        public string CvUrl { get; set; }
    }
}
