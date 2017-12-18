using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using RgSupportWofApi.Application.Data;
using RgSupportWofApi.Application.Data.Repositories;
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

            var MyLoggerFactory = new LoggerFactory(new[] { new ConsoleLoggerProvider(
                (category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information, true) });

            services.AddDbContext<DatabaseContext>(options => options
                                                   .UseSqlite(connection)
                                                   .UseLoggerFactory(MyLoggerFactory));

            services.AddTransient<IEngineerRepository, EngineerRepository>();
            services.AddTransient<IShiftRepository, ShiftRepository>();
            services.AddTransient<IWheelOfFateService, WheelOfFateService>();

            // The JsonOptions below are due to: https://stackoverflow.com/questions/39024354/asp-net-core-api-only-returning-first-result-of-list
            // In other words: It's for serializing circular references
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader()); // safety? what for? :)

            app.UseMvcWithDefaultRoute();
        }
    }
}
