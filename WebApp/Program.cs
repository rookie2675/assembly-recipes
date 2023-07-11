using IoC;
using Services.Contracts;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();
            builder.Services.ConfigureWebAppServices();
            builder.Services.ConfigureWebAppLogging();
            builder.Services.ConfigureWebAppRepositories(builder.Configuration);

            builder.Logging.AddConsole();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            using (var scope = app.Services.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializerService>();
                initializer.Initialize();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapRazorPages();

            app.MapGet("/", context =>
            {
                context.Response.Redirect("/Recipes/Recipes");
                return Task.CompletedTask;
            });

            app.Run();
        }
    }
}