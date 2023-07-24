using FluentValidation;
using FluentValidation.AspNetCore;
using LayerTestApp.Payroll.API.Attributes;
using LayerTestApp.Payroll.API.Middleware;
using LayerTestApp.Payroll.BAL;
using LayerTestApp.Payroll.BAL.Models.Validators;
using LayerTestApp.Payroll.DAL;
using LayerTestApp.Payroll.DAL.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

ConfigureConfiguration(builder);

// Add services to the container.
ConfigureServices(builder);

ConfigureLogging(builder);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    Migration.CheckOrCreateDatabase(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// for Swagger in Docker http -- start --
app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()); // allow credentials
// // for Swagger in Docker http -- end --

app.MapControllers();

app.Run();

static void ConfigureConfiguration(WebApplicationBuilder builder)
{
    if (builder.Environment.IsDevelopment())
    {
        builder.Configuration.AddJsonFile("appsettings.dev.json");
    }
    else
    {
        builder.Configuration.AddJsonFile("appsettings.prod.json");
    }
}

static void ConfigureServices(WebApplicationBuilder builder)
{
    IServiceCollection services = builder.Services;
    ConfigurationManager configuration = builder.Configuration;
    IWebHostEnvironment environment = builder.Environment;

    services.AddCors(); // for Swagger in Docker http

    services.AddControllers(config => config.Filters.Add(typeof(ValidationAttribute)));

    services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
    services.AddValidatorsFromAssemblyContaining<IValidatorsMarker>();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddPayrollDALDI(configuration);
    services.AddPayrollBALDI();

};

static void ConfigureLogging(WebApplicationBuilder builder)
{
    builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

    //IServiceCollection services = builder.Services;
    //ConfigurationManager configuration = builder.Configuration;

    //string logFileFolder = configuration["SerilogLogging:LogFileFolder"];
    //string logFileName = configuration["SerilogLogging:LogFileName"];
    //string debugLogFileName = configuration["SerilogLogging:DebugLogFileName"];

    //string logFile = Path.Combine(logFileFolder, logFileName);
    //string debugLogFile = Path.Combine(logFileFolder, debugLogFileName);

    //var serilog = new LoggerConfiguration()
    //              .MinimumLevel.Debug()
    //              .Enrich.FromLogContext()
    //              .WriteTo.Conditional(ev => ev.Level != Serilog.Events.LogEventLevel.Verbose,
    //                                   l => l.Logger(
    //                                       a => a.Filter.ByIncludingOnly(Matching.FromSource("Microsoft.EntityFrameworkCore"))
    //                                             .WriteTo.File(debugLogFile, shared: true)))
    //              .WriteTo.Conditional(ev => ev.Level != Serilog.Events.LogEventLevel.Verbose,
    //                                   l => l.Logger(
    //                                       a => a.Filter.ByExcluding(Matching.FromSource("Microsoft.EntityFrameworkCore"))
    //                                             .WriteTo.File(logFile, shared: true)))
    //              .WriteTo.Async(a => a.Console())
    //              .CreateLogger();

    //builder.Services.AddLogging(x => x.AddSerilog(serilog));
    //builder.Services.AddSingleton(typeof(Microsoft.Extensions.Logging.ILogger), typeof(Logger<ExceptionHandling>));

}