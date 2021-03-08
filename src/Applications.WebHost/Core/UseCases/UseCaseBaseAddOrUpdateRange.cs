using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Core.UseCases
{
    public partial class UseCaseBase<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public virtual Task<(bool Succeeded, T[] Entities)> AddOrUpdateRangeAsync((object[] KeyValues, T Model)[] values)
            => AddOrUpdateRangeCoreAsync(values);

        protected async Task<(bool Succeeded, T[] Entities)> AddOrUpdateRangeCoreAsync((object[] KeyValues, T Model)[] values)
        {
            // https://docs.microsoft.com/ja-jp/ef/core/miscellaneous/connection-resiliency
            var strategy = Context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await Context.Database.BeginTransactionAsync();

                var entities = new List<T>();

                foreach (var value in values)
                {
                    var result = await AddOrUpdateAsync(value.KeyValues, value.Model);
                    if (result.Succeeded)
                    {
                        entities.Add(result.Entity);
                        continue;
                    }
                    return (Succeeded: false, Entities: null);
                }

                await transaction.CommitAsync();

                return (Succeeded: true, Entities: entities.ToArray());
            });
        }
    }
}
