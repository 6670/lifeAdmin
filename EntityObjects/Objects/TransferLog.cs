using EntityObjects.Enaum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityObjects.Objects
{
    public class TransferLog
    {

        public int ID { get; set; }
        public DateTime GeneratedDatetime { get; set; }
        public DateTime TransferDatetime { get; set; }
       // public IntegrationType BuyerIntegrationTypeID { get; set; }
        public string Description { get; set; }
        public int BuyerMasterId { get; set; }
        public bool IsActualPrice { get; set; }
        public int CommonLeadId { get; set; }
        public string BuyerReturnLeadId { get; set; } = string.Empty;
        public bool Return { get; set; }
        public double Price { get; set; }
        public LeadType Status { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public BuyerMaster BuyerMaster { get; set; }
        public CommonLead CommonLead { get; set; }

    }
}

