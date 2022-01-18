using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Shared.Domain
{
    public class Listing : BaseDomainModel
    {
        public string ListingLoc { get; set; }
        public string ListingDesc { get; set; }
        public string ListingName { get; set; }
        public int EmployerId { get; set; }
        public int AppointmentId { get; set; }
    }
}
