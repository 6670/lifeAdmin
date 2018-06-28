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
    public class StatusMasterDetailMap : EntityTypeConfiguration<StatusMaster>
    {
        public StatusMasterDetailMap()
        {
            HasKey(t => t.ID);

            ////properties  
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.StatusName).IsRequired();
            Property(t => t.StatusDesc).IsRequired();

            ToTable("t_status_type_master");
        }

    }
}
