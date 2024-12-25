using EFCore.BulkExtensions;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tolo.EfCoreBulkExtensions
{
    public class EfCoreBulkOperationProvider : IEfCoreBulkOperationProvider, ITransientDependency
    {
        private readonly IAuditPropertySetter _auditPropertySetter;
        private readonly IDataFilter _dataFilter;
        private readonly EfCoreBulkOptions _options;

        public EfCoreBulkOperationProvider(
            IAuditPropertySetter auditPropertySetter,
            IDataFilter dataFilter,
            IOptions<EfCoreBulkOptions> options)
        {
            _auditPropertySetter = auditPropertySetter;
            _dataFilter = dataFilter;
            _options = options.Value;
        }

        public async Task DeleteManyAsync<TDbContext, TEntity>(
            IEfCoreRepository<TEntity> repository,
            IEnumerable<TEntity> entities,
            bool autoSave,
            CancellationToken cancellationToken)
                where TDbContext : IEfCoreDbContext
                where TEntity : class, IEntity

        {
            var dbContext = await repository.GetDbContextAsync();

            if (!_options.IsEnabled || _options.EnabledThreshold > entities.Count())
            {
                dbContext.RemoveRange(entities);
            }
            else 
            {
                if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)) && _dataFilter.IsEnabled<ISoftDelete>())
                {
                    foreach (var entity in entities)
                    {
                        _auditPropertySetter.SetDeletionProperties(entity);
                    }
                    await dbContext.BulkUpdateAsync(entities, cancellationToken: cancellationToken);
                }
                else
                {
                    await dbContext.BulkDeleteAsync(entities, cancellationToken: cancellationToken);
                }
            }          
            
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

            if (!_options.IsEnabled || _options.EnabledThreshold > entities.Count())
            {
                await dbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
            }
            else 
            {
                foreach (var entity in entities)
                {
                    _auditPropertySetter.SetCreationProperties(entity);
                }
                await dbContext.BulkInsertAsync(entities, cancellationToken: cancellationToken);
            }
           
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

            if (!_options.IsEnabled || _options.EnabledThreshold > entities.Count())
            {
                dbContext.Set<TEntity>().UpdateRange(entities);
            }
            else 
            {
                foreach (var entity in entities)
                {
                    _auditPropertySetter.SetModificationProperties(entity);
                }
                await dbContext.BulkUpdateAsync(entities, cancellationToken: cancellationToken);
            }
            
            if (autoSave)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
