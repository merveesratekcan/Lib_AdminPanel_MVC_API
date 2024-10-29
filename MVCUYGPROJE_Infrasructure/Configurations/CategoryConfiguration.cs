

namespace MVCUYGPROJE_Infrastructure.Configurations;

public class CategoryConfiguration:AudiTableEntityConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(128).IsRequired();
        base.Configure(builder);
    }
}
