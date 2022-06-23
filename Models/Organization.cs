using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium_2.Models
{
    public class Organization
    {
        public int OrganizationID { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationDomain { get; set; }

        public virtual IEnumerable<Member> Members { get; set; }
        public virtual IEnumerable<Team> Teams { get; set; }
    }
}
