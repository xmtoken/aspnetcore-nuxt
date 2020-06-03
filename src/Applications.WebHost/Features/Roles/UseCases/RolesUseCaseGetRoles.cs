using AspNetCoreNuxt.Domains.Entities;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Roles.UseCases
{
    public partial class RolesUseCase
    {
        /// <summary>
        /// ロール情報のコレクションを非同期に取得します。
        /// </summary>
        /// <typeparam name="T">取得したデータをマップするクラスの型。</typeparam>
        /// <returns><typeparamref name="T"/> オブジェクトのコレクション。</returns>
        public Task<T[]> GetRolesAsync<T>()
            => Context.Set<RoleEntity>()
                      .ProjectTo<T>(Mapper.ConfigurationProvider)
                      .ToArrayAsync();
    }
}
