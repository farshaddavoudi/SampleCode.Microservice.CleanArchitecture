using System.Globalization;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.Common.Implementations;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;
using Microsoft.AspNetCore.Localization;

namespace SampleMicroserviceApp.Identity.Application.ConfigureServices;

public class LocalizationDiInstaller : IDiInstaller
{
    public void InstallServices(IServiceCollection services, AppSettings appSettings)
    {
        services.AddLocalization();

        services.AddRequestLocalization(options =>
        {
            var supportedCultures = new[]
            {
                new CultureInfo("fa"),
                new CultureInfo("en")
            };

            options.SupportedCultures = supportedCultures;
            options.DefaultRequestCulture = new RequestCulture("fa");
            options.ApplyCurrentCultureToResponseHeaders = true;
            options.SupportedUICultures = supportedCultures;
            options.RequestCultureProviders = new List<IRequestCultureProvider>
            {
                new QueryStringRequestCultureProvider(),
                new AcceptLanguageHeaderRequestCultureProvider()
                // new CookieRequestCultureProvider()
            };
        });

        services.AddScoped<ILocalStringProvider, LocalStringProvider>();

        //services.AddSingleton<IConfigureOptions<MvcOptions>, MvcConfigurationToProvideModelBindingMessage>();
    }
}