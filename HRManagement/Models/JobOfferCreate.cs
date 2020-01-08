using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRManagement.Models
{
    public class JobOfferCreate
    {
        public string JobTitle { get; set; }
        public int Salary { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string ContractType { get; set; }
        public virtual Company CompanyName { get; set; }
    }
}
