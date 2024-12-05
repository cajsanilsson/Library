﻿using Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Application.Interface.RepositoryInterfaces;
using Infrastructure.Repositories;


namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<FakeDatabase>();

            services.AddDbContext<LibraryDatabase>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IAuthorRepository, AuthorRepository>();

            return services;
        }
       
    }
}
