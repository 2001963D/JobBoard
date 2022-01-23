using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Shared.Domain
{
    public class JS : BaseDomainModel
    {
        public int Contact { get; set; }
        public string Email { get; set; }
        public string Resume { get; set; }
        public string Name { get; set; }

    }
}
