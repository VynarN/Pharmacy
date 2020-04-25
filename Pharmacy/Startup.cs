using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Pharmacy.Application.Common.Mappings;
using Pharmacy.Domain.Entites;
using Pharmacy.Infrastructure;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Infrastructure.Services;
using Pharmacy.Infrastructure.Common.Interfaces;
using Pharmacy.Application.Middlewares.MiddlewareExtensions;
using Microsoft.AspNetCore.Antiforgery;
using Pharmacy.Application.Middlewares;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Services;
using Pharmacy.Infrastructure.Persistence.Repositories;

namespace Pharmacy
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddAntiforgery(options => { options.HeaderName = "x-xsrf-token"; });
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Pharmacy API",
                    Description = "Test API with ASP .NET CORE 3.0"
                });

            });
            services.AddControllers();
            services.AddDbContext<PharmacyContext>(cfg =>
            {
                cfg.UseSqlServer(
                    Configuration
                    .GetConnectionString("SqlServerConnectionString"));
            });
            services.AddIdentity<User, IdentityRole>(
                  options =>
                  {
                      options.SignIn.RequireConfirmedAccount = true;
                      options.User.RequireUniqueEmail = true;
                      options.Password.RequireNonAlphanumeric = false;
                  })
                    .AddEntityFrameworkStores<PharmacyContext>()
                    .AddDefaultTokenProviders();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["JwtConfiguration:Issuer"],
                    ValidAudience = Configuration["JwtConfiguration:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtConfiguration:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PharmacyAppMapper());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddScoped<IEmailSender, SendGridService>();
            services.AddScoped<IAccountService, AccountService>(); 
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
            });

            app.UseMiddleware<TokenMiddleware>();
            app.UseAuthentication();
            app.UseXsrfProtection(antiforgery);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
