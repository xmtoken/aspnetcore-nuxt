using AspNetCoreNuxt.Applications.WebHost.Features.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.Controllers
{
    public partial class UsersController
    {
        /// <summary>
        /// 指定された情報をもとにユーザーのパスワードを非同期に更新します。
        /// </summary>
        /// <param name="id">ユーザー ID。</param>
        /// <param name="model">ユーザーのパスワードの更新情報を表す <see cref="UserUpdatePasswordModel"/> オブジェクト。</param>
        /// <returns><see cref="IActionResult"/> オブジェクト。</returns>
        [Authorize]
        [HttpPatch]
        [Route("{id:int}/[action]")]
        public async Task<IActionResult> Password([FromRoute] int id, [FromBody] UserUpdatePasswordModel model)
        {
            var succeeded = await UseCase.UpdatePasswordAsync(id, model);
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
