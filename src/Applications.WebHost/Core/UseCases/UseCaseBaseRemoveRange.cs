using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Core.UseCases
{
    public partial class UseCaseBase<T>
    {
        /// <summary>
        /// 指定された主キーに一致するデータを非同期に削除します。
        /// </summary>
        /// <param name="keyValuesCollection">削除するデータの主キー値のコレクション。</param>
        /// <returns><see cref="Task"/> オブジェクト。</returns>
        public virtual Task RemoveRangeAsync(object[][] keyValuesCollection)
            => RemoveRangeCoreAsync(keyValuesCollection);

        /// <summary>
        /// 指定された主キーに一致するデータを非同期に削除します。
        /// </summary>
        /// <param name="keyValuesCollection">削除するデータの主キー値のコレクション。</param>
        /// <returns><see cref="Task"/> オブジェクト。</returns>
        protected async Task RemoveRangeCoreAsync(object[][] keyValuesCollection)
        {
            // https://docs.microsoft.com/ja-jp/ef/core/miscellaneous/connection-resiliency
            var strategy = Context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await Context.Database.BeginTransactionAsync();

                foreach (var keyValues in keyValuesCollection)
                {
                    await RemoveAsync(keyValues);
                }

                await transaction.CommitAsync();
            });
        }
    }
}
