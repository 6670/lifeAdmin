using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityObjects.Objects
{
   public class ProductMaster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime GenerateDate { get; set; } = DateTime.Now;
        public bool State { get; set; }
        public ICollection<BuyerProduct> BuyerProducts { get; set; }
    }
}
