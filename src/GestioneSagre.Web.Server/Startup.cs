using System.Reflection;
using System.Text.Json.Serialization;
using GestioneSagre.Addons.Extensions;
using GestioneSagre.Business.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

namespace GestioneSagre.Web.Server;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {

        services.AddControllersWithViews()
            .AddJsonOptions(options =>
            {
                // Info su: https://github.com/marcominerva/MyWebApiToolbox
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
            });

        services.AddRazorPages();
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });
        });

        services.AddDbContextPool<GestioneSagreDbContext>(optionBuilder =>
        {
            var maxRetryCount = Configuration.GetSection("Database").GetValue<int>("maxRetryCount");
            var maxRetryDelay = TimeSpan.FromSeconds(Configuration.GetSection("Database").GetValue<double>("maxRetryDelay"));
            var connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");

            optionBuilder.UseSqlServer(connectionString, options =>
            {
                // To perform a new migration you need:

                // 1. Open the Package Manager Console panel

                // 2. In the Default Project drop-down menu make sure that the selected project is GestioneSagre.Web.Server.

                // 3. Finally run the command Add-Migration NAME-MIGRATION -Project GestioneSagre.Web.Migrations
                // where NAME-MIGRATION represents the name of the migration to create (example: InitialMigration)

                // Info su: https://docs.microsoft.com/it-it/ef/core/managing-schemas/migrations/projects?tabs=dotnet-core-cli
                options.MigrationsAssembly("GestioneSagre.Web.Migrations");
                options.EnableRetryOnFailure(maxRetryCount, maxRetryDelay, null);
            });
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        // Services - Custom Extension Method
        services.AddApplicationServices(Configuration);
        services.AddSwaggerServices(Configuration, xmlPath);

        // Options
        services.Configure<KestrelServerOptions>(Configuration.GetSection("Kestrel"));
    }

    public void Configure(WebApplication app)
    {
        IWebHostEnvironment env = app.Environment;

        if (env.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }

        var enableSwagger = Configuration.GetSection("Swagger").GetValue<bool>("enabled");

        if (enableSwagger)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestione Sagre v1"));
        }

        app.UseApplicationServices(); // Custom Extension Method
        app.UseBlazorFrameworkFiles();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapFallbackToFile("index.html");
        });
    }
}