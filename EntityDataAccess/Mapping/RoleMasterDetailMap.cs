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
   public class RoleMasterDetailMap : EntityTypeConfiguration<RoleMasterDetail>
    {

        public RoleMasterDetailMap()
        {

            HasKey(t => t.ID);

            ////properties  
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.role_name).IsRequired();
            Property(t => t.role_desc).IsRequired();



            ToTable("t_role_master");
        }
    }
}
