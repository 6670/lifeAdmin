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
    class MortgagePropertyTypeDetailMap : EntityTypeConfiguration<MortgagePropertyTypeDetail>
    {
        public MortgagePropertyTypeDetailMap()
        {
            HasKey(t => t.ID);

            ////properties  
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.property_name).IsRequired();

            ToTable("t_mortgage_property_type");
        }
    }
}
