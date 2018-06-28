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
   public class MortgagePeriodTermDetailMap : EntityTypeConfiguration<MortgagePeriodTermDetail>
    {

        public MortgagePeriodTermDetailMap()
        {
            HasKey(t => t.ID);

            ////properties  
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.term_name).IsRequired();

            ToTable("t_mortgage_period_term");
        }
    }
}
