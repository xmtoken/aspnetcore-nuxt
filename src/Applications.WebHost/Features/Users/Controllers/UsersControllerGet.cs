using AspNetCoreNuxt.Applications.WebHost.Core.Entities;
using AspNetCoreNuxt.Applications.WebHost.Core.Models;
using AspNetCoreNuxt.Applications.WebHost.Features.Users.Models;
using AspNetCoreNuxt.Extensions.Collections;
using AspNetCoreNuxt.Extensions.Collections.Generic;
using AspNetCoreNuxt.Extensions.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.Controllers
{
    public partial class UsersController
    {
        /// <summary>
        /// 指定された情報をもとにページングされたユーザー情報のコレクションを非同期に取得します。
        /// </summary>
        /// <param name="conditions">検索条件を表す <see cref="UserSearchConditions"/> オブジェクト。</param>
        /// <param name="sorting">ソート条件を表す <see cref="Sorting"/> オブジェクト。</param>
        /// <param name="paging">ページング条件を表す <see cref="Paging"/> オブジェクト。</param>
        /// <param name="fields">取得する項目のプロパティを示す文字列のコレクション。</param>
        /// <returns><see cref="IPagination{T}"/> オブジェクト。</returns>
        [Authorize]
        [HttpGet]
        public Task<IPagination<User>> Get([FromQuery] UserSearchConditions conditions, [FromQuery] Sorting sorting, [FromQuery] Paging paging, [FromQuery] IEnumerable<string> fields)
        {
            var specification = SearchSpecificationFactory.Create(conditions);
            var sort = sorting.ToQueryableOrDefault<User>(x => x.Id, SortDirection.Descending);
            return UseCase.GetPaginatedUsersAsync(specification, sort, paging, fields);
        }
    }
}
