using EntityObjects.Enaum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityObjects.Objects
{
   public class CommonLead
    {
        public int Id { get; set; }
        public DateTime GeneratedDateTime { get; set; } = DateTime.Now;
        public  DateTime ? UpdatedDateTime { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string PostCode { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ProductName { get; set; }
        public int SiteId { get; set; }
        public LeadType Status { get; set; }
        public string Source { get; set; } = string.Empty;
        public string Keyword { get; set; } = string.Empty;
        public string MatchType { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
      
        public ICollection<TransferLog> TransferLogs { get; set; }
        public ICollection<LifeLead> LifeLeads { get; set; }
        public ICollection<FuneralLead> FuneralLeads { get; set; }


    }
}
