using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductionDAL.Entities;

namespace ProductionDAL.Configs
{
    internal class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasIndex(p => p.Name).IsUnique();
            builder.HasCheckConstraint("CK_NAME_MIN", "len(NAME) > 2");
            builder.Property(p => p.Name).HasMaxLength(20);
        }
    }
}
