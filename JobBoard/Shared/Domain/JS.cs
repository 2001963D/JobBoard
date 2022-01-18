using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Shared.Domain
{
    public class JS : BaseDomainModel
    {
        public int JSContact { get; set; }
        public string JSEmail { get; set; }
        public string JSResume { get; set; }
        public string JSName { get; set; }

    }
}
