using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PersonBook.Api.Extentions;
using PersonBook.Api.Infrastructure;
using PersonBook.DI;
using System.Linq;

namespace PersonBook.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            new DefaultDependencyResolver(Configuration).Resolve(services);

            services.AddSwaggerGen(x =>
            {
                x.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PersonBook API",
                    Version = "v1",
                    Description = "TBCPersonBook API Description",
                    Contact = new OpenApiContact
                    {
                        Name = "PersonBookApplication",
                        Email = "TBCTestApplication@mail.com",
                    },
                });
                x.CustomSchemaIds(s => s.FullName);
                x.DocumentFilter<LowercaseDocumentFilter>();
                x.OperationFilter<AddRequiredHeaderParameter>();
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { In = ParameterLocation.Header, Description = "Please enter JWT with Bearer into field", Name = "Authorization", Type = SecuritySchemeType.ApiKey });
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseGlobalLogging();
        }
    }
}
