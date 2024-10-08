﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LoopvieDataLayer.DAL.DBContext;
using LoopvieDataLayer.Identity;

namespace LoopvieAPI.Extensions
{
    public static class NetServicesExtension
    {
        public static void ConfigureAspNetServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                    builder.SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddDbContext<LoopvieDBContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .EnableSensitiveDataLogging());

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })
              .AddEntityFrameworkStores<LoopvieDBContext>()
              .AddDefaultTokenProviders();
        }
    }
}
