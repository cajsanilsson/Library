﻿
using Microsoft.Extensions.DependencyInjection;
using Application.UserQueries.LogIn.Helpers;
using Application.Interfaces.RepositoryInterfaces;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
            services.AddScoped<TokenHelper>();

            

            return services;
        }
    }
}
