using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SampleBank.Domain.Models;
using SampleBank.Domain.Services;
using SampleBank.Infrastructure;

namespace SampleBank.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SampleBank", Version = "v1" });
            });
            services.AddSingleton(typeof(JsonRepository<>));
            services.AddSingleton<IAccountService, AccountService>();
            services.Configure<RepositoryOptions>(options =>
                options.BaseDataFolder = Path.Combine(WebHostEnvironment.ContentRootPath, "data")
            );
            services.AddSingleton<IAccountRepository>(provider => 
                new AccountRepository(repository: provider.GetService<JsonRepository<Account>>())
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CardIdentificationService V1");
                c.RoutePrefix = "docs";
            });
            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
