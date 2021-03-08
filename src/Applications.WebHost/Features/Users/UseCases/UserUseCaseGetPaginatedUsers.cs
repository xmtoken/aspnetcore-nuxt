//using AspNetCoreNuxt.Domains.Entities;
//using AspNetCoreNuxt.Extensions.AutoMapper;
//using AspNetCoreNuxt.Extensions.Collections;
//using AspNetCoreNuxt.Extensions.Collections.Generic;
//using AspNetCoreNuxt.Extensions.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.UseCases
//{
//    public partial class UserUseCase
//    {
//        ///// <summary>
//        ///// 指定された情報をもとにページングされたユーザー情報のコレクションを非同期に取得します。
//        ///// </summary>
//        ///// <typeparam name="T">取得したデータをマップするクラスの型。</typeparam>
//        ///// <param name="specification">シーケンスをフィルターする仕様を表す <see cref="ILinqSpecification{T}"/> オブジェクト。</param>
//        ///// <param name="sortings">ソート条件を表す <see cref="IQueryableSorting{T}"/> オブジェクトのコレクション。</param>
//        ///// <param name="paging">ページング条件を表す <see cref="IPaging"/> オブジェクト。</param>
//        ///// <param name="fields">取得する項目のプロパティを示す文字列のコレクション。</param>
//        ///// <returns><see cref="IPagination{T}"/> オブジェクト。</returns>
//        //public Task<IPagination<T>> GetPaginatedUsersAsync<T>(ILinqSpecification<UserEntity> specification, IEnumerable<IQueryableSorting<T>> sortings, IPaging paging, IEnumerable<string> fields)
//        //    => Context.Set<UserEntity>()
//        //              .SatisfiedBy(specification)
//        //              .ProjectTo<T>(Mapper.ConfigurationProvider, sortings.Select(x => x.SortPropertyName).Concat(fields))
//        //              .OrderBy(sortings)
//        //              .ProjectTo<T>(Mapper.ConfigurationProvider, fields)
//        //              .PaginateAsync(paging);
//    }
//}
