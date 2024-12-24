using EFCore.BulkExtensions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tolo.EfCoreBulkExtensions
{
    public class EfCoreBulkOperationProvider : IEfCoreBulkOperationProvider, ITransientDependency
    {
        public async Task DeleteManyAsync<TDbContext, TEntity>(
            IEfCoreRepository<TEntity> repository,
            IEnumerable<TEntity> entities,
            bool autoSave,
            CancellationToken cancellationToken)
                where TDbContext : IEfCoreDbContext
                where TEntity : class, IEntity

        {
            var dbContext = await repository.GetDbContextAsync();
            await dbContext.BulkDeleteAsync(entities, cancellationToken: cancellationToken);
            if (autoSave)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task InsertManyAsync<TDbContext, TEntity>(
            IEfCoreRepository<TEntity> repository,
            IEnumerable<TEntity> entities,
            bool autoSave,
            CancellationToken cancellationToken)
                where TDbContext : IEfCoreDbContext
                where TEntity : class, IEntity
        {
            var dbContext = await repository.GetDbContextAsync();
            await dbContext.BulkInsertAsync(entities, cancellationToken: cancellationToken);
            if (autoSave)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UpdateManyAsync<TDbContext, TEntity>(
            IEfCoreRepository<TEntity> repository,
            IEnumerable<TEntity> entities,
            bool autoSave,
            CancellationToken cancellationToken)
                where TDbContext : IEfCoreDbContext
                where TEntity : class, IEntity
        {
            var dbContext = await repository.GetDbContextAsync();
            await dbContext.BulkUpdateAsync(entities, cancellationToken: cancellationToken);
            if (autoSave)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
