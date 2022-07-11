using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductionDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionDAL.Configs
{
    internal class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasIndex(p => p.Reference).IsUnique();
            builder.Property(p => p.Reference).HasMaxLength(8).IsFixedLength();
            builder.HasCheckConstraint("CK_NAME_MIN", "len(NAME) >= 4");
            builder.Property(p => p.Description).HasMaxLength(1000);
        }
        
    }
}
