using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonBook.Application.Infrastructure;
using PersonBook.Domain.CityAggregate;
using PersonBook.Domain.CityAggregate.Repositories;
using PersonBook.Domain.PersonAggregate;
using PersonBook.Domain.PersonAggregate.Repositories;
using PersonBook.Domain.PersonRelationAggregate.Repositories;
using PersonBook.Domain.PhotoAggregate.Repositories;
using PersonBook.Infrastructure.Db;
using PersonBook.Infrastructure.EvnetDispatching;
using PersonBook.Infrastructure.Repositories;

namespace PersonBook.DI
{
    public class DefaultDependencyResolver
    {
        public DefaultDependencyResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IConfiguration _configuration { get; }

        public IServiceCollection Resolve(IServiceCollection services)
        {
            services ??= new ServiceCollection();

            var connectionString = _configuration.GetConnectionString("PersonBookDbContext");

            services.AddDbContext<PersonBookDbContext>(options => options.UseSqlServer(connectionString).UseLazyLoadingProxies());

            services.AddScoped(x => new InternalDomainEventDispatcher(
                services.BuildServiceProvider(),
                typeof(Person).Assembly,
                typeof(Command).Assembly));

            services.AddScoped(x => new InternalDomainEventDispatcher(
               services.BuildServiceProvider(),
               typeof(City).Assembly,
               typeof(Command).Assembly));

            services.AddScoped<CommandExecutor>();
            services.AddScoped<QueryExecutor>();
            services.AddScoped<UnitOfWork>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IPersonRelationshipRepository, PersonRelationshipRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();

            services.AddHttpContextAccessor();

            services.AddTransient<ApplicationContext>();

            return services;
        }
    }
}
