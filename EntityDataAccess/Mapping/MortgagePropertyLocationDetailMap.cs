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
    public  class MortgagePropertyLocationDetailMap : EntityTypeConfiguration<MortgagePropertyLocationDetail>
    {
        public MortgagePropertyLocationDetailMap()
        {
            HasKey(t => t.ID);

            ////properties  
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.location_name).IsRequired();

            ToTable("t_property_location");
        }
    }
}
