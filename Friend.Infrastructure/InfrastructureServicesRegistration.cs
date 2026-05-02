using Friend.Application.Contracts.Persistence;
using Friend.Application.Contracts.Security;
using Friend.Infrastructure.Persistence;
using Friend.Infrastructure.Persistence.Repositories;
using Friend.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friend.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // ✅ Database setup
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // ✅ Register repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            // ✅ Register security services
            services.AddScoped<ITokenService, TokenService>();

            // Register UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
