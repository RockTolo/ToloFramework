using Volo.Abp.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace ToloFramework.Data;

public class ToloFrameworkDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public ToloFrameworkDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        
        /* We intentionally resolving the ToloFrameworkDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<ToloFrameworkDbContext>()
            .Database
            .MigrateAsync();

    }
}
