//using AspNetCoreNuxt.Applications.WebHost.Features.Users.Models;
//using AspNetCoreNuxt.Domains.Data.DbSetExtensions;
//using AspNetCoreNuxt.Domains.Entities;
//using Microsoft.EntityFrameworkCore;
//using Polly;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AspNetCoreNuxt.Applications.WebHost.Features.Users.UseCases
//{
//    public partial class UserUseCase
//    {
//        /// <summary>
//        /// 指定された情報をもとにユーザー情報を非同期に更新します。
//        /// </summary>
//        /// <param name="id">ユーザー ID。</param>
//        /// <param name="model">ユーザーの更新情報を表す <see cref="UserUpdateModel"/> オブジェクト。</param>
//        /// <returns>ユーザー情報の更新に成功した場合は true。それ以外の場合は false。</returns>
//        public async Task<bool> UpdateUserAsync(int id, UserUpdateModel model)
//        {
//            var entity = await Context.Set<UserEntity>().FindByIdAsync(id, QueryTrackingBehavior.TrackAll);
//            if (entity == null)
//            {
//                return false;
//            }

//            await Context.Entry(entity).Reference(x => x.Profile).LoadAsync();
//            entity.Profile.EmailAddress = model.EmailAddress;
//            entity.Profile.NormalizedEmailAddress = LookupNormalizer.NormalizeEmail(model.EmailAddress);
//            entity.Profile.Birthday = model.Birthday.Value;
//            entity.Profile.Gender = model.Gender.Value;

//            await Context.Entry(entity).Collection(x => x.Roles).LoadAsync();
//            entity.Roles.Clear();
//            entity.Roles = model.Roles?.Select(roleId => new UserRoleEntity()
//            {
//                RoleId = roleId,
//            }).ToList();

//            return await Policy<bool>
//                .Handle<DbUpdateConcurrencyException>()
//                .FallbackAsync(false)
//                .ExecuteAsync(async () => await Context.SaveChangesAsync() != 0);
//        }
//    }
//}
