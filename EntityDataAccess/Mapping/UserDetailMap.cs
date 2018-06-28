using EntityObjects;
using EntityObjects.Objects;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EntityDataAccess.Mapping
{
    public class UserDetailMap : EntityTypeConfiguration<UserMaster>
    {
        public UserDetailMap()
        {
            Property(t => t.UserName).IsRequired();
            Property(t => t.FirstName).IsRequired();
            Property(t => t.LastName).IsRequired();
            Property(t => t.Password).IsRequired();

        }
    }
}
