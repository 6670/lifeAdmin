using EntityObjects.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDataAccess.Mapping
{
    public class WebsiteMasterDetailMap : EntityTypeConfiguration<WebsiteMaster>
    {
        public WebsiteMasterDetailMap()
        {
            Property(t => t.WebsiteAddress).IsRequired();
            Property(t => t.WebsiteStatus).IsRequired();
            Property(t => t.WebsiteName).IsRequired();
        }
    }
}
