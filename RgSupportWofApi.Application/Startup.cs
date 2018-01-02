using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using RgSupportWofApi.Application.Data;
using RgSupportWofApi.Application.Data.Repositories;
using RgSupportWofApi.Application.Middleware;
using RgSupportWofApi.Application.Services;

namespace RgSupportWofApi.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            var connection = Configuration["Data:SqliteConnectionString"];
            var shiftsPerDayConfig = int.Parse(Configuration["Preferences:ShiftsPerDay"]);

            // Outputs generated SQL to Application Output/Console. Can be removed later
            var MyLoggerFactory = new LoggerFactory(new[] { new ConsoleLoggerProvider(
                (category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information, true) });
            
            // Using SQLite for simplicity's sake
            services.AddDbContext<DatabaseContext>(options => options.UseSqlite(connection).UseLoggerFactory(MyLoggerFactory));

            services.AddTransient<IEngineerService, EngineerService>();
            services.AddTransient<IShiftService, ShiftService>();
            services.AddTransient<IEngineerRepository, EngineerRepository>();
            services.AddTransient<IShiftRepository, ShiftRepository>();
            services.AddTransient<IWheelOfFateService>(x=> new WheelOfFateService(
                x.GetRequiredService<IShiftService>(), x.GetRequiredService<IEngineerService>(), shiftsPerDayConfig));
            services.AddTransient<IDbValidationService, DbValidationService>(x => new DbValidationService(
                x.GetRequiredService<IEngineerRepository>(), shiftsPerDayConfig));
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // I got this excelent middleware from some answer in stackoverflow... Can't remember the link to give credits though :(
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = new JsonExceptionMiddleware().Invoke
            });

            // Allows the React client to call this application from a different domain/port 
            // (when running in localhost, a different port is enough to make it a different domain)
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); // open to all origins... safety? what for? :)

            app.UseMvcWithDefaultRoute();
        }
    }
}
