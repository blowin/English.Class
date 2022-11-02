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
                selector.FromAssemblyOf<AppDbContext>()
                    .AddClasses(filter => filter.AssignableTo<IRepository>()).AsImplementedInterfaces();
            });
            return self;
        }
    }
}