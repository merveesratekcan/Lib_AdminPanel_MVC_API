
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MVCUYGPROJE_Infrastructure.AppContext;
using MVCUYGPROJE_Infrastructure.Repositories.AdminRepository;
using MVCUYGPROJE_Infrastructure.Repositories.AuthorRepository;
using MVCUYGPROJE_Infrastructure.Repositories.BookRepositories;
using MVCUYGPROJE_Infrastructure.Repositories.CategoryRepositories;
using MVCUYGPROJE_Infrastructure.Repositories.ProfileUserRepository;
using MVCUYGPROJE_Infrastructure.Repositories.PublicherRepository;
using MVCUYGPROJE_Infrastructure.Repositories.PublisherRepository;
using MVCUYGPROJE_Infrastructure.Seeds;


namespace MVCUYGPROJE_Infrastructure.Extentions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServicces(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseLazyLoadingProxies();
            options.UseSqlServer(configuration.GetConnectionString(AppDbContext.DevConnectionString));
            //appsettings.json dosyasındaki connection stringi alıyoruz.
        });

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IPublisherRepository, PublisherRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IProfileUserRepository, ProfileUserRepository>();
        services.AddScoped<IAdminRepository, AdminRepository>();


        AdminSeed.SeedAsync(configuration).GetAwaiter().GetResult();
        //AdminSeed sınıfındaki SeedAsync metodunu çalıştırıyoruz.
        return services;

     
    }
}
