//using EntityObjects;
//using EntityObjects.Objects;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EntityObjects.Objects;

namespace EntityDataAccess.Mapping
{
    public class CommonTableDetailMap : EntityTypeConfiguration<CommonLead>
    {
        public CommonTableDetailMap()
        {
            HasKey(t => t.ID);

            ////properties  
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Title).IsRequired();
            Property(t => t.FirstName).IsRequired();
            Property(t => t.LastName).IsRequired();
            Property(t => t.City).IsRequired();
            Property(t => t.Postcode).IsRequired();
            Property(t => t.Contact1).IsRequired();
            Property(t => t.Contact2).IsOptional();
            Property(t => t.Address).IsRequired();
            Property(t => t.Email).IsRequired();
            Property(t => t.ProductMasterId).IsRequired();
            Property(t => t.IpAddress).IsRequired();
            Property(t => t.Source).IsOptional();
            Property(t => t.MatchType).IsOptional();
            Property(t => t.Keyword).IsOptional();
            Property(t => t.StatusMasterId).IsRequired();
            Property(t => t.CreatedOn).IsRequired();
            Property(t => t.CreatedBy).IsRequired();
            Property(t => t.UpdatedOn).IsOptional();
            Property(t => t.UpdatedBy).IsOptional();
            Property(t => t.WebsiteMasterId).IsOptional();


            ToTable("t_common_leads");
        }
    }
}
