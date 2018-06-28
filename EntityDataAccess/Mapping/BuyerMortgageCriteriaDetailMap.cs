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
    public class BuyerMortgageCriteriaDetailMap : EntityTypeConfiguration<BuyerMortgageCriteriaDetail>
    {
        public BuyerMortgageCriteriaDetailMap()
        {
            HasKey(t => t.ID);

            ////properties  
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.buyer_id).IsRequired();
            Property(t => t.product_id).IsRequired();
            Property(t => t.age).IsRequired();
            Property(t => t.employment_status).IsRequired();
            Property(t => t.period_term).IsRequired();
            Property(t => t.property_value).IsRequired();
            Property(t => t.loan_value).IsRequired();
            Property(t => t.current_debt).IsRequired();

            Property(t => t.property_location).IsRequired();
            Property(t => t.credit_history).IsRequired();
            Property(t => t.annual_income).IsRequired();

            Property(t => t.miss_any_loan).IsRequired();
            Property(t => t.had_bankruptcy).IsRequired();
            Property(t => t.applied_iva).IsRequired();
            Property(t => t.had_ccj).IsRequired();

            Property(t => t.city).IsRequired();
            Property(t => t.postcode).IsRequired();
            Property(t => t.Home_Mobile_Phone1).IsRequired();
            Property(t => t.Home_Mobile_Phone2).IsOptional();
            Property(t => t.address).IsRequired();
            Property(t => t.email).IsRequired();
            Property(t => t.date_of_birth).IsRequired();
            Property(t => t.property_type).IsRequired();
            Property(t => t.ltv).IsRequired();
            Property(t => t.rate_id).IsRequired();
            Property(t => t.repayment_id).IsRequired();


            ToTable("t_buyer_mortgage_criteria");
        }
    }
}
