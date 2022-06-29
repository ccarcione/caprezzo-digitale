using CaprezzoDigitale.WebApi.Helpers;

namespace CaprezzoDigitale.WebApi.ExtensionMethods
{
    public static class IApplicationBuilderExtension
    {
        public static IApplicationBuilder UseStaticFilesCaprezzoDigitale(this IApplicationBuilder app, IServiceProvider services, IWebHostEnvironment env)
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                Models.Options options = scope.ServiceProvider.GetRequiredService<Models.Options>();

                string publicStaticFilesFolderPath = Path.Combine(env.ContentRootPath, UtilityHelper.GetWindowsLinuxPath(options.WebApiOptions["publicStaticFiles_FolderPath"]));
                if (!Directory.Exists(publicStaticFilesFolderPath))
                {
                    Directory.CreateDirectory(publicStaticFilesFolderPath);
                }
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(publicStaticFilesFolderPath),
                    RequestPath = string.Concat("/", options.WebApiOptions["publicStaticFiles_RequestPath"]),
                });

                string galleriaFolderPath = Path.Combine(env.ContentRootPath, UtilityHelper.GetWindowsLinuxPath(options.WebApiOptions["galleria_FolderPath"]));
                if (!Directory.Exists(galleriaFolderPath))
                {
                    Directory.CreateDirectory(galleriaFolderPath);
                }
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(galleriaFolderPath),
                    RequestPath = string.Concat("/", options.WebApiOptions["galleria_RequestPath"]),
                });
            }

            return app;
        }
    }
}
