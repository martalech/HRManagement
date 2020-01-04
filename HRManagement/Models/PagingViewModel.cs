using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRManagement.Models
{
    public class PagingViewModel
    {
        public IEnumerable<JobOffer> JobOffers { get; set; }
        public int TotalPage { get; set; }
    }
}
