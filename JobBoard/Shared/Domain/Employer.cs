using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Shared.Domain
{
    public class Employer : BaseDomainModel
    {
        public string Name { get; set; }
        public int Contact { get; set; }
        public string Email { get; set; }
       
    }
}
