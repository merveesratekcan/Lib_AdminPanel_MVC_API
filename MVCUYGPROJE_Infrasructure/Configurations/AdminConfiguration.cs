﻿

namespace MVCUYGPROJE_Infrastructure.Configurations;

public class AdminConfiguration:BaseUserConfiguration<Admin>
{
    public override void Configure(EntityTypeBuilder<Admin> builder)
    {
        base.Configure(builder);
    }
}
