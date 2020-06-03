using AspNetCoreNuxt.Extensions.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Roles.Controllers
{
    public partial class RolesController
    {
        /// <summary>
        /// ロール情報のコレクションを非同期に取得します。
        /// </summary>
        /// <returns><see cref="TextValuePair{TValue}"/> オブジェクトのコレクション。</returns>
        [Authorize]
        [HttpGet]
        public Task<TextValuePair<int>[]> Get()
            => UseCase.GetRolesAsync<TextValuePair<int>>();
    }
}
