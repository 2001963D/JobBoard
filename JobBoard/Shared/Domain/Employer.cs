using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Shared.Domain
{
    public class Employer : BaseDomainModel
    {
        public int EmployerContact { get; set; }
        public string EmployerEmail { get; set; }
        public int? ListingId { get; set; }
        public virtual Listing Listing { get; set; }
    }
}
