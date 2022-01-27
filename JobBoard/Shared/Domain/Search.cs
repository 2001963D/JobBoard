using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Shared.Domain
{
    public class Search 
    {
        public int SearchId { get; set; }
        public virtual List<Listing> Listings { get; set; }
        public int? ListingId { get; set; }
        public virtual Listing Listing { get; set; }
        public int? LocationId { get; set; }
        public virtual Location Location { get; set; }
    }
}
