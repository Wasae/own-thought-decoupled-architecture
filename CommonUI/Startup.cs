using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using AuthLayer.Repository;
using AuthLayer.Service;

namespace CommonUI
{
    public class Startup
    {

        String[] validDomains;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string _validOrigins = "_validOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("jwtkey").Value.ToString()))
                };
            });
            services.AddCors( options =>{
                options.AddPolicy(
                    name : _validOrigins,
                    builderpolicy =>{
                        builderpolicy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
            services.AddSingleton<IAuth,Auth>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CommonUI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CommonUI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_validOrigins);

            app.Use((context,next) => {
                validDomains = Configuration
                                .GetSection("validDomains")
                                .GetChildren()
                                .Select(x => x.Value)
                                .ToArray();
                if (validDomains.Count() != 0)
                {
                    int validDomainCount = validDomains.Where(x => x.ToLower().Equals(context.Request.Host.Value.ToLower())).Count();
                    if (validDomainCount == 0)
                    {
                        context.Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                        return System.Threading.Tasks.Task.FromResult<object>(null);
                    }   
                }                
                return next();
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
