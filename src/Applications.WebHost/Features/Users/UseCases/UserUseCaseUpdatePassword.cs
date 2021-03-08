//using AspNetCoreNuxt.Applications.WebHost.Features.Users.Models;
//using AspNetCoreNuxt.Domains.Data.DbSetExtensions;
//using AspNetCoreNuxt.Domains.Entities;
//using Microsoft.EntityFrameworkCore;
//using Polly;
//using System.Threading.Tasks;

//namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.UseCases
//{
//    public partial class UserUseCase
//    {
//        /// <summary>
//        /// 指定された情報をもとにユーザーのパスワードを非同期に更新します。
//        /// </summary>
//        /// <param name="id">ユーザー ID。</param>
//        /// <param name="model">ユーザーのパスワードの更新情報を表す <see cref="UserUpdatePasswordModel"/> オブジェクト。</param>
//        /// <returns>ユーザーのパスワードの更新に成功した場合は true。それ以外の場合は false。</returns>
//        public async Task<bool> UpdatePasswordAsync(int id, UserUpdatePasswordModel model)
//        {
//            var entity = await Context.Set<UserEntity>().FindByIdAsync(id, QueryTrackingBehavior.TrackAll);
//            if (entity == null)
//            {
//                return false;
//            }

//            entity.PasswordHash = Cryptography.HashValue(model.NewPassword);

//            return await Policy<bool>
//                .Handle<DbUpdateConcurrencyException>()
//                .FallbackAsync(false)
//                .ExecuteAsync(async () => await Context.SaveChangesAsync() != 0);
//        }
//    }
//}
