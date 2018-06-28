using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EntityObjects.Objects;

namespace EntityDataAccess.Mapping
{
    class MortgageLeadDetailMap : EntityTypeConfiguration<MortgageLead>
    {
        public MortgageLeadDetailMap()
        {
            HasKey(t => t.ID);

            ////properties  
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.CommonLeadId).IsRequired();
            Property(t => t.DateOfBirth).IsRequired();
            Property(t => t.Age).IsRequired();
            Property(t => t.EmploymentStatus).IsRequired();
            Property(t => t.PeriodTerm).IsRequired();
            Property(t => t.PropertyValue).IsRequired();
            Property(t => t.LoanValue).IsRequired();
            Property(t => t.CurrentDebt).IsRequired();
            Property(t => t.PropertyType).IsRequired();
            Property(t => t.PropertyLocation).IsRequired();
            Property(t => t.CreditHistory).IsRequired();
            Property(t => t.AnnualIncome).IsRequired();
            Property(t => t.MissAnyLoan).IsRequired();
            Property(t => t.HadBankruptcy).IsRequired();
            Property(t => t.AppliedIva).IsRequired();
            Property(t => t.HadCcj).IsRequired();
            Property(t => t.RateId).IsRequired();
            Property(t => t.RepaymentId).IsRequired();
            Property(t => t.CreatedOn).IsRequired();
            Property(t => t.CreatedBy).IsRequired();
            Property(t => t.UpdatedOn).IsOptional();
            Property(t => t.UpdatedBy).IsOptional();


            ToTable("t_mortgage_lead");
        }
    }
}
