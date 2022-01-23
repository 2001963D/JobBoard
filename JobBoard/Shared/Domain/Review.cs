using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Shared.Domain
{
    public class Review : BaseDomainModel
    {
        public string Description { get; set; }
        public int? JSId { get; set; }
        public virtual JS JS { get; set; }
        public int? ListingId { get; set; }
        public virtual Listing Listing { get; set; }
    }
}
