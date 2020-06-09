using AspNetCoreNuxt.Domains.Entities;
using AspNetCoreNuxt.Extensions.AutoMapper;
using AspNetCoreNuxt.Extensions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.UseCases
{
    public partial class UserUseCase
    {
        /// <summary>
        /// 指定された情報をもとにユーザー情報のコレクションを非同期に取得します。
        /// </summary>
        /// <typeparam name="T">取得したデータをマップするクラスの型。</typeparam>
        /// <param name="specification">シーケンスをフィルターする仕様を表す <see cref="ILinqSpecification{T}"/> オブジェクト。</param>
        /// <param name="sorting">ソート条件を表す <see cref="IQueryableSorting{T}"/> オブジェクト。</param>
        /// <param name="fields">取得する項目のプロパティを示す文字列のコレクション。</param>
        /// <returns><typeparamref name="T"/> オブジェクトのコレクション。</returns>
        public Task<T[]> GetUsersAsync<T>(ILinqSpecification<UserEntity> specification, IQueryableSorting<T> sorting, IEnumerable<string> fields)
            => Context.Set<UserEntity>()
                      .SatisfiedBy(specification)
                      .ProjectTo<T>(Mapper.ConfigurationProvider, fields != null ? fields.Append(sorting.SortPropertyName) : new[] { sorting.SortPropertyName })
                      .OrderBy(sorting)
                      .ProjectTo<T>(Mapper.ConfigurationProvider, fields)
                      .ToArrayAsync();
    }
}
