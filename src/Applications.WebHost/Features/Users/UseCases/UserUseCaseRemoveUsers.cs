using AspNetCoreNuxt.Domains.Entities;
using AspNetCoreNuxt.Extensions.EntityFrameworkCore;
using AspNetCoreNuxt.Extensions.Polly;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.UseCases
{
    public partial class UserUseCase
    {
        /// <summary>
        /// 指定されたユーザー ID のコレクションをもとにユーザー情報を非同期に削除します。
        /// </summary>
        /// <param name="ids">ユーザー ID のコレクション。</param>
        /// <returns><see cref="Task"/> オブジェクト。</returns>
        public async Task RemoveUsersAsync(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return;
            }

            var entities = await Context.Set<UserEntity>().Where(x => ids.Contains(x.Id)).ToArrayAsync();
            if (entities.Length == 0)
            {
                return;
            }

            Context.Set<UserEntity>().RemoveRange(entities);
            await Policy
                .Handle<DbUpdateConcurrencyException>()
                .RetryForeverAsync<DbUpdateConcurrencyException>(ex => ex.DetachEntries())
                .ExecuteAsync(() => Context.SaveChangesAsync());
        }
    }
}
