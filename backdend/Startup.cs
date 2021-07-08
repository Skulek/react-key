using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.DbContext;
using backend.Extensions;
using backend.Repositories;
using backend.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace backend
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

            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader().AllowAnyMethod();
            }));

            services.AddDbContext<KeyValuePairDbContext>(options => options.UseInMemoryDatabase("KVPDb"), ServiceLifetime.Singleton);

            services.AddAutoMapper(cfg => { cfg.AddMaps(typeof(Startup).Assembly); });
            services.AddTransient<IKeyValuePairService, KeyValuiePairService> ();
            services.AddTransient<IKeyValuePairRepository, KeyValuePairRepository>();
            services.AddControllers();
            services.AddHostedService<MigratorHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
