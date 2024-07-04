using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using API.Domain.Configuration;
using API.Domain.Contracts;
using API.Domain.Entities;
using API.Domain.Interfaces;
using API.Domain.Services;
using API.Infrastructure.Common;
using API.Infrastructure.Config;
using API.Infrastructure.DTO;
using API.Infrastructure.Projection;
using API.Infrastructure.Repositories;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using API.Domain.DTOs;

namespace API.Core
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

            services.AddMvcCore().AddApiExplorer();

            services.Configure<DataBaseSettings>(Configuration.GetSection(typeof(DataBaseSettings).Name));

            IMapper mapper = new MapperConfiguration(map =>
            {
                map.AddProfile(new AutomapperProfile());
            }).CreateMapper();

            services.AddSingleton(mapper);

            services.Configure<ApiSettings>(Configuration);

            services.AddTransient<IDbManager<LogUserDTO>>(s => new DbManager<LogUserDTO>(Configuration.GetValue<string>("environmentVariables:SqlSettings:BDUserConnectionString")));
            services.AddTransient<IDbManager<UserDTO>>(s => new DbManager<UserDTO>(Configuration.GetValue<string>("environmentVariables:SqlSettings:BDUserConnectionString")));
            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();


            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Safe api for Safe Vehicles", Version = "v1" });
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "API.Core.xml");
                options.IncludeXmlComments(xmlPath);
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                           {   new OpenApiSecurityScheme {
                                   Reference = new OpenApiReference {
                                       Type = ReferenceType.SecurityScheme,
                                       Id = "Bearer"
                                   },
                                   Scheme = "token",
                                   Name = "Bearer",
                                   In = ParameterLocation.Header
                               },
                               new List<string>()
                           }
                       });

                options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                           {   new OpenApiSecurityScheme {
                                   Reference = new OpenApiReference {
                                       Type = ReferenceType.SecurityScheme,
                                       Id = "bearer"
                                   },
                                   Scheme = "token",
                                   Name = "bearer",
                                   In = ParameterLocation.Header
                               },
                               new List<string>()
                           }
                       });
            });

            //Enable CORS
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod()
                  .AllowAnyHeader());
            });

            services.AddControllers();

            services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later");
                    });
                });
            }

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Safe api V1");
                c.RoutePrefix = "swagger";
            });


        }
    }
}
