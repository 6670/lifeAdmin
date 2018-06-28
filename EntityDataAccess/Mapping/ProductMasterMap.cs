using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityObjects.Objects;

namespace EntityDataAccess.Mapping
{
    public class ProductMasterMap : EntityTypeConfiguration<ProductMaster>
    {
        public ProductMasterMap()
        {
            Property(t => t.ProductName).IsRequired();           
        }
    }
}
