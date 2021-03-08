//using AspNetCoreNuxt.Domains.Entities;
//using AspNetCoreNuxt.Extensions.AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Diagnostics;
//using System.Collections.Generic;
//using System.Data.Common;
//using System.Dynamic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.UseCases
//{
//    public partial class UserUseCase
//    {
//        ///// <summary>
//        ///// 指定されたユーザー ID をもとにユーザー情報を非同期に取得します。
//        ///// </summary>
//        ///// <typeparam name="T">取得したデータをマップするクラスの型。</typeparam>
//        ///// <param name="id">ユーザー ID。</param>
//        ///// <param name="fields">取得する項目のプロパティを示す文字列のコレクション。</param>
//        ///// <returns><typeparamref name="T"/> オブジェクト。</returns>
//        //public Task<T> GetUserAsync<T>(int id, IEnumerable<string> fields)
//        //    => Context.Set<UserEntity>()
//        //              .Where(x => x.Id == id)
//        //              .ProjectTo<T>(Mapper.ConfigurationProvider, fields)
//        //              .SingleOrDefaultAsync();

//        public Task<T> GetUserAsync<T>(int id, IEnumerable<string> fields)
//        {
//            var entity = new UserEntity()
//            {
//                Name = "xxx",
//                PasswordHash = "10",
//            };

//            var dic = Mapper.Map<Dictionary<string, object>>(entity);
//            var exp = Mapper.Map<ExpandoObject>(entity);

//            return
//                Context.Set<UserEntity>()
//                      .Where(x => x.Id == id)
//                      .ProjectTo<T>(Mapper.ConfigurationProvider, fields)
//                      .SingleOrDefaultAsync();
//        }
//    }
//}
