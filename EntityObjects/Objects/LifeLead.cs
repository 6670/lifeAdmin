using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityObjects.Objects
{
   public class LifeLead
    {
        public int Id { get; set; }
        public DateTime GeneratedDateTime { get; set; } = DateTime.Now;
        public DateTime? UpdatedDateTime { get; set; }
        public int CommonLeadId { get; set; }
        public DateTime DOB { get; set; }
        public int Age { get; set; }
        public bool Smoker { get; set; }
        public string ProductType { get; set; }
        public int CoverAmount { get; set; }
        public bool SingleOwnership { get; set; }
        public int CoverPeriod { get; set; }
        public CommonLead CommonLeads { get; set; }

    }
}
