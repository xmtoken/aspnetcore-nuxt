using AspNetCoreNuxt.Domains;
using AspNetCoreNuxt.Domains.Data.DbSetExtensions;
using AspNetCoreNuxt.Domains.Entities;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Account.UseCases
{
    public partial class AccountUseCase
    {
        /// <summary>
        /// 指定されたユーザー名をもとにユーザーの権限のコレクションを非同期に取得します。
        /// </summary>
        /// <param name="userName">ユーザー名。</param>
        /// <returns>権限を表す <see cref="Permission"/> 値のコレクション。</returns>
        public Task<Permission[]> GetPermissionsAsync(string userName)
            => Context.Set<UserEntity>().GetPermissionsAsync(userName);
    }
}
