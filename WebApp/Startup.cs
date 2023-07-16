using IoC;

namespace WebApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)  
        {
            services.AddLogging();
            services.AddRazorPages();
            services.ConfigureServices();
            services.ConfigureRepositories();
            services.ConfigureAuthentication();
            services.ConfigureDatabase(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {   
            ConfigureEnvironment(app, env);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            app.Run(context =>
            {
                context.Response.Redirect("/Recipes");
                return Task.CompletedTask;
            });
        }

        private static void ConfigureEnvironment(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
        }
    }
}
