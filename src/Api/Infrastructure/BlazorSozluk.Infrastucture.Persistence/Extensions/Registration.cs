using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Infrastructure.Persistence.Repositories;
using BlazorSozluk.Infrastucture.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastucture.Persistence.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<BlazorSozlukContext>(conf =>
            {
                var connStr = configuration["BlazorSozlukConnectionString"].ToString();
                conf.UseSqlServer(connStr, opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            });
            //var seedData = new SeedData();
            //seedData.SeedAsync(configuration).GetAwaiter().GetResult();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
