using Application.Common.Interfaces.Authentication;
using Application.Data;
using Domain.Customers;
using Domain.CustomerStatuses;
using Domain.Login;
using Domain.Primitives;
using Infrastructure.Authentication;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        {

            AddPersistence(services, configuration);

            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Database")));

            services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerViewRepository, CustomerViewRepository>();
            services.AddScoped<ICustomerStatusRepository, CustomerStatusRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();

            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddHostedService<WriteFile>();

            return services;
        }
    }
}
