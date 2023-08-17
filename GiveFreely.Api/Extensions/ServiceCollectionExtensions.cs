using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GiveFreely.Contracts.Engine;
using GiveFreely.DataAccess;
using GiveFreely.DataAccess.Interfaces;
using GiveFreely.DataAccess.Repositories;
using GiveFreely.Api.Validator;
using GiveFreely.Models;
using GiveFreely.Models.Configuration;
using System.Diagnostics.CodeAnalysis;
using GiveFreely.Engine;

namespace GiveFreely.Api.Extensions
{

    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static void RegisterRepository(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAffiliateRepository, AffiliateRepository>();
            services.AddScoped<ICommisionRepository, CommisionRepository>();
        }

        public static void RegisterDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection(ConnectionStringSettings.KEY).Get<ConnectionStringSettings>();
            services.AddDbContext<GFContext>(options => options.UseSqlServer(settings.DefaultConnectionString), ServiceLifetime.Transient);
        }

        public static void RegisterValidation(this IServiceCollection services)
        {
            services.AddTransient<IValidator<Affiliate>, AffiliateValidation>();
            services.AddTransient<IValidator<Customer>, CustomerValidation>();
        }

        public static void RegisterEngines(this IServiceCollection services)
        {
            services.AddScoped<ICustomerEngine, CustomerEngine>();
            services.AddScoped<IAffiliateEngine, AffiliateEngine>();
            services.AddScoped<IReportEngine, ReportEngine>();
        }
    }
}
