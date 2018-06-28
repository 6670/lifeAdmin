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
   public class UserRoleDetailMap : EntityTypeConfiguration<UserRoleDetail>
    {
        public UserRoleDetailMap()
        {
            HasKey(t => t.ID);

            ////properties  
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.user_id).IsRequired();
            Property(t => t.role_id).IsRequired();

            ToTable("t_user_role");
        }
    }
}
