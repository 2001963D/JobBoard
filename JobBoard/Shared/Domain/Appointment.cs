using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Shared.Domain
{
    public class Appointment : BaseDomainModel
    {
        public string AppointmentLoc { get; set; }
        public DateTime DateAndTime {get; set;}
    }
}
