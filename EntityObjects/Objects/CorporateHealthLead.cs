using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityObjects.Objects
{
    public class CorporateHealthLead
    {
        public int Id { get; set; }
        public DateTime GeneratedDateTime { get; set; } = DateTime.Now;
        public DateTime? UpdatedDateTime { get; set; }
        public int CommonLeadId { get; set; }

        public DateTime DOB { get; set; }
        public int Age { get; set; }
        public string CompanyName { get; set; }
        public int EmployeeNumber { get; set; }
        public bool ExistingPolicy { get; set; }

        public CommonLead CommonLeads { get; set; }

    }
}
