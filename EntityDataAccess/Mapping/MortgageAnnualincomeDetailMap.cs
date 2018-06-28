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
    public class MortgageAnnualincomeDetailMap : EntityTypeConfiguration<MortgageAnnualincomeDetail>
    {

        public MortgageAnnualincomeDetailMap()
        {
            HasKey(t => t.ID);

            ////properties  
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.annual_income_value).IsRequired();

            ToTable("t_mortgage_annual_income");
        }
    }
}
