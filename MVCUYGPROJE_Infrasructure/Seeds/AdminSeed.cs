using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MVCUYGPROJE_Domain.Enums;
using MVCUYGPROJE_Infrastructure.AppContext;


namespace MVCUYGPROJE_Infrastructure.Seeds;

public static class AdminSeed
{
    //const değişkenlerin değerleri değiştirilemez.
    private const string adminEmail = "admin@bilgeadam.com";
    private const string adminPassword = "Password.1";
    public static async Task SeedAsync(IConfiguration confguration)
    {
        var dbContextBuilder= new DbContextOptionsBuilder<AppDbContext>();
        dbContextBuilder.UseSqlServer(confguration.GetConnectionString("AppConnectionDev"));
        AppDbContext context = new AppDbContext(dbContextBuilder.Options);
        if (!context.Roles.Any(role=>role.Name=="Admin"))
        {
            await AddRoles(context);
                
        }
        if (!context.Users.Any(user=>user.Email==adminEmail))
        {
            await AddAdmin(context);
        }
    }

    private static async Task AddAdmin(AppDbContext context)
    {
       IdentityUser user = new IdentityUser()
       {
           Email = adminEmail,
           NormalizedEmail = adminEmail.ToUpperInvariant(),
           UserName = adminEmail,
           NormalizedUserName = adminEmail.ToUpperInvariant(),
           EmailConfirmed = true
       };
        user.PasswordHash=new PasswordHasher<IdentityUser>().HashPassword(user, adminPassword);
        await context.Users.AddAsync(user);
       
        var adminRoleId = context.Roles.FirstOrDefault(role => role.Name == Roles.Admin.ToString()).Id;
        await context.UserRoles.AddAsync(new IdentityUserRole<string>()
        {
            RoleId = adminRoleId,
            UserId = user.Id
        });

        await context.Admins.AddAsync(new Admin()
        {
            FirstName = "Admin",
            LastName = "Admin",
            Email = adminEmail,
            IdentityId = user.Id
        });
        await context.SaveChangesAsync();

    }

    private static async Task AddRoles(AppDbContext context)
    {
        string[] roles = Enum.GetNames(typeof(Roles));
        //Enumda bunulan verilerin isimlerini dizi olarak döner.
        for (int i = 0; i < roles.Length; i++)
        {
            if (await context.Roles.AnyAsync(role => role.Name == roles[i]))
            {
                continue;
            }
            IdentityRole role = new IdentityRole()
            {
                Name = roles[i],
                NormalizedName = roles[i].ToUpperInvariant()
                //ToUpperInvariant() metodu küçük harfleri büyük harfe çevirir. Invariand i yi ı olarak büyütür.O sebeple ToUpper yerine ToUpperInvariant kullanılır.
            };
            await context.Roles.AddAsync(role);
            await context.SaveChangesAsync();
        }
    }
}
