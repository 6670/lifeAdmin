using EntityObjects.Enaum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.WebsearchMedia.Models
{
    public class TransferCommonLeads
    {
        public int CommonLeadId { get; set; }
        public int BuyerId { get; set; }
        public DateTime GeneratedDateTime { get; set; } = DateTime.Now;
        public DateTime? TransferDateTime { get; set; }
        public string BuyerName { get; set; } = string.Empty;
        public string ProductName { get; set; }
        public LeadType Status { get; set; }
        public string Source { get; set; } = string.Empty;
        public string Keyword { get; set; } = string.Empty;
        public string MatchType { get; set; } = string.Empty;
        

        public string FullName { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string PostCode { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
       public string Descriptions { get; set; }
   
    }
}