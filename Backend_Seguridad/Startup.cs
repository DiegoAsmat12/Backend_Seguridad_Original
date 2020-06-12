using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Backend_Seguridad.Contexts.Software;
using Backend_Seguridad.Entities.Software;
using Backend_Seguridad.Models.Software;
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
using Newtonsoft.Json;

namespace Backend_Seguridad
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
            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<Camara, CamaraDTO>();
                configuration.CreateMap<CamaraCreacionDTO, Camara>().ReverseMap();
                configuration.CreateMap<Placa, PlacaDTO>();
                configuration.CreateMap<PlacaCreacionDTO, Placa>().ReverseMap();
                configuration.CreateMap<ListaNegra, ListaNegraDTO>();
                configuration.CreateMap<ListaNegraCreacionDTO, ListaNegra>().ReverseMap();

            }, typeof(Startup));
            services.AddDbContext<ApplicationSoftwareDbContext>(options =>
            options.UseMySql(Configuration.GetConnectionString("SoftwareConnection")));
            services.AddDbContext<ApplicationSoftwareIdentityContext>(options => options.UseMySql(Configuration.GetConnectionString("SoftwareLogConnection")));
            services.AddIdentity<ApplicationSoftwareUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationSoftwareIdentityContext>()
                .AddDefaultTokenProviders();
            /*services.AddDbContext<ApplicationAndroidDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("AndroidConnection")));
            services.AddDbContext<ApplicationAndroidIdentityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AndroidLogConnection")));
            services.AddIdentity<ApplicationAndroidUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationAndroidIdentityContext>()
                .AddDefaultTokenProviders();*/
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                        ClockSkew = TimeSpan.Zero
                    });
            ;

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
