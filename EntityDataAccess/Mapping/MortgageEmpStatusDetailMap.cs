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
  public  class MortgageEmpStatusDetailMap : EntityTypeConfiguration<MortgageEmpStatusDetail>
    {
        public MortgageEmpStatusDetailMap()
        {
            HasKey(t => t.ID);

            ////properties  
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.status_type).IsRequired();

            ToTable("t_mortgage_employee_status");
        }
    }
}
