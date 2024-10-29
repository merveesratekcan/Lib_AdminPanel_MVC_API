

namespace MVCUYGPROJE_Infrastructure.Configurations;

public class PublisherConfiguration : AudiTableEntityConfiguration<Publisher>
{
    public override void Configure(EntityTypeBuilder<Publisher> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(128).IsRequired();
        builder.Property(x => x.Address).HasMaxLength(500).IsRequired();
        base.Configure(builder);
    }
}

