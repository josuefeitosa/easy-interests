using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyInterests.API.Application.Models;
using EasyInterests.API.Application.Services;
using EasyInterests.API.Infrastructure;
using EasyInterests.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace EasyInterests.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StaticConfiguration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static IConfiguration StaticConfiguration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region JWT Config
              services.AddAuthentication(x =>
              {
                  x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                  x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
              })
              .AddJwtBearer(x =>
              {
                  x.RequireHttpsMetadata = false;
                  x.SaveToken = true;
                  x.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["SecretKey"])),
                      ValidateIssuer = false,
                      ValidateAudience = false
                  };
              });
            #endregion

            #region Swagger
              services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo {
                  Version = "v1",
                  Title = "Easy Interests API",
                  Description = "API para cálculo de correção de dívidas com negociador",
                  Contact = new OpenApiContact{
                    Name = "Josué Feitosa",
                    Email = "josue.feitosa@outlook.com.br",
                    Url = new Uri("https://github.com/josuefeitosa")
                  }
                });

                //Security schema configurating to JWT Token
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });
              });
            #endregion

            #region Connection to DB for DBContext
              services.AddDbContext<EasyInterestsDBContext>(options =>
                  options.UseSqlite(Configuration["ConnectionStringSQLite"])
              );
            #endregion

            #region Dependency Injection
              #region Settings DI
                services.Configure<AppSettingsModel>(Configuration.GetSection("ApplicationSettings"));

                services.AddOptions();
              #endregion

              #region Authenticated User
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                services.AddScoped<AuthenticatedUser>();
              #endregion

              #region Repositories and Services
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IUserService, UserService>();

                services.AddScoped<IDebtRepository, DebtRepository>();
                services.AddScoped<IDebtService, DebtService>();
              #endregion
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c => {
              c.SwaggerEndpoint("/swagger/v1/swagger.json", "Easy Interests API");
            });

            app.UseRouting();

            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
