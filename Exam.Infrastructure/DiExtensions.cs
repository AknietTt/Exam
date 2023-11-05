using Exam.Domain.IRepository;
using Exam.Infrastructure.Repository;
using Exam.Infrastructure.Service;
using Exam.Parser;
using Exam.Parser.Mapping;
using Exam.Parser.Strategies;
using AutoMapper;
using AutoMapper.Execution;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Exam.Domain.SeedWork;
using Exam.Domain.Entities;
using Exam.Infrastructure.Mapping;

namespace Exam.Infrastructure;

public static class DiExtensions {
    public static IServiceCollection AddCommonServices(this IServiceCollection services, IConfiguration config) {
        services.AddAutoMapper(typeof(CategoryMapping).Assembly);
        services.AddAutoMapper(typeof(UserMapping).Assembly);
        //        services.AddScoped<AuthorizationReferencesMessageHandler>();
        //           services.AddScoped<IUnitOfWork, UnitOfWork>();

        #region Repositories

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICrudRepository<CategoryEntity>, CategoryRepository>();

        services.AddScoped<ILeagueRepository, LeagueRepository>();
        services.AddScoped<ICrudRepository<LeagueEntity>, LeagueRepository>();


        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICrudRepository<UserEntity>, UserRepository>();

        //        services.AddScoped<IRefMonthRepository, RefMonthRepository>();
        //        services.AddScoped<ICrudRepository<RefMonthEntity>, RefMonthRepository>();
        //        
        //        services.AddScoped<IRefUnitRepository, RefUnitRepository>();
        //        services.AddScoped<ICrudRepository<RefUnitEntity>, RefUnitRepository>();
        //
        //        services.AddScoped<IRefTradeMethodRepository, RefTradeMethodRepository>();
        //        services.AddScoped<ICrudRepository<RefTradeMethodEntity>, RefTradeMethodRepository>();


        #endregion

        #region APIs

        //        services.AddRefitClient<IReferencesApi>().ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler(){
        //                AllowAutoRedirect = false,
        //                ServerCertificateCustomValidationCallback = (message, cert, chain, sslErrors) => true
        //            })
        //           .ConfigureHttpClient(c =>{
        //                c.BaseAddress = new Uri(config["ReferencesApi:ApiEndpoint"] ?? string.Empty);
        //                c.Timeout = TimeSpan.FromMinutes(30);
        //            }).AddHttpMessageHandler<AuthorizationReferencesMessageHandler>();

        #endregion

        #region Handlers

        services.AddTransient<IStrategy, CategoryStrategy>();
        services.AddTransient<IStrategy, LeagueStrategy>();
        services.AddTransient<Context>();
        //        services.AddTransient<ExtractRefTradeMethodHandler>();

        #endregion

        #region Services

        services.AddScoped<ManagerService>();
        services.AddScoped<UserService>();

        #endregion

        return services;
    }

}
