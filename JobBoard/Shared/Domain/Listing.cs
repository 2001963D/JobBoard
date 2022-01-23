using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Shared.Domain
{
    public class Listing : BaseDomainModel
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public int? EmployerId { get; set; }
        public virtual Employer Employer{ get; set;}
        public virtual List<Appointment> Appointments { get; set; }
    }
}
