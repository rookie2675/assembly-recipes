using IoC;

using Microsoft.Data.SqlClient;
using Services.Contracts;

namespace WebApp
{
    public class Program
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Program> _logger;

        public Program(IConfiguration configuration, ILogger<Program> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();
            builder.Services.ConfigureWebAppServices();
            builder.Services.ConfigureWebAppRepositories(builder.Configuration);

            builder.Logging.AddConsole(); // Add Console logger

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession(); // Place app.UseSession() here before other middleware

            app.MapRazorPages();

            // Configure the default page to be "Recipes"
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/Recipes");
                    return Task.CompletedTask;
                });
            });


            DatabaseInitializer databaseInitializer = new DatabaseInitializer(app.Logger, app.Services.GetRequiredService<IUserService>(), app.Configuration);
            databaseInitializer.Initialize();

            app.Run();
        }
}