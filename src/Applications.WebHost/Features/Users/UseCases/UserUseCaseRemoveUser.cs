using AspNetCoreNuxt.Domains.Data.DbSetExtensions;
using AspNetCoreNuxt.Domains.Entities;
using AspNetCoreNuxt.Extensions.EntityFrameworkCore;
using AspNetCoreNuxt.Extensions.Polly;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.UseCases
{
    public partial class UserUseCase
    {
        /// <summary>
        /// 指定されたユーザー ID をもとにユーザー情報を非同期に削除します。
        /// </summary>
        /// <param name="id">ユーザー ID。</param>
        /// <returns><see cref="Task"/> オブジェクト。</returns>
        public async Task RemoveUserAsync(int id)
        {
            var entity = await Context.Set<UserEntity>().FindByIdAsync(id, QueryTrackingBehavior.TrackAll);
            if (entity == null)
            {
                return;
            }

            Context.Set<UserEntity>().Remove(entity);
            await Policy
                .Handle<DbUpdateConcurrencyException>()
                .RetryForeverAsync<DbUpdateConcurrencyException>(ex => ex.DetachEntries())
                .ExecuteAsync(() => Context.SaveChangesAsync());
        }
    }
}
