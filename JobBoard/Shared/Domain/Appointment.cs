using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Shared.Domain
{
    public class Appointment : BaseDomainModel
    {
        public int? LocationId { get; set; }
        public virtual Location Location { get; set; }
        public DateTime DateAndTime {get; set;}
        public int? ListingId { get; set; }
        public virtual Listing Listing { get; set; }
        
    }
}
