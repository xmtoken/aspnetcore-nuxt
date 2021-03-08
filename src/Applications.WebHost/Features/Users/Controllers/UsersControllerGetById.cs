//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.Controllers
//{
//    public partial class UsersController
//    {
//        ///// <summary>
//        ///// 指定されたユーザー ID をもとにユーザー情報を非同期に取得します。
//        ///// </summary>
//        ///// <param name="id">ユーザー ID。</param>
//        ///// <param name="fields">取得する項目のプロパティを示す文字列のコレクション。</param>
//        ///// <returns><see cref="User"/> オブジェクト。</returns>
//        //[Authorize]
//        //[HttpGet]
//        //[Route("{id:int}")]
//        //public Task<User> GetById([FromRoute] int id, [FromQuery] IEnumerable<string> fields)
//        //    => UseCase.GetUserAsync<User>(id, fields);

//        [Authorize]
//        [HttpGet]
//        [Route("{id:int}")]
//        public Task<Dictionary<string, object>> GetById([FromRoute] int id, [FromQuery] IEnumerable<string> fields)
//        {
//            return UseCase.GetUserAsync<Dictionary<string, object>>(id, fields);
//        }
//    }
//}
