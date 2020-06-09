using AspNetCoreNuxt.Applications.WebHost.Features.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.Controllers
{
    public partial class UsersController
    {
        /// <summary>
        /// 指定された情報をもとにユーザー情報を非同期に更新します。
        /// </summary>
        /// <param name="id">ユーザー ID。</param>
        /// <param name="model">ユーザーの更新情報を表す <see cref="UserUpdateModel"/> オブジェクト。</param>
        /// <returns><see cref="IActionResult"/> オブジェクト。</returns>
        [Authorize]
        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] UserUpdateModel model)
        {
            var succeeded = await UseCase.UpdateUserAsync(id, model);
            if (succeeded)
            {
                return Ok();
            }
            else
            {
                _ = TryValidateModel(model);
                return ValidationProblem();
            }
        }
    }
}
