using AspNetCoreNuxt.Extensions.EntityFrameworkCore;
using AspNetCoreNuxt.Extensions.Polly;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Core.UseCases
{
    public partial class UseCaseBase<T>
    {
        /// <summary>
        /// 指定された主キーに一致するデータを非同期に削除します。
        /// </summary>
        /// <param name="keyValues">削除するデータの主キー値のコレクション。</param>
        /// <returns><see cref="Task"/> オブジェクト。</returns>
        public virtual Task RemoveAsync(object[] keyValues)
            => RemoveCoreAsync(keyValues);

        /// <summary>
        /// 指定された主キーに一致するデータを非同期に削除します。
        /// </summary>
        /// <param name="keyValues">削除するデータの主キー値のコレクション。</param>
        /// <returns><see cref="Task"/> オブジェクト。</returns>
        protected async Task RemoveCoreAsync(object[] keyValues)
        {
            var entity = await Context.Set<T>().FindAsync(keyValues);
            if (entity is null)
            {
                return;
            }

            Context.Remove(entity);

            await Policy
                .Handle<DbUpdateConcurrencyException>()
                .RetryForeverAsync<DbUpdateConcurrencyException>(ex => ex.DetachEntries())
                .ExecuteAsync(() => Context.SaveChangesAsync());
        }
    }
}
