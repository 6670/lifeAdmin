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
    public class PostcodeDetailMap : EntityTypeConfiguration<PostcodeDetail>
    {
        public PostcodeDetailMap()
        {
            HasKey(t => t.ID);

            ////properties  
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Postcode).IsRequired();
            Property(t => t.mod_postcode).IsRequired();

            ToTable("t_postcode");
        }
    }
}
