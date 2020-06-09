using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.Controllers
{
    public partial class UsersController
    {
        /// <summary>
        /// 指定されたユーザー ID のコレクションをもとにユーザー情報を非同期に削除します。
        /// </summary>
        /// <param name="ids">ユーザー ID のコレクション。</param>
        /// <returns><see cref="Task"/> オブジェクト。</returns>
        [Authorize]
        [HttpDelete]
        public async Task Delete([FromQuery] IEnumerable<int> ids)
            => await UseCase.RemoveUsersAsync(ids);
    }
}
