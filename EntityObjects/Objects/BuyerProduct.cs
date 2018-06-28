using EntityObjects.Enaum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityObjects.Objects
{
   public class BuyerProduct
    {
        public int Id { get; set; }
        public int BuyerMasterId { get; set; }
        public int ProductMasterId { get; set; }

        public int Quata { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Piority { get; set; }
        public bool State { get; set; }
        public BuyerIntegrationType IntegrationType { get; set; }
        public string BuyerApiUrl { get; set; }

        public BuyerMaster BuyerMaster { get; set; }
        public ProductMaster ProductMaster { get; set; }
    }
}
