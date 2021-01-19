using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace SkribbleBE
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
            services.AddControllers();

          

            services.AddDbContext<ProjekatContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("Konekcija"))
                .EnableSensitiveDataLogging();
          
                //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                         {
                             options.SaveToken = true;
                             options.RequireHttpsMetadata = false;
                             options.TokenValidationParameters = new TokenValidationParameters()
                             {
                                 //provera issuer-a u tokenu
                                 ValidateIssuer = true,
                                 //provera audience u tokenu
                                 ValidateAudience = true,
                                 //provera LifeTime, da nije isteklo vreme tokenu
                                 ValidateLifetime=true,
                                 //provera key-a
                                 ValidateIssuerSigningKey=true,
                                 ValidAudience = Configuration["Jwt:Issuer"],
                                 ValidIssuer = Configuration["Jwt:Audience"],
                                 //niz bajtova koji se ocekuje da se primi preko token-a, taj niz bajtova u sustini predstavlja secretkey
                                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                                 ClockSkew=TimeSpan.Zero
                             };
                         });


            services.AddScoped<RecService>();
            services.AddScoped<KategorijaService>();
            services.AddScoped<RecPoKategorijiService>();
            services.AddScoped<PotezService>();
            services.AddScoped<TokIgreService>();
            services.AddScoped<TokIgrePoKorisnikuService>();
            services.AddScoped<KorisnikService>();
            services.AddScoped<SobaService>();
            services.AddScoped<KorisniciPoSobiService>();
            // services.AddScoped<DataLayer.Repository.KorisnikRepository>();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            }).AddXmlSerializerFormatters();

            services.AddCors(options => {
                options.AddPolicy("Corse", builder => {
                    builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Corse");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
      
    }
}
