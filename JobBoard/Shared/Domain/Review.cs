using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Shared.Domain
{
    public class Review : BaseDomainModel
    {
        [Required]
        [StringLength(1000, MinimumLength = 2, ErrorMessage = "Description does not meet length requirements")]
        public string Description { get; set; }
        [Required]
        public int? JSId { get; set; }
        public virtual JS JS { get; set; }
        public int? ListingId { get; set; }
        public virtual Listing Listing { get; set; }

    }
}
