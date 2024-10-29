

namespace MVCUYGPROJE_Infrastructure.Configurations;

public class AuthorConfiguration : AudiTableEntityConfiguration<Author>
{
    public override void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(128).IsRequired();
        builder.Property(b => b.BirthDate).IsRequired();
        base.Configure(builder);
    }
}


