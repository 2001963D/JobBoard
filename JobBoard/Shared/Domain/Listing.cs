using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Shared.Domain
{
    public class Listing : BaseDomainModel
    {
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name of Listing does not meet length requirements")]
        public string Name { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 2, ErrorMessage = "Description does not meet length requirements")]
        public string Description { get; set; }
        [Required]
        public int? EmployerId { get; set; }
        public virtual Employer Employer { get; set;}

        public virtual List<Appointment> Appointments { get; set; }
        [Required]
        public int? LocationId { get; set; }    
        public virtual Location Location { get; set; }
        public double Wage { get; set; }
        public string Image { get; set; }
    }
}
