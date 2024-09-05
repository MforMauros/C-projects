using Chapter6C_Exercise.DAO;
using FluentValidation;
using Serilog;

namespace Chapter6C_Exercise;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);
                /*config
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .Enrich.FromLogContext()
                    //.WriteTo.Console()
                    .WriteTo.File(
                        "Logs/logs.txt", // Specify the file path
                        rollingInterval: RollingInterval.Day, // Create a new log file daily
                        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {SourceContext} [{Level}] " +
                                        "{Message}{NewLine}{Exception}",
                        retainedFileCountLimit: null, // Set to null to keep all log files
                        fileSizeLimitBytes: null // Set to null to disable file size limit
                    );*/
            });

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddScoped<IBoatDAO, BoatDAOImpl>();
        builder.Services.AddScoped<IBoatService, BoatServiceImpl>();
        builder.Services.AddAutoMapper(typeof(MapperConfig));
        builder.Services.AddScoped<IValidator<BoatInsertDTO>, BoatInsertValidator>();
        builder.Services.AddScoped<IValidator<BoatUpdateDTO>, BoatUpdateValidator>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}
