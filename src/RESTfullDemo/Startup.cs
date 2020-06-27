using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using RESTfullDemo.Entities;
using RESTfullDemo.Filters;
using RESTfullDemo.Helpers;
using RESTfullDemo.Services;
using System.IO;

namespace RESTfullDemo
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "RESTfull Demo API",
                    Version = "v1"
                });

                var xmlFile = Path.ChangeExtension(typeof(Startup).Assembly.Location, ".xml");
                c.IncludeXmlComments(xmlFile);
            });

            services.AddControllers().AddNewtonsoftJson();
            services.AddMvc(config =>
            {
                config.Filters.Add<JsonExceptionFilter>();

                config.ReturnHttpNotAcceptable = true;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            .AddXmlSerializerFormatters();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme);
            services.AddAutoMapper(typeof(DemoMappingProfile));
            services.AddScoped<CheckAuthorExistFilterAttribute>();

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

            services.AddDbContext<DemoDbContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty; 
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RESTfull Demo API V1");
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
