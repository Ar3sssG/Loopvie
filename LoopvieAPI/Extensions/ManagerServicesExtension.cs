﻿using LoopvieBusinessLogic.Interfaces;
using LoopvieBusinessLogic.Managers;

namespace LoopvieAPI.Extensions
{
    public static class ManagerServicesExtension
    {
        public static void AddManagerServices(this IServiceCollection services)
        {
            services.AddScoped<IWordManager, WordManager>();
            services.AddScoped<IIdentityManager, IdentityManager>();
        }
    }
}
