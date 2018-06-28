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
    public class TransferDetailMap : EntityTypeConfiguration<TransferDetail>
    { 
        public TransferDetailMap()
        {
            HasKey(t => t.ID);

            ////properties  
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.generated_datetime).IsRequired();
            Property(t => t.transfer_datetime).IsRequired();
            Property(t => t.buyer_id).IsRequired();
            Property(t => t.lead_id).IsRequired();
            Property(t => t.buyer_return_lead_id).IsRequired();
            Property(t => t.buyer_end_status).IsRequired();
            Property(t => t.status_id).IsRequired();
            Property(t => t.msg_returned).IsRequired();

            ToTable("t_transfer_log");
        }
    }
}
