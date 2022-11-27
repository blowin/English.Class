using English.Class.Domain.Core;
using English.Class.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace English.Class.DependencyInjection
{
    public static class ServiceCollectionExt
    {
        public static IServiceCollection AddAppServices(this IServiceCollection self, Action<DbContextOptionsBuilder> optionsAction)
        {
            self.AddDbContextPool<AppDbContext>(optionsAction);
            self.Scan(selector =>
            {
                selector.FromAssemblies(typeof(AppDbContext).Assembly, typeof(Entity).Assembly)
                    .AddClasses(filter => filter.AssignableTo<IRepository>()).AsImplementedInterfaces()
                    .AddClasses(filter => filter.AssignableTo<ITransientService>()).AsSelfWithInterfaces().WithTransientLifetime()
                    .AddClasses(filter => filter.AssignableTo<IScopedService>()).AsSelfWithInterfaces().WithScopedLifetime()
                    .AddClasses(filter => filter.AssignableTo<ISingletonService>()).AsSelfWithInterfaces().WithSingletonLifetime()
                    ;
            });
            return self;
        }
    }
}