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
    public class StageMasterDetailMap : EntityTypeConfiguration<StageMaster>
    {
        public StageMasterDetailMap()
        {
            HasKey(t => t.ID);

            ////properties  
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.StageName).IsRequired();
            Property(t => t.StageDetail).IsRequired();

            ToTable("t_stage_master");
        }
    }
}
