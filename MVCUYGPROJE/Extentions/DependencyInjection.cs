using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using MVCUYGPROJE_Infrastructure.AppContext;
using System.Globalization;

namespace MVCUYGPROJE.Extentions;

public static class DependencyInjection
{
    public static IServiceCollection AddUIService(this IServiceCollection services)
    {
        AddIdentitiyService(services);
        services.AddNotyf(confic =>
        {
            confic.HasRippleEffect = true;
            confic.DurationInSeconds = 5;
            confic.Position = NotyfPosition.BottomRight;
            confic.IsDismissable = true;
        });
        services.AddLocalization(options => options.ResourcesPath = "Resources");
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("tr"),
                new CultureInfo("en"),
            };
            options.DefaultRequestCulture = new RequestCulture("tr");
            options.SupportedUICultures = supportedCultures;
            options.SupportedCultures = supportedCultures;
        });
        return services;

    }
    public static IServiceCollection AddIdentitiyService(this IServiceCollection services)
    {
                services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
                //.AddDefaultTokenProviders();
                return services;
    }
        
}
