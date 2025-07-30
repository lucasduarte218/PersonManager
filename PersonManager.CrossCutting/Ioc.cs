using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using PersonManager.Application.UseCases;
using PersonManager.Domain.Repositories;
using PersonManager.Infrastructure.Data;
using PersonManager.Infrastructure.Repositories;
using PersonManager.Application.Interfaces.UseCasesInterfaces;

namespace PersonManager.CrossCutting;

public static class Ioc
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICreatePersonUseCase, CreatePersonUseCase>();
        services.AddScoped<IUpdatePersonUseCase, UpdatePersonUseCase>();
        services.AddScoped<IDeletePersonUseCase, DeletePersonUseCase>();
        services.AddScoped<IGetPersonByIdUseCase, GetPersonByIdUseCase>();
        services.AddScoped<IGetAllPersonsUseCase, GetAllPersonsUseCase>();
        services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Repositórios
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRequestResponseLogRepository, RequestResponseLogRepository>();
        services.AddScoped<IExceptionLogRepository, ExceptionLogRepository>();

        // DbContext com SQL Server
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}
