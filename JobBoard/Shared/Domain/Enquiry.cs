using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Shared.Domain
{
    public class Enquiry : BaseDomainModel
    {
        public string Details { get; set; }
        public string Name { get; set; }
        public int Contact { get; set; }    
        public string Email { get; set; }
    }
}
