using EntityObjects.Objects;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace EntityDataAccess.Core
{
    public class EFDbContext : DbContext
    {
        static EFDbContext()
        {
            Database.SetInitializer<EFDbContext>(null);
        }
        public EFDbContext() : base("name=DbConnectionString")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<CommonLead> CommonLeads { get; set; }
        public DbSet<LifeLead> LifeLeads { get; set; }
        public DbSet<BuyerMaster> BuyerMasters { get; set; }
        public DbSet<ProductMaster> ProductMasters { get; set; }
        public DbSet<BuyerProduct> BuyerProducts { get; set; }
        public DbSet<TransferLog> TransferLogs { get; set; }
        public DbSet<FuneralLead> FuneralLeads { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommonLead>().Property(x => x.Address).IsRequired();
            modelBuilder.Entity<CommonLead>().Property(x => x.City).IsRequired();
            modelBuilder.Entity<CommonLead>().Property(x => x.FirstName).IsRequired();
            modelBuilder.Entity<CommonLead>().Property(x => x.GeneratedDateTime).IsRequired();
            modelBuilder.Entity<CommonLead>().Property(x => x.HomePhone).IsRequired();
            modelBuilder.Entity<CommonLead>().Property(x => x.IpAddress).IsOptional();
            modelBuilder.Entity<CommonLead>().Property(x => x.Keyword).IsOptional();
            modelBuilder.Entity<CommonLead>().Property(x => x.LastName).IsRequired();
            modelBuilder.Entity<CommonLead>().Property(x => x.MatchType).IsOptional();
            modelBuilder.Entity<CommonLead>().Property(x => x.PostCode).IsRequired();
            modelBuilder.Entity<CommonLead>().Property(x => x.ProductName).IsRequired();
            modelBuilder.Entity<CommonLead>().Property(x => x.SiteId).IsRequired();
            modelBuilder.Entity<CommonLead>().Property(x => x.Source).IsOptional();
            modelBuilder.Entity<CommonLead>().Property(x => x.Status).IsOptional();
            modelBuilder.Entity<CommonLead>().Property(x => x.Title).IsRequired();
            modelBuilder.Entity<CommonLead>().Property(x => x.UpdatedDateTime).IsOptional();
            modelBuilder.Entity<CommonLead>().Property(x => x.WorkPhone).IsRequired();
            modelBuilder.Entity<CommonLead>().Property(x => x.Email).IsRequired();
            //life Lead

            modelBuilder.Entity<LifeLead>().Property(x => x.DOB).IsRequired();
            modelBuilder.Entity<LifeLead>().Property(x => x.Age).IsOptional();
            modelBuilder.Entity<LifeLead>().Property(x => x.CommonLeadId).IsRequired();
            modelBuilder.Entity<LifeLead>().Property(x => x.GeneratedDateTime).IsRequired();
            modelBuilder.Entity<LifeLead>().Property(x => x.ProductType).IsRequired();
            modelBuilder.Entity<LifeLead>().Property(x => x.SingleOwnership).IsRequired();


            //FuneralLeads
            modelBuilder.Entity<FuneralLead>().Property(x => x.DOB).IsRequired();
            modelBuilder.Entity<FuneralLead>().Property(x => x.Age).IsOptional();
            modelBuilder.Entity<FuneralLead>().Property(x => x.UpdatedDateTime).IsOptional();
            modelBuilder.Entity<FuneralLead>().Property(x => x.GeneratedDateTime).IsRequired();




            // ProductMaster
            modelBuilder.Entity<ProductMaster>().Property(x => x.GenerateDate).IsRequired();
            modelBuilder.Entity<ProductMaster>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<ProductMaster>().Property(x => x.State).IsOptional();

            //Buyer Master
            modelBuilder.Entity<BuyerMaster>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<BuyerMaster>().Property(x => x.State).IsOptional();
            modelBuilder.Entity<BuyerMaster>().Property(x => x.Email).IsOptional();
            modelBuilder.Entity<BuyerMaster>().Property(x => x.CreatedDateTime).IsRequired();
            modelBuilder.Entity<BuyerMaster>().Property(x => x.ApiUrl).IsOptional();
            modelBuilder.Entity<BuyerMaster>().Property(x => x.CompanyName).IsOptional();

            // transferlogs

            modelBuilder.Entity<TransferLog>().Property(x => x.BuyerMasterId).IsRequired();
            modelBuilder.Entity<TransferLog>().Property(x => x.BuyerReturnLeadId).IsOptional();
            modelBuilder.Entity<TransferLog>().Property(x => x.CommonLeadId).IsRequired();
            modelBuilder.Entity<TransferLog>().Property(x => x.Description).IsRequired();
            modelBuilder.Entity<TransferLog>().Property(x => x.GeneratedDatetime).IsRequired();
            modelBuilder.Entity<TransferLog>().Property(x => x.IsActualPrice).IsOptional();
            modelBuilder.Entity<TransferLog>().Property(x => x.Price).IsOptional();
            modelBuilder.Entity<TransferLog>().Property(x => x.Return).IsOptional();
            modelBuilder.Entity<TransferLog>().Property(x => x.Status).IsRequired();


            //Buyer Products
            modelBuilder.Entity<BuyerProduct>().Property(x => x.BuyerMasterId).IsRequired();
            modelBuilder.Entity<BuyerProduct>().Property(x => x.EndDate).IsRequired();
            modelBuilder.Entity<BuyerProduct>().Property(x => x.StartDate).IsRequired();
            modelBuilder.Entity<BuyerProduct>().Property(x => x.Piority).IsRequired();
            modelBuilder.Entity<BuyerProduct>().Property(x => x.IntegrationType).IsRequired();
            modelBuilder.Entity<BuyerProduct>().Property(x => x.ProductMasterId).IsRequired();
            modelBuilder.Entity<BuyerProduct>().Property(x => x.Quata).IsRequired();
            modelBuilder.Entity<BuyerProduct>().Property(x => x.State).IsOptional();



















        }

    }
}
