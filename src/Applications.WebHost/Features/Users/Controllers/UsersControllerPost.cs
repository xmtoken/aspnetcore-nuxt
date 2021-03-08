//using AspNetCoreNuxt.Applications.WebHost.Features.Users.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;

//namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.Controllers
//{
//    public partial class UsersController
//    {
//        /// <summary>
//        /// 指定された情報をもとにユーザー情報を非同期に登録します。
//        /// </summary>
//        /// <param name="model">ユーザーの登録情報を表す <see cref="UserCreateModel"/> オブジェクト。</param>
//        /// <returns><see cref="IActionResult"/> オブジェクト。</returns>
//        [Authorize]
//        [HttpPost]
//        public async Task<IActionResult> Post([FromBody] UserCreateModel model)
//        {
//            var (succeeded, id) = await UseCase.AddUserAsync(model);
//            if (succeeded)
//            {
//                return CreatedAtAction(nameof(GetById), new { id }, value: null);
//            }
//            else
//            {
//                _ = TryValidateModel(model);
//                return ValidationProblem();
//            }
//        }
//    }
//}
