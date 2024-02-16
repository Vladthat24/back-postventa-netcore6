using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Aplication.Comnons.Orderning;
using POS.Aplication.Extensions.WatchDog;
using POS.Aplication.Interfaces;
using POS.Aplication.Services;
using POS.Infraestructure.Persistences.Interfaces;
using POS.Infraestructure.Persistences.Repositories;
using System.Reflection;

namespace POS.Aplication.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionAplicacion(this IServiceCollection services, IConfiguration configuration) {
            services.AddSingleton(configuration);

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<IOrderingQuery, OrderingQuery>();

            services.AddScoped<ICategoryApplication, CategoryApplication>();
            services.AddScoped<IUserApplication, UserApplication>();
            services.AddScoped<IProviderApplication, ProviderApplication>();
            services.AddScoped<IAuthApplication, AutApplication>();
            services.AddScoped<IDocumentTypeApplication, DocumentTypeApplication>();
            services.AddScoped<IGenerateExcelApplication, GenerateExcelApplication>();
            services.AddScoped<IWarehouseApplication, WarehouseApplication>();
            services.AddScoped<IProductApplication,ProductApplication>();
            services.AddScoped<IFileStorageLocalApplication, FileStorageLocalApplication>();
            services.AddScoped<IProductStockApplication, ProductStockApplication>();
            services.AddScoped<IPurcharseApplication, PurcharseApplication>();

            services.AddWatchDog(configuration);
            return services;
        }
    }
}
