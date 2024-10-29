using Microsoft.Extensions.DependencyInjection;
using MVCUYGPROJE.Aplication.Services.AccountServices;
using MVCUYGPROJE.Aplication.Services.AdminServices;
using MVCUYGPROJE.Aplication.Services.AuthorServices;
using MVCUYGPROJE.Aplication.Services.BookServices;
using MVCUYGPROJE.Aplication.Services.CategoryServices;
using MVCUYGPROJE.Aplication.Services.ProfileUserServices;
using MVCUYGPROJE.Aplication.Services.PublisherServices;
using MVCUYGPROJE_Infrastructure.Repositories.BookRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE.Aplication.Extentions;

public static class DependecyInjection
{
    public static IServiceCollection AddAplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IPublisherService, PublisherService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IProfileUserService, ProfileUserService>();
        services.AddScoped<IAdminService, AdminService>();


        return services;
    }
}
