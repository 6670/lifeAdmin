using EntityObjects.Enaum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityObjects.Objects
{
   public class BuyerMaster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string ApiUrl { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public EntityState State { get; set; }

        public ICollection<TransferLog> TransferLogs { get; set; }
        public ICollection<BuyerProduct> BuyerProducts { get; set; }
       // public ICollection<BuyerProduct> BuyerProducts { get; set; }
    }
}
