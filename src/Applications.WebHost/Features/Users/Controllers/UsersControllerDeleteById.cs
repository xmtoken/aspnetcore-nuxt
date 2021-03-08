//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;

//namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.Controllers
//{
//    public partial class UsersController
//    {
//        /// <summary>
//        /// 指定されたユーザー ID をもとにユーザー情報を非同期に削除します。
//        /// </summary>
//        /// <param name="id">ユーザー ID。</param>
//        /// <returns><see cref="Task"/> オブジェクト。</returns>
//        [Authorize]
//        [HttpDelete]
//        [Route("{id:int}")]
//        public async Task Delete([FromRoute] int id)
//            => await UseCase.RemoveUserAsync(id);
//    }
//}
